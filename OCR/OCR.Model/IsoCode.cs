using System;
using System.Collections.Generic;
using System.Text;

namespace OCR.Model
{
    
    public class IsoCode
    {
        public IsoCode(decimal CONTAINERSIZE, string CONTAINERTYPE, decimal CONTAINERHEIGHT)
        {
            this.CONTAINERHEIGHT = CONTAINERHEIGHT;
            this.CONTAINERSIZE = CONTAINERSIZE;
            this.CONTAINERTYPE = CONTAINERTYPE;
        }
        public decimal CONTAINERSIZE;
        public string CONTAINERTYPE;
        public decimal CONTAINERHEIGHT;
    }
}
