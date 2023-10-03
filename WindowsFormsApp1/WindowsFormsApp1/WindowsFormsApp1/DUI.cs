using Microsoft.Win32; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class DUI
    {
        SqlConnection conn = new SqlConnection(@"Server=strength\sqlexpress01;Initial Catalog=StudentDB;Integrated Security=True;");
        SqlCommand cmd;
        public int UDI(string qry)
        {
            conn.Open();
            cmd = new SqlCommand(qry,conn);  
            int res =cmd .ExecuteNonQuery();    
            conn.Close();   
            return res;
        
        }
        public SqlDataReader SearchConnOient(string qry)
        {
            conn.Open();
            cmd= new SqlCommand(qry,conn);
            SqlDataReader rd = cmd.ExecuteReader();
            // conn.Close();
            return rd;
         
        
        }
        public DataTable SearchConnLess(string qry)
        {
            conn.Open ();
            SqlDataAdapter adp = new SqlDataAdapter(qry, conn);
            conn.Close(); // connectionless
            /////     adpater 
            /// return adp
            DataTable dt = new DataTable();
            adp.Fill(dt);   
            return dt;
        }


    }
}
