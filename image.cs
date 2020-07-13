using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace publishfan
{
    public class image
    {

        public string Get_Image(int id)
        {
            SqlDataReader dr;
            using (SqlConnection con = new SqlConnection(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=mydata;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
            {
                SqlCommand com = new SqlCommand(" select nameimage from images where id = " + id, con);
                con.Open();
                dr = com.ExecuteReader();

            }

            string dir = dr[0].ToString();

            return dir;
        }
    }
}