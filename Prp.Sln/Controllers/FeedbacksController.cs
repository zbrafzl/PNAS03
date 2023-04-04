using Newtonsoft.Json;
using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
    public class FeedbacksController : BaseController
    {
        // GET: Feedback
        public ActionResult FeedBackByApplicant()
        {
            FeedBackModel model = new FeedBackModel();
            model.listFeedBack = new FeedBackDAL().GetByApplicant(loggedInUser.applicantId);
            return View(model);
        }

        //public ActionResult GrievanceApplicationMarks()
        //{
        //    GrievanceModel model = new GrievanceModel();

        //    model.grievanceId = Request.QueryString["grievanceId"].TooInt();

        //    model.grievance = new GrievanceDAL().GetByApplicantAndType(loggedInUser.applicantId, ProjConstant.Constant.GrievanceType.gazatMarks);

        //    int adminId = model.grievance.adminIdStatus;

        //    if (adminId == 0)
        //        model.isEditAble = true;
        //    else model.isEditAble = false;

        //    return View(model);
        //}

        public ActionResult GrievanceApplication()
        {
            ContactModel model = new ContactModel();
            model.isChangeAble = false;

            string url = HttpContext.Request.Url.AbsolutePath;

            model.redirectUrl = url;

            url = url.TrimStart('/');

            model.typeId = 1;
            if (url == (ProjConstant.preUrl + "/grievance-verification-application"))
                model.typeId = 11;
            else if (url == (ProjConstant.preUrl + "/grievance-gazette-application"))
                model.typeId = 21;



            model.listQuestion = new ContactDAL().GetQuestionByApplicant(model.typeId, ProjConstant.inductionId, loggedInUser.applicantId);

            if (model.listQuestion != null && model.listQuestion.Count > 0)
            {
                Contact contanct = model.listQuestion.Where(x => x.typeId == model.typeId && x.totalReply == 0).FirstOrDefault();
                if (contanct != null && contanct.contactId > 0)
                {
                    model.contact.contactId = contanct.contactId;
                    model.contact.typeId = contanct.typeId;
                    model.contact.applicantId = contanct.applicantId;
                    model.contact.name = loggedInUser.name;
                    model.contact.emailId = contanct.emailId;
                    model.contact.pmdcNo = contanct.pmdcNo;
                    model.contact.title = contanct.pmdcNo;
                    model.contact.question = contanct.question;
                    model.isChangeAble = false;
                }
                
            }
            else
            {
                model.contact.contactId = 0;
                model.contact.typeId = model.typeId;
                model.contact.name = loggedInUser.name;
                model.contact.pmdcNo = loggedInUser.pmdcNo;
                model.contact.emailId = loggedInUser.emailId;
                model.isChangeAble = true;
                model.contact.typeId = model.typeId;
            }

            ViewBag.message = TempData["Message"];

            return View(model);
        }




        [ValidateInput(false)]
        public ActionResult SaveFeedbackData(FeedBackModel ModelSave, HttpPostedFileBase files)
        {
            FeedBack obj = ModelSave.feedBack;


            obj.applicantIdBy = loggedInUser.applicantId.TooInt();
            obj.pmdcNo = obj.pmdcNo.TooString();
            obj.comments = obj.comments.TooString();
            obj.dated = DateTime.Now;


            Message m = new FeedBackDAL().AddUpdate(obj);

            return Redirect("/applicant/feedback");
        }

        [ValidateInput(false)]
        public ActionResult SaveGrievanceData(GrievanceModel ModelSave, HttpPostedFileBase files)
        {
            Grievance obj = ModelSave.grievance;
            obj.grievanceId = obj.grievanceId.TooInt();
            obj.typeId = 6;
            obj.typeId.TooInt();
            obj.verficationTypeId = obj.verficationTypeId.TooInt();
            obj.checkListIds = obj.checkListIds.TooString();

            obj.applicantId = loggedInUser.applicantId.TooInt();
            obj.detail = obj.detail.TooString();
            obj.title = obj.title.TooString();

            Message m = new GrievanceDAL().AddUpdate(obj);
            return Redirect("/grievance-gazzette");
        }



        public ActionResult ConactUs()
        {
            ContactModel model = new ContactModel();
            model.contact.typeId = 1;
            model.contact.name = loggedInUser.name;
            model.contact.emailId = loggedInUser.emailId;
            model.contact.pmdcNo = loggedInUser.pmdcNo;

            model.listQuestion = new ContactDAL().GetQuestionByApplicant(1, ProjConstant.inductionId, loggedInUser.applicantId);

            ViewBag.message = TempData["Message"];

            return View(model);
        }

        public ActionResult ConactUsDetail()
        {
            ContactModel model = new ContactModel();
            int contactId = Request.QueryString["id"].TooInt();
            model.contact = new ContactDAL().GetByApplicantId(ProjConstant.inductionId, contactId, loggedInUser.applicantId);
            if (model.contact.contactId > 0)
            {
                model.listAnswer = new ContactDAL().GetAnswerListByApplicant(contactId, loggedInUser.applicantId);
                model.listDocs = new ContactDAL().GetAllDocsByTypeAndContact(1, contactId);
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ContactUsSave(ContactModel model, IEnumerable<HttpPostedFileBase> flImages)
        {
            string url = Request.Url.AbsoluteUri;

            Message mssgg = new Message();
            Message msg = new Message();
            int applicantId = 0;
            try
            {
                applicantId = loggedInUser.applicantId;

            }
            catch (Exception)
            {

                applicantId = 0;
            }

            Contact contact = model.contact;
            contact.applicantId = applicantId;

            if (applicantId > 0)
            {

                contact.emailId = loggedInUser.emailId;
                contact.name = loggedInUser.name;
                contact.pmdcNo = loggedInUser.pmdcNo;
            }

            try
            {

                mssgg = new ContactDAL().AddUpdate(contact);

                if (contact.typeId == 1)
                {
                    if (mssgg.id > 0)
                        EmailFunctions.SendEmailQuestion(contact);
                }


                string images = "";
                try
                {
                    if (flImages != null && flImages.Count() > 0 && flImages.ToList()[0] != null)
                    {
                        images = Files.SaveMultipleFiles(flImages, "/images/contact/");
                    }
                }
                catch (Exception ex)
                {
                    images = "";
                }

                images = images.TrimEnd(',');

                if (!String.IsNullOrWhiteSpace(images))
                {
                    msg = new ContactDAL().AddUpdateDocs(mssgg.id, images, 1);
                }

            }
            catch (Exception ex)
            {
                mssgg.id = 0;
                mssgg.status = false;
                mssgg.msg = ex.Message;
            }

            TempData["Message"] = mssgg;

            return Redirect(model.redirectUrl);
        }


    }
}