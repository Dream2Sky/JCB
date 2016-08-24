using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.OleDb;

namespace Coffee.ImportData2.Web.Models
{
    public class ExcelReader
    {
        public static DataTable Read(string filepath)
        {
            string strCon = "Provider=Microsoft.Jet.OLEDB.4.0;" +
                            "Extended Properties=Excel 8.0;" +
                            "data source=" + filepath;

            DataTable dt = null;
            OleDbConnection conn = null;

            try
            {
                conn = new OleDbConnection(strCon);
                conn.Open();
            }
            catch (Exception ex)
            {
                LogHelper.Log.Write("can't open the file " + filepath + ";");
                LogHelper.Log.Write(ex.Message);
                LogHelper.Log.Write(ex.StackTrace);

                return null;
            }

            DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });

            string sql = "SELECT * FROM [" + dtSchema.Rows[0].Field<string>("TABLE_NAME") + "]";

            OleDbCommand comm = new OleDbCommand();
            comm.Connection = conn;
            comm.CommandText = sql;

            OleDbDataAdapter oleadapter = new OleDbDataAdapter(comm);
            DataSet ds = new DataSet();
            oleadapter.Fill(ds);

            dt = ds.Tables[0];

            dt.Rows.RemoveAt(0);

            conn.Close();
            return dt;
        }
    }
}