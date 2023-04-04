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
    public class HospitalsController : BaseAdminController
    {
        // GET: nadmin/Hospitals
        public ActionResult HospitalSetup()
        {
            HospitalModelAdmin model = new HospitalModelAdmin();
            int hospitalId = Request.QueryString["id"].TooInt();
            if (hospitalId > 0)
                model.hospital = new HospitalDAL().GetById(hospitalId);

            model.listDistrict = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.distric, 0, "GetDistrictAll");


            DDLConstants dDLConstant = new DDLConstants();
            dDLConstant.reffIds = "23,24,25";
            dDLConstant.condition = "ReffIdsParent";
            model.listLevel = new ConstantDAL().GetConstantDDL(dDLConstant);

            dDLConstant = new DDLConstants();
            dDLConstant.reffIds = "23,24,25";
            dDLConstant.condition = "ReffIdsChild";
            model.listType = new ConstantDAL().GetConstantDDL(dDLConstant);

            return View(model);
        }


        [HttpGet]
        public JsonResult GetHospitalById(int hospitalId)
        {
            Hospital hospital = new Hospital();
            try
            {
                hospital = new HospitalDAL().GetById(hospitalId);
            }
            catch (Exception)
            {
                hospital = new Hospital();
            }
            return Json(hospital, JsonRequestBehavior.AllowGet);
        }

        public ActionResult HospitalManage()
        {
            HospitalModelAdmin model = new HospitalModelAdmin();

            DDLConstants dDLConstant = new DDLConstants();
            dDLConstant.condition = "";
            dDLConstant.typeId = ProjConstant.Constant.instituteLevel;
            model.listLevel = new ConstantDAL().GetConstantDDL(dDLConstant);


            DDLRegions dDLRegion = new DDLRegions();
            dDLRegion.condition = "ByType";
            dDLRegion.typeId = ProjConstant.Constant.Region.distric;
            model.listRegion = new MasterSetupDAL().GetRegionDDL(dDLRegion);

            return View(model);
        }

        [HttpPost]
        public ActionResult HospitalSearch(HospitalSearch obj)
        {
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            DataTable dataTable = new HospitalDAL().HospitalSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public JsonResult GetHospitalDDL(DDLHospitals obj)
        {
            List<EntityDDL> list = new HospitalDAL().GetHospitalDDL(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult SaveHospitalData(HospitalModelAdmin ModelSave, HttpPostedFileBase files)
        {
            Hospital obj = ModelSave.hospital;

            obj.name = obj.name.TooString();
            obj.code = obj.code.TooString();
            obj.nameDisplay = obj.nameDisplay.TooString();
            obj.abbr = obj.abbr.TooString();
            obj.isActive = obj.isActive.TooBoolean();
            obj.address = obj.address.TooString();
            obj.regionId = obj.regionId.TooInt();
            obj.dated = DateTime.Now;
            obj.adminId = loggedInUser.userId;

            obj.instituteIds = obj.instituteIds.TooString();

            Message m = new HospitalDAL().AddUpdate(obj);

            return Redirect("/admin/hospital-manage");
        }

        #region Department Association

        [CheckHasRight]
        public ActionResult DepartmentAssociation()
        {
            HospitalModelAdmin model = new HospitalModelAdmin();
            model.hospitalId = Request.QueryString["id"].TooInt();
            model.listHospital = DDLHospital.GetAll(25, "GetTeaching");
            return View(model);
        }

        [HttpGet]
        public ActionResult GetDepartmentForHospital(int hospitalId)
        {

            DataTable dataTable = new CommonDAL().GetDepartmentForHospital(hospitalId);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpGet]
        public JsonResult DepartmentHospitalAddUpdate(int hospitalId, string ids)
        {
            ids = ids.TrimStart(',').TrimEnd(',');
            Message msg = new CommonDAL().DepartmentHospitalAddUpdate(hospitalId, ids, loggedInUser.userId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Unit

        [HttpPost]
        public JsonResult UnitAddUpdate(Unit obj)
        {
            obj.adminId = loggedInUser.userId;
            Message msg = new CommonDAL().UnitAddUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Hospital Discipline


        public ActionResult DisciplineAssociation()
        {
            HospitalDisciplineModelAdmin model = new HospitalDisciplineModelAdmin();

            model.listType = DDLConstant.GetAll(ProjConstant.Constant.bedsApprovalType);

            int id = Request.QueryString["id"].TooInt();
            if (id > 0)
                model.discipline = new HospitalDAL().HospitalDisciplineGetById(id);

            if (loggedInUser.typeId == ProjConstant.Constant.UserType.hospital)
            {
                model.hospitalId = loggedInUser.reffId;
            }
            else
            {
                model.hospitalId = Request.QueryString["hospitalId"].TooInt();
                model.listHospital = DDLHospital.GetAll(25, "GetTeaching");
                if (model.hospitalId == 0)
                    model.hospitalId = model.listHospital.FirstOrDefault().value.TooInt();
            }

            model.listDiscipline = DDLDiscipline.GetAll(model.hospitalId, model.discipline.disciplineId, "HospitalAccess");
            model.list = new HospitalDAL().HospitalDisciplineSearch(model.hospitalId);

            return View(model);
        }

        [HttpPost]
        public JsonResult HospitalDisciplineAddUpdate(HospitalDiscipline obj)
        {
            obj.adminId = loggedInUser.adminId;
            obj.remarks = obj.remarks.TooString();
            obj.dateStart = DateTime.Now;
            obj.dateEnd = DateTime.Now;
            obj.adminId = loggedInUser.userId;

            obj.startDate = obj.startDate.TooString();
            obj.endDate = obj.endDate.TooString();
            if (!String.IsNullOrWhiteSpace(obj.startDate))
                obj.dateStart = obj.startDate.TooDate();

            if (!String.IsNullOrWhiteSpace(obj.endDate))
                obj.dateEnd = obj.endDate.TooDate();

            Message msg = new HospitalDAL().HospitalDisciplineAddUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult HospitalDisciplineDelete(HospitalDiscipline obj)
        {
            Message msg = new HospitalDAL().HospitalDisciplineDelete(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region Beds

        public ActionResult BedsManagement()
        {
            BedModelAdmin model = new BedModelAdmin();
            model.hospitalId = loggedInUser.reffId;

            try
            {
                if (model.hospitalId == 0)
                {
                    model.listHospital = DDLHospital.GetAll(25, "HasEmployee");
                    model.hospitalId = Request.QueryString["hospitalId"].TooInt();
                    if (model.hospitalId == 0)
                        model.hospitalId = model.listHospital.FirstOrDefault().value.TooInt();
                }
            }
            catch (Exception)
            {
            }

            model.departmentId = Request.QueryString["departmentId"].TooInt();
            model.unitId = Request.QueryString["unitId"].TooInt();

            model.listDepartment = DDLDepartment.GetAll(model.hospitalId, "ByHospitalWithBed").OrderBy(x => x.key).ToList();
            model.listDiscipline = DDLDiscipline.GetAll(model.hospitalId, "ForBed").ToList();
            model.listSpeciality = DDLSpeciality.GetAll("GetAll").ToList();

            return View(model);
        }

        [HttpPost]
        public JsonResult BedsAddUpdate(Bed obj)
        {
            obj.inductionId = ProjConstant.inductionId;
            obj.adminId = loggedInUser.userId;
            obj.remarksN = obj.remarksN.TooString();
            obj.remarksDep = obj.remarksDep.TooString();

            obj.imageN = obj.imageN.TooString();
            obj.imageDep = obj.imageDep.TooString();

            Message msg = new CommonDAL().BedAddUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult BedsSearch(Bed obj)
        {
            if (obj.inductionId == 0)
                obj.inductionId = ProjConstant.inductionId;
            DataTable dataTable = new CommonDAL().BedsSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public JsonResult BedDelete(Bed obj)
        {
            Message msg = new CommonDAL().BedDelete(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}