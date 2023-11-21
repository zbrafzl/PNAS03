using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using Prp.Data;
using System.Windows.Interop;
using System.Web.Services.Description;
using Message = Prp.Data.Message;

namespace Prp.Sln.Controllers
{
    public class OtpController : Controller
    {
        // GET: Otp
      
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult SendOTP()
        {
            return View();
        }

        private string GenerateRandomOTP(int iOTPLength, string[] saAllowedCharacters)
        {

            string sOTP = String.Empty;

            string sTempChars = String.Empty;

            Random rand = new Random();

            for (int i = 0; i < iOTPLength; i++)

            {

                int p = rand.Next(0, saAllowedCharacters.Length);

                sTempChars = saAllowedCharacters[rand.Next(0, saAllowedCharacters.Length)];

                sOTP += sTempChars;

            }

            return sOTP;

       }

        [HttpGet]
        public JsonResult GetCode(string PhoneNumber)
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
                }
                else {
                    string[] saAllowedCharacters = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };
                    string sRandomOTP = string.Empty;
                    int randomNumber = 0;
                    sRandomOTP = GenerateRandomOTP(6, saAllowedCharacters);
                    randomNumber = Convert.ToInt32(sRandomOTP);
                    code = randomNumber.ToString();
                    string query1 = "insert into tblOtps values ((select top 1 applicantId from tblApplicant where contactNumber = '"+ PhoneNumber + "'),'" + PhoneNumber + "'," + randomNumber + ",getdate(),0)";
                    SqlConnection connection = new SqlConnection(PrpDbConnectADO.Conn);
                    connection.Open();
                    SqlCommand cmd = new SqlCommand(query1, connection);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
               
            }
            finally
            {
                con.Close();
            }
            string smsBody = "Dear Candidate, Your OTP is : " + code + ".";
            Message msgSms = FunctionUI.SendSms(Convert.ToString(PhoneNumber), smsBody);

            msgSms.message = msgSms.message + " sms : ["+ smsBody+"]" ;

            OTP otp = new OTP();
            otp.number =  "92" + PhoneNumber.Substring(1, PhoneNumber.Length - 1);
            otp.msg = smsBody;
            otp.result = msgSms.message;


            return Json(otp, JsonRequestBehavior.AllowGet);

        }
    }
}