using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RolesWebApi.Models.BankRoles
{
    public class UserRole
    {
    }
        public class BindResponseData
        {

            public List<BindBankRole> GridBindlist { get; set; }
           

        }

    public class BindResponseSecBranch
    {

        public List<BindSecbranch> BindSeclist { get; set; }


    }

    public class SaveResponseData
    {

        public List<SaveRoleData> SaveRoleList { get; set; }


    }

    public class BindSecbranch
    {

        public Nullable<Int64> BranchId { get; set; }
        public string BranchName { get; set; }
        


    }
    public class BindBankRole
        {
        public Nullable<Int64> UserId { get; set; }
        public string UserName { get; set; }
        public string BranchName { get; set; }
        public string Implementation { get; set; }
        public string Operation { get; set; }
        [Column("Product Billing")]
        public string ProductBilling { get; set; }



    }

        public class BindJsonData
        {
            public string EntityId { get; set; }
            public string AppId { get; set; }
        public string SearchText { get; set; }
    }

    public class SaveRoleData
    {
        public Nullable<Int64> UserId { get; set; }
        public Nullable<Int64> RoleID { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<Int32> result { get; set; }
    }


}