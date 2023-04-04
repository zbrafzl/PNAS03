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
    public class EmployeeAdminController : BaseAdminController
    {
        [CheckHasRight]
        public ActionResult Setup()
        {
            EmployeeModelAdmin model = new EmployeeModelAdmin();
            int employeeId = Request.QueryString["id"].TooInt();

            var list = DDLConstant.GetAll("ByTypeIds", "5,11,12,15,39,701");

            model.listGender = list.Where(x => x.typeId == 5).ToList();
            model.listDegree = list.Where(x => x.typeId == 11).ToList();
            model.listProgram = list.Where(x => x.typeId == 12).ToList();
            model.listRelation = list.Where(x => x.typeId == 39).ToList();
            model.listMartialStatus = list.Where(x => x.typeId == 15).ToList();
            model.listDesignation = list.Where(x => x.typeId == 701).ToList();
            // model.listDispline = DDLDispline.GetAll("GetAll");
            // 25 teaching hospital
            model.listHospital = DDLHospital.GetAll(25, "GetTeaching");
            model.listDistrict = DDLRegion.GetAll(ProjConstant.Constant.Region.distric);


            if (employeeId > 0)
            {
                model.employee = new EmployeeDAL().GetById(employeeId);
                model.employeeSpeciality = new EmployeeDAL().GetEmployeeSpecialitySingleByEmployee(employeeId);
            }
            else
            {
                //model.employee.disciplineId = model.listDispline.FirstOrDefault().value.TooInt();
                model.employee.genderId = model.listGender.FirstOrDefault().value.TooInt();
                model.employee.relationId = model.listRelation.FirstOrDefault().value.TooInt();
                model.employee.martialStatusId = model.listMartialStatus.FirstOrDefault().value.TooInt();
                model.employee.districtId = 0;
                if (loggedInUser.reffId > 0)
                {
                    int districtId = 0;
                    try
                    {
                        Hospital ff = new HospitalDAL().GetById(loggedInUser.reffId);
                        districtId = ff.regionId;
                    }
                    catch (Exception)
                    {
                        districtId = 0;
                    }
                    model.employee.districtId = districtId;
                }
            }

            return View(model);
        }

        [HttpPost]
        public JsonResult EmployeeIsExistsCellNo(Employee obj)
        {
            obj.cellNo = obj.cellNo.TooString();
            obj.hospitalId = loggedInUser.reffId;
            Message msg = new EmployeeDAL().EmployeeIsExistsCellNo(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EmployeeIsExistsCNIC(Employee obj)
        {
            obj.cnic = obj.cnic.TooString();
            obj.hospitalId = loggedInUser.reffId;
            Message msg = new EmployeeDAL().EmployeeIsExistsCNIC(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult EmployeeSetupSave(EmployeeModelAdmin ModelSave, HttpPostedFileBase files)
        {
            Employee obj = ModelSave.employee;
            obj.relationName = obj.relationName.TooString();
            obj.image = obj.image.TooString();
            obj.adminId = loggedInUser.userId;
            obj.isActive = obj.isActive.TooBoolean();
            obj.joiningDate = obj.dateJoining.TooDate();
            obj.programIds = obj.programIds.TooString();
            if (loggedInUser.typeId == ProjConstant.Constant.UserType.hospital)
            {
                obj.hospitalId = loggedInUser.reffId;
            }
            Message m = new EmployeeDAL().AddUpdate(obj);

            if (obj.employeeId == 0)
                obj.employeeId = m.id;
            if (obj.employeeId > 0)
            {
                EmployeeSpeciality es = ModelSave.employeeSpeciality;
                es.employeeId = obj.employeeId;
                es.hospitalId = obj.hospitalId;
                es.adminId = loggedInUser.userId;
                Message mm = new EmployeeDAL().AddUpdateEmployeeSpeciality(es);
                m.msg = m.msg + " - " + mm.msg;
            }

            return Redirect(ModelSave.redirectUrl);
        }

        [CheckHasRight]
        public ActionResult Manage()
        {
            EmployeeModelAdmin model = new EmployeeModelAdmin();

            model.hospitalId = Request.QueryString["hospitalId"].TooInt();
            model.listHospital = DDLHospital.GetAll(25, "HasEmployee");


            if (loggedInUser.typeId != 81 && model.hospitalId == 0 && model.listHospital.Count > 0)
            {
                model.hospitalId = model.listHospital.FirstOrDefault().value.TooInt();
            }

            EmployeeSearch objSearch = new EmployeeSearch();
            objSearch.search = "";
            objSearch.adminId = loggedInUser.userId;
            objSearch.hospitalId = model.hospitalId;

            model.list = new EmployeeDAL().EmployeeSearch(objSearch);


            return View(model);
        }

        [HttpPost]
        public JsonResult EmployeeDelete(Employee obj)
        {
            Message msg = new EmployeeDAL().EmployeeDelete(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ManageExperience()
        {
            EmployeeModelAdmin model = new EmployeeModelAdmin();
            model.listHospital = DDLHospital.GetAll(25, "GetTeaching");
            model.employeeId = Request.QueryString["employeeId"].TooInt();
            model.listTypeExperience = DDLConstant.GetAll(ProjConstant.Constant.experienceEmployee);

            model.id = Request.QueryString["id"].TooInt();
            model.employee = new EmployeeDAL().GetDetailById(model.employeeId);

            EmployeeExperience objExp = new EmployeeExperience();
            objExp.employeeId = model.employee.employeeId;
            objExp.hospitalId = model.employee.hospitalId;
            objExp.adminId = loggedInUser.userId;
            model.listExperience = new EmployeeDAL().EmployeeExperienceGet(objExp);
            model.employeeExperience = new EmployeeDAL().EmployeeExperienceGetById(model.id);

            return View(model);
        }

        [HttpPost]
        public JsonResult AddUpdateExperience(EmployeeExperience obj)
        {
            obj.executionType = 2; // From Application
            obj.hospitalName = obj.hospitalName.TooString();
            obj.dateEnd = DateTime.Now;
            obj.dateStart = obj.startDate.TooDate();
            if (obj.isCurrent == false)
                obj.dateEnd = obj.endDate.TooDate();
            obj.adminId = loggedInUser.userId;
            Message msg = new EmployeeDAL().AddUpdateExperience(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        //[CheckHasRight]
        public ActionResult ManageSpecialization()
        {
            EmployeeSpecialityModelAdmin model = new EmployeeSpecialityModelAdmin();
            model.employeeId = Request.QueryString["employeeId"].TooInt();
            model.employee = new EmployeeDAL().GetDetailById(model.employeeId);
            model.id = Request.QueryString["id"].TooInt();

            model.speciality = new EmployeeDAL().GetEmployeeSpeciality(model.id);
            if (model.speciality.id == 0)
                model.speciality.hospitalId = model.employee.hospitalId;
            model.listProgram = DDLConstant.GetAll(ProjConstant.Constant.degreeType, model.employee.degreeId, "GetByPrgram");

            model.list = new EmployeeDAL().GetEmployeeSpecialities(model.employeeId);
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult EmployeeSpecialitySave(EmployeeSpecialityModelAdmin ModelSave, HttpPostedFileBase files)
        {
            EmployeeSpeciality obj = ModelSave.speciality;
            obj.adminId = loggedInUser.userId;
            obj.hospitalId = ModelSave.employee.hospitalId;
            obj.employeeId = ModelSave.employee.employeeId;
            Message mm = new EmployeeDAL().AddUpdateEmployeeSpeciality(obj);
            return Redirect(ModelSave.redirectUrl);
        }

        #region Applications

        //[CheckHasRight]
        public ActionResult ApplicantAssign()
        {
            EmployeeApplicantAdmin model = new EmployeeApplicantAdmin();
            model.employeeId = Request.QueryString["employeeId"].TooInt();
            model.hospitalId = Request.QueryString["hospitalId"].TooInt();
            if (loggedInUser.typeId == ProjConstant.Constant.UserType.hospital)
            {
                model.hospitalId = loggedInUser.reffId;
            }
            else
            {
                model.hospitalId = Request.QueryString["hospitalId"].TooInt();
                model.listHospital = DDLHospital.GetAll(25, "HasEmployee");
                try
                {
                    if (model.hospitalId == 0)
                        model.hospitalId = model.listHospital.FirstOrDefault().value.TooInt();
                }
                catch (Exception)
                {
                }
            }
            model.listEmployee = DDLEmployee.GetAll(model.hospitalId, "ByHospital");
            return View(model);
        }


        [HttpPost]
        public ActionResult ApplicantForTraineeYear(TraineeSearch obj)
        {

            DataTable dataTable = new TraineeDAL().ApplicantForTraineeYear(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public JsonResult EmployeeTraineeAddUpdate(EmployeeTrainee obj)
        {
            obj.adminId = loggedInUser.userId;
            Message msg = new TraineeDAL().EmployeeTraineeAddUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult TraineeAttachedGetByEmployee(TraineeSearch obj)
        {

            DataTable dataTable = new TraineeDAL().TraineeAttachedGetByEmployee(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        #endregion


        #region Reports


        public ActionResult EmployeeReport()
        {
            EmployeeModelAdmin model = new EmployeeModelAdmin();

            var list = DDLConstant.GetAll("ByTypeIds", "5,11,701");


            model.listHospital = DDLHospital.GetAll(loggedInUser.userId, "GetTeachingAndByUserType");
            if (model.listHospital != null && model.listHospital.Count == 1
                && loggedInUser.typeId != ProjConstant.Constant.UserType.hospital)
                model.hospitalId = loggedInUser.reffId;

            model.listGender = list.Where(x => x.typeId == 5).ToList();
            model.listDegree = list.Where(x => x.typeId == 11).ToList();
            model.listDesignation = list.Where(x => x.typeId == 701).ToList();
            model.listSpeciality = DDLSpeciality.GetAll("GetAllHasEmployee");


            return View(model);
        }

        [HttpPost]
        public ActionResult EmployeeReportData(EmployeeSearch obj)
        {
            obj.adminId = loggedInUser.userId;
            DataTable dataTable = new EmployeeDAL().EmployeeSearchReport(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        #endregion

    }
}