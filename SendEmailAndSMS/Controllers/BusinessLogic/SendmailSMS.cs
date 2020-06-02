using BusinessLibrary;
using Encryptions;
using EntityDAL;
using SendEmailAndSMS.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;

namespace SendEmailAndSMS.Controllers.BusinessLogic
{
    public static class SendmailSMS
    {
        public static string SemdEmailSms(GetMandateReq Data)
        {
            string Msg = "";
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                List<GetCredential> GetCredentialList = new List<GetCredential>();


                var Result = dbcontext.MultipleResults("[dbo].[Sp_GetMandatemodeData]").With<GetCredential>().
               Execute("@QueryType", "@AppId", "GetEntityCredential",  Dbsecurity.Decrypt(Data.AppId));

                GetCredentialList = Result[0].Cast<GetCredential>().ToList();

                string WebAppUrl = GetCredentialList[0].EWebAppUrl; // ConfigurationManager.AppSettings["EWebAppUrl"].ToString();
                string SMTPHost = GetCredentialList[0].Amazon_SMTPHost;// ConfigurationManager.AppSettings["Amazon_SMTPHost"].ToString();
                string UserId = GetCredentialList[0].Amazon_UserId;// ConfigurationManager.AppSettings["Amazon_UserId"].ToString();
                string MailPassword = GetCredentialList[0].Amazon_MailPassword;// ConfigurationManager.AppSettings["Amazon_MailPassword"].ToString();
                string SMTPPort = GetCredentialList[0].Amazon_SMTPPort;// ConfigurationManager.AppSettings["Amazon_SMTPPort"].ToString();
                string SMTPEnableSsl =Convert.ToString(GetCredentialList[0].Amazon_SMTPEnableSsl);// ConfigurationManager.AppSettings["Amazon_SMTPEnableSsl"].ToString();
                string FromMailId = GetCredentialList[0].Amazon_FromMailId;// ConfigurationManager.AppSettings["Amazon_FromMailId" + Dbsecurity.Decrypt(Data.AppId)].ToString();
                string Teamtext = GetCredentialList[0].Team; //ConfigurationManager.AppSettings["Team"].ToString();

                string response = string.Empty;//Added by Bibhu on 18Mar2020  ** Reason to insert MessageRequstId SP-
                int SmsCount = 0;
                int EmailCount = 0;
                Boolean IsMail = false;
                Boolean IsSMS = false;
                int SMSlen = 0;

              
                string mandateid = Dbsecurity.Decrypt(Data.MandateId);
                //string Emandatetype = Data.Emandatetype;// txtmandatetype.Text;
                string EntityName = "";
                string Amt = "";
                string TempId = Dbsecurity.Decrypt(Data.AppId) + mandateid;
                TempId = Global.ReverseString(TempId);
                TempId = Global.CreateRandomCode(6) + TempId;
                DataSet ds = SendMailAndSMSMethods.GetMobileNo(mandateid, Dbsecurity.Decrypt(Data.AppId), Data.Emandatetype);//CommonManger.FillDatasetWithParam("sp_ESign", "@QueryType", "@mandateid", "@emandatetype", "GetMobileNo", mandateid, Emandatetype);
                DataSet dtset = SendMailAndSMSMethods.GetData_MandateID(mandateid, Dbsecurity.Decrypt(Data.AppId), WebAppUrl, "ESingle", Dbsecurity.Decrypt(Data.UserId)); //CommonManger.FillDatasetWithParam("Sp_SendEmail", "@QueryType", "@MandateId", "@Activity", "@WebAppUrl", "@EncodedMandateID", "@UserId", "GetData_MandateID", Convert.ToString(mandateid), "ESingle", WebAppUrl, TempId,Dbsecurity.Decrypt(Data.UserId).ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    EntityName = Convert.ToString(ds.Tables[0].Rows[0]["Entityname"]);
                    Amt = Dbsecurity.Decrypt(Convert.ToString(ds.Tables[0].Rows[0]["AmountRupees"]));
                    if (Data.EntityDebitValidateMail == "true" || Data.EntityNetValidateMail == "true")
                    {
                        try
                        {
                            StringBuilder sb = new StringBuilder();
                            sb.Append(dtset.Tables[2].Rows[0][0].ToString());
                            sb.Append("Team " + GetCredentialList[0].Team); //Convert.ToString(ConfigurationManager.AppSettings["Team"]));
                            sb.Append("<br/>");
                            sb.Append("<i style='font-size:11px'>(Email Generated from Unattendable MailBox, Please Do Not reply.)</i>");

                            SmtpClient smtpClient = new SmtpClient();
                            MailMessage mailmsg = new MailMessage();
                            MailAddress mailaddress = new MailAddress(FromMailId);
                            mailmsg.To.Add(ds.Tables[0].Rows[0]["emailid"].ToString());
                            mailmsg.From = mailaddress;
                            mailmsg.Subject = dtset.Tables[3].Rows[0][0].ToString() + " " + GetCredentialList[0].Team;// Convert.ToString(ConfigurationManager.AppSettings["Team"]);
                            mailmsg.IsBodyHtml = true;
                            mailmsg.Body = sb.ToString();
                            smtpClient.Host = SMTPHost;
                            smtpClient.Port = Convert.ToInt32(SMTPPort);
                            smtpClient.EnableSsl = Convert.ToBoolean(SMTPEnableSsl);
                            smtpClient.UseDefaultCredentials = false;
                            smtpClient.Credentials = new System.Net.NetworkCredential(UserId, MailPassword);
                            smtpClient.Send(mailmsg);
                            IsMail = true;
                            EmailCount = 1;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    if (Data.EntityNetSMS == "true" || Data.EntityDebitSMS == "true")
                    {
                        string[] arr = EntityName.Split(' ');
                        StringBuilder sb = new StringBuilder();

                        //sb.Append("To process Mandate Reg. amt " + Amt + " at " + arr[0] + " click " + WebAppUrl + "Master/MandateDetails.aspx?ID=" + DbSecurity.Base64Encode(mandateid));
                        sb.Append(dtset.Tables[1].Rows[0][0].ToString());

                        string URL = "";
                  if( GetCredentialList[0].IsClientSmsApi=="0") //  if (ConfigurationManager.AppSettings["IsSmsApi"] == "0")
                        {
                            //URL = "http://api.msg91.com/api/sendhttp.php?sender=" + ConfigurationManager.AppSettings["SenderId" + Dbsecurity.Decrypt(Data.AppId)] + "&route=4&mobiles=" + Convert.ToString(ds.Tables[0].Rows[0]["phonenumber"].ToString()) + "&authkey=" + ConfigurationManager.AppSettings["authkey"] + "&country=91&message=" + sb.ToString() + "";

                            URL = "http://api.msg91.com/api/sendhttp.php?sender=" + GetCredentialList[0].SenderId+ "&route=4&mobiles=" + Convert.ToString(ds.Tables[0].Rows[0]["phonenumber"].ToString()) + "&authkey=" + GetCredentialList[0].AuthKey + "&country=91&message=" + sb.ToString() + "";

                        }
                        else
                        {
                            URL = ConfigurationManager.AppSettings["IsSmsApiurL"] + "&mobiles=" + Convert.ToString(ds.Tables[0].Rows[0]["phonenumber"].ToString()) + "&message=" + sb.ToString() + "&sender=" + ConfigurationManager.AppSettings["IsSmsApisender"] + "&route=" + ConfigurationManager.AppSettings["IsSmsApiroute"] + "&country=" + ConfigurationManager.AppSettings["IsSmsApicountry"] + "";
                        }
                        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
                        request.Method = "POST";
                        request.ContentType = "application/json";

                        try
                        {
                            WebResponse webResponse = request.GetResponse();
                            using (Stream webStream = webResponse.GetResponseStream())
                            {
                                if (webStream != null)
                                {
                                    using (StreamReader responseReader = new StreamReader(webStream))
                                    {
                                        //string response = responseReader.ReadToEnd(); // Commented by Bibhu on 18Mar2020
                                        response = responseReader.ReadToEnd(); // Added by Bibhu on 18Mar2020
                                                                               // Console.Out.WriteLine(response);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.Out.WriteLine("-----------------");
                            Console.Out.WriteLine(ex.Message);
                        }
                        IsSMS = true;
                        SMSlen = (dtset.Tables[1].Rows[0][0].ToString()).Length;

                    }

                }

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

                
                SendMailAndSMSMethods.SendMailCount(mandateid, Dbsecurity.Decrypt(Data.AppId), EmailCount.ToString(), SmsCount.ToString(), SMSlen, response);

                Msg = "Email and SMS sent successfully on customer email address and mobile number";
            }
            catch(Exception ex)
            {
                Msg = "Some thing went wrong";
            }
            return Msg;
        }
    }
}