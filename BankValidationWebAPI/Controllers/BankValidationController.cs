using BankValidationWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BankValidationWebAPI.Controllers
{
    public class BankValidationController : ApiController
    {
        [HttpPost]
        [Route("api/BankValidation/BankValidation")]
        public bankValidationResponseData UpdateFirst(BankValidationModal MData)
        {
            try
            {
                return BankValidationMethods.bankvalida(MData);

               // ds.Tables[4].Rows[0]["mandateid"] = DbSecurity.Encrypt(Convert.ToString(ds.Tables[4].Rows[0]["mandateid"]));
            }
            catch
            {
                return null;
            }
            

        }
    }
}
