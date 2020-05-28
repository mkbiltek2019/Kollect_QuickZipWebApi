using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserWebApi.Models.User
{
    public class userReport
    {
        public Nullable<Int64> Srno { get; set; }
        
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string Status { get; set; }
    }
}