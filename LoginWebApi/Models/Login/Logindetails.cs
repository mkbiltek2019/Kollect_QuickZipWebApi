using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LoginWebApi.Models.Login
{
    public class Logindetails
    {
        public Nullable<bool>  IsZipShoreABPS { get; set; }
        public Nullable<bool> IsRefrenceCheck { get; set; }
        public Nullable<bool> IsOverPrintMandate { get; set; }
        public Nullable<bool> IsMandateEdit { get; set; }
        public Nullable<bool> IsRefrenceEdit { get; set; }
        public Nullable<bool> IsBulkMandate { get; set; }
        public Nullable<bool> IsMandate { get; set; }
        public Nullable<Int64> UserId { get; set; }
        public string UserName { get; set; }
        public string BranchName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string UserType { get; set; }
        public Nullable<Int64> ReferenceId { get; set; }
        public string Password { get; set; }
        public string PasswordKey { get; set; }
        public Nullable<Int64> UserCode { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<bool> IsLoginFirstTime { get; set; }
        public Nullable<DateTime> LastLogin { get; set; }
        public Nullable<Int64> UpdatedBy { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public Nullable<Int64> CreatedBy { get; set; }
        public Nullable<DateTime> CreatedOn { get; set; }
        public Nullable<int> BranchId { get; set; }
        public Nullable<bool> IsDefaultPswdChange { get; set; }
        public Nullable<bool> IsPhysical { get; set; }
        public Nullable<bool> IsEmandate { get; set; }
        public bool IsAccountBlocked { get; set; }
        public Int32 IswrongAttempt { get; set; }
    }
}