using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EntityDAL;
using Encryptions;
using BusinessLibrary;
using SetupAPI.Models;
using System.Data;

namespace SetupAPI.Controllers
{
    public class LinkSetupController : ApiController
    {
        [HttpPost]
        [Route("api/LinkSetup/BindGrid/")]
        public Dictionary<string, object> BindGrid(Initial_LinkSetup IL)
        {
            DataSet ds = new DataSet();
            try
            {
                using (QuickCheck_AngularEntities Dc = new QuickCheck_AngularEntities())
                {
                    int Min_Value = (IL.Maxcount) * ((IL.PageCount) - 1) + 1;
                    int Max_Value = (IL.Maxcount) * (IL.PageCount);

                    var _LinkList =
            from A in Dc.Tbl_Links
            join B in Dc.TblParentMenus on new { ParetmenuID = A.ParetmenuID } equals new { ParetmenuID = B.ParentID } into B_join
            from B in B_join.DefaultIfEmpty()
            where (A.LinkName.Contains(IL.SearchValue) || B.ParentMenuName.Contains(IL.SearchValue))
            select new
            {
                LinkID = A.LinkID,
                LinkName = A.LinkName,
                url = A.url,
                Purpose = A.Purpose,
                OrderNo = A.OrderNo,
                IconName = A.IconName,
                IsActive = A.IsActive,
                IsDeleted = A.IsDeleted,
                CreatedOn = (A.CreatedOn),//.ToString("dd-MM-yyyy").Substring(0, 10) == "01-01-1900",
                ModifiedOn = A.ModifiedOn,
                ParetmenuID = A.ParetmenuID,
                ParentMenuName = (B.ParentMenuName ?? "")
            };

                    var result = _LinkList.AsEnumerable().Select((x, RowNumber) => new
                    {
                        RowNumber = RowNumber + 1,
                        x.LinkID,
                        x.LinkName,
                        x.url,
                        x.Purpose,
                        x.OrderNo,
                        x.IsActive,
                        x.CreatedOn,
                        x.ParentMenuName,
                        x.ParetmenuID,
                        RoleName = string.Join(",", Dc.TblRoleLinkBindings.Join(Dc.TblRoleMasters, a => a.RoleID, b => b.RoleID, (a, b) => new { a.LinkID, b.RoleName }).Where(p => p.LinkID == x.LinkID).Select(b => b.RoleName))
                    }).Where(x => x.RowNumber >= Min_Value && x.RowNumber <= Max_Value);

                    DataTable dtLink = Common.LINQResultToDataTable(result);
                    dtLink.TableName = "Table";

                    var _iconNames = (from LS in Dc.Tbl_Icon
                                      select LS);
                    DataTable dtIcon = Common.LINQResultToDataTable(_iconNames);
                    dtIcon.TableName = "Table1";

                    var _RoleNames = (from LS in Dc.TblRoleMasters
                                      where LS.IsActive == true
                                      select new { LS.RoleID, RoleName = (LS.RoleName) + " " + (LS.RoleType == 1 ? "(Bank-Roles)" : LS.RoleType == 2 ? "(Corporate-Roles)" : "(Yoeki-Roles)") });

                    DataTable dtRole = Common.LINQResultToDataTable(_RoleNames);
                    dtRole.TableName = "Table2";

                    var _Parent = (from LS in Dc.TblParentMenus
                                   where LS.IsActive == true
                                   select new { LS.ParentID, LS.ParentMenuName });
                    DataTable dtParent = Common.LINQResultToDataTable(_Parent);
                    dtParent.TableName = "Table3";


                    ds.Tables.Add(dtLink);
                    ds.Tables.Add(dtIcon);
                    ds.Tables.Add(dtRole);
                    ds.Tables.Add(dtParent);
                    return ClsJson.JsonMethods.ToJson(ds);
                }
            }
            catch (Exception ex)
            { return null; }
        }

        [HttpGet]
        [Route("api/LinkSetup/BindEditData/{LinkID}")]
        public Dictionary<string, object> BindEditData(string LinkID)
        {
            DataSet ds = new DataSet();
            try
            {
                using (QuickCheck_AngularEntities Dc = new QuickCheck_AngularEntities())
                {
                    var _LinkList = (from LS in Dc.Tbl_Links
                                     where LS.IsActive == true && LS.LinkID.ToString() == LinkID
                                     select LS).ToList();
                    DataTable dtLink = Common.LINQResultToDataTable(_LinkList);
                    dtLink.TableName = "Table";

                    var _RoleLinkBindings = (from LS in Dc.TblRoleLinkBindings
                                             join RM in Dc.TblRoleMasters on LS.RoleID equals RM.RoleID
                                             where LS.IsActive == true && LS.LinkID.ToString() == LinkID
                                             select new { LS.RoleID, RM.RoleName }).ToList();
                    DataTable dtRoleLinkBindings = Common.LINQResultToDataTable(_RoleLinkBindings);
                    dtRoleLinkBindings.TableName = "Table1";

                    ds.Tables.Add(dtLink);
                    ds.Tables.Add(dtRoleLinkBindings);

                    return ClsJson.JsonMethods.ToJson(ds);
                }

            }
            catch (Exception ex)
            { return null; }
        }

        [HttpGet]
        [Route("api/LinkSetup/SaveParentmenuItem/{ParentmenuItem}")]
        public Dictionary<string, object> SaveParentmenuItem(string ParentmenuItem)
        {
            try
            {
                using (QuickCheck_AngularEntities Dc = new QuickCheck_AngularEntities())
                {
                    var _ParentName = (from p in Dc.TblParentMenus
                                       where p.ParentMenuName == ParentmenuItem
                                       select ParentmenuItem).ToList();

                    if (_ParentName.Count == 0 && (ParentmenuItem != null || ParentmenuItem != ""))
                    {
                        TblParentMenu TP = new TblParentMenu();
                        TP.CreatedBy = 2;
                        TP.CreatedOn = DateTime.Now;
                        TP.IsActive = true;
                        TP.IsDeleted = false;
                        TP.OrderNo = (Dc.TblParentMenus.DefaultIfEmpty().Max(p => p == null ? 0 : p.OrderNo) + 1);// (Dc.TblParentMenus.Select(x => x.OrderNo == null ? 0 : x.OrderNo).Max() + 1);
                        TP.ParentMenuName = ParentmenuItem;
                        Dc.TblParentMenus.Add(TP);
                        Dc.SaveChanges();
                    }
                    var _Parent = (from LS in Dc.TblParentMenus
                                   where LS.IsActive == true
                                   select new { LS.ParentID, LS.ParentMenuName });
                    DataTable dtParent = Common.LINQResultToDataTable(_Parent);
                    dtParent.TableName = "Table";
                    return ClsJson.JsonMethods.ToJson(dtParent);
                }
            }
            catch (Exception ex)
            { return null; }
        }


        [HttpPost]
        [Route("api/LinkSetup/SaveLink/")]
        public int SaveLink(LinkSetup_InsertModel LI)
        {
            DataSet ds = new DataSet();
            try
            {
                using (QuickCheck_AngularEntities Dc = new QuickCheck_AngularEntities())
                {
                    
                    if (LI.LinkID == 0)
                    {
                        Tbl_Links TL = new Tbl_Links();
                        TL.LinkName = LI.LinkName;
                        TL.IsActive = LI.IsActive;
                        TL.IconName = LI.IconName;
                        TL.OrderNo = LI.OrderNo;
                        TL.Purpose = LI.Purpose;
                        TL.url = LI.url;
                        TL.ParetmenuID = LI.ParentMenuID;
                        TL.IsDeleted = !LI.IsActive;
                        TL.CreatedOn = DateTime.Now;
                        TL.ModifiedOn = new DateTime(1900, 1, 1);
                        Dc.Tbl_Links.Add(TL);
                        Dc.SaveChanges();
                    }
                    else
                    {
                        var _R = Dc.Tbl_Links.Where(d => d.LinkID == LI.LinkID);
                        foreach (var k in _R)
                        {
                            k.LinkName = LI.LinkName;
                            k.IsActive = LI.IsActive;
                            k.IconName = LI.IconName;
                            k.OrderNo = LI.OrderNo;
                            k.Purpose = LI.Purpose;
                            k.url = LI.url;
                            k.ParetmenuID = LI.ParentMenuID;
                            k.IsDeleted = !LI.IsActive;
                           // k.CreatedOn = DateTime.Now;
                            k.ModifiedOn = DateTime.Now;
                            
                        }
                        Dc.SaveChanges();
                    }
                }
                return 1;
            }
            catch (Exception ex)
            { return 0; }
        }

    }
}
