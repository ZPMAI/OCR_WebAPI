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
    public class RecordController : ApiController
    {
        [HttpGet]
        public JsonResult<DataSet1.T_OCRX_CNTDataTable> SelectNextRecord(string userName, string companyCode)
        {
            var result = webDAL.SelectNextRecord(userName, companyCode);
            return Json<DataSet1.T_OCRX_CNTDataTable>(result);
        }

        [HttpGet]
        public JsonResult<int> SelectLeft(string companyCode)
        {
            var result = webDAL.SelectLeft(companyCode);
            return Json<int>(result);
        }

        [HttpGet]
        public JsonResult<DataSet> SelectReport(string eName, string companyCode)
        {
            var result = webDAL.SelectReport(eName, companyCode);
            return Json<DataSet>(result);
        }

        [HttpGet]
        public JsonResult<Int64> SelectSeqOcrx()
        {
            var result = webDAL.SelectSeqOcrx();
            return Json<Int64>(result);
        }
    }
}
