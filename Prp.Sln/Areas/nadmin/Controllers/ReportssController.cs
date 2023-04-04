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
    public class ReportssController : BaseAdminController
    {
        // GET: nadmin/Reportss
        public ActionResult Index()
        {
            return View();
        }

        [CheckHasRight]
        public ActionResult JoinedReport()
        {
            ReportApplicantModel model = new ReportApplicantModel();
            return View(model);
        }

        [CheckHasRight]
        public ActionResult JoinedApplicantHospital()
        {
            ReportApplicantModel model = new ReportApplicantModel();
            model.listHospital = DDLHospital.GetAll("GetTeaching");
            model.listAttachStatus = DDLConstant.GetAll(901);

            model.hospitalId = 0;
            if (loggedInUser.typeId == ProjConstant.Constant.UserType.hospital)
                model.hospitalId = loggedInUser.reffId;
            return View(model);
        }

        [CheckHasRight]
        public ActionResult JoinedApplicantHospitalStatus()
        {
            ReportApplicantModel model = new ReportApplicantModel();
            model.listHospital = DDLHospital.GetAll("GetTeaching");
            model.listAttachStatus = DDLConstant.GetAll(901);

            model.hospitalId = 0;
            if (loggedInUser.typeId == ProjConstant.Constant.UserType.hospital)
                model.hospitalId = loggedInUser.reffId;


            return View(model);
        }

        //


        [HttpPost]
        public ActionResult JoiningApplicantByHospitalSearch(SpecialityJobSearch obj)
        {
            obj.phaseId = ProjConstant.phaseId;

            if (loggedInUser.reffId > 0)
                obj.hospitalId = loggedInUser.reffId;

            obj.userId = loggedInUser.userId;

            DataTable dataTable = new ReportDAL().JoiningApplicantByHospitalSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }


        [HttpPost]
        public ActionResult JoinedApplicantByHospitalSearch(SpecialityJobSearch obj)
        {
            obj.phaseId = ProjConstant.phaseId;

            if (loggedInUser.reffId > 0)
                obj.hospitalId = loggedInUser.reffId;

            obj.userId = loggedInUser.userId;

            DataTable dataTable = new ReportDAL().JoinedApplicantByHospitalSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }


        #region Trainee

        [CheckHasRight]
        public ActionResult TraineeAttachReportHospitalWise()
        {
            ReportApplicantModel model = new ReportApplicantModel();
            model.listHospital = DDLHospital.GetAll("HasTrainee");
            model.hospitalId = 0;
            if (loggedInUser.typeId == ProjConstant.Constant.UserType.hospital)
                model.hospitalId = loggedInUser.reffId;
            return View(model);
        }

        [HttpPost]
        public ActionResult HospitalTraineeCountReport(SpecialityJobSearch obj)
        {
            obj.search = obj.search.TooString();

            DataTable dataTable = new ReportDAL().HospitalTraineeCountReport(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        #endregion

        [CheckHasRight]
        public ActionResult SeatsReport()
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
        public ActionResult RptSeatsStatus(SpecialityJobSearch obj)
        {
            obj.phaseId = ProjConstant.phaseId;
            DataTable dataTable = new ReportDAL().RptSeatsStatus(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }


        [HttpPost]
        public ActionResult RptSeatsStatusExport(SpecialityJobSearch obj)
        {
            Message msg = new Message();

            if (obj.inductionId.TooInt() == 0)
                obj.inductionId = ProjConstant.inductionId;
            obj.typeId = obj.typeId.TooInt();
            obj.search = obj.search.TooString();



            string fileName = "SeatsStatus" + obj.inductionId + ".xlsx";
            try
            {

                string filePath = fileName.GenerateFilePath(loggedInUser);
                if (!String.IsNullOrWhiteSpace(filePath))
                {
                    DataTable dt = new ReportDAL().RptSeatsStatus(obj);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        msg = filePath.ExcelFileWrite(dt);
                        msg.status = true;
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


        #region MyRegion


        #endregion
    }
}