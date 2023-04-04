using Prp.Data;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Prp.Sln
{

    public static class FunctionUI
    {

        public static SmsProcess SmsProcessMakeDefaultObject(this bool isSent, int applicantId, int smsId)
        {
            SmsProcess obj = new SmsProcess();
            try
            {
                obj.applicantId = applicantId;
                obj.isSent = 0;
                if (isSent)
                    obj.isSent = 1;
                obj.isProcess = 1;
                obj.smsId = smsId;

            }
            catch (Exception)
            {

                obj = new SmsProcess();
            }
            return obj;

        }

        public static Message SendSms(string number, string message)
        {

            Message msg = new Message();
            string msgBody = "";

            string fullLink = "";
            try

            {
                string link = "https://pk.eocean.us/APIManagement/API/RequestAPI";
                string password = "";
                try
                {
                    password = ConfigurationManager.AppSettings["SmsPassword"];
                }
                catch (Exception)
                {
                    password = "";
                }

                if (String.IsNullOrWhiteSpace(password))
                {
                    try
                    {
                        string path = ProjConstant.SMS.Path.smsPassword;
                        string filePath = Path.Combine(HttpContext.Current.Server.MapPath(path));
                        password = filePath.ReadFile();
                        password = password.Trim();
                    }
                    catch (Exception)
                    {
                        password = "";
                    }

                }

                if (String.IsNullOrWhiteSpace(password))
                {
                    password = "AHL%2fcJw8rwobY9hd2XefAq84EdiM8lf4GtDI08ob%2f2SciwVUqiYHKgN%2fNoFgo65deg%3d%3d";
                }

                string urlParameters = "?user=phf&pwd=#password#&sender=PHF&reciever=#number#&msg-data=#message#&response=string";
                number = "92" + number.Substring(1, number.Length - 1);

                urlParameters = urlParameters.Replace("#password#", password);
                urlParameters = urlParameters.Replace("#number#", number);
                urlParameters = urlParameters.Replace("#message#", message);

                fullLink = link + urlParameters;

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(link);

                // Add an Accept header for JSON format.
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                
                // List data response.
                HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;
                    msgBody = responseContent.ReadAsStringAsync().GetAwaiter().GetResult().ToLower();

                    if (msgBody.Contains("successfully"))
                    {
                        msg.status = true;
                        msg.msg = "Sent";
                    }
                    else
                    {
                        msg.status = false;
                        msg.msg = "1.Error!";
                    }
                }
                else
                {
                    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
                    msg.status = false;
                    msg.msg = "2.Error!!";
                }
                //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
                client.Dispose();
                msg.message = link + urlParameters;
            }
            catch (Exception ex)
            {
                msg.msg = "3.Error!!!";
                msg.message = ex.Message + "    " + fullLink;
            }

            msg.message = msgBody + "    :     " + msg.message;
            return msg;
        }

    }
    public static class EmailFunctions
    {

        public static Message SendEmailQuestion(Contact contact)
        {
            Message msg = new Message();
            try
            {
                string path = ProjConstant.Email.Path.question;
                string filePath = path.GetServerPathFolder();
                string body = filePath.ReadFile();

                if (String.IsNullOrWhiteSpace(contact.name))
                {
                    contact.name = "Applicant";
                }

                string emailBody = body.Replace("{#question#}", contact.question)
                    .Replace("{#name#}", contact.name)
                    .Replace("{#pmdcNo#}", contact.pmdcNo)
                                 .Replace("{#emailId#}", contact.emailId);
                msg = contact.emailId.SendEmail(ProjConstant.Email.Subject.contactUs, "PRP", emailBody);
            }
            catch (Exception ex)
            {

                msg.status = false;
                msg.message = ex.Message;
            }

            return msg;

        }
        public static Message SendEmail(this EmailProcess obj)
        {
            //, string subject, string emailTitle, string body
            Message msg = new Message();
            try
            {

                string emailIdFrom = WebConfigurationManager.AppSettings["MailEmailId"].ToString();
                string password = WebConfigurationManager.AppSettings["MailPassword"].ToString();
                string mailServer = WebConfigurationManager.AppSettings["MailServer"].ToString();
                int mailPort = WebConfigurationManager.AppSettings["MailPort"].TooInt();
                bool mailSSL = WebConfigurationManager.AppSettings["MailSSl"].TooBoolean();
                bool useDefaultCredentials = WebConfigurationManager.AppSettings["UseDefaultCredentials"].TooBoolean();

                var fromAddress = new MailAddress(emailIdFrom, obj.title);
                var toAddress = new MailAddress(obj.emailId);


                var smtp = new SmtpClient
                {
                    Host = mailServer,
                    Port = mailPort,
                    EnableSsl = mailSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = useDefaultCredentials,
                    Credentials = new NetworkCredential(fromAddress.Address, password)
                };


                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = obj.subject,
                    Body = obj.body
                })
                {
                    smtp.Send(message);
                }
                msg.status = true;
                msg.message = "Sent";

            }
            catch (Exception ex)
            {
                msg.status = false;

                msg.message = ex.Message;
            }

            return msg;

        }

        public static Message SendEmail(this string emailTo, string subject, string emailTitle, string body)
        {
            Message msg = new Message();
            try
            {

                string emailIdFrom = WebConfigurationManager.AppSettings["MailEmailId"].ToString();
                string password = WebConfigurationManager.AppSettings["MailPassword"].ToString();
                string mailServer = WebConfigurationManager.AppSettings["MailServer"].ToString();
                int mailPort = WebConfigurationManager.AppSettings["MailPort"].TooInt();
                bool mailSSL = WebConfigurationManager.AppSettings["MailSSl"].TooBoolean();
                bool useDefaultCredentials = WebConfigurationManager.AppSettings["UseDefaultCredentials"].TooBoolean();

                var fromAddress = new MailAddress(emailIdFrom, emailTitle);
                var toAddress = new MailAddress(emailTo);

                //MailAddress addressBCC = new MailAddress("phf.it.waheedahmad@gmail.com");

                var smtp = new SmtpClient
                {
                    Host = mailServer,
                    Port = mailPort,
                    EnableSsl = mailSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = useDefaultCredentials,
                    Credentials = new NetworkCredential(fromAddress.Address, password)
                };


                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                msg.status = true;
                msg.message = "Sent";

            }
            catch (Exception ex)
            {
                msg.status = false;

                msg.message = ex.Message;
            }

            return msg;

        }

        public static Message SendEmail0(this string emailTo, string subject, string emailTitle, string body)
        {
            Message msg = new Message();
            try
            {

                string emailIdFrom = WebConfigurationManager.AppSettings["MailEmailId0"].ToString();
                string password = WebConfigurationManager.AppSettings["MailPassword0"].ToString();
                string mailServer = WebConfigurationManager.AppSettings["MailServer"].ToString();
                int mailPort = WebConfigurationManager.AppSettings["MailPort"].TooInt();
                bool mailSSL = WebConfigurationManager.AppSettings["MailSSl"].TooBoolean();
                bool useDefaultCredentials = WebConfigurationManager.AppSettings["UseDefaultCredentials"].TooBoolean();

                var fromAddress = new MailAddress(emailIdFrom, emailTitle);
                var toAddress = new MailAddress(emailTo);

                //MailAddress addressBCC = new MailAddress("phf.it.waheedahmad@gmail.com");

                var smtp = new SmtpClient
                {
                    Host = mailServer,
                    Port = mailPort,
                    EnableSsl = mailSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = useDefaultCredentials,
                    Credentials = new NetworkCredential(fromAddress.Address, password)
                };


                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                msg.status = true;
                msg.message = "Sent";

            }
            catch (Exception ex)
            {
                msg.status = false;

                msg.message = ex.Message;
            }

            return msg;

        }

        public static Message SendEmail1(this string emailTo, string subject, string emailTitle, string body)
        {
            Message msg = new Message();
            try
            {

                string emailIdFrom = WebConfigurationManager.AppSettings["MailEmailId1"].ToString();
                string password = WebConfigurationManager.AppSettings["MailPassword1"].ToString();
                string mailServer = WebConfigurationManager.AppSettings["MailServer"].ToString();
                int mailPort = WebConfigurationManager.AppSettings["MailPort"].TooInt();
                bool mailSSL = WebConfigurationManager.AppSettings["MailSSl"].TooBoolean();
                bool useDefaultCredentials = WebConfigurationManager.AppSettings["UseDefaultCredentials"].TooBoolean();

                var fromAddress = new MailAddress(emailIdFrom, emailTitle);
                var toAddress = new MailAddress(emailTo);

                //MailAddress addressBCC = new MailAddress("phf.it.waheedahmad@gmail.com");

                var smtp = new SmtpClient
                {
                    Host = mailServer,
                    Port = mailPort,
                    EnableSsl = mailSSL,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = useDefaultCredentials,
                    Credentials = new NetworkCredential(fromAddress.Address, password)
                };


                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    IsBodyHtml = true,
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                msg.status = true;
                msg.message = "Sent";

            }
            catch (Exception ex)
            {
                msg.status = false;

                msg.message = ex.Message;
            }

            return msg;

        }

    }


}