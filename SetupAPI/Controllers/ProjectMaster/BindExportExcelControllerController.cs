using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SetupAPI.Models;

namespace SetupAPI.Controllers.ProjectMaster
{
    public class BindExportExcelControllerController : ApiController
    {
        [HttpPost]
        [Route("api/BindExportGridController/BindExportGrid")]
        public GridExportExcelResponseData BindExportExcelData([FromBody] BindExportExcelJsonData bindexportdata)
        {
            try
            {
                return BranchMastermethod.BindExportExcelData(bindexportdata);



            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }
       

        }
    }
}
