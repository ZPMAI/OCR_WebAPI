using System;
using System.Collections.Generic;
using System.Text;

namespace OCRX.Model
{
    public class DamageInfo
    {
        private decimal dockId = 0;

        #region 箱id
        /// <summary>
        /// 残损方位代码
        /// </summary>
        public decimal DockId
        {
            get
            {
                return dockId;
            }
            set
            {
                dockId = value;
            }
        }
        #endregion

        private string damagePosition = string.Empty;

        #region 残损部位代码
        /// <summary>
        /// 残损部位代码
        /// </summary>
        public string DamagePosition
        {
            get
            {
                return damagePosition;
            }
            set
            {
                damagePosition = value;
            }
        }
        #endregion

        private string damagePositionDesc = string.Empty;

        #region 残损部位描述
        /// <summary>
        /// 残损部位描述
        /// </summary>
        public string DamagePositionDesc
        {
            get
            {
                return damagePositionDesc;
            }
            set
            {
                damagePositionDesc = value;
            }
        }
        #endregion

        private string damageType = string.Empty;

        #region 残损类型代码
        /// <summary>
        /// 残损类型代码
        /// </summary>
        public string DamageType
        {
            get
            {
                return damageType;
            }
            set
            {
                damageType = value;
            }
        }
        #endregion

        private string damageTypeDesc = string.Empty;

        #region 残损类型描述
        /// <summary>
        /// 残损类型代码
        /// </summary>
        public string DamageTypeDesc
        {
            get
            {
                return damageTypeDesc;
            }
            set
            {
                damageTypeDesc = value;
            }
        }
        #endregion

        private string damageSize = string.Empty;

        private string damagePart = string.Empty;

        #region 残损方位代码
        /// <summary>
        /// 残损方位代码
        /// </summary>
        public string DamagePart
        {
            get
            {
                return damagePart;
            }
            set
            {
                damagePart = value;
            }
        }
        #endregion

        #region 残损尺寸
        /// <summary>
        /// 残损类型代码
        /// </summary>
        public string DamageSize
        {
            get
            {
                return damageSize;
            }
            set
            {
                damageSize = value;
            }
        }
        #endregion

        private string ctosInterface = string.Empty;

        #region CTOS接口标识
        /// <summary>
        /// 残损类型代码
        /// </summary>
        public string CTOSInterface
        {
            get
            {
                return ctosInterface;
            }
            set
            {
                ctosInterface = value;
            }
        }
        #endregion

        private string rollBack = string.Empty;

        #region 回退标识
        /// <summary>
        /// 残损类型代码
        /// </summary>
        public string RollBack
        {
            get
            {
                return rollBack;
            }
            set
            {
                rollBack = value;
            }
        }
        #endregion
    }
}
