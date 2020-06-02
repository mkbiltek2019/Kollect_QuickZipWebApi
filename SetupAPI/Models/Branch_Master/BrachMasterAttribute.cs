using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace SetupAPI.Models.Branch_Master
{
    public class BrachMasterAttribute
    {
        public Nullable<Int64> Srno { get; set; }
        public Nullable<Int64> BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public Nullable<Int64> Regionid { get; set; }
        public string RegionName { get; set; }
        public string Createdon { get; set; }
        public Nullable<Int64> CreatedBy { get; set; }
        public Nullable<Int64> UpdatedBy { get; set; }
    }        

    //public class BranchListData
    //{
    //    public List<BrachMasterAttribute> bmattr { get; set; }
    //}
}