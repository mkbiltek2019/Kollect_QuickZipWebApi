using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveEditMandateAPI.Models.MandateDetails
{
    public class MandateHeader
    {
        public string UserId { get; set; }
        public string EntityId { get; set; }
        public string AppId { get; set; }
        public string MandateId { get; set; }
        public string URL { get; set; }
        public string Authmode { get; set; }
    }
}