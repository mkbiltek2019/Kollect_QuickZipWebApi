using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PresentmentWebApi.Models.TransactionPresentment
{
    public class AllFieldHeader
    {
        public Nullable<Int64> RowNumber { get; set; }
        public string date { get; set; }
        public string FileNo { get; set; }
        public string UserName { get; set; }
        public string UpdatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedOn { get; set; }
        public Nullable<DateTime> LastStatus { get; set; }
        public string BankName { get; set; }
        public string FileStatus { get; set; }
    }
}