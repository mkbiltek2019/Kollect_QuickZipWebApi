using BusinessLibrary;
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
            string msg = "";
            try
            {
                msg = SendmailSMS.SemdEmailSms(Data);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return msg;
        }
        [Route("api/SendEmail/GetEncryptedmandateId")]
        [HttpPost]
        public string GetEncryptedmandateId(GetMandateReq Data)
        {
            string URl = "";
            string TempId = "";
            try
            {
                TempId = Dbsecurity.Decrypt(Data.AppId) + Dbsecurity.Decrypt(Data.MandateId);
                TempId = Global.ReverseString(TempId);
                TempId = Global.CreateRandomCode(6) + TempId;

                List<GetCredential> GetCredentialList = new List<GetCredential>();
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                var Result = dbcontext.MultipleResults("[dbo].[Sp_GetMandatemodeData]").With<GetCredential>().
               Execute("@QueryType", "@AppId", "GetEntityCredential", Dbsecurity.Decrypt(Data.AppId));
                GetCredentialList = Result[0].Cast<GetCredential>().ToList();
                try
                {
                    dbcontext = new QuickCheck_AngularEntities();
                    var Result1 = dbcontext.MultipleResults("[dbo].[sp_ESign]").With<doneres>().
                   Execute("@QueryType", "@MandateId", "@WebAppUrl", "@EncodedMandateID", "@AppId", "@EncodedAppId", "SaveLinkWithoutSMS", Dbsecurity.Decrypt(Data.MandateId), GetCredentialList[0].EWebAppUrl, TempId, Dbsecurity.Decrypt(Data.AppId),Data.AppId);

                    URl = "/MandateDetails?ID=" + TempId + "&AppId=" + Data.AppId;
                    // string WebAppUrl = ConfigurationManager.AppSettings["EWebAppUrl"].ToString();
                    //  CommonManger.FillDatasetWithParam("sp_ESign", );
                }
                catch(Exception ex) { }
               
            }
            catch (Exception ex)
            { return "0"; }
            return URl;
        }

    }
}
