using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using RolesWebApi.Models.BankRoles;
using RolesWebApi.Controllers.BusinessLogic;

namespace RolesWebApi.Controllers
{
    public class CorporateRolesController : ApiController
    {
        BankRoles obj = new BankRoles();
        [HttpPost]
        [Route("api/CorporateRoles/BindDataCorporate")]
        public BindResponseDataCorporate BindDataCorporate([FromBody] BindJsonData bindata)
        {

            try
            {
                return obj.BindDataCorporate(bindata);

            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }


        }

        [HttpGet]
        [Route("api/CorporateRoles/BindSecBranch/{Id}")]
        public BindResponseSecBranch BindSecBranch(int Id)
        {
            try
            {
                return obj.BindSecBranch(Id);

            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }


        }

        [HttpPost]
        [Route("api/CorporateRoles/SaveRole")]
        public SaveResponseData SaveRole([FromBody] SaveRoleData savedata)
        {
            try
            {
                return obj.SaveRole(savedata);

            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }



        }
        [HttpPost]
        [Route("api/CorporateRoles/deleteRole")]
        public SaveResponseData deleteRole([FromBody] SaveRoleData savedata)
        {
            try
            {
                return obj.deleteRole(savedata);

            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }


        }
    }
}
