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
using publishfan.Controllers;

namespace publishfan.Models
{
    public class profiledownload
    {
        string cs = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        publishfanController p = new publishfanController();
        public void profileimage(string file,string id)
        {
           
        }


        
        

    }
}