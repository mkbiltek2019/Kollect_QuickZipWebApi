using BusinessLibrary;
using EntityDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BankValidationWebAPI.Models
{
    public static class BankValidationMethods
    {

        public static bankValidationResponseData bankvalida(BankValidationModal MData)
        {
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                bankValidationResponseData res = new bankValidationResponseData();

                List<IFSCResultModal> IFSCResultModallist = new List<IFSCResultModal>();
                List<MICRResultModal> MICRResultModallist = new List<MICRResultModal>();
                List<BankNameModal> BankNameModallist = new List<BankNameModal>();
                List<MandateDataModal> MandateDataModallist = new List<MandateDataModal>();
                List<MandateActivityDataModal> MandateActivityDataModallist = new List<MandateActivityDataModal>();
                List<IsLiveInNACHModal> IsLiveInNACHModallist = new List<IsLiveInNACHModal>();
                List<EMandateDataModal> EMandateDataModallist = new List<EMandateDataModal>();
                List<EMandateTypeDataModal> EMandateTypeDataModallist = new List<EMandateTypeDataModal>();

                //ds = CommonManger.FillDatasetWithParam("Sp_Mandate", "@QueryType", "@MandateId", "@UserId", "UpdateIsFirstvalidation", DbSecurity.Decrypt(mandateId), DbSecurity.Decrypt(UserId));
                var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<IFSCResultModal>().With<MICRResultModal>().With< MandateDataModal>().With<BankNameModal>().With<MandateActivityDataModal>().With<IsLiveInNACHModal>().With<EMandateDataModal>().With<EMandateTypeDataModal>().
                    Execute("@QueryType", "@MandateId", "@Appid", "@UserId", "UpdateIsFirstvalidation", MData.MandateId, MData.AppId, MData.UserId);
                Result.FirstOrDefault().Cast<IFSCResultModal>().ToList();
                res.IFSCResultModallist = Result[0].Cast<IFSCResultModal>().ToList();


                //foreach (var Logindata in Result)
                //{
                res.IFSCResultModallist = Result[0].Cast<IFSCResultModal>().ToList();
                res.MICRResultModallist = Result[1].Cast<MICRResultModal>().ToList();                
                res.MandateDataModallist = Result[2].Cast<MandateDataModal>().ToList();
                res.BankNameModallist = Result[3].Cast<BankNameModal>().ToList();
                res.MandateActivityDataModallist = Result[4].Cast<MandateActivityDataModal>().ToList();
                res.IsLiveInNACHModallist = Result[5].Cast<IsLiveInNACHModal>().ToList();
                res.EMandateDataModallist = Result[6].Cast<EMandateDataModal>().ToList();
                res.EMandateTypeDataModallist = Result[7].Cast<EMandateTypeDataModal>().ToList();


                //}
               
                return res;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}