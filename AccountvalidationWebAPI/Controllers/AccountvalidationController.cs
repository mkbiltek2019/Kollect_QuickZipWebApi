using AccountvalidationWebAPI.Models;
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
        public LoginResponse Acvalidate(AccountValidationModel Acdata)
        {
            try
            {
                return AccountValidationMethods.AckPaymentTestNew(Acdata.ActivityId, Acdata.MandateId, Acdata.AcNo, Acdata.IFSC, Acdata.UserId, Acdata.AppId, Acdata.EntityId);               
            }
            catch
            {
                return null;
            }


        }
    }
}
