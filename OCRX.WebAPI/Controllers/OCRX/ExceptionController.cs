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
    public class ExceptionController : ApiController
    {
        [HttpGet]
        public  JsonResult<DataSet1.T_OCRX_CNTDataTable> SelectExcep([FromUri]string companyCode)
        {
            var result = webDAL.SelectExcep(companyCode);
            return Json<DataSet1.T_OCRX_CNTDataTable>(result);
        }

        [HttpPost]
        public void UpdateExcepStatus([FromBody]DataSet1.T_OCRX_CNTDataTable row)
        {
            webDAL.UpdateExcepStatus(row[0]);
        }
    }
}
