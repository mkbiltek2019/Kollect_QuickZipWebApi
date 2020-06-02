using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLibrary;
using Encryptions;
using EntityDAL;
using RolesWebApi.Models.BankRoles;

namespace RolesWebApi.Controllers.BusinessLogic
{
    public class BankRoles
    {
        QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();

        public  BindResponseData BindData(BindJsonData obj2)
        {
            
            try
            {
                
                BindResponseData res = new BindResponseData();
                //CountResponseData res = new CountResponseData();

                List<BindBankRole> GridBindlist = new List<BindBankRole>();
                


                var Result = dbcontext.MultipleResults("[dbo].[Sp_CreateUser]").With<BindBankRole>().
                    Execute("@QueryType", "@EntityId", "@AppId","@SearchText", "BindUserRoles", Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(obj2.EntityId.Replace("_", "%"))),Dbsecurity.Decrypt(obj2.AppId),obj2.SearchText);


                res.GridBindlist = Result[0].Cast<BindBankRole>().ToList();
                

                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public BindResponseSecBranch BindSecBranch(int Id)
        {
            
            try
            {

                BindResponseSecBranch res = new BindResponseSecBranch();
                //CountResponseData res = new CountResponseData();

                List<BindSecbranch> BindSeclist = new List<BindSecbranch>();



                var Result = dbcontext.MultipleResults("[dbo].[Sp_CreateUser]").With<BindSecbranch>().
                    Execute("@QueryType", "@UserId", "BindSecBranch", Id.ToString());


                res.BindSeclist = Result[0].Cast<BindSecbranch>().ToList();


                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SaveResponseData SaveRole(SaveRoleData savedata)
        {

            try
            {

                SaveResponseData res = new SaveResponseData();
                

                List<SaveRoleData> SaveRoleList = new List<SaveRoleData>();



                var Result = dbcontext.MultipleResults("[dbo].[Sp_CreateUser]").With<SaveRoleData>().
                    Execute("@QueryType", "@Id","@RoleID","@UserId", "SaveUserRole", savedata.UserId.ToString(),savedata.RoleID.ToString(), Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(savedata.CreatedBy.Replace("_", "%"))));


                res.SaveRoleList = Result[0].Cast<SaveRoleData>().ToList();


                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public SaveResponseData deleteRole(SaveRoleData savedata)
        {

            try
            {

                SaveResponseData res = new SaveResponseData();


                List<SaveRoleData> SaveRoleList = new List<SaveRoleData>();



                var Result = dbcontext.MultipleResults("[dbo].[Sp_CreateUser]").With<SaveRoleData>().
                    Execute("@QueryType", "@Id", "@RoleID", "@UserId", "DeleteUserRole", savedata.UserId.ToString(), savedata.RoleID.ToString(), Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(savedata.CreatedBy.Replace("_", "%"))));


                res.SaveRoleList = Result[0].Cast<SaveRoleData>().ToList();


                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}