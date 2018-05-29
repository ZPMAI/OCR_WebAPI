using System;
using System.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

using DBFramework.Mapping;

namespace DBFramework
{
    /// <remarks>
    /// 反射的执行效率太低
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

        // 最终执行动态方法的一个委托 参数是IDataRecord接口
        private DataRecord2Entity dataRecord2Entity;

        /// 构造函数
        public DynamicBuilder(IDataRecord dataRecord) 
        {
            Prepare(dataRecord);
        }

        /// 为了节省时间, 可以调用一次 GetOrdinal, 然后将结果分配给整数变量以便在循环中使用。
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
                    // 若指定的名称不是有效的列名称，即类属性名称与数据库字段名称不匹配
                }
            }

            return dictionary;
        }


        private void Prepare(IDataRecord dataRecord)
        {
            // 定义一个动态方法，返回类型 typeof(T)，输入类型 typeof(IDataRecord)
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

                // 创建一个MSIL生成器，为动态方法生成代码
                ILGenerator generator = dynamicMethod.GetILGenerator();

                // 声明指定类型的局部变量，可以理解为 T t;
                LocalBuilder localBuilder = generator.DeclareLocal(type);

                // 可以理解为 T t = new T(); The next piece of code instantiates the requested type of object and stores it in the local variable. 
                ConstructorInfo constructorInfo = type.GetConstructor(Type.EmptyTypes);
                generator.Emit(OpCodes.Newobj, constructorInfo);
                generator.Emit(OpCodes.Stloc, localBuilder);

                foreach (string propertyName in dictionary.Keys)
                {
                    PropertyInfo propertyInfo = type.GetProperty(propertyName);
                    int index = dictionary[propertyName];

                    // 定义标签
                    Label endIfLabel = generator.DefineLabel();

                    generator.Emit(OpCodes.Ldarg_0);
                    generator.Emit(OpCodes.Ldc_I4, index);

                    // 调用 Callvirt : dataRecord.IsDBNull() 方法                    
                    generator.Emit(OpCodes.Callvirt, isDBNullMethod);
                    // 如果是 DataRecord.IsDBNull() == true 则 contine
                    generator.Emit(OpCodes.Brtrue, endIfLabel);

                    generator.Emit(OpCodes.Ldloc, localBuilder);
                    generator.Emit(OpCodes.Ldarg_0);
                    generator.Emit(OpCodes.Ldc_I4, index);

                    // 调用 Callvirt : dataRecord.get_Item(i) 方法
                    generator.Emit(OpCodes.Callvirt, getItemMethod);
                    generator.Emit(OpCodes.Unbox_Any, dataRecord.GetFieldType(index));

                    // 调用 Callvirt : propertyInfo.GetSetMethod 方法
                    generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());

                    // 在当前 MSIL 流位置进行标记
                    generator.MarkLabel(endIfLabel);
                }

                // The last part of the code returns the value of the local variable
                generator.Emit(OpCodes.Ldloc, localBuilder);

                // 方法结束
                generator.Emit(OpCodes.Ret);

                DynamicMethodContainer.Add(name, dynamicMethod);
            }

            // 完成动态方法的创建并创建执行该动态方法的委托
            // DataRecord2Entity 在 Build 方法里实现
            this.dataRecord2Entity = (DataRecord2Entity)dynamicMethod.CreateDelegate(typeof(DataRecord2Entity));
        }

        /// 执行 DataRecord2Entity 的方法
        public T Build(IDataRecord dataRecord)
        {
            return this.dataRecord2Entity(dataRecord);
        }        
    }
}
