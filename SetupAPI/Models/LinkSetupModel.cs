using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SetupAPI.Models
{
    public class LinkSetupModel
    {
        public string LinkID { get; set; }
        public string LinkName { get; set; }
        public string IconName { get; set; }
        public string Purpose { get; set; }
        public string url { get; set; }
        public string IsActive { get; set; }
        public string ParentId { get; set; }
        public string OrderNo { get; set; }
    }
}