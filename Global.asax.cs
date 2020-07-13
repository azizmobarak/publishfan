using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Data;

namespace publishfan
{
    public class MvcApplication : System.Web.HttpApplication
    {
        DataTable dt = new DataTable();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Application["article_id"] = "";
            Application["ID"] = "";
            Application["userfullname"] = " ";
            Application["interests"] = " ";
            Application["others_id"] = " ";
            Application["message"] = " ";
        }
       protected void Session_Start()
        {
            Session["user_id"] = "";
            
        }
        
    }
}
