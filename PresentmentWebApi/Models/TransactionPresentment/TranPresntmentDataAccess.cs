using Encryptions;
using EntityDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLibrary;


namespace PresentmentWebApi.Models.TransactionPresentment
{
    public class TranPresntmentDataAccess
    {
        QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
        public Dictionary<string, object> BindHeader(JsonFields Data)
        {
            try
            {
                var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_Presenment] ").With<AllFieldHeader>().Execute("@QueryType",
                          "@UserId", "BindHeaderData", Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(Data.UserId.Replace("_", "%")))));
                return Result;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}