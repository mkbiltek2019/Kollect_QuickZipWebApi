using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessLibrary;
using Encryptions;
using EntityDAL;
namespace MandateAnalysisWebAPI.Models.EMandateAnalysis
{
    public static class EMandateDataMethods
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

        public static GetMandateDataResponseData EmandateSearchData(GetMandateData objectname)
        {
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                GetMandateDataResponseData res = new GetMandateDataResponseData();

                List<GridData> GridDatalist = new List<GridData>();
                List<Count> MandateCountlist = new List<Count>();


                var Result = dbcontext.MultipleResults("[dbo].[sp_MandateAnalysis]").With<GridData>().With<Count>().
                    Execute("@QueryType", "@Month", "@Year", "@topVal", "GetMandateData_Emandate", objectname.Month, objectname.Year, objectname.Total);

                res.GridDatalist = Result[0].Cast<GridData>().ToList();
                res.MandateCountlist = Result[1].Cast<Count>().ToList();


                return res;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




    }
}