using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AccountvalidationWebAPI.Models
{
   
    public class AccountValidationModel
    {
        public string MandateId { get; set; }
        //public string IsliveInNach { get; set; }
        public string ActivityId { get; set; }
        public string AcNo { get; set; }
        public string IFSC { get; set; }
        public string MICR { get; set; }
        public string UserId { get; set; }
        public string AppId { get; set; }
        public string EntityId { get; set; }
    }
    public class InsertrequestModel
    {
        public string Customer1 { get; set; }
        //public string IsliveInNach { get; set; }
        public Int32 LengthName { get; set; }
        public Int64 paymentRequestId { get; set; }

    }
    public class InsertrequestNOModel
    {
        public string RequestNo { get; set; }
    }
    public class ResDescription
    {
        public string Description { get; set; }
        //public string IsliveInNach { get; set; }
        public string Status { get; set; }
        public string Tab { get; set; }
        public string type { get; set; }
        public string BankReturnCustNme { get; set; }
        
    }
    public class ResCust1
    {
        public string Customer1 { get; set; }
    }
    public class ResIfsc
    {
        public Nullable<Boolean> IsIMPSLive { get; set; }
        public string IFSC { get; set; }
    }

    public class Resshow
    {
        public string show { get; set; }
    }
    public class ResAccountValidation
    {
        public List<ResDescription> ResDescriptionlist { get; set; }
        public List<ResCust1> ResCust1list { get; set; }
        public List<ResIfsc> ResIfsclist { get; set; }
        public List<Resshow> Resshowlist { get; set; }
    }
}