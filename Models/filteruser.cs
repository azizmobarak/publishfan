using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.IO;
using publishfan.Models;

namespace publishfan.Models
{
    public class filteruser
    {
        string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        public DataTable getbyname(string fullname)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand("select user_numero from [dbo].[publishfan_users] where [lastname] like '%"+fullname+ "%' or [firstname] like '%"+fullname+"%'", con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
            }
            return dt;
        }
    }
}