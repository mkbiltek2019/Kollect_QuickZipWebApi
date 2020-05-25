using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SetupAPI.Models;
using BusinessLibrary;
using EntityDAL;
using Encryptions;

namespace SetupAPI.Controllers.BusinessLogics
{
    public class LinkSetupController : ApiController
    {
        QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();

        [HttpPost]
        [Route("api/Accountvalidation/Accountvalidation")]
        public void GetList()
        {
            try
            {
                var Result = dbcontext.MultipleResults("[dbo].[Sp_UserLogin]").With<LinkSetupModel>().Execute("@QueryType", "GetLinks");
            }
            catch (Exception ex)
            { }

        }
    }
}
