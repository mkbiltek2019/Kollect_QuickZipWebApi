using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserWebApi.Models.User
{
    public class createUser
    {
        public string UserName { get; set; }
        public string phoneno { get; set; }
        public string EmailId { get; set; }
        public Nullable<Int64> branch { get; set; }
        public Nullable<Int64> region { get; set; }
        public Nullable<Int64> product { get; set; }
        public Nullable<Int64> editcount { get; set; }
        public Nullable<Int64> mandatecount { get; set; }
        public Nullable<Boolean> chkactive { get; set; }
        public string UserId { get; set; }
        public string EntityId { get; set; }
        public string AppId { get; set; }
        public IList<Int64> sponsorcodearr { get; set; }
        public IList<Int64> utilitycodearr { get; set; }
        public IList<string> categorycodearr { get; set; }
        public IList<Int64> clientcodearr { get; set; }
        public IList<Int64> secbrancharr { get; set; }
        public string password { get; set; }
        public Nullable<Int64> Id { get; set; }
        public Nullable<Int32> result { get; set; }





    }
}
