using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DownloadmandateApi.Models
{
    public class RejectMandateModel
    {
        public string UserId { get; set; }
        public string strToDate { get; set; }
        public string strFromDate { get; set; } //cv date
        public string RejectedReason { get; set; }
        public string[] strTable { get; set; }
    }
}