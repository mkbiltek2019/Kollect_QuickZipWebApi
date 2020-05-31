using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Encryptions;
using BusinessLibrary;
using EntityDAL;
using System.Data;

namespace SidebarLinksAPI.Controllers
{
    public class SideBarLinksController : ApiController
    {
        [Route("api/SideBarLinks/BindSidebarLinks/")]
        public Dictionary<string, object> BindSidebarLinks()
        {
            DataSet ds = new DataSet();
            try
            {
                using (QuickCheck_AngularEntities Dc = new QuickCheck_AngularEntities())
                {
                    // var _Links = Dc.Tbl_Links.Where(x => x.IsActive == true).Select(x => new { x.IconName, x.LinkID, x.LinkName, x.url, x.OrderNo, x.ParetmenuID });
                    //var _ParentLinks = Dc.TblParentMenus.Where(x => x.IsActive == true).Select(x => new { x.ParentID, x.ParentMenuName });

                    var _ParentLinks = Dc.Tbl_Links.Where(x => x.IsActive == true && x.ParetmenuID == 0).Select(x => new { LinkID = x.LinkID.ToString(), x.LinkName, url = x.url.Trim(), IsParent = 0, x.OrderNo, IconName = x.IconName }).Union
                        (Dc.TblParentMenus.Where(x => x.IsActive == true).Select(x => new { LinkID = x.ParentID.ToString(), LinkName = x.ParentMenuName, url = "", IsParent = 1, OrderNo = 3, IconName = "" })).OrderBy(x => new { x.OrderNo, x.LinkName }).ToList();
                    var _Links = Dc.Tbl_Links.Where(x => x.IsActive == true && x.ParetmenuID != 0).Select(x => new { x.LinkID, x.LinkName, url = x.url.Trim(), x.ParetmenuID });
                    DataTable dtLink = Common.LINQResultToDataTable(_Links);
                    dtLink.TableName = "Table1";
                    DataTable dtParentLinks = Common.LINQResultToDataTable(_ParentLinks);
                    dtParentLinks.TableName = "Table";

                    ds.Tables.Add(dtLink);
                    ds.Tables.Add(dtParentLinks);
                    return ClsJson.JsonMethods.ToJson(ds);
                }
            }
            catch (Exception ex)
            { var e = ex; return null; }
        }
    }
}
