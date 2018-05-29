using OCRX.Model;
using OCRX.Web.DAL;
using System.Data;
using System.Web.Http;
using System.Web.Http.Results;

namespace OCRX.WebAPI.Controllers.OCRX
{
    public class CompanyController : ApiController
    {
        [HttpGet]
        public JsonResult<DataSet1.T_OCRX_CODataTable> SelectCompany()
        {
            var result = webDAL.SelectCompany();
            return Json<DataSet1.T_OCRX_CODataTable>(result);
        }

        [HttpGet]
        public JsonResult<DataSet1.T_OCRX_CODataTable> SelectCompany([FromUri]string companyCode)
        {
            var result = webDAL.SelectCompany(companyCode);
            return Json<DataSet1.T_OCRX_CODataTable>(result);
        }

        [HttpPost]
        public void InsertCompany(DataSet1.T_OCRX_CODataTable row)
        {
            webDAL.InsertCompany(row[0]);
            //return Json<DataSet1.T_OCRX_CODataTable>(result);
        }

        [HttpPost]
        public void DeleteCompany(DataSet1.T_OCRX_CODataTable row)
        {
            webDAL.DeleteCompany(row[0]);
            //return Json<DataSet1.T_OCRX_CODataTable>(result);
        }
    }
}
