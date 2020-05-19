using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SaveEditMandateAPI.Models.BankForm
{
    public class CheckReference
    {

       
        public Int32 available { get; set; }

        public Int64 FreshManteId { get; set; }
        public Int64 rownumber { get; set; }
        public string UserName { get; set; }
        public string datetim { get; set; }
        public string MandateId { get; set; }
        public string Refrence1 { get; set; }
        public string BankName { get; set; }
        public string Customer1 { get; set; }
        public string Code { get; set; }
        public string AcNo { get; set; }
      //  public string Reference { get; set; }

    }
}