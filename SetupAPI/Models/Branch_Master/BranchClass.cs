using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Net.NetworkInformation;
using System.Net.Http;
using BusinessLibrary;
using EntityDAL;
using Encryptions;
using SetupAPI.Models.Branch_Master;
using System.Web.Http;

namespace SetupAPI.Models.Branch_Master
{
    public class BranchClass
    {
        QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities(); 

        //Grid Bind Data
        public Dictionary<string, object> GetData(GetDTCls gtdt)
        {
            try
            {
              
               var Data = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_BranchMaster]").With<BrachMasterAttribute>().With<PageCountAtrribute>().Execute("@QueryType", "@AppId", "@EntityId", "@PageCount", "@SearchText", "GetData",gtdt.AppId, Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(gtdt.EntityId.Replace("_", "%"))),gtdt.PageCount, gtdt.SearchText));
                    return Data;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
              // RegionID get
        public Dictionary<string, object> RegionID()
        {
            try
            {

                var Data = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_BranchMaster]").With<RegionClsAttribute>().Execute("@QueryType", "GetRegion"));
                return Data;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //save data
        public Dictionary<string, object> AddData(InsertjsonData insdata)
        {
            try
            {

                var Data = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_BranchMaster]").With<returndata>().Execute("@QueryType", "@BranchCode", "@BranchName", "@RegionId","@AppId", "@EntityId", "@CreatedBy", "SaveData", insdata.BranchCode, insdata.BranchName, insdata.RegionId, insdata.AppId, Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(insdata.EntityId.Replace("_", "%"))), Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(insdata.UserId.Replace("_", "%")))));
                return Data;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //updatedata
        public Dictionary<string, object> Updata(upjsondata attr)
        {
            try
            {

                var Data = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_BranchMaster]").With<returndata>().Execute("@QueryType", "@BranchCode", "@BranchName", "@RegionId", "@AppId", "@EntityId", "@UserId", "@BranchId", "UpdateData", attr. BranchCode, attr.BranchName, attr.RegionId,attr.AppId, Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(attr.EntityId.Replace("_", "%"))), Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(attr.UserId.Replace("_", "%"))),attr.BranchId));
                return Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Excel Attribute
        public Dictionary<string, object> Exportexcel(Excelparameter exp)
        {
            try
            {

                var Data = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_BranchMaster]").With<ExcelAttribute>().Execute("@QueryType", "@AppId", "@EntityId", "ExportExcelBind", exp.AppId, Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(exp.EntityId.Replace("_", "%")))));
                return Data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}


