using BusinessLibrary;
using Encryptions;
using EntityDAL;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using PhysicalMandate.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;


namespace PhysicalMandate.Controllers.BusinessLogic
{
    public static class PrintWithoutQR
    {

        public static string PrintWIthoutQR(string MandateId, string AppId)
        {
            string PDFBase64 = "";
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                List<GetCredential> GetCredentialList = new List<GetCredential>();
                var Result = dbcontext.MultipleResults("[dbo].[Sp_GetMandatemodeData]").With<GetCredential>().
               Execute("@QueryType", "@AppId", "GetEntityCredential", AppId);
                GetCredentialList = Result[0].Cast<GetCredential>().ToList();

                string temppath = System.Web.Hosting.HostingEnvironment.MapPath("~");
                string ID = MandateId;
                DataSet ds = new DataSet();

                // var result = CreatePdf.CheckQrLogo(DbSecurity.Encrypt(Iace.User.CurrentUser.User.ReferenceId.ToString()));
                // var result = Request.QueryString["Type"];
                string filePath = GetCredentialList[0].FilePath_SaveEditAPI; //ConfigurationManager.AppSettings["FilePath"];
                Byte[] bytes;
                //if (result == "1")
                //{
                //    FileStream fs = new FileStream(filePath + "/MandateQrcode/" + ID + ".png", FileMode.Open, FileAccess.Read);
                //    BinaryReader br = new BinaryReader(fs);
                //    bytes = br.ReadBytes((Int32)fs.Length);
                //    Response.Redirect("~/CreatePdfQR.aspx?id=" + DbSecurity.Encrypt(ID), false);
                //}
                //else
               //{
                    FileStream fs = new FileStream(filePath + "/PdfLogoImage/"+AppId+"/logo.png", FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    bytes = br.ReadBytes((Int32)fs.Length);
                ds = PrintHelper.GetShowForPDF(MandateId, AppId);
                //}

                //ds = BusinessLibrary1.GetShowForPDF(ID);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        int count = (dr["EmailId"].ToString()).Length;
                        int count_Ref1 = (dr["Reference1"].ToString()).Length;
                        int count_Ref2 = (dr["Reference2"].ToString()).Length;
                        int count_Customer = (dr["BenificiaryName"].ToString()).Length;
                        int count_IherebyAuthorize = (dr["CompanyName"].ToString()).Length;
                        int count_BankName = (dr["BankName"].ToString()).Length;



                        Font fontAb11 = FontFactory.GetFont("Verdana", 8, Font.BOLD);
                        Font fontAbCutomer = FontFactory.GetFont("Verdana", 7, Font.BOLD);
                        Font font9 = FontFactory.GetFont("Verdana", 8, Font.BOLD);
                        Font fontAb11BU = FontFactory.GetFont("Verdana", 7, Font.UNDERLINE, Color.GRAY);
                        Font fontAb6 = FontFactory.GetFont("Verdana", 8, Font.BOLD);
                        Font fontAb9 = FontFactory.GetFont("Verdana", 8, Font.BOLD);
                        Font fontAb9B = FontFactory.GetFont("Verdana", 8, Font.BOLD);
                        Font fontAb11B = FontFactory.GetFont("Verdana", 8, Font.BOLD);
                        Font fontA11B = FontFactory.GetFont("Verdana", 8, Font.BOLD);
                        Font fontA119B = FontFactory.GetFont("Verdana", 9, Font.BOLD);
                        Font fontText = FontFactory.GetFont("Verdana", 7, Font.BOLD);
                        Font fontText6 = FontFactory.GetFont("Verdana", 6, Font.BOLD);
                        Font fontText5 = FontFactory.GetFont("Verdana", 5, Font.BOLD);
                        Font fontText55 = FontFactory.GetFont("Verdana", 5, Font.BOLD);
                        Font fontText44 = FontFactory.GetFont("Verdana", 4, Font.BOLD);
                        Font fontText4 = FontFactory.GetFont("Verdana", 1);


                        //Font fontText = FontFactory.GetFont("Verdana", 7);

                        iTextSharp.text.Image CutterImage = iTextSharp.text.Image.GetInstance(PrintHelper.GetcutterImage(AppId));
                        CutterImage.ScaleToFit(550f, 200f);


                        iTextSharp.text.Image LogoImage = iTextSharp.text.Image.GetInstance(bytes);
                        LogoImage.ScaleAbsolute(20f, 20f);
                        iTextSharp.text.Image Rupee = iTextSharp.text.Image.GetInstance(PrintHelper.GetRupeeIcon());
                        //Rupee.Border = iTextSharp.text.Rectangle.BOX;
                        Rupee.BorderColor = iTextSharp.text.Color.BLACK;
                        Rupee.BorderWidth = 12f;
                        Rupee.ScaleToFit(7f, 7f);


                        //FileStream fs111 = new FileStream(filePath + "/images/checkbox.jpg", FileMode.Open, FileAccess.Read);
                        FileStream fs111 = new FileStream(filePath + "/images/checkbox-background.jpg", FileMode.Open, FileAccess.Read);
                        BinaryReader br111 = new BinaryReader(fs111);
                        Byte[] bytes111 = br111.ReadBytes((Int32)fs111.Length);

                        iTextSharp.text.Image SmallcheckBox = iTextSharp.text.Image.GetInstance(bytes111);

                        SmallcheckBox.BorderColor = iTextSharp.text.Color.BLACK;
                        SmallcheckBox.BorderWidth = 12f;
                        SmallcheckBox.ScaleToFit(5f, 5f);


                        FileStream fs11 = new FileStream(filePath + "/images/tick-iconmandate.png", FileMode.Open, FileAccess.Read);
                        BinaryReader br11 = new BinaryReader(fs11);
                        Byte[] bytes11 = br11.ReadBytes((Int32)fs11.Length);

                        iTextSharp.text.Image checkBox = iTextSharp.text.Image.GetInstance(bytes11);

                        checkBox.BorderColor = iTextSharp.text.Color.BLACK;
                        checkBox.BorderWidth = 12f;
                        checkBox.ScaleToFit(8f, 8f);

                        FileStream fs1 = new FileStream(filePath + "/images/checkbox-background.jpg", FileMode.Open, FileAccess.Read);
                        BinaryReader br1 = new BinaryReader(fs1);
                        Byte[] bytes1 = br1.ReadBytes((Int32)fs1.Length);

                        //br.Close();
                        //fs.Close();

                        //string strQuery = "insert into tblLogoImages(ImageData) values (@ImageData)";
                        //SqlCommand cmd = new SqlCommand(strQuery);
                        //cmd.Parameters.Add("@ImageData", SqlDbType.Binary).Value = bytes;
                        //InsertUpdateData(cmd);

                        iTextSharp.text.Image Box = iTextSharp.text.Image.GetInstance(bytes1);

                        Box.BorderColor = iTextSharp.text.Color.BLACK;
                        Box.BorderWidth = 12f;
                        Box.ScaleToFit(8f, 8f);

                        int i = 1;

                        PdfPTable PdfHeaderTable = new PdfPTable(31);
                        PdfHeaderTable.DefaultCell.NoWrap = false;
                        PdfHeaderTable.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderTable.DefaultCell.Border = PdfCell.NO_BORDER;
                        PdfHeaderTable.WidthPercentage = 100;
                        float[] Headerwidths = new float[] { 4f, 3f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 2f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
                        PdfHeaderTable.SetWidths(Headerwidths);
                        PdfPCell PdfHeaderCell = null;
                        Document document = new Document();
                        document.Open();
                        Paragraph p = new Paragraph();
                        p.Add(new Chunk(LogoImage, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));

                        PdfHeaderCell = new PdfPCell(p);
                        PdfHeaderCell.FixedHeight = 25f;
                        PdfHeaderCell.Rowspan = 2;
                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);

                        PdfHeaderCell = new PdfPCell(new Phrase("UMRN", fontAb11B));
                        PdfHeaderCell.NoWrap = false;
                        PdfHeaderCell.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);

                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfHeaderCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);


                        //-------------------------------Add Date-------------------------------


                        PdfHeaderCell = new PdfPCell(new Phrase("Date", fontAb11B));
                        PdfHeaderCell.NoWrap = false;
                        PdfHeaderCell.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderTable.AddCell(PdfHeaderCell);

                        string Date = dr["SlipDate"].ToString();
                        char[] chars = new char[8];
                        chars = Date.ToCharArray();
                        if (Convert.ToInt32(chars.Length) > 0)
                        {
                            for (int j = 0; j < Convert.ToInt32(chars.Length); j++)
                            {
                                PdfHeaderCell = new PdfPCell(new Phrase(chars[j].ToString(), fontA119B));
                                PdfHeaderCell.NoWrap = false;
                                PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfHeaderCell.HorizontalAlignment = 1;
                                PdfHeaderTable.AddCell(PdfHeaderCell);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                PdfHeaderCell = new PdfPCell(new Phrase(" ", fontA119B));
                                PdfHeaderCell.NoWrap = false;
                                PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfHeaderCell.HorizontalAlignment = 1;
                                PdfHeaderTable.AddCell(PdfHeaderCell);
                            }
                        }

                        //----------------------------------------Add Sponsor bankcode---------------------
                        string SpBankCode = dr["SponserBankCode"].ToString();

                        PdfHeaderCell = new PdfPCell(new Phrase("Sponsor bankcode", fontAb11B));
                        PdfHeaderCell.NoWrap = false;
                        PdfHeaderCell.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell.Colspan = 4;
                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable.AddCell(PdfHeaderCell);


                        PdfHeaderCell = new PdfPCell(new Phrase(dr["SponserBankCode"].ToString(), fontAb11B));
                        PdfHeaderCell.NoWrap = false;
                        PdfHeaderCell.Colspan = 12;
                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable.AddCell(PdfHeaderCell);


                        PdfHeaderCell = new PdfPCell(new Phrase("Utility Code", fontAb11B));
                        PdfHeaderCell.NoWrap = false;
                        PdfHeaderCell.Colspan = 6;
                        PdfHeaderCell.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable.AddCell(PdfHeaderCell);

                        PdfHeaderCell = new PdfPCell(new Phrase(dr["UtilityCode"].ToString(), fontAb11B));
                        PdfHeaderCell.NoWrap = false;
                        PdfHeaderCell.Colspan = 8;
                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable.AddCell(PdfHeaderCell);

                        Document documentCheckBox = new Document();
                        documentCheckBox.Open();
                        Paragraph pCheckBox = new Paragraph();
                        //------------------------------- add Created Status-------------------------------------
                        string status = dr["CreatedStatus"].ToString();
                        if (status == "C")
                        {
                            pCheckBox.Add(new Phrase("CREATE ", fontText));
                            pCheckBox.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBox.Add(new Phrase(" MODIFY ", fontText));
                            pCheckBox.Add(new Phrase(" CANCEL ", fontText));

                        }
                        else if (status == "M")
                        {
                            pCheckBox.Add(new Phrase("CREATE ", fontText));

                            pCheckBox.Add(new Phrase(" MODIFY ", fontText));
                            pCheckBox.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBox.Add(new Phrase(" CANCEL ", fontText));

                        }
                        else if (status == "L")
                        {
                            pCheckBox.Add(new Phrase("CREATE ", fontText));

                            pCheckBox.Add(new Phrase(" MODIFY ", fontText));

                            pCheckBox.Add(new Phrase(" CANCEL ", fontText));
                            pCheckBox.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                        }
                        PdfHeaderCell = new PdfPCell(pCheckBox);
                        PdfHeaderCell.NoWrap = false;
                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderTable.AddCell(PdfHeaderCell);

                        //------------------------------------add BenificiaryName----------------------------------
                        PdfHeaderCell = new PdfPCell(new Phrase("I/We hereby Authorize", fontText));
                        PdfHeaderCell.NoWrap = false;
                        PdfHeaderCell.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                        PdfHeaderCell.Colspan = 4;
                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable.AddCell(PdfHeaderCell);


                        //PdfHeaderCell = new PdfPCell(new Phrase(dr["CompanyName"].ToString(), fontAb11B));
                        if (count_IherebyAuthorize < 34)
                        {
                            PdfHeaderCell = new PdfPCell(new Phrase(dr["CompanyName"].ToString(), fontAb11B));
                        }
                        else if (count_IherebyAuthorize < 40)
                        { PdfHeaderCell = new PdfPCell(new Phrase(dr["CompanyName"].ToString(), fontText)); }
                        else if (count_IherebyAuthorize < 48)
                        {
                            PdfHeaderCell = new PdfPCell(new Phrase(dr["CompanyName"].ToString(), fontText6));
                        }
                        else
                        {
                            PdfHeaderCell = new PdfPCell(new Phrase(dr["CompanyName"].ToString(), fontText5));
                        }

                        PdfHeaderCell.NoWrap = false;
                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell.Colspan = 10;
                        PdfHeaderCell.FixedHeight = 20f;
                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable.AddCell(PdfHeaderCell);

                        PdfHeaderCell = new PdfPCell(new Phrase("To Debit", fontAb11B));
                        PdfHeaderCell.NoWrap = false;
                        PdfHeaderCell.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell.Colspan = 5;
                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        Document documentCheckBoxSB = new Document();
                        documentCheckBoxSB.Open();
                        Paragraph pCheckBoxSB = new Paragraph();
                        //----------------------------------add To Debit---------------------------
                        string chDebit = dr["DebitTo"].ToString();
                        if (chDebit == "SB")
                        {
                            pCheckBoxSB.Add(new Phrase(" ", fontText));
                            pCheckBoxSB.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" OTHER ", fontText));
                        }
                        else if (chDebit == "CA")
                        {
                            pCheckBoxSB.Add(new Phrase(" ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" OTHER ", fontText));
                        }

                        else if (chDebit == "CC")
                        {
                            pCheckBoxSB.Add(new Phrase(" ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" OTHER ", fontText));
                        }
                        else if (chDebit == "RE")
                        {
                            pCheckBoxSB.Add(new Phrase(" ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" OTHER ", fontText));
                        }
                        else if (chDebit == "RD")
                        {
                            pCheckBoxSB.Add(new Phrase(" ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" OTHER ", fontText));
                        }
                        else if (chDebit == "OT")
                        {
                            pCheckBoxSB.Add(new Phrase(" ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB.Add(new Phrase(" OTHER ", fontText));
                        }
                        PdfHeaderCell = new PdfPCell(pCheckBoxSB);
                        PdfHeaderCell.NoWrap = false;

                        PdfHeaderCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell.Colspan = 11;
                        PdfHeaderCell.HorizontalAlignment = 1;
                        PdfHeaderCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable.AddCell(PdfHeaderCell);
                        PdfPTable PdfMidTable = new PdfPTable(32);
                        PdfMidTable.DefaultCell.NoWrap = false;
                        PdfMidTable.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfMidTable.DefaultCell.Border = PdfCell.NO_BORDER;
                        PdfMidTable.WidthPercentage = 100;
                        float[] PdfMidTableHeaderwidths = new float[] { 4f, 3f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
                        PdfMidTable.SetWidths(PdfMidTableHeaderwidths);
                        PdfPCell PdfMidCell = null;
                        //----------------------------------Add AccountNo.-------------------------------------------------

                        PdfMidCell = new PdfPCell(new Phrase("Bank a/c Number", fontAb11B));
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 6;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfHeaderCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfMidTable.AddCell(PdfMidCell);
                        string AccountNo = Dbsecurity.Decrypt(dr["AccountNo"].ToString());
                        char[] chrAcountNo = new char[Convert.ToInt32(AccountNo.Length)];
                        chrAcountNo = AccountNo.ToCharArray();
                        if (Convert.ToInt32(AccountNo.Length) <= 26)
                        {


                            for (int j = 0; j < Convert.ToInt32(chrAcountNo.Length); j++)
                            {
                                PdfMidCell = new PdfPCell(new Phrase(chrAcountNo[j].ToString(), fontA119B));
                                PdfMidCell.NoWrap = false;
                                PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfMidCell.HorizontalAlignment = 1;
                                PdfHeaderCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                PdfMidTable.AddCell(PdfMidCell);
                            }

                            int len = 26 - Convert.ToInt32(AccountNo.Length);
                            for (int k = 0; k < len; k++)
                            {
                                PdfMidCell = new PdfPCell(new Phrase(" ", fontA119B));
                                PdfMidCell.NoWrap = false;
                                PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfMidCell.HorizontalAlignment = 1;
                                PdfHeaderCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                PdfMidTable.AddCell(PdfMidCell);
                            }
                        }

                        PdfMidCell = new PdfPCell(new Phrase("With Bank", fontAb11B));
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidCell.FixedHeight = 20f;
                        PdfMidCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfMidTable.AddCell(PdfMidCell);

                        // PdfMidCell = new PdfPCell(new Phrase(dr["BankName"].ToString(), fontAb11));
                        if (count_BankName < 34)
                        {
                            PdfMidCell = new PdfPCell(new Phrase(dr["BankName"].ToString(), fontAb11B));
                        }
                        else if (count_BankName < 40)
                        {
                            PdfMidCell = new PdfPCell(new Phrase(dr["BankName"].ToString(), fontText));
                        }
                        else if (count_BankName < 48)
                        {
                            PdfMidCell = new PdfPCell(new Phrase(dr["BankName"].ToString(), fontText6));
                        }
                        else
                        {
                            PdfMidCell = new PdfPCell(new Phrase(dr["BankName"].ToString(), fontText5));
                        }

                        PdfMidCell.NoWrap = false;
                        PdfMidCell.FixedHeight = 30f;
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 6;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfMidTable.AddCell(PdfMidCell);


                        PdfMidCell = new PdfPCell(new Phrase("IFSC", fontAb11B));
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 2;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfMidTable.AddCell(PdfMidCell);

                        //-------------------------Add IFSC code--------------------------------
                        string IFSCcode = dr["IFSCcode"].ToString();
                        char[] chrIFSCcode = new char[Convert.ToInt32(IFSCcode.Length)];
                        chrIFSCcode = IFSCcode.ToCharArray();
                        if (Convert.ToInt32(chrIFSCcode.Length) == 11)
                        {
                            for (int j = 0; j < Convert.ToInt32(chrIFSCcode.Length); j++)
                            {
                                PdfMidCell = new PdfPCell(new Phrase(chrIFSCcode[j].ToString(), fontA119B));
                                PdfMidCell.NoWrap = false;
                                PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfMidCell.HorizontalAlignment = 1;
                                PdfMidCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;

                                PdfMidTable.AddCell(PdfMidCell);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 11; j++)
                            {
                                PdfMidCell = new PdfPCell(new Phrase(" ", fontA119B));
                                PdfMidCell.NoWrap = false;
                                PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfMidCell.HorizontalAlignment = 1;
                                PdfMidCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                PdfMidTable.AddCell(PdfMidCell);
                            }
                        }
                        PdfMidCell = new PdfPCell(new Phrase("or MICR", fontAb11B));
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 3;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfMidTable.AddCell(PdfMidCell);

                        //-------------------------Add MICRcode--------------------------------
                        string MICRcode = dr["MICRcode"].ToString();
                        char[] chrMICRcode = new char[9];
                        chrMICRcode = MICRcode.ToCharArray();

                        if (Convert.ToInt32(chrMICRcode.Length) == 9)
                        {
                            for (int j = 0; j < Convert.ToInt32(chrMICRcode.Length); j++)
                            {
                                PdfMidCell = new PdfPCell(new Phrase(chrMICRcode[j].ToString(), fontA119B));
                                PdfMidCell.NoWrap = false;
                                PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfMidCell.HorizontalAlignment = 1;
                                PdfMidCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                PdfMidTable.AddCell(PdfMidCell);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 9; j++)
                            {
                                PdfMidCell = new PdfPCell(new Phrase(" ", fontA119B));
                                PdfMidCell.NoWrap = false;
                                PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfMidCell.HorizontalAlignment = 1;
                                PdfMidCell.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                PdfMidTable.AddCell(PdfMidCell);
                            }
                        }
                        //-----------------------------------Add amount of Rupees---------------------------
                        PdfMidCell = new PdfPCell(new Phrase("an amount of Rupees", fontAb11B));
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 3;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);

                        PdfMidCell = new PdfPCell(new Phrase(PrintHelper.NumberToWords(Convert.ToInt32(Dbsecurity.Decrypt(Convert.ToString(dr["AmountInWord"])))), fontA11B));
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 22;
                        PdfMidCell.FixedHeight = 10f;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);

                        Document documentAmountInDigit = new Document();
                        documentAmountInDigit.Open();
                        Paragraph pAmountInDigit = new Paragraph();
                        pAmountInDigit.Add(new Chunk(Rupee, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_CENTER));
                        pAmountInDigit.Add(new Phrase(" " + Dbsecurity.Decrypt(dr["AmountInDigit"].ToString()), fontA119B));
                        PdfMidCell = new PdfPCell(pAmountInDigit);

                        PdfMidCell.NoWrap = false;

                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 18;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);




                        //Document documentAmountInDigit = new Document();
                        //documentAmountInDigit.Open();
                        //Paragraph pAmountInDigit = new Paragraph();
                        //pAmountInDigit.Add(new Chunk(Rupee, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_CENTER));
                        //pAmountInDigit.Add(new Phrase(" " + dr["AmountInDigit"].ToString(), fontAb11));
                        //PdfMidCell = new PdfPCell(pAmountInDigit);

                        //PdfMidCell.NoWrap = false;

                        //PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        //PdfMidCell.Colspan = 13;

                        //PdfMidTable.AddCell(PdfMidCell);

                        //---------------------------------Add Frequency--------------------------------------
                        string Freq = dr["Frequency"].ToString();

                        PdfMidCell = new PdfPCell(new Phrase("Frequency", fontAb11B));
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);


                        Document documentMonthly = new Document();
                        documentMonthly.Open();
                        Paragraph pMonthly = new Paragraph();
                        //------------------------------- add Monthly-------------------------------------
                        if (Freq == "M")
                        {
                            pMonthly.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pMonthly.Add(new Phrase("  Monthly ", fontText));
                        }
                        else
                        {
                            pMonthly.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pMonthly.Add(new Phrase("  Monthly ", fontText));
                        }

                        PdfMidCell = new PdfPCell(pMonthly);
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 2;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);

                        Document documentQtly = new Document();
                        documentQtly.Open();
                        Paragraph pQtly = new Paragraph();
                        //------------------------------- add Qtly-------------------------------------
                        if (Freq == "Q")
                        {
                            pQtly.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pQtly.Add(new Phrase("  Qtly ", fontText));
                        }
                        else
                        {
                            pQtly.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pQtly.Add(new Phrase("  Qtly ", fontText));
                        }

                        PdfMidCell = new PdfPCell(pQtly);
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 2;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);

                        Document documentHYrly = new Document();
                        documentHYrly.Open();
                        Paragraph pHYrly = new Paragraph();
                        //------------------------------- add H-Yrly-------------------------------------
                        if (Freq == "H")
                        {
                            pHYrly.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pHYrly.Add(new Phrase("  H-Yrly ", fontText));
                        }
                        else
                        {
                            pHYrly.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pHYrly.Add(new Phrase("  H-Yrly ", fontText));
                        }

                        PdfMidCell = new PdfPCell(pHYrly);
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 3;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);

                        Document documentYearly = new Document();
                        documentYearly.Open();
                        Paragraph pYearly = new Paragraph();
                        //------------------------------- add Yearly-------------------------------------
                        if (Freq == "Y")
                        {
                            pYearly.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pYearly.Add(new Phrase("  Yearly ", fontText));
                        }
                        else
                        {
                            pYearly.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pYearly.Add(new Phrase("  Yearly ", fontText));
                        }

                        PdfMidCell = new PdfPCell(pYearly);
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 3;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);

                        Document prensentedprensented = new Document();
                        prensentedprensented.Open();
                        Paragraph prensented = new Paragraph();
                        if (Freq == "A")
                        {
                            prensented.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            prensented.Add(new Phrase("  As & when prensented ", fontText));
                        }
                        else
                        {
                            prensented.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            prensented.Add(new Phrase("  As & when prensented ", fontText));
                        }

                        PdfMidCell = new PdfPCell(prensented);
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 7;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);

                        string DebitType = dr["DebitType"].ToString();

                        PdfMidCell = new PdfPCell(new Phrase("Debit Type", fontAb11B));
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 3;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);

                        Document documentFixed = new Document();
                        documentFixed.Open();
                        Paragraph pFixed = new Paragraph();
                        //------------------------------- add H-Yrly-------------------------------------
                        if (DebitType == "F")
                        {
                            pFixed.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pFixed.Add(new Phrase("  Fixed Amount ", fontText));
                        }
                        else
                        {
                            pFixed.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pFixed.Add(new Phrase("  Fixed Amount ", fontText));
                        }
                        PdfMidCell = new PdfPCell(pFixed);
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 5;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);

                        Document documentMaximum = new Document();
                        documentMaximum.Open();
                        Paragraph pMaximum = new Paragraph();
                        //------------------------------- add H-Yrly-------------------------------------
                        if (DebitType == "M")
                        {
                            pMaximum.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pMaximum.Add(new Phrase("  Maximum Amount ", fontText));
                        }
                        else
                        {
                            pMaximum.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pMaximum.Add(new Phrase("  Maximum Amount ", fontText));
                        }

                        PdfMidCell = new PdfPCell(pMaximum);
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 6;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);

                        PdfMidCell = new PdfPCell(new Phrase("Reference 1", fontAb11B));
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);
                        //PdfMidCell = new PdfPCell(new Phrase(dr["Reference1"].ToString(), fontAb11B));
                        if (count_Ref1 < 34)
                        {
                            PdfMidCell = new PdfPCell(new Phrase(dr["Reference1"].ToString(), fontAb11B));
                        }
                        else if (count_Ref1 < 40)
                        { PdfMidCell = new PdfPCell(new Phrase(dr["Reference1"].ToString(), fontText)); }
                        else if (count_Ref1 < 48)
                        {
                            PdfMidCell = new PdfPCell(new Phrase(dr["Reference1"].ToString(), fontText6));
                        }
                        else
                        {
                            PdfMidCell = new PdfPCell(new Phrase(dr["Reference1"].ToString(), fontText5));
                        }

                        PdfMidCell.NoWrap = false;
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 15;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);


                        PdfMidCell = new PdfPCell(new Phrase("Phone Number ", fontAb11B));
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 6;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);

                        PdfMidCell = new PdfPCell(new Phrase(dr["PhoneNo"].ToString(), fontAb11B));
                        PdfMidCell.NoWrap = false;

                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 10;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);
                        PdfMidCell = new PdfPCell(new Phrase("Reference 2", fontAb11B));
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);

                        //PdfMidCell = new PdfPCell(new Phrase(dr["Reference2"].ToString(), fontAb11B));
                        if (count_Ref2 < 34)
                        {
                            PdfMidCell = new PdfPCell(new Phrase(dr["Reference2"].ToString(), fontAb11B));
                        }
                        else if (count_Ref2 < 40)
                        { PdfMidCell = new PdfPCell(new Phrase(dr["Reference2"].ToString(), fontText)); }
                        else if (count_Ref2 < 48)
                        {
                            PdfMidCell = new PdfPCell(new Phrase(dr["Reference2"].ToString(), fontText6));
                        }
                        else
                        {
                            PdfMidCell = new PdfPCell(new Phrase(dr["Reference2"].ToString(), fontText5));
                        }


                        PdfMidCell.NoWrap = false;
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 15;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);


                        PdfMidCell = new PdfPCell(new Phrase("EMail ID", fontAb11B));
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 6;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);

                        //PdfMidCell = new PdfPCell(new Phrase(dr["EmailId"].ToString(), fontAb11B));
                        if (count < 34)
                        {
                            PdfMidCell = new PdfPCell(new Phrase(dr["EmailId"].ToString(), fontAb11B));
                        }
                        else if (count < 40)
                        { PdfMidCell = new PdfPCell(new Phrase(dr["EmailId"].ToString(), fontText)); }
                        else if (count < 48)
                        {
                            PdfMidCell = new PdfPCell(new Phrase(dr["EmailId"].ToString(), fontText6));
                        }
                        else
                        {
                            PdfMidCell = new PdfPCell(new Phrase(dr["EmailId"].ToString(), fontText5));
                        }

                        PdfMidCell.NoWrap = false;
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 10;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);


                        PdfMidCell = new PdfPCell(new Phrase("I agree for the debit of mandate processing charges by the bank whom I am authorizing to debit my account as per latest schedule of charges of bank ", fontText));
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.Border = Rectangle.NO_BORDER;
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 32;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);
                        PdfMidCell = new PdfPCell(new Phrase("PERIOD", fontAb11B));
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);
                        PdfMidCell = new PdfPCell(new Phrase("", fontAb11B));
                        PdfMidCell.NoWrap = false;
                        PdfMidCell.Border = Rectangle.NO_BORDER;
                        PdfMidCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell.Colspan = 31;
                        PdfMidCell.HorizontalAlignment = 1;
                        PdfMidTable.AddCell(PdfMidCell);
                        PdfPTable PdfDetailTable = new PdfPTable(34);
                        PdfDetailTable.DefaultCell.NoWrap = false;
                        PdfDetailTable.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailTable.DefaultCell.Border = PdfCell.NO_BORDER;
                        PdfDetailTable.WidthPercentage = 100;
                        float[] Headerwidths1 = new float[] { 4f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 2f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
                        PdfDetailTable.SetWidths(Headerwidths1);
                        PdfPCell PdfDetailCell = null;
                        PdfDetailCell = new PdfPCell(new Phrase("From", fontAb11B));
                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.BackgroundColor = new Color(252, 252, 252);
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailTable.AddCell(PdfDetailCell);

                        string PeriodFrom = Convert.ToDateTime(Dbsecurity.Decrypt(dr["PeriodFrom"].ToString())).ToString("ddMMyyyy");
                        char[] chrPeriodFrom = new char[8];
                        chrPeriodFrom = PeriodFrom.ToCharArray();
                        if (Convert.ToInt32(chrPeriodFrom.Length) > 0)
                        {
                            for (int j = 0; j < Convert.ToInt32(chrPeriodFrom.Length); j++)
                            {
                                PdfDetailCell = new PdfPCell(new Phrase(chrPeriodFrom[j].ToString(), fontA119B));
                                PdfDetailCell.NoWrap = false;
                                PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfDetailCell.HorizontalAlignment = 1;
                                PdfDetailTable.AddCell(PdfDetailCell);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                PdfDetailCell = new PdfPCell(new Phrase(" ", fontA119B));
                                PdfDetailCell.NoWrap = false;
                                PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfDetailCell.HorizontalAlignment = 1;
                                PdfDetailTable.AddCell(PdfDetailCell);
                            }

                        }
                        PdfDetailCell = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailCell.Border = Rectangle.NO_BORDER;
                        PdfDetailCell.Colspan = 25;
                        PdfDetailTable.AddCell(PdfDetailCell);
                        PdfDetailCell = new PdfPCell(new Phrase("To*", fontAb11B));
                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.BackgroundColor = new Color(252, 252, 252);
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailTable.AddCell(PdfDetailCell);
                        string PeriodTo = "";// dr["PeriodTo"].ToString();
                        try
                        {
                            PeriodTo = Convert.ToDateTime(Dbsecurity.Decrypt(Convert.ToString(dr["PeriodTo"]))).ToString("ddMMyyyy");
                        }
                        catch { }
                        char[] chrPeriodTo = new char[8];
                        chrPeriodTo = PeriodTo.ToCharArray();
                        if (Convert.ToInt32(chrPeriodTo.Length) > 0)
                        {
                            if (PeriodTo != "01011900")
                            {
                                for (int j = 0; j < Convert.ToInt32(chrPeriodTo.Length); j++)
                                {
                                    PdfDetailCell = new PdfPCell(new Phrase(chrPeriodTo[j].ToString(), fontA119B));
                                    PdfDetailCell.NoWrap = false;
                                    PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    PdfDetailCell.HorizontalAlignment = 1;
                                    PdfDetailTable.AddCell(PdfDetailCell);
                                }
                            }
                            else
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    PdfDetailCell = new PdfPCell(new Phrase(" ", fontA119B));
                                    PdfDetailCell.NoWrap = false;
                                    //PdfDetailCell.BackgroundColor = new Color(0,0,0);
                                    PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    PdfDetailCell.HorizontalAlignment = 1;
                                    PdfDetailTable.AddCell(PdfDetailCell);
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                PdfDetailCell = new PdfPCell(new Phrase(" ", fontA119B));
                                PdfDetailCell.NoWrap = false;
                                PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfDetailCell.HorizontalAlignment = 1;
                                //PdfDetailCell.BackgroundColor = new Color(0, 0, 0);
                                PdfDetailTable.AddCell(PdfDetailCell);
                            }
                        }
                        PdfDetailCell = new PdfPCell(new Phrase(" ", fontAb11B));
                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailCell.Border = Rectangle.NO_BORDER;
                        PdfDetailCell.Colspan = 25;
                        PdfDetailTable.AddCell(PdfDetailCell);
                        PdfDetailCell = new PdfPCell(new Phrase("Or", fontAb11B));
                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.BackgroundColor = new Color(252, 252, 252);
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailTable.AddCell(PdfDetailCell);


                        Document documentCheckBox123 = new Document();
                        documentCheckBox.Open();
                        Paragraph pCheckBox123 = new Paragraph();
                        if (PeriodTo == "01011900")
                        {
                            pCheckBox123.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                        }
                        else
                        {
                            pCheckBox123.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                        }
                        pCheckBox123.Add(new Phrase(" Until Cancelled ", fontAb11));


                        PdfDetailCell = new PdfPCell(pCheckBox123);

                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.Colspan = 8;
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailTable.AddCell(PdfDetailCell);



                        PdfDetailCell = new PdfPCell(new Phrase("Sign. Primary Acc. Holder", fontAb11BU));
                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailCell.Border = Rectangle.NO_BORDER;
                        PdfDetailCell.Colspan = 8;
                        PdfDetailTable.AddCell(PdfDetailCell);
                        PdfDetailCell = new PdfPCell(new Phrase("Sign Acc. Holder", fontAb11BU));
                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailCell.Border = Rectangle.NO_BORDER;
                        PdfDetailCell.Colspan = 8;
                        PdfDetailTable.AddCell(PdfDetailCell);
                        PdfDetailCell = new PdfPCell(new Phrase("Sign Acc. Holder", fontAb11BU));
                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailCell.Border = Rectangle.NO_BORDER;
                        PdfDetailCell.Colspan = 9;
                        PdfDetailTable.AddCell(PdfDetailCell);
                        //PdfDetailCell = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfDetailCell.NoWrap = false;
                        //PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        //PdfDetailCell.HorizontalAlignment = 1;
                        //PdfDetailCell.Border = Rectangle.NO_BORDER;
                        //PdfDetailCell.Colspan = 34;
                        //PdfDetailTable.AddCell(PdfDetailCell);


                        PdfDetailCell = new PdfPCell(new Phrase(" ", fontAb11B));
                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailCell.Border = Rectangle.NO_BORDER;

                        PdfDetailCell.Colspan = 5;
                        PdfDetailTable.AddCell(PdfDetailCell);


                        //PdfDetailCell = new PdfPCell(new Phrase(dr["BenificiaryName"].ToString(), fontAbCutomer));
                        if (count_Customer < 10)
                        {
                            PdfDetailCell = new PdfPCell(new Phrase(dr["BenificiaryName"].ToString(), fontAbCutomer));//7
                        }
                        else if (count_Customer < 20)
                        {
                            PdfDetailCell = new PdfPCell(new Phrase(dr["BenificiaryName"].ToString(), fontText6));//6
                        }
                        else if (count_Customer < 30)
                        {
                            PdfDetailCell = new PdfPCell(new Phrase(dr["BenificiaryName"].ToString(), fontText55));//5
                        }
                        else
                        {
                            PdfDetailCell = new PdfPCell(new Phrase(dr["BenificiaryName"].ToString(), fontText44));//4
                        }

                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailCell.Border = Rectangle.NO_BORDER;
                        PdfDetailCell.Colspan = 13;
                        PdfDetailTable.AddCell(PdfDetailCell);

                        //PdfDetailCell = new PdfPCell(new Phrase(dr["Customer2"].ToString(), fontAbCutomer));
                        if (count_Customer < 10)
                        {
                            PdfDetailCell = new PdfPCell(new Phrase(dr["Customer2"].ToString(), fontAbCutomer));
                        }
                        else if (count_Customer < 20)
                        { PdfDetailCell = new PdfPCell(new Phrase(dr["Customer2"].ToString(), fontText6)); }
                        else if (count_Customer < 30)
                        {
                            PdfDetailCell = new PdfPCell(new Phrase(dr["Customer2"].ToString(), fontText55));
                        }
                        else
                        {
                            PdfDetailCell = new PdfPCell(new Phrase(dr["Customer2"].ToString(), fontText44));
                        }
                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailCell.Border = Rectangle.NO_BORDER;

                        PdfDetailCell.Colspan = 10;
                        PdfDetailTable.AddCell(PdfDetailCell);

                        //PdfDetailCell = new PdfPCell(new Phrase(dr["Customer3"].ToString(), fontAbCutomer));
                        if (count_Customer < 10)
                        {
                            PdfDetailCell = new PdfPCell(new Phrase(dr["Customer3"].ToString(), fontAbCutomer));
                        }
                        else if (count_Customer < 20)
                        { PdfDetailCell = new PdfPCell(new Phrase(dr["Customer3"].ToString(), fontText6)); }
                        else if (count_Customer < 30)
                        {
                            PdfDetailCell = new PdfPCell(new Phrase(dr["Customer3"].ToString(), fontText55));
                        }
                        else
                        {
                            PdfDetailCell = new PdfPCell(new Phrase(dr["Customer3"].ToString(), fontText44));
                        }


                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailCell.Border = Rectangle.NO_BORDER;

                        PdfDetailCell.Colspan = 6;
                        PdfDetailTable.AddCell(PdfDetailCell);



                        PdfDetailCell = new PdfPCell(new Phrase(" ", fontAb11B));
                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailCell.Border = Rectangle.NO_BORDER;
                        PdfDetailCell.Colspan = 9;
                        PdfDetailTable.AddCell(PdfDetailCell);
                        PdfDetailCell = new PdfPCell(new Phrase("1.Name as in Bank Records", fontAb11BU));
                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailCell.Border = Rectangle.NO_BORDER;
                        PdfDetailCell.Colspan = 8;
                        PdfDetailTable.AddCell(PdfDetailCell);
                        PdfDetailCell = new PdfPCell(new Phrase("2.Name as in Bank Records", fontAb11BU));
                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailCell.Border = Rectangle.NO_BORDER;
                        PdfDetailCell.Colspan = 8;
                        PdfDetailTable.AddCell(PdfDetailCell);
                        PdfDetailCell = new PdfPCell(new Phrase("3.Name as in Bank Records", fontAb11BU));
                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailCell.Border = Rectangle.NO_BORDER;
                        PdfDetailCell.Colspan = 9;
                        PdfDetailTable.AddCell(PdfDetailCell);

                        PdfDetailCell = new PdfPCell(new Phrase(" ", fontText4));
                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.HorizontalAlignment = 1;
                        PdfDetailCell.Border = Rectangle.NO_BORDER;
                        PdfDetailCell.Colspan = 34;
                        PdfDetailTable.AddCell(PdfDetailCell);

                        PdfDetailCell = new PdfPCell(new Phrase("This is to confirm that declaration has been carefully read, understood & made by me/us. I'm authorizing the user entity/Corporate to debit my account, based on the instruction as agreed and signed by me. I've understood that I'm authorized to cancel/amend this mandate by appropriately communicating the cancellation/amendment request to the user/entity/corporate or the bank where I've authorized the debit.", fontText5));
                        PdfDetailCell.NoWrap = false;
                        PdfDetailCell.Border = Rectangle.NO_BORDER;
                        PdfDetailCell.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell.Colspan = 34;
                        PdfDetailTable.AddCell(PdfDetailCell);

                        PdfPTable PdfHeaderTable1 = new PdfPTable(31);
                        PdfHeaderTable1.DefaultCell.NoWrap = false;
                        PdfHeaderTable1.SpacingBefore = 0;
                        PdfHeaderTable1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderTable1.DefaultCell.Border = PdfCell.NO_BORDER;
                        PdfHeaderTable1.WidthPercentage = 100;
                        float[] PdfHeaderTable1Headerwidths = new float[] { 4f, 3f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 2f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
                        PdfHeaderTable1.SetWidths(PdfHeaderTable1Headerwidths);
                        PdfPCell PdfHeaderCell1 = null;
                        PdfHeaderCell1 = new PdfPCell(new Phrase("Status:Success/Failure/Response Awaited", fontAb9));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.Colspan = 34;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                        Document document1 = new Document();
                        document1.Open();
                        Paragraph p1 = new Paragraph();
                        p1.Add(new Chunk(LogoImage, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));

                        PdfHeaderCell1 = new PdfPCell(p1);
                        PdfHeaderCell1.FixedHeight = 25f;
                        PdfHeaderCell1.Rowspan = 2;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase("UMRN", fontAb11B));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase("Date", fontAb11B));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        string Date1 = dr["SlipDate"].ToString();
                        char[] chars1 = new char[8];
                        chars1 = Date1.ToCharArray();
                        if (Convert.ToInt32(chars1.Length) > 0)
                        {
                            for (int j = 0; j < Convert.ToInt32(chars1.Length); j++)
                            {
                                PdfHeaderCell1 = new PdfPCell(new Phrase(chars1[j].ToString(), fontA119B));
                                PdfHeaderCell1.NoWrap = false;
                                PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfHeaderCell1.HorizontalAlignment = 1;
                                PdfHeaderTable1.AddCell(PdfHeaderCell1);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                PdfHeaderCell1 = new PdfPCell(new Phrase(" ", fontA119B));
                                PdfHeaderCell1.NoWrap = false;
                                PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfHeaderCell1.HorizontalAlignment = 1;
                                PdfHeaderTable1.AddCell(PdfHeaderCell1);
                            }
                        }
                        PdfHeaderCell1 = new PdfPCell(new Phrase("Sponsor bankcode", fontAb11B));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.Colspan = 4;
                        PdfHeaderCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(dr["SponserBankCode"].ToString(), fontAb11B));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.Colspan = 12;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase("Utility Code", fontAb11B));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.Colspan = 6;
                        PdfHeaderCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase(dr["UtilityCode"].ToString(), fontAb11B));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.Colspan = 8;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        Document documentCheckBox1 = new Document();
                        documentCheckBox1.Open();
                        Paragraph pCheckBox1 = new Paragraph();
                        //------------------------------- add Created Status-------------------------------------
                        string status1 = dr["CreatedStatus"].ToString();
                        if (status1 == "C")
                        {
                            pCheckBox1.Add(new Phrase("CREATE ", fontText));
                            pCheckBox1.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBox1.Add(new Phrase(" MODIFY ", fontText));
                            pCheckBox1.Add(new Phrase(" CANCEL ", fontText));

                        }
                        else if (status1 == "M")
                        {
                            pCheckBox1.Add(new Phrase("CREATE ", fontText));

                            pCheckBox1.Add(new Phrase(" MODIFY ", fontText));
                            pCheckBox1.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBox1.Add(new Phrase(" CANCEL ", fontText));

                        }
                        else if (status1 == "L")
                        {
                            pCheckBox1.Add(new Phrase("CREATE ", fontText));

                            pCheckBox1.Add(new Phrase(" MODIFY ", fontText));

                            pCheckBox1.Add(new Phrase(" CANCEL ", fontText));
                            pCheckBox1.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                        }
                        PdfHeaderCell1 = new PdfPCell(pCheckBox1);
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase("I/We hereby Authorize", fontText));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                        PdfHeaderCell1.Colspan = 4;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);

                        //PdfHeaderCell1 = new PdfPCell(new Phrase(dr["CompanyName"].ToString(), fontAb11B));
                        if (count_IherebyAuthorize < 34)
                        {
                            PdfHeaderCell1 = new PdfPCell(new Phrase(dr["CompanyName"].ToString(), fontAb11B));
                        }
                        else if (count_IherebyAuthorize < 40)
                        { PdfHeaderCell1 = new PdfPCell(new Phrase(dr["CompanyName"].ToString(), fontText)); }
                        else if (count_IherebyAuthorize < 48)
                        {
                            PdfHeaderCell1 = new PdfPCell(new Phrase(dr["CompanyName"].ToString(), fontText6));
                        }
                        else
                        {
                            PdfHeaderCell1 = new PdfPCell(new Phrase(dr["CompanyName"].ToString(), fontText5));
                        }


                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.FixedHeight = 20f;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.Colspan = 10;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfHeaderCell1 = new PdfPCell(new Phrase("To Debit", fontAb11B));
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.Colspan = 5;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        Document documentCheckBoxSB1 = new Document();
                        documentCheckBoxSB1.Open();
                        Paragraph pCheckBoxSB1 = new Paragraph();

                        //----------------------------------add To Debit---------------------------
                        string chDebit1 = dr["DebitTo"].ToString();
                        if (chDebit1 == "SB")
                        {
                            pCheckBoxSB1.Add(new Phrase(" ", fontText));
                            pCheckBoxSB1.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" OTHER ", fontText));
                        }
                        else if (chDebit1 == "CA")
                        {
                            pCheckBoxSB1.Add(new Phrase(" ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" OTHER ", fontText));
                        }

                        else if (chDebit1 == "CC")
                        {
                            pCheckBoxSB1.Add(new Phrase(" ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" OTHER ", fontText));
                        }
                        else if (chDebit1 == "RE")
                        {
                            pCheckBoxSB1.Add(new Phrase(" ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" OTHER ", fontText));
                        }
                        else if (chDebit1 == "RD")
                        {
                            pCheckBoxSB1.Add(new Phrase(" ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" OTHER ", fontText));
                        }
                        else if (chDebit1 == "OT")
                        {
                            pCheckBoxSB1.Add(new Phrase(" ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB1.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB1.Add(new Phrase(" OTHER ", fontText));
                        }
                        PdfHeaderCell1 = new PdfPCell(pCheckBoxSB1);
                        PdfHeaderCell1.NoWrap = false;
                        PdfHeaderCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell1.Colspan = 11;
                        PdfHeaderCell1.HorizontalAlignment = 1;
                        PdfHeaderCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable1.AddCell(PdfHeaderCell1);
                        PdfPTable PdfMidTable1 = new PdfPTable(32);
                        PdfMidTable1.DefaultCell.NoWrap = false;
                        PdfMidTable1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidTable1.DefaultCell.Border = PdfCell.NO_BORDER;
                        PdfMidTable1.WidthPercentage = 100;
                        float[] PdfMidTable1Headerwidths = new float[] { 4f, 3f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
                        PdfMidTable1.SetWidths(PdfMidTable1Headerwidths);
                        PdfPCell PdfMidCell1 = null;
                        PdfMidCell1 = new PdfPCell(new Phrase("Bank a/c Number", fontAb11B));
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 6;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);
                        //---------------------------Add AccountNo-------------------------------------
                        string AccountNo1 = Dbsecurity.Decrypt(dr["AccountNo"].ToString());
                        char[] chrAcountNo1 = new char[Convert.ToInt32(AccountNo1.Length)];
                        chrAcountNo1 = AccountNo1.ToCharArray();

                        if (Convert.ToInt32(chrAcountNo1.Length) <= 26)
                        {
                            for (int j = 0; j < Convert.ToInt32(chrAcountNo1.Length); j++)
                            {
                                PdfMidCell1 = new PdfPCell(new Phrase(chrAcountNo1[j].ToString(), fontA119B));
                                PdfMidCell1.NoWrap = false;
                                PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfMidCell1.HorizontalAlignment = 1;
                                PdfMidTable1.AddCell(PdfMidCell1);
                            }
                            int len1 = 26 - Convert.ToInt32(AccountNo1.Length);
                            for (int k = 0; k < len1; k++)
                            {
                                PdfMidCell1 = new PdfPCell(new Phrase(" ", fontA119B));
                                PdfMidCell1.NoWrap = false;
                                PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfMidCell1.HorizontalAlignment = 1;
                                PdfMidTable1.AddCell(PdfMidCell1);
                            }

                        }
                        PdfMidCell1 = new PdfPCell(new Phrase("With Bank", fontAb11B));
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.FixedHeight = 20f;
                        PdfMidCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfMidTable1.AddCell(PdfMidCell1);


                        // PdfMidCell1 = new PdfPCell(new Phrase(dr["BankName"].ToString(), fontAb11));

                        if (count_BankName < 34)
                        {
                            PdfMidCell1 = new PdfPCell(new Phrase(dr["BankName"].ToString(), fontAb11B));
                        }
                        else if (count_BankName < 40)
                        {
                            PdfMidCell1 = new PdfPCell(new Phrase(dr["BankName"].ToString(), fontText));
                        }
                        else if (count_BankName < 48)
                        {
                            PdfMidCell1 = new PdfPCell(new Phrase(dr["BankName"].ToString(), fontText6));
                        }
                        else
                        {
                            PdfMidCell1 = new PdfPCell(new Phrase(dr["BankName"].ToString(), fontText5));
                        }

                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.FixedHeight = 30f;

                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 6;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfMidTable1.AddCell(PdfMidCell1);


                        PdfMidCell1 = new PdfPCell(new Phrase("IFSC", fontAb11B));
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 2;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfMidTable1.AddCell(PdfMidCell1);
                        //-------------------------Add IFSC code--------------------------------
                        string IFSCcode1 = dr["IFSCcode"].ToString();
                        char[] chrIFSCcode1 = new char[Convert.ToInt32(IFSCcode.Length)];
                        chrIFSCcode1 = IFSCcode1.ToCharArray();
                        if (Convert.ToInt32(chrIFSCcode1.Length) > 0)
                        {
                            if (Convert.ToInt32(chrIFSCcode1.Length) == 11)
                            {
                                for (int j = 0; j < Convert.ToInt32(chrIFSCcode1.Length); j++)
                                {
                                    PdfMidCell1 = new PdfPCell(new Phrase(chrIFSCcode1[j].ToString(), fontA119B));
                                    PdfMidCell1.NoWrap = false;
                                    PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    PdfMidCell1.HorizontalAlignment = 1;
                                    PdfMidCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                    PdfMidTable1.AddCell(PdfMidCell1);
                                }
                            }
                            else
                            {
                                for (int j = 0; j < 11; j++)
                                {
                                    PdfMidCell1 = new PdfPCell(new Phrase(" ", fontA119B));
                                    PdfMidCell1.NoWrap = false;
                                    PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    PdfMidCell1.HorizontalAlignment = 1;
                                    PdfMidCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                    PdfMidTable1.AddCell(PdfMidCell1);
                                }
                            }

                        }
                        else
                        {
                            for (int j = 0; j < 11; j++)
                            {
                                PdfMidCell1 = new PdfPCell(new Phrase(" ", fontA119B));
                                PdfMidCell1.NoWrap = false;
                                PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfMidCell1.HorizontalAlignment = 1;
                                PdfMidCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                PdfMidTable1.AddCell(PdfMidCell1);
                            }
                        }
                        PdfMidCell1 = new PdfPCell(new Phrase("or MICR", fontAb11B));
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 3;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfMidTable1.AddCell(PdfMidCell1);
                        //-------------------------Add MICRcode--------------------------------
                        string MICRcode1 = dr["MICRcode"].ToString();
                        char[] chrMICRcode1 = new char[9];
                        chrMICRcode1 = MICRcode1.ToCharArray();
                        if (true)
                        {
                            if (Convert.ToInt32(chrMICRcode.Length) == 9)
                            {
                                for (int j = 0; j < Convert.ToInt32(chrMICRcode1.Length); j++)
                                {
                                    PdfMidCell1 = new PdfPCell(new Phrase(chrMICRcode1[j].ToString(), fontA119B));
                                    PdfMidCell1.NoWrap = false;
                                    PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    PdfMidCell1.HorizontalAlignment = 1;
                                    PdfMidCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                    PdfMidTable1.AddCell(PdfMidCell1);
                                }
                            }
                            else
                            {
                                for (int j = 0; j < 9; j++)
                                {
                                    PdfMidCell1 = new PdfPCell(new Phrase(" ", fontA119B));
                                    PdfMidCell1.NoWrap = false;
                                    PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    PdfMidCell1.HorizontalAlignment = 1;
                                    PdfMidCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                    PdfMidTable1.AddCell(PdfMidCell1);
                                }
                            }
                        }
                        else
                        {
                            //for (int j = 0; j < 9; j++)
                            //{
                            //    PdfMidCell1 = new PdfPCell(new Phrase(" ", fontA119B));
                            //    PdfMidCell1.NoWrap = false;
                            //    PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                            //    PdfMidCell1.HorizontalAlignment = 1;
                            //    PdfMidCell1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                            //    PdfMidTable1.AddCell(PdfMidCell1);
                            //}
                        }
                        PdfMidCell1 = new PdfPCell(new Phrase("an amount of Rupees", fontAb11B));
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 3;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);
                        PdfMidCell1 = new PdfPCell(new Phrase(PrintHelper.NumberToWords(Convert.ToInt32(Dbsecurity.Decrypt(Convert.ToString(dr["AmountInWord"])))), fontA11B));
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 22;
                        PdfMidCell1.FixedHeight = 10f;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);

                        Document documentAmountInDigit1 = new Document();
                        documentAmountInDigit1.Open();
                        Paragraph pAmountInDigit1 = new Paragraph();
                        pAmountInDigit1.Add(new Chunk(Rupee, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_CENTER));
                        pAmountInDigit1.Add(new Phrase(" " + Dbsecurity.Decrypt(dr["AmountInDigit"].ToString()), fontA119B));
                        PdfMidCell1 = new PdfPCell(pAmountInDigit1);

                        PdfMidCell1.NoWrap = false;

                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 18;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);


                        //Document documentAmountInDigit1 = new Document();
                        //documentAmountInDigit1.Open();
                        //Paragraph pAmountInDigit1 = new Paragraph();
                        //pAmountInDigit1.Add(new Chunk(Rupee, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_CENTER));
                        //pAmountInDigit1.Add(new Phrase(" " + dr["AmountInDigit"].ToString(), fontAb11));
                        //PdfMidCell1 = new PdfPCell(pAmountInDigit1);

                        //PdfMidCell1.NoWrap = false;

                        //PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        //PdfMidCell1.Colspan = 13;

                        //PdfMidTable1.AddCell(PdfMidCell1);
                        string Freq1 = dr["Frequency"].ToString();
                        PdfMidCell1 = new PdfPCell(new Phrase("Frequency", fontAb11B));
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);
                        Document documentMonthly1 = new Document();
                        documentMonthly1.Open();
                        Paragraph pMonthly1 = new Paragraph();
                        //------------------------------- add Monthly-------------------------------------
                        if (Freq1 == "M")
                        {
                            pMonthly1.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pMonthly1.Add(new Phrase("  Monthly ", fontText));
                        }
                        else
                        {
                            pMonthly1.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pMonthly1.Add(new Phrase("  Monthly ", fontText));
                        }

                        PdfMidCell1 = new PdfPCell(pMonthly1);
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 2;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);
                        Document documentQtly1 = new Document();
                        documentQtly1.Open();
                        Paragraph pQtly1 = new Paragraph();
                        //------------------------------- add Qtly-------------------------------------
                        if (Freq1 == "Q")
                        {
                            pQtly1.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pQtly1.Add(new Phrase("  Qtly ", fontText));
                        }
                        else
                        {
                            pQtly1.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pQtly1.Add(new Phrase("  Qtly ", fontText));
                        }
                        PdfMidCell1 = new PdfPCell(pQtly1);
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 2;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);
                        Document documentHYrly1 = new Document();
                        documentHYrly1.Open();
                        Paragraph pHYrly1 = new Paragraph();
                        //------------------------------- add H-Yrly-------------------------------------
                        if (Freq1 == "H")
                        {
                            pHYrly1.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pHYrly1.Add(new Phrase("  H-Yrly ", fontText));
                        }
                        else
                        {
                            pHYrly1.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pHYrly1.Add(new Phrase("  H-Yrly ", fontText));
                        }
                        PdfMidCell1 = new PdfPCell(pHYrly1);
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 3;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);
                        Document documentYearly1 = new Document();
                        documentYearly1.Open();
                        Paragraph pYearly1 = new Paragraph();
                        //------------------------------- add Yearly-------------------------------------
                        if (Freq == "Y")
                        {
                            pYearly1.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pYearly1.Add(new Phrase("  Yearly ", fontText));
                        }
                        else
                        {
                            pYearly1.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pYearly1.Add(new Phrase("  Yearly ", fontText));
                        }

                        PdfMidCell1 = new PdfPCell(pYearly1);
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 3;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);


                        Document prensentedprensented11 = new Document();
                        prensentedprensented11.Open();
                        Paragraph prensented11 = new Paragraph();
                        if (Freq == "A")
                        {
                            prensented11.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            prensented11.Add(new Phrase("  As & when prensented ", fontText));
                        }
                        else
                        {
                            prensented11.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            prensented11.Add(new Phrase("  As & when prensented ", fontText));
                        }

                        PdfMidCell1 = new PdfPCell(prensented11);
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 7;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);


                        string DebitType1 = dr["DebitType"].ToString();
                        PdfMidCell1 = new PdfPCell(new Phrase("Debit Type", fontAb11B));
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 3;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);
                        Document documentFixed1 = new Document();
                        documentFixed1.Open();
                        Paragraph pFixed1 = new Paragraph();
                        //------------------------------- add H-Yrly-------------------------------------
                        if (DebitType1 == "F")
                        {
                            pFixed1.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pFixed1.Add(new Phrase("  Fixed Amount ", fontText));
                        }
                        else
                        {
                            pFixed1.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pFixed1.Add(new Phrase("  Fixed Amount ", fontText));
                        }
                        PdfMidCell1 = new PdfPCell(pFixed1);
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 5;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);
                        Document documentMaximum1 = new Document();
                        documentMaximum1.Open();
                        Paragraph pMaximum1 = new Paragraph();
                        //------------------------------- add H-Yrly-------------------------------------
                        if (DebitType1 == "M")
                        {
                            pMaximum1.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pMaximum1.Add(new Phrase("  Maximum Amount ", fontText));
                        }
                        else
                        {
                            pMaximum1.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pMaximum1.Add(new Phrase("  Maximum Amount ", fontText));
                        }

                        PdfMidCell1 = new PdfPCell(pMaximum1);
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 6;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);
                        PdfMidCell1 = new PdfPCell(new Phrase("Reference 1", fontAb11B));
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);

                        //PdfMidCell1 = new PdfPCell(new Phrase(dr["Reference1"].ToString(), fontAb11B));
                        if (count_Ref1 < 34)
                        {
                            PdfMidCell1 = new PdfPCell(new Phrase(dr["Reference1"].ToString(), fontAb11B));
                        }
                        else if (count_Ref1 < 40)
                        {
                            PdfMidCell1 = new PdfPCell(new Phrase(dr["Reference1"].ToString(), fontText));
                        }
                        else if (count_Ref1 < 48)
                        {
                            PdfMidCell1 = new PdfPCell(new Phrase(dr["Reference1"].ToString(), fontText6));
                        }
                        else
                        {
                            PdfMidCell1 = new PdfPCell(new Phrase(dr["Reference1"].ToString(), fontText5));
                        }

                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 15;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);
                        PdfMidCell1 = new PdfPCell(new Phrase("Phone Number ", fontAb11B));
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 6;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);
                        PdfMidCell1 = new PdfPCell(new Phrase(dr["PhoneNo"].ToString(), fontAb11B));
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 10;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);
                        PdfMidCell1 = new PdfPCell(new Phrase("Reference 2", fontAb11B));
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);

                        //PdfMidCell1 = new PdfPCell(new Phrase(dr["Reference2"].ToString(), fontAb11B));
                        if (count_Ref2 < 34)
                        {
                            PdfMidCell1 = new PdfPCell(new Phrase(dr["Reference2"].ToString(), fontAb11B));
                        }
                        else if (count_Ref2 < 40)
                        { PdfMidCell1 = new PdfPCell(new Phrase(dr["Reference2"].ToString(), fontText)); }
                        else if (count_Ref2 < 48)
                        {
                            PdfMidCell1 = new PdfPCell(new Phrase(dr["Reference2"].ToString(), fontText6));
                        }
                        else
                        {
                            PdfMidCell1 = new PdfPCell(new Phrase(dr["Reference2"].ToString(), fontText5));
                        }

                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 15;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);

                        PdfMidCell1 = new PdfPCell(new Phrase("EMail ID", fontAb11B));
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 6;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);

                        //PdfMidCell1 = new PdfPCell(new Phrase("EMail ID", fontAb11B));

                        if (count < 34)
                        {
                            PdfMidCell1 = new PdfPCell(new Phrase(dr["EmailId"].ToString(), fontAb11B));
                        }
                        else if (count < 40)
                        { PdfMidCell1 = new PdfPCell(new Phrase(dr["EmailId"].ToString(), fontText)); }
                        else if (count < 48)
                        {
                            PdfMidCell1 = new PdfPCell(new Phrase(dr["EmailId"].ToString(), fontText6));
                        }
                        else
                        {
                            PdfMidCell1 = new PdfPCell(new Phrase(dr["EmailId"].ToString(), fontText5));
                        }


                        // PdfMidCell1 = new PdfPCell(new Phrase(dr["EmailId"].ToString(), fontAb11B));
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 10;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);

                        PdfMidCell1 = new PdfPCell(new Phrase("I agree for the debit of mandate processing charges by the bank whom I am authorizing to debit my account as per latest schedule of charges of bank ", fontText));
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.Border = Rectangle.NO_BORDER;
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 32;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);
                        PdfMidCell1 = new PdfPCell(new Phrase("PERIOD", fontAb11B));
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);
                        PdfMidCell1 = new PdfPCell(new Phrase("", fontAb11B));
                        PdfMidCell1.NoWrap = false;
                        PdfMidCell1.Border = Rectangle.NO_BORDER;
                        PdfMidCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell1.Colspan = 31;
                        PdfMidCell1.HorizontalAlignment = 1;
                        PdfMidTable1.AddCell(PdfMidCell1);
                        PdfPTable PdfDetailTable1 = new PdfPTable(34);
                        PdfDetailTable1.DefaultCell.NoWrap = false;
                        PdfDetailTable1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailTable1.DefaultCell.Border = PdfCell.NO_BORDER;
                        PdfDetailTable1.WidthPercentage = 100;
                        float[] PdfDetailTable1Headerwidths1 = new float[] { 4f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 2f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
                        PdfDetailTable1.SetWidths(PdfDetailTable1Headerwidths1);
                        PdfPCell PdfDetailCell1 = null;
                        PdfDetailCell1 = new PdfPCell(new Phrase("From", fontAb11B));
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailTable1.AddCell(PdfDetailCell1);
                        string PeriodFrom1 = Convert.ToDateTime(Dbsecurity.Decrypt(dr["PeriodFrom"].ToString())).ToString("ddMMyyyy");
                        char[] chrPeriodFrom1 = new char[8];
                        chrPeriodFrom1 = PeriodFrom1.ToCharArray();
                        if (Convert.ToInt32(chrPeriodFrom1.Length) > 0)
                        {
                            for (int j = 0; j < Convert.ToInt32(chrPeriodFrom1.Length); j++)
                            {
                                PdfDetailCell1 = new PdfPCell(new Phrase(chrPeriodFrom1[j].ToString(), fontA119B));
                                PdfDetailCell1.NoWrap = false;
                                PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfDetailCell1.HorizontalAlignment = 1;
                                PdfDetailTable1.AddCell(PdfDetailCell1);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                PdfDetailCell1 = new PdfPCell(new Phrase(" ", fontA119B));
                                PdfDetailCell1.NoWrap = false;
                                PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfDetailCell1.HorizontalAlignment = 1;
                                PdfDetailTable1.AddCell(PdfDetailCell1);
                            }
                        }
                        PdfDetailCell1 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailCell1.Border = Rectangle.NO_BORDER;
                        PdfDetailCell1.Colspan = 25;
                        PdfDetailTable1.AddCell(PdfDetailCell1);
                        PdfDetailCell1 = new PdfPCell(new Phrase("To*", fontAb11B));
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailTable1.AddCell(PdfDetailCell1);
                        string PeriodTo1 = "";// dr["PeriodTo"].ToString();
                        try
                        {
                            PeriodTo1 = Convert.ToDateTime(Dbsecurity.Decrypt(Convert.ToString(dr["PeriodTo"]))).ToString("ddMMyyyy");
                        }
                        catch { }
                        char[] chrPeriodTo1 = new char[8];
                        chrPeriodTo1 = PeriodTo1.ToCharArray();
                        if (Convert.ToInt32(chrPeriodTo1.Length) > 0)
                        {
                            if (PeriodTo1 != "01011900")
                            {
                                for (int j = 0; j < Convert.ToInt32(chrPeriodTo.Length); j++)
                                {
                                    PdfDetailCell1 = new PdfPCell(new Phrase(chrPeriodTo[j].ToString(), fontA119B));
                                    PdfDetailCell1.NoWrap = false;
                                    PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    PdfDetailCell1.HorizontalAlignment = 1;
                                    PdfDetailTable1.AddCell(PdfDetailCell1);
                                }
                            }
                            else
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    PdfDetailCell1 = new PdfPCell(new Phrase(" ", fontA119B));
                                    PdfDetailCell1.NoWrap = false;
                                    //PdfDetailCell1.BackgroundColor = new Color(0, 0, 0);
                                    PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    PdfDetailCell1.HorizontalAlignment = 1;
                                    PdfDetailTable1.AddCell(PdfDetailCell1);
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                PdfDetailCell1 = new PdfPCell(new Phrase(" ", fontA119B));
                                PdfDetailCell1.NoWrap = false;
                                //PdfDetailCell1.BackgroundColor = new Color(0, 0, 0);
                                PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfDetailCell1.HorizontalAlignment = 1;
                                PdfDetailTable1.AddCell(PdfDetailCell1);
                            }
                        }
                        PdfDetailCell1 = new PdfPCell(new Phrase(" ", fontAb11B));
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailCell1.Border = Rectangle.NO_BORDER;
                        PdfDetailCell1.Colspan = 25;
                        PdfDetailTable1.AddCell(PdfDetailCell1);
                        PdfDetailCell1 = new PdfPCell(new Phrase("Or", fontAb11B));
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.BackgroundColor = new Color(252, 252, 252);
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailTable1.AddCell(PdfDetailCell1);

                        Document documentCheckBox1234 = new Document();
                        documentCheckBox1234.Open();
                        Paragraph pCheckBox1234 = new Paragraph();
                        if (PeriodTo1 == "01011900")
                        {
                            pCheckBox1234.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                        }
                        else
                        {
                            pCheckBox1234.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                        }

                        pCheckBox1234.Add(new Phrase(" Until Cancelled ", fontAb11));


                        PdfDetailCell1 = new PdfPCell(pCheckBox1234);

                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.Colspan = 8;
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailTable1.AddCell(PdfDetailCell1);


                        PdfDetailCell1 = new PdfPCell(new Phrase("Sign. Primary Acc. Holder", fontAb11BU));
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailCell1.Border = Rectangle.NO_BORDER;
                        PdfDetailCell1.Colspan = 8;
                        PdfDetailTable1.AddCell(PdfDetailCell1);
                        PdfDetailCell1 = new PdfPCell(new Phrase("Sign Acc. Holder", fontAb11BU));
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailCell1.Border = Rectangle.NO_BORDER;
                        PdfDetailCell1.Colspan = 8;
                        PdfDetailTable1.AddCell(PdfDetailCell1);
                        PdfDetailCell1 = new PdfPCell(new Phrase("Sign Acc. Holder", fontAb11BU));
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailCell1.Border = Rectangle.NO_BORDER;
                        PdfDetailCell1.Colspan = 9;
                        PdfDetailTable1.AddCell(PdfDetailCell1);

                        //PdfDetailCell1 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfDetailCell1.NoWrap = false;
                        //PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        //PdfDetailCell1.HorizontalAlignment = 1;
                        //PdfDetailCell1.Border = Rectangle.NO_BORDER;

                        //PdfDetailCell1.Colspan = 34;
                        //PdfDetailTable1.AddCell(PdfDetailCell1);



                        PdfDetailCell1 = new PdfPCell(new Phrase(" ", fontAb11B));
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailCell1.Border = Rectangle.NO_BORDER;

                        PdfDetailCell1.Colspan = 5;
                        PdfDetailTable1.AddCell(PdfDetailCell1);

                        //PdfDetailCell1 = new PdfPCell(new Phrase(dr["BenificiaryName"].ToString(), fontAbCutomer));
                        if (count_Customer < 10)
                        {
                            PdfDetailCell1 = new PdfPCell(new Phrase(dr["BenificiaryName"].ToString(), fontAbCutomer));//7
                        }
                        else if (count_Customer < 20)
                        {
                            PdfDetailCell1 = new PdfPCell(new Phrase(dr["BenificiaryName"].ToString(), fontText6));//6
                        }
                        else if (count_Customer < 30)
                        {
                            PdfDetailCell1 = new PdfPCell(new Phrase(dr["BenificiaryName"].ToString(), fontText55));//5
                        }
                        else
                        {
                            PdfDetailCell1 = new PdfPCell(new Phrase(dr["BenificiaryName"].ToString(), fontText44));//4
                        }
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailCell1.Border = Rectangle.NO_BORDER;

                        PdfDetailCell1.Colspan = 13;
                        PdfDetailTable1.AddCell(PdfDetailCell1);

                        //PdfDetailCell1 = new PdfPCell(new Phrase(dr["Customer2"].ToString(), fontAbCutomer));
                        if (count_Customer < 10)
                        {
                            PdfDetailCell1 = new PdfPCell(new Phrase(dr["Customer2"].ToString(), fontAbCutomer));//7
                        }
                        else if (count_Customer < 20)
                        {
                            PdfDetailCell1 = new PdfPCell(new Phrase(dr["Customer2"].ToString(), fontText6));//6
                        }
                        else if (count_Customer < 30)
                        {
                            PdfDetailCell1 = new PdfPCell(new Phrase(dr["Customer2"].ToString(), fontText55));//5
                        }
                        else
                        {
                            PdfDetailCell1 = new PdfPCell(new Phrase(dr["Customer2"].ToString(), fontText44));//4
                        }
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailCell1.Border = Rectangle.NO_BORDER;

                        PdfDetailCell1.Colspan = 10;
                        PdfDetailTable1.AddCell(PdfDetailCell1);

                        //PdfDetailCell1 = new PdfPCell(new Phrase(dr["Customer3"].ToString(), fontAbCutomer));
                        if (count_Customer < 10)
                        {
                            PdfDetailCell1 = new PdfPCell(new Phrase(dr["Customer3"].ToString(), fontAbCutomer));//7
                        }
                        else if (count_Customer < 20)
                        {
                            PdfDetailCell1 = new PdfPCell(new Phrase(dr["Customer3"].ToString(), fontText6));//6
                        }
                        else if (count_Customer < 30)
                        {
                            PdfDetailCell1 = new PdfPCell(new Phrase(dr["Customer3"].ToString(), fontText55));//5
                        }
                        else
                        {
                            PdfDetailCell1 = new PdfPCell(new Phrase(dr["Customer3"].ToString(), fontText44));//4
                        }
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailCell1.Border = Rectangle.NO_BORDER;

                        PdfDetailCell1.Colspan = 6;
                        PdfDetailTable1.AddCell(PdfDetailCell1);


                        PdfDetailCell1 = new PdfPCell(new Phrase(" ", fontAb11B));
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailCell1.Border = Rectangle.NO_BORDER;

                        PdfDetailCell1.Colspan = 9;
                        PdfDetailTable1.AddCell(PdfDetailCell1);

                        PdfDetailCell1 = new PdfPCell(new Phrase("1.Name as in Bank Records", fontAb11BU));
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailCell1.Border = Rectangle.NO_BORDER;

                        PdfDetailCell1.Colspan = 8;
                        PdfDetailTable1.AddCell(PdfDetailCell1);

                        PdfDetailCell1 = new PdfPCell(new Phrase("2.Name as in Bank Records", fontAb11BU));
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailCell1.Border = Rectangle.NO_BORDER;

                        PdfDetailCell1.Colspan = 8;
                        PdfDetailTable1.AddCell(PdfDetailCell1);

                        PdfDetailCell1 = new PdfPCell(new Phrase("3.Name as in Bank Records", fontAb11BU));
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailCell1.Border = Rectangle.NO_BORDER;

                        PdfDetailCell1.Colspan = 9;
                        PdfDetailTable1.AddCell(PdfDetailCell1);


                        PdfDetailCell1 = new PdfPCell(new Phrase(" ", fontText4));
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.HorizontalAlignment = 1;
                        PdfDetailCell1.Border = Rectangle.NO_BORDER;

                        PdfDetailCell1.Colspan = 34;
                        PdfDetailTable1.AddCell(PdfDetailCell1);


                        PdfDetailCell1 = new PdfPCell(new Phrase("This is to confirm that declaration has been carefully read, understood & made by me/us. I'm authorizing the user entity/Corporate to debit my account, based on the instruction as agreed and signed by me. I've understood that I'm authorized to cancel/amend this mandate by appropriately communicating the cancellation/amendment request to the user/entity/corporate or the bank where I've authorized the debit.", fontText5));
                        PdfDetailCell1.NoWrap = false;
                        PdfDetailCell1.Border = Rectangle.NO_BORDER;
                        PdfDetailCell1.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell1.Colspan = 34;

                        PdfDetailTable1.AddCell(PdfDetailCell1);

                        PdfPTable PdfHeaderTable2 = new PdfPTable(31);
                        PdfHeaderTable2.DefaultCell.NoWrap = false;
                        PdfHeaderTable2.SpacingBefore = 0;
                        PdfHeaderTable2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderTable2.DefaultCell.Border = PdfCell.NO_BORDER;
                        PdfHeaderTable2.WidthPercentage = 100;
                        float[] PdfHeaderTable2Headerwidths = new float[] { 4f, 3f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 2f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
                        PdfHeaderTable2.SetWidths(PdfHeaderTable2Headerwidths);




                        PdfPCell PdfHeaderCell2 = null;

                        PdfHeaderCell2 = new PdfPCell(new Phrase("Status:Success/Failure/Response Awaited", fontAb9));
                        PdfHeaderCell2.NoWrap = false;
                        PdfHeaderCell2.Colspan = 34;
                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;



                        Document document2 = new Document();
                        document2.Open();
                        Paragraph p2 = new Paragraph();
                        p2.Add(new Chunk(LogoImage, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                        PdfHeaderCell2 = new PdfPCell(p2);
                        PdfHeaderCell2.FixedHeight = 25f;
                        PdfHeaderCell2.Rowspan = 2;


                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);

                        PdfHeaderCell2 = new PdfPCell(new Phrase("UMRN", fontAb11B));
                        PdfHeaderCell2.NoWrap = false;
                        PdfHeaderCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;




                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);

                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);



                        PdfHeaderCell2 = new PdfPCell(new Phrase("Date", fontAb11B));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);

                        string Date2 = dr["SlipDate"].ToString();
                        char[] chars2 = new char[8];
                        chars2 = Date2.ToCharArray();
                        if (Convert.ToInt32(chars2.Length) > 0)
                        {
                            for (int j = 0; j < Convert.ToInt32(chars2.Length); j++)
                            {
                                PdfHeaderCell2 = new PdfPCell(new Phrase(chars2[j].ToString(), fontA119B));
                                PdfHeaderCell2.NoWrap = false;
                                PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfHeaderCell2.HorizontalAlignment = 1;
                                PdfHeaderTable2.AddCell(PdfHeaderCell2);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                PdfHeaderCell2 = new PdfPCell(new Phrase(" ", fontA119B));
                                PdfHeaderCell2.NoWrap = false;
                                PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfHeaderCell2.HorizontalAlignment = 1;
                                PdfHeaderTable2.AddCell(PdfHeaderCell2);
                            }
                        }


                        //PdfHeaderCell2 = new PdfPCell(new Phrase("d", fontAb11));
                        //PdfHeaderCell2.NoWrap = false;

                        //PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfHeaderCell2.HorizontalAlignment = 1;
                        //PdfHeaderTable2.AddCell(PdfHeaderCell2);

                        //PdfHeaderCell2 = new PdfPCell(new Phrase("d", fontAb11));
                        //PdfHeaderCell2.NoWrap = false;

                        //PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfHeaderCell2.HorizontalAlignment = 1;
                        //PdfHeaderTable2.AddCell(PdfHeaderCell2);

                        //PdfHeaderCell2 = new PdfPCell(new Phrase("m", fontAb11));
                        //PdfHeaderCell2.NoWrap = false;

                        //PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfHeaderCell2.HorizontalAlignment = 1;
                        //PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        //PdfHeaderCell2 = new PdfPCell(new Phrase("m", fontAb11));
                        //PdfHeaderCell2.NoWrap = false;

                        //PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfHeaderCell2.HorizontalAlignment = 1;
                        //PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        //PdfHeaderCell2 = new PdfPCell(new Phrase("y", fontAb11));
                        //PdfHeaderCell2.NoWrap = false;

                        //PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfHeaderCell2.HorizontalAlignment = 1;
                        //PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        //PdfHeaderCell2 = new PdfPCell(new Phrase("y", fontAb11));
                        //PdfHeaderCell2.NoWrap = false;

                        //PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfHeaderCell2.HorizontalAlignment = 1;
                        //PdfHeaderTable2.AddCell(PdfHeaderCell2);

                        //PdfHeaderCell2 = new PdfPCell(new Phrase("y", fontAb11));
                        //PdfHeaderCell2.NoWrap = false;

                        //PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfHeaderCell2.HorizontalAlignment = 1;
                        //PdfHeaderTable2.AddCell(PdfHeaderCell2);
                        //PdfHeaderCell2 = new PdfPCell(new Phrase("y", fontAb11));
                        //PdfHeaderCell2.NoWrap = false;

                        //PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfHeaderCell2.HorizontalAlignment = 1;
                        //PdfHeaderTable2.AddCell(PdfHeaderCell2);


                        PdfHeaderCell2 = new PdfPCell(new Phrase("Sponsor bankcode", fontAb11B));
                        PdfHeaderCell2.NoWrap = false;
                        PdfHeaderCell2.Colspan = 4;
                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);


                        PdfHeaderCell2 = new PdfPCell(new Phrase(dr["SponserBankCode"].ToString(), fontAb11B));
                        PdfHeaderCell2.NoWrap = false;
                        PdfHeaderCell2.Colspan = 12;
                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);


                        PdfHeaderCell2 = new PdfPCell(new Phrase("Utility Code", fontAb11B));
                        PdfHeaderCell2.NoWrap = false;
                        PdfHeaderCell2.Colspan = 6;
                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);


                        PdfHeaderCell2 = new PdfPCell(new Phrase(dr["UtilityCode"].ToString(), fontAb11B));
                        PdfHeaderCell2.NoWrap = false;
                        PdfHeaderCell2.Colspan = 8;
                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;

                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);


                        //PdfHeaderCell2 = new PdfPCell(new Phrase("CREATE MODIFY CANCEL", fontAb11B));
                        //PdfHeaderCell2.NoWrap = false;

                        //PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfHeaderCell2.HorizontalAlignment = 1;
                        //PdfHeaderTable2.AddCell(PdfHeaderCell2);

                        Document documentCheckBox2 = new Document();
                        documentCheckBox2.Open();
                        Paragraph pCheckBox2 = new Paragraph();
                        //------------------------------- add Created Status-------------------------------------
                        string status2 = dr["CreatedStatus"].ToString();
                        if (status2 == "C")
                        {
                            pCheckBox2.Add(new Phrase("CREATE ", fontText));
                            pCheckBox2.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBox2.Add(new Phrase(" MODIFY ", fontText));
                            pCheckBox2.Add(new Phrase(" CANCEL ", fontText));

                        }
                        else if (status2 == "M")
                        {
                            pCheckBox2.Add(new Phrase("CREATE ", fontText));

                            pCheckBox2.Add(new Phrase(" MODIFY ", fontText));
                            pCheckBox2.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBox2.Add(new Phrase(" CANCEL ", fontText));

                        }
                        else if (status2 == "L")
                        {
                            pCheckBox2.Add(new Phrase("CREATE ", fontText));

                            pCheckBox2.Add(new Phrase(" MODIFY ", fontText));

                            pCheckBox2.Add(new Phrase(" CANCEL ", fontText));
                            pCheckBox2.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                        }

                        //pCheckBox.Add(new Phrase("CREATE ", fontAb11B));
                        //pCheckBox.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                        //pCheckBox.Add(new Phrase(" MODIFY ", fontAb11B));
                        //pCheckBox.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                        //pCheckBox.Add(new Phrase(" CANCEL ", fontAb11B));ss
                        //pCheckBox.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                        PdfHeaderCell2 = new PdfPCell(pCheckBox2);
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);

                        PdfHeaderCell2 = new PdfPCell(new Phrase("I/We hereby Authorize", fontText));
                        PdfHeaderCell2.NoWrap = false;
                        PdfHeaderCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
                        PdfHeaderCell2.Colspan = 4;
                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);


                        //PdfHeaderCell2 = new PdfPCell(new Phrase(dr["CompanyName"].ToString(), fontAb11B));
                        if (count_IherebyAuthorize < 34)
                        {
                            PdfHeaderCell2 = new PdfPCell(new Phrase(dr["CompanyName"].ToString(), fontAb11B));
                        }
                        else if (count_IherebyAuthorize < 40)
                        { PdfHeaderCell2 = new PdfPCell(new Phrase(dr["CompanyName"].ToString(), fontText)); }
                        else if (count_IherebyAuthorize < 48)
                        {
                            PdfHeaderCell2 = new PdfPCell(new Phrase(dr["CompanyName"].ToString(), fontText6));
                        }
                        else
                        {
                            PdfHeaderCell2 = new PdfPCell(new Phrase(dr["CompanyName"].ToString(), fontText5));
                        }


                        PdfHeaderCell2.NoWrap = false;
                        PdfHeaderCell2.FixedHeight = 20f;
                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell2.Colspan = 10;
                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);


                        PdfHeaderCell2 = new PdfPCell(new Phrase("To Debit", fontAb11B));
                        PdfHeaderCell2.NoWrap = false;
                        PdfHeaderCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell2.Colspan = 5;
                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);

                        Document documentCheckBoxSB2 = new Document();
                        documentCheckBoxSB2.Open();
                        Paragraph pCheckBoxSB2 = new Paragraph();

                        //----------------------------------add To Debit---------------------------
                        string chDebit2 = dr["DebitTo"].ToString();
                        if (chDebit1 == "SB")
                        {
                            pCheckBoxSB2.Add(new Phrase(" ", fontText));
                            pCheckBoxSB2.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" OTHER ", fontText));
                        }
                        else if (chDebit1 == "CA")
                        {
                            pCheckBoxSB2.Add(new Phrase(" ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" OTHER ", fontText));
                        }

                        else if (chDebit1 == "CC")
                        {
                            pCheckBoxSB2.Add(new Phrase(" ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" OTHER ", fontText));
                        }
                        else if (chDebit1 == "RE")
                        {
                            pCheckBoxSB2.Add(new Phrase(" ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" OTHER ", fontText));
                        }
                        else if (chDebit1 == "RD")
                        {
                            pCheckBoxSB2.Add(new Phrase(" ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" OTHER ", fontText));
                        }
                        else if (chDebit1 == "OT")
                        {
                            pCheckBoxSB2.Add(new Phrase(" ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" CA/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" CC/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB-NRE/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(SmallcheckBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" SB-NRO/ ", fontText));
                            pCheckBoxSB2.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pCheckBoxSB2.Add(new Phrase(" OTHER ", fontText));
                        }
                        //pCheckBoxSB.Add(new Phrase("SB ", fontAb11B));
                        //pCheckBoxSB.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));

                        // pCheckBoxSB.Add(new Phrase(" /CA/CC/SB-NRE/SB-NRO/OTHER", fontAb11B));
                        PdfHeaderCell2 = new PdfPCell(pCheckBoxSB2);



                        //  PdfHeaderCell2 = new PdfPCell(new Phrase("SB/CA/CC/SB-NRE/SB-NRO/OTHER", fontAb11B));
                        PdfHeaderCell2.NoWrap = false;

                        PdfHeaderCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfHeaderCell2.Colspan = 11;
                        PdfHeaderCell2.HorizontalAlignment = 1;
                        PdfHeaderCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfHeaderTable2.AddCell(PdfHeaderCell2);


                        PdfPTable PdfMidTable2 = new PdfPTable(32);
                        PdfMidTable2.DefaultCell.NoWrap = false;

                        PdfMidTable2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfMidTable2.DefaultCell.Border = PdfCell.NO_BORDER;
                        PdfMidTable2.WidthPercentage = 100;
                        float[] PdfMidTable2Headerwidths = new float[] { 4f, 3f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
                        PdfMidTable2.SetWidths(PdfMidTable2Headerwidths);




                        PdfPCell PdfMidCell2 = null;






                        PdfMidCell2 = new PdfPCell(new Phrase("Bank a/c Number", fontAb11B));
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 6;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);

                        string AccountNo2 = Dbsecurity.Decrypt(dr["AccountNo"].ToString());
                        char[] chrAcountNo2 = new char[Convert.ToInt32(AccountNo2.Length)];
                        chrAcountNo2 = AccountNo2.ToCharArray();
                        if (Convert.ToInt32(AccountNo2.Length) <= 26)
                        {
                            for (int j = 0; j < Convert.ToInt32(chrAcountNo2.Length); j++)
                            {
                                PdfMidCell2 = new PdfPCell(new Phrase(chrAcountNo2[j].ToString(), fontA119B));
                                PdfMidCell2.NoWrap = false;
                                PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfMidCell2.HorizontalAlignment = 1;
                                PdfMidTable2.AddCell(PdfMidCell2);
                            }
                            int len2 = 26 - Convert.ToInt32(AccountNo2.Length);
                            for (int k = 0; k < len2; k++)
                            {
                                PdfMidCell2 = new PdfPCell(new Phrase(" ", fontA119B));
                                PdfMidCell2.NoWrap = false;
                                PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfMidCell2.HorizontalAlignment = 1;
                                PdfMidTable2.AddCell(PdfMidCell2);
                            }
                        }





                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);



                        PdfMidCell2 = new PdfPCell(new Phrase("With Bank", fontAb11B));
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.FixedHeight = 20f;
                        PdfMidCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfMidTable2.AddCell(PdfMidCell2);


                        char[] array = (Convert.ToString(dr["BankName"]).Trim()).ToCharArray();
                        //if (array.Length < 22)
                        //{
                        // PdfMidCell2 = new PdfPCell(new Phrase(dr["BankName"].ToString(), fontAb11));
                        if (count_BankName < 34)
                        {
                            PdfMidCell2 = new PdfPCell(new Phrase(dr["BankName"].ToString(), fontAb11B));
                        }
                        else if (count_BankName < 40)
                        {
                            PdfMidCell2 = new PdfPCell(new Phrase(dr["BankName"].ToString(), fontText));
                        }
                        else if (count_BankName < 48)
                        {
                            PdfMidCell2 = new PdfPCell(new Phrase(dr["BankName"].ToString(), fontText6));
                        }
                        else
                        {
                            PdfMidCell2 = new PdfPCell(new Phrase(dr["BankName"].ToString(), fontText5));
                        }
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.FixedHeight = 30f;
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 6;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfMidTable2.AddCell(PdfMidCell2);
                        //}
                        //else
                        //{
                        //    PdfMidCell2 = new PdfPCell(new Phrase(dr["BankName"].ToString(), fontText));
                        //    PdfMidCell2.NoWrap = false;

                        //    PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        //    PdfMidCell2.Colspan = 6;
                        //    PdfMidCell2.HorizontalAlignment = 1;
                        //    PdfMidTable2.AddCell(PdfMidCell2);
                        //}


                        PdfMidCell2 = new PdfPCell(new Phrase("IFSC", fontAb11B));
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 2;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfMidTable2.AddCell(PdfMidCell2);


                        //-------------------------Add IFSC code--------------------------------
                        string IFSCcode2 = dr["IFSCcode"].ToString();
                        char[] chrIFSCcode2 = new char[Convert.ToInt32(IFSCcode.Length)];
                        chrIFSCcode2 = IFSCcode2.ToCharArray();
                        if (Convert.ToInt32(chrIFSCcode2.Length) > 0)
                        {
                            if (Convert.ToInt32(chrIFSCcode2.Length) == 11)
                            {
                                for (int j = 0; j < Convert.ToInt32(chrIFSCcode2.Length); j++)
                                {
                                    PdfMidCell2 = new PdfPCell(new Phrase(chrIFSCcode2[j].ToString(), fontA119B));
                                    PdfMidCell2.NoWrap = false;
                                    PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    PdfMidCell2.HorizontalAlignment = 1;
                                    PdfMidCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                    PdfMidTable2.AddCell(PdfMidCell2);
                                }
                            }
                            else
                            {
                                for (int j = 0; j < 11; j++)
                                {
                                    PdfMidCell2 = new PdfPCell(new Phrase(" ", fontA119B));
                                    PdfMidCell2.NoWrap = false;
                                    PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    PdfMidCell2.HorizontalAlignment = 1;
                                    PdfMidCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                    PdfMidTable2.AddCell(PdfMidCell2);
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 11; j++)
                            {
                                PdfMidCell2 = new PdfPCell(new Phrase(" ", fontA119B));
                                PdfMidCell2.NoWrap = false;
                                PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfMidCell2.HorizontalAlignment = 1;
                                PdfMidCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                PdfMidTable2.AddCell(PdfMidCell2);
                            }
                        }



                        //PdfMidCell2 = new PdfPCell(new Phrase("x", fontAb11));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase("x", fontAb11));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase("x", fontAb11));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase("x", fontAb11));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase("x", fontAb11));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase("x", fontAb11));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase("x", fontAb11));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase("x", fontAb11));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase("x", fontAb11));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);



                        PdfMidCell2 = new PdfPCell(new Phrase("or MICR", fontAb11B));
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 3;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                        PdfMidTable2.AddCell(PdfMidCell2);


                        //-------------------------Add MICRcode--------------------------------
                        string MICRcode2 = dr["MICRcode"].ToString();
                        char[] chrMICRcode2 = new char[9];
                        chrMICRcode2 = MICRcode2.ToCharArray();
                        if (Convert.ToInt32(chrMICRcode2.Length) > 0)
                        {
                            if (Convert.ToInt32(chrMICRcode.Length) == 9)
                            {
                                for (int j = 0; j < Convert.ToInt32(chrMICRcode2.Length); j++)
                                {
                                    PdfMidCell2 = new PdfPCell(new Phrase(chrMICRcode2[j].ToString(), fontA119B));
                                    PdfMidCell2.NoWrap = false;
                                    PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    PdfMidCell2.HorizontalAlignment = 1;
                                    PdfMidCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                    PdfMidTable2.AddCell(PdfMidCell2);
                                }
                            }
                            else
                            {
                                for (int j = 0; j < 9; j++)
                                {
                                    PdfMidCell2 = new PdfPCell(new Phrase(" ", fontA119B));
                                    PdfMidCell2.NoWrap = false;
                                    PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    PdfMidCell2.HorizontalAlignment = 1;
                                    PdfMidCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                    PdfMidTable2.AddCell(PdfMidCell2);
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 9; j++)
                            {
                                PdfMidCell2 = new PdfPCell(new Phrase(" ", fontA119B));
                                PdfMidCell2.NoWrap = false;
                                PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfMidCell2.HorizontalAlignment = 1;
                                PdfMidCell2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE;
                                PdfMidTable2.AddCell(PdfMidCell2);
                            }
                        }



                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);
                        //PdfMidCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);



                        PdfMidCell2 = new PdfPCell(new Phrase("an amount of Rupees", fontAb11B));
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 3;
                        PdfMidCell2.HorizontalAlignment = 1;

                        PdfMidTable2.AddCell(PdfMidCell2);


                        PdfMidCell2 = new PdfPCell(new Phrase(PrintHelper.NumberToWords(Convert.ToInt32(Dbsecurity.Decrypt(Convert.ToString(dr["AmountInWord"])))), fontA11B));
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.FixedHeight = 10f;
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 22;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);

                        Document documentAmountInDigit2 = new Document();
                        documentAmountInDigit2.Open();
                        Paragraph pAmountInDigit2 = new Paragraph();
                        pAmountInDigit2.Add(new Chunk(Rupee, PdfPCell.ALIGN_CENTER, PdfPCell.ALIGN_CENTER));
                        pAmountInDigit2.Add(new Phrase(" " + Dbsecurity.Decrypt(dr["AmountInDigit"].ToString()), fontA119B));
                        PdfMidCell2 = new PdfPCell(pAmountInDigit2);

                        PdfMidCell2.NoWrap = false;

                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 18;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);


                        string Freq2 = dr["Frequency"].ToString();

                        PdfMidCell2 = new PdfPCell(new Phrase("Frequency", fontAb11B));
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);


                        Document documentMonthly2 = new Document();
                        documentMonthly2.Open();
                        Paragraph pMonthly2 = new Paragraph();
                        //------------------------------- add Monthly-------------------------------------
                        if (Freq2 == "M")
                        {
                            pMonthly2.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pMonthly2.Add(new Phrase("  Monthly ", fontText));
                        }
                        else
                        {
                            pMonthly2.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pMonthly2.Add(new Phrase("  Monthly ", fontText));
                        }

                        PdfMidCell2 = new PdfPCell(pMonthly2);

                        // PdfMidCell1 = new PdfPCell(new Phrase("Monthly", fontAb11));
                        PdfMidCell2.NoWrap = false;

                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 2;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);

                        Document documentQtly2 = new Document();
                        documentQtly2.Open();
                        Paragraph pQtly2 = new Paragraph();
                        //------------------------------- add Qtly-------------------------------------
                        if (Freq2 == "Q")
                        {
                            pQtly2.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pQtly2.Add(new Phrase("  Qtly ", fontText));
                        }
                        else
                        {
                            pQtly2.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pQtly2.Add(new Phrase("  Qtly ", fontText));
                        }

                        PdfMidCell2 = new PdfPCell(pQtly2);

                        // PdfMidCell1 = new PdfPCell(new Phrase("Qtly", fontAb11B));

                        PdfMidCell2.NoWrap = false;

                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 2;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);

                        Document documentHYrly2 = new Document();
                        documentHYrly2.Open();
                        Paragraph pHYrly2 = new Paragraph();
                        //------------------------------- add H-Yrly-------------------------------------
                        if (Freq2 == "H")
                        {
                            pHYrly2.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pHYrly2.Add(new Phrase("  H-Yrly ", fontText));
                        }
                        else
                        {
                            pHYrly2.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pHYrly2.Add(new Phrase("  H-Yrly ", fontText));
                        }

                        PdfMidCell2 = new PdfPCell(pHYrly2);

                        // PdfMidCell1 = new PdfPCell(new Phrase("H-Yrly", fontAb11B));
                        PdfMidCell2.NoWrap = false;

                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 3;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);

                        Document documentYearly2 = new Document();
                        documentYearly2.Open();
                        Paragraph pYearly2 = new Paragraph();
                        //------------------------------- add Yearly-------------------------------------
                        if (Freq2 == "Y")
                        {
                            pYearly2.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pYearly2.Add(new Phrase("  Yearly ", fontText));
                        }
                        else
                        {
                            pYearly2.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pYearly2.Add(new Phrase("  Yearly ", fontText));
                        }

                        PdfMidCell2 = new PdfPCell(pYearly2);

                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 3;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);


                        Document prensentedprensented22 = new Document();
                        prensentedprensented22.Open();
                        Paragraph prensented22 = new Paragraph();
                        if (Freq == "A")
                        {
                            prensented22.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            prensented22.Add(new Phrase("  As & when prensented ", fontText));
                        }
                        else
                        {
                            prensented22.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            prensented22.Add(new Phrase("  As & when prensented ", fontText));
                        }

                        PdfMidCell2 = new PdfPCell(prensented22);
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 7;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);


                        //PdfMidCell2 = new PdfPCell(new Phrase("As & when prensented", fontAb11B));
                        //PdfMidCell2.NoWrap = false;

                        //PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        //PdfMidCell2.Colspan = 6;
                        //PdfMidCell2.HorizontalAlignment = 1;
                        //PdfMidTable2.AddCell(PdfMidCell2);

                        string DebitType2 = dr["DebitType"].ToString();

                        PdfMidCell2 = new PdfPCell(new Phrase("Debit Type", fontAb11B));
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 3;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);

                        Document documentFixed2 = new Document();
                        documentFixed2.Open();
                        Paragraph pFixed2 = new Paragraph();
                        //------------------------------- add H-Yrly-------------------------------------
                        if (DebitType2 == "F")
                        {
                            pFixed2.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pFixed2.Add(new Phrase("  Fixed Amount ", fontText));
                        }
                        else
                        {
                            pFixed2.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pFixed2.Add(new Phrase("  Fixed Amount ", fontText));
                        }

                        PdfMidCell2 = new PdfPCell(pFixed2);
                        //   PdfMidCell1 = new PdfPCell(new Phrase("Fixed Amount", fontAb11B));
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 5;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);


                        Document documentMaximum2 = new Document();
                        documentMaximum2.Open();
                        Paragraph pMaximum2 = new Paragraph();
                        //------------------------------- add H-Yrly-------------------------------------
                        if (DebitType2 == "M")
                        {
                            pMaximum2.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pMaximum2.Add(new Phrase("  Maximum Amount ", fontText));
                        }
                        else
                        {
                            pMaximum2.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                            pMaximum2.Add(new Phrase("  Maximum Amount ", fontText));
                        }

                        PdfMidCell2 = new PdfPCell(pMaximum2);
                        // PdfMidCell2 = new PdfPCell(new Phrase("Maximum Amount", fontAb11));
                        PdfMidCell2.NoWrap = false;

                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 6;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);

                        PdfMidCell2 = new PdfPCell(new Phrase("Reference 1", fontAb11B));
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);


                        //PdfMidCell2 = new PdfPCell(new Phrase(dr["Reference1"].ToString(), fontAb11B));
                        if (count_Ref1 < 34)
                        {
                            PdfMidCell2 = new PdfPCell(new Phrase(dr["Reference1"].ToString(), fontAb11B));
                        }
                        else if (count_Ref1 < 40)
                        {
                            PdfMidCell2 = new PdfPCell(new Phrase(dr["Reference1"].ToString(), fontText));
                        }
                        else if (count_Ref1 < 48)
                        {
                            PdfMidCell2 = new PdfPCell(new Phrase(dr["Reference1"].ToString(), fontText6));
                        }
                        else
                        {
                            PdfMidCell2 = new PdfPCell(new Phrase(dr["Reference1"].ToString(), fontText5));
                        }


                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 15;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);


                        PdfMidCell2 = new PdfPCell(new Phrase("Phone Number ", fontAb11B));
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 6;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);

                        PdfMidCell2 = new PdfPCell(new Phrase(dr["PhoneNo"].ToString(), fontAb11B));
                        PdfMidCell2.NoWrap = false;

                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 10;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);


                        PdfMidCell2 = new PdfPCell(new Phrase("Reference 2", fontAb11B));
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);


                        //    PdfMidCell2 = new PdfPCell(new Phrase(dr["Reference2"].ToString(), fontAb11B));
                        if (count_Ref2 < 34)
                        {
                            PdfMidCell2 = new PdfPCell(new Phrase(dr["Reference2"].ToString(), fontAb11B));
                        }
                        else if (count_Ref2 < 40)
                        { PdfMidCell2 = new PdfPCell(new Phrase(dr["Reference2"].ToString(), fontText)); }
                        else if (count_Ref2 < 48)
                        {
                            PdfMidCell2 = new PdfPCell(new Phrase(dr["Reference2"].ToString(), fontText6));
                        }
                        else
                        {
                            PdfMidCell2 = new PdfPCell(new Phrase(dr["Reference2"].ToString(), fontText5));
                        }

                        PdfMidCell2.NoWrap = false;

                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 15;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);


                        PdfMidCell2 = new PdfPCell(new Phrase("EMail ID", fontAb11B));
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 6;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);

                        //  PdfMidCell2 = new PdfPCell(new Phrase(dr["EmailId"].ToString(), fontAb11B));
                        if (count < 34)
                        {
                            PdfMidCell2 = new PdfPCell(new Phrase(dr["EmailId"].ToString(), fontAb11B));
                        }
                        else if (count < 40)
                        { PdfMidCell2 = new PdfPCell(new Phrase(dr["EmailId"].ToString(), fontText)); }
                        else if (count < 48)
                        {
                            PdfMidCell2 = new PdfPCell(new Phrase(dr["EmailId"].ToString(), fontText6));
                        }
                        else
                        {
                            PdfMidCell2 = new PdfPCell(new Phrase(dr["EmailId"].ToString(), fontText5));
                        }

                        PdfMidCell2.NoWrap = false;

                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 10;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);


                        PdfMidCell2 = new PdfPCell(new Phrase("I agree for the debit of mandate processing charges by the bank whom I am authorizing to debit my account as per latest schedule of charges of bank ", fontText));
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.Border = Rectangle.NO_BORDER;
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 32;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);


                        PdfMidCell2 = new PdfPCell(new Phrase("PERIOD", fontAb11B));
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);

                        PdfMidCell2 = new PdfPCell(new Phrase("", fontAb11B));
                        PdfMidCell2.NoWrap = false;
                        PdfMidCell2.Border = Rectangle.NO_BORDER;
                        PdfMidCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfMidCell2.Colspan = 31;
                        PdfMidCell2.HorizontalAlignment = 1;
                        PdfMidTable2.AddCell(PdfMidCell2);



                        PdfPTable PdfDetailTable2 = new PdfPTable(34);
                        PdfDetailTable2.DefaultCell.NoWrap = false;

                        PdfDetailTable2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;

                        PdfDetailTable2.DefaultCell.Border = PdfCell.NO_BORDER;
                        PdfDetailTable2.WidthPercentage = 100;
                        float[] PdfDetailTable2Headerwidths1 = new float[] { 4f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 2f, 1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f };
                        PdfDetailTable2.SetWidths(PdfDetailTable2Headerwidths1);




                        PdfPCell PdfDetailCell2 = null;

                        PdfDetailCell2 = new PdfPCell(new Phrase("From", fontAb11B));
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailTable2.AddCell(PdfDetailCell2);

                        string PeriodFrom2 = Convert.ToDateTime(Dbsecurity.Decrypt(dr["PeriodFrom"].ToString())).ToString("ddMMyyyy");
                        char[] chrPeriodFrom2 = new char[8];
                        chrPeriodFrom2 = PeriodFrom2.ToCharArray();
                        if (Convert.ToInt32(chrPeriodFrom2.Length) > 0)
                        {
                            for (int j = 0; j < Convert.ToInt32(chrPeriodFrom2.Length); j++)
                            {
                                PdfDetailCell2 = new PdfPCell(new Phrase(chrPeriodFrom2[j].ToString(), fontA119B));
                                PdfDetailCell2.NoWrap = false;
                                PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfDetailCell2.HorizontalAlignment = 1;
                                PdfDetailTable2.AddCell(PdfDetailCell2);
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                PdfDetailCell2 = new PdfPCell(new Phrase(" ", fontA119B));
                                PdfDetailCell2.NoWrap = false;
                                PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfDetailCell2.HorizontalAlignment = 1;
                                PdfDetailTable2.AddCell(PdfDetailCell2);
                            }
                        }


                        //PdfDetailCell2 = new PdfPCell(new Phrase("x", fontAb11));
                        //PdfDetailCell2.NoWrap = false;
                        //PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        //PdfDetailCell2.HorizontalAlignment = 1;
                        //PdfDetailTable2.AddCell(PdfDetailCell2);
                        //PdfDetailCell2 = new PdfPCell(new Phrase("x", fontAb11));
                        //PdfDetailCell2.NoWrap = false;
                        //PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        //PdfDetailCell2.HorizontalAlignment = 1;
                        //PdfDetailTable2.AddCell(PdfDetailCell2);
                        //PdfDetailCell2 = new PdfPCell(new Phrase("x", fontAb11));
                        //PdfDetailCell2.NoWrap = false;
                        //PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        //PdfDetailCell2.HorizontalAlignment = 1;
                        //PdfDetailTable2.AddCell(PdfDetailCell2);
                        //PdfDetailCell2 = new PdfPCell(new Phrase("x", fontAb11));
                        //PdfDetailCell2.NoWrap = false;
                        //PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        //PdfDetailCell2.HorizontalAlignment = 1;
                        //PdfDetailTable2.AddCell(PdfDetailCell2);
                        //PdfDetailCell2 = new PdfPCell(new Phrase("x", fontAb11));
                        //PdfDetailCell2.NoWrap = false;
                        //PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        //PdfDetailCell2.HorizontalAlignment = 1;
                        //PdfDetailTable2.AddCell(PdfDetailCell2);
                        //PdfDetailCell2 = new PdfPCell(new Phrase("x", fontAb11));
                        //PdfDetailCell2.NoWrap = false;
                        //PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        //PdfDetailCell2.HorizontalAlignment = 1;
                        //PdfDetailTable2.AddCell(PdfDetailCell2);
                        //PdfDetailCell2 = new PdfPCell(new Phrase("x", fontAb11));
                        //PdfDetailCell2.NoWrap = false;
                        //PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        //PdfDetailCell2.HorizontalAlignment = 1;
                        //PdfDetailTable2.AddCell(PdfDetailCell2);

                        //PdfDetailCell2 = new PdfPCell(new Phrase("x", fontAb11));
                        //PdfDetailCell2.NoWrap = false;
                        //PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        //PdfDetailCell2.HorizontalAlignment = 1;
                        //PdfDetailTable2.AddCell(PdfDetailCell2);

                        PdfDetailCell2 = new PdfPCell(new Phrase(" ", fontAb11));
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailCell2.Border = Rectangle.NO_BORDER;

                        PdfDetailCell2.Colspan = 25;
                        PdfDetailTable2.AddCell(PdfDetailCell2);




                        PdfDetailCell2 = new PdfPCell(new Phrase("To*", fontAb11B));
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailTable2.AddCell(PdfDetailCell2);

                        string PeriodTo2 = "";// dr["PeriodTo"].ToString();
                        try
                        {
                            PeriodTo2 = Convert.ToDateTime(Dbsecurity.Decrypt(Convert.ToString(dr["PeriodTo"]))).ToString("ddMMyyyy");
                        }
                        catch { }
                        char[] chrPeriodTo2 = new char[8];
                        chrPeriodTo2 = PeriodTo2.ToCharArray();
                        if (Convert.ToInt32(chrPeriodTo2.Length) > 0)
                        {
                            if (PeriodTo2 != "01011900")
                            {
                                for (int j = 0; j < Convert.ToInt32(chrPeriodTo.Length); j++)
                                {
                                    PdfDetailCell2 = new PdfPCell(new Phrase(chrPeriodTo[j].ToString(), fontA119B));
                                    PdfDetailCell2.NoWrap = false;
                                    PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    PdfDetailCell2.HorizontalAlignment = 1;
                                    PdfDetailTable2.AddCell(PdfDetailCell2);
                                }
                            }
                            else
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    PdfDetailCell2 = new PdfPCell(new Phrase(" ", fontA119B));
                                    PdfDetailCell2.NoWrap = false;
                                    //PdfDetailCell2.BackgroundColor = new Color(0, 0, 0);
                                    PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                    PdfDetailCell2.HorizontalAlignment = 1;
                                    PdfDetailTable2.AddCell(PdfDetailCell2);
                                }
                            }
                        }
                        else
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                PdfDetailCell2 = new PdfPCell(new Phrase(" ", fontA119B));
                                PdfDetailCell2.NoWrap = false;
                                //PdfDetailCell2.Colspan = 8;
                                //PdfDetailCell2.BackgroundColor = new Color(0, 0, 0);
                                PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                                PdfDetailCell2.HorizontalAlignment = 1;
                                PdfDetailTable2.AddCell(PdfDetailCell2);
                            }
                        }


                        //PdfDetailCell2 = new PdfPCell(new Phrase("", fontAb11B));
                        //PdfDetailCell2.NoWrap = false;
                        //PdfDetailCell2.Colspan = 8;
                        //PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        //PdfDetailCell2.HorizontalAlignment = 1;
                        //PdfDetailTable2.AddCell(PdfDetailCell2);

                        PdfDetailCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailCell2.Border = Rectangle.NO_BORDER;

                        PdfDetailCell2.Colspan = 25;
                        PdfDetailTable2.AddCell(PdfDetailCell2);


                        PdfDetailCell2 = new PdfPCell(new Phrase("Or", fontAb11B));
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.BackgroundColor = new Color(252, 252, 252);
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailTable2.AddCell(PdfDetailCell2);

                        Document documentCheckBox1235 = new Document();
                        documentCheckBox1235.Open();
                        Paragraph pCheckBox1235 = new Paragraph();
                        if (PeriodTo2 == "01011900")
                        {
                            pCheckBox1235.Add(new Chunk(checkBox, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                        }
                        else
                        {
                            pCheckBox1235.Add(new Chunk(Box, PdfPCell.ALIGN_LEFT, PdfPCell.ALIGN_LEFT));
                        }

                        pCheckBox1235.Add(new Phrase(" Until Cancelled ", fontAb11));


                        PdfDetailCell2 = new PdfPCell(pCheckBox1235);


                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.Colspan = 8;
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailTable2.AddCell(PdfDetailCell2);


                        PdfDetailCell2 = new PdfPCell(new Phrase("Sign. Primary Acc. Holder", fontAb11BU));
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailCell2.Border = Rectangle.NO_BORDER;

                        PdfDetailCell2.Colspan = 8;
                        PdfDetailTable2.AddCell(PdfDetailCell2);

                        PdfDetailCell2 = new PdfPCell(new Phrase("Sign Acc. Holder", fontAb11BU));
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailCell2.Border = Rectangle.NO_BORDER;

                        PdfDetailCell2.Colspan = 8;
                        PdfDetailTable2.AddCell(PdfDetailCell2);

                        PdfDetailCell2 = new PdfPCell(new Phrase("Sign Acc. Holder", fontAb11BU));
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailCell2.Border = Rectangle.NO_BORDER;

                        PdfDetailCell2.Colspan = 9;
                        PdfDetailTable2.AddCell(PdfDetailCell2);

                        //PdfDetailCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        //PdfDetailCell2.NoWrap = false;
                        //PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        //PdfDetailCell2.HorizontalAlignment = 1;
                        //PdfDetailCell2.Border = Rectangle.NO_BORDER;

                        //PdfDetailCell2.Colspan = 34;
                        //PdfDetailTable2.AddCell(PdfDetailCell2);

                        PdfDetailCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailCell2.Border = Rectangle.NO_BORDER;

                        PdfDetailCell2.Colspan = 5;
                        PdfDetailTable2.AddCell(PdfDetailCell2);

                        //PdfDetailCell2 = new PdfPCell(new Phrase(dr["BenificiaryName"].ToString(), fontAbCutomer));
                        if (count_Customer < 10)
                        {
                            PdfDetailCell2 = new PdfPCell(new Phrase(dr["BenificiaryName"].ToString(), fontAbCutomer));//7
                        }
                        else if (count_Customer < 20)
                        {
                            PdfDetailCell2 = new PdfPCell(new Phrase(dr["BenificiaryName"].ToString(), fontText6));//6
                        }
                        else if (count_Customer < 30)
                        {
                            PdfDetailCell2 = new PdfPCell(new Phrase(dr["BenificiaryName"].ToString(), fontText55));//5
                        }
                        else
                        {
                            PdfDetailCell2 = new PdfPCell(new Phrase(dr["BenificiaryName"].ToString(), fontText44));//4
                        }
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailCell2.Border = Rectangle.NO_BORDER;

                        PdfDetailCell2.Colspan = 13;
                        PdfDetailTable2.AddCell(PdfDetailCell2);

                        //PdfDetailCell2 = new PdfPCell(new Phrase(dr["Customer2"].ToString(), fontAbCutomer));
                        if (count_Customer < 10)
                        {
                            PdfDetailCell2 = new PdfPCell(new Phrase(dr["Customer2"].ToString(), fontAbCutomer));//7
                        }
                        else if (count_Customer < 20)
                        {
                            PdfDetailCell2 = new PdfPCell(new Phrase(dr["Customer2"].ToString(), fontText6));//6
                        }
                        else if (count_Customer < 30)
                        {
                            PdfDetailCell2 = new PdfPCell(new Phrase(dr["Customer2"].ToString(), fontText55));//5
                        }
                        else
                        {
                            PdfDetailCell2 = new PdfPCell(new Phrase(dr["Customer2"].ToString(), fontText44));//4
                        }
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailCell2.Border = Rectangle.NO_BORDER;

                        PdfDetailCell2.Colspan = 10;
                        PdfDetailTable2.AddCell(PdfDetailCell2);

                        //PdfDetailCell2 = new PdfPCell(new Phrase(dr["Customer3"].ToString(), fontAbCutomer));
                        if (count_Customer < 10)
                        {
                            PdfDetailCell2 = new PdfPCell(new Phrase(dr["Customer3"].ToString(), fontAbCutomer));//7
                        }
                        else if (count_Customer < 20)
                        {
                            PdfDetailCell2 = new PdfPCell(new Phrase(dr["Customer3"].ToString(), fontText6));//6
                        }
                        else if (count_Customer < 30)
                        {
                            PdfDetailCell2 = new PdfPCell(new Phrase(dr["Customer3"].ToString(), fontText55));//5
                        }
                        else
                        {
                            PdfDetailCell2 = new PdfPCell(new Phrase(dr["Customer3"].ToString(), fontText44));//4
                        }
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailCell2.Border = Rectangle.NO_BORDER;

                        PdfDetailCell2.Colspan = 6;
                        PdfDetailTable2.AddCell(PdfDetailCell2);


                        PdfDetailCell2 = new PdfPCell(new Phrase(" ", fontAb11B));
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailCell2.Border = Rectangle.NO_BORDER;

                        PdfDetailCell2.Colspan = 9;
                        PdfDetailTable2.AddCell(PdfDetailCell2);

                        PdfDetailCell2 = new PdfPCell(new Phrase("1.Name as in Bank Records", fontAb11BU));
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailCell2.Border = Rectangle.NO_BORDER;

                        PdfDetailCell2.Colspan = 8;
                        PdfDetailTable2.AddCell(PdfDetailCell2);

                        PdfDetailCell2 = new PdfPCell(new Phrase("2.Name as in Bank Records", fontAb11BU));
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailCell2.Border = Rectangle.NO_BORDER;

                        PdfDetailCell2.Colspan = 8;
                        PdfDetailTable2.AddCell(PdfDetailCell2);

                        PdfDetailCell2 = new PdfPCell(new Phrase("3.Name as in Bank Records", fontAb11BU));
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailCell2.Border = Rectangle.NO_BORDER;

                        PdfDetailCell2.Colspan = 9;
                        PdfDetailTable2.AddCell(PdfDetailCell2);


                        PdfDetailCell2 = new PdfPCell(new Phrase(" ", fontText4));
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.HorizontalAlignment = 1;
                        PdfDetailCell2.Border = Rectangle.NO_BORDER;

                        PdfDetailCell2.Colspan = 34;
                        PdfDetailTable2.AddCell(PdfDetailCell2);


                        PdfDetailCell2 = new PdfPCell(new Phrase("This is to confirm that declaration has been carefully read, understood & made by me/us. I'm authorizing the user entity/Corporate to debit my account, based on the instruction as agreed and signed by me. I've understood that I'm authorized to cancel/amend this mandate by appropriately communicating the cancellation/amendment request to the user/entity/corporate or the bank where I've authorized the debit.", fontText5));
                        PdfDetailCell2.NoWrap = false;
                        PdfDetailCell2.Border = Rectangle.NO_BORDER;
                        PdfDetailCell2.RunDirection = PdfWriter.RUN_DIRECTION_LTR;
                        PdfDetailCell2.Colspan = 34;

                        PdfDetailTable2.AddCell(PdfDetailCell2);



                        Document pdfDoc = new Document(PageSize.A4, 20f, 20f, 10f, 10f);
                        MemoryStream memoryStream = new MemoryStream();
                        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, memoryStream);
                        //PdfWriter.GetInstance(pdfDoc, new FileStream("" + Convert.ToString(dr["Reference1"]) + ".pdf", FileMode.Create));

                        pdfDoc.Open();
                        //Response.ContentType = "Application/pdf";
                        //Response.AppendHeader("content-disposition", "attachment;filename=" + Convert.ToString(dr["Reference1"]) + ".pdf");
                        pdfDoc.Add(PdfHeaderTable);
                        pdfDoc.Add(PdfMidTable);
                        pdfDoc.Add(PdfDetailTable);
                        pdfDoc.Add(CutterImage);
                        pdfDoc.Add(PdfHeaderTable1);
                        pdfDoc.Add(PdfMidTable1);
                        pdfDoc.Add(PdfDetailTable1);
                        pdfDoc.Add(CutterImage);
                        pdfDoc.Add(PdfHeaderTable2);
                        pdfDoc.Add(PdfMidTable2);
                        pdfDoc.Add(PdfDetailTable2);
                        pdfDoc.Close();
                        PDFBase64 = Convert.ToBase64String(memoryStream.ToArray());
                        //Response.Buffer = true;
                        //Response.ContentType = "application/pdf";
                        //Response.AddHeader("content-disposition", "attachment;filename=" + Convert.ToString(dr["Reference1"]) + ".pdf");
                        //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        //Response.Write(pdfDoc);
                        //Response.End();

                    }
                }
            }
            catch (Exception ex)
            {
                //BusinessLibrary1.RaiseError(ex.Message);
            }
            return PDFBase64;
        }
    }
}