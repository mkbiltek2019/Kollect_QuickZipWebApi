using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLibrary;
using Encryptions;
using EntityDAL;
namespace MandateAnalysisWebAPI.Models.PhysicalMandateAnalysis
{
    public static class PhysicalMandateDataMethods
    {

        public static GetMandateDataResponseData SetData()
        {
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                GetMandateDataResponseData res = new GetMandateDataResponseData();

                List<GetMonth> GetMonthlist = new List<GetMonth>();
                List<GetYear> GetYearlist = new List<GetYear>();


                var Result = dbcontext.MultipleResults("[dbo].[sp_MandateAnalysis]").With<GetMonth>().With<GetYear>().
                    Execute("@QueryType", "GetMonthAndYear");

                res.GetMonthlist = Result[0].Cast<GetMonth>().ToList();
                res.GetYearlist = Result[1].Cast<GetYear>().ToList();


                return res;
            }
            catch (Exception ex)
            {
                //return null;
                throw ex;
            }
        }

        public static GetMandateDataResponseData SearchData(GetMandateData objectname)
        {
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                GetMandateDataResponseData res = new GetMandateDataResponseData();

                List<GridData> GridDatalist = new List<GridData>();
                List<Count> MandateCountlist = new List<Count>();


                var Result = dbcontext.MultipleResults("[dbo].[sp_MandateAnalysis]").With<GridData>().With<Count>().
                    Execute("@QueryType", "@Month", "@Year", "@topVal", "GetMandateData", objectname.Month, objectname.Year, objectname.Total);

                res.GridDatalist = Result[0].Cast<GridData>().ToList();
                res.MandateCountlist = Result[1].Cast<Count>().ToList();


                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static GetMandateDataResponseData PhysicalSearchData(GetMandateData objectname)
        {
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                GetMandateDataResponseData res = new GetMandateDataResponseData();

                List<GridData> GridDatalist = new List<GridData>();
                List<Count> MandateCountlist = new List<Count>();


                var Result = dbcontext.MultipleResults("[dbo].[sp_MandateAnalysis]").With<GridData>().With<Count>().
                    Execute("@QueryType", "@Month", "@Year", "@topVal", "GetMandateData_Physical", objectname.Month, objectname.Year, objectname.Total);

                res.GridDatalist = Result[0].Cast<GridData>().ToList();
                res.MandateCountlist = Result[1].Cast<Count>().ToList();



                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static Dictionary<string, object> UpdateIsCancel1(UpdateToCancelData obj)
        {
            try
            {

                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();


                var Result1 = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_Mandate]").
                    Execute("@QueryType", "@MandateId", "@UserId", "UpdateIsCancel", Dbsecurity.Decrypt(obj.mandateId), Dbsecurity.Decrypt(obj.UserId)));
                Result1.Add("result", "1");

                return Result1;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}