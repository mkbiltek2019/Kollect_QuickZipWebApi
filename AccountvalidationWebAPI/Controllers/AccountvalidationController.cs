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
               
                return AccountValidationMethods.AckPaymentTestNew(Acdata.ActivityId, Dbsecurity.Decrypt(Acdata.MandateId), Dbsecurity.Decrypt(Acdata.AcNo), Acdata.IFSC, Dbsecurity.Decrypt(Acdata.UserId), Dbsecurity.Decrypt(Acdata.AppId), Dbsecurity.Decrypt(Acdata.EntityId));               
            }
            catch(Exception ex)
            {
                return null;
            }


        }
    }
}
