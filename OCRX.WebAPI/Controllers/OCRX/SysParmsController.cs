using OCR.Model;
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
    public class SysParmsController : ApiController
    {
        public JsonResult<SysParms> SelectParams()
        {
            var result = webDAL.SelectParams();
            return Json<SysParms>(result);
        }
    }
}
