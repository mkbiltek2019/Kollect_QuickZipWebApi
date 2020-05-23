using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Net.Http;
using BusinessLibrary;
using EntityDAL;
using Encryptions;
namespace LoginWebApi.Models.Login
{
    public class Login
    {
        QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
       
        public IEnumerable<CommonFlag> Binddetails(Formdataget Login)
        {
            List<CommonFlag> common = new List<CommonFlag>();
            CommonFlag Flag = new CommonFlag();
            int Isallowattempt = 5;
            List<Logindetails> dataList = new List<Logindetails>();
            try
            {

                var Result = dbcontext.MultipleResults("[dbo].[sp_UserLogin]").With<Logindetails>().Execute("@QueryType", "@UserName", "@Appid", "GetUser", Login.UserName, Login.APPID);
                foreach (var Logindata in Result)
                {
                    dataList = Logindata.Cast<Logindetails>().ToList();
                    if (dataList.Count > 0)
                    {
                        string strDbPassword = Dbsecurity.Decypt(Convert.ToString(Logindata.Cast<Logindetails>().ToList().Select(x => x.Password).First().ToString()));
                        if (strDbPassword.Trim() != Dbsecurity.Decypt(Login.Password))
                        {
                            if (Convert.ToBoolean(Logindata.Cast<Logindetails>().ToList().Select(x => x.IsAccountBlocked).First().ToString()) == true)
                            {
                                Flag.Flag = "0";
                                Flag.FlagValue = "Account is blocked please contact to admin!!";
                                common.Add(Flag);
                            }
                            else if (Convert.ToInt32(Logindata.Cast<Logindetails>().ToList().Select(x => x.IswrongAttempt).First().ToString()) >= Isallowattempt)
                            {
                                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                                var Updateaccountblockflag = dbcontext.MultipleResults("[dbo].[sp_UserLogin]").With<Logindetails>().Execute("@QueryType", "@UserName", "@Appid", "@IsAccountblock", "GetUser", Login.UserName, Login.APPID, Convert.ToString(1));
                                Flag.Flag = "0";
                                Flag.FlagValue = "Account is blocked please contact to admin!!";
                                common.Add(Flag);
                            }
                            else if (Convert.ToInt32(Logindata.Cast<Logindetails>().ToList().Select(x => x.IswrongAttempt).First().ToString()) != Isallowattempt)
                            {
                                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                                var Updateleftattemptflag = dbcontext.MultipleResults("[dbo].[sp_UserLogin]").With<Logindetails>().Execute("@QueryType", "@UserName", "@Appid", "@Iswrongattempt", "GetUser", Login.UserName, Login.APPID, Convert.ToString(1));
                                int attempt = Convert.ToInt32(Updateleftattemptflag.FirstOrDefault().Cast<Logindetails>().ToList().Select(x => x.IswrongAttempt).First().ToString());
                                if (attempt == Isallowattempt)
                                {
                                    QuickCheck_AngularEntities dbcontext1 = new QuickCheck_AngularEntities();
                                    var Updateaccountblockflag = dbcontext1.MultipleResults("[dbo].[sp_UserLogin]").With<Logindetails>().Execute("@QueryType", "@UserName", "@Appid", "@IsAccountblock", "GetUser", Login.UserName, Login.APPID, Convert.ToString(1));
                                    Flag.Flag = "0";
                                    Flag.FlagValue = "Account is blocked please contact to admin!!";
                                    common.Add(Flag);
                                }
                                else
                                {
                                    int leftAttempt = Isallowattempt - attempt;
                                    Flag.Flag = "0";
                                    Flag.FlagValue = "Login Failed! Left attempt is '" + leftAttempt + "'!!";
                                    common.Add(Flag);
                                }
                            }
                            else if (Convert.ToInt32(Logindata.Cast<Logindetails>().ToList().Select(x => x.IswrongAttempt).First().ToString()) != Isallowattempt)
                            {
                                Flag.Flag = "0";
                                Flag.FlagValue = "Account is blocked please contact to admin!!";
                                common.Add(Flag);
                            }



                        }
                        else
                        {
                            if (Convert.ToBoolean(Logindata.Cast<Logindetails>().ToList().Select(x => x.IsAccountBlocked).First().ToString()) == true)
                            {
                                Flag.Flag = "0";
                                Flag.FlagValue = "Account is blocked please contact to admin!!";
                                common.Add(Flag);
                            }
                            else
                            {
                                Random generator = new Random();
                                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                                var SaveLoginSessionTrxn = dbcontext.MultipleResults("[dbo].[sp_UserLogin]").With<SaveLoginSessionTrxn>().Execute("@QueryType", "@UserId", "@TokenID", "@IPAddress", "@MacAddress", "@IsLogin", "@Iscorrectattempt", "@Appid", "SaveLoginSessionTrxn", Convert.ToString(Logindata.Cast<Logindetails>().ToList().Select(x => x.UserId).First().ToString()), Convert.ToString(generator.Next(1, 1000000)), Convert.ToString(GetMacIP.GetIpAddress()), Convert.ToString(GetMacIP.GetMacAddress()), Convert.ToString(1), Convert.ToString(1), Login.APPID);
                                foreach (var Existlogin in SaveLoginSessionTrxn)
                                {

                                    //  if (Existlogin.Cast<SaveLoginSessionTrxn>().ToList().Select(x => x.SessionActive).First().ToString() == "0")
                                    //  {
                                    #region Session creation
                                    // Iace.User.User.SaveUserToSession(dataList);
                                    Flag.IsRefrenceCheck = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.IsRefrenceCheck).First().ToString()));
                                    Flag.IsOverPrintMandate = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.IsOverPrintMandate).First().ToString()));
                                    Flag.IsBulkMandate = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.IsBulkMandate).First().ToString()));
                                    Flag.IsMandate = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.IsMandate).First().ToString()));
                                    //NewCode
                                    Flag.IsMandateEdit = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.IsMandateEdit).First().ToString()));
                                    Flag.IsRefrenceEdit = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.IsRefrenceEdit).First().ToString()));
                                    Flag.IsEmandate = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.IsEmandate).First().ToString()));
                                    Flag.IsPhysical = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.IsPhysical).First().ToString()));
                                    Flag.IsZipShoreABPS = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.IsZipShoreABPS).First().ToString()));
                                    Flag.UserId = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.UserId).First().ToString()));
                                    Flag.ReferenceId = Dbsecurity.Encrypt(dataList.Cast<Logindetails>().ToList().Select(x => x.ReferenceId).First().ToString());
                                    Flag.UserName = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.UserName).First().ToString()));
                                    Flag.Password = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.Password).First().ToString()));
                                    Flag.UserCode = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.UserCode).First().ToString()));
                                    Flag.UserType = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.UserType).First().ToString()));
                                    Flag.BranchId = Dbsecurity.Encrypt(dataList.Cast<Logindetails>().ToList().Select(x => x.BranchId).First().ToString());
                                    Flag.BranchName = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.BranchName).First().ToString()));
                                    Flag.IsDefaultPswdChange = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.IsDefaultPswdChange).First().ToString()));
                                    Flag.LastLogin = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.LastLogin).First().ToString()));
                                    Flag.IsActive = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.IsActive).First().ToString()));
                                    Flag.IsDeleted = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.IsDeleted).First().ToString()));
                                    Flag.CreatedBy = Dbsecurity.Encrypt(dataList.Cast<Logindetails>().ToList().Select(x => x.CreatedBy).First().ToString());
                                    Flag.CreatedOn = Dbsecurity.Encrypt(dataList.Cast<Logindetails>().ToList().Select(x => x.CreatedOn).First().ToString());
                                    Flag.AppId = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.AppId).First().ToString()));
                                    #endregion

                                    Flag.Flag = "1";
                                    common.Add(Flag);
                                    //}
                                    //else {
                                    //    Flag.Flag = "0";
                                    //    Flag.FlagValue = "User already logged on. Either Try logging in after closing the current session or Try after some time!!";
                                    //    common.Add(Flag);
                                    //}
                                }
                            }
                        }
                    }
                    else
                    {
                        Flag.Flag = "0";
                        Flag.FlagValue = "Login Failed!!";
                        common.Add(Flag);
                    }
                }
                return common;
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
        }
        public IEnumerable<EmailSent> SendMail(string email)
        {
            List<EmailSent> common = new List<EmailSent>();
            EmailSent emailobect = new EmailSent();
            try
            {
               
                List<Responsevalue> dataList = new List<Responsevalue>(); List<UserDetails> dataList1 = new List<UserDetails>();
                var Result = dbcontext.MultipleResults("[dbo].[Sp_UserLogin]").With<Responsevalue>().With<UserDetails>().Execute("@QueryType", "@EmailId", "ChkEmail", email);
                dataList = Result.FirstOrDefault().Cast<Responsevalue>().ToList();
                dataList1 = Result.LastOrDefault().Cast<UserDetails>().ToList();
                if (dataList.Cast<Responsevalue>().ToList().Select(x => x.value).First().ToString() == "1")
                {
                    if (dataList1.Count > 0)
                    {
                        using (StringWriter sw = new StringWriter())
                        {
                            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                            {
                                StringBuilder sb = new StringBuilder();
                                string WebAppUrl = ConfigurationManager.AppSettings["WebAppUrl"].ToString();
                                string SMTPHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
                                string FromMailId = ConfigurationManager.AppSettings["UserId"].ToString();
                                string MailPassword = ConfigurationManager.AppSettings["MailPassword"].ToString();
                                string SMTPPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();
                                string SMTPEnableSsl = ConfigurationManager.AppSettings["SMTPEnableSsl"].ToString();
                                sb.Append("Dear " + dataList1.Cast<UserDetails>().ToList().Select(x => x.UserName).First().ToString() + ",<br> <br>");
                                sb.Append("Please click on the below button to set a new Password . <br> <br>");
                                string User = HttpContext.Current.Server.UrlEncode(Dbsecurity.Encrypt(dataList1.Cast<UserDetails>().ToList().Select(x => x.UserId).First().ToString()));
                                sb.Append("<a href=' " + WebAppUrl + "ChangePassword/" + User + "' target='_blank'>");
                                sb.Append("<input style='background-color: #3965a9;color: #fff;padding: 3px 10px 3px 10px;' type='button' value='Change Password' /></a> </div>");

                                SmtpClient smtpClient = new SmtpClient();
                                MailMessage mailmsg = new MailMessage();
                                MailAddress mailaddress = new MailAddress(FromMailId);
                                mailmsg.To.Add(email);
                                mailmsg.From = mailaddress;

                                mailmsg.Subject = "Recover Password";
                                mailmsg.IsBodyHtml = true;
                                mailmsg.Body = sb.ToString();

                                smtpClient.Host = SMTPHost;
                                smtpClient.Port = Convert.ToInt32(SMTPPort);
                                smtpClient.EnableSsl = Convert.ToBoolean(SMTPEnableSsl);
                                smtpClient.UseDefaultCredentials = true;
                                smtpClient.Credentials = new System.Net.NetworkCredential(FromMailId, MailPassword);
                                smtpClient.Send(mailmsg);
                                //QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                                //dbcontext.MultipleResults("[dbo].[Sp_SendEmail]").With<Responsevalue>().Execute("@QueryType", "@MandateId", "@EmailCount", "@SmsCount", "SendMail", Convert.ToString(0), "1", "0");

                                emailobect.Flag = "1";
                                emailobect.FlagValue = "Email sent successfully!!";
                                common.Add(emailobect);

                            }
                        }
                    }
                }
                else {
                    emailobect.Flag = "0";
                    emailobect.FlagValue = "Invalid EmailID!!";
                    common.Add(emailobect);
                }

            }
            catch (Exception)
            {
                emailobect.Flag = "0";
                emailobect.FlagValue = "Email sent not successfully!!";
                common.Add(emailobect);
            }

            return common;
        }
        public IEnumerable<ChangePasswordRes> UpdatePassworddtail(ChangePasswordJsn changepassword)
        {
            List<ChangePasswordRes> common = new List<ChangePasswordRes>();
            ChangePasswordRes changepasswordobect = new ChangePasswordRes();
            try
            {
                List<Forgotflag> dataList = new List<Forgotflag>();
                var Result = dbcontext.MultipleResults("[dbo].[sp_UserLogin]").With<Forgotflag>().Execute("@QueryType", "@ChangePassword",
                           "@UserId", "UpdatePassword",changepassword.password, Dbsecurity.Decypt(changepassword.Userid));
                dataList = Result.FirstOrDefault().Cast<Forgotflag>().ToList();
                if (dataList.Count > 0)
                {
                    changepasswordobect.Flag = "1";
                    changepasswordobect.FlagValue = "Password Updated Successfuly !!";
                    common.Add(changepasswordobect);
                }
                else
                {
                    changepasswordobect.Flag = "0";
                    changepasswordobect.FlagValue = "Invalid UserId !!";
                    common.Add(changepasswordobect);
                }
            }
            catch (Exception ex) { throw ex; }
            return common;
        }


    }

}