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
    public class DynamicBuilder
    {
        private delegate object DataRecord2Entity(IDataRecord dataRecord);

        private static readonly MethodInfo getItemMethod = typeof(IDataRecord).GetMethod("get_Item", new Type[] { typeof(int) });
        private static readonly MethodInfo IsDBNullMethod = typeof(IDataRecord).GetMethod("IsDBNull", new Type[] { typeof(int) });

        private DataRecord2Entity dataRecord2Entity;
        private Type type;

        /// 构造函数
        public DynamicBuilder(Type type, IDataRecord dataRecord)
        {
            this.type = type;

            PrepareDelegate(dataRecord);
        }


        /// 为了节省时间, 可以调用一次 GetOrdinal, 然后将结果分配给整数变量以便在循环中使用。
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


        /// 定义一个动态方法，返回类型 typeof(T)，输入类型 typeof(IDataRecord)
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

            // 创建一个MSIL生成器，为动态方法生成代码
            ILGenerator generator = dynamicMethod.GetILGenerator();

            // 声明指定类型的局部变量，可以理解为 T t;
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

                // 定义标签
                Label endIfLabel = generator.DefineLabel();

                // 调用 Callvirt : IDataRecord.IsDBNull() 方法
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldc_I4, i);
                generator.Emit(OpCodes.Callvirt, IsDBNullMethod);
                // 如果是 DataRecord.IsDBNull() == true 则 contine
                generator.Emit(OpCodes.Brtrue, endIfLabel);

                // POP : T t
                generator.Emit(OpCodes.Ldloc, localBuilder);

                // 调用 Callvirt : IDataRecord.get_Item(i) 方法
                generator.Emit(OpCodes.Ldarg_0);
                generator.Emit(OpCodes.Ldc_I4, i);
                generator.Emit(OpCodes.Callvirt, getItemMethod);

                //Ad By RQ 2011-01-18 12:46 ORACLE DATE 用 dataRecord.GetFieldType(i) 得出的类型转换出错
                //generator.Emit(OpCodes.Unbox_Any, dataRecord.GetFieldType(i));

                generator.Emit(OpCodes.Unbox_Any, propertyInfo.PropertyType);

                // 调用 Callvirt : propertyInfo.GetSetMethod 方法
                generator.Emit(OpCodes.Callvirt, propertyInfo.GetSetMethod());

                // 在当前 MSIL 流位置进行标记
                generator.MarkLabel(endIfLabel);
            }

            // returns the value of the local variable
            generator.Emit(OpCodes.Ldloc, localBuilder);

            // 方法结束
            generator.Emit(OpCodes.Ret);

            // 完成动态方法的创建并创建执行该动态方法的委托
            this.dataRecord2Entity = (DataRecord2Entity)dynamicMethod.CreateDelegate(typeof(DataRecord2Entity));
        }


        /// 执行 DataRecord2Entity 的方法
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
    /// DynamicBuilder 的摘要说明。
    /// </summary>
    public class DynamicBuilder<T> : DynamicBuilder
    {
        /// 构造函数
        public DynamicBuilder(IDataRecord dataRecord)
            : base(typeof(T), dataRecord)
        {
        }

        /// 执行 DataRecord2Entity 的方法
        public new T CreateEntity(IDataRecord dataRecord)
        {
            return (T)base.CreateEntity(dataRecord);
        }


        /// <summary>
        /// 把 IDataReader 对象转换为 T 集合
        /// </summary>
        /// <param name="dataReader">IDataReader 对象</param>
        /// <returns>T 集合</returns>
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