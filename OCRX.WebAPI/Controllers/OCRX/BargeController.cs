using OCRX.Model;
using OCRX.Web.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace OCRX.WebAPI.Controllers.OCRX
{
    //班轮分发规则
    public class BargeController : ApiController
    {
        [HttpGet]
        public JsonResult<DataSet1.T_OCRX_BARGEDataTable> SelectBarge([FromUri]string companyCode)
        {
            var result = webDAL.SelectBarge(companyCode);
            return Json<DataSet1.T_OCRX_BARGEDataTable>(result);
        }

        [HttpGet]
        public JsonResult<DataSet1.T_OCRX_BARGEDataTable> SelectBargeByAgent([FromUri]string agent)
        {
            var result = webDAL.SelectBargeByAgent(agent);
            return Json<DataSet1.T_OCRX_BARGEDataTable>(result);
        }

        [HttpGet]
        public JsonResult<DataSet1.T_OCRX_BARGEDataTable> SelectBargeByVelaliase([FromUri]string velaliase)
        {
            var result = webDAL.SelectBargeByVelaliase(velaliase);
            return Json<DataSet1.T_OCRX_BARGEDataTable>(result);
        }

        [HttpPost]
        public void InsertBarge([FromBody]DataSet1.T_OCRX_BARGEDataTable row)
        {
            webDAL.InsertBarge(row[0]);
            //return Json<DataSet1.T_OCRX_CODataTable>(result);
        }

        [HttpPost]
        public void UpdateBarge([FromBody]DataSet1.T_OCRX_BARGEDataTable row)
        {
            webDAL.UpdateBarge(row[0]);
            //return Json<DataSet1.T_OCRX_CODataTable>(result);
        }

        [HttpPost]
        public void DeleteBarge([FromBody]DataSet1.T_OCRX_BARGEDataTable row)
        {
            webDAL.DeleteBarge(row[0]);
            //return Json<DataSet1.T_OCRX_CODataTable>(result);
        }
    }
}
