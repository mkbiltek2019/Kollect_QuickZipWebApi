using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Net.Http;
using BusinessLibrary;
using EntityDAL;
using Encryptions;

namespace SaveEditMandateAPI.Models.MandateDetails
{
    public class MandateDetailsData
    {
        QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
        string GMTformattedDateTime = Convert.ToString(DateTime.Now.ToString("ddMMyyyyHHmmssfff"));
        string MsgId = EntityDAL.Global.CreateRandomCode(6);
        string MerchantKey = EntityDAL.Global.CreateRandomCode(6);
        string GMTformattedDateTime1 = DateTime.Now.ToString("yyyyMMddTHHmmssfff", CultureInfo.CurrentCulture);

        string TempMandateID = "";


        public Dictionary<string, object> GetMandateDetails(MandateHeader data)
        {
            try
            { 
           
                

            TempMandateID = Convert.ToString(data.MandateId);
            TempMandateID = TempMandateID.Substring(6, TempMandateID.Length - 6);
            TempMandateID = EntityDAL.Global.ReverseString(TempMandateID);
            TempMandateID = TempMandateID.Substring(6, TempMandateID.Length - 6);


            var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[sp_MandateDetails]").With<MandateDetailList>().With<MandateDetailList1>().Execute("@QueryType", "@MandateId", "@Link",  "@AppId", "MandateDetails", TempMandateID, data.URL,  data.AppId));

                Result.Add("TempMandateID", TempMandateID);
                return Result;

            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        public Dictionary<string, object> btnMandate_Click(MandateHeader data)
        {
            try
            {

                var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[sp_MandateDetails]").Execute("@QueryType", "@MandateId", "@EMandatetype", "@AppId", "UpdateEmandateType", Dbsecurity.Decypt(data.MandateId), data.Authmode, Dbsecurity.Decypt(data.AppId)));
                Result.Add("result", "1");
                return Result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }

        public gridlist GetEmandateData(MandateHeader data)
        {
            try
            {
                MsgId = Dbsecurity.Decypt(data.AppId) + MsgId + GMTformattedDateTime + RandomDigits(4);
                gridlist res = new gridlist();
                List<MandateDetailList> list = new List<MandateDetailList>();
               
                var Result = dbcontext.MultipleResults("[dbo].[sp_ENach]").With<MandateDetailList>().Execute("@QueryType", "@MandateId", "@EMandatetype", "@MsgId"
         , "@Link", "@AppId", "Emandate", Dbsecurity.Decypt(data.MandateId),data.Authmode,MsgId, data.URL,Dbsecurity.Decypt(data.AppId));
                list = Result[0].Cast<MandateDetailList>().ToList();



                for (var i = 0; i < list.Count; i++)
                {
                    list[i].CustAcNo = Dbsecurity.Decypt(Convert.ToString(list[i].CustAcNo));
                    list[i].AmountRupees = Dbsecurity.Decypt(Convert.ToString(list[i].AmountRupees));
                }

                if (list.Count > 0)
                {
                    MerchantKey = MerchantKey + GMTformattedDateTime1;
                    string dDateTime = DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.CurrentCulture);
                    string xml = "";
                    xml = @"<mdata><FirstCollectionDate>" + list[0].FirstCollectionDate.ToString() + "</FirstCollectionDate><FinalCollectionDate>" + list[0].FinalCollectionDate.ToString() + "</FinalCollectionDate> <CollectionAmount>" + list[0].AmountRupees.ToString() + "</CollectionAmount><DebitType>" + list[0].DebitType.ToString()
                        + "</DebitType><EntityName>" + list[0].EntityName.ToString() + "</EntityName><YoekiNachCode>" + list[0].YoekiNachCode.ToString() + "</YoekiNachCode><EntityIFSC>" + list[0].EntityIFSC.ToString() + "</EntityIFSC><EntityBank>" 
                        + list[0].EntityBankName.ToString() + "</EntityBank><CustIFSC>" + list[0].CustIFSC.ToString() + "</CustIFSC><CustBank>" + list[0].customerBankname.ToString() + "</CustBank><CustAcNo>" + list[0].CustAcNo.ToString() + "</CustAcNo><EntityAcNo>" + 
                        list[0].EntityAcNo.ToString() + "</EntityAcNo><CustName>" + list[0].CustName.ToString() + "</CustName><Refrence1>" + list[0].Refrence1.ToString() + "</Refrence1><PhoneNo>" + list[0].PhoneNo.ToString() + "</PhoneNo><emailId>" + list[0].EmailId.ToString() 
                        + "</emailId><ToDebit>" + list[0].ToDebit.ToString() + "</ToDebit><BankID>" +list[0].BankID.ToString() + "</BankID><MsgId>" + MsgId + "</MsgId><CategoryName>" + list[0].CategoryName.ToString() + "</CategoryName><CategoryCode>" 
                        + list[0].CategoryCode.ToString() + "</CategoryCode><Frequency>" + list[0].Frequency.ToString() + "</Frequency><EMandatetype>" + list[0].EMandatetype.ToString() + "</EMandatetype><BankCode>" + list[0].BankCode.ToString() + "</BankCode>  </mdata>";

                    string ASPId_Key = ConfigurationManager.AppSettings["ASPId_Key"];
                    string ConsentURL = "";
                    ConsentURL = ConfigurationManager.AppSettings["EMandateURL"];
                    var url = ConsentURL + "Consent?UserId=" + data.UserId + "&EntityId=" + Dbsecurity.Encrypt(data.EntityId) + "&AppId=" + Dbsecurity.Encrypt(data.AppId) + "&MandateId=" + Dbsecurity.Encrypt(data.MandateId) + "&Same=B&AuthType=" + data + "";
                    NameValueCollection collections = new NameValueCollection();
                    collections.Add("Request", xml);
                    string remoteUrl = url;
                    string html = "<html><head><style>#preloader_1 label{clear: both;margin-bottom: 28%!important;font-weight: bold;}#preloader_1{ position: relative;margin: 20% 0 10% 45%;}#preloader_1 span{display:block;bottom: -15px;width: 9px;height: 5px; background:#9b59b6;position:absolute;animation: preloader_1 1.5s  infinite ease-in-out;}#preloader_1 span:nth-child(2){left:11px;animation-delay: .2s;}" +
                        "#preloader_1 span:nth-child(3){left:22px;animation-delay: .4s;}" +
                        "#preloader_1 span:nth-child(4){left:33px;animation-delay: .6s;}#preloader_1 span:nth-child(5){left:44px;animation-delay: .8s;}@keyframes preloader_1 {0% {height:5px;transform:translateY(0px);background:#9b59b6;}25% {height:30px;transform:translateY(15px);background:#3498db;}50% {height:5px;transform:translateY(0px);background:#9b59b6;}" +
                        "100% {height:5px;transform:translateY(0px);background:#9b59b6;}}</style>";
                    html += "</head><body onload='document.forms[0].submit()'>";
                    // html += string.Format("<form name='PostForm' method='POST' action='{0}' target='_blank'>", remoteUrl);
                    html += string.Format("<form name='PostForm' method='POST' action='{0}'>", remoteUrl);
                    foreach (string key in collections.Keys)
                    {
                        html += string.Format("<input name='{0}' type='text' value='{1}'>", key, collections[key]);
                    }
                    html += "<div id='overlay' style='background: #fff;overflow: auto;display: block;position: absolute;width: 98%;top: 0;height: 90%;z-index: 999;' class='col-xs-12 col-md-12 col-sm-12 overlay'></div><div id='preloader_1'  style='display: block; z-index: 999999; position: relative;' class='preloader'><span></span><span></span><span></span><span></span><span></span><label style='margin-bottom:5%'>Loading...</label></div>";

                    html += "</form></body></html>";
                    //Response.Clear();
                    //Response.ContentEncoding = Encoding.GetEncoding("ISO-8859-1");
                    //Response.HeaderEncoding = Encoding.GetEncoding("ISO-8859-1");
                    //Response.Charset = "ISO-8859-1";
                    //Response.Write(html);
                    //Response.End();
                }

                res.Gridlist = list;

                return res;
            }


            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }
    }
}