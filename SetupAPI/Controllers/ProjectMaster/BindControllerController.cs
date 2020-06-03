using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SetupAPI.Models;

namespace SetupAPI.Controllers.ProjectMaster
{
    public class BindControllerController : ApiController
    {

        [HttpPost]
        [Route("api/BindController/BindData")]
        public BindResponseData BindData([FromBody] BindJsonData bindata)
        {
            try
            {
                return BranchMastermethod.BindData(bindata);

            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }

        
        }
    }
}
