using OCR.BLL;
using OCR.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace OCRX.WebAPI.Controllers
{
    public class EMailController : ApiController
    {
        [HttpPost]
        public void SendEMail(EMail mail)
        {
            Mails ml = new Mails(mail.To,
                        mail.Subject,
                        mail.Content, null);
            ml.Send();
        }
    }
}
