using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.IO;

namespace publishfan.Models
{
    public class publishedby
    {
        string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        public string publisher(string ID)
        {
            string fullname = "";
           
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand("select firstname+' '+lastname from publishfan_users where user_numero='" + ID+"'", con);
                con.Open();
                fullname = com.ExecuteScalar().ToString();
                con.Close();
            }
            return fullname;
        }
        public string publisherimage(string ID)
        {
            string path = "";
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand("select profile_path from publishfan_users where user_numero='" + ID+"'", con);
                con.Open();
                path = com.ExecuteScalar().ToString();
                con.Close();
            }
            return path;
        }

        public DataTable topfiveusers()
        {
        DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand("select top 5 user_numero,count(article_id) from user_articles group by user_numero order by count(article_id) desc", con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
            }
            return dt;
        }
    }
}