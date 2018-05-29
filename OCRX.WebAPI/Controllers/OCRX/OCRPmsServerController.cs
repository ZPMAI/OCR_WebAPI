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
    public class OCRPmsServerController : ApiController
    {
        [HttpGet]
        public JsonResult<DataTable> GetPMSServer()
        {
            var result = webDAL.SelectocrdbPmsServer();
            return Json<DataTable>(result);
        }
    }
}
