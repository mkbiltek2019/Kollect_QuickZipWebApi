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
    using System.Collections.Generic;
    
    public partial class TblEntityMandateMode
    {
        public long TMMID { get; set; }
        public Nullable<long> EntityId { get; set; }
        public Nullable<bool> Physical { get; set; }
        public Nullable<bool> Emandate { get; set; }
        public Nullable<bool> NetBanking { get; set; }
        public Nullable<bool> Debit { get; set; }
        public Nullable<bool> Aadhar { get; set; }
        public Nullable<bool> ValidateByCustomer { get; set; }
        public Nullable<bool> Customerphnumber { get; set; }
        public Nullable<bool> Customeremailid { get; set; }
        public Nullable<bool> ValidateByCooperate { get; set; }
        public Nullable<bool> PrintQR { get; set; }
        public Nullable<bool> PrintLogo { get; set; }
        public Nullable<bool> OCR { get; set; }
        public Nullable<bool> NetValidateMail { get; set; }
        public Nullable<bool> NetManual { get; set; }
        public Nullable<bool> NetSMS { get; set; }
        public Nullable<bool> DebitValidateMail { get; set; }
        public Nullable<bool> DebitManual { get; set; }
        public Nullable<bool> DebitSMS { get; set; }
        public Nullable<bool> AadharValidateMail { get; set; }
        public Nullable<bool> AadharManual { get; set; }
        public Nullable<bool> AadharSMS { get; set; }
        public Nullable<bool> IsAccountvalidation { get; set; }
        public Nullable<long> CreatedBy { get; set; }
        public Nullable<long> UpdatedBy { get; set; }
        public Nullable<bool> IsActive { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
        public Nullable<System.DateTime> UpdatedOn { get; set; }
        public bool IsPresentment { get; set; }
        public Nullable<bool> EnableUserWise { get; set; }
        public Nullable<bool> EnableCancelUserWise { get; set; }
    }
}
