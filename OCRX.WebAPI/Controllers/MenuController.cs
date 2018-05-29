using OCRX.Web.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace OCRX.WebAPI.Controllers
{
    public class MenuController : ApiController
    {
        [HttpGet]
        public JsonResult<DataSet> GetMenu(string username)
        {
            var menu = OCRDbContext.GetMenu(username);
            return Json<DataSet>(menu);
        }
    }
}
