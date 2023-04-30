﻿using Newtonsoft.Json;
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
    public class ApplicantAdminController : BaseAdminController
    {
        // GET: nadmin/ApplicantAdmin
        public ActionResult ApplicantAccountStatus()
        {


            ApplicantStatusModel model = new ApplicantStatusModel();
            model.search.accountStatusId = Request.QueryString["statusId"].TooInt();
            model.search.condition = "GetByAccountStatusId";
            if (model.search.accountStatusId == -1)
                model.search.condition = "GetByAccountStatus";

            model.listApplicant = new ApplicantDAL().GetApplicantList(model.search);

            DDLConstants ddlConstant = new DDLConstants();
            ddlConstant.typeId = ProjConstant.Constant.statusApplicantAccount;
            ddlConstant.condition = "ByType";
            model.listStatus = new ConstantDAL().GetConstantDDL(ddlConstant);

            return View(model);
        }

        public ActionResult ApplicantStatus()
        {
            ApplicantStatusModel model = new ApplicantStatusModel();
            int applicantId = Request.QueryString["applicantId"].TooInt();
            model.applicantStatus = new ApplicantAdminDAL().ApplicantStatusHistory(applicantId);
            return View(model);
        }

        [CheckHasRight]
        public ActionResult ApplicantSearch()
        {

            ApplicantStatusModel model = new ApplicantStatusModel();

            model.inductionId = 12;
            model.phaseId = 1;
            model.statusTypeId = Request.QueryString["statusTypeId"].TooInt();
            model.statusId = Request.QueryString["statusId"].TooInt();
            Session["SelectedStatusID"] = model.statusId;
            if (model.statusTypeId == 0)
                model.statusTypeId = 52;

            DDLConstants ddlConstant = new DDLConstants();
            ddlConstant.typeId = model.statusTypeId;
            ddlConstant.condition = "ByType";
            model.listStatus = new ConstantDAL().GetConstantDDL(ddlConstant);
            model.listApplicant = getListApplicant(model.statusTypeId, model.statusId);

            return View(model);
        }

        public ActionResult ApplicantSearchToVeify()
        {

            ApplicantStatusModel model = new ApplicantStatusModel();

            model.inductionId = 12;
            model.phaseId = 1;
            model.statusTypeId = Request.QueryString["statusTypeId"].TooInt();
            model.statusId = Request.QueryString["statusId"].TooInt();
            Session["SelectedStatusID"] = model.statusId;
            if (model.statusTypeId == 0)
                model.statusTypeId = 52;

            DDLConstants ddlConstant = new DDLConstants();
            ddlConstant.typeId = model.statusTypeId;
            ddlConstant.condition = "ByType";
            model.listStatus = new ConstantDAL().GetConstantDDL(ddlConstant);
            model.listApplicant = getListApplicant(model.statusTypeId, model.statusId);

            return View(model);
        }

        public ActionResult ApplicantSearchVerify()
        {
            ApplicantStatusModel model = new ApplicantStatusModel();

            model.inductionId = 12;
            model.phaseId = 1;
            model.statusTypeId = Request.QueryString["statusTypeId"].TooInt();
            model.statusId = Request.QueryString["statusId"].TooInt();
            Session["SelectedStatusID"] = model.statusId;
            if (model.statusTypeId == 0)
                model.statusTypeId = 52;

            DDLConstants ddlConstant = new DDLConstants();
            ddlConstant.typeId = model.statusTypeId;
            ddlConstant.condition = "ByType";
            model.listStatus = new ConstantDAL().GetConstantDDL(ddlConstant);
            model.listApplicant = getListApplicant(model.statusTypeId, model.statusId);

            return View(model);
        }

        public ActionResult ApplicantSearchJoin()
        {
            ApplicantStatusModel model = new ApplicantStatusModel();

            model.inductionId = 12;
            model.phaseId = 1;
            model.statusTypeId = Request.QueryString["statusTypeId"].TooInt();
            model.statusId = Request.QueryString["statusId"].TooInt();
            Session["SelectedStatusID"] = model.statusId;
            if (model.statusTypeId == 0)
                model.statusTypeId = 52;

            DDLConstants ddlConstant = new DDLConstants();
            ddlConstant.typeId = model.statusTypeId;
            ddlConstant.condition = "ByType";
            model.listStatus = new ConstantDAL().GetConstantDDL(ddlConstant);
            model.listApplicant = getListApplicant(model.statusTypeId, model.statusId);

            return View(model);
        }

        public List<Applicant> getListApplicant(int statusTypeId, int statusId)
        {
            List<Applicant> list = new List<Applicant>();
            Applicant app = new Applicant();
            app.inductionId = 12;
            app.phaseId = 1;
            app.applicantId = 121432;
            app.name = "ZBC";
            app.pmdcNo = "121";
            app.pncNo = "1sdf";
            app.emailId = "adfa";
            app.password = "asdsaf";
            app.passwordDecrypt = "adfasd";
            app.contactNumber = "dafa";
            app.maritalStatus = "married";
            app.network = 1;
            app.levelId = 11;
            app.facultyId = 11;
            app.pic = "pic.png";
            app.date = DateTime.Now.ToString();
            app.dated = DateTime.Now;
            app.levelName = "NURSING";
            app.facultyName = "BSN";
            app.accountStatus = "Active";
            app.accountStatusId = 1;
            app.isValid = 1;
            app.pathProfilePic = "D:\\Project\\PRP\\Prp.Sln\\assets\\img\\avatar1.jpg";
            app.adminId = 1;
            app.status = "Active";
            app.genderID = 1;
            list.Add(app);
            return list;
        }

      
        [HttpPost]
        public ActionResult ApplicantSearchSimple(ApplicantSearch obj)
        {
            DataTable dataTable = new ApplicantAdminDAL().ApplicantSearchSimple(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult ApplicantSearchSimpleToVerify(ApplicantSearch obj)
        {
            obj.userId = loggedInUser.userId;
            DataTable dataTable = new ApplicantAdminDAL().ApplicantSearchSimpleToVerify(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult ApplicantSearchSimpleVerify(ApplicantSearch obj)
        {
            DataTable dataTable = new ApplicantAdminDAL().ApplicantSearchSimpleVerify(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult ApplicantSearchSimpleJoin(ApplicantSearch obj)
        {
            DataTable dataTable = new ApplicantAdminDAL().ApplicantSearchSimpleJoin(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
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

        public ActionResult ApplicantApplicationStatus()
        {
            ApplicantStatusModel model = new ApplicantStatusModel();
            model.search.applicationStatusId = Request.QueryString["applicationStatusId"].TooInt();
            model.search.condition = "GetByAccountApplicationStatusId";
            if (model.search.applicationStatusId == 0)
                model.search.condition = "GetByAccountApplicationStatus";
            model.listApplicant = new ApplicantDAL().GetApplicantList(model.search);

            DDLConstants ddlConstant = new DDLConstants();
            ddlConstant.typeId = ProjConstant.Constant.statusApplicantApplication;
            ddlConstant.condition = "ByType";
            model.listStatus = new ConstantDAL().GetConstantDDL(ddlConstant);

            return View(model);
        }

        [CheckHasRight]
        public ActionResult ApplicantActionStatus()
        {
            ApplicantCurrentStatusModelAdmin model = new ApplicantCurrentStatusModelAdmin();
            model.applicantId = Request.QueryString["applicantId"].TooInt();
            model.listHospitalFrom = DDLHospital.GetAll("GetTeaching");
            model.listHospitalTo = DDLHospital.GetAll("GetTeaching");
            model.listStatus = DDLConstant.GetAll(ProjConstant.Constant.currentStatus);
            return View(model);
        }

        [HttpPost]
        public JsonResult TraineeInfoStatusUpdate(TraineeInfo obj)
        {

            obj.adminId = loggedInUser.userId;
            Message msg = new TraineeDAL().TraineeInfoStatusUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);

        }

        [CheckHasRight]
        public ActionResult ApplicantAddition()
        {
            ProfileModelAdmin model = new ProfileModelAdmin();
            model.hospitalId = loggedInUser.reffId;
            model.listInduction = DDLInduction.GetAll(loggedInUser.userId, "GetByUser");
            model.listHospital = DDLHospital.GetAll(loggedInUser.userId, "GetTeachingAndByUserType");
            model.listProgram = DDLConstant.GetAll("GetRelvantProgram");
            model.applicantId = Request.QueryString["applicantId"].TooInt();
            model.listCountry = DDLRegion.GetAll(ProjConstant.Constant.Region.country, "ByType");
            model.listProvince = DDLRegion.GetAll("GetPakistanProvince");
            model.listDistrict = DDLRegion.GetAll("GetPakistanDistrict");

            model.applicantId = Request.QueryString["applicantId"].TooInt();
            if (model.applicantId > 0)
            {
                //model.applicant = new ApplicantDAL().GetApplicant(0, model.applicantId);
                // model.applicantInfo = new ApplicantDAL().GetApplicantInfoDetail(0, 0, model.applicantId);
            }
            return View(model);
        }


        


        [HttpPost]
        public JsonResult ApplicantAddUpdate(ProofReadingAdminModel obj)
        {
            Message msg = new Message();
            msg.status = true;
            msg.message = "Data saved successfully";
            int applicantId = 0;

            try
            {
                Applicant objApp = obj.applicant;
                objApp.adminId = loggedInUser.userId;

                if (objApp.applicantId == 0)
                {
                    Message msgg = new ApplicantDAL().Registration(objApp);
                    applicantId = msgg.id;
                }

                else
                {
                    Message msgg = new ApplicantDAL().ApplicantUpdateByAdmin(objApp);
                    applicantId = objApp.applicantId;
                }

            }
            catch (Exception ex)
            {
                msg.id = 0;
                msg.status = false;
                msg.message = ex.Message;
            }

            if (applicantId > 0)
            {
                //try
                //{
                //    ApplicantInfo objInfo = obj.applicantInfo;
                //    objInfo.applicantId = applicantId;
                //    new ApplicantDAL().ApplicantInfoAddUpdate(objInfo);
                //}
                //catch (Exception ex)
                //{

                //    msg.status = false;
                //    msg.message = msg.message + " - " + ex.Message;
                //}

                try
                {
                    TraineeInfo trainee = obj.trainee;
                    trainee.adminId = loggedInUser.userId;
                    trainee.applicantId = applicantId;
                    try
                    {
                        trainee.joiningDate = trainee.dateJoining.TooDate();
                    }
                    catch (Exception)
                    {
                        trainee.joiningDate = DateTime.Now;
                    }


                    new TraineeDAL().TraineeAddUpdate(trainee);
                }
                catch (Exception ex)
                {

                    msg.status = false;
                    msg.message = msg.message + " - " + ex.Message;
                }

            }
            else
            {
                msg.id = 0;
                msg.status = false;
                msg.message = msg.message + " Error! While added applicant data";
            }

            return Json(msg, JsonRequestBehavior.AllowGet);

        }

        //

        


        [HttpGet]
        public ActionResult GetApplicantByIdAdmin(int applicantId)
        {
            DataTable dataTable = new ApplicantDAL().GetApplicantByIdAdmin(applicantId);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpGet]
        public ActionResult GetApplicantById(int applicantId)
        {
            DataTable dataTable = new ApplicantDAL().GetApplicantById(applicantId);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpGet]
        public ActionResult TraineeGetById(int applicantId)
        {
            DataTable dataTable = new TraineeDAL().TraineeGetById(applicantId);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }


        [HttpPost]
        public ActionResult ApplicantSearchByAdmin(ApplicantSearch obj)
        {
            DataTable dataTable = new ApplicantAdminDAL().ApplicantSearchByAdmin(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

    }
}