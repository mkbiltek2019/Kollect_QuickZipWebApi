using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UserWebApi.Models.User
{
    public class binduserdata
    {
        public string EntityId { get; set; }
        public string AppId { get; set; }
        public string SearchText { get; set; }
        public string PageCount { get; set; }

    }
}