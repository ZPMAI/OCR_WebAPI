using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using OCRX.Web.DAL;
using OCRX.WebAPI;

namespace OCRX.WebAPI.Controllers
{
    public class LoginController : ApiController
    {
        [HttpGet]
        public JsonResult<string> Login(string user,string pwd)
        {
            string errorCode = OCRDbContext.Login(user,pwd).ToString();
            return Json<string>(errorCode);
        }
    }
}
