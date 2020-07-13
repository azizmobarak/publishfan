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
    public class ArticleDetails
    {
        string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        DataTable dtarticle = new DataTable();
        DataTable dtuser = new DataTable();
        public DataTable Dtarticle(int article_ID)
        { 
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand("select a.article_title,ad.[image_path],ad.article_text,a.user_numero,ad.[article_id]  from user_articles as a join [dbo].[article_details] ad on a.article_id=ad.article_id where ad.[article_id]=" + article_ID+"", con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dtarticle);

            }
            return dtarticle;
        }

        public void updatearticle(string idarticle,string text)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                text.Replace("'", "''");
                SqlCommand com = new SqlCommand("update [article_details] set  article_text= '"+text+"' where [article_id]="+idarticle+"", con);
                con.Open();
                com.ExecuteNonQuery();
            }
        }
       
    }
}