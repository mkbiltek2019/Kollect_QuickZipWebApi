using Encryptions;
using EntityDAL;
using SendEmailAndSMS.Controllers.BusinessLogic;
using SendEmailAndSMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Web.Http;

namespace SendEmailAndSMS.Controllers
{
    public class SendEmailController : ApiController
    {
        [Route("api/SendEmail/SendLinks")]
        [HttpPost]
        public string GetMandateInfo(GetMandateReq Data)
        {
            try
            {
                SendmailSMS.SemdEmailSms(Data);
               }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "";
        }


    }
}
