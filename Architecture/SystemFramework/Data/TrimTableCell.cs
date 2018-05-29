using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace CCT.SystemFramework.Data
{
    public class TrimTableCell
    {
        public static DataTable Trim(DataTable dt)
        {
            List<int> cols = new List<int>();

            foreach (DataColumn col in dt.Columns)
            {
                if (col.DataType == typeof(string))
                {
                    cols.Add(col.Ordinal);
                }
            }

            foreach (DataRow dr in dt.Rows)
            {
                foreach (int i in cols)
                {
                    dr[i] = dr[i].ToString().Trim();
                }
            }

            return dt;
        }

        public static DataRow Trim(DataRow dr)
        {
            List<int> cols = new List<int>();

            foreach (DataColumn col in dr.Table.Columns)
            {
                if (col.DataType == typeof(string))
                {
                    cols.Add(col.Ordinal);
                }
            }

            foreach (int i in cols)
            {
                dr[i] = dr[i].ToString().Trim();
            }

            return dr;
        }
    }
}
