using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace DBFramework.MSIL
{
    /// <example>
    /// public class MyClass
    /// {
    ///     private string _Field;
    /// 
    ///     public string Field
    ///     {
    ///         get { return _Field; }
    ///         set { _Field = value; }
    ///     }
    /// 
    ///     public MyClass()
    ///     {
    ///     }
    /// }
    /// </example>
    public sealed class DynamicClass
    {
        #region Property ��ժҪ˵��

        /// <summary>
        /// Property ��ժҪ˵����
        /// </summary>
        class Property
        {
            private string name;
            private Type fieldType;
            
            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            public Type FieldType
            {
                get { return fieldType; }
                set { fieldType = value; }
            }


            /// ���캯��
            public Property(string name, Type fieldType)
            {
                if (fieldType.Name.Equals("DateTime"))
                {
                    this.fieldType = typeof(Nullable<DateTime>);
                }
                else
                {
                    this.fieldType = fieldType;
                }

                this.name = name;
            }
        }

        #endregion

        private string assemblyName;
        private string typeName;
        private Type classType;
        private List<Property> properties = new List<Property>();

        public Type ClassType
        {
            get { return this.classType; }
        }

        /// ���캯��
        public DynamicClass(string assemblyName, string typeName)
        {
            this.assemblyName = assemblyName;
            this.typeName = typeName;
        }

        public void AddProperty(string propertyName, Type fieldType)
        {
            Property property = new Property(propertyName, fieldType);
            this.properties.Add(property);
        }

        /// <remarks>
        /// �����Ѵ����� Assembly
        /// Assembly assembly = Assembly.LoadFrom(fileName);
        /// Type type = assembly.GetType(typeName);
        /// </remarks>
        public void CreateAssembly()
        {
            string fileName = string.Format("{0}.dll", this.assemblyName);            

            //��̬��������
            AssemblyName name = new AssemblyName(this.assemblyName);
            AssemblyBuilder assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(name, AssemblyBuilderAccess.RunAndSave);

            //��̬����ģ��
            ModuleBuilder moduleBuilder = assemblyBuilder.DefineDynamicModule(this.assemblyName, fileName);

            //��̬������
            TypeBuilder typeBuilder = moduleBuilder.DefineType(this.typeName, TypeAttributes.Public);

            //��̬�������캯��
            ConstructorBuilder constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, Type.EmptyTypes);
            ConstructorInfo constructorInfo = typeof(object).GetConstructor(Type.EmptyTypes);
            ILGenerator generator = constructorBuilder.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Call, constructorInfo);
            generator.Emit(OpCodes.Ret);

            foreach (Property property in this.properties)
            {
                CreateProperty(typeBuilder, property);
            }

            //ʹ�ö�̬�ഴ������
            this.classType = typeBuilder.CreateType();

            //���涯̬�����ĳ���(���򼯽�����������Ŀ¼��)
            assemblyBuilder.Save(fileName);
        }

        
        private void CreateProperty(TypeBuilder typeBuilder, Property property)
        {
            string fieldName = string.Format("_{0}", property.Name);
            FieldBuilder fieldBuilder = typeBuilder.DefineField(fieldName, property.FieldType, FieldAttributes.Private);
            PropertyBuilder propertyBuilder = typeBuilder.DefineProperty(property.Name, PropertyAttributes.HasDefault, property.FieldType, null);

            CreateGetter(typeBuilder, propertyBuilder, property.Name, property.FieldType, fieldBuilder);
            CreateSetter(typeBuilder, propertyBuilder, property.Name, property.FieldType, fieldBuilder);
        }

        /// ���� Getter
        private void CreateGetter(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, string propertyName, Type fieldType, FieldBuilder fieldBuilder)
        {
            string getMethodName = string.Format("get_{0}", propertyName);
            MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName;
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(getMethodName, methodAttributes, fieldType, Type.EmptyTypes);

            ILGenerator generator = methodBuilder.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldfld, fieldBuilder);
            generator.Emit(OpCodes.Ret);

            propertyBuilder.SetGetMethod(methodBuilder);
        }

        /// ���� Setter
        private void CreateSetter(TypeBuilder typeBuilder, PropertyBuilder propertyBuilder, string propertyName, Type fieldType, FieldBuilder fieldBuilder)
        {
            string setMethodName = string.Format("set_{0}", propertyName);
            MethodAttributes methodAttributes = MethodAttributes.Public | MethodAttributes.FamANDAssem | MethodAttributes.Family | MethodAttributes.HideBySig | MethodAttributes.SpecialName;
            Type[] parameterTypes = new Type[] { fieldType };
            MethodBuilder methodBuilder = typeBuilder.DefineMethod(setMethodName, methodAttributes, null, parameterTypes);

            ILGenerator generator = methodBuilder.GetILGenerator();
            generator.Emit(OpCodes.Ldarg_0);
            generator.Emit(OpCodes.Ldarg_1);
            generator.Emit(OpCodes.Stfld, fieldBuilder);
            generator.Emit(OpCodes.Ret);

            propertyBuilder.SetSetMethod(methodBuilder);
        }
    }
}