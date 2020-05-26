using AccountvalidationWebAPI.Models;
using BusinessLibrary;
using EntityDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountvalidationWebAPI.Controllers.BusinessLogic
{
    public static class GetTraceNoClass
    {
        public static string getTraceNo(string AppId)
        {
            QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
            List<InsertrequestNOModel> InsertrequestNOModelllist = new List<InsertrequestNOModel>();
            var Result = dbcontext.MultipleResults("[dbo].[sp_Payment]").With<InsertrequestNOModel>().Execute("@QueryType", "@AppId", "getTraceNo", AppId);


            InsertrequestNOModelllist = Result[0].Cast<InsertrequestNOModel>().ToList();

            return Convert.ToString(InsertrequestNOModelllist[0].RequestNo);

        }
    }
}