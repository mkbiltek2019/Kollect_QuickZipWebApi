using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data;
using System.Xml.Linq;
using System.Web.UI.WebControls;

using EntityDAL;
using Encryptions;
using BusinessLibrary;
using Ionic.Zip;
using System.IO.Compression;
using System.IO;
using System.Reflection;
using System.Configuration;

namespace DownloadmandateApi.Models
{
    public class DownloadMandate
    {
        QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
        //List<CommonFlag> common = new List<CommonFlag>();
        //CommonFlag Flag = new CommonFlag();
        List<DownloadMandateDetails> dataList = new List<DownloadMandateDetails>();
        List<DownloadMandateGridDetails> dataListG = new List<DownloadMandateGridDetails>();
        List<ExcelGrid> dataListEG = new List<ExcelGrid>();
        List<ExcelGrid1> dataListEG1 = new List<ExcelGrid1>();//ZipGrid
        List<ZipGrid> dataListZP = new List<ZipGrid>();
        public IEnumerable<DownloadMandateDetails> Binddropdownbank(string userId)
        {
            // List<DownloadMandateDetails> dataList = new List<DownloadMandateDetails>();
            try//Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(userId.Replace("_", "%")))
            {
                 var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateDetails>().Execute("@QueryType", "@UserId", "UserBank", Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(userId.Replace("_", "%"))));

                foreach (var employe in Result)
                {
                    dataList = employe.Cast<DownloadMandateDetails>().ToList();
                }
                return dataList;

                // return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public IEnumerable<DownloadMandateGridDetails> BindGrid(Bindgrid griddata)
        {
            try
            {


                // var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateDetails>().Execute("@QueryType", "@UserId", "@strToDate", "@strFromDate", "@SponsorBankCode", "grdMandateRefrenceWise", userId, todate, fromdate, sponsorbankcode));
                var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateGridDetails>().Execute("@QueryType", "@UserId", "@strToDate", "@strFromDate", "@SponsorBankCode", "grdMandateDateWise", Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(griddata.UserId.Replace("_", "%"))), griddata.strToDate, griddata.strFromDate, griddata.SponsorBankCode);

                foreach (var bgrid in Result)
                {
                    dataListG = bgrid.Cast<DownloadMandateGridDetails>().ToList();

                }
                return dataListG;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public IEnumerable<DownloadMandateGridDetails> BindGridRef(string userId, string refNo)
        {
            try
            {
                // var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateDetails>().Execute("@QueryType", "@UserId", "@strToDate", "@strFromDate", "@SponsorBankCode", "grdMandateRefrenceWise", userId, todate, fromdate, sponsorbankcode));
                var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateGridDetails>().Execute("@QueryType", "@UserId", "@Refrence1", "grdMandateRefrenceWise", Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(userId.Replace("_", "%"))), refNo);

                foreach (var bgrid in Result)
                {
                    dataListG = bgrid.Cast<DownloadMandateGridDetails>().ToList();

                }
                return dataListG;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<string, object> RejectMandate(RejectMandateModel data)
        {
            string[] fromdate = data.strFromDate.Split('T');
            string[] todate = data.strToDate.Split('T');

            string[] mandatearr = data.strTable;



            XDocument doc = new XDocument();
            doc.Add(new XElement("dtXml", mandatearr.Select(x => new XElement("MandateId", x))));


            DataTable dt = new DataTable();
            dt.Columns.Add("MandateId", typeof(Int64));
            // Boolean IsFound = false;

            for (int i = 0; i < data.strTable.Length; i++)
            {
                DataRow dr = dt.NewRow();

                // dr = IsMandateID;
                dt.Rows.Add(data.strTable[i]);

                // dt.Rows.Add(dr);
                //IsFound = true;
                // }
                //}



            }
            
            string strTable = GetXmlByDatable(dt);

            try
            {
                var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateGridDetails>().Execute("@QueryType", "@UserId", "@strToDate", "@strFromDate", "@strTable", "@RejectedReason", "RejectdataDateWise", Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(data.UserId.Replace("_", "%"))), todate[0], fromdate[0], strTable,data.RejectedReason));
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public Dictionary<string, object> submitted(string userID, string selectMandateId)
        {
            string[] mandatearr = selectMandateId.Split(',');

            XDocument doc = new XDocument();
            doc.Add(new XElement("dtXml", mandatearr.Select(x => new XElement("MandateId", x))));


            DataTable dt = new DataTable();
            dt.Columns.Add("MandateId", typeof(Int64));
            // Boolean IsFound = false;

            for (int i = 0; i < mandatearr.Length; i++)
            {
                DataRow dr = dt.NewRow();
                dt.Rows.Add(mandatearr[i]);

            }
            string strTable = GetXmlByDatable(dt);

            try
            {
                var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateGridDetails>().Execute("@QueryType", "@UserId", "@strTable", "UpdateSubmitted", Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(userID.Replace("_", "%"))), strTable));
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //ExcelDownload exceldata

        //public Dictionary<string, object> ExcelDownload(Exceldata data)
        //{

        //    XDocument doc = new XDocument();
        //    doc.Add(new XElement("dtXml", data.strTable.Select(x => new XElement("MandateId", x))));


        //    DataTable dt = new DataTable();
        //    dt.Columns.Add("MandateId", typeof(Int64));
        //    // Boolean IsFound = false;

        //    for (int i = 0; i < data.strTable.Length; i++)
        //    {
        //        DataRow dr = dt.NewRow();
        //        dt.Rows.Add(data.strTable[i]);

        //    }
        //    string strTable = GetXmlByDatable(dt);

        //    try
        //    {      //.With<ExcelGrid>()  .With<ExcelGrid>() .With<ExcelGrid>()
        //        var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<ExcelGrid1>().With<ExcelGrid>().Execute("@QueryType", "@UserId", "@strToDate", "@strFromDate", "@strTable", "@BankName", "testExeceldataDateWise", Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(data.UserId.Replace("_", "%"))), data.strToDate, data.strFromDate, strTable, data.BankName));
        //        //   var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<ExcelGrid1>().Execute("@QueryType", "@UserId", "@strToDate", "@strFromDate", "@strTable", "@BankName", "testExeceldataDateWise", Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(data.UserId.Replace("_", "%"))), data.strToDate, data.strFromDate, strTable, data.BankName);
        //        ///  var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_user]").With<Maker>().With<NachUser>().Execute("@QueryType", "@EntityId", "@UserId", "BindPresentmentMaker", DbSecurity.Decrypt(HttpContext.Current.Server.UrlDecode(EntityId.Replace("_", "%"))), DbSecurity.Decrypt(HttpContext.Current.Server.UrlDecode(UserId.Replace("_", "%")))));
        //        //foreach (var employe in Result)
        //        //{
        //        //    dataListEG1 = employe.Cast<ExcelGrid1>().ToList();
        //        //}

        //        return Result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        public void savePhysicalZip(DataTable dtExcelData,string userid)
        {
            try
            {

                if (dtExcelData.Rows.Count > 0)
                {
                    string filename = ConfigurationManager.AppSettings["DownloadFileName"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + "001" + "_" + "NachMan" + "_" + dtExcelData.Rows.Count + ".csv";
                    //  string xlsPath = Server.MapPath("../MandateFile/Xmlpath/" + Convert.ToString(Iace.User.CurrentUser.User.UserId)); ;
                    string xlsPath = System.Web.Hosting.HostingEnvironment.MapPath("../MandateFile/Xmlpath/" + userid); ;
                    // string Path = ConfigurationManager.AppSettings["OtherDomainPath_" + EntityId];
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                        string TempCount = "000000" + dtExcelData.Rows.Count.ToString();

                        zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                        zip.AddDirectoryByName(ConfigurationManager.AppSettings["ClientCode"].ToString() + "_" + ConfigurationManager.AppSettings["CollectionProductCode"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + TempCount.Substring(TempCount.Length - 6));



                        //zip.AddFile(xlsPath + filename, ConfigurationManager.AppSettings["DownloadFileName_" + EntityId].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + "001" + "_" + "NachMan" + "_" + dtExcelData.Rows.Count).FileName = filename;
                        var files = Directory.GetFiles(xlsPath, "*.xml");
                        foreach (var file in files)
                        {
                            filename = System.IO.Path.GetFileName(file);
                            zip.AddFile(file, ConfigurationManager.AppSettings["ClientCode"].ToString() + "_" + ConfigurationManager.AppSettings["CollectionProductCode"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + TempCount.Substring(TempCount.Length - 6));
                        }
                        int i = 1;
                        foreach (DataRow row in dtExcelData.Rows)
                        {
                            try
                            {

                                //string filePath = Server.MapPath(Convert.ToString(row["JPGPath"]).Replace("../", "/"));
                                string filePath = System.Web.Hosting.HostingEnvironment.MapPath(Convert.ToString(row["JPGPath"]).Replace("../", "/"));
                                string mandateid = Convert.ToString(row["mandateid"]);

                                string[] pathArr = filePath.Split('\\');
                                string fileName = System.IO.Path.GetFileName(filePath);
                                string Temp = "000000";
                                Temp = Temp + i.ToString();
                                Temp = Temp.Substring(Temp.Length - 6);

                                // Path = Path + ClientCode;
                                fileName = ConfigurationManager.AppSettings["ClientCode"] + "_" + ConfigurationManager.AppSettings["CollectionProductCode"] + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + Temp;
                                zip.AddFile(filePath, ConfigurationManager.AppSettings["ClientCode"].ToString() + "_" + ConfigurationManager.AppSettings["CollectionProductCode"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + TempCount.Substring(TempCount.Length - 6)).FileName = ConfigurationManager.AppSettings["ClientCode"].ToString() + "_" + ConfigurationManager.AppSettings["CollectionProductCode"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + TempCount.Substring(TempCount.Length - 6) + "//" + fileName + System.IO.Path.GetExtension(filePath);

                                // string filetifPath = Server.MapPath(Convert.ToString(row["TIPPath"]).Replace("../", "/"));
                               
                                    string filetifPath = System.Web.Hosting.HostingEnvironment.MapPath(Convert.ToString(row["TIPPath"]).Replace("../", "/"));
                                pathArr = filetifPath.Split('\\');
                                // fileName = System.IO.Path.GetExtension(filetifPath);
                                zip.AddFile(filetifPath, ConfigurationManager.AppSettings["ClientCode"].ToString() + "_" + ConfigurationManager.AppSettings["CollectionProductCode"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + TempCount.Substring(TempCount.Length - 6)).FileName = ConfigurationManager.AppSettings["ClientCode"].ToString() + "_" + ConfigurationManager.AppSettings["CollectionProductCode"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + TempCount.Substring(TempCount.Length - 6) + "//" + fileName + System.IO.Path.GetExtension(filetifPath);
                            }
                            catch { }
                            i++;
                        }
                        string zipName = String.Format("{0}.zip", (ConfigurationManager.AppSettings["ClientCode"].ToString() + "_" + ConfigurationManager.AppSettings["CollectionProductCode"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + TempCount.Substring(TempCount.Length - 6)));


                        // zip.Save(Path);
                        try
                        {
                            //var files = Directory.GetFiles(xlsPath, "*.xml", SearchOption.AllDirectories);
                            var filess = Directory.GetFiles(xlsPath, "*.xml");
                            foreach (var file in filess)
                            {
                                System.IO.File.Delete(file);
                            }
                        }
                        catch (System.IO.IOException e)
                        {
                            Console.WriteLine(e.Message);
                            return;
                        }

                        //
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.BufferOutput = false;
                        // string zipName = String.Format("Zip_{0}.zip", (ConfigurationManager.AppSettings["DownloadFileName"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + dtExcelData.Rows.Count));
                        HttpContext.Current.Response.ContentType = "application/zip";
                        HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                        //Response.Charset = "";
                        //this.EnableViewState = false;
                        //Response.Write("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
                        //Response.Write("\r\n");
                        //Response.Write("<style> td {mso-number-format:" + "\\@" + "; text-align:center;} </style>");
                        zip.Save(HttpContext.Current.Response.OutputStream);
                        HttpContext.Current.Response.End();
                    }

                    try
                    {
                        //var files = Directory.GetFiles(xlsPath, "*.xml", SearchOption.AllDirectories);
                        var files = Directory.GetFiles(xlsPath, "*.xml");
                        foreach (var file in files)
                        {
                            System.IO.File.Delete(file);
                        }
                    }
                    catch (System.IO.IOException e)
                    {
                        Console.WriteLine(e.Message);
                        return;
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                //RaiseError("savePhysicalZip " + ex.Message, EntityId);
            }
        }


        protected void PhysicalDownloadExcel(DataTable dtExcelData,string userid)
        {
            try
            {


                if (dtExcelData.Rows.Count > 0)
                {

                    //string filename = ConfigurationManager.AppSettings["DownloadFileName_" + EntityId].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + "001" + "_" + "NachMan" + "_" + dtExcelData.Rows.Count + ".csv";
                    //string Path = Server.MapPath("../MandateFile/Xmlpath/" + Convert.ToString(Iace.User.CurrentUser.User.UserId)); //ConfigurationManager.AppSettings["OtherDomainPath_"];

                    string Path = System.Web.Hosting.HostingEnvironment.MapPath("../MandateFile/Xmlpath/" + userid); //ConfigurationManager.AppSettings["OtherDomainPath_"];
                    if (!Directory.Exists(Path))
                    {
                        Directory.CreateDirectory(Path);
                    }

                    try
                    {
                        //var files = Directory.GetFiles(xlsPath, "*.xml", SearchOption.AllDirectories);
                        var filess = Directory.GetFiles(Path, "*.xml");
                        foreach (var file in filess)
                        {
                            System.IO.File.Delete(file);
                        }
                    }
                    catch (System.IO.IOException e)
                    {
                        Console.WriteLine(e.Message);
                        return;
                    }

                    string str = "";

                    int i = 1;
                    string filetifPath = "";
                    string fileName = "";
                    foreach (DataRow dr in dtExcelData.Rows)
                    {
                        //filetifPath = Server.MapPath(Convert.ToString(dr["TIPPath"]).Replace("../", "/"));
                        filetifPath = System.Web.Hosting.HostingEnvironment.MapPath(Convert.ToString(dr["TIPPath"]).Replace("../", "/"));
                        string ClientCode = ConfigurationManager.AppSettings["ClientCode"] + DateTime.Now.ToString("ddMMyyyy") + "";
                        string Temp = "000000000";
                        Temp = Temp + i.ToString();
                        Temp = Temp.Substring(Temp.Length - 9);
                        ClientCode = ClientCode + Temp;
                        // Path = Path + ClientCode;

                        string Temp1 = "000000";
                        Temp1 = Temp1 + i.ToString();
                        Temp1 = Temp1.Substring(Temp1.Length - 6);
                        fileName = ConfigurationManager.AppSettings["ClientCode"] + "_" + ConfigurationManager.AppSettings["CollectionProductCode"] + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + Temp1;
                        str = "<?xml version='1.0' encoding='UTF-8'?><Document xmlns='urn:iso:std:iso:20022:tech:xsd:pain.009.001.01'>  <MndtInitnReq><GrpHdr><MsgId>" + ClientCode + "</MsgId><CreDtTm>" + Convert.ToString(dr["MessageCreationDateTime"]) + "</CreDtTm><InstgAgt><FinInstnId><ClrSysMmbId><MmbId>" + Convert.ToString(dr["SponserbankIFSC"]) + "</MmbId></ClrSysMmbId><Nm>" + Convert.ToString(dr["SponsorBankName"]) + "</Nm></FinInstnId></InstgAgt><InstdAgt><FinInstnId><ClrSysMmbId><MmbId>" + Convert.ToString(dr["CustIFSC"]) + "</MmbId></ClrSysMmbId><Nm>" + Convert.ToString(dr["CustBankName"]) + "</Nm></FinInstnId></InstdAgt></GrpHdr><Mndt><MndtReqId>" + Convert.ToString(dr["Debtorotherdetails"]) + "</MndtReqId><Tp><SvcLvl><Prtry>" + Convert.ToString(dr["CategoryCode"]) + "</Prtry></SvcLvl><LclInstrm><Prtry>" + Convert.ToString(dr["TXNtype"]) + "</Prtry></LclInstrm></Tp><Ocrncs><SeqTp>" + Convert.ToString(dr["Recurring"]) + "</SeqTp><Frqcy>" + Convert.ToString(dr["Frequency"]) + "</Frqcy><FrstColltnDt>" + Convert.ToString(dr["FirstCollectionDate"]) + "</FrstColltnDt></Ocrncs>";
                        if (Convert.ToString(dr["MaximumAmount"]) != "" || Convert.ToString(dr["MaximumAmount"]) != "")
                        {
                            str = str + "<MaxAmt Ccy='INR'>" + Convert.ToString(dr["MaximumAmount"]) + "</MaxAmt>";
                        }
                        else
                        {
                            str = str + "<ColltnAmt Ccy='INR'>" + Convert.ToString(dr["CollectionAmount"]) + "</ColltnAmt>";
                        }

                        str = str + "<Cdtr><Nm>" + Convert.ToString(dr["NameofUtility"]) + "</Nm></Cdtr><CdtrAcct><Id><Othr><Id>" + Convert.ToString(dr["UtilityCode"]) + "</Id></Othr></Id></CdtrAcct><CdtrAgt><FinInstnId><ClrSysMmbId><MmbId>" + Convert.ToString(dr["SponserbankIFSC"]) + "</MmbId></ClrSysMmbId><Nm>" + Convert.ToString(dr["SponsorBankName"]) + "</Nm></FinInstnId></CdtrAgt><Dbtr><Nm>" + Convert.ToString(dr["DebtorName"]) + "</Nm><Id><PrvtId><Othr><Id>" + Convert.ToString(dr["ConsumerReferencNo"]) + "</Id>";
                        if (Convert.ToString(dr["ConsumerReferencNo2"]) != "" && Convert.ToString(dr["ConsumerReferencNo2"]) != null)
                        {
                            str = str + "<SchmeNm><Prtry>" + Convert.ToString(dr["ConsumerReferencNo2"]) + "</Prtry></SchmeNm>";
                        }
                        str = str + "</Othr></PrvtId></Id><CtctDtls><MobNb>+91-" + Convert.ToString(dr["DebtorMobileNo"]) + "</MobNb></CtctDtls></Dbtr><DbtrAcct><Id><Othr><Id>" + Convert.ToString(dr["DestinationBankAccountNumber"]) + "</Id></Othr></Id><Tp><Prtry>" + Convert.ToString(dr["DestinationBankAccountType"]) + "</Prtry></Tp></DbtrAcct><DbtrAgt><FinInstnId><ClrSysMmbId><MmbId>" + Convert.ToString(dr["CustIFSC"]) + "</MmbId></ClrSysMmbId><Nm>" + Convert.ToString(dr["CustBankName"]) + "</Nm></FinInstnId></DbtrAgt></Mndt></MndtInitnReq></Document>";
                        File.WriteAllText(Path + "\\" + fileName + ".xml", str);
                        //  XmlDocument doc = new XmlDocument();

                        i++;
                    }

                }



            }
            catch (Exception ex)
            {
                //RaiseError("PhysicalDownloadExcel " + ex.Message, EntityId);
            }
        }

        public BindExcelData ExcelDownload(Exceldata data)
        {
            try
            {

                BindExcelData res = new BindExcelData();
                //CountResponseData res = new CountResponseData();
                // QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                List<ExcelGrid> GridBindlist = new List<ExcelGrid>();
                List<ExcelGrid1> GridBindlist1 = new List<ExcelGrid1>();

                XDocument doc = new XDocument();
                doc.Add(new XElement("dtXml", data.strTable.Select(x => new XElement("MandateId", x))));


                DataTable dt = new DataTable();
                dt.Columns.Add("MandateId", typeof(Int64));
                // Boolean IsFound = false;

                for (int i = 0; i < data.strTable.Length; i++)
                {
                    DataRow dr = dt.NewRow();
                    dt.Rows.Add(data.strTable[i]);

                }
                string strTable = GetXmlByDatable(dt);
                string UserID = Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(data.UserId.Replace("_", "%")));
                //var Result = dbcontext.MultipleResults("[dbo].[Sp_ProductMaster]").With<GridBind>().With<Count>().
                //    Execute("@QueryType", "@PageCount", "@SearchText", "@AppId", "@EntityId", "BindGrid", obj2.PageCount, obj2.SearchText, obj2.AppId, Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(obj2.EntityId.Replace("_", "%"))));
                var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<ExcelGrid1>().With<ExcelGrid>().Execute("@QueryType", "@UserId", "@strToDate", "@strFromDate", "@strTable", "@BankName", "testExeceldataDateWise", Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(data.UserId.Replace("_", "%"))), data.strToDate, data.strFromDate, strTable, data.BankName);
                //.With<ExcelGrid>()s
               // res.ExcelGrid_1_list=Result[0].Cast<ExcelGrid1>().ToList();

                res.ExcelGrid_1_list = Result[0].Cast<ExcelGrid1>().ToList();
                res.ExcelGridlist = Result[1].Cast<ExcelGrid>().ToList();
                

                DataTable dtExcelData0 = ToDataTable(res.ExcelGridlist);
                DataTable dtExcelData1 = ToDataTable(res.ExcelGrid_1_list);

                if (data.BankName == "IDFC Bank")
                {
                    savePhysicalZip(dtExcelData0, UserID);
                    PhysicalDownloadExcel(dtExcelData1, UserID);

                }
                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }








        public IEnumerable<DownloadMandateDetails> getzip(ZipDownload data)
        {

            //string[] mandatearr = IsMandateID.Split(',');



            XDocument doc = new XDocument();
            doc.Add(new XElement("dtXml", data.strTable.Select(x => new XElement("MandateId", x))));

            DataTable dt = new DataTable();
            dt.Columns.Add("MandateId", typeof(Int64));


            for (int i = 0; i < data.strTable.Length; i++)
            {
                DataRow dr = dt.NewRow();

                dt.Rows.Add(data.strTable[i]);

            }
            string strTable = GetXmlByDatable(dt);

            try
            {

                // var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateGridDetails>().Execute("@QueryType", "@UserId", "@strToDate", "@strFromDate", "@strTable", "SnapdataDateWise", Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(data.UserId.Replace("_", "%"))), data.strToDate, data.strFromDate, strTable));

                //  var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateGridDetails>().Execute("@QueryType", "@UserId", "@strToDate", "@strFromDate", "@strTable", "@RejectedReason", "RejectdataDateWise", DbSecurity.Decrypt(HttpContext.Current.Server.UrlDecode(userID.Replace("_", "%"))), todate, fromdate, strTable, rejectcomnt));
                var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateDetails>().Execute("@QueryType", "@UserId", "UserBank", Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode("0IMQt1aVz7k_3d_7cXydbXRkAQiA_3d".Replace("_", "%"))));


                //var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<ZipGrid>().Execute("@QueryType", "@strTable", "@strToDate" , "@strFromDate", "@UserId", "SnapdataDateWise", strTable, data.strToDate,data.strFromDate, Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(data.UserId.Replace("_", "%"))));

                foreach (var employe in Result)
                {
                    dataList = employe.Cast<DownloadMandateDetails>().ToList();
                }

                DataTable dtzip = ToDataTable(dataListZP);

                //if (IsFound == true)
                //{
                // DataTable dtExcelData = dtdata.Tables[0];
                //if (dtExcelData.Rows.Count > 0)
                //{

                // Extract the directory we just created.
                // ... Store the results in a new folder called "destination".
                // ... The new folder must not exist.


                // string Path = "C:/Users/Rishabh/Pictures/Saved Pictures/CHKrDLa"; //Server.MapPath("");
                using (ZipFile zip = new ZipFile())
                {

                    zip.AlternateEncodingUsage = ZipOption.AsNecessary;

                    //zip.AddDirectoryByName(ConfigurationManager.AppSettings["DownloadFileName"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy"));
                    //foreach (GridViewRow row in grdDownload.Rows)
                    //{
                    //if ((row.FindControl("chkSelect") as CheckBox).Checked)
                    //{
                    //string Data = (row.FindControl("lblFilePath") as Label).Text;
                    //if (Data != Path)
                    //{https://cdn2.iconfinder.com/data/icons/designer-skills/128/react-512.png
                    // isfound = true;E:/vscode angular/QuickcheckApi/QuickZipWebAPI/Images/CHKrDLa.jpg
                    string filePath = "E:/vscode angular/QuickcheckApi/QuickZipWebAPI/Images/CHKrDLa.jpg";
                    string mandateid = "101200120102";

                    string[] pathArr = filePath.Split('\\');
                    string fileName = pathArr.Last();

                    string[] New_FileName_jpg = fileName.Split('_');

                    // string[] last_name_jpg = New_FileName_jpg[2].Split('.');

                    //zip.AddFile(filePath, 1212 + "_" + DateTime.Now.ToString("ddMMyyyy")).FileName =
                    ////ConfigurationManager.AppSettings["DownloadFileName"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "/" + mandateid + "_" + fileName;
                    //1212 + "_" + DateTime.Now.ToString("ddMMyyyy") + "/" + 1212 + "_" + mandateid + "_" + New_FileName_jpg[0].ToString() + "." + 1212;

                    zip.AddFile(filePath);
                    //string filetifPath = (row.FindControl("lblTifPath") as Label).Text;
                    //pathArr = filetifPath.Split('\\');
                    //fileName = pathArr.Last();
                    //string[] New_FileName_tif = fileName.Split('_');

                    //string[] last_name_tif = New_FileName_tif[2].Split('.');

                    //zip.AddFile(filetifPath, ConfigurationManager.AppSettings["DownloadFileName"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy")).FileName =
                    //ConfigurationManager.AppSettings["DownloadFileName"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "/" + last_name_tif[0].ToString() + "_" + mandateid + "_" + New_FileName_tif[0].ToString() + "." + last_name_tif[1].ToString();

                    //}
                    //}
                    // }
                    //);
                    //                            if (isfound == false)
                    //                            {
                    //                                lblmsg.Text = "Image is not Found !!";
                    //                            }
                    //else
                    //{Response.Buffer = true

                    HttpContext.Current.Response.Clear();
                    //HttpContext.Current.Response.Buffer = true; //new added
                    HttpContext.Current.Response.BufferOutput = true; // edited in place of false
                    string zipName = String.Format("Zip_{0}.zip", (1212 + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + "132"));// dtExcelData.Rows.Count));
                    HttpContext.Current.Response.ContentType = "application/zip";
                    HttpContext.Current.Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                    //Response.Charset = "";
                    //this.EnableViewState = false;
                    //Response.Write("<html xmlns:x=\"urn:schemas-microsoft-com:office:excel\">");
                    //Response.Write("\r\n");
                    //Response.Write("<style> td {mso-number-format:" + "\\@" + "; text-align:center;} </style>");

                    if (HttpContext.Current.Response.IsClientConnected) {
                        zip.Save(HttpContext.Current.Response.OutputStream);

                        HttpContext.Current.Response.Close();
                        //Response.End(); Flush EndRequest End
                    }// }
                    
                }

                // }




                // }

                //return Result;
                return dataList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);
            //Get all the properties by using reflection   
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Setting column names as Property names  
                dataTable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {

                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }

            return dataTable;
        }

        public string GetXmlByDatable(DataTable dtObjectforXml)
        {
            if (dtObjectforXml == null)
                return "";
            if (dtObjectforXml.Rows.Count == 0)
                return "";

            if (dtObjectforXml.TableName == "")
                dtObjectforXml.TableName = "dtXml";

            XmlDocument objectXmlDocument = new XmlDocument();
            XmlElement objElement = objectXmlDocument.CreateElement(dtObjectforXml.TableName);

            for (int iRecordCounter = 0; iRecordCounter < dtObjectforXml.Rows.Count; iRecordCounter++)
            {
                // Generate XmlObject and Append Nodes by calling a Child function.
                objElement.AppendChild(BuildXmlElement(dtObjectforXml.Rows[iRecordCounter], objectXmlDocument));
            }

            objectXmlDocument.AppendChild(objElement);
            return objectXmlDocument.OuterXml;
        }

        private XmlElement BuildXmlElement(DataRow drObjectforXml, XmlDocument objectXmlDocument)
        {
            XmlElement XmlElement = objectXmlDocument.CreateElement(drObjectforXml.Table.TableName);
            for (int iColumnCounter = 0; iColumnCounter < drObjectforXml.Table.Columns.Count; iColumnCounter++)
            {
                XmlElement.SetAttribute(drObjectforXml.Table.Columns[iColumnCounter].ColumnName, Convert.ToString(drObjectforXml[iColumnCounter].ToString()));
            }

            return XmlElement;
        }


    }
}