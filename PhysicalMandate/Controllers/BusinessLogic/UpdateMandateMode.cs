using BusinessLibrary;
using Encryptions;
using EntityDAL;
using PhysicalMandate.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhysicalMandate.Controllers.BusinessLogic
{
    public static class UpdateMandateMode
    {
        public static string UpdateMandateModeMethod(UpdateSelectModeModal MData)
        {
           
            QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
            List<UpdateSelectModeRes> UpdateSelectModeResList = new List<UpdateSelectModeRes>();
            try
            {              
                var Result = dbcontext.MultipleResults("[dbo].[sp_ENach]").With<UpdateSelectModeRes>().
                    Execute("@QueryType", "@MandateId", "@IsPhysical", "@Enach", "@MobileNo", "@EmailId", "@EMandatetype", "@Amount","@AppId", "@AmountRupeesInWords", "UpdateENach", Dbsecurity.Decrypt(MData.MandateId),MData.Physical,MData.EMandate,MData.MobileNumber,MData.Email,MData.EMandatetype,MData.Amount, Dbsecurity.Decrypt(MData.AppId),MData.AmountRupeesInWords);

                UpdateSelectModeResList= Result[0].Cast<UpdateSelectModeRes>().ToList();
            }
            catch (Exception ex) { }
            return UpdateSelectModeResList[0].done;
        }
    }
}