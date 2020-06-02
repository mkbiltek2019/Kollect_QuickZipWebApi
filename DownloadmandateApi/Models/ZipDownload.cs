using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DownloadmandateApi.Models
{
    public class ZipDownload
    {
        public string UserId { get; set; }
        public string strToDate { get; set; }
        public string strFromDate { get; set; } //cv date
        public string BankName { get; set; }
        public string[] JPGPath { get; set; }
        public string[] TIPPath { get; set; }
        public string[] strTable { get; set; }
        
    }

    public class ZipGrid
    {
        public string Message_ID { get; set; }
        public string Message_Creation_Date_Time { get; set; }
        public string Initiating_Party_ID { get; set; }
        public string Instructing_Member_ID { get; set; }
        public string Instructed_Agent_Member_ID { get; set; }
        public string Instructed_Agent_Name { get; set; }
        public string Mandate_Request_ID { get; set; }
        public string Mandate_Category { get; set; }
        public string Mandate_Category_Name { get; set; }
        public string TXN_type { get; set; }
        public string Recurring_or_One_Off { get; set; }
        public string Frequency { get; set; }
        public string First_Collection_Date { get; set; }


        //public string Until_Cancelled { get; set; } //check


        public string Final_Collection_Date { get; set; }
        public string Collection_Amount { get; set; }
        public string Maximum_Amount { get; set; }
        public string Name_of_Utility { get; set; }
        public string Utility_Code { get; set; }

        public string Sponsor_Bank_Code { get; set; }
        public string Debtor_Name { get; set; }
        public string Consumer_Reference_No { get; set; }
        public string Scheme_Reference_No { get; set; }
        public string Debtor_Telephone_No { get; set; }
        public string Debtor_Mobile_No { get; set; }
        public string Debtor_Email_Add { get; set; }


        public string Debtor_other_details { get; set; }
        public string Destination_Bank_Account_Number { get; set; }
        public string Destination_Bank_Account_Type { get; set; }
        public string Destination_Bank_IFSC { get; set; }
        public string Destination_Bank_Name { get; set; } //MANDATE_IMAGE_NAME
        public string MANDATE_IMAGE_NAME { get; set; }
        public string Action { get; set; }
        public Nullable<Int64> Mandate_Id { get; set; }
    }
}