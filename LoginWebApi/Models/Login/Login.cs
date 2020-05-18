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
        
        List<CommonFlag> common = new List<CommonFlag>();
        CommonFlag Flag = new CommonFlag();
        int Isallowattempt = 5;
        public IEnumerable<CommonFlag> Binddetails(Formdataget Login)
        {
         
            List<Logindetails> dataList = new List<Logindetails>();
            try
            {
              
               var Result =  dbcontext.MultipleResults ("[dbo].[sp_UserLogin]").With<Logindetails>().Execute("@QueryType", "@UserName", "@Appid", "GetUser",Login.UserName,Login.APPID);
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
                                var SaveLoginSessionTrxn = dbcontext.MultipleResults("[dbo].[sp_UserLogin]").With<SaveLoginSessionTrxn>().Execute("@QueryType", "@UserId", "@TokenID", "@IPAddress", "@MacAddress", "@IsLogin", "@Iscorrectattempt", "@Appid", "SaveLoginSessionTrxn", Convert.ToString(Logindata.Cast<Logindetails>().ToList().Select(x => x.UserId).First().ToString()), Convert.ToString(generator.Next(1, 1000000)), Convert.ToString(GetMacIP.GetIpAddress()), Convert.ToString(GetMacIP.GetMacAddress()), Convert.ToString(1), Convert.ToString(1),Login.APPID);
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
                                    Flag.IsDefaultPswdChange =Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.IsDefaultPswdChange).First().ToString()));
                                    Flag.LastLogin = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.LastLogin).First().ToString()));
                                    Flag.IsActive = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.IsActive).First().ToString()));
                                    Flag.IsDeleted = Dbsecurity.Encrypt(Convert.ToString(dataList.Cast<Logindetails>().ToList().Select(x => x.IsDeleted).First().ToString()));
                                    Flag.CreatedBy = Dbsecurity.Encrypt(dataList.Cast<Logindetails>().ToList().Select(x => x.CreatedBy).First().ToString());
                                    Flag.CreatedOn = Dbsecurity.Encrypt(dataList.Cast<Logindetails>().ToList().Select(x => x.CreatedOn).First().ToString());
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
       
       
    }

}