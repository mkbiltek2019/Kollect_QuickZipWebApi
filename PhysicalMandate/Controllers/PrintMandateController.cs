using Encryptions;
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
    public class PrintMandateController : ApiController
    {
        


        [HttpPost]
        [Route("api/PrintMandate/PrintMandate")]
        public string PrintMandate(PrintmandateModal MData)
        {
            try
            {
                if (MData.QR == "1")
                {
                    return PrintWithQR.PrintWIthQR(Dbsecurity.Decypt(MData.MandateId), Dbsecurity.Decypt(MData.AppId));
                }
                else
                {

                    return PrintWithoutQR.PrintWIthoutQR(Dbsecurity.Decypt(MData.MandateId), Dbsecurity.Decypt(MData.AppId));

                }
                // ds.Tables[4].Rows[0]["mandateid"] = DbSecurity.Encrypt(Convert.ToString(ds.Tables[4].Rows[0]["mandateid"]));
            }
            catch
            {
                return null;
            }


        }

        //[HttpPost]
        //[Route("api/PrintMandate/UpdateMandateMode")]
        //public string UpdateFirst(UpdateSelectModeModal MData)
        //{
        //    try
        //    {
        //        return UpdateMandateMode.UpdateMandateModeMethod(MData);

        //        // ds.Tables[4].Rows[0]["mandateid"] = DbSecurity.Encrypt(Convert.ToString(ds.Tables[4].Rows[0]["mandateid"]));
        //    }
        //    catch
        //    {
        //        return null;
        //    }


        //}

        [HttpPost]
        [Route("api/PrintMandate/UpdateMandateMode")]
        public string UpdateFirst(UpdateSelectModeModal MData)
        {
            try
            {
                return UpdateMandateMode.UpdateMandateModeMethod(MData);

                // ds.Tables[4].Rows[0]["mandateid"] = DbSecurity.Encrypt(Convert.ToString(ds.Tables[4].Rows[0]["mandateid"]));
            }
            catch
            {
                return null;
            }


        }
    }
}
