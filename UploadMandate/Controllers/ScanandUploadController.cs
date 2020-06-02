using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UploadMandate.Models;
namespace UploadMandate.Controllers
{
    public class ScanandUploadController : ApiController
    {
        Scanandupload ScanUploadmandate = new Scanandupload();
        [Route("api/ScanandUpload/uploadCopscan")]
        [HttpPost]
        public IEnumerable<ResponseFlag> uploadCopscan([FromBody]  UploadMandateModel context)
        {
            return ScanUploadmandate.UploadCropImage(context);
        }
        [Route("api/ScanandUpload/FinaluploadCopscan")]
        [HttpPost]
        public IEnumerable<ResponseFlag> FinaluploadCopscan([FromBody]  UploadMandateModel context)
        {
            return ScanUploadmandate.FinalUploadCropImage(context);
        }
    }
}
