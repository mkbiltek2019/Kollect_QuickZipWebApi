using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SetupAPI.Models.Branch_Master
{
    public class BranchAttibute
    {
        public Int64 BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public Int64 Regionid { get; set; }
    }
}