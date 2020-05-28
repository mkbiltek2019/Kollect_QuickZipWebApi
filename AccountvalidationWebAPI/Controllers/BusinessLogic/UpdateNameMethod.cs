using AccountvalidationWebAPI.Models;
using BusinessLibrary;
using Encryptions;
using EntityDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountvalidationWebAPI.Controllers.BusinessLogic
{
    public static class UpdateNameMethod
    {
        public static string UpdateName(UpdateName Data)
        {
           
            QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
            //int value = CommonManger.IntMethodWithParam("sp_Payment", "@QueryType", "@Type",
            //                 "@MandateId",
            //                "UpdateName", rdoName.SelectedValue, ViewState["id"].ToString());
            var Result = dbcontext.MultipleResults("[dbo].[sp_Payment]").With<UpdateNameRes>().
                    Execute("@QueryType", "@Type","@MandateId", "@Appid", "UpdateName",Data.RadioValue, Dbsecurity.Decypt(Data.MandateId), Dbsecurity.Decypt(Data.AppId));
            return "1";
                 
        }
    }
}