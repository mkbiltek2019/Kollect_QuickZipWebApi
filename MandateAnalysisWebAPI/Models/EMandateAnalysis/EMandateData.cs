using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
namespace MandateAnalysisWebAPI.Models.EMandateAnalysis
{
    public class EMandateData
    {


    }


    public class GetMandateData
    {
        public string Physicalmandate { get; set; }
        public string Total { get; set; }
        public string Year { get; set; }
        public string Month { get; set; }


    }

    public class GetMonth
    {
        public Int32 MonthNumber { get; set; }
        public string MonthName { get; set; }

    }
    public class GetYear
    {
        public string CurrentYear { get; set; }
        public Int32 CurrentYearVal { get; set; }

    }
    public class Count
    {

        public Int32 MandateCount { get; set; }

    }
    public class GridData
    {

        public string StatusName { get; set; }
        public string MandateId { get; set; }
        public string Refrence1 { get; set; }
        public string Refrence2 { get; set; }
        public string UMRN { get; set; }
        public string NPCLMsgId { get; set; }
        public string MSGId { get; set; }
        public string AcceptRejectReason { get; set; }
        public string Customer1 { get; set; }
        public string PhoneNumber { get; set; }
        public string Emailid { get; set; }
        public string Createdby { get; set; }
        public Nullable<DateTime> Createdon { get; set; }
        public Nullable<DateTime> UpdatedOn { get; set; }
        public string Updatedby { get; set; }
        public Nullable<DateTime> BankValidationOn { get; set; }
        public Nullable<DateTime> AccountValidationOn { get; set; }
        public Nullable<DateTime> PrintedOn { get; set; }
        public Nullable<DateTime> LinkGeneratedOn { get; set; }
        public Nullable<DateTime> EMandateJournyStartedOn { get; set; }
        public Nullable<DateTime> MandateUploadedOn { get; set; }
        public Nullable<DateTime> ImageDownloadedOn { get; set; }
        public Nullable<DateTime> RegisterRejectedOn { get; set; }
        public Nullable<DateTime> SubmittedToBank { get; set; }
        public Nullable<DateTime> CancelOn { get; set; }
        public Nullable<DateTime> ResponseReceivedOn { get; set; }
        public string AcceptRefNo { get; set; }
        public string SMSRejectionReason { get; set; }
        public string SMSStatus { get; set; }
        public Nullable<DateTime> SMSDel_Ts { get; set; }
        public Nullable<DateTime> SMSSendOn { get; set; }
        public string LastSMSStatus { get; set; }
        public Nullable<DateTime> LastSMSSendOn { get; set; }
        public Nullable<DateTime> LastDelivery { get; set; }
        public Nullable<DateTime> EmailSendOn { get; set; }
        public Nullable<DateTime> LastEmailSendOn { get; set; }

    }

    public class GetMandateDataResponseData
    {

        public List<GetMonth> GetMonthlist { get; set; }
        public List<GetYear> GetYearlist { get; set; }
        public List<GridData> GridDatalist { get; set; }
        public List<Count> MandateCountlist { get; set; }
    }


    public class BankFormData
    {
        public string mandateId { get; set; }
        public string UserId { get; set; }
    }

}