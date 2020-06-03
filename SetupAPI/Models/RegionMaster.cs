using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SetupAPI.Models.RegionMaster
{
    public class RegionMaster
    {

    }

    public class GetDatavalue
    {

        public string PageCount { get; set; }
        public string SearchText { get; set; }
        public string EntityId { get; set; }
        public string AppId { get; set; }


    }

    public class GridBind
    {
        public Nullable<Int64> Srno { get; set; }
        public Nullable<Int64> RegionId { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string StateId { get; set; }
        public string StateName { get; set; }
        public Nullable<Int64> Createdby { get; set; }
        public DateTime Createdon { get; set; }
        public Nullable<Int64> Updatedby { get; set; }

    }

    public class BindaResponseData
    {

        public List<GridBind> GridBindlist { get; set; }
        public List<Count> BindCountlist { get; set; }
    }

    //paras
    public class Gridstate
    {

        public Nullable<Int64> StateId { get; set; }
        public string StateName { get; set; }

    }

    public class BindResponseStateData
    {

        public List<Gridstate> GridBindStatelist { get; set; }
    }
    //paras

    public class Count
    {

        public Nullable<Int32> IsNxtRequired { get; set; }
        public string TR { get; set; }


    }

    public class InsertData
    {
        public Nullable<Int32> value { get; set; }

    }

    public class SaveJsonData1
    {

        //public Int32 ProductId { get; set; }
        public string RegionName { get; set; }
        public string[] sponsorcodearr { get; set; }
        public string RegionCode { get; set; }
        public string UserId { get; set; }
        public string EntityId { get; set; }
        public string AppId { get; set; }



    }


    /// <summary>
    /// Save data
    /// </summary>

    public class SaveResponseData1
    {
        public List<InsertData> InsertDatalist { get; set; }


    }

    public class UpdateJsonData1
    {

        //public Int32 ProductId { get; set; }
        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        public string[] sponsorcodearr { get; set; }
        public string UserId { get; set; }
        public string EntityId { get; set; }
        public string AppId { get; set; }
        public string RegionId { get; set; }



    }

    public class UpdateData1
    {
        //public Int32 ProductId { get; set; }
        //public string code { get; set; }
        //public string name { get; set; }
        public Nullable<Int32> value { get; set; }

    }



    public class UpdateResponseData1
    {


        public List<UpdateData1> UpdateDatalist1 { get; set; }

    }

    public class BindExportExcelJsonData1
    {

        public string EntityId { get; set; }
        public string AppId { get; set; }



    }

    public class GridExportExcelBind1
    {
        public Nullable<Int64> Srno { get; set; }

        public string RegionCode { get; set; }
        public string RegionName { get; set; }
        //public string StateId { get; set; }
        public string StateName { get; set; }
        //public string Createdon { get; set; }
        //public string Updatedby { get; set; }

    }
    public class GridExportExcelResponseData1
    {

        public List<GridExportExcelBind1> GridBindExportlist1 { get; set; }


    }

    public class editdata
    {

        public string StateName { get; set; }
        public Nullable<Int64> StateId { get; set; }
    }

    public class editdataJsonData1
    {

        public string RegionId { get; set; }
        public string EntityId { get; set; }
        public string AppId { get; set; }



    }
    public class editResponseData1
    {

        public List<editdata> editdataExportlist1 { get; set; }


    }

}