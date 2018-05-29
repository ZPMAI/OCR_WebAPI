using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

using DBFramework.Mapping;

namespace DBFramework.MSIL
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
    public class DynamicBuilder
    {
        private delegate object DataRecord2Entity(IDataRecord dataRecord);

        private static readonly MethodInfo getItemMethod = typeof(IDataRecord).GetMethod("get_Item", new Type[] { typeof(int) });
        private static readonly MethodInfo IsDBNullMethod = typeof(IDataRecord).GetMethod("IsDBNull", new Type[] { typeof(int) });

        private DataRecord2Entity dataRecord2Entity;
        private Type type;

        /// ���캯��
        public DynamicBuilder(Type type, IDataRecord dataRecord)
        {
            this.type = type;

            PrepareDelegate(dataRecord);
        }


        /// Ϊ�˽�ʡʱ��, ���Ե���һ�� GetOrdinal, Ȼ�󽫽����������������Ա���ѭ����ʹ�á�
        private Dictionary<int, string> Match(IDataRecord dataRecord)
        {
            Dictionary<int, string> dictionary = new Dictionary<int, string>();
            MappingInfo mappingInfo = new MappingInfo(this.type);

            for (int i = 0; i < dataRecord.FieldCount; i++)
            {
                string columnName = dataRecord.GetName(i);

                if (mappingInfo.ContainsColumn(columnName))
                {
                    dictionary.Add(i, mappingInfo.GetPropertyName(columnName));
                }
            }

            return dictionary;
        }


        /// ����һ����̬�������������� typeof(T)���������� typeof(IDataRecord)
        /// <remarks>
        /// if (DynamicMethodContainer.IsExist(name))
        /// {
        ///     DynamicMethod dynamicMethod = DynamicMethodContainer.Get(name);
        /// }
        /// else
        /// {
        ///     DynamicMethodContainer.Add(name, dynamicMethod);
        /// }
        /// </remarks>
        protected void PrepareDelegate(IDataRecord dataRecord)
        {
            string name = string.Format("Create{0}", this.type.Name);
            Type[] parameterTypes = new Type[] { typeof(IDataRecord) };
            DynamicMethod dynamicMethod = new DynamicMethod(name, this.type, parameterTypes, this.type, true);

            // ����һ��MSIL��������Ϊ��̬�������ɴ���
            ILGenerator generator = dynamicMethod.GetILGenerator();

            // ����ָ�����͵ľֲ��������������Ϊ T t;
            LocalBuilder localBuilder = generator.DeclareLocal(this.type);

            // PUSH : T t = new T();
            // Instantiates the requested type of object and stores it in the local variable. 
            ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes);
            generator.Emit(OpCodes.Newobj, constructorInfo);
            generator.Emit(OpCodes.Stloc, localBuilder);

            Dictionary<int, string> dictionary = this.Match(dataRecord);

            foreach (int i in dictionary.Keys)
            {
                string propertyName = dictionary[i];
                PropertyInfo propertyInfo = this.type.GetProperty(propertyName);

                // �����ǩ
                Label endIfLabel = generator.DefineLabel();

                // ���� Callvirt : IDataRecord.IsDBNull() ����
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldc_I4, i);
                generator.Emit(OpCodes.Callvirt, IsDBNullMethod);
                // ����� DataRecord.IsDBNull() == true �� contine
                generator.Emit(OpCodes.Brtrue, endIfLabel);

                // POP : T t
                generator.Emit(OpCodes.Ldloc, localBuilder);

                // ���� Callvirt : IDataRecord.get_Item(i) ����
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldc_I4, i);
                generator.Emit(OpCodes.Callvirt, getItemMethod);

                //Ad By RQ 2011-01-18 12:46 ORACLE DATE �� dataRecord.GetFieldType(i) �ó�������ת������
                //generator.Emit(OpCodes.Unbox_Any, dataRecord.GetFieldType(i));

                generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);

                // ���� Callvirt : propertyInfo.GetSetMethod ����
                generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());

                // �ڵ�ǰ MSIL ��λ�ý��б��
                generator.MarkLabel(endIfLabel);
            }

            // returns the value of the local variable
            generator.Emit(OpCodes.Ldloc, localBuilder);

            // ��������
            generator.Emit(OpCodes.Ret);

            // ��ɶ�̬�����Ĵ���������ִ�иö�̬������ί��
            this.dataRecord2Entity = (DataRecord2Entity)dynamicMethod.CreateDelegate(typeof(DataRecord2Entity));
        }


        /// ִ�� DataRecord2Entity �ķ���
        public virtual object CreateEntity(IDataRecord dataRecord)
        {
            return this.dataRecord2Entity(dataRecord);
        }

        public static List<object> DataReader2Entity(Type type, IDataReader dataReader)
        {
            List<object> list = new List<object>();

            try
            {
                DynamicBuilder dynamicBuilder = new DynamicBuilder(type, dataReader);

                while (dataReader.Read())
                {
                    object entity = dynamicBuilder.CreateEntity(dataReader);

                    list.Add(entity);
                }
            }
            finally
            {
                dataReader.Close();
            }

            return list;
        }
    }


    /// <summary>
    /// DynamicBuilder ��ժҪ˵����
    /// </summary>
    public class DynamicBuilder<T> : DynamicBuilder
    {
        /// ���캯��
        public DynamicBuilder(IDataRecord dataRecord)
            : base(typeof(T), dataRecord)
        {
        }

        /// ִ�� DataRecord2Entity �ķ���
        public new T CreateEntity(IDataRecord dataRecord)
        {
            return (T)base.CreateEntity(dataRecord);
        }


        /// <summary>
        /// �� IDataReader ����ת��Ϊ T ����
        /// </summary>
        /// <param name="dataReader">IDataReader ����</param>
        /// <returns>T ����</returns>
        public static List<T> DataReader2Entity(IDataReader dataReader)
        {
            List<T> list = new List<T>();

            try
            {
                DynamicBuilder<T> dynamicBuilder = new DynamicBuilder<T>(dataReader);

                while (dataReader.Read())
                {
                    T entity = dynamicBuilder.CreateEntity(dataReader);

                    list.Add(entity);
                }
            }
            finally
            {
                dataReader.Close();
            }

            return list;
        }
    }
}