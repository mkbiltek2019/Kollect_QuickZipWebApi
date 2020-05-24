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

        public static ResAccountValidation AckPaymentTestNew(string ActivityId, string MandateId, string AcNo, string IFSC, string UserId, string AppId, string EntityId)
        {
            string temp = Dbsecurity.Encrypt("YK17");
            string temp1 = Dbsecurity.Encrypt("KMBANK");
            string hash1 = string.Empty;
            QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
            ResAccountValidation res = new ResAccountValidation();
            //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings[Convert.ToString(AppId)].ConnectionString);
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

                hash_string = hash_string + Dbsecurity.Decypt(ConfigurationManager.AppSettings["MERCHANT"]);//Merchant Id
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

                hash1 = hash_string + '|' + Dbsecurity.Decypt(ConfigurationManager.AppSettings["CheckSum"]);

                byte[] Message = Encoding.UTF8.GetBytes(hash1);

                uni = DamienG.Security.Cryptography.Crc32.Compute(Message);//Checksum


                //string query = "sp_Payment";

                //SqlCommand cmd = new SqlCommand(query, con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@QueryType", "InsertpaymentReq");
                //cmd.Parameters.AddWithValue("@BeniACNo", AcNo);
                //cmd.Parameters.AddWithValue("@BeniAcType", "10");
                //cmd.Parameters.AddWithValue("@BeniAmount", "1.00");
                //cmd.Parameters.AddWithValue("@BeniIFSC", IFSC);
                //cmd.Parameters.AddWithValue("@ChkSum", Convert.ToString(uni));
                //cmd.Parameters.AddWithValue("@UserId", UserId);

                //cmd.Parameters.AddWithValue("@EntityId", 0);
                //cmd.Parameters.AddWithValue("@Filler1", "Yoeki Soft Pvt. Ltd");
                //cmd.Parameters.AddWithValue("@Filler2", "9810462147");
                //cmd.Parameters.AddWithValue("@Filler3", "");
                //cmd.Parameters.AddWithValue("@Filler4", "");

                //cmd.Parameters.AddWithValue("@Filler5", "");
                //cmd.Parameters.AddWithValue("@MandateId", MandateId);
                //cmd.Parameters.AddWithValue("@MerchantId", Dbsecurity.Decypt(ConfigurationManager.AppSettings["MERCHANT"], ConfigurationManager.AppSettings["MERCHANT_KEY"]));
                //cmd.Parameters.AddWithValue("@MessageCode", "6210");
                //cmd.Parameters.AddWithValue("@Remarks", "Payment");

                //cmd.Parameters.AddWithValue("@RequestDateTime", GMTformattedDateTime);
                //cmd.Parameters.AddWithValue("@RequestType", "R");
                //cmd.Parameters.AddWithValue("@TraceNo", TraceNumber);
                //cmd.Parameters.AddWithValue("@ActivityId", ActivityId);

                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                //DataTable dt = new DataTable();
                //da.Fill(dt);

                List<InsertrequestModel> InsertrequestModellist = new List<InsertrequestModel>();
                List<InsertrequestNOModel> InsertrequestNOModelllist = new List<InsertrequestNOModel>();

                var Result = dbcontext.MultipleResults("[dbo].[sp_Payment]").With<InsertrequestModel>().With<InsertrequestNOModel>().Execute("@QueryType", "@BeniACNo", "@BeniAcType", "@BeniAmount", "@BeniIFSC", "@ChkSum", "@UserId", "@EntityId", "@Filler1", "@Filler2", "@Filler3", "@Filler4",
               "@Filler5", "@MandateId", "@MerchantId", "@MessageCode", "@Remarks", "@RequestDateTime", "@RequestType", "@TraceNo", "@ActivityId", "InsertpaymentReq", AcNo, "10", "1.00", IFSC, Convert.ToString(uni), UserId, EntityId, "Yoeki Soft Pvt. Ltd", "9810462147", "", "", "", MandateId, Dbsecurity.Decypt(ConfigurationManager.AppSettings["MERCHANT"]), "6210", "Payment", GMTformattedDateTime, "R", TraceNumber, ActivityId);

                if (Result.Count > 2)
                {
                    //i have to code here
                }
                else
                {
                    //Result.FirstOrDefault().Cast<IFSCResultModal>().ToList();
                    InsertrequestModellist = Result[0].Cast<InsertrequestModel>().ToList();
                    InsertrequestNOModelllist = Result[1].Cast<InsertrequestNOModel>().ToList();
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

                    // string query1 = "sp_Payment";

                    //SqlCommand cmd1 = new SqlCommand(query1, con);
                    //cmd1.CommandType = CommandType.StoredProcedure;
                    //cmd1.Parameters.AddWithValue("@QueryType", "InsertpaymentResMobile");
                    //cmd1.Parameters.AddWithValue("@BankRefNo", Data[7]);
                    //cmd1.Parameters.AddWithValue("@BeniName", Data[9]);
                    //cmd1.Parameters.AddWithValue("@ChkSum", Data[10]);
                    //cmd1.Parameters.AddWithValue("@UserId", UserId);
                    //cmd1.Parameters.AddWithValue("@EntityId", 0);
                    //cmd1.Parameters.AddWithValue("@ErrorReason", Data[5]);
                    //cmd1.Parameters.AddWithValue("@MandateId", MandateId);
                    //cmd1.Parameters.AddWithValue("@MerchantId", Data[2]);
                    //cmd1.Parameters.AddWithValue("@MessageCode", Data[0]);
                    //cmd1.Parameters.AddWithValue("@RRN", Data[6]);
                    //cmd1.Parameters.AddWithValue("@RequestDateTime", Data[1]);
                    //cmd1.Parameters.AddWithValue("@ResponseCode", Data[4]);
                    //cmd1.Parameters.AddWithValue("@TraceNo", Data[3]);
                    //cmd1.Parameters.AddWithValue("@ActivityId", ActivityId);
                    //cmd1.Parameters.AddWithValue("@IFSC", IFSC);
                    //SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                    //DataSet ds1 = new DataSet();
                    //da1.Fill(ds1);

                    dbcontext = new QuickCheck_AngularEntities();
                    var ResResult = dbcontext.MultipleResults("[dbo].[sp_Payment]").With<ResDescription>().With<ResCust1>().With<ResIfsc>().With<Resshow>().Execute("@QueryType", "@BankRefNo", "@BeniName", "@ChkSum", "@UserId", "@EntityId", "@ErrorReason", "@MandateId", "@MerchantId", "@MessageCode", "@RRN", "@RequestDateTime", "@ResponseCode", "@TraceNo", "@IFSC", "@ActivityId", "InsertpaymentRes", Data[7], Data[9], Data[10], UserId.ToString(), EntityId, Data[5], MandateId, Data[2], Data[0], Data[6], Data[1], Data[4], Data[3], Convert.ToString(IFSC), ActivityId);
                    res.ResDescriptionlist = ResResult[0].Cast<ResDescription>().ToList();
                    res.ResCust1list = ResResult[1].Cast<ResCust1>().ToList();
                    res.ResIfsclist = ResResult[2].Cast<ResIfsc>().ToList();
                    res.Resshowlist = ResResult[3].Cast<Resshow>().ToList();

                }

                
            }
            catch (Exception ex)
            {
                // res.IsliveInNach = "";
              //  res.Status = "Failure";
               // res.Message = ex.Message;
                // res.userId = "";

            } 
            return res;
        }
    }
}