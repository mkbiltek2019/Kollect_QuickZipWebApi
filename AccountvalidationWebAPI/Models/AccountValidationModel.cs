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
        public Nullable<Int32> LengthName { get; set; }
        public Nullable<Int64> paymentRequestId { get; set; }

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
    public class Error
    {
        public string cd { get; set; }
        public string rsn { get; set; }
    }
    public class MsgBdyRes
    {
        public string sts { get; set; }
        public string respCd { get; set; }
        public string txnId { get; set; }
        public string cdtrAcctId { get; set; }
        public string cdtrNm { get; set; }
        public string txnRfNo { get; set; }
    }
    public class MsgHdrRes
    {
        public string rslt { get; set; }
        public Error error { get; set; }
    }
    public class InitiateAuthIMPSFundTransferResp
    {
        public MsgHdrRes msgHdr { get; set; }
        public MsgBdyRes msgBdy { get; set; }
    }
    public class ResponseObject
    {
        public InitiateAuthIMPSFundTransferResp initiateAuthIMPSFundTransferResp { get; set; }
    }
    public class ReuestObject
    {
        public InitiateAuthIMPSFundTransferReq InitiateAuthIMPSFundTransferReq { get; set; }

    }
    public class MsgHdr
    {
        public string msgId { get; set; }
        public string cnvId { get; set; }
        public string extRefId { get; set; }
        public string bizObjId { get; set; }
        public string appId { get; set; }
        public string timestamp { get; set; }
    }
    public class InitiateAuthIMPSFundTransferReq
    {
        public MsgHdr msgHdr { get; set; }
        public MsgBdy msgBdy { get; set; }
    }
    public class MsgBdy
    {
        public string remNm { get; set; }
        public string remMobNb { get; set; }
        public string cnsmrNm { get; set; }
        public string dbtrAcctId { get; set; }
        public string acctTp { get; set; }
        public string cdtrAcctId { get; set; }
        public string finInstnId { get; set; }
        public string amt { get; set; }
        public string ccy { get; set; }
        public string pmtDesc { get; set; }
        public string txnRfNo { get; set; }

    }
    public class pennyDetails
    {
        public Int64 PennyDropId { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string MERCHANT { get; set; }
        public string CheckSum { get; set; }
        public string IDFCextRefId { get; set; }
        public string dbtrAcctId_idfc { get; set; }
        public string remMobNb { get; set; }
    }
    public class pennyDetailsList
    {
      public  List<pennyDetails> pennyDetailsListData { get; set; }
    }


}