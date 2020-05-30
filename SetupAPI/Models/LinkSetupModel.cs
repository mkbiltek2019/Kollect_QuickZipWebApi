using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SetupAPI.Models
{
    public class LinkSetupModel
    {
        public int LinkID { get; set; }
        public string LinkName { get; set; }
        public string IconName { get; set; }
        public string Purpose { get; set; }
        public string url { get; set; }
        public bool IsActive { get; set; }
        public int ParentId { get; set; }
        public int OrderNo { get; set; }
        public string CreatedOn { get; set; }
    }

    public class Initial_LinkSetup
    {
        public int Maxcount { get; set; }
        public int PageCount { get; set; }
        public string SearchValue { get; set; }
    }

    public class LinkSetup_InsertModel
    {
        public string IconName { get; set; }
        public bool IsActive { get; set; }
        public string LinkName { get; set; }
        public int OrderNo { get; set; }
        public string Purpose { get; set; }
        public string url { get; set; }
        public int ParentMenuID { get; set; }
        public string RoleID { get; set; }
        public long LinkID { get; set; }
    }

}