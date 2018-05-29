using System;
using System.Collections.Generic;
using System.Text;

namespace CCT.SystemFramework.Forms
{
    /// <summary>
    /// ʹ����COMBOBOX,LISTBOX�ȿؼ����������ຯ��
    /// </summary>
    public class KeyValuePair
    {
        private string myShortName;
        private string myLongName;

        public KeyValuePair(string strLongName, string strShortName)
        {
            this.myShortName = strShortName;
            this.myLongName = strLongName;
        }

        public string ShortName
        {
            get
            {
                return myShortName;
            }
        }

        public string LongName
        {
            get
            {
                return myLongName;
            }
        }

        public override string ToString()
        {
            return String.Format("{0} - {1}", this.ShortName, this.LongName);
        }
    }
}
