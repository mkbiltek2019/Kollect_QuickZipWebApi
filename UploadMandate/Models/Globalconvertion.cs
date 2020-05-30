using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Http.Cors;
using System.Web;

namespace UploadMandate.Models
{
    public class Globalconvertion
    {
       
        public string ConvertImageBase64(string path)
        {
            //byte[] imageArray = System.IO.File.ReadAllBytes(@"E:\Projects\Fateh\MandatePic.jpg");
            byte[] imageArray = System.IO.File.ReadAllBytes(path);
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            string[] temp = base64ImageRepresentation.Split(',');
            var img = System.Drawing.Image.FromStream(new MemoryStream(Convert.FromBase64String(base64ImageRepresentation)));
            return base64ImageRepresentation;
        }
       
    }
}