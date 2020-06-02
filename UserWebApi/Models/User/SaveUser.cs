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


        public Dictionary<string, object> BindData(string EntityId)
        {
            try
            {
                var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_CreateUser]").With<BindUtilityCode>().With<Sponsorbankcode>().With<CategoryCode>().With<BindClientCode>().With<Product>().With<Region>().With<Branch>().Execute("@QueryType", "@EntityId", "BindData", Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(EntityId.Replace("_", "%")))));
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}