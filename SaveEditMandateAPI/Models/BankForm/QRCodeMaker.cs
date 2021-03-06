﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QRCoder;
using BusinessLibrary;
using EntityDAL;
using System.Drawing;
using Encryptions;
using System.Data;
using System.IO;
using System.Configuration;
using System.Net.Mail;
using System.Text;

namespace SaveEditMandateAPI.Models.BankForm
{
    public class QRCodeMaker
    {
        public static void QRGenerator(string MandateID, string EntityId, string Refrence1,string AppId)
        {
            QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
            string code = MandateID + "_" + Refrence1;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.H);
            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
            imgBarCode.Height = 250;
            imgBarCode.Width = 250;
            using (Bitmap bitMap = qrCode.GetGraphic(20))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    byte[] byteImage = ms.ToArray();
                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                    string result = Convert.ToBase64String(byteImage, 0, byteImage.Length);
                    string check = CreateImage(result.ToString(), MandateID, AppId);
                    if (check != "")
                    {                        
                        var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").Execute("@QueryType", "@EntityId", "@MandateId", "@QRCodeImagepath", "QRCodeImagepath", EntityId, MandateID, check);
                    }
                }               
            }
        }

        public static string CreateImage(string Byt, string MandateID,string AppId)
        {
            try
            {
                byte[] data = Convert.FromBase64String(Byt);
                var filename = MandateID + ".png";
                string FolderPath = System.Web.Hosting.HostingEnvironment.MapPath("~/MandateQrcode/"+ AppId);
                string FilePath = FolderPath +'/' + filename;
                if (!Directory.Exists(FolderPath))   // CHECK IF THE FOLDER EXISTS. IF NOT, CREATE A NEW FOLDER.
                {
                    Directory.CreateDirectory(FolderPath);
                }
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                }    
                System.IO.File.WriteAllBytes(FilePath, data);
                FolderPath = FolderPath + filename;
                string ImgName = ".../profileimages/" + filename;
                return FilePath;
            }
            catch (Exception e)
            {
                return "Error";
            }
        }
    }
}