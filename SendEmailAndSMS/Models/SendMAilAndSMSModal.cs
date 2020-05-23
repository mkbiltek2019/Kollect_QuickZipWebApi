using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendEmailAndSMS.Models
{
    public class GetSendLinkResponse
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public string ResCode { get; set; }
    }
    public class GetMandateReq
    {
        public string AppID { get; set; }
        public string MdtID { get; set; }
        public string MerchantKey { get; set; }

    }
}