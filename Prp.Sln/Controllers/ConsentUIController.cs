using Newtonsoft.Json;
using Prp.Data;
using Prp.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
    public class ConsentUIController : BaseController
    {
        // GET: ConsentUI
        public ActionResult ApplicantConsent()
        {
            ConsentModel model = new ConsentModel();


            int roundId = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();
            model.roundId = roundId;

            model.applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, loggedInUser.applicantId);
            model.consentId = Request.QueryString["consentId"].TooInt();

            DDLConstants dDLConstant = new DDLConstants();
            dDLConstant.condition = "ByType";
            dDLConstant.typeId = ProjConstant.Constant.consentType;
            model.listConsentStatus = new ConstantDAL().GetConstantDDL(dDLConstant).OrderBy(x => x.id).ToList();


            if (model.validStatus == 1)
            {

                if (model.consentId > 0)
                {
                    model.consent = new ConsentDAL().GetById(model.consentId);

                    if (model.consent.applicantId != loggedInUser.applicantId)
                        model.validStatus = 0;
                }
                if (model.validStatus == 1)
                {

                    model.listType = new ConsentDAL().GetTypeApplicantHasMerit(loggedInUser.applicantId, roundId);
                    //if (model.listType != null && model.listType.Count > 0)
                    model.listConsent = new ConsentDAL().GetAllByApplicant(loggedInUser.applicantId);
                }


            }

            return View(model);
        }


        public ActionResult ApplicantJoining()
        {
            ApplicantJoiningModel model = new ApplicantJoiningModel();
            model.applicantFinal = new JoiningDAL().GetByApplicantDetailById(loggedInUser.applicantId);
            model.info = new ApplicantDAL().GetApplicantInfo(ProjConstant.inductionId, ProjConstant.phaseId, loggedInUser.applicantId);
            return View(model);
        }


        [ValidateInput(false)]
        public ActionResult SaveConsentData(ConsentModel ModelSave, HttpPostedFileBase files)
        {

            int roundId = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();
            Consent obj = ModelSave.consent;
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            obj.consentId = obj.consentId.TooInt();
            obj.applicantId = loggedInUser.applicantId;
            obj.typeId = obj.typeId.TooInt();
            obj.consentTypeId = obj.consentTypeId.TooInt();
            obj.roundId = roundId;
            Message m = new ConsentDAL().AddUpdate(obj);
            return Redirect("/"+ProjConstant.preUrl+"/applicant/consent");
        }
    }
}