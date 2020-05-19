using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SaveEditMandateAPI.Models.BankForm;



namespace SaveEditMandateAPI.Controllers
{
    public class SaveMandateController : ApiController
    {
        BankFormData objbankform = new BankFormData();

        [HttpPost]
        [Route("api/ReportView/GetBankFormdata")]
        public Dictionary<string, object> GetBankFormdata([FromBody] UserEntity data)
        {
            return objbankform.GetPageLoaddata(data);
        }
    }
}
