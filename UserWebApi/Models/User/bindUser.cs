using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserWebApi.Models.User
{
    public class bindUser
    {
        public Nullable<Int64> Srno { get; set; }
        public Nullable<Int64> UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public string Status { get; set; }

    }
}