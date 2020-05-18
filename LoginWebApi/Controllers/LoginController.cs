using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LoginWebApi.Models.Login;
namespace LoginWebApi.Controllers
{
    public class LoginController : ApiController
    {
        Login objlogin = new Login();
        [HttpPost]
        [Route("api/Login/getlogindetails")]
        public IEnumerable<CommonFlag> getlogindetails([FromBody] Formdataget Login)
        {
            return objlogin.Binddetails(Login);
        }
    }
}
