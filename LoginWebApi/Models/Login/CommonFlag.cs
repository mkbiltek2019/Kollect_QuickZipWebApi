using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

namespace LoginWebApi
{
    public class CommonFlag
    {
        public string Flag { get; set; }
        public string FlagValue { get; set; }
        public string IsZipShoreABPS { get; set; }
        public string IsRefrenceCheck { get; set; }
        public string IsOverPrintMandate { get; set; }
        public string IsMandateEdit { get; set; }
        public string IsRefrenceEdit { get; set; }
        public string IsBulkMandate { get; set; }
        public string IsMandate { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string BranchName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string UserType { get; set; }
        public string ReferenceId { get; set; }
        public string Password { get; set; }
        public string PasswordKey { get; set; }
        public string UserCode { get; set; }
        public string  IsActive { get; set; }
        public string IsDeleted { get; set; }
        public string IsLoginFirstTime { get; set; }
        public string LastLogin { get; set; }
        public string UpdatedBy { get; set; }
        public string UpdatedOn { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public string BranchId { get; set; }
        public string IsDefaultPswdChange { get; set; }
        public string IsPhysical { get; set; }
        public string IsEmandate { get; set; }
        public string AppId { get; set; }
    }
    

}