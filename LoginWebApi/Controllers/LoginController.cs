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
        [HttpGet]
        [Route("api/Login/SentEmail/{email}")]
        public IEnumerable<EmailSent> SentEmail(string email)
        {
            return objlogin.SendMail(email);
        }
        [HttpPost]
        [Route("api/Login/Updatepassword")]
        public IEnumerable<ChangePasswordRes> Updatepassword([FromBody] ChangePasswordJsn Changepassword)
        {
            return objlogin.UpdatePassworddtail(Changepassword);
        }
    }
}
