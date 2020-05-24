using AccountvalidationWebAPI.Models;
using Encryptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AccountvalidationWebAPI.Controllers
{
    public class AccountvalidationController : ApiController
    {
        [HttpPost]
        [Route("api/Accountvalidation/Accountvalidation")]
        public ResAccountValidation Acvalidate(AccountValidationModel Acdata)
        {

            try
            {
               
                return AccountValidationMethods.AckPaymentTestNew(Acdata.ActivityId, Dbsecurity.Decypt(Acdata.MandateId), Dbsecurity.Decypt(Acdata.AcNo), Acdata.IFSC, Dbsecurity.Decypt(Acdata.UserId), Dbsecurity.Decypt(Acdata.AppId), Dbsecurity.Decypt(Acdata.EntityId));               
            }
            catch(Exception ex)
            {
                return null;
            }


        }
    }
}
