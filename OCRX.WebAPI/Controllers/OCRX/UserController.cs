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
    public class UserController : ApiController
    {
        [HttpGet]
        public JsonResult<DataSet1.T_OCRX_USERSDataTable> SelectUsers()
        {
            var result = webDAL.SelectUsers();
            return Json<DataSet1.T_OCRX_USERSDataTable>(result);
        }

        [HttpGet]
        public JsonResult<DataSet1.T_OCRX_USERSDataTable> SelectUser([FromUri]string userId)
        {
            var result = webDAL.SelectUser(userId);
            return Json<DataSet1.T_OCRX_USERSDataTable>(result);
        }

        [HttpPost]
        public void InsertUsers([FromBody]DataSet1.T_OCRX_USERSDataTable row)
        {
            webDAL.InsertUsers(row[0]);
            //return Json<DataSet1.T_OCRX_CODataTable>(result);
        }

        [HttpPost]
        public void DeleteUsers([FromBody]DataSet1.T_OCRX_USERSDataTable row)
        {
            webDAL.DeleteUsers(row[0]);
            //return Json<DataSet1.T_OCRX_CODataTable>(result);
        }
    }
}
