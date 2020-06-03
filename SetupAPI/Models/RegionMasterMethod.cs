//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace SetupAPI.Models
//{
//    public class RegionMasterMethod
//    {

//    }
//}
using System;
using BusinessLibrary;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EntityDAL;
using Encryptions;
using System.Xml.Linq;
using System.Xml;
using System.Data;

namespace SetupAPI.Models.RegionMaster
{
    public class RegionMasterDataAccess
    {
        public static BindaResponseData GetData(GetDatavalue obj)
        {
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                BindaResponseData res = new BindaResponseData();

                List<GridBind> Gridlist = new List<GridBind>();
                List<Count> BindCountlist = new List<Count>();

                var Result = dbcontext.MultipleResults("[dbo].[Sp_RegionMaster]").With<GridBind>().With<Count>().
                    Execute("@QueryType", "@PageCount", "@SearchText", "@AppId", "@EntityId", "GetData1", obj.PageCount, obj.SearchText, obj.AppId, Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(obj.EntityId.Replace("_", "%"))));

                res.GridBindlist = Result[0].Cast<GridBind>().ToList();
                res.BindCountlist = Result[1].Cast<Count>().ToList();



                return res;
            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }
        }

        public  SaveResponseData1 SaveData(SaveJsonData1 obj)
        {
            try
            {


                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                SaveResponseData1 res = new SaveResponseData1();

                string[] sponsorcodearr = obj.sponsorcodearr;

                string StatesId = "";


                for (int i = 0; i < sponsorcodearr.Length; i++)
                {
                    StatesId += sponsorcodearr[i];
                    StatesId += ",";
                
                }
                StatesId = StatesId.TrimEnd(',');

                List<SaveJsonData1> InsertDatalist = new List<SaveJsonData1>();
                // List<Count> MandateCountlist = new List<Count>()
                var Result = dbcontext.MultipleResults("[dbo].[Sp_RegionMaster]").With<InsertData>().
                        Execute("@QueryType", "@RegionCode", "@RegionName", "@strTable", "@AppId", "@EntityId", "@UserId", "InsertData", obj.RegionCode, obj.RegionName, StatesId, obj.AppId, Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(obj.EntityId.Replace("_", "%"))), Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(obj.UserId.Replace("_", "%"))));


                res.InsertDatalist = Result[0].Cast<InsertData>().ToList();

                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public static GridExportExcelResponseData1 BindExportExcelData1(BindExportExcelJsonData1 obj3)
        {
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                GridExportExcelResponseData1 res = new GridExportExcelResponseData1();
                //CountResponseData res = new CountResponseData();

                List<GridExportExcelBind1> GridBindExportlist1 = new List<GridExportExcelBind1>();



                var Result = dbcontext.MultipleResults("[dbo].[Sp_RegionMaster]").With<GridExportExcelBind1>().
                    Execute("@QueryType", "@AppId", "@EntityId", "GetData", obj3.AppId, Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(obj3.EntityId.Replace("_", "%"))));


                res.GridBindExportlist1 = Result[0].Cast<GridExportExcelBind1>().ToList();



                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static editResponseData1 EditData(editdataJsonData1 obj3)
        {
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                editResponseData1 res = new editResponseData1();
                //CountResponseData res = new CountResponseData();

                List<editdata> editdataExportlist1 = new List<editdata>();



                var Result = dbcontext.MultipleResults("[dbo].[Sp_RegionMaster]").With<editdata>().
                    Execute("@QueryType", "@RegionId", "@AppId", "@EntityId", "editdata", obj3.RegionId,obj3.AppId, Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(obj3.EntityId.Replace("_", "%"))));


                res.editdataExportlist1 = Result[0].Cast<editdata>().ToList();



                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //paras
        public static BindResponseStateData GetState(GetDatavalue obj)
        {
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                BindResponseStateData res = new BindResponseStateData();

                List<Gridstate> GridBindStatelist = new List<Gridstate>();

                var Result = dbcontext.MultipleResults("[dbo].[Sp_RegionMaster]").With<Gridstate>().
                    Execute("@QueryType", "@AppId", "@EntityId", "GetState", obj.AppId, Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(obj.EntityId.Replace("_", "%"))));

                res.GridBindStatelist = Result[0].Cast<Gridstate>().ToList();




                return res;
            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }
        }
        //paras

        public static UpdateResponseData1 UpdateData1(UpdateJsonData1 obj1)
        {
            try
            {

                string[] sponsorcodearr = obj1.sponsorcodearr;

                string StatesId = "";


                for (int i = 0; i < sponsorcodearr.Length; i++)
                {
                    StatesId += sponsorcodearr[i];
                    StatesId += ",";

                }
                StatesId = StatesId.TrimEnd(',');

                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                UpdateResponseData1 res = new UpdateResponseData1();

                List<UpdateData1> UpdateDatalist = new List<UpdateData1>();
                // List<Count> MandateCountlist = new List<Count>();

                var Result = dbcontext.MultipleResults("[dbo].[Sp_RegionMaster]").With<UpdateData1>().
                    Execute("@QueryType", "@strTable", "@RegionCode", "@RegionName", "@AppId", "@EntityId", "@UserId", "@RegionId", "UpdateData", StatesId,obj1.RegionCode, obj1.RegionName, obj1.AppId, Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(obj1.EntityId.Replace("_", "%"))), Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(obj1.UserId.Replace("_", "%"))), obj1.RegionId);


                res.UpdateDatalist1 = Result[0].Cast<UpdateData1>().ToList();
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

