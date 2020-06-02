using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SetupAPI.Models.Branch_Master
{
    public class ExcelAttribute
    {
        public Nullable<Int64> Srno { get; set; }
      //  public Nullable<Int64> BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        // public Nullable<Int64> Regionid { get; set; }
        public string RegionName { get; set; }
        public string Createdon { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}