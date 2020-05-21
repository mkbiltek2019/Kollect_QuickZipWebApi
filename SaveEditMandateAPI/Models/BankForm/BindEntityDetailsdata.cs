using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveEditMandateAPI.Models.BankForm
{
    public class BindEntityDetailsdata
    {
        public Nullable<bool> IsValidationCountEnable { get; set; }
        public Nullable<Int64> BankValidationAdminCount { get; set; }
        public Nullable<Int64> AcValidationAdminCount { get; set; }
        public Nullable<Int64> BankValidationUserCount { get; set; }
        public Nullable<Int64> AcValidationUserCount { get; set; }
        
    }
}