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
    public class Commentdetails
    {
        string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        DataTable dt = new DataTable();
        public DataTable get(int id)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand("select user_name_ac,comment_text,user_numero from [dbo].[article_comments] where [article_id]=" + id + "", con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
            }
            return dt;
        }

    }
}