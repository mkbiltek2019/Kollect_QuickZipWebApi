using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MandateAnalysisWebAPI.Models.PhysicalMandateAnalysis;
namespace MandateAnalysisWebAPI.Controllers
{
    public class PhysicalMandateDataController : ApiController
    {
        [HttpGet]
        [Route("api/PhysicalMandateData/SetData")]
        public GetMandateDataResponseData SetData()
        {
            try
            {
                return PhysicalMandateDataMethods.SetData();


            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }


        }


        [HttpPost]
        [Route("api/PhysicalMandateData/SearchData")]
        public GetMandateDataResponseData SearchData([FromBody] GetMandateData obj)
        {
            try
            {
                return PhysicalMandateDataMethods.PhysicalSearchData(obj);


            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }


        }
        [HttpPost]
        [Route("api/PhysicalMandateData/UpdateIsCancel1")]
        public Dictionary<string, object> UpdateIsCancel1([FromBody] UpdateToCancelData obj)
        {
            try
            {
                return PhysicalMandateDataMethods.UpdateIsCancel1(obj);


            }
            catch (Exception ex)
            {

                throw ex;


            }

        }
    }
}
