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
            if (!message.Contains("Dear Loanees"))
            {
                try

                {
                    string link = ConfigurationManager.AppSettings["SmsApiLink"];
                    if (String.IsNullOrWhiteSpace(link))
                    {
                        link = "https://pk.eocean.net/APIManagement/API/RequestAPI";
                    }
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

                            msgBody = msgBody + " if";
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

                        msgBody = msg.msg;
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
            }


            msg.message = msgBody + "    :     " + msg.message;
            return msg;
        }
        public static async Task<Message> SendSmsAsync(string number, string message, int counter)
        {

            Message msg = new Message();
            try
            {
                //var client = new HttpClient();
                //var request = new HttpRequestMessage(HttpMethod.Post, "http://116.58.20.67:1131/api/SMSServicesAPI/SentSMS");
                //request.Headers.Add("User-Agent", "PostmanRuntime/7.34.0");
                //request.Headers.Add("Accept", "*/*");
                //request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                //request.Headers.Add("Connection", "keep-alive");
                //request.Headers.Add("XApiKey", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
                //var content = new StringContent("{\r\n    \"id\":0,\r\n    \"mobileNumber\":\"" + number + "\",\r\n    \"message\":\"" + message + "\",\r\n    \"hospitalId\":\"PMF\",\r\n    \"hospitalName\":\"Promotion\",\r\n    \"projectId\":1,\r\n    \"masking\":\" PHF\"\r\n}", null, "application/json");
                //request.Content = content;
                //var response = await client.SendAsync(request);
                //response.EnsureSuccessStatusCode();
                //Console.WriteLine(await response.Content.ReadAsStringAsync());

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://116.58.20.67:1131/api/SMSServicesAPI/SentSMS");
                request.Headers.Add("User-Agent", "PostmanRuntime/7.34.0");
                request.Headers.Add("Accept", "*/*");
                request.Headers.Add("Accept-Encoding", "gzip, deflate, br");
                request.Headers.Add("Connection", "keep-alive");
                request.Headers.Add("XApiKey", "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp");
                var content = new StringContent("{\r\n    \"id\":0,\r\n    \"mobileNumber\":\"" + number + "\",\r\n    \"message\":\""+message.Replace("\n", "<br>") +"\",\r\n    \"hospitalId\":\"PMF\",\r\n    \"hospitalName\":\"Promotion\",\r\n    \"projectId\":1,\r\n    \"masking\":\" PHF\"\r\n}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());


            }
            catch (Exception ex)
            {

            }
            
            //string msgBody = "";
            //string result = "";
            //string fullLink = "";
            //if (!message.Contains("Dear Loanees"))
            //{
            //    try

            //    {
            //        string link = ConfigurationManager.AppSettings["SmsApiLink"];
            //        if (String.IsNullOrWhiteSpace(link))
            //        {
            //            link = "http://116.58.20.67:1131/api/SMSServicesAPI/SentSMS";
            //        }
            //        link = "http://116.58.20.67:1131/api/SMSServicesAPI/SentSMS";
            //        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(link);
            //        httpWebRequest.ContentType = "application/json; charset=utf-8";
            //        httpWebRequest.Method = "POST";
            //        httpWebRequest.Headers["XApiKey"] = "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp";
            //        using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            //        {
            //            string json = "[  { \"id\": \"0\", \"mobileNumber\": \""+number+ "\",\"message\": \"" + message + "\",\"hospitalId\": \" PMF \",\"hospitalName\": \" Promotion \",\"projectId\": 1, \"masking\": \" HISDU \" } ]";
            //            streamWriter.Write(json);
            //            streamWriter.Flush();
            //            streamWriter.Close();
            //        }
            //        try
            //        {
            //            using (var response = httpWebRequest.GetResponse() as HttpWebResponse)
            //            {
            //                if (httpWebRequest.HaveResponse && response != null)
            //                {
            //                    using (var reader = new StreamReader(response.GetResponseStream()))
            //                    {
            //                        result = reader.ReadToEnd();
            //                    }
            //                }
            //            }
            //        }
            //        catch (WebException e)
            //        {
            //            if (e.Response != null)
            //            {
            //                using (var errorResponse = (HttpWebResponse)e.Response)
            //                {
            //                    using (var reader = new StreamReader(errorResponse.GetResponseStream()))
            //                    {
            //                        string error = reader.ReadToEnd();
            //                        result = error;
            //                    }
            //                }

            //            }
            //        }


            //        //string password = "";
            //        //try
            //        //{
            //        //    password = ConfigurationManager.AppSettings["SmsPassword"];
            //        //}
            //        //catch (Exception)
            //        //{
            //        //    password = "";
            //        //}
            //        //link = "http://116.58.20.67:1131/api/SMSServicesAPI/SentSMS";
            //        //if (String.IsNullOrWhiteSpace(password))
            //        //{
            //        //    try
            //        //    {
            //        //        string path = ProjConstant.SMS.Path.smsPassword;
            //        //        string filePath = Path.Combine(HttpContext.Current.Server.MapPath(path));
            //        //        password = filePath.ReadFile();
            //        //        password = password.Trim();
            //        //    }
            //        //    catch (Exception)
            //        //    {
            //        //        password = "";
            //        //    }

            //        //}

            //        //if (String.IsNullOrWhiteSpace(password))
            //        //{
            //        //    password = "AHL%2fcJw8rwobY9hd2XefAq84EdiM8lf4GtDI08ob%2f2SciwVUqiYHKgN%2fNoFgo65deg%3d%3d";
            //        //}

            //        //string urlParameters = "?id=0&mobileNumber=#number#&message=#message#&hospitalId=PMF&hospitalName=Promotion&projectId=1&masking=HISDU";
            //        //number = "92" + number.Substring(1, number.Length - 1);

            //        //urlParameters = urlParameters.Replace("#password#", password);
            //        //urlParameters = urlParameters.Replace("#number#", number);
            //        //urlParameters = urlParameters.Replace("#message#", message);

            //        //fullLink = link + urlParameters;

            //        //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(fullLink);
            //        //request.Method = "POST";
            //        //request.ContentType = "application/json";
            //        //request.ContentLength = 0;
            //        //request.Expect = "application/json";

            //        //request.Headers["XApiKey"] = "pgH7QzFHJx4w46fI~5Uzi4RvtTwlEXp";

            //        //try
            //        //{
            //        //    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //        //    if (response.StatusCode == HttpStatusCode.OK)
            //        //    {
            //        //        using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            //        //        {
            //        //            string responseText = streamReader.ReadToEnd();
            //        //            msg.message = responseText;
            //        //        }
            //        //    }
            //        //    else
            //        //    {
            //        //        msg.message = "Error: " + response.StatusCode;
            //        //    }
            //        //}
            //        //catch (Exception ex)
            //        //{
            //        //    msg.message = "Error: " + ex.Message;
            //        //}

            //        //HttpClient client = new HttpClient();
            //        //client.BaseAddress = new Uri(link);

            //        //// Add an Accept header for JSON format.
            //        //client.DefaultRequestHeaders.Accept.Add(
            //        //new MediaTypeWithQualityHeaderValue("application/json"));

            //        //// List data response.
            //        //HttpResponseMessage response = client.GetAsync(urlParameters).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            //        //if (response.IsSuccessStatusCode)
            //        //{
            //        //    var responseContent = response.Content;
            //        //    msgBody = responseContent.ReadAsStringAsync().GetAwaiter().GetResult().ToLower();

            //        //    if (msgBody.Contains("successfully"))
            //        //    {
            //        //        msg.status = true;
            //        //        msg.msg = "Sent";

            //        //        msgBody = msgBody + " if";
            //        //    }
            //        //    else
            //        //    {
            //        //        msg.status = false;
            //        //        msg.msg = "1.Error!";
            //        //    }
            //        //}
            //        //else
            //        //{
            //        //    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            //        //    msg.status = false;
            //        //    msg.msg = "2.Error!!";

            //        //    msgBody = msg.msg;
            //        //}
            //        ////Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            //        //client.Dispose();
            //        //msg.message = link + urlParameters;
            //    }
            //    catch (Exception ex)
            //    {
            //        msg.msg = "3.Error!!!";
            //        msg.message = ex.Message + "    " + fullLink;
            //    }
            //}


            //msg.message = msgBody + "    :     " + msg.message;
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