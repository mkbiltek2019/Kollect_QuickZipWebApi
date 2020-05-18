using System;
using System.Collections.Generic;
using System.Collections;

namespace EntityDAL
{
    public class Common
    {
        public static Dictionary<string, object> Getdata(List<IEnumerable> results)
        {
            Dictionary<string, object> d = new Dictionary<string, object>();
            string value = "Table";
            for (int i = 0; i < results.Count; i++)
            {
                if (i == 0)
                {
                    d.Add(value, results[i]);
                }
                else
                {
                    d.Add(value + i, results[i]);
                }
            }

            return d;
        }

    }
}