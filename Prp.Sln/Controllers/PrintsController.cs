using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
    public class PrintsController : BaseController
    {

        public ActionResult Application()
        {
            return View();
        }

        public ActionResult ApplicationTest()
        {
            return View();
        }
        // GET: Prints
        public ActionResult Vouchers()
        {
            VoucherModel model = new VoucherModel();
            return View(model);
        }


        public ActionResult GrievanceGazzettePrint()
        {
            ContactModel model = new ContactModel();
            string url = HttpContext.Request.Url.AbsolutePath;

            if (url == ("/print/grievance-verification"))
                model.typeId = 11;
            else if (url == ("/print/grievance-gazzette"))
                model.typeId = 21;

            model.listQuestion = new ContactDAL().GetQuestionByApplicant(model.typeId, ProjConstant.inductionId, loggedInUser.applicantId);
            model.contact = model.listQuestion.Where(x => x.typeId == model.typeId && x.totalReply == 0).FirstOrDefault();
            model.applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, loggedInUser.applicantId);
            try
            {
                model.listStatusApproval = new VerificationDAL().GetApplicationApprovalStatusGetById(ProjConstant.inductionId, ProjConstant.phaseId, loggedInUser.applicantId);
            }
            catch (Exception)
            {
            }
           

            return View(model);
        }
    }
}