using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class ApplicationReviewAdminController : BaseAdminController
    {
        [CheckHasRight]
        // GET: nadmin/ApplicationReviewAdmin
        public ActionResult ApplicantList()
        {
            ApplicantStatusModel model = new ApplicantStatusModel();
            model.search.applicationStatusId = ProjConstant.Constant.ApplicationStatus.completed;
            model.search.condition = "GetByAccountApplicationStatusId";
            model.listApplicant = new ApplicantDAL().GetApplicantList(model.search);
            return View(model);
        }

        [CheckHasRight]
        public ActionResult ApplicantDetail()
        {
            ProofReadingAdminModel model = new ProofReadingAdminModel();
            try
            {

                int inductionId = 13;
                int phaseId = 1;

                string key = Request.QueryString["key"].TooString();
                string value = Request.QueryString["value"].TooString();

                if (!String.IsNullOrEmpty(key) && !String.IsNullOrWhiteSpace(value))
                {

                    Message msg = new ApplicantDAL().GetApplicantIdBySearch(value, key);
                    int applicantId = msg.id.TooInt();
                    if (applicantId > 0)
                    {
                        int applicationStatusId = 0;
                        try
                        {
                            ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(inductionId, ProjConstant.phaseId
                                              , applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                            applicationStatusId = objStatus.statusId;
                        }
                        catch (Exception)
                        {

                            applicationStatusId = 0;
                        }

                        if ((loggedInUser.typeId == ProjConstant.Constant.UserType.keVerification
                            || loggedInUser.typeId == ProjConstant.Constant.UserType.keSenior
                            || loggedInUser.typeId == ProjConstant.Constant.UserType.callCenter))
                        {
                            if (applicationStatusId == 8)
                            {
                                model = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, applicantId);
                                model.applicantId = applicantId;
                            }
                            else
                            {
                                model.applicantId = 0;
                                model.requestType = 2;
                            }
                        }
                        else
                        {
                            model = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, applicantId);
                            model.applicantId = applicantId;
                        }

                        if (applicationStatusId == 8)
                        {
                            try
                            {
                                model.statusApproval = new VerificationDAL().GetApplicationApprovalStatusGetByTypeAndId(ProjConstant.inductionId
                                                                                , ProjConstant.phaseId, 131, model.applicant.applicantId);
                            }
                            catch (Exception)
                            {
                                model.statusApproval = new ApplicantApprovalStatus();
                            }
                            try
                            {
                                model.statusAmendment = new VerificationDAL().GetApplicationApprovalStatusGetByTypeAndId(ProjConstant.inductionId
                                                                                , ProjConstant.phaseId, 132, model.applicant.applicantId);
                            }
                            catch (Exception)
                            {
                                model.statusAmendment = new ApplicantApprovalStatus();
                            }
                        }


                        model.search.key = key;
                        model.search.value = value;
                    }

                    model.applicantId = applicantId;
                    model.requestType = 1;
                }
                else
                {

                    model.applicantId = 0;
                    model.requestType = 2;
                }

                model.inductionId = inductionId;
                model.search.key = key;
                model.search.value = value;

            }
            catch (Exception)
            {
                model.applicantId = 0;
                model.requestType = 3;
            }

            Session["degreeAchieved"] = model.applicant.facultyId;
            Session["applicantId"] = model.applicant.applicantId;
            Session["appliedFor"] = model.applicant.levelId;

            return View(model);
        }
        public ActionResult AmendmentsApplicantNursing()
        {
            ProofReadingAdminModel model = new ProofReadingAdminModel();
            try
            {

                int inductionId = 13;
                int phaseId = 1;

                int applicantId = Request.QueryString["applicantId"].TooInt();

                if (1>0)
                {
                    if (applicantId > 0)
                    {
                        int applicationStatusId = 0;
                        try
                        {
                            ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(inductionId, ProjConstant.phaseId
                                              , applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                            applicationStatusId = objStatus.statusId;
                        }
                        catch (Exception)
                        {

                            applicationStatusId = 0;
                        }

                        if ((loggedInUser.typeId == ProjConstant.Constant.UserType.keVerification
                            || loggedInUser.typeId == ProjConstant.Constant.UserType.keSenior))
                        {
                            if (applicationStatusId == 8)
                            {
                                model = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, applicantId);
                                model.applicantId = applicantId;
                            }
                            else
                            {
                                model.applicantId = 0;
                                model.requestType = 2;
                            }
                        }
                        else
                        {
                            model = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, applicantId);
                            model.applicantId = applicantId;
                        }

                        if (applicationStatusId == 8)
                        {
                            try
                            {
                                model.statusApproval = new VerificationDAL().GetApplicationApprovalStatusGetByTypeAndId(ProjConstant.inductionId
                                                                                , ProjConstant.phaseId, 131, model.applicant.applicantId);
                            }
                            catch (Exception)
                            {
                                model.statusApproval = new ApplicantApprovalStatus();
                            }
                            try
                            {
                                model.statusAmendment = new VerificationDAL().GetApplicationApprovalStatusGetByTypeAndId(ProjConstant.inductionId
                                                                                , ProjConstant.phaseId, 132, model.applicant.applicantId);
                            }
                            catch (Exception)
                            {
                                model.statusAmendment = new ApplicantApprovalStatus();
                            }
                        }
                    }

                    model.applicantId = applicantId;
                    model.requestType = 1;
                }
                else
                {

                    model.applicantId = 0;
                    model.requestType = 2;
                }

                model.inductionId = inductionId;
            }
            catch (Exception)
            {
                model.applicantId = 0;
                model.requestType = 3;
            }

            AmendmentsApplicantNursing data = new ActionDAL().getAmendmentsApplicantNursingData(model.applicantId);
            model.amendmentsData = data;
            Session["degreeAchieved"] = model.applicant.facultyId;
            Session["applicantId"] = model.applicant.applicantId;
            Session["appliedFor"] = model.applicant.levelId;

            return View(model);
        }
        public ActionResult ApplicantDetailVerify()
        {
            ProofReadingAdminModel model = new ProofReadingAdminModel();
            try
            {

                int inductionId = 13;
                int phaseId = 1;

                string key = Request.QueryString["key"].TooString();
                string value = Request.QueryString["value"].TooString();

                if (!String.IsNullOrEmpty(key) && !String.IsNullOrWhiteSpace(value))
                {

                    Message msg = new ApplicantDAL().GetApplicantIdBySearch(value, key);
                    int applicantId = msg.id.TooInt();
                    if (applicantId > 0)
                    {
                        int applicationStatusId = 0;
                        try
                        {
                            ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(inductionId, ProjConstant.phaseId
                                              , applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                            applicationStatusId = objStatus.statusId;
                        }
                        catch (Exception)
                        {

                            applicationStatusId = 0;
                        }

                        if ((loggedInUser.typeId == ProjConstant.Constant.UserType.keVerification
                            || loggedInUser.typeId == ProjConstant.Constant.UserType.keSenior))
                        {
                            if (applicationStatusId == 8)
                            {
                                model = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, applicantId);
                                model.applicantId = applicantId;
                            }
                            else
                            {
                                model.applicantId = 0;
                                model.requestType = 2;
                            }
                        }
                        else
                        {
                            model = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, applicantId);
                            model.applicantId = applicantId;
                        }

                        if (applicationStatusId == 8)
                        {
                            try
                            {
                                model.statusApproval = new VerificationDAL().GetApplicationApprovalStatusGetByTypeAndId(ProjConstant.inductionId
                                                                                , ProjConstant.phaseId, 131, model.applicant.applicantId);
                            }
                            catch (Exception)
                            {
                                model.statusApproval = new ApplicantApprovalStatus();
                            }
                            try
                            {
                                model.statusAmendment = new VerificationDAL().GetApplicationApprovalStatusGetByTypeAndId(ProjConstant.inductionId
                                                                                , ProjConstant.phaseId, 132, model.applicant.applicantId);
                            }
                            catch (Exception)
                            {
                                model.statusAmendment = new ApplicantApprovalStatus();
                            }
                        }


                        model.search.key = key;
                        model.search.value = value;
                    }

                    model.applicantId = applicantId;
                    model.requestType = 1;
                }
                else
                {

                    model.applicantId = 0;
                    model.requestType = 2;
                }

                model.inductionId = inductionId;
                model.search.key = key;
                model.search.value = value;

            }
            catch (Exception)
            {
                model.applicantId = 0;
                model.requestType = 3;
            }

            Session["degreeAchieved"] = model.applicant.facultyId;
            Session["applicantId"] = model.applicant.applicantId;
            Session["appliedFor"] = model.applicant.levelId;

            return View(model);
        }

        [CheckHasRight]
        public ActionResult ApplicantView()
        {
            ProofReadingAdminModel model = new ProofReadingAdminModel();
            try
            {
                int inductionId = AdminHelper.GetInductionId();
                int phaseId = AdminHelper.GetPhaseId();

                string key = Request.QueryString["key"].TooString();
                string value = Request.QueryString["value"].TooString();

                model.search.key = key;
                model.search.value = value;

                if (!String.IsNullOrEmpty(key) && !String.IsNullOrWhiteSpace(value))
                {

                    Message msg = new ApplicantDAL().GetApplicantIdBySearch(value, key);
                    int applicantId = msg.id.TooInt();
                    if (applicantId > 0)
                    {
                        model = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, applicantId);

                        model.search.key = key;
                        model.search.value = value;
                    }

                    model.applicantId = applicantId;
                    model.requestType = 1;
                }
                else
                {

                    model.applicantId = 0;
                    model.requestType = 2;
                }

            }
            catch (Exception)
            {
                model.applicantId = 0;
                model.requestType = 3;
            }


            return View(model);
        }

        public ActionResult ApplicantViewDetail()
        {

            int applicantId = Request.QueryString["applicantId"].TooInt();
            int inductionId = AdminHelper.GetInductionId();
            int phaseId = AdminHelper.GetPhaseId();
            ProofReadingAdminModel model = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, applicantId);

            return View(model);
        }


        public ActionResult ApplicantViewDetailView()
        {

            int applicantId = Request.QueryString["applicantId"].TooInt();
            int inductionId = AdminHelper.GetInductionId();
            int phaseId = AdminHelper.GetPhaseId();
            ProofReadingAdminModel model = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, applicantId);

            return View(model);
        }



    }
}