using BusinessLibrary;
using Encryptions;
using EntityDAL;
using PhysicalMandate.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace PhysicalMandate.Controllers.BusinessLogic
{
    public static class PrintHelper
    {
       
        public static string UpdateIsPHysical(string MandateId, string UserId,String AppId)
        {

            QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
            
            var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<UpdateNameRes>().
                    Execute("@QueryType", "@MandateId", "@AppId", "@UserId", "UpdateIsPHysical", MandateId, AppId,Dbsecurity.Decrypt(UserId));
            return "1";

        }
        public static DataSet GetShowForPDF(string MandateId, string AppId)
        {
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                List<ImagePathData> ImagePathDataList = new List<ImagePathData>();
                List<ShowData> ShowDataList = new List<ShowData>();
                var Result = dbcontext.MultipleResults("[dbo].[Sp_GetEmailData]").With<ShowData>().With<ImagePathData>().
                        Execute("@QueryType", "@Id", "@AppId",  "ShowData", MandateId, AppId);
                ///  return "1";

                //string query = "Sp_GetEmailData";
                //SqlCommand cmd = new SqlCommand(query, con);
                //cmd.CommandType = CommandType.StoredProcedure;
                //cmd.Parameters.AddWithValue("@QueryType", "ShowData");
                //cmd.Parameters.AddWithValue("@Id", MandateId);
                //SqlDataAdapter da = new SqlDataAdapter(cmd);
                ShowDataList= Result[0].Cast<ShowData>().ToList();
                ImagePathDataList = Result[1].Cast<ImagePathData>().ToList();
                var tb = new DataTable(typeof(ShowData).Name);

                PropertyInfo[] props = typeof(ShowData).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var prop in props)
                {
                    tb.Columns.Add(prop.Name, prop.PropertyType);
                }

                foreach (var item in ShowDataList)
                {
                    var values = new object[props.Length];
                    for (var i = 0; i < props.Length; i++)
                    {
                        values[i] = props[i].GetValue(item, null);
                    }

                    tb.Rows.Add(values);
                }
                var tbImagePathData = new DataTable(typeof(ImagePathData).Name);
                PropertyInfo[] propsImagePathData = typeof(ImagePathData).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (var prop in propsImagePathData)
                {
                    tbImagePathData.Columns.Add(prop.Name, prop.PropertyType);
                }

                foreach (var item in ImagePathDataList)
                {
                    var values = new object[propsImagePathData.Length];
                    for (var i = 0; i < propsImagePathData.Length; i++)
                    {
                        values[i] = propsImagePathData[i].GetValue(item, null);
                    }
                    tbImagePathData.Rows.Add(values);
                }


                DataSet dsData = new DataSet();
                dsData.Tables.Add(tb);
                dsData.Tables.Add(tbImagePathData);
                return dsData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static byte[] GetcutterImage(string AppID)
        {
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                List<CutterImag> CutterImagList = new List<CutterImag>();
                List<ShowData> ShowDataList = new List<ShowData>();
                var Result = dbcontext.MultipleResults("[dbo].[Sp_LogoImageData]").With<CutterImag>().
                        Execute("@QueryType",  "Getcutternew");
                CutterImagList= Result[0].Cast<CutterImag>().ToList();

                DataSet dsData = new DataSet();
             
                return (byte[])CutterImagList[0].ImageData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static byte[] GetRupeeIcon()
        {
            DataSet dsData = null;
            try
            {
                QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();
                List<CutterImag> CutterImagList = new List<CutterImag>();
                List<ShowData> ShowDataList = new List<ShowData>();
                var Result = dbcontext.MultipleResults("[dbo].[Sp_LogoImageData]").With<CutterImag>().
                        Execute("@QueryType", "GetRupeeIcon");
                CutterImagList = Result[0].Cast<CutterImag>().ToList();
                return (byte[])CutterImagList[0].ImageData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words+ " Only";
        }
        
    }
}