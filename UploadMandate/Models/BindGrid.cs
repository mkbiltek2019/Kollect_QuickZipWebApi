using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UploadMandate.Models
{
    public class BindGrid
    {
        public string MandateFreshId { get; set; }
        public string mandateMode { get; set; }
        public string AutoRejectReason { get; set; }
        public string AcceptRefNo { get; set; }
        public string updatedon { get; set; }
        public string UpdateBy { get; set; }
        public string username { get; set; }
        public string Enach { get; set; }
        public string IsMobileData { get; set; }
        public string RejectedReason { get; set; }
        public string REJECTED { get; set; }
        public string IsLiveInNACH { get; set; }
        public string CreatedOn { get; set; }
        public string Srno { get; set; }
        public string IsScan { get; set; }
        public string IsPrint { get; set; }
        public string DateOnMandate { get; set; }
        public string ToDebit { get; set; }
        public string BankName { get; set; }
        public string Code { get; set; }
        public string Frequency { get; set; }
        public string status { get; set; }
        public string Customer1 { get; set; }
        public string debittype { get; set; }
        public string AcNo { get; set; }
        public string mandateid { get; set; }
        public string Refrence1 { get; set; }
        public string FromDate { get; set; }
        public string UMRN { get; set; }
        public string Amount { get; set; }
        public string AmountRupees { get; set; }
        public string EmailId { get; set; }
        public string SponsorBankCode { get; set; }
        public string UtilityCode { get; set; }
        public string PhoneNumber { get; set; }
        public string NPCIErrorDesc { get; set; }
        public string NPCIMsgId { get; set; }
        public string MSGId { get; set; }
        public string AggregatorValue { get; set; }
        public decimal Amount_Numeric { get; set; }
        public string SendToBankDate { get; set; }
    }
}