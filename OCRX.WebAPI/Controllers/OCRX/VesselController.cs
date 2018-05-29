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
    //班轮分发规则
    public class VesselController : ApiController
    {
        [HttpGet]
        public JsonResult<DataSet1.T_OCRX_VESSELDataTable> SelectVessel()
        {
            var result = webDAL.SelectVessel();
            return Json<DataSet1.T_OCRX_VESSELDataTable>(result);
        }

        [HttpGet]
        public JsonResult<DataSet1.T_OCRX_VESSELDataTable> SelectVesselByService([FromUri]string serviceCode)
        {
            var result = webDAL.SelectVesselByService(serviceCode);
            return Json<DataSet1.T_OCRX_VESSELDataTable>(result);
        }

        [HttpGet]
        public JsonResult<DataSet1.T_OCRX_VESSELDataTable> SelectVesselByCompanyCode([FromUri]string serviceCode, [FromUri]string companyCode)
        {
            var result = webDAL.SelectVesselByCompany(serviceCode, companyCode);
            return Json<DataSet1.T_OCRX_VESSELDataTable>(result);
        }

        [HttpGet]
        public JsonResult<List<string>> SelectVesselOwners([FromUri]string vslaliase)
        {
            var result = webDAL.SelectVesselOwners(vslaliase);
            return Json<List<string>>(result);
        }

        [HttpGet]
        public JsonResult<List<string>> SelectCompanyVesselOwner([FromUri]string vslaliase, [FromUri]string CompanyCode, [FromUri] bool disc)
        {
            var result = webDAL.SelectCompanyVesselOwner(vslaliase,CompanyCode,disc);
            return Json(result);
        }

        [HttpGet]
        public JsonResult<DataTable> GetDischargeList([FromUri]string vslaliase, [FromUri]string lineid)
        {
            var result = webDAL.GetDischargeList(vslaliase, lineid);
            return Json<DataTable>(result);
        }

        [HttpGet]
        public JsonResult<DataTable> GetLoadingList([FromUri]string vslaliase, [FromUri]string lineid)
        {
            var result = webDAL.GetLoadingList(vslaliase, lineid);
            return Json<DataTable>(result);
        }

        [HttpPost]
        public void InsertVessel([FromBody]DataSet1.T_OCRX_VESSELDataTable row)
        {
            webDAL.InsertVessel(row[0]);
            //return Json<DataSet1.T_OCRX_CODataTable>(result);
        }

        [HttpPut]
        public void UpdateVessel([FromBody]DataSet1.T_OCRX_VESSELDataTable row)
        {
            webDAL.UpdateVessel(row[0]);
            //return Json<DataSet1.T_OCRX_CODataTable>(result);
        }

        [HttpDelete]
        public void DeleteVessel([FromBody]DataSet1.T_OCRX_VESSELDataTable row)
        {
            webDAL.DeleteVessel(row[0]);
            //return Json<DataSet1.T_OCRX_CODataTable>(result);
        }

        /// <summary>
        /// 船期监控
        /// </summary>
        /// <returns></returns>
        public JsonResult<DataTable> SelectVslMonitor2()
        {
            var result = webDAL.SelectVslMonitor2();

            return Json<DataTable>(result);
        }
    }
}
