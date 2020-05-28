using EntityDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLibrary;
using Encryptions;

namespace UserWebApi.Models.User
{
    public class SaveUser
    {

        QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
        List<createUser> dataList = new List<createUser>();

        public Dictionary<string, object> BindData(bindalldata _bindalldata)
        {
            try
            {
                var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_CreateUser]").With<BindUtilityCode>().With<Sponsorbankcode>().With<CategoryCode>().With<BindClientCode>().With<Product>().With<Region>().With<Branch>().Execute("@QueryType", "@EntityId", "BindData", Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(_bindalldata.EntityId.Replace("_", "%"))),Dbsecurity.Decypt(_bindalldata.AppId)));
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      public IEnumerable<createUser> SaveData(createUser createuser,string dtSponsorbankcode,string dtCategorycode,string dtUtilitycode,string dtClientcode,string dtSecBranch,int IsActive,string UserId,string EntityId,string AppId)
        {
            try
            {

               

                var Result = dbcontext.MultipleResults("[dbo].[Sp_CreateUser]").With<createUser>().Execute("@QueryType", "@AppId", "@EntityId", "@UserId", "@EmailId", "@userName", "@ContactNo",
         "@password", "@ProductId", "@RegionId", "@EditPerMandate", "@MandatesPerRefrence", "@XmlSponsorBankCode", "@XmlCategoryCode", "@XmlUtilityCode", "@XmlClientCode", "@XmlSecBranch","@IsActive","@BranchId", "SaveUser"
                       , Dbsecurity.Decypt(AppId), Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(EntityId.Replace("_", "%"))), Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(UserId.Replace("_", "%"))),createuser.EmailId, createuser.UserName,createuser.phoneno, createuser.password,
             createuser.product.ToString(), createuser.region.ToString(), createuser.editcount.ToString(), createuser.mandatecount.ToString(), dtSponsorbankcode, dtCategorycode, dtUtilitycode, dtClientcode, dtSecBranch,IsActive.ToString(),createuser.branch.ToString());

                foreach (var userdata in Result)
                {
                    
                    dataList = userdata.Cast<createUser>().ToList();
                }
                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IEnumerable<createUser> UpdateData(createUser createuser, string dtSponsorbankcode, string dtCategorycode, string dtUtilitycode, string dtClientcode, string dtSecBranch, int IsActive, string UserId, string EntityId, string AppId)
        {
            try
            {

                
            var Result = dbcontext.MultipleResults("[dbo].[Sp_CreateUser]").With<createUser>().Execute("@QueryType", "@AppId", "@EntityId", "@UserId", "@EmailId", "@userName", "@ContactNo",
         "@ProductId", "@RegionId", "@EditPerMandate", "@MandatesPerRefrence", "@XmlSponsorBankCode", "@XmlCategoryCode", "@XmlUtilityCode", "@XmlClientCode", "@XmlSecBranch", "@IsActive", "@BranchId","@User", "UpdateUser"
                       , Dbsecurity.Decypt(AppId), Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(EntityId.Replace("_", "%"))), Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(UserId.Replace("_", "%"))), createuser.EmailId, createuser.UserName, createuser.phoneno, 
             createuser.product.ToString(), createuser.region.ToString(), createuser.editcount.ToString(), createuser.mandatecount.ToString(), dtSponsorbankcode, dtCategorycode, dtUtilitycode, dtClientcode, dtSecBranch, IsActive.ToString(), createuser.branch.ToString(),createuser.Id.ToString());

                foreach (var userdata in Result)
                {
                    //Flag = employe.Cast<ResFlag>().ToList() .Select(x=>x.Responseflag).First().ToString();
                    dataList = userdata.Cast<createUser>().ToList();
                }
                return dataList;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public Dictionary<string, object> BindUser(binduserdata _binduserdata)
      {
            try
            {
                var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_CreateUser]").With<bindUser>().With<TempData>().Execute("@QueryType", "@EntityId","@AppId","@SearchText", "@PageCount", "BindUser", Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(_binduserdata.EntityId.Replace("_", "%"))),Dbsecurity.Decypt(_binduserdata.AppId),_binduserdata.SearchText,_binduserdata.PageCount));
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<string, object> GetExcelReport(bindalldata _bindalldata)
        {
            try
            {
                var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_CreateUser]").With<userReport>().Execute("@QueryType", "@EntityId", "@AppId", "ExportExcel_UserGrid", Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(_bindalldata.EntityId.Replace("_", "%"))), Dbsecurity.Decypt(_bindalldata.AppId)));
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<string, object> EditData(int UserId)
        {
            try
            {
                var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_CreateUser]").With<tblUser>().With<BindUtilityCode>().With<Sponsorbankcode>().With<CategoryCode>().With<BindClientCode>().With<Branch>().With<tbluserprimarybranch>().Execute("@QueryType","@UserId", "Edit", UserId.ToString()));
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}