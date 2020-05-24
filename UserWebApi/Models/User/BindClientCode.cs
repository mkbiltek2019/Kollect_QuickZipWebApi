using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserWebApi.Models.User
{
    public class BindClientCode
    {
        public Nullable<Int64> CliendID { get; set; }
        public string Clientcode { get; set; }
    }
}