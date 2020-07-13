// Copy Right  Aziz Mobarak 2019

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.ApplicationServices;
using System.Web.ClientServices;
using System.Web.Caching;

namespace publishfan.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        //Register form
        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.errormessage = HttpContext.Application["message"].ToString();
            return View();
        }


        string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        // GET: Acount
        //Login 
        [HttpPost]
        
        public ActionResult Access_to_main(string email, string password)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                DataTable dt = new DataTable();
                SqlCommand com = new SqlCommand("select pass_word,email,user_numero from publishfan_users where email='" + email + "'", con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                con.Open();
                da.Fill(dt);
                con.Close();
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0][0].ToString() == password)
                    {
                        Session["user_id"] = dt.Rows[0][2].ToString();
                        string ID = Session["user_id"].ToString();
                        return RedirectToAction("main", "publishfan", new { ID });
                    }
                }

            }
            ViewBag.errormessage = "User Not Found Try to Verify Your Email And Password";
            return View("login");
        }


        //Getting Data from register form

        [HttpPost]
        public ActionResult New_Account(string FirstName, string LastName, string Email, string confirmemail, string bio, string PassWord, string confirmpassword,string city,string tele, string Age, string Gender, string tags)
        {
            HttpContext.Application["message"] = "";
            if ((FirstName != "" || LastName != "") || (FirstName != null || LastName != null))
            {
                if (Email == confirmemail)
                {
                    if (PassWord == confirmpassword)
                    {
                        if (PassWord.Length > 6)
                        {
                            using (SqlConnection con = new SqlConnection(cs))
                            {

                                string numero = FirstName.Substring(0, 2) + PassWord.Substring(0, 1) + PassWord.Substring(3, 4) + FirstName.Length.ToString() + LastName.Length.ToString() + Email.Substring(3, 5) + Email.Length.ToString() + Age + Email.IndexOf("@").ToString() + (int.Parse(Age) - 10).ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Day.ToString() + (int.Parse(DateTime.Now.Year.ToString()).ToString()) + "@" + (FirstName + LastName).Length.ToString() + ((LastName + Email).Length * 2).ToString();
                                numero = numero.Replace(".", "-");
                                SqlCommand com = new SqlCommand("insert into publishfan_users values('" + numero + "','" + FirstName + "','" + LastName + "','" + Email + "','" + PassWord + "','" + Age + "','" + Gender + "','"+tele+"','"+bio+"','"+city+ "','/images/profile.png','/images/ad1.png')", con);
                                SqlCommand com2 = new SqlCommand("insert into user_interest values('" + numero + "','" + tags + "')", con);

                                con.Open();

                                com.ExecuteNonQuery();
                                com2.ExecuteNonQuery();

                                con.Close();
                                return RedirectToAction("login","Account");
                            }
                        }
                        else
                        {
                           HttpContext.Application["message"] = "PassWord should be more than 6 character!";
                            return RedirectToAction("Register");
                        }

                    }
                    else
                    {
                        HttpContext.Application["message"] = "PassWord is not matching!";
                        return RedirectToAction("Register");
                    }

                }
                else
                {
                    HttpContext.Application["message"] = "Email is not matching!";
                    return RedirectToAction("Register");
                }
            }
            else
            {
                HttpContext.Application["message"] = "Name or last Name should not be empty please verify your information";
                return RedirectToAction("Register");
            }

        }

        //conversation between register form and login form
        public ActionResult toregister()
        {
            return View("Register");
        }
        public ActionResult tologin()
        {
            return View("Login");
        }
    }
}