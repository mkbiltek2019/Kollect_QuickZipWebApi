using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserWebApi.Models.User
{
    public class Branch
    {
        public Nullable<Int64> BranchId { get; set; }
        public string name { get; set; }
    }
}