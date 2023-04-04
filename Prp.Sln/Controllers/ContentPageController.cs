using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

using Prp.Data;

namespace Prp.Sln.Controllers
{
    public class ContentPageController : BaseLoginController
    {
        // GET: ContentPage
        public ActionResult PrivatePolicy()
        {
            ContentPageModel model = new ContentPageModel();
            return View(model);
        }

        public ActionResult TermAndCondition()
        {
            ContentPageModel model = new ContentPageModel();
            return View(model);
        }

        public ActionResult Notifications()
        {
            ContentPageModel model = new ContentPageModel();
            return View(model);
        }


        public ActionResult JobListing()
        {
            SpecialityJobModel model = new SpecialityJobModel();

            int typeId = Request.QueryString["typeId"].TooInt();

            model.listDegreeType = new ConstantDAL().GetAll(ProjConstant.Constant.degreeType).OrderBy(x => x.id).ToList();

            model.listQouta = new ConstantDAL().GetAll(ProjConstant.Constant.qoutaType).OrderBy(x => x.id).ToList();

            if (typeId == 0)
                typeId = model.listDegreeType.FirstOrDefault().id.TooInt();


            model.typeId = typeId;

            model.listSpecialityJob = new SpecialityDAL().GetSpecialityJobByTypeId(1, typeId);
            return View(model);
        }



        public ActionResult TestSMS()
        {
            ContentPageModel model = new ContentPageModel();

            Message msg = new Message();

            try
            {
                string number = Request.QueryString["number"].ToString();
                string message = "Dear Applicant, the testing message";
                msg = FunctionUI.SendSms(number, message);

                model.msg = msg.msg;
                model.content = msg.message;
            }
            catch (Exception ex)
            {
                model.msg = "Error";
                model.content = ex.Message;
            }
            return View(model);
        }

        public ActionResult TestEmail()
        {
            ContentPageModel model = new ContentPageModel();
            Message msg = new Message();
            try
            {
                string email = Request.QueryString["email"].TooString();
                if (!String.IsNullOrWhiteSpace(email))
                {
                    string body = "Test Body";
                    msg = email.SendEmail("Test Subject", "Test Title", body);
                    if (msg.status == true)
                    {
                        model.msg = "Email sent";
                    }
                    else
                    {
                        model.msg = msg.message;
                    }

                    model.key = email;
                }
                else
                {
                    model.key = email;
                    model.msg = "Email not provided.";
                }
               
            }
            catch (Exception)
            {

            }
            return View(model);
        }

        public ActionResult TestEmailCredentials()
        {
            ContentPageModel model = new ContentPageModel();
          

            ViewBag.message = TempData["MessageEmail"];
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult TestAndSendEmail(ContentPageModel ModelSave, HttpPostedFileBase files)
        {
            EmailEntity email = ModelSave.email;


            Message msg = new Message();
            try
            {

                

                string password = email.password.ToString();
                string mailServer = WebConfigurationManager.AppSettings["MailServer"].ToString();
                int mailPort = WebConfigurationManager.AppSettings["MailPort"].TooInt();
                bool mailSSL = WebConfigurationManager.AppSettings["MailSSl"].TooBoolean();
                bool useDefaultCredentials = WebConfigurationManager.AppSettings["UseDefaultCredentials"].TooBoolean();

                var fromAddress = new MailAddress(email.emailFrom, email.title);
                var toAddress = new MailAddress(email.emailId);

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
                    Subject = email.subject,
                    Body = email.body
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

            TempData["MessageEmail"] = msg;

            return Redirect("/email-test-credentials");
        }


        public Message TestingSMSSend()
        {
            Message msg = new Message();
            string message = "Dear Applicant, the testing message";
            try
            {
                //msg = FunctionUI.SendSms("03038132009", message);
            }
            catch (Exception)
            {
                msg = new Message();
            }
            return msg;
        }
    }
}