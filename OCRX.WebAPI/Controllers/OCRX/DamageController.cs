using OCRX.Model;
using OCRX.Web.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace OCRX.WebAPI.Controllers.OCRX
{
    public class DamageController : ApiController
    {
        /// <summary>
        /// 查残损方位代码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("SelectDamagePositionCode")]
        public JsonResult< DataTable> SelectDamagePositionCode()
        {
            var result = webDAL.SelectDamagePositionCode();

            return Json<DataTable>(result);
        }

        /// <summary>
        /// 查残损类型代码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ActionName("SelectDamageTypeCode")]
        public JsonResult<DataTable> SelectDamageTypeCode()
        {
            var result = webDAL.SelectDamageTypeCode();

            return Json<DataTable>(result);
        }

        /// <summary>
        /// 查询单个残损箱作业信息
        /// </summary>
        /// <param name="row"></param>
        [HttpGet]
        public JsonResult<DataTable> SelectCTOSDamageRecord99XX(decimal containerid)
        {
            var result = webDAL.SelectCTOSDamageRecord99XX(containerid);

            return Json<DataTable>(result);
        }

        /// <summary>
        /// 查询单个残损箱作业信息
        /// </summary>
        /// <param name="row"></param>
        [HttpGet]
        public JsonResult<DataSet1.T_OCRX_CNTDataTable> SelectCntDmgRecord(decimal dockid)
        {
            var result = webDAL.SelectCntDmgRecord(dockid);

            return Json<DataSet1.T_OCRX_CNTDataTable>(result);
        }

        /// <summary>
        /// 查询单个箱残损信息
        /// </summary>
        /// <param name="row"></param>
        [HttpGet]
        public JsonResult<DataSet1.T_OCRX_DAMAGEDataTable>  SelectDamageRecord(decimal dockid)
        {
            var result = webDAL.SelectDamageRecord(dockid);

            return Json<DataSet1.T_OCRX_DAMAGEDataTable>(result);
        }

        /// <summary>
        /// 查询单个箱残损信息
        /// </summary>
        /// <param name="row"></param>
        [HttpGet]
        public JsonResult<DataSet> SelectDamageReport(string eName, string companyCode)
        {
            var result = webDAL.SelectDamageReport(eName,companyCode);

            return Json<DataSet>(result);
        }

        /// <summary>
        /// 插入残损记录
        /// </summary>
        /// <param name="row"></param>
        [HttpPost]
        public void InsertDamage(DataSet1.T_OCRX_DAMAGEDataTable row)
        {
            webDAL.InsertDamage(row[0]);
        }


        /// <summary>
        /// 查询残损记录(内理)
        /// </summary>
        /// <param name="row"></param>
        [HttpGet]
        public JsonResult<DataSet1.T_OCRX_DAMAGEDataTable> SelectDmgRecord(string velaliase)
        {
            var result = webDAL.SelectDmgRecord(velaliase);

            return Json<DataSet1.T_OCRX_DAMAGEDataTable>(result);
        }

        /// <summary>
        /// 查询残损记录(外理)
        /// </summary>
        /// <param name="row"></param>
        [HttpGet]
        public JsonResult<DataSet1.T_OCRX_DAMAGEDataTable> SelectDmgRecordEx(string velaliase)
        {
            var result = webDAL.SelectDmgRecordEx(velaliase);

            return Json<DataSet1.T_OCRX_DAMAGEDataTable>(result);
        }

        /// <summary>
        /// 更新残损记录状态
        /// </summary>
        /// <param name="row"></param>
        [HttpPost]
        public  void UpdateDamageStatus(DataSet1.T_OCRX_DAMAGEDataTable row)
        {
            webDAL.UpdateDamageStatus(row[0]);
        }

        /// <summary>
        /// 更新残损记录信息
        /// </summary>
        /// <param name="row"></param>
        [HttpPost]
        public void UpdateDamageInfo(DataSet1.T_OCRX_DAMAGEDataTable row)
        {
            webDAL.UpdateDamageInfo(row[0]);
        }

        /// <summary>
        /// 删除残损记录
        /// </summary>
        /// <param name="row"></param>
        [HttpPost]
        public void DeleteDamage(DataSet1.T_OCRX_DAMAGEDataTable row)
        {
            webDAL.DeleteDamage(row[0]);
        }
    }
}
