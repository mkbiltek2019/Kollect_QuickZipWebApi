using AccountvalidationWebAPI.Controllers.BusinessLogic;
using AccountvalidationWebAPI.Models;
using Encryptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AccountvalidationWebAPI.Controllers
{
    public class AccountvalidationController : ApiController
    {
        [HttpPost]
        [Route("api/Accountvalidation/Accountvalidation")]
        public ResAccountValidation Acvalidate(AccountValidationModel Acdata)
        {
          List<pennyDetails> res = new List<pennyDetails>();
            List<pennyDetailsList> res1 = new List<pennyDetailsList>();
            res = GetPennydropDetails.Getpennydata(Acdata.IFSC, Dbsecurity.Decrypt(Acdata.AppId)).Cast<pennyDetails>().ToList(); 
            try
            {
                if (res[0].Name.ToUpper().Contains("IDFC"))
                {
                    //if (Acdata.IFSC.Substring(0, 4).Trim() == ConfigurationManager.AppSettings["IDFC_IFSC"].ToString().Trim())
                    //{
                    //    return AccountValidationMethods.AckPaymentTestNew(Acdata.ActivityId, Dbsecurity.Decrypt(Acdata.MandateId), Dbsecurity.Decrypt(Acdata.AcNo), Acdata.IFSC, Dbsecurity.Decrypt(Acdata.UserId), Dbsecurity.Decrypt(Acdata.AppId), Dbsecurity.Decrypt(Acdata.EntityId));
                    //}
                    //else
                    //{
                        // Response.Write("In AckPaymentTestNew_IDFC");
                        return IDFC_Acvalidate.IDFCAccountval(Acdata.ActivityId, Dbsecurity.Decrypt(Acdata.MandateId), Dbsecurity.Decrypt(Acdata.AcNo), Acdata.IFSC, Dbsecurity.Decrypt(Acdata.UserId), Dbsecurity.Decrypt(Acdata.AppId), Dbsecurity.Decrypt(Acdata.EntityId),res);
                   /// }


                }
                else
                {
                    return AccountValidationMethods.KotakAccountVal(Acdata.ActivityId, Dbsecurity.Decrypt(Acdata.MandateId), Dbsecurity.Decrypt(Acdata.AcNo), Acdata.IFSC, Dbsecurity.Decrypt(Acdata.UserId), Dbsecurity.Decrypt(Acdata.AppId), Dbsecurity.Decrypt(Acdata.EntityId), res);
                }

                              
            }
            catch(Exception ex)
            {
                return null;
            }


        }
    }
}
