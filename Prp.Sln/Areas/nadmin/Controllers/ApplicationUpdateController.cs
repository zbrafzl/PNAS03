using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class ApplicationUpdateController : BaseAdminController
    {
        [CheckHasRight]
        public ActionResult ExperienceAdmin()
        {
            ExperienceModelAdmin model = new ExperienceModelAdmin();
            try
            {
                int applicantId = Request.QueryString["applicantid"].TooInt();
                if (applicantId > 0)
                {
                    model.applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, applicantId);
                    model.listProvince = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.province
                        , ProjConstant.Constant.pakistan);

                    model.listExperience = new ApplicantDAL().GetApplicantExperienceListByApplicant(model.inductionId, model.phaseId, applicantId);

                    model.listMarks = new MarksDAL().GetMarksAccumulativeByApplicant(applicantId);

                }
            }
            catch (Exception)
            {
                model = new ExperienceModelAdmin();
            }
            return View(model);
        }


        [HttpGet]
        public JsonResult GetApplicantExperienceDataSingleRow(int applicantId, int applicantExperienceId)
        {
            ApplicantExperience obj = new ApplicantExperience();

            try
            {
                List<ApplicantExperience> list = new ApplicantDAL().GetApplicantExperienceListByApplicant(ProjConstant.inductionId, ProjConstant.phaseId, applicantId);
                if (list != null && list.Count > 0)
                {
                    obj = list.FirstOrDefault(x => x.applicantExperienceId == applicantExperienceId);
                }
            }
            catch (Exception)
            {
                obj = new ApplicantExperience();
            }
          
            return Json(obj, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ApplicantExperienceAddUpdate(ApplicantExperienceParam objExperience)
        {
            ApplicantExperience obj = new ApplicantExperience();
            obj.applicantExperienceId = objExperience.applicantExperienceId;
            obj.applicantId = objExperience.applicantId;
            obj.levelId = objExperience.levelId;
            obj.levelTypeId = objExperience.levelTypeId;
            obj.instituteId = objExperience.instituteId;
            obj.provinceId = objExperience.provinceId;
            obj.typeId = objExperience.typeId;
            obj.instituteName = objExperience.instituteName;
            obj.imageExperience = objExperience.imageExperience;
            obj.isCurrent = objExperience.isCurrent;
            obj.startDated = objExperience.startDated.TooDate();
            if (obj.isCurrent)
                obj.endDate = DateTime.Now;
            else
                obj.endDate = objExperience.endDate.TooDate();

            obj.adminId = loggedInUser.userId;

            Message msg = new ApplicantAdminDAL().ApplicantExperienceAddUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

    }
}