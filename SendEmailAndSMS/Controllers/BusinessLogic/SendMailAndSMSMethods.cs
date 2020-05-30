using BusinessLibrary;
using EntityDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace SendEmailAndSMS.Models
{
    public static class SendMailAndSMSMethods
    {
        public static string SendMailCount(string MandateID, string AppId, string EmailCount, string SmsCount, int SMSLength, string MessageRequestId)
        {
            string status = "";
            try
            {
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[Convert.ToString(AppId)].ConnectionString);
                //string query = "Sp_SendEmail";
                //SqlCommand cmd = new SqlCommand(query, con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@QueryType", "SendMail");
                //cmd.Parameters.AddWithValue("@MandateID", MandateID);
                //cmd.Parameters.AddWithValue("@EmailCount", EmailCount);
                //cmd.Parameters.AddWithValue("@SmsCount", SmsCount);
                //cmd.Parameters.AddWithValue("@SMSLength", SMSLength);
                //cmd.Parameters.AddWithValue("@MessageRequestId", MessageRequestId);

                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                
                var Result = dbcontext.MultipleResults("[dbo].[Sp_SendEmail]").With<doneres>().
               Execute("@QueryType", "@MandateID", "@EmailCount", "@SmsCount", "@SMSLength", "@MessageRequestId", "@AppId", "SendMail", MandateID, EmailCount, SmsCount,SMSLength.ToString(),MessageRequestId, AppId);


            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(ex.Message);
                return ex.Message;
            }
            return status;
        }
       
        public static DataSet GetData_MandateID(string MandateId, string AppID, string WebAppUrl, string Activity, string UserId)
        {
            DataSet dsData = new DataSet();
            try
            {
                string TempId = AppID + MandateId;
                TempId = SendEmailAndSMSGlobal.Global.ReverseString(TempId);
                TempId = SendEmailAndSMSGlobal.Global.CreateRandomCode(6) + TempId;
                //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[Convert.ToString(AppID)].ConnectionString);
                //string query = "Sp_SendEmail";
                //SqlCommand cmd = new SqlCommand(query, con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@QueryType", "GetData_MandateID");
                //cmd.Parameters.AddWithValue("@MandateId", MandateId);
                //cmd.Parameters.AddWithValue("@Activity", Activity);
                //cmd.Parameters.AddWithValue("@WebAppUrl", WebAppUrl);
                //cmd.Parameters.AddWithValue("@EncodedMandateID", TempId);
                //cmd.Parameters.AddWithValue("@UserId", UserId);
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //da.Fill(dtset);

                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                List<GetMandateRes> GetMandateResList = new List<GetMandateRes>();
                List<SMS_MessageStringRes> SMS_MessageStringResList = new List<SMS_MessageStringRes>();
                List<Mail_MessageStringRes> Mail_MessageStringResList = new List<Mail_MessageStringRes>();
                List<SubjectHeadRes> SubjectHeadResList = new List<SubjectHeadRes>();
                
                var Result = dbcontext.MultipleResults("[dbo].[Sp_SendEmail]").With<GetMandateRes>().With<SMS_MessageStringRes>().With<Mail_MessageStringRes>().With<SubjectHeadRes>().
                        Execute("@QueryType", "@MandateId", "@Activity", "@WebAppUrl", "@EncodedMandateID", "@UserId", "@AppId", "@EncodedAppId", "GetData_MandateID", Convert.ToString(MandateId), "ESingle", WebAppUrl, TempId, UserId.ToString(),AppID,Encryptions.Dbsecurity.Encrypt(AppID));
                ///  return "1";

                //string query = "Sp_GetEmailData";
                //SqlCommand cmd = new SqlCommand(query, con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@QueryType", "ShowData");
                //cmd.Parameters.AddWithValue("@Id", MandateId);
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                GetMandateResList = Result[0].Cast<GetMandateRes>().ToList();
                SMS_MessageStringResList = Result[1].Cast<SMS_MessageStringRes>().ToList();
                Mail_MessageStringResList = Result[2].Cast<Mail_MessageStringRes>().ToList();
                SubjectHeadResList = Result[3].Cast<SubjectHeadRes>().ToList();
                var tb = new DataTable(typeof(GetMandateRes).Name);

                PropertyInfo[] props = typeof(GetMandateRes).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var prop in props)
                {
                    tb.Columns.Add(prop.Name, prop.PropertyType);
                }

                foreach (var item in GetMandateResList)
                {
                    var values = new object[props.Length];
                    for (var i = 0; i < props.Length; i++)
                    {
                        values[i] = props[i].GetValue(item, null);
                    }

                    tb.Rows.Add(values);
                }
                var tbSMS_MessageStringRes = new DataTable(typeof(SMS_MessageStringRes).Name);
                PropertyInfo[] propsSMS_MessageStringRes = typeof(SMS_MessageStringRes).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var prop in propsSMS_MessageStringRes)
                {
                    tbSMS_MessageStringRes.Columns.Add(prop.Name, prop.PropertyType);
                }

                foreach (var item in SMS_MessageStringResList)
                {
                    var values = new object[propsSMS_MessageStringRes.Length];
                    for (var i = 0; i < propsSMS_MessageStringRes.Length; i++)
                    {
                        values[i] = propsSMS_MessageStringRes[i].GetValue(item, null);
                    }
                    tbSMS_MessageStringRes.Rows.Add(values);
                }

                var tbMail_MessageStringResList = new DataTable(typeof(Mail_MessageStringRes).Name);
                PropertyInfo[] propsMail_MessageStringResList = typeof(Mail_MessageStringRes).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var prop in propsMail_MessageStringResList)
                {
                    tbMail_MessageStringResList.Columns.Add(prop.Name, prop.PropertyType);
                }
                foreach (var item in Mail_MessageStringResList)
                {
                    var values = new object[propsMail_MessageStringResList.Length];
                    for (var i = 0; i < propsMail_MessageStringResList.Length; i++)
                    {
                        values[i] = propsMail_MessageStringResList[i].GetValue(item, null);
                    }
                    tbMail_MessageStringResList.Rows.Add(values);
                }

                var tbSubjectHeadResList = new DataTable(typeof(SubjectHeadRes).Name);
                PropertyInfo[] propsSubjectHeadResList = typeof(SubjectHeadRes).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (var prop in propsSubjectHeadResList)
                {
                    tbSubjectHeadResList.Columns.Add(prop.Name, prop.PropertyType);
                }
                foreach (var item in SubjectHeadResList)
                {
                    var values = new object[propsSubjectHeadResList.Length];
                    for (var i = 0; i < propsSubjectHeadResList.Length; i++)
                    {
                        values[i] = propsSubjectHeadResList[i].GetValue(item, null);
                    }
                    tbSubjectHeadResList.Rows.Add(values);
                }

               
                dsData.Tables.Add(tb);
                dsData.Tables.Add(tbSMS_MessageStringRes);
                dsData.Tables.Add(tbMail_MessageStringResList);
                dsData.Tables.Add(tbSubjectHeadResList);



            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(ex.Message);
            }
            return dsData;
        }
        public static DataSet GetMobileNo(string MandateId, string AppID,string EmandateType)
        {
            DataSet dsData = new DataSet();
            try
            {
                               
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                List<GetMobileNoRes> GetMandateResList = new List<GetMobileNoRes>();


               var Result = dbcontext.MultipleResults("[dbo].[sp_ESign]").With<GetMobileNoRes>().
              Execute("@QueryType", "@mandateid", "@emandatetype", "@AppId", "GetMobileNo",MandateId,EmandateType,AppID);
                
                GetMandateResList = Result[0].Cast<GetMobileNoRes>().ToList();
               
                var tb = new DataTable(typeof(GetMobileNoRes).Name);

                PropertyInfo[] props = typeof(GetMobileNoRes).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var prop in props)
                {
                    tb.Columns.Add(prop.Name, prop.PropertyType);
                }

                foreach (var item in GetMandateResList)
                {
                    var values = new object[props.Length];
                    for (var i = 0; i < props.Length; i++)
                    {
                        values[i] = props[i].GetValue(item, null);
                    }

                    tb.Rows.Add(values);
                }
                dsData.Tables.Add(tb);
               
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(ex.Message);
            }
            return dsData;
        }
    }
}