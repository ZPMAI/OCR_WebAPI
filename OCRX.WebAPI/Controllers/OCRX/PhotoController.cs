using OCR.Model;
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
    public class PhotoController : ApiController
    {
        [HttpGet]
        public JsonResult<OcrPhoto.T_OCR_PHOTODataTable> SelectPhotos([FromUri]decimal dock_id)
        {
            var result = webDAL.SelectPhotos(dock_id);

            return Json<OcrPhoto.T_OCR_PHOTODataTable>(result);
        }
       
        [HttpPost]
        public void CopyPhotos([FromUri]decimal dock_id1, [FromUri]decimal dock_id2)
        {
            webDAL.CopyPhotos(dock_id1, dock_id2);
        }
    }
}
