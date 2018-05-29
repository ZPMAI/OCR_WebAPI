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
    public class CTOSController : ApiController
    {
        [HttpGet]
        public JsonResult<CtosResult> CM005001([FromUri]string CONTAINERNO)
        {
            string TICKET_ID = System.Configuration.ConfigurationManager.AppSettings["Ticket_Id"];
            return Json < CtosResult >( CTOSApi.CM005001(CONTAINERNO, TICKET_ID));
        }
    }
}
