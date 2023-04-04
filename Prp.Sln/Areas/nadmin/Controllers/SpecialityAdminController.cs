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
    public class SpecialityAdminController : BaseAdminController
    {
        public ActionResult SpecialityJobSetup()
        {
            SpecialityJobModelAdmin model = new SpecialityJobModelAdmin();
            model.job.specialityId = Request.QueryString["specialityId"].TooInt();
            model.job.instituteId = Request.QueryString["instituteId"].TooInt();
            model.job.isActive = true;

            model.listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasHospital); 

            DDLSpecialitys dDLSpeciality = new DDLSpecialitys();
            dDLSpeciality.condition = "GetAll";
            model.listSpeciality = new SpecialityDAL().GetSpecialityDDL(dDLSpeciality);

            model.listDegreeType = new ConstantDAL().GetAll(ProjConstant.Constant.degreeType).OrderBy(x=>x.id).ToList();

            model.listQouta = new ConstantDAL().GetAll(ProjConstant.Constant.qoutaType).OrderBy(x => x.id).ToList();


            model.listInduction = DDLInduction.GetAll("GetAllActive"); 

            if (model.listInduction != null && model.listInduction.Count == 1)
                model.job.inductionId = model.listInduction.FirstOrDefault().id.TooInt();

            model.listSpecialityJob = new SpecialityDAL().GetSpecialityJobByParameters(model.job.inductionId, model.job.specialityId);

            return View(model);
        }

        public ActionResult JobInstituteWise()
        {
            SpecialityJobModelAdmin model = new SpecialityJobModelAdmin();
            model.job.specialityId = Request.QueryString["specialityId"].TooInt();
            model.job.instituteId = Request.QueryString["instituteId"].TooInt();
            model.job.isActive = true;

            model.listInstitute = DDLInstitute.GetAll(loggedInUser.userId, ProjConstant.DDL.Institute.hasHospitalByUserId);
          
            DDLSpecialitys dDLSpeciality = new DDLSpecialitys();
            dDLSpeciality.condition = "GetAll";
            model.listSpeciality = new SpecialityDAL().GetSpecialityDDL(dDLSpeciality);

            model.listDegreeType = new ConstantDAL().GetAll(ProjConstant.Constant.degreeType).OrderBy(x => x.id).ToList();
            model.listQouta = new ConstantDAL().GetAll(ProjConstant.Constant.qoutaType).OrderBy(x => x.id).ToList();
            model.listInduction = DDLInduction.GetAll("GetAllActive");

            if (model.listInduction != null && model.listInduction.Count == 1)
                model.job.inductionId = model.listInduction.FirstOrDefault().id.TooInt();

            if (model.job.inductionId > 0 && model.job.specialityId > 0)
            {
                model.listSpecialityJob = new SpecialityDAL().GetSpecialityJobByParameters(model.job.inductionId, model.job.specialityId);
            }

            return View(model);
        }

        public ActionResult JobHospitalWise()
        {
            int hospitalId = Request.QueryString["hospitalId"].TooInt();
            if (hospitalId > 0)
            {
                
            }

            return View();
        }

        [CheckHasRight]
        public ActionResult JobApplicationSearch()
        {
            SpecialityJobModelAdmin model = new SpecialityJobModelAdmin();
            model.inductionId = Request.QueryString["instituteId"].TooInt();
            if (model.inductionId == 0)
                model.inductionId = ProjConstant.inductionId;
            DDLConstants dDLInstitute = new DDLConstants();
            dDLInstitute.typeId = ProjConstant.Constant.degreeType;
            model.listType = new ConstantDAL().GetConstantDDL(dDLInstitute);
            return View(model);
        }

        [HttpPost]
        public ActionResult SpecialityJobPrefferenceSearch(SpecialityJobSearch obj)
        {
            obj.phaseId = ProjConstant.phaseId;
            DataTable dataTable = new SpecialityDAL().SpecialityJobPrefferenceSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpGet]
        public JsonResult GetSpecialityJobById(int specialityJobId)
        {
            SpecialityJob specialityJob = new SpecialityDAL().GetSpecialityJobById(specialityJobId);
            return Json(specialityJob, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult SaveSpecialityJobData(SpecialityJobModelAdmin ModelSave, HttpPostedFileBase files)
        {
            SpecialityJob obj = ModelSave.job;
            obj.dated = DateTime.Now;
            obj.adminId = loggedInUser.userId;
            obj.isActive = obj.isActive.TooBoolean();
            Message m = new SpecialityDAL().AddUpdate(obj);
            return Redirect("/admin/speciality-job?specialityId="+ obj.specialityId);
        }


        public ActionResult JobListing()
        {
            SpecialityJobModelAdmin model = new SpecialityJobModelAdmin();
            int typeId = Request.QueryString["typeId"].TooInt();
            model.listDegreeType = new ConstantDAL().GetAll(ProjConstant.Constant.degreeType).OrderBy(x => x.id).ToList();
            model.listQouta = new ConstantDAL().GetAll(ProjConstant.Constant.qoutaType).OrderBy(x => x.id).ToList();
            if (typeId == 0)
                typeId = model.listDegreeType.FirstOrDefault().id.TooInt();

            model.typeId = typeId;
            model.listSpecialityJob = new SpecialityDAL().GetSpecialityJobByTypeId(ProjConstant.inductionId, typeId);

            return View(model);
        }


        public ActionResult SeatsStatusList()
        {
            SpecialityJobModelAdmin model = new SpecialityJobModelAdmin();
            int typeId = Request.QueryString["typeId"].TooInt();
            model.listDegreeType = new ConstantDAL().GetAll(ProjConstant.Constant.degreeType).OrderBy(x => x.id).ToList();
            model.listQouta = new ConstantDAL().GetAll(ProjConstant.Constant.qoutaType).OrderBy(x => x.id).ToList();
            if (typeId == 0)
                typeId = model.listDegreeType.FirstOrDefault().id.TooInt();

            model.typeId = typeId;
            model.listSpecialityJob = new SpecialityDAL().GetSpecialityJobByTypeId(ProjConstant.inductionId, typeId);

            return View(model);
        }

        [HttpPost]
        public ActionResult ApplicantSearchList(ApplicantSearch obj)
        {
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            DataTable dataTable = new ApplicantAdminDAL().ApplicantSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        #region Displine



        #endregion

    }
}