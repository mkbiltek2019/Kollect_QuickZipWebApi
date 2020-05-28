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
            [HttpPost]
            [Route("api/SaveUser/BindData")]
            public Dictionary<string, object> BindData([FromBody] bindalldata _bindalldata)
            {
                return obj.BindData(_bindalldata);
            }

        [HttpPost]
        [Route("api/SaveUser/SaveData")]
        public IEnumerable<createUser> SaveData([FromBody] createUser createuser)
        {
            int IsActive = 0;
            string dtSponsorbankcode= "<dtXml>";
            string dtCategorycode = "<dtXml>";
            string dtUtilitycode = "<dtXml>";
            string dtClientcode = "<dtXml>";
            string dtSecBranch = "<dtXml>";

            if (createuser.chkactive == true)
            {
                IsActive = 1;

            }
            else
            {
                IsActive = 0;
            }

            

            for (var i = 0; i < createuser.sponsorcodearr.Count; i++)
            {
                dtSponsorbankcode += "<dtXml ";
                dtSponsorbankcode += " SponsorBankCodeId=" + @"""" + createuser.sponsorcodearr[i] + @"""";
                dtSponsorbankcode += " />";

            }
            
            dtSponsorbankcode += "</dtXml>";


           

            for (var i = 0; i < createuser.categorycodearr.Count; i++)
            {
                dtCategorycode += "<dtXml ";
                dtCategorycode += " CategoryCode=" + @"""" + createuser.categorycodearr[i] + @"""";
                dtCategorycode += " />";
            }
            
            dtCategorycode += "</dtXml>";


            

            for (var i = 0; i < createuser.utilitycodearr.Count; i++)
            {
                dtUtilitycode += "<dtXml ";
                dtUtilitycode += " UtilityCodeId=" + @"""" + createuser.utilitycodearr[i] + @"""";
                dtUtilitycode += " />";
            }
           
            dtUtilitycode += "</dtXml>";

            

            for (var i = 0; i < createuser.clientcodearr.Count; i++)
            {
                dtClientcode += "<dtXml ";
                dtClientcode += " ClientId=" + @"""" + createuser.clientcodearr[i] + @"""";
                dtClientcode += " />";
            }
            
            dtClientcode += "</dtXml>";

            

            for (var i = 0; i < createuser.secbrancharr.Count; i++)
            {
                dtSecBranch += "<dtXml ";
                dtSecBranch += " BranchId=" + @"""" + createuser.secbrancharr[i] + @"""";
                dtSecBranch += " />";
            }
            
            dtSecBranch += "</dtXml>";




            return obj.SaveData(createuser,dtSponsorbankcode,dtCategorycode,dtUtilitycode,dtClientcode,dtSecBranch,IsActive,createuser.UserId,createuser.EntityId,createuser.AppId);


        }


        [HttpPost]
        [Route("api/SaveUser/BindUser")]
        public Dictionary<string, object> BindUser([FromBody] binduserdata _binduserdata )
        {
            return obj.BindUser(_binduserdata);
        }

        [HttpPost]
        [Route("api/SaveUser/GetExcelReport")]
        public Dictionary<string, object> GetExcelReport([FromBody] bindalldata _bindalldata)
        {
            return obj.GetExcelReport(_bindalldata);
        }

        [HttpGet]
        [Route("api/SaveUser/EditData/{UserId}")]
        public Dictionary<string, object> EditData(int UserId)
        {
            return obj.EditData(UserId);
        }

        [HttpPost]
        [Route("api/SaveUser/UpdateData")]
        public IEnumerable<createUser> UpdataData([FromBody] createUser createuser)
        {
            int IsActive = 0;
            string dtSponsorbankcode = "<dtXml>";
            string dtCategorycode = "<dtXml>";
            string dtUtilitycode = "<dtXml>";
            string dtClientcode = "<dtXml>";
            string dtSecBranch = "<dtXml>";

            if (createuser.chkactive == true)
            {
                IsActive = 1;

            }
            else
            {
                IsActive = 0;
            }



            for (var i = 0; i < createuser.sponsorcodearr.Count; i++)
            {
                dtSponsorbankcode += "<dtXml ";
                dtSponsorbankcode += " SponsorBankCodeId=" + @"""" + createuser.sponsorcodearr[i] + @"""";
                dtSponsorbankcode += " />";

            }

            dtSponsorbankcode += "</dtXml>";




            for (var i = 0; i < createuser.categorycodearr.Count; i++)
            {
                dtCategorycode += "<dtXml ";
                dtCategorycode += " CategoryCode=" + @"""" + createuser.categorycodearr[i] + @"""";
                dtCategorycode += " />";
            }

            dtCategorycode += "</dtXml>";




            for (var i = 0; i < createuser.utilitycodearr.Count; i++)
            {
                dtUtilitycode += "<dtXml ";
                dtUtilitycode += " UtilityCodeId=" + @"""" + createuser.utilitycodearr[i] + @"""";
                dtUtilitycode += " />";
            }

            dtUtilitycode += "</dtXml>";



            for (var i = 0; i < createuser.clientcodearr.Count; i++)
            {
                dtClientcode += "<dtXml ";
                dtClientcode += " ClientId=" + @"""" + createuser.clientcodearr[i] + @"""";
                dtClientcode += " />";
            }

            dtClientcode += "</dtXml>";



            for (var i = 0; i < createuser.secbrancharr.Count; i++)
            {
                dtSecBranch += "<dtXml ";
                dtSecBranch += " BranchId=" + @"""" + createuser.secbrancharr[i] + @"""";
                dtSecBranch += " />";
            }

            dtSecBranch += "</dtXml>";




            return obj.UpdateData(createuser, dtSponsorbankcode, dtCategorycode, dtUtilitycode, dtClientcode, dtSecBranch, IsActive, createuser.UserId, createuser.EntityId, createuser.AppId);


        }



    }
}
