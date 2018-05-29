using System;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using System.CodeDom;
using System.CodeDom.Compiler;

using Microsoft.CSharp;

namespace DBFramework
{    
    public class DynamicClass
    {
        #region Property 的摘要说明

        /// <summary>
        /// Property 的摘要说明。
        /// </summary>
        class Property
        {
            private string fieldType;
            private string name;

            public string FieldType
            {
                get { return fieldType; }
                set { fieldType = value; }
            }

            public string Name
            {
                get { return name; }
                set { name = value; }
            }

            /// 构造函数
            public Property(string fieldType, string name)
            {
                if (string.Compare(fieldType, "DateTime", true) == 0)
                {
                    this.fieldType = string.Format("{0}?", fieldType);
                }
                else
                {
                    this.fieldType = fieldType;
                }

                this.name = name;
            }
        }

        #endregion

        private string typeName;
        private List<Property> properties = new List<Property>();
        private Assembly assembly;

        /// 构造函数
        public DynamicClass(string typeName)
        {
            this.typeName = typeName;
        }

        private string PropertyString(Property property)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder
                .AppendFormat("    private {0} _{1};", property.FieldType, property.Name)
                .AppendLine()
                .AppendFormat("    public {0} {1}", property.FieldType, property.Name)
                .AppendLine()
                .AppendLine("    {")
                .AppendLine("        get { return _" + property.Name + "; }")
                .AppendLine("        set { _" + property.Name + " = value; } ")
                .AppendLine("    }");

            return stringBuilder.ToString();
        }

        public void AddProperty(string fieldType, string name)
        {
            Property property = new Property(fieldType, name);
            this.properties.Add(property);
        }

        public void CreateAssembly()
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            CompilerParameters compilerParameters = new CompilerParameters();
            compilerParameters.GenerateExecutable = false;
            compilerParameters.GenerateInMemory = true;            

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder
                .AppendLine("using System;")
                .AppendFormat("public class {0} ", typeName)
                .AppendLine("{");

            foreach (Property property in this.properties)
            {
                stringBuilder.Append(PropertyString(property));
            }

            stringBuilder.AppendLine("}");

            CompilerResults compilerResults = codeProvider.CompileAssemblyFromSource(compilerParameters, stringBuilder.ToString());
            this.assembly = compilerResults.CompiledAssembly;
        }

        public object CreateInstance()
        {
            return assembly.CreateInstance(typeName);
        }

        public static void SetValue(object entity, string propertyName, object value)
        {
            if (value == DBNull.Value) { return; }

            PropertyInfo propertyInfo = entity.GetType().GetProperty(propertyName);

            if (propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(entity, value, null);
            }
        }


        public static object GetValue(object entity, string propertyName)
        {
            PropertyInfo propertyInfo = entity.GetType().GetProperty(propertyName);

            if (propertyInfo.CanRead)
            {
                return propertyInfo.GetValue(entity, null);
            }

            return null;
        }
    }
}
