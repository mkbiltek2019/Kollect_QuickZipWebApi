using PhysicalMandate.Controllers.BusinessLogic;
using PhysicalMandate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PhysicalMandate.Controllers
{
    public class SelectModeController : ApiController
    {
        [HttpPost]
        [Route("api/SelectMode/SelectMode")]
        public EMandateTypeDataList UpdateFirst(SelectModeModal MData)
        {
            try
            {
                return SelectModeMethods.Selectdata(MData);

                // ds.Tables[4].Rows[0]["mandateid"] = DbSecurity.Encrypt(Convert.ToString(ds.Tables[4].Rows[0]["mandateid"]));
            }
            catch
            {
                return null;
            }


        }
    }
}
