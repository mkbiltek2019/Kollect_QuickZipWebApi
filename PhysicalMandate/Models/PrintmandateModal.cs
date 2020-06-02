using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhysicalMandate.Models
{
    public class PrintmandateModal
    {

        public string MandateId { get; set; }
        public string AppId { get; set; }
        public string QR { get; set; }
        public string UserId { get; set; }

    }
    public class UpdateNameRes
    {
        public string done { get; set; }
    }
    public class ImagePathData
    {
        public string ImagePath { get; set; }
    }
    public class CutterImag
    {
        public byte[] ImageData { get; set; }
    }
    public class ShowData
    {
        public string Customer2 { get; set; }
        public string Customer3 { get; set; }
        public string Customer1 { get; set; }
        public string DebitType { get; set; }
        public string Frequency { get; set; }
        public string BankName { get; set; }
        public string AccountNo { get; set; }
        public string DebitTo { get; set; }
        public string CreatedStatus { get; set; }
        public string SponserBankCode { get; set; }
        public string UtilityCode { get; set; }
        public string CompanyName { get; set; }
        public string SlipDate { get; set; }
        public string IFSCcode { get; set; }
        public string MICRcode { get; set; }
        public string AmountInDigit { get; set; }
        public string AmountInWord { get; set; }
        public string Reference1 { get; set; }
        public string Reference2 { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set; }
        public Int32 NoOfQRCopy { get; set; }

        public string BenificiaryName { get; set; }
    }
    public class GetCredential
    {
        public string Amazon_SMTPHost { get; set; }
        public string Amazon_SMTPPort { get; set; }
        public Nullable<Boolean> Amazon_SMTPEnableSsl { get; set; }
        public string Amazon_UserId { get; set; }
        public string Amazon_MailPassword { get; set; }
        public string Amazon_FromMailId { get; set; }
        public string Team { get; set; }
        public string AuthKey { get; set; }
        public string SenderId { get; set; }
        public string FilePath_SaveEditAPI { get; set; }
        public string IsClientSmsApi { get; set; }
        public string EWebAppUrl { get; set; }

    }
}