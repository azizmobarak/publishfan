using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace publishfan
{
    
    public class RouteConfig
    {
        
       
        public static void RegisterRoutes(RouteCollection routes)
        {
           
                routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //Home page
            routes.MapRoute(
                "Default",
                "{controller}/{action}/{id}",
                new { controller = "Home", action = "index", id = UrlParameter.Optional }
                );
            //main page
            routes.MapRoute(
                    name: "Main",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "publishfan", action = "main", id = UrlParameter.Optional }
                );
            //filter page
            routes.MapRoute(
                    name: "filter",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "publishfan", action = "filter", id = UrlParameter.Optional }
                );
            //profile
            routes.MapRoute(
                    name: "Profile",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "publishfan", action = "profile", id = UrlParameter.Optional }
                );
            //profile user
            routes.MapRoute(
                    name: "userprofile",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "publishfan", action = "userprofile", id = UrlParameter.Optional }
                );
            //add new article
            routes.MapRoute(
                    name: "AddNewArticle",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "publishfan", action = "Addnew", id = UrlParameter.Optional }
                );
            
            //add new article
            routes.MapRoute(
                    name: "Article",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "publishfan", action = "Article", id = UrlParameter.Optional }
                );
            //List of followed people
            routes.MapRoute(
                    name: "Followedlist",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "publishfan", action = "Followedlist", id = UrlParameter.Optional }
                ); 
            //Edite info
            routes.MapRoute(
                    name: "Edite",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "publishfan", action = "edite", id = UrlParameter.Optional }
                );

            //register
            routes.MapRoute(
                    name: "Register",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "Account", action = "Register", id = UrlParameter.Optional }
                );

            //login
            routes.MapRoute(
                    name: "Login",
                    url: "{controller}/{action}/{id}",
                    defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
                );

            

        }
           
        
    }
}