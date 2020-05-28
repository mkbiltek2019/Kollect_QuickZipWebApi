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

namespace HistoricalMandateWebAPI.Models.HistoricalMandate
{
    public class HistoricalMandate
    {
        QuickCheck_AngularEntities dbcontext = new QuickCheck_AngularEntities();

        public gridlist BindGrid(MandateDetails data)
        {
            try
            {
                gridlist res = new gridlist();
                List<BindGrid> BindGridlist = new List<BindGrid>();
          //      var Result = Common.Getdata(dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<BindGrid>().Execute("@QueryType", "@UserId", "@AppId", "@ToDate"
          //, "@FromDate", "grdMandateDataDateWise", Dbsecurity.Decypt(HttpContext.Current.Server.UrlDecode(data.UserId.Replace("_", "%"))), data.AppId, data.ToDate, data.FromDate));
          //      BindGridlist = Result.Cast<BindGrid>().ToList();
                var Result = dbcontext.MultipleResults("[dbo].[Sp_Mandate]").With<BindGrid>().Execute("@QueryType", "@UserId", "@AppId", "@ToDate"
         , "@FromDate", "grdMandateDataDateWise", Dbsecurity.Decrypt(HttpContext.Current.Server.UrlDecode(data.UserId.Replace("_", "%"))), data.AppId, data.ToDate, data.FromDate);
                BindGridlist = Result[0].Cast<BindGrid>().ToList();

               for(var i=0;i< BindGridlist.Count;i++)
                {
                    BindGridlist[i].AcNo = Dbsecurity.Decrypt(Convert.ToString(BindGridlist[i].AcNo));
                    BindGridlist[i].Amount_Numeric = Dbsecurity.Decrypt(Convert.ToString(BindGridlist[i].Amount_Numeric));
                }
                res.BindGridlist = BindGridlist;

                return res;
            }


            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}