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
    public class GrievanceAdminController : BaseAdminController
    {
        [CheckHasRight]
        public ActionResult GrievanceManage()
        {
            GrievanceAdminModel model = new GrievanceAdminModel();
            return View(model);
        }

        [CheckHasRight]
        public ActionResult GrievanceAttendence()
        {
            GrievanceAdminModel model = new GrievanceAdminModel();
            model.searchTxt = Request.QueryString["search"].TooString();
            return View(model);
        }


        [HttpPost]
        public ActionResult GrievanceSearch(Grievance obj)
        {
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            DataTable dataTable = new GrievanceDAL().GrievanceSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult GrievanceSearchAttendence(Grievance obj)
        {
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            DataTable dataTable = new GrievanceDAL().GrievanceSearchAttendence(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }


        [HttpPost]
        public ActionResult GrievanceSearchPrint(Grievance obj)
        {
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            obj.dated = obj.datedStr.TooDate();
            DataTable dataTable = new GrievanceDAL().GrievancePrint(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }


        [CheckHasRight]
        public ActionResult GrievancePrint()
        {
            GrievanceAdminModel model = new GrievanceAdminModel();

            DDLConstants dDLConstant = new DDLConstants();
            dDLConstant.condition = "";
            dDLConstant.typeId = ProjConstant.Constant.grievanceType;
            model.listType = new ConstantDAL().GetConstantDDL(dDLConstant);

            return View(model);
        }

        public ActionResult GrievanceAttendenceMark()
        {
            GrievanceActionModel model = new GrievanceActionModel();

            int grievanceId = Request.QueryString["id"].TooInt();

            if (grievanceId > 0)
            {
                model.grievance = new GrievanceDAL().GetById(grievanceId);
                model.action = new GrievanceDAL().ActionGetById(grievanceId);
                model.applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, model.grievance.applicantId);
            }

            DDLConstants dDLConstant = new DDLConstants();
            dDLConstant.condition = "";
            dDLConstant.typeId = ProjConstant.Constant.guardianType;
            model.listRelation = new ConstantDAL().GetConstantDDL(dDLConstant);

          
            return View(model);
        }

        public ActionResult GrievanceAttendenceRemarks()
        {
            GrievanceRemarkModel model = new GrievanceRemarkModel();

            int grievanceId = Request.QueryString["id"].TooInt();

            if (grievanceId > 0)
            {
                model.action = new GrievanceDAL().ActionGetById(grievanceId);
                model.grievance = new GrievanceDAL().GetById(grievanceId);
                model.remark = new GrievanceDAL().RemarksGetById(grievanceId);
                model.applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, model.grievance.applicantId);
            }

            DDLConstants dDLConstant = new DDLConstants();
            dDLConstant.condition = "";
            dDLConstant.typeId = ProjConstant.Constant.guardianType;
            model.listRelation = new ConstantDAL().GetConstantDDL(dDLConstant);

            dDLConstant = new DDLConstants();
            dDLConstant.condition = "";
            dDLConstant.typeId = ProjConstant.Constant.grievanceStatusType;
            model.listType = new ConstantDAL().GetConstantDDL(dDLConstant);
            return View(model);
        }




        public ActionResult GrievanceGazetteMarksDetail()
        {
            GrievanceAdminModel model = new GrievanceAdminModel();

            DDLConstants objConst = new DDLConstants();
            objConst.typeId = ProjConstant.Constant.appearanceType;
            model.listAppearanceType = new ConstantDAL().GetConstantDDL(objConst);

            objConst = new DDLConstants();
            objConst.typeId = ProjConstant.Constant.guardianType;
            model.listGuardian = new ConstantDAL().GetConstantDDL(objConst);

            objConst = new DDLConstants();
            objConst.typeId = ProjConstant.Constant.grievanceActionType;
            model.listActionType = new ConstantDAL().GetConstantDDL(objConst);

            model.grievanceId = Request.QueryString["grievanceId"].TooInt();
            model.appearanceId = Request.QueryString["appearanceId"].TooInt();


            try
            {
                model.grievance = new GrievanceDAL().GetById(model.grievanceId);
                model.applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, model.grievance.applicantId);
                //model.listGrievance = new GrievanceDAL().GetByGrievanceId(model.grievanceId);
                //model.appearance = new GrievanceDAL().GetByAppearanceId(model.appearanceId);

            }
            catch (Exception)
            {
                model = new GrievanceAdminModel();
            }
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult SaveGrievanceAppearanceData(GrievanceAdminModel ModelSave, HttpPostedFileBase files)
        {
            Grievance obj = ModelSave.appearance;
            obj.grievanceId = obj.grievanceId.TooInt();
            obj.attendanceNo = obj.attendanceNo.TooInt();
            obj.appearanceTypeId = obj.appearanceTypeId.TooInt();
            obj.guardianId = obj.guardianId.TooInt();
            obj.guardianName = obj.guardianName.TooString();
            obj.guardianContactNumber = obj.guardianContactNumber.TooString();
            obj.actionTypeId = obj.actionTypeId.TooInt();
            obj.actionComments = obj.actionComments.TooString();
            obj.datedAppearance = obj.appearanceDate.TooDate('/');
            obj.adminIdAppearance = loggedInUser.userId;

            obj.adminIdStatus = 0;
            obj.statusId = 0;
            obj.datedStatus = DateTime.Now;

            //Message m = new GrievanceDAL().GrievanceAppearanceAddUpdate(obj);
            return Redirect("/admin/grievance-marks-detail?grievanceId=" + obj.grievanceId);
        }


        [HttpPost]
        public JsonResult SaveGrievanceAttendence(GrievanceAction obj)
        {
            Message msg = new Message();
            try
            {
                obj.grievanceId = obj.grievanceId.TooInt();
                obj.grievanceActionId = obj.grievanceActionId.TooInt();
                obj.isSelf = obj.isSelf.TooInt();
                obj.relationId = obj.relationId.TooInt();
                obj.adminIdAttendece = loggedInUser.userId;
                msg = new GrievanceDAL().ActionAddUpdate(obj);
            }
            catch (Exception)
            {

                msg.status = false;
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveGrievanceRemarks(GrievanceRemark obj)
        {
            Message msg = new Message();
            try
            {
                obj.grievanceId = obj.grievanceId.TooInt();
                obj.grievanceRemarksId = obj.grievanceRemarksId.TooInt();
                obj.typeId = obj.typeId.TooInt();
                obj.remarks = obj.remarks.TooString();
                obj.title = obj.title.TooString();
                obj.adminId = loggedInUser.userId;
                msg = new GrievanceDAL().RemarksAddUpdate(obj);
            }
            catch (Exception)
            {

                msg.status = false;
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        public ActionResult SaveGrievanceAttendenceData(GrievanceActionModel ModelSave, HttpPostedFileBase files)
        {
            GrievanceAction obj = ModelSave.action;
            obj.grievanceId = obj.grievanceId.TooInt();
            obj.grievanceActionId = obj.grievanceActionId.TooInt();
            obj.isSelf = obj.isSelf.TooInt();
            obj.relationId = obj.relationId.TooInt();
            obj.adminIdAttendece = loggedInUser.userId;
            Message m = new GrievanceDAL().ActionAddUpdate(obj);
            return Redirect("/admin/grievance-attendence?search=" + ModelSave.applicant.applicantId);
        }

        //
    }
}