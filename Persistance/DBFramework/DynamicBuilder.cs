using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

using DBFramework.Mapping;

namespace DBFramework
{
    /// <remarks>
    /// �����ִ��Ч��̫��
    /// T entity = new T();
    /// foreach (string propertyName in tableInfo.Columns.Keys)
    /// {
    ///     int index = dictionary[propertyName];
    ///     if (index != -1
    ///         && dataReader.GetValue(index) != DBNull.Value)
    ///     {
    ///         object value = dataReader.GetValue(index);
    ///         SqlUtility.SetValue(entity, propertyName, value);
    ///     }
    /// }
    /// </remarks>
    public class DynamicBuilder<T>
    {
        private delegate T DataRecord2Entity(IDataRecord dataRecord);

        private static readonly MethodInfo getItemMethod = typeof(IDataRecord).GetMethod("get_Item", new Type[] { typeof(int) });
        private static readonly MethodInfo isDBNullMethod = typeof(IDataRecord).GetMethod("IsDBNull", new Type[] { typeof(int) });        

        // ����ִ�ж�̬������һ��ί�� ������IDataRecord�ӿ�
        private DataRecord2Entity dataRecord2Entity;

        /// ���캯��
        public DynamicBuilder(IDataRecord dataRecord) 
        {
            Prepare(dataRecord);
        }

        /// Ϊ�˽�ʡʱ��, ���Ե���һ�� GetOrdinal, Ȼ�󽫽����������������Ա���ѭ����ʹ�á�
        private Dictionary<string, int> Match(IDataRecord dataRecord)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            TableInfo<T> tableInfo = new TableInfo<T>();

            foreach (string propertyName in tableInfo.Columns.Keys)
            {
                int index = -1;

                try
                {
                    ColumnAttribute columnAttribute = tableInfo.Columns[propertyName];

                    if (columnAttribute != null)
                    {
                        index = dataRecord.GetOrdinal(columnAttribute.Name);
                    }
                    else
                    {
                        index = dataRecord.GetOrdinal(propertyName);
                    }

                    dictionary.Add(propertyName, index);
                }
                catch (IndexOutOfRangeException)
                {
                    // ��ָ�������Ʋ�����Ч�������ƣ������������������ݿ��ֶ����Ʋ�ƥ��
                }
            }

            return dictionary;
        }


        private void Prepare(IDataRecord dataRecord)
        {
            // ����һ����̬�������������� typeof(T)���������� typeof(IDataRecord)
            Type type = typeof(T);
            string name = string.Format("Create{0}", type.Name);
            DynamicMethod dynamicMethod;

            if (DynamicMethodContainer.IsExist(name))
            {
                dynamicMethod = DynamicMethodContainer.Get(name);
            }
            else
            {
                Dictionary<string, int> dictionary = Match(dataRecord);

                dynamicMethod = new DynamicMethod(
                    name,
                    type,
                    new Type[] { typeof(IDataRecord) },
                    type,
                    true);

                // ����һ��MSIL��������Ϊ��̬�������ɴ���
                ILGenerator generator = dynamicMethod.GetILGenerator();

                // ����ָ�����͵ľֲ��������������Ϊ T t;
                LocalBuilder localBuilder = generator.DeclareLocal(type);

                // �������Ϊ T t = new T(); The next piece of code instantiates the requested type of object and stores it in the local variable. 
                ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes);
                generator.Emit(OpCodes.Newobj, constructorInfo);
                generator.Emit(OpCodes.Stloc, localBuilder);

                foreach (string propertyName in dictionary.Keys)
                {
                    PropertyInfo propertyInfo = type.GetProperty(propertyName);
                    int index = dictionary[propertyName];

                    // �����ǩ
                    Label endIfLabel = generator.DefineLabel();

                    generator.Emit(OpCodes.Ldarg_0);
                    generator.Emit(OpCodes.Ldc_I4, index);

                    // ���� Callvirt : dataRecord.IsDBNull() ����                    
                    generator.Emit(OpCodes.Callvirt, isDBNullMethod);
                    // ����� DataRecord.IsDBNull() == true �� contine
                    generator.Emit(OpCodes.Brtrue, endIfLabel);

                    generator.Emit(OpCodes.Ldloc, localBuilder);
                    generator.Emit(OpCodes.Ldarg_0);
                    generator.Emit(OpCodes.Ldc_I4, index);

                    // ���� Callvirt : dataRecord.get_Item(i) ����
                    generator.Emit(OpCodes.Callvirt, getItemMethod);
                    generator.Emit(OpCodes.Unbox_Any, dataRecord.GetFieldType(index));

                    // ���� Callvirt : propertyInfo.GetSetMethod ����
                    generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());

                    // �ڵ�ǰ MSIL ��λ�ý��б��
                    generator.MarkLabel(endIfLabel);
                }

                // The last part of the code returns the value of the local variable
                generator.Emit(OpCodes.Ldloc, localBuilder);

                // ��������
                generator.Emit(OpCodes.Ret);

                DynamicMethodContainer.Add(name, dynamicMethod);
            }

            // ��ɶ�̬�����Ĵ���������ִ�иö�̬������ί��
            // DataRecord2Entity �� Build ������ʵ��
            this.dataRecord2Entity = (DataRecord2Entity)dynamicMethod.CreateDelegate(typeof(DataRecord2Entity));
        }

        /// ִ�� DataRecord2Entity �ķ���
        public T Build(IDataRecord dataRecord)
        {
            return this.dataRecord2Entity(dataRecord);
        }        
    }
}
