using AccountvalidationWebAPI.Models;
using BusinessLibrary;
using EntityDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountvalidationWebAPI.Controllers.BusinessLogic
{
    public static class GetPennydropDetails
    {
        public static IEnumerable<pennyDetails> Getpennydata(string IFSC, string AppId)
        {
            QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
           
            List<pennyDetails> nn = new List<pennyDetails>();
            var Result = dbcontext.MultipleResults("[dbo].[sp_GetPennydropdetails]").With<pennyDetails>().Execute("@QueryType", "@Ifsc", "@AppId", "getpennydata", IFSC, AppId);
            nn = Result[0].Cast<pennyDetails>().ToList();
            return nn;
        }
    }
}