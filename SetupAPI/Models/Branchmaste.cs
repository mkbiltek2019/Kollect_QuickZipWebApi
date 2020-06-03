using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SetupAPI.Models
{
    public class Branchmaste
    {

    }

    public class GridBind
    {
        public Nullable<Int64> Srno { get; set; }
        public Nullable<Int64> ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Createdby { get; set; }
        public string Createdon { get; set; }
        public string Updatedby { get; set; }
   
    }
    public class GridExportExcelBind
    {
        public Nullable<Int64> Srno { get; set; }

        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string Createdby { get; set; }
        public string Createdon { get; set; }
        public string Updatedby { get; set; }

    }
    public class GridExportExcelResponseData
    {

        public List<GridExportExcelBind> GridBindExportlist { get; set; }


    }


    public class BindResponseData
    {

        public List<GridBind> GridBindlist { get; set; }
        public List<Count> BindCountlist { get; set; }

    }

    public class Count
    {

        public Nullable<Int32> IsNxtRequired { get; set; }
        public string TR { get; set; }


    }

    //public class CountResponseData
    //{

    //    public List<Count> BindCountlist { get; set; }

    //}
    //paras
    public class InsertData
    {
        //public Int32 ProductId { get; set; }
        //public string code { get; set; }
        //public string name { get; set; }
        public Nullable<Int32> value { get; set; }

    }

    public class SaveJsonData
    {

        //public Int32 ProductId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string UserId { get; set; }
        public string EntityId { get; set; }
        public string AppId { get; set; }



    }

    public class BindJsonData
    {

        //public Int32 ProductId { get; set; }

        public string PageCount { get; set; }
        public string SearchText { get; set; }
        public string EntityId { get; set; }
        public string AppId { get; set; }



    }

    public class BindExportExcelJsonData
    {

        public string EntityId { get; set; }
        public string AppId { get; set; }



    }
    public class UpdateJsonData
    {

        //public Int32 ProductId { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public string UserId { get; set; }
        public string EntityId { get; set; }
        public string AppId { get; set; }
        public string ProductId { get; set; }



    }


    public class SaveResponseData
    {


        public List<InsertData> InsertDatalist { get; set; }

    }

    public class UpdateData
    {
        //public Int32 ProductId { get; set; }
        //public string code { get; set; }
        //public string name { get; set; }
        public Nullable<Int32> value { get; set; }

    }



    public class UpdateResponseData
    {


        public List<UpdateData> UpdateDatalist { get; set; }

    }
    //paras
}