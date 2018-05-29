using System;
using System.Collections.Generic;
using System.Text;

namespace OCRX.Model
{
    public class QCWorkRecord
    {
        private string qc = string.Empty;

        #region QC 号码
        /// <summary>
        /// QC 号码
        /// </summary>
        public string QC
        {
            get
            {
                return qc;
            }
            set
            {
                qc = value;
            }
        }
        #endregion

        private string containerno = string.Empty;

        #region 箱号
        /// <summary>
        /// 箱号
        /// </summary>
        public string ContainerNo
        {
            get
            {
                return containerno;
            }
            set
            {
                containerno = value;
            }
        }
        #endregion

        private string containertype = string.Empty;

        #region 箱型尺寸
        /// <summary>
        /// 箱型尺寸
        /// </summary>
        public string ContainerType
        {
            get
            {
                return containertype;
            }
            set
            {
                containertype = value;
            }
        }
        #endregion

        private string worktime = string.Empty;

        #region 箱型尺寸
        /// <summary>
        /// 作业时间
        /// </summary>
        public string WorkTime
        {
            get
            {
                return worktime;
            }
            set
            {
                worktime = value;
            }
        }
        #endregion
    }
}
