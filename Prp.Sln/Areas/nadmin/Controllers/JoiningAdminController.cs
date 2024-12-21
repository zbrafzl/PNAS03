using Newtonsoft.Json;
using Prp.Data;
using Prp.Data.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class JoiningAdminController : BaseAdminController
    {
        [CheckHasRight]
        public ActionResult ApplicantListFinal()
        {
            ApplicantJoiningAdminModel model = new ApplicantJoiningAdminModel();
            try
            {
                model.listApplicant = new JoiningDAL().GetAllByHospitalUser(loggedInUser.userId);
            }
            catch (Exception)
            {

            }
            return View(model);
        }

        [CheckHasRight]
        public ActionResult ApplicantTakeJoining()
        {
            ApplicantJoiningAdminModel model = new ApplicantJoiningAdminModel();
            int applicantId = Request.QueryString["applicantId"].TooInt();
            if (applicantId > 0)
            {
                ApplicantJoined join = new JoiningDAL().GetByApplicantById(applicantId);


                model.applicant = new ApplicantDAL().GetApplicant(0, applicantId);
                model.applicantInfo = new ApplicantDAL().GetApplicantInfoDetail(0, ProjConstant.phaseId, applicantId);

                if (join != null && join.joiningId > 0)
                {
                    model.isExist = true;
                }
                else
                {
                    model.isExist = false;
                    model.joinApplicant = new JoiningDAL().GetApplicantByInstiteUser(loggedInUser.userId, applicantId);
                }
            }
            return View(model);
        }

        [CheckHasRight]
        public ActionResult ApplicantTakeJoiningMigration()
        {
            ApplicantJoiningAdminModel model = new ApplicantJoiningAdminModel();
            int applicantId = Request.QueryString["applicantId"].TooInt();
            if (applicantId > 0)
            {
                ApplicantJoined join = new JoiningDAL().GetByApplicantById(applicantId);


                model.applicant = new ApplicantDAL().GetApplicant(0, applicantId);
                model.applicantInfo = new ApplicantDAL().GetApplicantInfoDetail(0, ProjConstant.phaseId, applicantId);

                if (join != null && join.joiningId > 0)
                {
                    model.isExist = true;
                }
                else
                {
                    model.isExist = false;
                    model.joinApplicant = new JoiningDAL().GetApplicantByInstiteUser(loggedInUser.userId, applicantId);
                }
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult GetAllByInstiteUser(string search)
        {
            JoiningApplicantSearch obj = new JoiningApplicantSearch();
            obj.inductionId = ProjConstant.inductionId;
            obj.instituteId = 0;
            obj.userId = loggedInUser.userId;
            obj.search = search.TooString();

            DataTable dataTable = new JoiningDAL().JoiningSearchGetByInstitute(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");


        }


        [HttpPost]
        public JsonResult ApplicantJoiningAddUpdate(ApplicantJoined objJoin)
        {
            Message msg = new Message();

            objJoin.adminId = loggedInUser.userId;

            try
            {
                objJoin.joiningDate = objJoin.dateJoining.TooDate();
                objJoin.jobs = loggedInUser.userId;
                msg = new JoiningDAL().AddUpdate(objJoin);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;

            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ApplicantMigrationJoiningAddUpdate(ApplicantJoined objJoin)
        {
            Message msg = new Message();

            objJoin.adminId = loggedInUser.userId;

            try
            {
                objJoin.joiningDate = objJoin.dateJoining.TooDate();
                objJoin.jobs = loggedInUser.userId;
                msg = new JoiningDAL().AddUpdateMigration(objJoin);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;

            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ApplicantReleavingAddUpdate(ApplicantJoined objJoin)
        {
            Message msg = new Message();

            objJoin.adminId = loggedInUser.userId;

            try
            {
                objJoin.joiningDate = objJoin.dateJoining.TooDate();
                objJoin.jobs = loggedInUser.userId;
                msg = new JoiningDAL().AddUpdateReleaving(objJoin);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;

            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ApplicantJoiningListHospInst()
        {
            ApplicantJoiningAdminModel model = new ApplicantJoiningAdminModel();
            model.instituteId = Request.QueryString["instituteId"].TooInt();
            model.hospitalId = Request.QueryString["hospitalId"].TooInt();
            if (model.instituteId > 0)
            {
                model.listApplicant = new JoiningDAL().GetAllByInstiteUser(loggedInUser.userId, model.instituteId, "");
            }
            else if (model.hospitalId > 0)
            {
                model.listApplicant = new JoiningDAL().GetAllByHospitalUser(loggedInUser.userId, model.hospitalId);
            }
            return View(model);
        }


        [CheckHasRight]
        public ActionResult ApplicantJoiningFullList()
        {
            ApplicantJoiningAdminModel model = new ApplicantJoiningAdminModel();
            return View(model);
        }

        [HttpGet]
        public JsonResult GetJoiningAll(int top, int pageNo, int joinStatus, string search)
        {
            List<ApplicantJoined> list = new JoiningDAL().GetJoiningAll(top, pageNo, joinStatus, search);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        //[CheckHasRight]
        public ActionResult ApplicantJoined()
        {
            ApplicantJoiningAdminModel model = new ApplicantJoiningAdminModel();

            DDLConstants ddlConstant = new DDLConstants();
            ddlConstant.typeId = ProjConstant.Constant.degreeType;
            ddlConstant.condition = "ByType";
            model.listType = new ConstantDAL().GetConstantDDL(ddlConstant);
            return View(model);
        }

        [HttpPost]
        public ActionResult JoiningSearch(JoiningApplicantSearch obj)
        {
            if (obj.inductionId.TooInt() == 0)
                obj.inductionId = ProjConstant.inductionId;
            obj.typeId = obj.typeId.TooInt();
            obj.search = obj.search.TooString();

            DataTable dataTable = new JoiningDAL().JoiningSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");


        }

        [HttpPost]
        public ActionResult ExportDataToExcelAndDownload(JoiningApplicantSearch obj)
        {
            Message msg = new Message();

            if (obj.inductionId.TooInt() == 0)
                obj.inductionId = ProjConstant.inductionId;
            obj.typeId = obj.typeId.TooInt();
            obj.search = obj.search.TooString();



            string fileName = "JoiningList" + obj.inductionId + ".xlsx";
            try
            {
               
                string filePath = fileName.GenerateFilePath(loggedInUser);
                if (!String.IsNullOrWhiteSpace(filePath))
                {
                    DataTable dt = new JoiningDAL().JoiningSearch(obj);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        msg = filePath.ExcelFileWrite(dt);
                        msg.status = true;
                        //filePath.FileDownload();
                        msg.msg = fileName;
                    }
                    else
                    {
                        msg.status = false;
                        msg.msg = "Some thing went wrong!";
                    }
                }
                else
                {
                    msg.status = false;
                    msg.msg = "Error : File path and name creating.";
                }
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = "Error in exported : " + ex.Message;
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        //[CheckHasRight]
        public ActionResult ApplicantTransfer()
        {
            ApplicantJoiningAdminModel model = new ApplicantJoiningAdminModel();

            DDLConstants ddlConstant = new DDLConstants();
            ddlConstant.typeId = ProjConstant.Constant.degreeType;
            ddlConstant.condition = "ByType";
            model.listType = new ConstantDAL().GetConstantDDL(ddlConstant);
            return View(model);
        }
    }
}