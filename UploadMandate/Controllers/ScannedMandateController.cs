using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UploadMandate.Models;
namespace UploadMandate.Controllers
{
    public class ScannedMandateController : ApiController
    {
        ViewCropimage ViewCropmandate = new ViewCropimage();
        [Route("api/ScannedMandate/ViewCopscan")]
        [HttpPost]
        public IEnumerable<ResponseFlag> ViewCopscan([FromBody]  UploadMandateModel context)
        {
            return ViewCropmandate.ViewCropImage(context);
        }
    }
}
