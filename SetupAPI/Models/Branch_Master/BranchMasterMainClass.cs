using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLibrary;
using EntityDAL;
using SetupAPI.Models.Branch_Master;
namespace SetupAPI.Models.Branch_Master
{
    public class BranchMasterMainClass
    {
        QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
        //  List<BrachMasterAttribute> dataList = new List<BrachMasterAttribute>();
      //  BrachMasterAttribute cnjd = new BrachMasterAttribute();
        public Dictionary<string,object> GetData()
        {
            try
            {
                var Data = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_BranchMaster]").With<BrachMasterAttribute>().Execute("@QueryType", "GetData"));
             
                return Data;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}