using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SaveEditMandateAPI.Models.MandateDetails;

namespace SaveEditMandateAPI.Controllers
{
    public class MandateDetailsController : ApiController
    {
        MandateDetailsData objmandate = new MandateDetailsData();
        
        [HttpPost]
        [Route("api/MandateDetails/GetMandateDetails")]
        public Dictionary<string, object> GetMandateDetails([FromBody] MandateHeader data)
        {
            return objmandate.GetMandateDetails(data);
        }

        [HttpPost]
        [Route("api/MandateDetails/btnMandate_Click")]
        public Dictionary<string, object> btnMandate_Click([FromBody] MandateHeader data)
        {
            return objmandate.btnMandate_Click(data);
        }

        //[HttpPost]
        //[Route("api/MandateDetails/GetEmandateData")]
        //public Dictionary<string, object> GetEmandateData([FromBody] MandateHeader data)
        //{
        //    return objmandate.GetEmandateData(data);
        //}


        [HttpPost]
        [Route("api/MandateDetails/GetEmandateData")]
        public gridlist GetEmandateData([FromBody] MandateHeader data)
        {
            return objmandate.GetEmandateData(data);
        }
    }
}
