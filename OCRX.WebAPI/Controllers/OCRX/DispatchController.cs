using OCR.Model;
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
    public class DispatchController : ApiController
    {
        [HttpGet]
        public JsonResult<OcrCnt.T_OCR_CNTDataTable> SelectDispatch()
        {
            var result = webDAL.SelectDispatch();
            return Json<OcrCnt.T_OCR_CNTDataTable>(result);
        }

        [HttpGet]
        public JsonResult<OcrCnt.T_OCR_CNTDataTable> SelectDispatch2()
        {
            var result = webDAL.SelectDispatch2();
            return Json<OcrCnt.T_OCR_CNTDataTable>(result);
        }

        [HttpGet]
        public JsonResult<OcrCnt.T_OCR_CNTDataTable> SelectDispatch3()
        {
            var result = webDAL.SelectDispatch3();
            return Json<OcrCnt.T_OCR_CNTDataTable>(result);
        }

        [HttpPost]
        public void InsertCntx([FromBody]DataSet1.T_OCRX_CNTDataTable row)
        {
            webDAL.InsertCntx(row[0]);
        }

        [HttpPost]
        public void UpdateDispatched([FromUri]decimal dock_id, [FromUri]string lineCode, [FromUri]string companyCode)
        {
            webDAL.UpdateDispatched(dock_id, lineCode, companyCode);
        }
    }
}
