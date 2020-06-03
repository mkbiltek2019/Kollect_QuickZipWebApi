using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityDAL;
using Encryptions;
using UploadMandate.Models;
using System.IO;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using BusinessLibrary;
using System.Diagnostics;
using System.Threading;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using ZXing;
using ZXing.Common;
namespace UploadMandate.Controllers
{
    public class Scanandupload
    {
        QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
        Globalconvertion base64image = new Globalconvertion();
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern int DeleteDC(IntPtr hdc);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern int BitBlt(IntPtr hdcDst, int xDst, int yDst, int w, int h, IntPtr hdcSrc, int xSrc, int ySrc, int rop);
        static int SRCCOPY = 0x00CC0020;
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        static extern IntPtr CreateDIBSection(IntPtr hdc, ref BITMAPINFO bmi, uint Usage, out IntPtr bits, IntPtr hSection, uint dwOffset);
        static uint BI_RGB = 0;
        static uint DIB_RGB_COLORS = 0;
        string finalresult = "";
        int newDoc = 0;
        int cmbCMIndex = 1;
        Boolean Greater;
        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct BITMAPINFO
        {
            public uint biSize;
            public int biWidth, biHeight;
            public short biPlanes, biBitCount;
            public uint biCompression, biSizeImage;
            public int biXPelsPerMeter, biYPelsPerMeter;
            public uint biClrUsed, biClrImportant;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 256)]
            public uint[] cols;
            //public uint cols;
        }
        static uint MAKERGB(int r, int g, int b)
        {
            return ((uint)(b & 255)) | ((uint)((r & 255) << 8)) | ((uint)((g & 255) << 16));
        }
        static System.Drawing.Bitmap CopyToBpp(System.Drawing.Bitmap b, int bpp)
        {
            if (bpp != 1 && bpp != 8) throw new System.ArgumentException("1 or 8", "bpp");
            int w = b.Width, h = b.Height;
            IntPtr hbm = b.GetHbitmap();
            BITMAPINFO bmi = new BITMAPINFO();
            bmi.biSize = 40;
            bmi.biWidth = w;
            bmi.biHeight = h;
            bmi.biPlanes = 1;
            bmi.biBitCount = (short)bpp;
            bmi.biCompression = BI_RGB;
            bmi.biSizeImage = (uint)(((w + 7) & 0xFFFFFFF8) * h / 8);
            bmi.biXPelsPerMeter = 1000000;
            bmi.biYPelsPerMeter = 1000000;
            // Now for the colour table.

            uint ncols = (uint)1 << bpp;
            bmi.biClrUsed = ncols;
            bmi.biClrImportant = ncols;
            bmi.cols = new uint[256];
            if (bpp == 1) { bmi.cols[0] = MAKERGB(0, 0, 0); bmi.cols[1] = MAKERGB(255, 255, 255); }
            else { for (int i = 0; i < ncols; i++) bmi.cols[i] = MAKERGB(i, i, i); }
            IntPtr bits0; IntPtr hbm0 = CreateDIBSection(IntPtr.Zero, ref bmi, DIB_RGB_COLORS, out bits0, IntPtr.Zero, 0);
            IntPtr sdc = GetDC(IntPtr.Zero);
            IntPtr hdc = CreateCompatibleDC(sdc); SelectObject(hdc, hbm);
            IntPtr hdc0 = CreateCompatibleDC(sdc); SelectObject(hdc0, hbm0);
            BitBlt(hdc0, 0, 0, w, h, hdc, 0, 0, SRCCOPY);
            System.Drawing.Bitmap b0 = System.Drawing.Bitmap.FromHbitmap(hbm0);
            DeleteDC(hdc);
            DeleteDC(hdc0);
            ReleaseDC(IntPtr.Zero, sdc);
            DeleteObject(hbm);
            DeleteObject(hbm0);
            return b0;
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        public IEnumerable<ResponseFlag> UploadCropImage(UploadMandateModel context)
        {
            List<ResponseFlag> common = new List<ResponseFlag>();

            ResponseFlag Flag = new ResponseFlag();

            string UserId = Dbsecurity.Decrypt(context.UserId);
            string ID = Convert.ToString(Dbsecurity.Decrypt(context.MdtID));
            string No = Convert.ToString(Dbsecurity.Decrypt(context.RefrenceNo));
            string APPId = Convert.ToString(Dbsecurity.Decrypt(context.AppID));
            string extension = string.Empty;
            string JPGFilepath = string.Empty;
            byte[] bytes = System.Convert.FromBase64String(context.ScannedImage);
                extension = context.Extention;
                if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif" || extension == ".bmp")
                {
                    string fileName1 = ConfigurationManager.AppSettings["DownloadFileName"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + No + ".jpg";
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/FullMandate/" + APPId + "/" + ID)))
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/FullMandate/" + APPId + "/" + ID));
                    System.IO.DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/FullMandate/" + APPId + "/" + ID));
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                    string targetPath = HttpContext.Current.Server.MapPath("~/FullMandate/" + APPId + "/" + ID + @"\" + fileName1);
                    using (System.Drawing.Image image = new Bitmap(new MemoryStream(bytes)))
                    {
                        image.Save(targetPath, ImageFormat.Png);
                    }

                }
                bool Flag1 = Directory.Exists(HttpContext.Current.Server.MapPath("~/MandateFile/" + APPId + "/" + ID));
                if (!Flag1)
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/MandateFile/" + APPId + "/" + ID));
                }
                else
                {
                    System.IO.DirectoryInfo di = new DirectoryInfo(HttpContext.Current.Server.MapPath("~/MandateFile/" + APPId + "/" + ID));
                    foreach (FileInfo file in di.GetFiles())
                    {
                        file.Delete();
                    }
                }
                string croppedFileName = string.Empty;
                string croppedFilePath = string.Empty;

                string fileName = ConfigurationManager.AppSettings["DownloadFileName"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + No + ".jpg";
                string filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/FullMandate/"), ConfigurationManager.AppSettings["DownloadFileName" + context.AppID].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + No + ".jpg");
                if (File.Exists(filePath))
                {
                    System.Drawing.Image orgImg = System.Drawing.Image.FromFile(filePath);
                    Rectangle areaToCrop = new Rectangle(Convert.ToInt32(0),
                        Convert.ToInt32(0),
                        Convert.ToInt32(orgImg.Width),
                        Convert.ToInt32(orgImg.Height));

                    if (orgImg.Width > orgImg.Height)
                    {
                        Greater = true;
                    }
                    else
                    {
                        Greater = false;
                    }
                    try
                    {
                        Bitmap bitMap = new Bitmap(orgImg.Width, orgImg.Height, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
                        using (Graphics g = Graphics.FromImage(bitMap))
                        {
                            //Draw image to screen
                            g.DrawImage(orgImg, new Rectangle(0, 0, bitMap.Width, bitMap.Height), areaToCrop, GraphicsUnit.Pixel);
                            g.CompositingQuality = CompositingQuality.HighQuality;
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        }

                        bitMap.SetResolution(96, 96);
                    croppedFileName = ConfigurationManager.AppSettings["DownloadFileName" + APPId].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + No + ".jpg";
                    //Create path to store the cropped image
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/CropImage/" + APPId + "/" + ID)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/CropImage/" + APPId + "/" + ID));
                    }
                    croppedFilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/CropImage/" + APPId + "/" + ID), croppedFileName);
                    bitMap.Save(croppedFilePath);
                    var CropImagePath = Path.Combine(HttpContext.Current.Server.MapPath("~/CropImage/" + APPId + "/" + ID), ConfigurationManager.AppSettings["DownloadFileName" + APPId].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + No + ".jpg");
                    System.Drawing.Image CropImage = System.Drawing.Image.FromFile(CropImagePath);
                    using (var image = CropImage)
                        {
                            int newWidth = 0;
                            int newHeight = 0;
                            if (Greater == true)
                            {
                                newWidth = 827; // New Width of Image in Pixel  
                                newHeight = 356;
                                //newWidth = 4000; // New Width of Image in Pixel  
                                //newHeight = 1700;
                            }
                            else
                            {
                                newWidth = 356;
                                newHeight = 827;
                                //newWidth = 1700;
                                //newHeight = 4000;
                            }

                            var thumbImg = new Bitmap(newWidth, newHeight, System.Drawing.Imaging.PixelFormat.Format16bppRgb555);
                            var thumbGraph = Graphics.FromImage(thumbImg);
                            var imgRectangle = new Rectangle(0, 0, newWidth, newHeight);

                            thumbGraph.DrawImage(image, imgRectangle);
                            thumbImg.SetResolution(100, 100);

                            System.Drawing.Bitmap b0 = CopyToBpp(thumbImg, 8);
                            b0.SetResolution(100, 100);
                        JPGFilepath = "../MandateFile/" + APPId + "/" + ID + "/" + croppedFileName;
                        croppedFileName = ConfigurationManager.AppSettings["DownloadFileName"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + No + ".jpg";
                        croppedFilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/CropImage/" + APPId + "/" + ID), croppedFileName);
                        ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                            System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Quality;
                            EncoderParameters myEncoderParameters = new EncoderParameters(1);
                            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
                            myEncoderParameters.Param[0] = myEncoderParameter;
                            b0.Save(croppedFilePath, jpgEncoder, myEncoderParameters);
                        }
                    var CropImagePath1 = Path.Combine(HttpContext.Current.Server.MapPath("~/CropImage/" + APPId + "/" + ID), ConfigurationManager.AppSettings["DownloadFileName" + APPId].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + No + ".jpg");
                    System.Drawing.Image CropImage1 = System.Drawing.Image.FromFile(CropImagePath1);
                    bool result = true;
                    using (var image1 = CropImage1)
                    {
                        
                        int newWidth = 0;
                        int newHeight = 0;
                        if (Greater == true)
                        {
                            newWidth = CropImage1.Width;
                            newHeight = CropImage1.Height;
                            //newWidth = 2800;
                            //newHeight = 1200;
                        }
                        else
                        {
                            newWidth = CropImage1.Width;
                            newHeight = CropImage1.Height;
                            //newWidth = 1200;
                            //newHeight = 2800;
                        }
                        var thumbImg1 = new Bitmap(newWidth, newHeight);
                        var thumbGraph1 = Graphics.FromImage(thumbImg1);
                        var imgRectangle1 = new Rectangle(0, 0, newWidth, newHeight);
                        thumbGraph1.DrawImage(image1, imgRectangle1);
                        System.Drawing.Bitmap b1 = CopyToBpp(thumbImg1, 1);
                        b1.SetResolution(200, 200);
                        croppedFileName = ConfigurationManager.AppSettings["DownloadFileName"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + No + ".jpg";
                        croppedFilePath = Path.Combine(HttpContext.Current.Server.MapPath("~/CropImage/" + APPId + "/" + ID), croppedFileName);

                        ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Tiff);
                        System.Drawing.Imaging.Encoder myEncoder = System.Drawing.Imaging.Encoder.Compression;
                        EncoderParameters myEncoderParameters = new EncoderParameters(1);
                        EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder,
                                (long)EncoderValue.CompressionCCITT4);
                        myEncoderParameters.Param[0] = myEncoderParameter;
                        b1.Save(croppedFilePath, jpgEncoder, myEncoderParameters);
                        List<Checklogo> dataList = new List<Checklogo>();
                        var Result2 = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<Checklogo>().Execute("@QueryType", "@MandateId", "CheckLogo", ID);
                        dataList = Result2.FirstOrDefault().Cast<Checklogo>().ToList();
                        string isQR = dataList.Cast<Checklogo>().ToList().Select(x => x.PrintQR).First().ToString();
                        if (isQR == "True")
                        {
                            result = (Convert.ToString(ID + "_" + No) == GetMandatefromQR(croppedFilePath, ID)) ? true : false;
                        }
                    }
                    orgImg.Dispose();
                    bitMap = null;
                    if (File.Exists(CropImagePath1))
                    {
                        File.Delete(CropImagePath1);
                    }
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/FullMandate/" + APPId + "/" + ID)))
                    {
                        Directory.Delete(HttpContext.Current.Server.MapPath("~/FullMandate/" + APPId + "/" + ID));
                    }
                    if (Directory.Exists(HttpContext.Current.Server.MapPath("~/CropImage/" + APPId + "/" + ID)))
                    {
                        Directory.Delete(HttpContext.Current.Server.MapPath("~/CropImage/" + APPId + "/" + ID));
                    }
                    if (result)
                    {
                        Flag.Flag = "1";
                        Flag.FlagValue = "Image cropped and saved successfully";
                        string Fullmandatepath = Path.Combine(HttpContext.Current.Server.MapPath("~/MandateFile/" + APPId + "/" + ID), ConfigurationManager.AppSettings["DownloadFileName"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + No + ".jpg");
                        Flag.Base64image = base64image.ConvertImageBase64(Fullmandatepath);
                        common.Add(Flag);
                        QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                        List<GetLogo> dataList1 = new List<GetLogo>();
                        var Result2 = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<GetLogo>().Execute("@QueryType", "@TIPPath", "@PNGPath", "@MandateId", "@UserId", "UpdatePNGTIP",
                        Convert.ToString(filePath), Convert.ToString(JPGFilepath), Convert.ToString(ID), UserId);
                    }
                    else
                    {
                        Flag.Flag = "0";
                        Flag.FlagValue = "Invalid Mandate. Please Re-Upload Valid Mandate";
                        common.Add(Flag);

                        if (File.Exists(HttpContext.Current.Server.MapPath("~/MandateFile/" + APPId + "/" + ID + "/" + ConfigurationManager.AppSettings["DownloadFileName"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + No + ".tif")))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath("~/MandateFile/" + APPId + "/" + ID + "/" + ConfigurationManager.AppSettings["DownloadFileName"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + No + ".tif"));
                        }

                        if (File.Exists(HttpContext.Current.Server.MapPath("~/MandateFile/" + APPId + "/" + ID + "/" + ConfigurationManager.AppSettings["DownloadFileName"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + No + ".jpg")))
                        {
                            File.Delete(HttpContext.Current.Server.MapPath("~/MandateFile/" + APPId + "/" + ID + "/" + ConfigurationManager.AppSettings["DownloadFileName"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + No + ".jpg"));
                        }

                        List<GetLogo> dataList1 = new List<GetLogo>();
                        QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                        var Result2 = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<GetLogo>().Execute("@QueryType", "@TIPPath", "@PNGPath", "@MandateId", "@UserId", "UpdatePNGTIP",
                        null, null, ID, UserId);
                        if (Directory.Exists(HttpContext.Current.Server.MapPath("~/MandateFile/" + APPId + "/" + ID)))
                        {
                            Directory.Delete(HttpContext.Current.Server.MapPath("~/MandateFile/" + APPId + "/" + ID));
                        }
                    }

                }
                    catch (Exception ex)
                {
                    Flag.Flag = "0";
                    Flag.FlagValue = "Oops!! error occured : " + ex.Message.ToString();
                    common.Add(Flag);

                }
                finally
                {
                    fileName = string.Empty;
                    filePath = string.Empty;
                    croppedFileName = string.Empty;
                    croppedFilePath = string.Empty;
                }
            }
                    return common;


                }
        public IEnumerable<ResponseFlag> FinalUploadCropImage(UploadMandateModel context)
        {
            List<ResponseFlag> common = new List<ResponseFlag>();
            ResponseFlag Flag = new ResponseFlag();
            try
            {
                string croppedFileName = string.Empty;
                string filePath = string.Empty;
                string ID = Convert.ToString(Dbsecurity.Decrypt(context.MdtID));
                string No = Convert.ToString(Dbsecurity.Decrypt(context.RefrenceNo));
                string APPId = Convert.ToString(Dbsecurity.Decrypt(context.AppID));
                string UserId = Convert.ToString(Dbsecurity.Decrypt(context.UserId));
                string JPGFilepath = string.Empty;
                croppedFileName = ConfigurationManager.AppSettings["DownloadFileName" + APPId].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + No + ".tif";
                filePath = "../MandateFile/" + APPId + "/" + ID + "/" + croppedFileName;
                string MandateId = ID;

                if (File.Exists(HttpContext.Current.Server.MapPath("~/MandateFile/" + APPId + "/" + MandateId + "/" + ConfigurationManager.AppSettings["DownloadFileName"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + Convert.ToString(No) + ".tif")) && File.Exists(HttpContext.Current.Server.MapPath("~/MandateFile/" + APPId + "/" + MandateId + "/" + ConfigurationManager.AppSettings["DownloadFileName"].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + Convert.ToString(No) + ".jpg")))
                {
                    croppedFileName = ConfigurationManager.AppSettings["DownloadFileName" + APPId].ToString() + "_" + DateTime.Now.ToString("ddMMyyyy") + "_" + No + ".jpg";
                    JPGFilepath = "../MandateFile/" + APPId + "/" + ID + "/" + croppedFileName;
                    QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                    List<GetLogo> dataList1 = new List<GetLogo>();
                    var Result2 = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<GetLogo>().Execute("@QueryType", "@TIPPath", "@PNGPath", "@MandateId", "@UserId", "UpdatePNGTIP",
                    Convert.ToString(filePath), Convert.ToString(JPGFilepath), Convert.ToString(ID), Convert.ToString(UserId));
                    Flag.Flag = "1";
                    Flag.FlagValue = "Mandate uploaded successfully";
                    common.Add(Flag);
                }
                else
                {
                    QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                    List<GetLogo> dataList1 = new List<GetLogo>();
                    var Result2 = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<GetLogo>().Execute("@QueryType", "@TIPPath", "@PNGPath", "@MandateId", "@UserId", "UpdatePNGTIP",
                    null, null, ID, UserId);
                    Flag.Flag = "1";
                    Flag.FlagValue = "Image is not Properly Uploaded,Please Upload again";
                    common.Add(Flag);

                }
                // }
            }
            catch (Exception ex)
            {
                Flag.Flag = "1";
                Flag.FlagValue = "Image is not Properly Uploaded,Please Upload again";
                common.Add(Flag);
            }


            return common;
        }
        





        public static string GetMandatefromQR(string ImagePath, string MandateID)
        {
            try
            {

                byte[] imageArray = File.ReadAllBytes(ImagePath);
                string base64ImageRepresentation = Convert.ToBase64String(imageArray);
                //string apiUrl = "http://localhost:49979/api/GetMandateStatus/ReadQrCode";
                string apiUrl = ConfigurationManager.AppSettings["ScanAPIUrl"];
                string base64Image = MandateID + "_" + base64ImageRepresentation;



                var input = new
                {
                    //MandateID = MandateID,
                    //base64Image = base64ImageRepresentation
                    base64Image = base64Image//MandateID + "_" + base64ImageRepresentation
                };
                //string inputJson = (new JavaScriptSerializer()).Serialize(input);
                string inputJson = base64Image;
                WebClient client = new WebClient();
                //client.Headers["Content-type"] = "application/json";
                client.Headers["Content-type"] = "text/plain";
                client.Encoding = Encoding.UTF8;
                string json = client.UploadString(apiUrl, inputJson);
                //    var results = JsonConvert.DeserializeObject<dynamic>(json);
                // var message = results.Message;
                // return message;
                return json;
            }
            catch (Exception Ex)
            {
                return "Some Error Occured";
            }
        }

    }
}