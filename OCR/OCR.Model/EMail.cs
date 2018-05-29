using System;
using System.Collections.Generic;
using System.Text;

namespace OCR.Model
{
    [Serializable]
    public class EMail
    {
        public string From;
        public string To;
        public string Content;
        public string Subject;
    }
}
