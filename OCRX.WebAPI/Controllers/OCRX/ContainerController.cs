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
    public class ContainerController : ApiController
    {
        [HttpGet]
        public JsonResult<DataTable> SelectCnt(DateTime from, DateTime to, string isArchived, string qcno, decimal dock_status, decimal cstatus, string containerno, string companyCode, string isdmg)
        {
            var result = webDAL.SelectCnt(from, to, isArchived, qcno, dock_status, cstatus, containerno, companyCode, isdmg);
            return Json<DataTable>(result);
        }
        [HttpGet]
        public JsonResult<DataTable> SelectCntMonitor(string companyCode)
        {
           var result=webDAL.SelectCntMonitor(companyCode);

            return Json<DataTable>(result);
        }
        /// <summary>
        /// 查箱是否确认过
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<DataTable> SelectCntConfirmed(string velaliase, string contNo, decimal dockStatus)
        {
            var result = webDAL.SelectCntConfirmed(velaliase, contNo, dockStatus);
            return Json<DataTable>(result);
        }
        /// <summary>
        /// 查是否是箱号溢卸
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<DataTable> SelectOverDisCnt(decimal containerid)
        {
            var result = webDAL.SelectOverDisCnt(containerid);
            return Json<DataTable>(result);
        }

        /// <summary>
        /// 查询同一吊次记录
        /// </summary>
        /// <param name="row"></param>
        [HttpGet]
        public JsonResult<DataSet1.T_OCRX_CNTDataTable> SelectCnt(decimal dockid)
        {
            var result = webDAL.SelectCnt(dockid);
            return Json<DataSet1.T_OCRX_CNTDataTable>(result);
        }

        /// <summary>
        /// 单箱查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<DataTable> GetContainerInfo(string containerno)
        {
            var result = webDAL.GetContainerInfo(containerno);
            return Json<DataTable>(result);
        }

        [HttpPost]
        public void UpdateCntStatus(DataSet1.T_OCRX_CNTDataTable row)
        {
            webDAL.UpdateCntStatus(row[0]);
        }

        [HttpPut]
        public void RollbackCntStatus(DataSet1.T_OCRX_CNTDataTable row)
        {
            webDAL.UpdateCntStatus(row[0]);
        }
    }
}
