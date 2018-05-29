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
    public class ServiceController : ApiController
    {
        /// <summary>
        /// 查航线
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<DataTable> SelectService()
        {
            var result=webDAL.SelectService();

            return Json<DataTable>(result);
        }

        /// <summary>
        /// 按船别名和公司查匹配箱主
        /// </summary>
        /// <returns></returns>
        public JsonResult<DataTable> SelectServiceByVslaliase([FromUri]string velaliase)
        {
            var result = webDAL.SelectService(velaliase);
            return Json<DataTable>(result);
        }
    }
}
