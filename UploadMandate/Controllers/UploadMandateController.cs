using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UploadMandate.Models;

namespace UploadMandate.Controllers
{

    public class UploadMandateController : ApiController
    {
        UploadA4Scanimages Uploadmandate = new UploadA4Scanimages();
        [Route("api/UploadMandate/uploadA4scanbase64")]
        [HttpPost]
        public IEnumerable<ResponseFlag> uploadA4scan([FromBody]  UploadMandateModel context)
        {
            return  Uploadmandate.UploadA4Image(context);
        }
        [Route("api/UploadMandate/GetA4scanbase64")]
        [HttpPost]
        public IEnumerable<ResponseFlag> GetA4scanbase64([FromBody]  UploadMandateModel context)
        {
            return Uploadmandate.GetA4Image(context);
        }
        [Route("api/UploadMandate/CropA4scanbase64")]
        [HttpPost]
        public IEnumerable<ResponseFlag> CropA4scan([FromBody]  UploadMandateModel context)
        {
            return Uploadmandate.CropA4Image(context);
        }
        [Route("api/UploadMandate/CropUploadA4scan")]
        [HttpPost]
        public IEnumerable<ResponseFlag> CropUploadA4scan([FromBody]  UploadMandateModel context)
        {
            return Uploadmandate.CropUploadA4Image(context);
        }
        [Route("api/UploadMandate/GetA4Cropbase64")]
        [HttpPost]
        public IEnumerable<ResponseFlag> GetA4Cropbase64([FromBody]  UploadMandateModel context)
        {
            return Uploadmandate.GetA4CropImage(context);
        }
    }
}
