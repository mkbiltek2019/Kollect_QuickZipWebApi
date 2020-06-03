using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SaveEditMandateAPI.Models.BankForm
{
	public class SaveData
	{
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
    public class EditData0
    {
        public string MandateFreshId { get; set; }
        public string xmlpath { get; set; }
        public Nullable<bool> isphysical { get; set; }
        public Nullable<bool> iscancel { get; set; }
        public string status { get; set; }
        public string MandateMode { get; set; }
        public string autorejectreason { get; set; }

        public Nullable<bool> IsFinalReject { get; set; }
        public Nullable<Int64> BankValidationAdminCount { get; set; }
        public Nullable<Int64> AcValidationAdminCount { get; set; }
        public Nullable<Int64> BankValidationUserCount { get; set; }
        public Nullable<Int64> AcValidationUserCount { get; set; }

        public Nullable<Int64> MandateId { get; set; }
        public Nullable<bool> IsNachLive { get; set; }
        public Nullable<bool> IsFirstValidation { get; set; }
        public Nullable<bool> IsSecondValidation { get; set; }
        public Nullable<bool> enach { get; set; }

        public string SponsorbankCode { get; set; }
        public string UtilityCode { get; set; }
        public string IsScan { get; set; }
        public string IsPrint { get; set; }
        public string jpgpath { get; set; }
        public string DateOnMandate { get; set; }
        public string Customer1 { get; set; }
        public string Customer2 { get; set; }
        public string Customer3 { get; set; }
        public string DebitType { get; set; }

        public string FrequencyType { get; set; }
        public string EntityId { get; set; }
        public string ToDebit { get; set; }
        public string AcNo { get; set; }
        public string BankName { get; set; }
        public string IFSC { get; set; }
        public string MICR { get; set; }

        public string AmountRupees { get; set; }
        public string Amount { get; set; }
        public string Refrence1 { get; set; }
        public string Refrence2 { get; set; }
        public Nullable<bool> IsSendToBank { get; set; }

        public string PhoneNumber { get; set; }
        public string MandateStatus { get; set; }
        public string AcceptRefNo { get; set; }
        public string EmailId { get; set; }
        public string FromDate { get; set; }
        public string Todate { get; set; }

        public Nullable<bool> Enach { get; set; }
        public Nullable<bool> isAccountValidation { get; set; }
        public string CategoryCode { get; set; }
        public Nullable<Int32> EmandateBankLive { get; set; }
        public Nullable<Int64> ProductId { get; set; }

    }
    public class EditData1
    {
        public Nullable<Int64> MandateId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
    }
    public class EditData2
    {
        public Nullable<Int32> amount { get; set; }
        public string type { get; set; }
    }
    public class EditData3
    {
        public decimal amount { get; set; }
    }
    public class EditData4
    {
        public Nullable<Int32> show { get; set; }
    }
    public class SaveData0
    {
        public string IFSCResult { get; set; }
    }
    public class SaveData1
    {
        public string MICRResult { get; set; }
    }
    public class SaveData2
    {
        public Nullable<Int64> MandateId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public int result { get; set; }
    }
    public class SaveData3
    {
        public string Bank { get; set; }
        public string Bankname { get; set; }
    }
    public class SaveData4
    {
        public string mandateid { get; set; }
        public string MandateFreshId { get; set; }
        public Nullable<Int64> ActivityId { get; set; }
        public string IFSC { get; set; }
    }
    public class SaveData5
    {
        public Nullable<bool> IsLiveInNACH { get; set; }
    }
    public class SaveData6
    {
        public Nullable<bool> IsLiveIMPS { get; set; }
        public Nullable<bool> IsNachLive { get; set; }
        public Nullable<bool> is_enach_live { get; set; }
        public string FullBank { get; set; }
    }
    public class SaveData16
    {
        public string Emailid { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<bool> IsPhysical { get; set; }
        public Nullable<bool> Enach { get; set; }      
        public string EMandatetype { get; set; }
    }
    public class SaveData7
    {
        public Nullable<bool> IsPhysical { get; set; }
        public Nullable<bool> Enach { get; set; }
    }
    public class SaveData8
    {
        public int result { get; set; }
        public int Column1 { get; set; }
    }
}