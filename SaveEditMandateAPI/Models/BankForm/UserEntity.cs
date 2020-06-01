using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveEditMandateAPI.Models.BankForm
{
    public class UserEntity
    {
        public string UserId { get; set; }
        public string EntityId { get; set; }
        public string AppId { get; set; }
        public string MandateId { get; set; }
        public string MandateMode { get; set; }
        public string Bankaccountno { get; set; }
        public string Catagorycode { get; set; }
        public string Mandatetype { get; set; }
        public string UMRN { get; set; }
        public string UMRNDATE { get; set; }
        public string Sponsorcode { get; set; }
        public string Utilitycode { get; set; }
        public string Authrizename { get; set; }
        public string Todebit { get; set; }
        public string Withbank { get; set; }
        public string Debittype { get; set; }
        public string IFSC { get; set; }
        public string MICR { get; set; }
        public string Frequency { get; set; }
        public string Amountrupees { get; set; }
        public string Amount { get; set; }
        public string Email { get; set; }
        public string Phoneno { get; set; }
        public string Refrence1 { get; set; }
        public string Refrence2 { get; set; }
        public string PeriodFrom { get; set; }
        public string PeriodTo { get; set; }
        public string Untillcancelled { get; set; }
        public string Customer1 { get; set; }
        public string Customer2 { get; set; }
        public string Customer3 { get; set; }
    }
}