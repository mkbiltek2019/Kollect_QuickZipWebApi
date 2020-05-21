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
        [Route("api/UploadMandate/SendLinks")]
        [HttpPost]
        public GetSendLinkResponse GetMandateInfo(GetMandateReq Data)
        {
            bool Flag = true;
            GetSendLinkResponse response = new GetSendLinkResponse();


            DataTable dt = SendMailAndSMSMethods.GetMandateData(Data.MdtID, Data.AppID);
            string WebAppUrl = ConfigurationManager.AppSettings["ENachUrl" + Data.AppID].ToString();

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[Convert.ToString(Data.AppID)].ConnectionString);
            //bool Flag = false;
            // string temp = ConfigurationManager.AppSettings["EnitityMarchantKey" + ul.AppID];
            string UserIdd = "";
            string EnityId = "";


            try
            {
                DataSet ds = new DataSet();
                int SMSlen = 0;
                int EmailCount = 0;
                int SmsCount = 0;
                string MessageRequestId = "";
                if (dt.Rows.Count > 0 && dt != null)
                {

                    string ID = Convert.ToString(dt.Rows[0]["MandateId"]);
                    string PhoneNumber = Convert.ToString(dt.Rows[0]["PhoneNumber"]);
                    string Email = Convert.ToString(dt.Rows[0]["PhoneNumber"]);
                    if (Convert.ToString(dt.Rows[0]["Emandate"]).ToLower() == "true")
                    {
                        DataSet dtset = SendMailAndSMSMethods.GetData_MandateID(Data.MdtID, Data.AppID, WebAppUrl, "ESingle", UserIdd);
                        if (Convert.ToString(dt.Rows[0]["PhoneNumber"]) != "" && SendMailAndSMSMethods.CheckSMSAndEMail(Convert.ToString(dt.Rows[0]["EntityId"]), Data.AppID, "S"))
                        {
                            Flag = false;
                            StringBuilder sb = new StringBuilder();

                            sb.Append(dtset.Tables[1].Rows[0][0].ToString());


                            string URL = "http://api.msg91.com/api/sendhttp.php?sender=" + ConfigurationManager.AppSettings["SenderId" + Data.AppID] + "&route=4&mobiles=" + Convert.ToString(dt.Rows[0]["PhoneNumber"]) + "&authkey=" + ConfigurationManager.AppSettings["authkey"] + "&country=91&message=" + sb.ToString() + "";

                            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);

                            request.Method = "POST";
                            request.ContentType = "application/json";
                            //string response1 = "";

                            WebResponse webResponse = request.GetResponse();
                            using (Stream webStream = webResponse.GetResponseStream())
                            {
                                if (webStream != null)
                                {
                                    using (StreamReader responseReader = new StreamReader(webStream))
                                    {
                                        MessageRequestId = responseReader.ReadToEnd();
                                        // Console.Out.WriteLine(response);
                                    }
                                }
                            }
                            SMSlen = Convert.ToString(dtset.Tables[1].Rows[0][0]).Length;
                            if (SMSlen <= 160)
                            {
                                SmsCount = 1;
                            }
                            if (SMSlen > 160 && SMSlen <= 306)
                            {
                                SmsCount = 2;
                            }
                            if (SMSlen > 306 && SMSlen <= 459)
                            {
                                SmsCount = 3;
                            }
                            if (SMSlen > 459)
                            {
                                SmsCount = 4;
                            }

                        }

                        if ((Convert.ToString(dt.Rows[0]["EmailID"]).Trim()) != "" && SendMailAndSMSMethods.CheckSMSAndEMail(Convert.ToString(dt.Rows[0]["EntityId"]), Data.AppID, "E"))
                        {
                            Flag = false;
                            StringBuilder sb = new StringBuilder();
                            WebAppUrl = ConfigurationManager.AppSettings["ENachUrl" + Data.AppID].ToString();

                            string Teamtext = ConfigurationManager.AppSettings["Team" + Data.AppID].ToString().Replace("less", "<").Replace("great", ">");


                            string SMTPHost = ConfigurationManager.AppSettings["Amazon_SMTPHost"].ToString();
                            string UserId = ConfigurationManager.AppSettings["Amazon_UserId"].ToString();
                            string MailPassword = ConfigurationManager.AppSettings["Amazon_MailPassword"].ToString();
                            string SMTPPort = ConfigurationManager.AppSettings["Amazon_SMTPPort"].ToString();
                            string SMTPEnableSsl = ConfigurationManager.AppSettings["Amazon_SMTPEnableSsl"].ToString();
                            string FromMailId = ConfigurationManager.AppSettings["Amazon_FromMailId" + Data.AppID].ToString();


                            sb.Append(dtset.Tables[2].Rows[0][0].ToString());
                            sb.Append(Convert.ToString(ConfigurationManager.AppSettings["Team" + Data.AppID])).Replace("less", "<").Replace("great", ">"); ;
                            sb.Append("<br/>");
                            sb.Append("<i style='font-size:11px'>(Email Generated from Unattendable MailBox, Please Do Not reply.)</i>");

                            SmtpClient smtpClient = new SmtpClient();
                            MailMessage mailmsg = new MailMessage();
                            MailAddress mailaddress = new MailAddress(FromMailId);
                            mailmsg.To.Add(Convert.ToString(dt.Rows[0]["EmailID"]));

                            mailmsg.From = mailaddress;
                            mailmsg.Subject = dtset.Tables[3].Rows[0][0].ToString();//"E - Mandate";
                            mailmsg.IsBodyHtml = true;
                            mailmsg.Body = sb.ToString();
                            smtpClient.Host = SMTPHost;
                            smtpClient.Port = Convert.ToInt32(SMTPPort);
                            smtpClient.EnableSsl = Convert.ToBoolean(SMTPEnableSsl);
                            smtpClient.UseDefaultCredentials = true;
                            smtpClient.Credentials = new System.Net.NetworkCredential(UserId, MailPassword);
                            smtpClient.Send(mailmsg);
                            //CheckMandateInfo.SendMailCount(Data.MdtID, Data.AppID, "1", "0",0,"");
                            //   CommonManger.FillDatasetWithParam("Sp_SendEmail", "@QueryType", "@MandateId", "@EmailCount", "@SmsCount", "SendMail", Convert.ToString(dt.Rows[l]["MandateId"]), "1", "0");
                            EmailCount = 1;

                        }
                        SendMailAndSMSMethods.SendMailCount(Data.MdtID, Data.AppID, EmailCount.ToString(), SmsCount.ToString(), SMSlen, MessageRequestId);
                        response.Message = "Email and SMS have been sent successfully";
                        response.ResCode = "ykR20041";
                        response.Status = "Success";

                        if (Flag)
                        {
                            response.Message = "Email and phone number do not exist";
                            response.ResCode = "ykR20042";
                            response.Status = "Failure";
                        }



                    }

                }
            }
            catch (Exception ex)
            {
                response.Message = "Invalid data";
                response.Status = "Failure";
                response.ResCode = "ykR20020";
            } 



            return response;
        }


    }
}
