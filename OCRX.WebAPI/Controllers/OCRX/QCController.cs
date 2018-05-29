using OCR.Model;
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
    public class QCController : ApiController
    {
        [HttpGet]
        public JsonResult<DataTable> SelectQcMonitor(string companyCode)
        {
            var result = webDAL.SelectQcMonitor(companyCode);
            return Json<DataTable>(result);
        }

        [HttpGet]
        public JsonResult<QcSet.T_OCR_QCSETDataTable> SelectQCSet()
        {
            var result = webDAL.SelectQCSet();
            return Json<QcSet.T_OCR_QCSETDataTable>(result);
        }

        [HttpGet]
        public JsonResult<QcSet.T_OCR_QCSETDataTable> SelectQCSetByQCNo(string qcno)
        {
            var result = webDAL.SelectQCSet(qcno);
            return Json<QcSet.T_OCR_QCSETDataTable>(result);
        }

        /// <summary>
        /// QC监控
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<DataTable> SelectQcMonitor2()
        {
            var result = webDAL.SelectQcMonitor2();

            return Json<DataTable>(result);
        }

        /// <summary>
        /// 查QC作业记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<DataTable> SelectQCWorkRecord(string qcNo, string containerId)
        {
            var result = webDAL.SelectQCWorkRecord(qcNo,containerId);

            return Json<DataTable>(result);
        }
    }
}
