using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using UserWebApi.Models.User;

namespace UserWebApi.Controllers
{
    public class SaveUserController : ApiController
    {
        
            SaveUser obj = new SaveUser();
            [HttpGet]
            [Route("api/SaveUser/BindData/{EntityId}")]
            public Dictionary<string, object> BindData(string EntityId)
            {
                return obj.BindData(EntityId);
            }
        
    }
}
