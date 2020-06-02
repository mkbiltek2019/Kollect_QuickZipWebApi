using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadMandate.Models
{
    public class ResponseFlag
    {
        public string Flag { get; set; }
        public string FlagValue { get; set; }
        public string Base64image { get; set; }
        public string IsScan { get; set; }
        public string IsPrint { get; set; }

    }
}