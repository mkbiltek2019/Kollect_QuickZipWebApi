using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SetupAPI.Models;
using SetupAPI.Models.RegionMaster;

namespace SetupAPI.Controllers
{
    public class RegionMasterController : ApiController
    {
        RegionMasterDataAccess obj = new RegionMasterDataAccess(); 
        [HttpPost]        [Route("api/RegionMaster/GetData")]        public BindaResponseData GetData([FromBody] GetDatavalue obj)        {            try            {                return RegionMasterDataAccess.GetData(obj);            }            catch (Exception ex)            {
                //return null;
                throw ex;            }        }

        [HttpPost]        [Route("api/RegionMaster/SaveData")]        public SaveResponseData1 SaveData([FromBody] SaveJsonData1 insertdata)        {            try            {                return obj.SaveData(insertdata);            }            catch (Exception ex)            {
                //return null;
                throw ex;            }        }


        [HttpPost]        [Route("api/RegionMaster/GetState")]        public BindResponseStateData GetState([FromBody] GetDatavalue Statedata)        {            try            {                return RegionMasterDataAccess.GetState(Statedata);            }            catch (Exception ex)            {
                //return null;
                throw ex;            }        }

        [HttpPost]        [Route("api/RegionMaster/UpdateData")]        public UpdateResponseData1 UpdateData1([FromBody] UpdateJsonData1 updateregion)        {            try            {                return RegionMasterDataAccess.UpdateData1(updateregion);            }            catch (Exception ex)            {
                //return null;
                throw ex;            }        }

        [HttpPost]
        [Route("api/RegionMaster/BindExportGrid")]
        public GridExportExcelResponseData1 BindExportExcelData1([FromBody] BindExportExcelJsonData1 bindexportdata1)
        {
            try
            {
                return RegionMasterDataAccess.BindExportExcelData1(bindexportdata1);



            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }


        }

        [HttpPost]
        [Route("api/RegionMaster/EditData")]
        public editResponseData1 EditData([FromBody] editdataJsonData1 editstatename)
        {
            try
            {
                return RegionMasterDataAccess.EditData(editstatename);



            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }


        }
    }
}



