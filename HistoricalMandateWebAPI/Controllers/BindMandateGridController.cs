using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using HistoricalMandateWebAPI.Models.HistoricalMandate;


namespace HistoricalMandateWebAPI.Controllers
{
    public class BindMandateGridController : ApiController
    {
        HistoricalMandate objmandate = new HistoricalMandate();

        [HttpPost]
        [Route("api/BindMandateGrid/BindGrid")]
        public gridlist BindGrid([FromBody] MandateDetails data)
        {
            return objmandate.BindGrid(data);
        }
        //public Dictionary<string, object> BindGrid([FromBody] MandateDetails data)
        //{
        //    return objmandate.BindGrid(data);
        //}
    }
}
