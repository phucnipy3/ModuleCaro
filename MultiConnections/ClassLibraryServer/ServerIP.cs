using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ClassLibraryServer
{
    public class ServerIP
    {
        DBMain db;
        string err;
        public ServerIP()
        {
            //db = new DBMain();
        }

        public void DeleteIP()
        {
            //string str = "delete from ServerIP";
            //db.MyExecuteNonQuery(str, CommandType.Text);
        }

        public void PushIP(string ip)
        {
            //string str = $"insert into ServerIP values('{ip}')";
            //db.MyExecuteNonQuery(str, CommandType.Text);
        }
    }
}
