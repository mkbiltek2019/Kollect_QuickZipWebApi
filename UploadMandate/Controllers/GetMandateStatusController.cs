using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Web;
using UploadMandate.Models;
using System.Drawing;
using ZXing;
using ZXing.Common;
namespace UploadMandate.Controllers
{
    public class GetMandateStatusController : ApiController
    {
        [Route("api/GetMandateStatus/ReadQrCode")]
        [HttpPost]
        public GetMandateResponse ReadQrCode(QRImage Data)
        {
            GetMandateResponse response = new GetMandateResponse();
            try
            {
                if (Data.base64Image == "")
                {
                    response.Message = "Incomplete data";
                    response.Status = "Failure";
                    response.ResCode = "ykR20020";
                    return response;
                }
                if (Data.base64Image == null)
                {
                    response.Message = "Incomplete data";
                    response.Status = "Failure";
                    response.ResCode = "ykR20020";
                    return response;
                }
                if (Data.MandateID == "")
                {
                    response.Message = "Incomplete data";
                    response.Status = "Failure";
                    response.ResCode = "ykR20020";
                    return response;
                }
                if (Data.MandateID == null)
                {
                    response.Message = "Incomplete data";
                    response.Status = "Failure";
                    response.ResCode = "ykR20020";
                    return response;
                }
                else
                {

                    byte[] data = Convert.FromBase64String(Data.base64Image);
                    var filename = Data.MandateID + ".jpg";
                    var path = "/QRCodeImage/";
                    bool exists = System.IO.Directory.Exists(HttpContext.Current.Server.MapPath(path));

                    if (!exists)
                    {
                        System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath(path));
                    }

                    var file = HttpContext.Current.Server.MapPath(path + filename);
                    System.IO.File.WriteAllBytes(file, data);
                    path = path + filename;

                    Bitmap bitmap1 = new Bitmap(file);
                    //  int x = 0, y = 0, width = 1531, height = 486;
                    int x = 0, y = 0, width = 260, height = 190;
                    Bitmap CroppedImage = bitmap1.Clone(new System.Drawing.Rectangle(x + 124, y + 198, width, height), bitmap1.PixelFormat);
                    LuminanceSource source;
                    source = new BitmapLuminanceSource(CroppedImage);
                    // CroppedImage.Save("E:\\abc.jpg");
                    BinaryBitmap bitmap = new BinaryBitmap(new HybridBinarizer(source));
                    Result result = new MultiFormatReader().decode(bitmap);

                    string finalresult = result.ToString();
                    response.Message = finalresult;
                    bitmap1.Dispose();
                    CroppedImage.Dispose();
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(file);
                    return response;
                }
            }
            catch (Exception Ex)
            {
                response.Message = "Incomplete data";
                return response;
            }
        }
        public class GetMandateResponse
        {
            public string Message { get; set; }
            public string Status { get; set; }
            public string MandateId { get; set; }
            public string Mandatestatus { get; set; }
            public string ResCode { get; set; }
            public string MandateData { get; set; }
        }
    }
}
