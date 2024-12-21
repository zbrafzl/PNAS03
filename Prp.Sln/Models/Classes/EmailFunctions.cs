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
            if (number.Contains('-'))
            {

            }
            else
            {
                number = (number.Substring(0, 4) + '-' + number.Substring(4, 7)).ToString();
            }
            Message msg = new Message();
            msg = Task.Run(async () =>
            {
                return await SendSms_M1(number, message);
            }).GetAwaiter().GetResult();


            return msg;
        }

        public static async Task<Message> SendSms_M1(string number, string message)
        {
            Message msg = new Message();
            string msgBody = "";
            try
            {

                var url = ConfigurationManager.AppSettings["SmsApiLink2"].TooString();
                var password = ConfigurationManager.AppSettings["SmsPassword2"].TooString();

                var projectId = ConfigurationManager.AppSettings["projectId"].TooString();

                MsgBodyHisdu objBody = new MsgBodyHisdu();
                objBody.message = message;
                objBody.mobileNumber = number;
                objBody.projectId = projectId;
                // Add an Accept header for JSON format.
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(objBody);

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, url);
                request.Headers.Add("XApiKey", password);
                //var content = new StringContent("{\r\n \"id\": 0,\r\n \"mobileNumber\": \""+ number + "\",\r\n \"message\": \""+message+"\",\r\n  \"hospitalId\": \"PMF\",\r\n\"hospitalName\": \"Promotion\",\r\n\"projectId\": 1,\r\n \"masking\": \"HISDU\"\r\n}\r\n", null, "application/json");
                var content = new StringContent(json, null, "application/json");

                request.Content = content;
                var resp = client.SendAsync(request).Result;
                resp.EnsureSuccessStatusCode();
                string responseBody = await resp.Content.ReadAsStringAsync();

                msg.msg = responseBody;

                if (responseBody.ToLower().Contains("invalid"))
                {
                    msg.status = false;
                    msg.msg = msg.msg + " Status : Not Sent";
                }
                else
                {
                    msg.status = true;
                    msg.msg = msg.msg + " Status : Sent";
                }

                //if (resp.IsSuccessStatusCode == true)
                //{
                //    msg.status = true;
                //    msg.msg = "Sent";
                //}
                //else
                //{
                //    msg.status = false;
                //    msg.msg = "1.Error!";
                //}
                client.Dispose();
            }
            catch (Exception ex)
            {
                msg.msg = "3.Error!!!";
                msg.msg = "3.Error!!!  " + ex.Message;
            }

            msg.message = msg.msg + msgBody;
            return msg;
        }

        public static SmsAPI GetSmsUrl(string number, string message)
        {
            SmsAPI smsAPI = new SmsAPI();
            try
            {
                smsAPI.link = ConfigurationManager.AppSettings["SmsApiLink"];
                string item = "";
                if (string.IsNullOrWhiteSpace(item))
                {
                    item = "AHL%2fcJw8rwobY9hd2XefAq84EdiM8lf4GtDI08ob%2f2SciwVUqiYHKgN%2fNoFgo65deg%3d%3d";
                }
                string str1 = "?user=phf&pwd=#password#&sender=PHF&reciever=#number#&msg-data=#message#&response=string";
                number = string.Concat("92", number.Substring(1, number.Length - 1));
                str1 = str1.Replace("#password#", item);
                str1 = str1.Replace("#number#", number);
                smsAPI.url = str1.Replace("#message#", message);
                smsAPI.fullUrl = string.Concat(smsAPI.link, smsAPI.url);
            }
            catch (Exception exception2)
            {
                smsAPI.fullUrl = "";
            }
            return smsAPI;
        }
        public static async Task<Message> SendSmsAsync(string number, string message, int counter)
        {

            Message msg = new Message();
            try
            {
                var client = new HttpClient();
                string link = "https://pk.eocean.net/APIManagement/API/RequestAPI?user=phf&pwd=AHL%2fcJw8rwobY9hd2XefAq84EdiM8lf4GtDI08ob%2f2SciwVUqiYHKgN%2fNoFgo65deg%3d%3d&sender=PHF&reciever=#number#&msg-data=#message#&response=string";
                link = link.Replace("#number#", number).Replace("#message#", message);
                var request = new HttpRequestMessage(HttpMethod.Get, link);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());

                //var client = new HttpClient();
                //var request = new HttpRequestMessage(HttpMethod.Post, "https://pk.eocean.net/APIManagement/API/RequestAPI");
                //request.Headers.Add("User-Agent", "PostmanRuntime/7.34.0");
                //request.Headers.Add("Accept", "*/*");
                //request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                //request.Headers.Add("Connection", "keep-alive");
                //request.Headers.Add("XApiKey", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
                //var content = new StringContent("{\r\n    \"id\":0,\r\n    \"mobileNumber\":\"" + number + "\",\r\n    \"message\":\""+message.Replace("\n", "<br>") + "\",\r\n    \"hospitalId\":\"PMF\",\r\n    \"hospitalName\":\"Promotion\",\r\n    \"projectId\":77093443-318d-4d11-83a9-88a0120718b2,\r\n    \"masking\":\" PHF\"\r\n}", null, "application/json");
                //request.Content = content;
                //var response = await client.SendAsync(request);
                //response.EnsureSuccessStatusCode();
                //Console.WriteLine(await response.Content.ReadAsStringAsync());


            }
            catch (Exception ex)
            {

            }
            
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