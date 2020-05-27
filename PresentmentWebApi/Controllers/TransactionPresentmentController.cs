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
        [HttpPost]
        [Route("api/TransactionPresentment/BindHeader")]
        public Dictionary<string, object> getlogindetails(JsonFields Data )
        {
            return ObjPDA.BindHeader(Data);
        }
    }
}
