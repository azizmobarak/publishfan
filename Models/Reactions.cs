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
    public class Reactions
    {
        string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        //update
        public void setup(int up,string userid,int id)
        {
            
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand("update [dbo].[reaction_details] set [number_up]=" + up+ " , [user_numero]='"+userid+"' where [article_id]="+id+"", con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close(); 
            }

        }
       

        //update down
        public void setdown(int down,string userid,int id)
        {
            
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand("update [dbo].[reaction_details] set [number_down]=" + down+ " , [user_numero]='" + userid + "' where [article_id]=" + id+"", con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close(); 
            }

        }

       
        ///------------------------------------------
        public DataTable get(string id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand("select [number_up],[number_down] from [dbo].[reaction_details] where [user_numero]='" + id + "'", con);
                SqlDataAdapter ad = new SqlDataAdapter(com);
                ad.Fill(dt);
            }

            return dt;
        }
        ///get for article
        public DataTable getforarticle(int id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand("select [number_up],[number_down] from [dbo].[reaction_details] where [article_id]=" + id + "", con);
                SqlDataAdapter ad = new SqlDataAdapter(com);
                ad.Fill(dt);
            }

            return dt;
        }

        public void addidtoreaction(int idarticle)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand("insert into [dbo].[reaction_details] (article_id) values("+ idarticle + ")", con);
                con.Open();
                com.ExecuteNonQuery();
            }

         
        }

        //number reactions up 
        public int numberreact(string ID)
        {
            int cnt = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand("select count(*) from [dbo].[reaction_details] where user_numero='" + ID+"'", con);
                con.Open();
                cnt = int.Parse(com.ExecuteScalar().ToString());
            }
            return cnt;
            }

        //number comments
         public int numbercomment(string username,string profilepth)
        {
            int cnt = 0;
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand("select count(*) from [dbo].[article_comments] where  user_name_ac='"+username+"' and profile_path='"+profilepth+"'", con);
                con.Open();
                cnt = int.Parse(com.ExecuteScalar().ToString());
            }
            return cnt;
        }

        public void updatereact()
        {

            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand com1 = new SqlCommand("update [dbo].[reaction_details] set number_down=0 where number_down is null", con);
                SqlCommand com2 = new SqlCommand("update [dbo].[reaction_details] set number_up=0 where number_up is null", con);
                con.Open();
                com1.ExecuteNonQuery();
                com2.ExecuteNonQuery();

            }
        }

        //return if user reacted to this article or not
        public bool reacted(int id,string ID)
        {
            DataTable dt = new DataTable();
           
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand("select user_numero from reaction_details where article_id="+id+ " and user_numero='"+ID+"'", con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
                if(dt.Rows.Count==0)
                {
                    return false;
                }
                
            }
            return true;
        }

    }
}