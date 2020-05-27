using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SaveEditMandateAPI.Models.BankForm;

namespace SaveEditMandateAPI.Controllers
{
    public class EditMandateController : ApiController
    {
        BankFormData objbankform = new BankFormData();

        //[HttpGet]
        //[Route("api/BankForm/EditMethod/{mandateid}")]
        //public Dictionary<string, object> EditMethod([FromBody] UserEntity data,string mandateid)
        //{
        //    return objbankform.EditMethod(data,mandateid);
        //}

        [HttpPost]
        [Route("api/EditMandate/EditMethod/{mandateid}")]
        public Dictionary<string, object> EditMethod([FromBody] UserEntity data, string mandateid)
        {
            return objbankform.EditMethod(data, mandateid);

        }

        [HttpPost]
        [Route("api/EditMandate/UpdateData/{mandateid}")]
        public Dictionary<string, object> UpdateData([FromBody] UserEntity savedata, string mandateid)
        {
            return objbankform.SaveData(savedata, mandateid);

        }
    }
}
