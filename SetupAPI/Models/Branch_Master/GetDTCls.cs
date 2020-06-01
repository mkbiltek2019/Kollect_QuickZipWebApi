using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SetupAPI.Models.Branch_Master
{
    public class GetDTCls
    {
        public string AppId { get; set; }
        public string EntityId { get; set; }
        public string SearchText { get; set; }
       public string PageCount { get; set; }
    }
}