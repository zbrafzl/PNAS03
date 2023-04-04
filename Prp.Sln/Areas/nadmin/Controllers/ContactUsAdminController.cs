using Newtonsoft.Json;
using Prp.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class ContactUsAdminController : BaseAdminController
    {
        // GET: nadmin/ContactUsAdmin

        public ActionResult ContactStatus()
        {
            ContactAdminModel model = new ContactAdminModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult ContactStatusByType(Contact obj)
        {
            obj.inductionId = ProjConstant.inductionId;
            obj.typeId = obj.typeId.TooInt();
            if (obj.typeId == 0)
                obj.typeId = 1;
            DataTable dataTable = new ContactDAL().GetContactStatus(ProjConstant.inductionId, obj.typeId);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        public ActionResult ContactListing()
        {
            ContactAdminModel model = new ContactAdminModel();
            model.contact.typeId = 1;

            return View(model);
        }

        [HttpGet]
        public JsonResult ContactQuestionGetNoReplied()
        {
            Message msg = new Message();
            int totalCount = new ContactDAL().ContactQuestionGetNoReplied(loggedInUser.userId);
            msg.id = totalCount;
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ContactAnswer()
        {
            ContactAdminModel model = new ContactAdminModel();
            int contactId = Request.QueryString["id"].TooInt();
            model.contact = new ContactDAL().GetById(contactId, loggedInUser.userId);
            model.listAnswer = new ContactDAL().GetAnswerListByContactId(contactId, loggedInUser.userId);
            model.listDocs = new ContactDAL().GetAllDocsByTypeAndContact(1, contactId);
            return View(model);
        }


        [HttpPost]
        public ActionResult ContactSearchByType(Contact obj)
        {
            obj.inductionId = ProjConstant.inductionId;
            if (obj.adminId == 0)
                obj.adminId = loggedInUser.userId;
            obj.search = obj.search.TooString();
            obj.typeId = obj.typeId.TooInt();
            if (obj.typeId == 0)
                obj.typeId = 1;
            DataTable dataTable = new ContactDAL().ContactQuestionSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }


        [ValidateInput(false)]
        public ActionResult ReplyAnswerSave(ContactAdminModel ModelSave, HttpPostedFileBase files)
        {
            ContactReply reply = ModelSave.reply;
            reply.contactId = ModelSave.contact.contactId;
            reply.adminId = loggedInUser.userId;

            reply.answer = reply.answer.TooString();
            if (!String.IsNullOrWhiteSpace(reply.answer))
            {
                Message m = new ContactDAL().ContactReplyAnswer(reply);
                try
                {
                    Message msgEmail = new Message();
                    try
                    {
                        Contact contact = new ContactDAL().GetById(reply.contactId, loggedInUser.userId);

                        string path = ProjConstant.Email.Path.replyQuestion;
                        string filePath = path.GetServerPathFolder();
                        string body = filePath.ReadFile();

                        if (String.IsNullOrWhiteSpace(contact.name))
                        {
                            contact.name = "Applicant";
                        }

                        string emailBody = body.Replace("{#question#}", contact.question)
                            .Replace("{#name#}", contact.name)
                            .Replace("{#pmdcNo#}", contact.pmdcNo)
                                         .Replace("{#emailId#}", contact.emailId).Replace("{#answer#}", reply.answer);


                        msgEmail = contact.emailId.SendEmail(ProjConstant.Email.Subject.contactUs, "PRP", emailBody);
                    }
                    catch (Exception ex)
                    {
                        msgEmail.status = false;
                        msgEmail.msg = ex.Message;
                    }
                }
                catch (Exception)
                {

                    throw;
                }

            }
            return Redirect("/admin/contact/reply?id=" + ModelSave.contact.contactId);
        }


        #region Grieviance


        public ActionResult ContactAttendence()

        {
            ContactAdminModel model = new ContactAdminModel();

            string url = HttpContext.Request.Url.AbsolutePath.ToLower();

            model.typeId = 11;

            if (url.Contains("verification"))
                model.typeId = 11;
            else if (url.Contains("gazette"))
                model.typeId = 21;
            return View(model);
        }

        public ActionResult ContactAttendenceCurrent()

        {
            ContactAdminModel model = new ContactAdminModel();

            string url = HttpContext.Request.Url.AbsolutePath.ToLower();

            model.applicantId = Request.QueryString["search"].TooInt();

            model.typeId = 11;

            if (url.Contains("verification"))
                model.typeId = 11;
            else if (url.Contains("gazette"))
                model.typeId = 21;
            return View(model);
        }

        public ActionResult ContactAttendenceMark()
        {
            ContactAdminModel model = new ContactAdminModel();

            int contactId = Request.QueryString["contactId"].TooInt();

            if (contactId > 0)
            {
                model.contact = new ContactDAL().GetById(contactId);
                //model.action = new GrievanceDAL().ActionGetById(grievanceId);
                model.applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, model.contact.applicantId);
            }

            DDLConstants dDLConstant = new DDLConstants();
            dDLConstant.condition = "";
            dDLConstant.typeId = ProjConstant.Constant.guardianType;
            model.listRelation = new ConstantDAL().GetConstantDDL(dDLConstant);


            return View(model);
        }

        public ActionResult ContactAttendenceView()
        {
            ContactAdminModel model = new ContactAdminModel();

            string url = HttpContext.Request.Url.AbsolutePath.ToLower();

            model.typeId = 11;

            if (url.Contains("verification"))
                model.typeId = 11;
            else if (url.Contains("gazette"))
                model.typeId = 21;
            return View(model);
        }

        [HttpPost]
        public ActionResult ContactAttendenceSearch(ContactSearch obj)
        {
            obj.search = obj.search.TooString();
            DataTable dataTable = new ContactDAL().ContactAttendenceSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult ContactAttendenceSearchCurrent(ContactSearch obj)
        {
            obj.search = obj.search.TooString();
            DataTable dataTable = new ContactDAL().ContactAttendenceSearchCurrent(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult ContactAttendencePrint(ContactSearch obj)
        {
            obj.search = obj.search.TooString();
            DataTable dataTable = new ContactDAL().ContactAttendencePrint(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult SaveAttendence(ContactAttendence obj)
        {
            obj.inductionId = ProjConstant.inductionId;
            obj.adminId = loggedInUser.userId;

            Message msg = new ContactDAL().AttendenceAddUpdate(obj);
            string json = JsonConvert.SerializeObject(msg, Formatting.Indented);
            return Content(json, "application/json");
        }





        #endregion

        #region Contact Griviance Status


        public ActionResult ContactStatusListCurrent()

        {
            ContactAdminModel model = new ContactAdminModel();

            string url = HttpContext.Request.Url.AbsolutePath.ToLower();

            model.typeId = 11;

            if (url.Contains("verification"))
                model.typeId = 11;
            else if (url.Contains("gazette"))
                model.typeId = 21;
            return View(model);
        }

        public ActionResult ContactStatusListAll()
        {
            ContactAdminModel model = new ContactAdminModel();

            string url = HttpContext.Request.Url.AbsolutePath.ToLower();

            model.typeId = 11;

            if (url.Contains("verification"))
                model.typeId = 11;
            else if (url.Contains("gazette"))
                model.typeId = 21;
            return View(model);
        }

        [HttpPost]
        public ActionResult ContactAttendenceStatusSearchCurrent(ContactSearch obj)
        {
            obj.search = obj.search.TooString();
            DataTable dataTable = new ContactDAL().ContactAttendenceStatusSearchCurrent(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        public ActionResult ContactAttendenceComment()
        {
            ContactAdminModel model = new ContactAdminModel();

            int contactId = Request.QueryString["contactId"].TooInt();

            if (contactId > 0)
            {
                model.contact = new ContactDAL().GetById(contactId);
                model.applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, model.contact.applicantId);

                model.status = new ContactDAL().GetContactStatusById(contactId);
            }

            DDLConstants dDLConstant = new DDLConstants();
            dDLConstant.condition = "";
            dDLConstant.typeId = ProjConstant.Constant.ApplicationStatusType.grievanceVerificationStatus;
            model.listStatus = new ConstantDAL().GetConstantDDL(dDLConstant);


            return View(model);
        }

        [HttpPost]
        public ActionResult SaveAttendenceStatus(ContactStatus obj)
        {
            obj.comments = obj.comments.TooString();
            obj.adminId = loggedInUser.userId;
            Message msg = new ContactDAL().AddUpdateStatus(obj);
            string json = JsonConvert.SerializeObject(msg, Formatting.Indented);
            return Content(json, "application/json");
        }



        public ActionResult ContactStatusPrintList()

        {
            ContactAdminModel model = new ContactAdminModel();

            string url = HttpContext.Request.Url.AbsolutePath.ToLower();

            model.typeId = 11;

            if (url.Contains("verification"))
                model.typeId = 11;
            else if (url.Contains("gazette"))
                model.typeId = 21;
            return View(model);
        }

        [HttpPost]
        public ActionResult ContactStatusPrintListSearch(ContactSearch obj)
        {
            DataTable dataTable = new ContactDAL().ContactAttendenceStatusPrint(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }


        #endregion
    }
}