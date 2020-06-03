using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SetupAPI.Models;

namespace SetupAPI.Controllers.ProjectMaster
{
    public class UpdateDataControllerController : ApiController
    {
        [HttpPost]

        [Route("api/UpdateDataController/UpdateData")]

        public UpdateResponseData UpdateData([FromBody] UpdateJsonData updatedata)
        {
            try
            {
                return BranchMastermethod.UpdateData(updatedata);


            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }
   

        }
    }
}
