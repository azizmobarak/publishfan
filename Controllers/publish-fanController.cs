// Copy Right  Aziz Mobarak 2019



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.IO;
using publishfan.Models;

namespace publishfan.Controllers
{
    public class publishfanController : Controller
    {
        
        string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        // GET: main
        
        public ActionResult main()
        {
            if(Session["user_id"].ToString()!="")
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand com = new SqlCommand("select a.article_title, a.article_Description,ad.[image_path],ad.[article_id],a.user_numero   from user_articles as a join [dbo].[article_details] ad on a.article_id=ad.article_id  ", con);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    da.Fill(dt);
                }

                ViewData["data"] = dt;

                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        //filter
        public ActionResult filter(string categorie,string key_word, string range)
        {
            if (Session["user_id"].ToString() != "")
            {
                DataTable dtuser = new DataTable();
                filteruser fu = new filteruser();

                if (key_word == "" || key_word == null && categorie!= "Categories")
                {
                    DataTable dt = new DataTable();
                  
                    using (SqlConnection con = new SqlConnection(cs))
                    {
                        SqlCommand com = new SqlCommand("select a.article_title, a.article_Description,ad.[image_path],ad.[article_id],a.user_numero from user_articles as a join [dbo].[article_details] ad on a.article_id=ad.article_id where a.[categorie] like '%"+categorie+"%' ", con);
                       
                        SqlDataAdapter da = new SqlDataAdapter(com);
                        con.Open();
                        da.Fill(dt);
                        con.Close();
                    }
                   

                    ViewData["filter"] = dt;
                   

                    return View();

                }
                else
                {
                    if (key_word != "" || key_word != null && categorie == "Categories")
                    {
                        key_word = key_word.Replace("'", "''");
                        DataTable dt = new DataTable();
                        using (SqlConnection con = new SqlConnection(cs))
                        {
                            SqlCommand com = new SqlCommand("select a.article_title, a.article_Description,ad.[image_path],ad.[article_id],a.user_numero from user_articles as a join [dbo].[article_details] ad on a.article_id=ad.article_id where a.[categorie] like '" + key_word + "' or  a.[article_title] like '%" + key_word + "%'", con);
                            //com.Parameters.AddWithValue("@key_word", key_word);
                            //com.Parameters.AddWithValue("@range", int.Parse(range));
                            SqlDataAdapter da = new SqlDataAdapter(com);
                            con.Open();
                            da.Fill(dt);
                            con.Close();
                            ViewData["filter"] = dt;

                            dtuser = fu.getbyname(key_word);
                            ViewData["usersnums"] = dtuser;
                        }
                    }else
                    {
                        DataTable dt = new DataTable();
                        using (SqlConnection con = new SqlConnection(cs))
                        {
                            SqlCommand com = new SqlCommand("select a.article_title, a.article_Description,ad.[image_path],ad.[article_id],a.user_numero from user_articles as a join [dbo].[article_details] ad on a.article_id=ad.article_id", con);
                            SqlDataAdapter da = new SqlDataAdapter(com);
                            con.Open();
                            da.Fill(dt);
                            con.Close();
                            ViewData["filter"] = dt;
                            
                        }
                    }
                   
                    return View();
                }
            }
            
            return RedirectToAction("Login", "Account");
        }

        //filter iside user and search
       filteruser fu = new filteruser();
        public ActionResult filteruser(string key_word)
        {

            if (Session["user_id"].ToString() != "")
            {
                DataTable dtuser = new DataTable();
                dtuser = fu.getbyname(key_word);
                ViewData["usersnums"] = dtuser;
                HttpContext.Application["userfullname"] = key_word;
                DataTable dt = new DataTable();
                ViewBag.numerofilteruse = key_word;
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand com = new SqlCommand("select a.article_title, a.article_Description,ad.[image_path],ad.[article_id],a.user_numero from user_articles as a join [dbo].[article_details] ad on a.article_id=ad.article_id", con);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    con.Open();
                    da.Fill(dt);
                    con.Close();
                    ViewData["filter"] = dt;
                    return View("filter");
                }

            }
            return RedirectToAction("Login", "Account");
        }

        //profile
        // [HttpPost]
        // MvcApplication mm = new MvcApplication();
        public ActionResult profile()
        { 
            
            if (Session["user_id"].ToString() != "")
            {
               HttpContext.Application["ID"] = Session["user_id"].ToString();
                string ID = Session["user_id"].ToString();
                DataTable dt = new DataTable();
                DataTable interst = new DataTable();
                DataTable post = new DataTable();
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand com = new SqlCommand("select [firstname],[lastname],[email],[age],[gender],[tele],[bio],city,[profile_path],[profile_cover],[user_numero] from [dbo].[publishfan_users] where [user_numero] ='" + ID + "'", con);
                    SqlCommand intersts = new SqlCommand("select [interests] from [dbo].[user_interest] where [user_numero] ='" + ID + "'", con);
                    SqlCommand posts = new SqlCommand("select [article_text],[article_id] from [dbo].[article_details] where [user_numero] ='" + ID + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    SqlDataAdapter dainterests = new SqlDataAdapter(intersts);
                    SqlDataAdapter daposts = new SqlDataAdapter(posts);
                    da.Fill(dt);
                    dainterests.Fill(interst);
                    daposts.Fill(post);
                   
                }

                ViewBag.interests = interst.Rows[0][0].ToString();
                ViewData["posts"] = post;
                ViewData["user"] = dt;
                return View();
            }
            return RedirectToAction("Login", "Account");
        }
        //user profile
        usersprofiles us = new usersprofiles();
        public ActionResult userprofile()
        {

            if (Session["user_id"].ToString() != "")
            {
                string ID = Session["user_id"].ToString();
                string id = HttpContext.Application["others_id"].ToString();
                if(id== ID)
                {
                    return RedirectToAction("profile", "publishfan");
                }
                ViewBag.interests = us.getinterests(id);
                ViewData["posts"] = us.getposts(id);
                ViewData["user"] = us.getusersprofile(id);
                if (us.isfollowed(ID,id)==true)
                {
                    ViewBag.follow = "Followed";
                }else
                {
                    ViewBag.follow = "Follow";
                }
                    return View();
            }
            return RedirectToAction("Login", "Account");

        }
        //Get the other users 's profile
        public ActionResult getprofiles(string userid)
        {
            if (Session["user_id"].ToString() != "")
            {
               HttpContext.Application["others_id"] = userid;
                return RedirectToAction("userprofile", "publishfan");
            }
            return RedirectToAction("Login", "Account");
        }
        string followed = "";
        //Follow users
        public ActionResult ifollow(string id)
        {
            followed = "";
            string ID = Session["user_id"].ToString();
             if(us.isfollowed(ID,id))
                {
                    followed= "this user is followed";
                }else
                {
                using (SqlConnection con = new SqlConnection(cs))
                {

                    SqlCommand com = new SqlCommand("insert into ifollow values('" + ID + "','" + id + "')", con);
                    con.Open();
                    com.ExecuteNonQuery();
                }

            }
            return RedirectToAction("Followedlist","publishfan");
        }
        //followed list 
        public ActionResult Followedlist()
        {
            string ID = Session["user_id"].ToString();
            if(ID!="")
            {
                ViewData["followed"] = us.followedlist(ID);
                ViewBag.message = followed;
                return View();
            }
            return RedirectToAction("Login", "Account");
        }

        //addnew article
        public ActionResult Addnew()
        {
            if (Session["user_id"].ToString() != "")
            {
                return View();
            }
            return RedirectToAction("Login", "Account");
        }
        // update article 
        ArticleDetails d = new ArticleDetails();
        public ActionResult updatetext(string idarticle, string text)
        {
            string ID = Session["user_id"].ToString();
            if (Session["user_id"].ToString() != "")
            {
                
                HttpContext.Application["article_id"] = idarticle;
                d.updatearticle(idarticle, text);
                Session["user_id"] = ID;
                return RedirectToAction("profile", "publishfan");
            }
            return RedirectToAction("Login", "Account");
        }

        //Edite user info 
        public ActionResult edite()
        {

            string ID = Session["user_id"].ToString();
            if (ID!= "")
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand com = new SqlCommand("select [firstname],[lastname],[email],[tele],[pass_word],city from [dbo].[publishfan_users] where [user_numero] ='" + ID + "'", con);
                    SqlDataAdapter da = new SqlDataAdapter(com);
                    da.Fill(dt);
                    ViewData["user"] = dt;
                    ViewBag.message = HttpContext.Application["message"].ToString();
                }
                return View();
            }
            return RedirectToAction("Login", "Account");
        }
        //method to update user info
        usersprofiles up = new usersprofiles();
        public ActionResult updateinfo(string name,string lastname,string city,string email,string tele,string oldpassword,string newpass,string pass)
        {
            string ID = Session["user_id"].ToString();
            if (Session["user_id"].ToString() != "")
            {
              string result= up.updateuser(name,lastname,city,email,tele,ID);
                HttpContext.Application["message"] = result;
                Session["user_id"] = ID;
                return RedirectToAction("edite", "publishfan");
            }
            return RedirectToAction("Login", "Account");
        }
        public ActionResult updatepassword(string oldpassword,string newpass,string pass)
        {
            string ID = Session["user_id"].ToString();
            if (Session["user_id"].ToString() != "")
            {
              string result= up.updatepassword(oldpassword,newpass,pass,ID);
                HttpContext.Application["message"] = result;
                Session["user_id"] = ID;
                return RedirectToAction("edite", "publishfan");
            }
            return RedirectToAction("Login", "Account");
        }

        public ActionResult logout()
        {
            Session["user_id"] ="";
            string ID = Session["user_id"].ToString();
            HttpContext.Application["ID"]= "";

            return RedirectToAction("Login", "Account");
        }

        // ad article in database
        Reactions reaction = new Reactions();
        public ActionResult addpost(string title,string description,string categorie,HttpPostedFileBase filepath,string text)
        {
            string ID = Session["user_id"].ToString();
            if (ID != "")
            {
                if (title.Length < 25 && title.Length > 10)
                {
                    if (description.Length > 40 && description.Length < 120)
                    {
                        if (text.Length > 200)
                        {
                            
                            if (filepath != null)
                            {
                                if (categorie != "select")
                                {
                                    if (title + description + filepath + text != "")
                                    {
                                        using (SqlConnection con = new SqlConnection(cs))
                                        {
                                            try
                                            {
                                                string art = text.Replace("'", "''");
                                                string des = description.Replace("'", "''");
                                                string tit = title.Replace("'", "''");

                                                

                                                SqlCommand com = new SqlCommand("insert into user_articles values (N'" + tit + "','" + des + "',N'" + ID + "',N'" + categorie + "')", con);
                                                SqlCommand numberlin = new SqlCommand("select top 1 article_id from user_articles order by article_id desc ", con);
                                                con.Open();
                                                com.ExecuteNonQuery();
                                                con.Close();
                                                con.Open();
                                                string count = numberlin.ExecuteScalar().ToString();
                                                int a = int.Parse(count);
                                                con.Close();
                                                string path = savefile(filepath, ID, count);
                                                SqlCommand com2 = new SqlCommand("insert into article_details values(" + a + ",N'" + art + "','" + ID + "',N'" + path + "')", con);
                                                SqlCommand com3 = new SqlCommand("insert into [dbo].[reaction_details] values(" + a + ",'" + ID + "',0,0)", con);
                                                con.Open();
                                                com2.ExecuteNonQuery();
                                                com3.ExecuteNonQuery();
                                                con.Close();

                                                reaction.addidtoreaction(a);
                                            }
                                            catch 
                                            {
                                                ViewBag.error = "* Verify all entries if it not correct or does not respect the text front of each controle )";

                                            }
                                        }
                                    }
                                    else
                                    {
                                        ViewBag.error = "*Please fill in all choices and verify all your entries";
                                    }
                                }
                                else
                                {
                                    ViewBag.error = "Categorie :Please select categorie of your article";
                                }
                            }
                            else
                            {
                                ViewBag.error = "File: please remember that the file is important";
                            }
                        }
                        else
                        {
                            ViewBag.error = "Article: please remember that the min is 200";
                        }
                    }
                    else
                    {
                        ViewBag.error = "Description: please remember that the max is 120 and the min is 40";

                    }
                }
                else
                {
                    ViewBag.error = "Title: please remember that the max is 25 and the min is 10";
                }
                return View("Addnew");
            }else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //save profile image
        
        public ActionResult saveprofile(HttpPostedFileBase file)
        {
            string ID = Session["user_id"].ToString();
            if (ID!="")
            {
                string path = saveimages(file, ID);
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand comm = new SqlCommand("select [profile_path] from [dbo].[publishfan_users] where [user_numero]='" + ID + "'", con);
                    con.Open();
                    string userimage = comm.ExecuteScalar().ToString();
                    con.Close();

                    SqlCommand commm = new SqlCommand(" update [dbo].[article_comments] set [profile_path] ='" + path + "'  where [profile_path]='" + userimage + "'", con);
                    con.Open();
                    commm.ExecuteNonQuery();
                    con.Close();


                    SqlCommand com = new SqlCommand("update [dbo].[publishfan_users] set [profile_path]='" + path + "' where [user_numero]='" + ID + "'", con);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                    
                }
                Session["user_id"] = ID;
                return RedirectToAction("profile","publishfan");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }


        //save profile cover photo
        
        public ActionResult savecover(HttpPostedFileBase filecover)
        {
            string ID = Session["user_id"].ToString();
            if (ID!="")
            {
                string path = savecovers(filecover, ID);
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand com = new SqlCommand("update [dbo].[publishfan_users] set [profile_cover]='" + path + "' where [user_numero]='" + ID + "'", con);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                }
                Session["user_id"] = ID;
                return RedirectToAction("profile","publishfan");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        //change bio desc
        public ActionResult bio(string bio)
        {
            string ID = Session["user_id"].ToString();
            if (ID != "")
            {
                bio = bio.Replace("'", "''");
                using (SqlConnection con = new SqlConnection(cs))
                {
                    SqlCommand com = new SqlCommand("update [dbo].[publishfan_users] set bio='" + bio + "' where [user_numero]='" + ID + "'", con);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();

                }
                Session["user_id"] = ID;
                return RedirectToAction("profile","publishfan");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

  

        //Go to view Article from this method

        ArticleDetails ad = new ArticleDetails();
        Commentdetails c = new Commentdetails();
        string articleid = "";
        MvcApplication m = new MvcApplication();
        public ActionResult Article(string article)
        {
            
            string ID = Session["user_id"].ToString();
            if (ID!="")
            {
                if (article == null)
                {
                    article = HttpContext.Application["article_id"].ToString();
                }
                else
                {
                    HttpContext.Application["article_id"] = article;
                }
                string id= article;
                articleid = id;
                 ViewData["article"] = ad.Dtarticle(int.Parse(id));
                ViewData["comments"] = c.get(int.Parse(id));
               
                ViewBag.id = id;
                return View(new { id= ad.Dtarticle(int.Parse(id)).Rows[0][0] });
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

        }

        //ad comment 
        // geting id article to comment in from the last method that return the article view
        publishfan.Models.publishedby p = new publishedby();
        public ActionResult addcomment(string articleid, string acommenttext)
        {
            string ID = Session["user_id"].ToString();
          
            if (ID != "")
            {
                //get the name and path of user
                string fullname = p.publisher(ID); ;
                string filepath = p.publisherimage(ID).Trim();
                acommenttext = acommenttext.Replace("'", "''");
                 using (SqlConnection con = new SqlConnection(cs))
                    {
                        SqlCommand com = new SqlCommand("insert into [dbo].[article_comments] values (" + int.Parse(articleid) + ",'" + fullname + "','" + acommenttext + "','" + filepath + "','"+ID+"')", con);
                        con.Open();
                        com.ExecuteNonQuery();
                        con.Close();

                    }

                    ViewData["article"] = ad.Dtarticle(int.Parse(articleid));
                    ViewData["comments"] = c.get(int.Parse(articleid));
               
                return View("Article");
            } else
            {
               return RedirectToAction("Login", "Account");
            }
               

        }


        Reactions r = new Reactions();
        /// this functions under this line for Vote on the articles
        /// vote to up
        public ActionResult voteup(string articleid)
        {
            string ID= Session["user_id"].ToString();
            if(ID!="")
            {
                int up=0;
                int id = int.Parse(articleid);
                if (r.reacted(id, ID) == false)
                {
                    up = int.Parse(r.getforarticle(id).Rows[0][0].ToString());
                    r.setup(up + 1, ID, id); 
                }
                return RedirectToAction("Article");
            }
            return RedirectToAction("Login", "Account");
        }

        //vote down
        public ActionResult votedown(string articleid)
        {
            string ID= Session["user_id"].ToString();
            if(ID!="")
            {
                int id = int.Parse(articleid);
                int down = 0;
                if (r.reacted(id, ID) == false)
                {
                    down = int.Parse(r.getforarticle(id).Rows[0][1].ToString());
                    r.setdown(down + 1, ID, id);
                }
                return RedirectToAction("Article");
            }
            return RedirectToAction("Login", "Account");
        }

        ///up and down for profile (number votes per user)
        
        ///up
        public ActionResult voteupprofile(string id_article)
        {
            string ID = Session["user_id"].ToString();
            if (ID != "")
            {
                int up = 0;
                int id = int.Parse(id_article);
                if (r.reacted(id, ID) == false)
                {
                    up = int.Parse(r.getforarticle(id).Rows[0][0].ToString());
                    r.setup(up + 1, ID, id);
                }
                return RedirectToAction("profile");
            }
            return RedirectToAction("Login", "Account");
        }

        //down

        public ActionResult votedownprofile(string id_article)
        {
            string ID = Session["user_id"].ToString();
            if (ID != "")
            {
                int id = int.Parse(id_article);
                int down = 0;
                if (r.reacted(id, ID) == false)
                {
                    down = int.Parse(r.getforarticle(id).Rows[0][1].ToString());
                    r.setdown(down + 1, ID, id);
                }
                return RedirectToAction("profile");
            }
            return RedirectToAction("Login", "Account");
        }

        /////////HERE IS THE FUNCTIONS ////////////////////////////////

        protected string savefile(HttpPostedFileBase file, string ID, string idarticle)
        {
            string path = "";
            string p = "";
            if (file != null)
            {
                string extention = System.IO.Path.GetExtension(file.FileName);
                path = System.IO.Path.Combine(Server.MapPath(" /Articles_image/"), idarticle + DateTime.Now.Year + file.FileName);
                p = " /Articles_image/" + idarticle + DateTime.Now.Year + file.FileName;
                if (extention.ToLower() == ".png" || extention.ToLower() == ".jpg")
                {
                    file.SaveAs(path);
                }
            }

            return p;
        }

        //this function used in other classes to save the profile images
        protected string saveimages(HttpPostedFileBase file,string userid)
        {
            string path = "";
            string p = "";
            if (file != null)
            {
                string extention = System.IO.Path.GetExtension(file.FileName);
                path = System.IO.Path.Combine(Server.MapPath(" /profile_images/"), DateTime.Now.Year + userid.Substring(3, 5) + file.FileName);
                p = " /profile_images/" +  + DateTime.Now.Year + userid.Substring(3, 5) + file.FileName;
                if (extention.ToLower() == ".png" || extention.ToLower() == ".jpg" || extention.ToLower() == ".jpge")
                {
                    file.SaveAs(path);
                }
            }

            return p;
        }
        
        //this function used in other classes for covers photos
     protected string savecovers(HttpPostedFileBase file,string userid)
        {
            string path = "";
            string p = "";
            if (file != null)
            {
                string extention = System.IO.Path.GetExtension(file.FileName);
                path = System.IO.Path.Combine(Server.MapPath(" /cover_images/"), DateTime.Now.Year + userid.Substring(3, 5) + file.FileName);
                p = " /cover_images/" +  + DateTime.Now.Year + userid.Substring(3, 5) + file.FileName;
                if (extention.ToLower() == ".png" || extention.ToLower() == ".jpg" || extention.ToLower() == ".jpge")
                {
                    file.SaveAs(path);
                }
            }

            return p;
        }
    }

  
}