using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MandateAnalysisWebAPI.Models.EMandateAnalysis;
namespace MandateAnalysisWebAPI.Controllers
{
    public class EMandateDataController : ApiController
    {

        [HttpGet]
        [Route("api/EMandateData/SetData")]
        public GetMandateDataResponseData SetData()
        {
            try
            {
                return EMandateDataMethods.SetData();


            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }


        }


        [HttpPost]
        [Route("api/EMandateData/SearchData")]
        public GetMandateDataResponseData SearchData([FromBody] GetMandateData obj)
        {
            try
            {
                return EMandateDataMethods.EmandateSearchData(obj);


            }
            catch (Exception ex)
            {

                throw ex;


            }


        }
    }
}
