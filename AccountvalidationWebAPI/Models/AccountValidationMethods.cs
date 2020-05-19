using BusinessLibrary;
using Encryptions;
using EntityDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace AccountvalidationWebAPI.Models
{
    public static class AccountValidationMethods
    {
       
        public static LoginResponse AckPaymentTestNew(string ActivityId, string MandateId, string AcNo, string IFSC, string UserId,  string AppId,string EntityId)
        {
            string hash1 = string.Empty;
            QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
            LoginResponse res = new LoginResponse();
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[Convert.ToString(AppId)].ConnectionString);
            string Status = "";
            try
            { 

                long uni;
                uni = 0;

                string GMTformattedDateTime = DateTime.Now.ToString("ddMMyyyyHHmmss");

                string hash_string = string.Empty;
                string action1 = string.Empty;



                string TraceNumber = string.Empty;
                TraceNumber = Global.CreateRandomCode(6) + GMTformattedDateTime;
                //if (string.IsNullOrEmpty(Request.Form["hash"])) // generating hash value
                //{

                hash_string = "6210";//Message Code
                hash_string = hash_string + '|';

                hash_string = hash_string + GMTformattedDateTime;//	Date and Time  in GMT
                hash_string = hash_string + '|';

                hash_string = hash_string + Dbsecurity.Decypt(ConfigurationManager.AppSettings["MERCHANT_KEY"]);//Merchant Id
                hash_string = hash_string + '|';

                hash_string = hash_string + TraceNumber;//Trace number
                hash_string = hash_string + '|';

                hash_string = hash_string + "R";//REQUEST_TYPE
                hash_string = hash_string + '|';

                hash_string = hash_string + IFSC;//BENE_IFSC
                hash_string = hash_string + '|';

                hash_string = hash_string + AcNo;//BENE_ACCNT_NUMBER
                hash_string = hash_string + '|';

                hash_string = hash_string + "1.00";//AMOUNT
                hash_string = hash_string + '|';

                hash_string = hash_string + "Payment";//REMARKS
                hash_string = hash_string + '|';

                hash_string = hash_string + "10";//BENE_ACCOUNT_TYPE
                hash_string = hash_string + '|';

                hash_string = hash_string + "Yoeki Soft Pvt. Ltd";//Filler 1
                hash_string = hash_string + '|';

                hash_string = hash_string + "9810462147";//Filler 2
                hash_string = hash_string + '|';

                hash_string = hash_string + "";//Filler 3
                hash_string = hash_string + '|';

                hash_string = hash_string + "";
                hash_string = hash_string + '|';//Filler 4

                hash_string = hash_string + "";//Filler 5

                action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"];

                //}

                hash1 = hash_string + '|' + Dbsecurity.Decypt(ConfigurationManager.AppSettings["CheckSum"], ConfigurationManager.AppSettings["CheckSum_Key"]);

                byte[] Message = Encoding.UTF8.GetBytes(hash1);

                uni = DamienG.Security.Cryptography.Crc32.Compute(Message);//Checksum


                string query = "sp_Payment";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@QueryType", "InsertpaymentReq");
                cmd.Parameters.AddWithValue("@BeniACNo", AcNo);
                cmd.Parameters.AddWithValue("@BeniAcType", "10");
                cmd.Parameters.AddWithValue("@BeniAmount", "1.00");
                cmd.Parameters.AddWithValue("@BeniIFSC", IFSC);
                cmd.Parameters.AddWithValue("@ChkSum", Convert.ToString(uni));
                cmd.Parameters.AddWithValue("@UserId", UserId);

                cmd.Parameters.AddWithValue("@EntityId", 0);
                cmd.Parameters.AddWithValue("@Filler1", "Yoeki Soft Pvt. Ltd");
                cmd.Parameters.AddWithValue("@Filler2", "9810462147");
                cmd.Parameters.AddWithValue("@Filler3", "");
                cmd.Parameters.AddWithValue("@Filler4", "");

                cmd.Parameters.AddWithValue("@Filler5", "");
                cmd.Parameters.AddWithValue("@MandateId", MandateId);
                cmd.Parameters.AddWithValue("@MerchantId", Dbsecurity.Decypt(ConfigurationManager.AppSettings["MERCHANT"], ConfigurationManager.AppSettings["MERCHANT_KEY"]));
                cmd.Parameters.AddWithValue("@MessageCode", "6210");
                cmd.Parameters.AddWithValue("@Remarks", "Payment");

                cmd.Parameters.AddWithValue("@RequestDateTime", GMTformattedDateTime);
                cmd.Parameters.AddWithValue("@RequestType", "R");
                cmd.Parameters.AddWithValue("@TraceNo", TraceNumber);
                cmd.Parameters.AddWithValue("@ActivityId", ActivityId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                List<InsertrequestModel> InsertrequestModellist = new List<InsertrequestModel>();
                List<InsertrequestNOModel> InsertrequestNOModelllist = new List<InsertrequestNOModel>();

                var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<InsertrequestModel>().With<InsertrequestNOModel>().Execute("@QueryType", "@BeniACNo", "@BeniAcType", "@BeniAmount", "@BeniIFSC", "@ChkSum", "@UserId", "@EntityId", "@Filler1", "@Filler2", "@Filler3", "@Filler4",
               "@Filler5", "@MandateId", "@MerchantId", "@MessageCode", "@Remarks", "@RequestDateTime", "@RequestType", "@TraceNo", "@ActivityId", "InsertpaymentReq",AcNo,"10","1.00",IFSC, Convert.ToString(uni),UserId,EntityId, "Yoeki Soft Pvt. Ltd", "9810462147","","","",MandateId, Dbsecurity.Decypt(ConfigurationManager.AppSettings["MERCHANT"], ConfigurationManager.AppSettings["MERCHANT_KEY"]),"6210", "Payment", "GMTformattedDateTime","R", TraceNumber, ActivityId);

                //Result.FirstOrDefault().Cast<IFSCResultModal>().ToList();
                InsertrequestModellist = Result[0].Cast<InsertrequestModel>().ToList();
                InsertrequestNOModelllist = Result[0].Cast<InsertrequestNOModel>().ToList();
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //lblMandateName.Text = dt.Rows[0][0].ToString();
                string msg = hash_string + '|' + Convert.ToString(uni); //hash_string = hash_string + '|' + Convert.ToString(uni);

                string ActionUrl = action1 + msg;

                System.Net.ServicePointManager.ServerCertificateValidationCallback += delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslPolicyErrors)
                {
                    return true; // **** Always accept
                };
                string webData = "";
                System.Net.WebClient wc = new System.Net.WebClient();
                if (Convert.ToString(ConfigurationManager.AppSettings["IsLocal"]) == "1")

                {
                    // webData = "6220|24082018120636|YK17|wIw4GP24082018122440|KPY00|Successful Transaction|823612654816|KMB0000037731||Fateh singh|1658580580";
                    webData = "6220|24082018120636|YK17|wIw4GP24082018122440|KPYM3|Invalid Beneficiary details, Inform customer|823612654816|KMB0000037731||Fateh singh|1658580580";
                }
                else
                {
                    webData = wc.DownloadString(ActionUrl);
                }
                string[] Data = webData.Split('|');



                string query1 = "sp_Payment";

                SqlCommand cmd1 = new SqlCommand(query1, con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@QueryType", "InsertpaymentResMobile");
                cmd1.Parameters.AddWithValue("@BankRefNo", Data[7]);
                cmd1.Parameters.AddWithValue("@BeniName", Data[9]);
                cmd1.Parameters.AddWithValue("@ChkSum", Data[10]);
                cmd1.Parameters.AddWithValue("@UserId", UserId);
                cmd1.Parameters.AddWithValue("@EntityId", 0);
                cmd1.Parameters.AddWithValue("@ErrorReason", Data[5]);
                cmd1.Parameters.AddWithValue("@MandateId", MandateId);
                cmd1.Parameters.AddWithValue("@MerchantId", Data[2]);
                cmd1.Parameters.AddWithValue("@MessageCode", Data[0]);
                cmd1.Parameters.AddWithValue("@RRN", Data[6]);
                cmd1.Parameters.AddWithValue("@RequestDateTime", Data[1]);
                cmd1.Parameters.AddWithValue("@ResponseCode", Data[4]);
                cmd1.Parameters.AddWithValue("@TraceNo", Data[3]);
                cmd1.Parameters.AddWithValue("@ActivityId", ActivityId);
                cmd1.Parameters.AddWithValue("@IFSC", IFSC);

                

                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                if (ds1.Tables[0] != null && ds1.Tables[0].Rows.Count > 0)
                {
                    Status = Convert.ToString(ds1.Tables[2].Rows[0]["Status"]);
                    Status = Status + "_" + Convert.ToString(ds1.Tables[2].Rows[0]["StatusDescription"]);
                    if (ds1.Tables[0].Rows[0]["Status"].ToString() == "Success")
                    {
                        if (ds1.Tables[1] != null && ds1.Tables[1].Rows.Count > 0)
                        {
                           // GenerateGrid(Id, UserId, AppID);
                            res.Status = "Success";
                            res.ResCode = "ykR20039"; //Convert.ToString(ds1.Tables[0].Rows[0]["ErrorCode"]);
                            res.Message = "Data saved successfully";//Convert.ToString(ds1.Tables[0].Rows[0]["Description"]);
                            res.AccountHolderName = Data[9];
                            // res.userId = Id;
                            res.MdtID = MandateId;
                            //  res.IsliveInNach = Convert.ToString(ds1.Tables[3].Rows[0]["IsNachLive"]);
                        }
                        else
                        {
                            res.Status = "Success";
                            res.Message = "Account validated but name mismatch";//"Name Missmatch";
                            res.ResCode = "ykR20012";// Convert.ToString(ds1.Tables[0].Rows[0]["ErrorCode"]);

                            res.AccountHolderName = Data[9];
                            // res.userId = Convert.ToString(ds1.Tables[2].Rows[0]["customerlength"]);
                            //   res.IsliveInNach = Convert.ToString(ds1.Tables[3].Rows[0]["IsNachLive"]);
                            res.MdtID = MandateId;

                        }
                    }
                    else
                    {
                        string[] printer = { "KPY65", "KPY86", "KPY92", "KPY92", "KPYM57", "KPYERR06", "KPYERR07", "KPYERR08", "KPYERR09", "KPYERR10", "KPYM3" };
                        string[] printerr = { "KPY20", "KPY06", "KPY08", "KPY12", "KPY13", "KPY18", "KPY51", "KPY90", "KPY94", "KPY96", "KPYM0", "KPYM6", "KPYM7", "KPYM8", "KPYM9", "KPYMA", "KPYMF", "KPYMH", "KPYMV", "KPYMZ", "KPYPK", "KPYPR", "KPYWC", "KPY56", "KPYMM1", "KPYN0", "KPYNC", "KPYRM1", "KPYERR01", "KPYERR02", "KPYERR03", "KPYERR04", "KPYERR05", "KPYERR11", "KPYERR12", "KPYERR13", "KPYERR14", "KPYERR15", "KPYERR16", "KPYERR17", "KPYERR18" };
                        if (Convert.ToString(ds1.Tables[0].Rows[0]["ErrorCode"]) == "KPYM1" || Convert.ToString(ds1.Tables[0].Rows[0]["ErrorCode"]) == "KPYMM" || Convert.ToString(ds1.Tables[0].Rows[0]["ErrorCode"]) == "KPYM2")
                        {
                            res.Status = "Failure";
                            res.ResCode = "ykR20008";
                            res.Message = "Invalid A/c Number";
                            res.MdtID = MandateId;
                        }
                        else if (Convert.ToString(ds1.Tables[0].Rows[0]["ErrorCode"]) == "KPYM4" || Convert.ToString(ds1.Tables[0].Rows[0]["ErrorCode"]) == "KPYR57" || Convert.ToString(ds1.Tables[0].Rows[0]["ErrorCode"]) == "KPYMM4")
                        {
                            res.Status = "Failure";
                            res.ResCode = "ykR20009";
                            res.Message = "Invalid Account (NRE Account)";
                            res.MdtID = MandateId;
                        }


                        else if (printer.Contains(Convert.ToString(ds1.Tables[0].Rows[0]["ErrorCode"])))
                        {
                            res.Status = "Failure";
                            res.ResCode = "ykR20007";
                            res.Message = "Account invalid/Inactive/Blocked/Frozen (Transaction Not permitted in this a/c)";
                            res.MdtID = MandateId;
                        }


                        else if (printerr.Contains(Convert.ToString(ds1.Tables[0].Rows[0]["ErrorCode"])))
                        {
                            res.Status = "Failure";
                            res.ResCode = "ykR20019";
                            res.Message = "Technical Issue, Retry after some time";
                            res.MdtID = MandateId;
                        }
                        else if (Convert.ToString(ds1.Tables[0].Rows[0]["ErrorCode"]) == "ERR00")
                        {
                            res.Status = "Failure";
                            res.ResCode = "ykR20018";
                            res.Message = "Bank Service Down, Please try after some time";
                            res.MdtID = MandateId;
                        }
                        else
                        {
                            res.Status = "Failure";
                            res.ResCode = "ykR20018";
                            res.Message = "Bank Service Down, Please try after some time";
                            res.MdtID = MandateId;
                        }
                        //if (Convert.ToBoolean(ds1.Tables[3].Rows[0]["IsIMPSLive"]) == false)
                        //{
                        //    //res.IsliveInNach = "";
                        //    res.Status = "Failure";
                        //    res.ResCode = Convert.ToString(ds1.Tables[0].Rows[0]["ErrorCode"]);
                        //    res.Message = Convert.ToString(ds1.Tables[0].Rows[0]["Description"]);
                        //    // res.userId = Id;
                        //    res.MdtID = Id;
                        //}
                        //else
                        //{
                        //    // res.IsliveInNach = "";
                        //    res.Status = "Failure";
                        //    res.ResCode = Convert.ToString(ds1.Tables[0].Rows[0]["ErrorCode"]);
                        //    res.Message = Convert.ToString(ds1.Tables[0].Rows[0]["Description"]);
                        //    //res.userId = Id;
                        //    res.MdtID = Id;



                        //}
                    }
                }


                else
                {
                    res.Status = "Failure";
                    res.ResCode = "ykR20018";
                    res.Message = "Bank Service Down, Please try after some time";
                    res.MdtID = MandateId;

                }

            }

            catch (Exception ex)
            {
                // res.IsliveInNach = "";
                res.Status = "Failure";
                res.Message = ex.Message;
                // res.userId = "";

            }
            return res;
        }
    }
}