//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EntityDAL
{
    using System;
    
    public partial class sp_UserLogin_Result
    {
        public Nullable<bool> IsZipShoreABPS { get; set; }
        public Nullable<bool> IsRefrenceCheck { get; set; }
        public Nullable<bool> IsOverPrintMandate { get; set; }
        public Nullable<bool> IsMandateEdit { get; set; }
        public Nullable<bool> IsRefrenceEdit { get; set; }
        public Nullable<bool> IsBulkMandate { get; set; }
        public Nullable<bool> IsMandate { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public string BranchName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string UserType { get; set; }
        public long ReferenceId { get; set; }
        public string Password { get; set; }
        public string PasswordKey { get; set; }
        public Nullable<long> UserCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Nullable<bool> IsLoginFirstTime { get; set; }
        public Nullable<System.DateTime> LastLogin { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public long CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public int BranchId { get; set; }
        public bool IsDefaultPswdChange { get; set; }
        public bool IsPhysical { get; set; }
        public bool IsEmandate { get; set; }
    }
}
