using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserWebApi.Models.User
{
    public class Product
    {
        public Nullable<Int64> ProductId { get; set; }
        public string name { get; set; }
    }
}