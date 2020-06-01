using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HistoricalMandateWebAPI.Models.HistoricalMandate
{
    public class MandateDetails
    {
        public string UserId { get; set; }
        public string EntityId { get; set; }
        public string AppId { get; set; }
        public string  FromDate { get; set; }
        public string ToDate { get; set; }
    }
}