using PresentmentWebApi.Models.TransactionPresentment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PresentmentWebApi.Controllers
{
    public class TransactionPresentmentController : ApiController
    {
        TranPresntmentDataAccess ObjPDA = new TranPresntmentDataAccess();
        [HttpGet]
        [Route("api/TransactionPresentment/BindHeader/{UserId}")]
        public Dictionary<string, object> getlogindetails(string UserId)
        {
            return ObjPDA.BindHeader(UserId);
        }
    }
}
