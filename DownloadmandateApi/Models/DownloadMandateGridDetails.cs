using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DownloadmandateApi.Models
{
    public class DownloadMandateGridDetails
    {
        //public Nullable<bool> IsExcelMandateDownload { get; set; }
        //public Nullable<bool> IsSnapMandateDownload { get; set; }
        //public Nullable<bool> IsMobileData { get; set; }
        //public string createdon { get; set; }
        //public string JPGPath { get; set; }
        //public string IsScan { get; set; }
        //public string TIPPath { get; set; }
        //public string IsPrint { get; set; }
        //public Nullable<Int64> mandateid { get; set; }
        //public string BankName { get; set; }
        //public string status { get; set; }
        //public string Amount { get; set; }
        //public string Code { get; set; }
        //public string BankName1 { get; set; }
        //public string DateOnMandate { get; set; }
        //public string AcNo { get; set; }
        //public string Refrence1 { get; set; }
        //public string FromDate { get; set; }
        //public string Customer1 { get; set; }
        //public string DebitType { get; set; }
        //public string Frequency { get; set; }
        //public string ToDebit { get; set; }

        //public Nullable<Int64> mandateid { get; set; }
        public string mandateid { get; set; }
        public string Customer1 { get; set; }
       // public string DateOnMandate { get; set; }
        public DateTime DateOnMandate { get; set; } // add FromDate
        public string IsPrint { get; set; }
        public string IsScan { get; set; }
        public string Refrence1 { get; set; }
        public string Amount { get; set; }
        public string AcNo { get; set; }
        public string Code { get; set; }
        public string BankName { get; set; }
        public string Frequency { get; set; }
        public string DebitType { get; set; }
        public string ToDebit { get; set; }
        public string createdon { get; set; }

        public Nullable<Int32> value { get; set; }
    }
}