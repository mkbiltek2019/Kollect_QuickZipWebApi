using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadMandate.Models
{
    public class QRImage
    {
        public string MandateID { get; set; }
        public string base64Image { get; set; }
    }
}