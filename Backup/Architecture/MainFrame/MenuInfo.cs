using System;
using System.Collections.Generic;
using System.Text;

namespace CCT.MainFrame
{
    class MenuInfo
    {
        private string menuFlag;
        private string typeName;
        private string assemblyName;

        public string MenuFlag
        {
            get { return this.menuFlag; }
        }

        public string TypeName
        {
            get { return this.typeName; }
        }

        public string AssemblyName
        {
            get { return this.assemblyName; }
        }

        /// ¹¹Ôìº¯Êý
        public MenuInfo(string menuFlag, string typeName, string assemblyName)
        {
            this.menuFlag = menuFlag;
            this.typeName = typeName;
            this.assemblyName = assemblyName;
        }
    }
}
