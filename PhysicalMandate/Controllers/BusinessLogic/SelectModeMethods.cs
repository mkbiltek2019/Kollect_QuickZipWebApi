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
    public static class SelectModeMethods
    {
        public static EMandateTypeDataList Selectdata(SelectModeModal MData)
        {
            EMandateTypeDataList res = new EMandateTypeDataList();
            QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
            try
            {
                var Result = dbcontext.MultipleResults("[dbo].[Sp_GetMandatemodeData]").With<EMandateTypeDataModal>().
                    Execute("@QueryType", "@MandateId", "@Appid", "GetMandatemode", Dbsecurity.Decrypt(MData.MandateId), Dbsecurity.Decrypt(MData.AppId));

                res.EMandateTypeDataModalList = Result[0].Cast<EMandateTypeDataModal>().ToList();
            }
            catch(Exception ex) { }
            return res;
        }
    }
}