using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SendEmailAndSMS.Models
{
    public class SendMailAndSMSMethods
    {
        public static string SendMailCount(string MandateID, string AppId, string EmailCount, string SmsCount, int SMSLength, string MessageRequestId)
        {
            string status = "";
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[Convert.ToString(AppId)].ConnectionString);
                string query = "Sp_SendEmail";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "SendMail");
                cmd.Parameters.AddWithValue("@MandateID", MandateID);
                cmd.Parameters.AddWithValue("@EmailCount", EmailCount);
                cmd.Parameters.AddWithValue("@SmsCount", SmsCount);
                cmd.Parameters.AddWithValue("@SMSLength", SMSLength);
                cmd.Parameters.AddWithValue("@MessageRequestId", MessageRequestId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    status = "";
                }


            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(ex.Message);
                return ex.Message;
            }
            return status;
        }
        public static DataTable GetMandateData(string MandateID, string AppId)
        {

            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[Convert.ToString(AppId)].ConnectionString);
                string query = "Sp_WebAPI";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "GetMandateData");
                cmd.Parameters.AddWithValue("@MandateID", MandateID);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                return dt;

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(ex.Message);
                return null;
            }
        }
        public static bool CheckSMSAndEMail(string EntityId, string AppId, string Type)
        {
            bool status = false;
            try
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[Convert.ToString(AppId)].ConnectionString);
                string query = "Sp_WebAPI";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "CheckSMSAndEMail");
                cmd.Parameters.AddWithValue("@EntityId", EntityId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    if (Type == "E" && (Convert.ToString(dt.Rows[0]["NetValidateMail"]).ToUpper() == "TRUE" || Convert.ToString(dt.Rows[0]["DebitValidateMail"]).ToUpper() == "TRUE"))
                    {
                        status = true;
                    }
                    if (Type == "S" && (Convert.ToString(dt.Rows[0]["DebitSMS"]).ToUpper() == "TRUE" || Convert.ToString(dt.Rows[0]["NetSMS"]).ToUpper() == "TRUE"))
                    {
                        status = true;
                    }
                }
                return status;

            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(ex.Message);
                return status;
            }
        }
        public static DataSet GetData_MandateID(string MandateId, string AppID, string WebAppUrl, string Activity, string UserId)
        {
            DataSet dtset = new DataSet();
            try
            {
                string TempId = AppID + MandateId;
                TempId = SendEmailAndSMSGlobal.Global.ReverseString(TempId);
                TempId = SendEmailAndSMSGlobal.Global.CreateRandomCode(6) + TempId;
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[Convert.ToString(AppID)].ConnectionString);
                string query = "Sp_SendEmail";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "GetData_MandateID");
                cmd.Parameters.AddWithValue("@MandateId", MandateId);
                cmd.Parameters.AddWithValue("@Activity", Activity);
                cmd.Parameters.AddWithValue("@WebAppUrl", WebAppUrl);
                cmd.Parameters.AddWithValue("@EncodedMandateID", TempId);
                cmd.Parameters.AddWithValue("@UserId", UserId);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dtset);


            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("-----------------");
                Console.Out.WriteLine(ex.Message);
            }
            return dtset;
        }
    }
}