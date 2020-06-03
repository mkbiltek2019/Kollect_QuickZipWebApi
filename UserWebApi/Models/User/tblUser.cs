using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserWebApi.Models.User
{
    public class tblUser
    {
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public string UserName { get; set; }
        public Nullable<Int64> ProductId { get; set; }
        public Nullable<Int64> RegionId { get; set; }
        public Nullable<Int32> EditPerMandate { get; set; }
        public Nullable<Int32> MandatesPerRefrence { get; set; }
        public Nullable<Boolean> IsActive { get; set; }
    }
}