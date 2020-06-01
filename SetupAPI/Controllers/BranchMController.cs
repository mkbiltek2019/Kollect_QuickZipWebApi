using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SetupAPI.Models;
using SetupAPI.Models.Branch_Master;

namespace SetupAPI.Controllers
{
    public class BranchMController : ApiController
    {

        
         BranchClass bmc = new BranchClass();
      

        [HttpPost]
        [Route("api/Branch_Master/GetAllData")]
        public Dictionary<string,object> GetAllData(GetDTCls gtdt)
        {
            try
            {
                return bmc.GetData(gtdt);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // excel export data
        [HttpPost]
        [Route("api/Branch_Master/Exportalldata")]
        public Dictionary<string, object> Exportalldata(Excelparameter exp)
        {
            try
            {
                return bmc.Exportexcel(exp);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        //Regionid get controller
        [HttpGet]
        [Route("api/Branch_Master/GetAllRegion")]
        public Dictionary<string, object> GetAllRegion()
        {
            try
            {
                return bmc.RegionID();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //Save Data

        [HttpPost]
        [Route("api/Branch_Master/SaveData")]
        public Dictionary<string, object> SaveData([FromBody] InsertjsonData insdata1)
        {
            try
            {
                return bmc.AddData(insdata1);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }



        [HttpPost]
        [Route("api/Branch_Master/UpdateData")]
        public Dictionary<string, object> UpdateData([FromBody] upjsondata upjson)
        {
            try
            {
                return bmc.Updata(upjson);

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
