using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLibrary;
using Encryptions;
using EntityDAL;

namespace SetupAPI.Models
{
    public static class BranchMastermethod
    {
        public static BindResponseData BindData(BindJsonData obj2)
        {
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                BindResponseData res = new BindResponseData();
                //CountResponseData res = new CountResponseData();

                List<GridBind> GridBindlist = new List<GridBind>();
                List<Count> BindCountlist = new List<Count>();


                var Result = dbcontext.MultipleResults("[dbo].[Sp_ProductMaster]").With<GridBind>().With<Count>().
                    Execute("@QueryType", "@PageCount", "@SearchText", "@AppId", "@EntityId", "BindGrid", obj2.PageCount, obj2.SearchText, obj2.AppId, Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(obj2.EntityId.Replace("_", "%"))));
         

                res.GridBindlist = Result[0].Cast<GridBind>().ToList();
                res.BindCountlist = Result[1].Cast<Count>().ToList();


                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public static GridExportExcelResponseData BindExportExcelData(BindExportExcelJsonData obj3)
        {
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                GridExportExcelResponseData res = new GridExportExcelResponseData();
                //CountResponseData res = new CountResponseData();

                List<GridExportExcelBind> GridBindExportlist = new List<GridExportExcelBind>();



                var Result = dbcontext.MultipleResults("[dbo].[Sp_ProductMaster]").With<GridExportExcelBind>().
                    Execute("@QueryType", "@AppId", "@EntityId", "ExportExcelBind", obj3.AppId, Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(obj3.EntityId.Replace("_", "%"))));


                res.GridBindExportlist = Result[0].Cast<GridExportExcelBind>().ToList();



                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static SaveResponseData SaveData(SaveJsonData obj)
        {
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                SaveResponseData res = new SaveResponseData();

                List<InsertData> InsertDatalist = new List<InsertData>();
                // List<Count> MandateCountlist = new List<Count>();

                var Result = dbcontext.MultipleResults("[dbo].[Sp_ProductMaster]").With<InsertData>().
                    Execute("@QueryType", "@ProductCode", "@ProductName", "@AppId", "@EntityId", "@UserId", "InsertData", obj.code, obj.name, obj.AppId, Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(obj.EntityId.Replace("_", "%"))), Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(obj.UserId.Replace("_", "%"))));


                res.InsertDatalist = Result[0].Cast<InsertData>().ToList();
                // res.MandateCountlist = Result[1].Cast<Count>().ToList();


                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static UpdateResponseData UpdateData(UpdateJsonData obj1)
        {
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                UpdateResponseData res = new UpdateResponseData();

                List<UpdateData> UpdateDatalist = new List<UpdateData>();
                // List<Count> MandateCountlist = new List<Count>();

                var Result = dbcontext.MultipleResults("[dbo].[Sp_ProductMaster]").With<UpdateData>().
                    Execute("@QueryType", "@ProductCode", "@ProductName", "@AppId", "@EntityId", "@UserId", "@ProductId", "UpdateData", obj1.code, obj1.name, obj1.AppId, Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(obj1.EntityId.Replace("_", "%"))), Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(obj1.UserId.Replace("_", "%"))), obj1.ProductId);


                res.UpdateDatalist = Result[0].Cast<UpdateData>().ToList();
                //res.UpdateDatalist = Result[0].Cast<UpdateData>().ToList();


                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}