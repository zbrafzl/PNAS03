using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using Prp.Data;

namespace Prp.Sln.Controllers
{
    public class OtpController : Controller
    {
        // GET: Otp
      
        public ActionResult Index()
        {
            return View();
        }

        public string GetCode(string PhoneNumber)
        {
           
            string code = string.Empty;
            // set up connection string to your database
            string connectionString = ConfigurationManager.ConnectionStrings["DbPrpConn"].ConnectionString;
            SqlConnection con = new SqlConnection(PrpDbConnectADO.Conn);
            try
            {
                string query = "Select top(1) Cast(otpCode as varchar(250)) From tblOtps where mobilenumber = '" + PhoneNumber + "' and isUsed = 0  order by 1 desc";
                SqlCommand cmdUpdate = new SqlCommand(query);
                con.Open();
                cmdUpdate.Connection = con;
                SqlDataReader reader = cmdUpdate.ExecuteReader();
                if (reader.HasRows)
                {
                    // read the first row and retrieve the code value
                    reader.Read();
                     code = reader.GetString(0);
                    return code;
                }
                return code;
            }
            catch (Exception ex)
            {
               
            }
            finally
            {
                con.Close();
            }
            return code;
       
        }
    }
}