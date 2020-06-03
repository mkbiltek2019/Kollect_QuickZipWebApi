using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveEditMandateAPI.Models.MandateDetails
{

    public class gridlist
    {
        public List<MandateDetailList> Gridlist { get; set; }
    }
    public class MandateDetailList
    {
        public Nullable<Int64> MandateId { get; set;}
        public string CustName { get; set; }
        public string customerBankname { get; set; }
        public string AcType { get; set; }
        public string CustAcNo { get; set; }
        public string EntityAcNo { get; set; }
        public string DebitType { get; set; }
        public string AmountRupees { get; set; }
        public string FirstCollectionDate { get; set; }
        public string FinalCollectionDate { get; set; }
        public string ToDebit { get; set; }
        public string BankID { get; set; }
        public string Frequency { get; set; }
        public string EntityName { get; set; }
        public string YoekiNachCode { get; set; }
        public string CategoryName { get; set; }
        public string CategoryCode { get; set; }
        public string BankCode { get; set; }
        public string Refrence1 { get; set; }
        public string ImagePath { get; set; }       
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string EMandatetype { get; set; }
        public Nullable<Int64> CreatedBy { get; set; }
        public string EntityIFSC { get; set; }
        public string EntityBankName { get; set; }
        public string Paise { get; set; }
        public string AcceptRefNo { get; set; }
        public string MandateStatus { get; set; }

        public Nullable<bool> IsShowConsent { get; set; }
        public string Isexpire { get; set; }
        public string CustIFSC { get; set; }
        public string url { get; set; }
        public string xml { get; set; }
        
    }

    public class MandateDetailList1
    {
        public string EmandatetypeText { get; set; }
        public string EMandatetype { get; set; }
    }
    }