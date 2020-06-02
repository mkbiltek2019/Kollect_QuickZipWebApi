using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityDAL;
using BusinessLibrary;
//using Encryptions;


namespace PresentmentWebApi.Models.Legacy
{
    public class LegacyHeaderBind
    {
        QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();

        //public static LegacyHeaderResponseData bankvalida(LegacyHeaderModal MData)
        //{
        //    try
        //    {
        //        LegacyHeaderResponseData res = new LegacyHeaderResponseData();


        //        //ds = CommonManger.FillDatasetWithParam("Sp_Mandate", "@QueryType", "@MandateId", "@UserId", "UpdateIsFirstvalidation", DbSecurity.Decrypt(mandateId), DbSecurity.Decrypt(UserId));
        //        var Result = dbcontext.MultipleResults("[dbo].[Sp_Uploaddata]").With<EMandateTypeDataModal>().
        //            Execute("@QueryType", "@MandateId", "@Appid", "@UserId", "UpdateIsFirstvalidation", MData.MandateId, MData.AppId, MData.UserId);
        //        Result.FirstOrDefault().Cast<IFSCResultModal>().ToList();
        //        res.IFSCResultModallist = Result[0].Cast<IFSCResultModal>().ToList();


        //        //}

        //        return res;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
    }
}