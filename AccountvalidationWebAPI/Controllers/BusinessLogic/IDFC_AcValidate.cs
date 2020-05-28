using AccountvalidationWebAPI.Models;
using BusinessLibrary;
using Encryptions;
using EntityDAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace AccountvalidationWebAPI.Controllers.BusinessLogic
{
    public static class IDFC_Acvalidate
    {
        public static ResAccountValidation IDFCAccountval(string ActivityId, string MandateId, string AcNo, string IFSC, string UserId, string AppId, string EntityId, List<pennyDetails> PendydropData)
        {
            string tempp = Dbsecurity.Encrypt("YKS");
           
            ResAccountValidation res = new ResAccountValidation();
            string webData = "";
            try
            {
                //processing.Style.Add("display", "none");
                long uni;
                uni = 0;
                string GMTformattedDateTime = DateTime.Now.ToString("ddMMyyyyHHmmss");
                string hash_string = string.Empty;
                string action1 = string.Empty;

                string TraceNumber = string.Empty;

                TraceNumber = GetTraceNoClass.getTraceNo(AppId); //Global.CreateRandomCode(6) + GMTformattedDateTime;

                string TempId = MandateId;
                TempId = Global.ReverseString(TempId);
                TempId = Global.CreateRandomCode(6) + TempId;
                string msgId, cnvId, txnRfNo = "";
                msgId = TempId.Substring(0, 6).ToUpper();
                cnvId = Global.ReverseString(TempId.Substring(0, 6)).ToUpper();


                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                List<InsertrequestModel> InsertrequestModellist = new List<InsertrequestModel>();
                List<InsertrequestNOModel> InsertrequestNOModelllist = new List<InsertrequestNOModel>();
                //var Result = dbcontext.MultipleResults("[dbo].[sp_Payment]").With<InsertrequestModel>().With<InsertrequestNOModel>().Execute("@QueryType", "@BeniACNo", "@BeniAcType", "@BeniAmount", "@BeniIFSC", "@ChkSum", "@UserId", "@EntityId", "@Filler1", "@Filler2", "@Filler3", "@Filler4",
                // "@Filler5", "@MandateId", "@MerchantId", "@MessageCode", "@Remarks", "@RequestDateTime", "@RequestType", "@TraceNo", "@ActivityId", "@msgId", "@cnvId", "@AppId", "InsertpaymentReq", AcNo, "10", "1.00", IFSC, Convert.ToString(uni), UserId, EntityId, "Yoeki Soft Pvt. Ltd", "9810462147", "", "", "", MandateId, Dbsecurity.Decypt(), "6210", "Payment", GMTformattedDateTime, "R", TraceNumber, ActivityId, msgId, cnvId, AppId);


                var Result = dbcontext.MultipleResults("[dbo].[sp_Payment]").With<InsertrequestModel>().With<InsertrequestNOModel>().Execute("@QueryType", "@BeniACNo", "@BeniAcType", "@BeniAmount", "@BeniIFSC", "@ChkSum", "@UserId" , "@EntityId", "@Filler1", "@Filler2", "@Filler3", "@Filler4",
             "@Filler5", "@MandateId", "@MerchantId", "@MessageCode", "@Remarks", "@RequestDateTime", "@RequestType", "@TraceNo", "@ActivityId", "@msgId", "@cnvId", "@AppId", "InsertpaymentReq", AcNo, "10", "1",IFSC, Convert.ToString(uni), UserId.ToString(),EntityId, "Yoeki Soft Pvt. Ltd",PendydropData[0].remMobNb.Replace("+91-", ""), "", "", "", MandateId,Dbsecurity.Decrypt(PendydropData[0].MERCHANT), "6211" , "Payment", GMTformattedDateTime, "R", TraceNumber,ActivityId, msgId, cnvId, AppId);


                //Edited by Bibhu on 25Jan2020 End

                //Added by Bibhu on 25Jan2020 Start
                InsertrequestModellist = Result[0].Cast<InsertrequestModel>().ToList();
                InsertrequestNOModelllist = Result[1].Cast<InsertrequestNOModel>().ToList();
                if (InsertrequestModellist != null && InsertrequestModellist.Count > 0)
                {
                    txnRfNo = Convert.ToString(InsertrequestNOModelllist[0].RequestNo);
                }
                //Added by Bibhu on 25Jan2020 End
                ReuestObject obj = new ReuestObject
                {
                    InitiateAuthIMPSFundTransferReq = new InitiateAuthIMPSFundTransferReq
                    {
                        msgHdr = new MsgHdr
                        {
                            msgId = msgId,
                            cnvId = cnvId,
                            extRefId = Convert.ToString(PendydropData[0].IDFCextRefId),//ConfigurationManager.AppSettings["IDFCextRefId"]),
                            appId =Dbsecurity.Decrypt(PendydropData[0].MERCHANT),//ConfigurationManager.AppSettings["IDFCAppId"]),
                            timestamp = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss", CultureInfo.CurrentCulture)
                        },
                        msgBdy = new MsgBdy
                        {
                            remNm = "",
                            cnsmrNm = Dbsecurity.Decrypt(PendydropData[0].MERCHANT),
                            //Convert.ToString(ConfigurationManager.AppSettings["IDFCAppId"]),
                            remMobNb = PendydropData[0].remMobNb,//Convert.ToString(ConfigurationManager.AppSettings["remMobNb"]),
                            dbtrAcctId = PendydropData[0].dbtrAcctId_idfc,//Convert.ToString(ConfigurationManager.AppSettings["dbtrAcctId"]),
                            acctTp = "10",
                            cdtrAcctId = AcNo,
                            finInstnId = IFSC,
                            amt = "1",
                            ccy = "INR",
                            pmtDesc = "Penny Drop",
                            txnRfNo =TraceNumber //txnRfNo
                        }
                    }
                };




                string inputJson = (new JavaScriptSerializer()).Serialize(obj);
                //  requestjson = inputJson.Replace("\\u003c", "<").Replace("\\u003e", ">");

                string postData = new JavaScriptSerializer().Serialize(obj);


                string host = PendydropData[0].URL;//Convert.ToString(ConfigurationManager.AppSettings["IMPSFundTransferURL"]);

                string certName = HttpContext.Current.Server.MapPath("../../../YoekiDSC.pfx");
                //  string password = Convert.ToString(ConfigurationManager.AppSettings["password"]);

                X509Certificate2 certificate = new X509Certificate2(certName, "Creative0786!@#");

                ServicePointManager.CheckCertificateRevocationList = false;
                ServicePointManager.ServerCertificateValidationCallback = (a, b, c, d) => true;
                ServicePointManager.Expect100Continue = true;
                HttpWebRequest req = (HttpWebRequest)WebRequest.Create(host);
                req.PreAuthenticate = true;
                req.AllowAutoRedirect = true;
                req.ClientCertificates.Add(certificate);
                req.Method = "POST";
                req.ContentType = "application/json";
                req.KeepAlive = true;
                //  string postData = "login-form-type=cert";
                byte[] postBytes = Encoding.UTF8.GetBytes(postData);
                req.ContentLength = postBytes.Length;

                Stream postStream = req.GetRequestStream();
                postStream.Write(postBytes, 0, postBytes.Length);
                postStream.Flush();
                postStream.Close();
                WebResponse resp = req.GetResponse();

                Stream stream = resp.GetResponseStream();

                string temp = "";
                using (StreamReader reader = new StreamReader(stream))
                {
                    string line = reader.ReadLine();
                    while (line != null)
                    {
                        temp = temp + line;

                        line = reader.ReadLine();
                    }
                }

                stream.Close();
                // Response.Write(temp);
               
                string Signedxml = temp;
                ResponseObject UserList = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseObject>(Signedxml);

                if (Result != null && Result.Count > 0)
                {
                    if (Result.Count > 2 && Result[0].Cast<InsertrequestModel>().ToList().Count > 0)
                    {
                    }
                    else
                    {
                        string msg = hash_string + '|' + Convert.ToString(uni); //hash_string = hash_string + '|' + Convert.ToString(uni);

                        string ActionUrl = action1 + msg;

                        //if (System.Configuration.ConfigurationManager.AppSettings["IsLocal"] == "1")
                        //{
                        //    webData = "6220|24082018120636|YK17|wIw4GP24082018122440|KPY00|Successful Transaction|823612654816|KMB0000037731||Fateh|1658580580";
                        //    //webData = "6220|24082018120636|YK17|wIw4GP24082018122440|KPY00|Successful Transaction|823612654816|KMB0000037731||Fateh|1658580580";
                        //}
                        //else
                        //{

                            if (UserList.ToString() != "")
                            {

                                if (Convert.ToString(UserList.initiateAuthIMPSFundTransferResp.msgHdr.rslt) == "OK")
                                {
                                    webData = "6221|" + GMTformattedDateTime + "|" +Dbsecurity.Decrypt( PendydropData[0].MERCHANT) + "|" + TraceNumber + "|" + UserList.initiateAuthIMPSFundTransferResp.msgBdy.respCd + "|Successful Transaction|" + UserList.initiateAuthIMPSFundTransferResp.msgBdy.txnId + "|||" + UserList.initiateAuthIMPSFundTransferResp.msgBdy.cdtrNm + "|";
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(UserList.initiateAuthIMPSFundTransferResp.msgBdy.txnId))
                                    {
                                        webData = "6221|" + GMTformattedDateTime + "|" + Dbsecurity.Decrypt(PendydropData[0].MERCHANT) + "|" + TraceNumber + "|" + UserList.initiateAuthIMPSFundTransferResp.msgHdr.error.cd + "|" + UserList.initiateAuthIMPSFundTransferResp.msgHdr.error.rsn + "|" + UserList.initiateAuthIMPSFundTransferResp.msgBdy.txnId + "|||" + UserList.initiateAuthIMPSFundTransferResp.msgBdy.cdtrNm + "|";
                                    }
                                    else
                                    {
                                        webData = "6221|" + GMTformattedDateTime + "|" + Dbsecurity.Decrypt(PendydropData[0].MERCHANT) + "|" + TraceNumber + "|" + UserList.initiateAuthIMPSFundTransferResp.msgHdr.error.cd + "|" + UserList.initiateAuthIMPSFundTransferResp.msgHdr.error.rsn + "||||" + UserList.initiateAuthIMPSFundTransferResp.msgBdy.cdtrNm + "|";
                                    }
                                }
                            }
                       // }


                        //string webData = wc.DownloadString(ActionUrl);


                        string[] Data = webData.Split('|');

                        dbcontext = new QuickCheck_AngularEntities();
                        var ResResult = dbcontext.MultipleResults("[dbo].[sp_Payment]").With<ResDescription>().With<ResCust1>().With<ResIfsc>().With<Resshow>().Execute("@QueryType", "@BankRefNo", "@BeniName", "@ChkSum", "@UserId", "@EntityId", "@ErrorReason", "@MandateId", "@MerchantId", "@MessageCode", "@RRN", "@RequestDateTime", "@ResponseCode", "@TraceNo", "@IFSC", "@ActivityId", "@AppId", "InsertpaymentResIDFC", Data[7], Data[9], Data[10], UserId.ToString(), EntityId, Data[5], MandateId, Data[2], Data[0], Data[6], Data[1], Data[4], Data[3], Convert.ToString(IFSC), ActivityId, AppId);
                        res.ResDescriptionlist = ResResult[0].Cast<ResDescription>().ToList();
                        res.ResCust1list = ResResult[1].Cast<ResCust1>().ToList();
                        res.ResIfsclist = ResResult[2].Cast<ResIfsc>().ToList();
                        res.Resshowlist = ResResult[3].Cast<Resshow>().ToList();

                    }
                }

            }

            catch (Exception ex)
            {
            }
            return res;
        }

    }
}