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
        List<ExcelGrid1> dataListEG1 = new List<ExcelGrid1>();
        
        public IEnumerable<DownloadMandateDetails> Binddropdownbank(string userId)
        {
            // List<DownloadMandateDetails> dataList = new List<DownloadMandateDetails>();
            try//Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(userId.Replace("_", "%")))
            {
                 var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateDetails>().Execute("@QueryType", "@UserId", "UserBank", Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(userId.Replace("_", "%"))));

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
                var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateGridDetails>().Execute("@QueryType", "@UserId", "@strToDate", "@strFromDate", "@SponsorBankCode", "grdMandateDateWise", Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(griddata.UserId.Replace("_", "%"))), griddata.strToDate, griddata.strFromDate, griddata.SponsorBankCode);

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
                var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateGridDetails>().Execute("@QueryType", "@UserId", "@Refrence1", "grdMandateRefrenceWise", Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(userId.Replace("_", "%"))), refNo);

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
                var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateGridDetails>().Execute("@QueryType", "@UserId", "@strToDate", "@strFromDate", "@strTable", "@RejectedReason", "RejectdataDateWise", Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(data.UserId.Replace("_", "%"))), todate[0], fromdate[0], strTable,data.RejectedReason));
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
                var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateGridDetails>().Execute("@QueryType", "@UserId", "@strTable", "UpdateSubmitted", Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(userID.Replace("_", "%"))), strTable));
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //ExcelDownload exceldata

        public Dictionary<string, object> ExcelDownload(Exceldata data)
        {

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

            try
            {      //.With<ExcelGrid>()  .With<ExcelGrid>() .With<ExcelGrid>()
                var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<ExcelGrid1>().With<ExcelGrid>().Execute("@QueryType", "@UserId", "@strToDate", "@strFromDate", "@strTable", "@BankName", "testExeceldataDateWise", Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(data.UserId.Replace("_", "%"))), data.strToDate, data.strFromDate, strTable, data.BankName));
             //   var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<ExcelGrid1>().Execute("@QueryType", "@UserId", "@strToDate", "@strFromDate", "@strTable", "@BankName", "testExeceldataDateWise", Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(data.UserId.Replace("_", "%"))), data.strToDate, data.strFromDate, strTable, data.BankName);
                ///  var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_user]").With<Maker>().With<NachUser>().Execute("@QueryType", "@EntityId", "@UserId", "BindPresentmentMaker", DbSecurity.Decrypt(HttpContext.Current.Server.UrlDecode(EntityId.Replace("_", "%"))), DbSecurity.Decrypt(HttpContext.Current.Server.UrlDecode(UserId.Replace("_", "%")))));
                //foreach (var employe in Result)
                //{
                //    dataListEG1 = employe.Cast<ExcelGrid1>().ToList();
                //}
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public BindExcelData ExcelDownload(Exceldata data)
        //{
        //    try
        //    {

        //        BindExcelData res = new BindExcelData();
        //        //CountResponseData res = new CountResponseData();
        //        // QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
        //        List<ExcelGrid> GridBindlist = new List<ExcelGrid>();
        //        List<ExcelGrid1> GridBindlist1 = new List<ExcelGrid1>();

        //        XDocument doc = new XDocument();
        //        doc.Add(new XElement("dtXml", data.strTable.Select(x => new XElement("MandateId", x))));


        //        DataTable dt = new DataTable();
        //        dt.Columns.Add("MandateId", typeof(Int64));
        //        // Boolean IsFound = false;

        //        for (int i = 0; i < data.strTable.Length; i++)
        //        {
        //            DataRow dr = dt.NewRow();
        //            dt.Rows.Add(data.strTable[i]);

        //        }
        //        string strTable = GetXmlByDatable(dt);
        //        //var Result = dbcontext.MultipleResults("[dbo].[Sp_ProductMaster]").With<GridBind>().With<Count>().
        //        //    Execute("@QueryType", "@PageCount", "@SearchText", "@AppId", "@EntityId", "BindGrid", obj2.PageCount, obj2.SearchText, obj2.AppId, Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(obj2.EntityId.Replace("_", "%"))));
        //        var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<ExcelGrid1>().Execute("@QueryType", "@UserId", "@strToDate", "@strFromDate", "@strTable", "@BankName", "testExeceldataDateWise", Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(data.UserId.Replace("_", "%"))), data.strToDate, data.strFromDate, strTable, data.BankName);
        //        //.With<ExcelGrid>()s
        //       // res.ExcelGridlist = Result[0].Cast<ExcelGrid>().ToList();
        //        res.ExcelGrid_1_list = Result[1].Cast<ExcelGrid1>().ToList();


        //        return res;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}








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

                // var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateGridDetails>().Execute("@QueryType", "@UserId", "@strToDate", "@strFromDate", "@strTable", "SnapdataDateWise", Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(data.UserId.Replace("_", "%"))), data.strToDate, data.strFromDate, strTable));

                //  var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateGridDetails>().Execute("@QueryType", "@UserId", "@strToDate", "@strFromDate", "@strTable", "@RejectedReason", "RejectdataDateWise", DbSecurity.Decrypt(HttpContext.Current.Server.UrlDecode(userID.Replace("_", "%"))), todate, fromdate, strTable, rejectcomnt));
                //var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateDetails>().Execute("@QueryType", "@UserId", "UserBank", DbSecurity.Decrypt(HttpContext.Current.Server.UrlDecode("0IMQt1aVz7k_3d_7cXydbXRkAQiA_3d".Replace("_", "%"))));


                var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<DownloadMandateDetails>().Execute("@QueryType", "@UserId", "UserBank", Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode("TlmOmfXm76ngWqiSFTG4rQ==".Replace("_", "%"))));

                foreach (var employe in Result)
                {
                    dataList = employe.Cast<DownloadMandateDetails>().ToList();
                }

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