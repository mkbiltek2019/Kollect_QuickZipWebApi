using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace BusinessLibrary
{
   public  class ClsJson
    {
        #region "Convert Datatable and dataset into Json data"

        public static class JsonMethods
        {
            private static List<Dictionary<string, object>>
                RowsToDictionary(DataTable table)
            {
                List<Dictionary<string, object>> objs =
                    new List<Dictionary<string, object>>();
                foreach (DataRow dr in table.Rows)
                {
                    Dictionary<string, object> drow = new Dictionary<string, object>();
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        drow.Add(table.Columns[i].ColumnName, dr[i]);
                    }
                    objs.Add(drow);
                }

                return objs;
            }

            public static Dictionary<string, object> ToJson(DataTable table)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                d.Add(table.TableName, RowsToDictionary(table));
                return d;
            }

            public static Dictionary<string, object> ToJson(DataSet data)
            {
                Dictionary<string, object> d = new Dictionary<string, object>();
                foreach (DataTable table in data.Tables)
                {
                    d.Add(table.TableName, RowsToDictionary(table));
                }
                return d;
            }
        }

        #endregion
    }
}
