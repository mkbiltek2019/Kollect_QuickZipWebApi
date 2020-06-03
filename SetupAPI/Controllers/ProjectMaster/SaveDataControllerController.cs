using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SetupAPI.Models;

namespace SetupAPI.Controllers.ProjectMaster
{
    public class SaveDataControllerController : ApiController
    {
        [HttpPost]

        [Route("api/SaveDataController/SaveData")]

        public SaveResponseData SaveData([FromBody] SaveJsonData insertdata)
        {
            try
            {
                return BranchMastermethod.SaveData(insertdata);
         

            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }


        }
    }
}
