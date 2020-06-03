using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UploadMandate.Models;
using Encryptions;
using BusinessLibrary;
using EntityDAL;
namespace UploadMandate.Controllers
{
    public class ViewCropimage
    {
        public IEnumerable<ResponseFlag> ViewCropImage(UploadMandateModel context)
        {
            List<ResponseFlag> common = new List<ResponseFlag>();
            ResponseFlag Flag = new ResponseFlag();
            string ID = Convert.ToString(Dbsecurity.Decrypt(context.MdtID));
            string No = Convert.ToString(Dbsecurity.Decrypt(context.RefrenceNo));
            string APPId = Convert.ToString(Dbsecurity.Decrypt(context.AppID));
            string UserId = Convert.ToString(Dbsecurity.Decrypt(context.UserId));
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                List<BindGrid> dataList1 = new List<BindGrid>();
                var Result2 = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<BindGrid>().Execute("@QueryType", "@MandateId", "@AppId", "EditMandate",ID ,APPId);
                Flag.IsPrint = dataList1.Cast<BindGrid>().ToList().Select(x => x.IsPrint).First().ToString();
                Flag.IsScan = dataList1.Cast<BindGrid>().ToList().Select(x => x.IsScan).First().ToString();
                Flag.Flag = "1";
                common.Add(Flag);
            }
            catch (Exception ex) { throw ex; }
            return common;
        }

    }
}