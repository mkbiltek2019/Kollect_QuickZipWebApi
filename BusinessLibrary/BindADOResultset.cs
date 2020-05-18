using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
namespace BusinessLibrary
{

    public class BindADOResultset
    {
        string conn = ConfigurationManager.ConnectionStrings["Conn1"].ConnectionString;
        public DataSet FillDatasetWithParam(string spName, params string[] parameter)
        {
            DataSet ds = new DataSet();
            try
            {
                //conn = ConfigurationManager.ConnectionStrings["Conn"].ToString();
                ds = GetTablesWithParameter(spName, parameter);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
            return ds;
        }
        public DataSet GetTablesWithParameter(string spName, params string[] parameter)
        {
            DataSet ds = null;
           
            int Length = parameter.Length / 2;
            SqlParameter[] tableParameter = new SqlParameter[Length + 1];
            try
            {
                for (int counter = 0; counter < Length; counter++)
                {
                    tableParameter[counter] = new SqlParameter(parameter[counter], parameter[counter + Length]);
                }
                ds = ExecuteDataset( conn, CommandType.StoredProcedure, spName, tableParameter);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (ds.Tables.Count > 0)
                return ds;
            else
                return null;
        }
        public static DataSet ExecuteDataset(string connectionString, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connectionString == null || connectionString.Length == 0) throw new ArgumentNullException("connectionString");

            // Create & open a SqlConnection, and dispose of it after we are done
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //connection.Open(); comment by satish
                //add by satish pandey dated 11 Oct 2011

                SqlCommand Comm = new SqlCommand();
                Comm.CommandTimeout = 100000; // Add By Satish 19/NOv
                connection.Open();
                //end dated 11 Oct 2011

                // Call the overload that takes a connection in place of the connection string
                return ExecuteDataset(connection, commandType, commandText, commandParameters);
            }
        }
        public static DataSet ExecuteDataset(SqlConnection connection, CommandType commandType, string commandText, params SqlParameter[] commandParameters)
        {
            if (connection == null) throw new ArgumentNullException("connection");

            // Create a command and prepare it for execution
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 100000; // Add By Satish 114/Oct
            bool mustCloseConnection = false;
            PrepareCommand(cmd, connection, (SqlTransaction)null, commandType, commandText, commandParameters, out mustCloseConnection);

            // Create the DataAdapter & DataSet
            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
            {
                DataSet ds = new DataSet();

                // Fill the DataSet using default values for DataTable names, etc
                da.Fill(ds);

                // Detach the SqlParameters from the command object, so they can be used again
                cmd.Parameters.Clear();

                if (mustCloseConnection)
                    connection.Close();

                // Return the dataset
                return ds;
            }
        }
        private static void PrepareCommand(SqlCommand command, SqlConnection connection, SqlTransaction transaction, CommandType commandType, string commandText, SqlParameter[] commandParameters, out bool mustCloseConnection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandText == null || commandText.Length == 0) throw new ArgumentNullException("commandText");

            // If the provided connection is not open, we will open it
            if (connection.State != ConnectionState.Open)
            {
                mustCloseConnection = true;
                connection.Open();
            }
            else
            {
                mustCloseConnection = false;
            }

            // Associate the connection with the command
            command.Connection = connection;

            // Set the command text (stored procedure name or SQL statement)
            command.CommandText = commandText;

            // If we were provided a transaction, assign it
            if (transaction != null)
            {
                if (transaction.Connection == null) throw new ArgumentException("The transaction was rollbacked or commited, please provide an open transaction.", "transaction");
                command.Transaction = transaction;
            }

            // Set the command type
            command.CommandType = commandType;

            // Attach the command parameters if they are provided
            if (commandParameters != null)
            {
                AttachParameters(command, commandParameters);
            }
            return;
        }
        private static void AttachParameters(SqlCommand command, SqlParameter[] commandParameters)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (commandParameters != null)
            {
                foreach (SqlParameter p in commandParameters)
                {
                    if (p != null)
                    {
                        // Check for derived output value with no value assigned
                        if ((p.Direction == ParameterDirection.InputOutput ||
                            p.Direction == ParameterDirection.Input) &&
                            (p.Value == null))
                        {
                            p.Value = DBNull.Value;
                        }
                        command.Parameters.Add(p);
                    }
                }
            }
        }
        public DataTable ExcelImport(string Extension, string path, string FolderPath,FileUpload FileUpload)
        {
            try
            {
                DataTable dt = new DataTable();
                Dictionary<int, string> country = new Dictionary<int, string>();
                country.Add(1, "Maldives");
                string FilePath = FolderPath + path;
                string conString = string.Empty;
                switch (Extension)
                {
                    case ".xls": //Excel 97-03.
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 and above.
                        conString = ConfigurationManager.ConnectionStrings["Excel07ConString"].ConnectionString;
                        break;
                    default:

                        dt.Columns.Add("RES", typeof(string));
                        dt.Rows.Add(1);
                        return dt;
                }

                if (!Directory.Exists(FolderPath))   // CHECK IF THE FOLDER EXISTS. IF NOT, CREATE A NEW FOLDER.
                {
                    Directory.CreateDirectory(FolderPath);
                }
                if (File.Exists(FilePath))
                {
                    File.Delete(FilePath);
                }
                FileUpload.SaveAs(FilePath);
                conString = string.Format(conString, FilePath, true);
                OleDbConnection connExcel = new OleDbConnection(conString);
                OleDbCommand cmdExcel = new OleDbCommand();
                OleDbDataAdapter oda = new OleDbDataAdapter();
                cmdExcel.Connection = connExcel;
                connExcel.Close();
                connExcel.Open();
                System.Data.DataTable dtExcelSchema;
                dtExcelSchema = connExcel.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string SheetName = dtExcelSchema.Rows[0]["Table_Name"].ToString();
                //connExcel.Close();
                //connExcel.Open();
                cmdExcel.CommandText = "SELECT * From [" + SheetName + "]";
                oda.SelectCommand = cmdExcel;
                oda.Fill(dt);
                connExcel.Close();
                return dt;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
