using Encryptions;
using EntityDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PresentmentWebApi.Models.TransactionPresentment
{
    public class TranPresntmentDataAccess
    {
        QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
        //public Dictionary<string, object> BindHeader(string UserId)
        //{
        //    try
        //    {                          
        //        var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<AllFieldHeader>().Execute("@QueryType",
        //                  "@UserId", "BindHeaderData", Dbsecurity.Decypt(UserId)));
        //        return Result;
        //    }
        //    catch (Exception ex) { throw ex; }
        //}
    }
}