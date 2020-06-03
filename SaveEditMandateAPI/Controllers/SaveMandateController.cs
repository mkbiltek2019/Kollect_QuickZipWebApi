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
        [Route("api/SaveMandate/GetBankFormdata")]
        public Dictionary<string, object> GetBankFormdata([FromBody] UserEntity data)
        {
            return objbankform.GetPageLoaddata(data);

        }

        [HttpPost]
        [Route("api/SaveMandate/SaveData/{mandateid}")]
        public Dictionary<string, object> SaveData([FromBody] UserEntity savedata, string mandateid)
        {
            return objbankform.SaveData(savedata, mandateid);

        }

        [HttpGet]
        [Route("api/SaveMandate/BindIFSC/{BankName}")]
        public Dictionary<string, object> BindIFSC(string BankName)
        {
            return objbankform.BindIFSC(BankName);
        }

        [HttpGet]
        [Route("api/SaveMandate/BindALLIFSC")]
        public Dictionary<string, object> BindALLIFSC()
        {
            return objbankform.BindALLIFSC();
        }

        [HttpGet]
        [Route("api/SaveMandate/BindBankFromIFSC/{IFSC}")]
        public Dictionary<string, object> BindBankFromIFSC(string IFSC)
        {
            return objbankform.BindBankFromIFSC(IFSC);
        }
    }
}