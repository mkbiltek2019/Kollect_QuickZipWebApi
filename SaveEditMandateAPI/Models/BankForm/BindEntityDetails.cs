using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveEditMandateAPI.Models.BankForm
{
    public class BindEntityDetails
    {
        public Nullable<bool> IsTodatemandatoryenach { get; set; }
        public Nullable<bool> ISSendEmailCustomer { get; set; }
        public Nullable<bool> IsRefrence2Mandatory { get; set; }
        public Nullable<bool> IsShowMandateMode { get; set; }
        public string ModeOfPayment { get; set; }
        public Nullable<bool> IsRefrenceNumeric { get; set; }
        public string amout { get; set; }
        public string Date { get; set; }
        public string FromDate { get; set; }
        public string Name { get; set; }
        public string UtilityCode { get; set; }
        public string DebitType { get; set; }
        public string FrequencyType { get; set; }
        public string ToDebit { get; set; }
        public string SponsorBankCode { get; set; }
        public string PeriodType { get; set; }

    }
}