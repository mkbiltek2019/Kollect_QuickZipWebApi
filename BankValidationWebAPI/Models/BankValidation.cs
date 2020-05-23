using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace BankValidationWebAPI.Models
{
    public class BankValidationModal
    {
        public string AppId { get; set; }
        public string MandateId { get; set; }
        public string UserId { get; set; }

    }
    public class IFSCResultModal
    {
        public string IFSCResult { get; set; }
    }
    public class MICRResultModal
    {
        public string MICRResult { get; set; }
    }
    public class BankNameModal
    {
        public string BankName { get; set; }
    }
    public class MandateDataModal
    {
        public string MandateId { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }

    }
    public class MandateActivityDataModal
    {
        public string MandateId { get; set; }
        public Int64 ActivityId { get; set; }
        public string IFSC { get; set; }

    }
    public class IsLiveInNACHModal
    {
        public Nullable<Boolean> IsLiveInNACH { get; set; }
    }
    public class EMandateDataModal
    {
        public Nullable<Boolean> IsLiveIMPS { get; set; }
        public Nullable<Boolean> IsNachLive { get; set; }
        public Nullable<Boolean> is_enach_live { get; set; }//emandate 
        public string FullBank { get; set; }
        public string DebitCard { get; set; }
        public string NetBanking { get; set; }
    }
    public class EMandateTypeDataModal
    {
        public string Emailid { get; set; }
        public string PhoneNumber { get; set; }
        public Nullable<Boolean> IsPhysical { get; set; }
        public Nullable<Boolean> Enach { get; set; }
        public string EMandatetype { get; set; }
    }
    public class bankValidationResponseData
    {
        public List<IFSCResultModal> IFSCResultModallist { get; set; }
        public List<MICRResultModal> MICRResultModallist { get; set; }
        public List<BankNameModal> BankNameModallist { get; set; }
        public List<MandateDataModal> MandateDataModallist { get; set; }
        public List<MandateActivityDataModal> MandateActivityDataModallist { get; set; }
        public List<IsLiveInNACHModal> IsLiveInNACHModallist { get; set; }
        public List<EMandateDataModal> EMandateDataModallist { get; set; }//emandate data
        public List<EMandateTypeDataModal> EMandateTypeDataModallist { get; set; }
    }
}