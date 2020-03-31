using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace ClassLibraryServer
{
    public class DBMain
    {
        string ConnStr = @"Data Source=PHUCNI\SQLEX;Initial Catalog=CaroModuleDB;Integrated Security=False;User=client;password=clientcaro";  
        SqlConnection conn = null;       
        SqlCommand comm = null;         
        SqlDataAdapter da = null;       
        public DBMain()       
        {           
            conn = new SqlConnection(ConnStr);  
            comm = conn.CreateCommand();        
        }
        public DataSet ExecuteQueryDataSet(string strSQL, CommandType ct)       
        {

            try
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                conn.Open();
                comm.CommandText = strSQL;
                comm.CommandType = ct;
                da = new SqlDataAdapter(comm);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds;
            }
            catch
            {
                throw;
            }
            finally
            {
                conn.Close();
            }  
        }

        public bool MyExecuteNonQuery(string strSQL, CommandType ct) 
        {
            bool f = false; 
            if (conn.State == ConnectionState.Open)   
                conn.Close();
            conn.Open(); 
            comm.CommandText = strSQL; 
            comm.CommandType = ct; 
            try 
            { 
                comm.ExecuteNonQuery();
                f = true; 
            } 
            catch (SqlException ex) 
            {
                throw;
            }
            finally 
            { 
                conn.Close(); 
            } 
            return f; 
        }
    }
}
