using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ClassLibraryClient
{
    public class ServerIP
    {
        DBMain db;
        string err;
        DataTable dt;
        public ServerIP()
        {
            db = new DBMain();
        }

        public string GetIP()
        {
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString();
            return "";
        }
        public bool Accessable()
        {
            // Access random table for check the connection
            try
            {
                dt = db.ExecuteQueryDataSet("Select * from ServerIP", CommandType.Text).Tables[0];
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
