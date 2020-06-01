using AccountvalidationWebAPI.Controllers.BusinessLogic;
using AccountvalidationWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AccountvalidationWebAPI.Controllers
{
    public class UpdateNameController : ApiController
    {
        [HttpPost]
        [Route("api/UpdateName/UpdateName")]
        public string UpdateFirst(UpdateName Data)
        {
            try
            {
                return UpdateNameMethod.UpdateName(Data);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
    }
