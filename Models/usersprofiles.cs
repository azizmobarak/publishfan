using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace publishfan.Models
{
    public class usersprofiles
    {
        string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;

        public DataTable getusersprofile(string userid)
        {
            DataTable dt = new DataTable();
           
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand("select [firstname],[lastname],[email],[age],[gender],[bio],city,[profile_path],[profile_cover],[user_numero] from [dbo].[publishfan_users] where [user_numero] ='" + userid + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable getposts(string userid)
        {
           
            DataTable post = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand posts = new SqlCommand("select [article_text],[article_id] from [dbo].[article_details] where [user_numero] ='" + userid + "'", con);
                
                SqlDataAdapter daposts = new SqlDataAdapter(posts);
               
                daposts.Fill(post);

            }
            return post;
        }
        
        public string getinterests(string userid)
        {
           
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {
              
                SqlCommand intersts = new SqlCommand("select [interests] from [dbo].[user_interest] where [user_numero] ='" + userid + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(intersts);
                da.Fill(dt);
                
            }
            return dt.Rows[0][0].ToString();
        }

        public bool isfollowed(string ID,string id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {

                SqlCommand intersts = new SqlCommand("select user_numero from ifollow where user_numero='"+ID+"' and followed_numero='"+id+"' ", con);
                SqlDataAdapter da = new SqlDataAdapter(intersts);
                da.Fill(dt);
            }
            try
            {
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
           
        }

        public DataTable followedlist(string ID)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(cs))
            {

                SqlCommand intersts = new SqlCommand("select [followed_numero] from ifollow where user_numero='" + ID + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(intersts);
                da.Fill(dt);
            }
            return dt;
        }

        /// update user data profile
        /// 
        public string updateuser(string name, string lastname, string city, string email, string tele,string ID)
        {
 
          
            if (name.Length > 3 || lastname.Length > 3)
                {
                    if (email.Length > 13)
                    {
                        if (tele.Length > 10)
                        {
                       
                            using (SqlConnection con = new SqlConnection(cs))
                            {
                                SqlCommand com = new SqlCommand("update [dbo].[publishfan_users] set [firstname]='" + name + "',[lastname]='" + lastname + "',[email]='" + email + "',[tele]='" + tele + "',city='" + city + "' where [user_numero] ='" + ID + "'", con);
                                con.Open();
                                com.ExecuteNonQuery();
                            }
                        return "Congrutalation!,your info changed succefully ";

                    }
                    else
                        {
                            return "phone number is not valid";
                        }

                    }
                    else
                    {
                        return "email is too short";
                    }
                }
                else
                {
                    return "Name and last name sould be more than 3 character";
                }

        }

        //for password
        public string updatepassword(string oldpassword, string newpas, string pass, string ID)
        {
            string oldone = "";
            using (SqlConnection con = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand(" select [pass_word] from [dbo].[publishfan_users]  where [user_numero] ='" + ID + "'", con);
                con.Open();
                oldone = com.ExecuteScalar().ToString();
            }
            if (oldpassword == "" || pass == "")
            {
                pass = oldone;
            }
            if(pass.Length>8)
            {
                if (pass.CompareTo(newpas) == 0)
                {
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        SqlCommand com = new SqlCommand("update [dbo].[publishfan_users] set [pass_word]='" + pass + "' where [user_numero] ='" + ID + "'", con);
                        con.Open();
                        com.ExecuteNonQuery();
                    }
                    return "Congrutalation!,Password changed succefully ";
                }
            }
           
            return "Password Not changed";
        }
    }
}