using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SendEmailAndSMS.Models
{
    public class GetSendLinkResponse
    {
        public string Message { get; set; }
        public string Status { get; set; }
        public string ResCode { get; set; }
    }
    public class GetMandateReq
    {
        public string UserId { get; set; }
        public string EntityId { get; set; }
        public string AppId { get; set; }
        public string MandateId { get; set; }
        public string EntityDebitSMS { get; set; }
        public string EntityNetSMS { get; set; }
        public string EntityNetValidateMail { get; set; }
        public string EntityDebitValidateMail { get; set; }
        public string Emandatetype { get; set; }

    }
    public class GetMandateRes
    {
        public string MandateId { get; set; }
        public string Reference { get; set; }
        public string PhoneNumber { get; set; }
        public string CustName { get; set; }
        public string Amnt { get; set; }
        public string EmailId { get; set; }
        public string CatgName { get; set; }
        public string EntityName { get; set; }
    }
    public class SMS_MessageStringRes
    {
        public string SMS_MessageString { get; set; }
    }
    public class Mail_MessageStringRes
    {
        public string Mail_MessageString { get; set; }
    }
    public class SubjectHeadRes
    {
        public string SubjectHead { get; set; }
    }
    public class doneres
    {
        public string done { get; set; }
    }
    public class GetMobileNoRes
    {
        public string phonenumber { get; set; }
        public string emailid { get; set; }
        public string refrence1 { get; set; }
        public string Entityname { get; set; }
        public string Customer1 { get; set; }
        public string AmountRupees { get; set; }
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