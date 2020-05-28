using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhysicalMandate.Models
{
    public class UpdateSelectModeRes
    {
        public string done { get; set; }
    }
        public class UpdateSelectModeModal
    {
        public string MandateId { get; set; }
        public string AppId { get; set; }
        public string Physical { get; set; }
        public string EMandate { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Amount { get; set; }
        public string EMandatetype { get; set; }
        public string AmountRupeesInWords { get; set; }
        
    }
    public class SelectModeModal
    {
        public string MandateId { get; set; }
        public string AppId { get; set; }

    }
    public class EMandateTypeDataList
    {
        public List<EMandateTypeDataModal> EMandateTypeDataModalList { get; set; }
    }
    public class EMandateTypeDataModal
    {
        public string BankName { get; set; }
        public string IfscMicr { get; set; }
        public Nullable<Boolean> LiveOnNach { get; set; }
        public string LiveonDebit { get; set; }
        public string liveOnNetBanking { get; set; }
        public string Emailid { get; set; }
        public string PhoneNumber { get; set; }
        public string AmountRupees { get; set; }
        public Nullable<Boolean> IsPhysical { get; set; }
        public Nullable<Boolean> Enach { get; set; }
        public string EMandatetype { get; set; }
        public Nullable<Boolean> IsAccountvalidation { get; set; }
        public Nullable<Boolean> EntityEmandate { get; set; }
        public Nullable<Boolean> EntityPhysical { get; set; }
        public Nullable<Boolean> EntityDebit { get; set; }
        public Nullable<Boolean> EntityNetBanking { get; set; }
        public Nullable<Boolean> EntityNetSMS { get; set; }
        public Nullable<Boolean> EntityNetValidateMail { get; set; }
        public Nullable<Boolean> EntityNetManual { get; set; }
        public Nullable<Boolean> EntityDebitSMS { get; set; }
        public Nullable<Boolean> EntityDebitValidateMail { get; set; }
        public Nullable<Boolean> EntityDebitManual { get; set; }
        public Nullable<Boolean> ValidateByCooperate { get; set; }
        public Nullable<Boolean> ValidateByCustomer { get; set; }
        public Nullable<Boolean> liveOnEmandate { get; set; }
        

    }
}