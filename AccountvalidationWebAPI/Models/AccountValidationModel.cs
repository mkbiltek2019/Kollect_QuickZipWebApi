using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace AccountvalidationWebAPI.Models
{
    public class LoginResponse
    {
        //public List<Mandate> MandateData
        //{ get; set; }
        public string MdtID { get; set; }
        //public string IsliveInNach { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public string AccountHolderName { get; set; }
        public string ResCode { get; set; }
        public string MandateData { get; set; }
    }
    public class AccountValidationModel
    {
        public string MandateId { get; set; }
        //public string IsliveInNach { get; set; }
        public string ActivityId { get; set; }
        public string AcNo { get; set; }
        public string IFSC { get; set; }
        public string UserId { get; set; }
        public string AppId { get; set; }
        public string EntityId { get; set; }
    }
    public class InsertrequestModel
    {
        public string Customer1 { get; set; }
        //public string IsliveInNach { get; set; }
        public string LengthName { get; set; }
        public Int64 paymentRequestId { get; set; }
       
    }
    public class InsertrequestNOModel
    {
        public string RequestNo { get; set; }      
    }
}