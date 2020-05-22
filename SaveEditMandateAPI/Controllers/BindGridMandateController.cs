using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SaveEditMandateAPI.Models.BankForm;

namespace SaveEditMandateAPI.Controllers
{
    public class BindGridMandateController : ApiController
    {
        BankFormData objbankform = new BankFormData();

        [HttpGet]
        [Route("api/BankForm/BindGrid/{UserId}/{AppId}")]
        public Dictionary<string, object> BindGrid(string UserId,string AppId)
        {
            return objbankform.BindGrid(UserId, AppId);
        }
    }
}
