using Newtonsoft.Json;
using Prp.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class CommonAdminController : BaseAdminController
    {
        #region Constatnt
        [CheckHasRight]
        public ActionResult ConstantManage()
        {
            ConstantModelAdmin model = new ConstantModelAdmin();
            model.typeId = Request.QueryString["typeId"].TooInt();
            model.listType = new ConstantDAL().GetAll(0).OrderBy(x => x.id).ToList();
            model.list = new ConstantDAL().GetAll(model.typeId).OrderBy(x => x.id).ToList();
            return View(model);
        }

        [CheckHasRight]
        public ActionResult ConstantSetup()
        {
            ConstantModelAdmin model = new ConstantModelAdmin();
            int constantId = Request.QueryString["id"].TooInt();
            if (constantId > 0)
                model.constant = new ConstantDAL().GetById(constantId);

            model.listType = new ConstantDAL().GetAll(0);

            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult SaveConstantData(ConstantModelAdmin ModelSave, HttpPostedFileBase files)
        {
            Constant obj = ModelSave.constant;

            obj.id = obj.id.TooInt();
            obj.typeId = obj.typeId.TooInt();
            obj.parentId = obj.parentId.TooInt();
            obj.detail = obj.name.TooString();
            obj.shortDesc = obj.shortDesc.TooString();
            if (String.IsNullOrWhiteSpace(obj.shortDesc))
                obj.shortDesc = obj.name.TooString();
            obj.nameDisplay = obj.name.TooString();
            obj.name = obj.name.TooString();
            obj.code = obj.code.TooString();
            obj.value = obj.value.TooInt();
            obj.isActive = obj.isActive.TooBoolean();
            obj.isDeleted = obj.isDeleted.TooBoolean();
            obj.dated = DateTime.Now;
            obj.adminId = loggedInUser.userId;


            Message m = new ConstantDAL().AddUpdate(obj);

            return Redirect("/admin/constant-manage?typeId=" + obj.typeId);
        }

        #endregion

        [CheckHasRight]
        public ActionResult DisciplineManage()
        {
            DisciplineModelAdmin model = new DisciplineModelAdmin();
            model.disciplineId = Request.QueryString["disciplineId"].TooInt();
            model.list = new CommonDAL().DisciplineGetAll();
            //model.listParent = new ConstantDAL().GetAll(0).OrderBy(x => x.id).ToList();
            //model.listCategory = new ConstantDAL().GetAll(model.typeId).OrderBy(x => x.id).ToList();
            return View(model);
        }

        #region SMS

        [CheckHasRight]
        public ActionResult SMSManage()
        {
            SMSModelAdmin model = new SMSModelAdmin();
            //model.typeId = Request.QueryString["typeId"].TooInt();
            model.inductionId = ProjConstant.inductionId;
            model.list = new SMSDAL().GetAll(model.inductionId).ToList();

            //model.listType = new ConstantDAL().GetAll(model.typeId).OrderBy(x => x.id).ToList();
            return View(model);
        }

        [CheckHasRight]
        public ActionResult SMSSetup()
        {
            SMSModelAdmin model = new SMSModelAdmin();
            int smsId = Request.QueryString["id"].TooInt();
            if (smsId > 0)
                model.sms = new SMSDAL().GetById(smsId);

            model.listType = new ConstantDAL().GetAll(ProjConstant.Constant.smsType).OrderBy(x => x.id).ToList();
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult SaveSMSData(SMSModelAdmin ModelSave, HttpPostedFileBase files)
        {
            SMS obj = ModelSave.sms;

            obj.smsId = obj.smsId.TooInt();
            obj.typeId = obj.typeId.TooInt();
            obj.inductionId = ProjConstant.inductionId;
            obj.detail = obj.detail.TooString();
            obj.preDetail = obj.preDetail.TooString();
            obj.postDetail = obj.postDetail.TooString();
            obj.name = obj.name.TooString();
            obj.isActive = obj.isActive.TooBoolean();
            obj.dated = DateTime.Now;
            obj.adminId = loggedInUser.userId;
            Message m = new SMSDAL().AddUpdate(obj);
            return Redirect("/admin/sms-manage?inductionId=" + obj.inductionId);
        }
        #endregion

        [CheckHasRight]
        public ActionResult ResearchJournalManage()
        {
            ResearchJournalModel model = new ResearchJournalModel();
            model.listType = new ConstantDAL().GetAll(ProjConstant.Constant.journalDispline).OrderBy(x => x.id).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult ResearchJournalSearch(ResearchJournalSearch obj)
        {
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            DataTable dataTable = new MasterSetupDAL().ResearchJournalSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpGet]
        public JsonResult GetApplicantTentativeMarks(int applicantId, double fsc, double gn, double mw, double bsn, double extraQ, double minExp, double exp, double entryTest)
        {
            TentativeMarks tm = new TentativeMarks();
            tm.applicantId = applicantId;
            tm.fscTotalWeightage = fsc;
            tm.gnTotalWeightage = gn;
            tm.mwTotalWeightage = mw;
            tm.bsnTotalWeightage = bsn;
            tm.expTotalWeightage = extraQ;
            tm.minExpRequired = minExp;
            tm.expTotalWeightage = exp;
            tm.entryTestTotalWeightage = entryTest;
            tm.entryTestObtained = 0;
            string qualification = "(";
            if (fsc != 0)
            {
                qualification += "2,9,";
            }
            if (gn != 0)
            {
                qualification += "3,";
            }
            if (mw != 0)
            {
                qualification += "4,";
            }
            if (bsn != 0)
            {
                qualification += "5,";
            }
            if (extraQ != 0)
            {
                qualification += "6,7,";
            }
            qualification = qualification.Remove(qualification.Length - 1);
            qualification += ")";
            string marksQuery = "select degreeTypeID, obtainedMarks,totalMarks, obtainedMarks/totalMarks as percentageMarks from tblNursingApplicantDegreeData where applicantID = " + applicantId + " and degreeTypeID in " + qualification + "";
            double gnAndMwObtained = 0;
            double gnAndMwTotal = 0;
            SqlCommand cmd = new SqlCommand(marksQuery);
            SqlConnection con = new SqlConnection(PrpDbConnectADO.Conn);
            con.Open();
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToInt32(dr[0]) == 2 || Convert.ToInt32(dr[0]) == 9)
                        {
                            tm.fscObtained = Math.Round(Convert.ToDouble(dr[3]) * fsc, 2);
                        }
                        else if (Convert.ToInt32(dr[0]) == 3)
                        {
                            gnAndMwObtained += Convert.ToDouble(dr[1]);
                            gnAndMwTotal += Convert.ToDouble(dr[2]);
                            //tm.gnObtained = Math.Round(Convert.ToDouble(dr[3]) * gn, 2);
                            tm.gnObtained = 0;
                        }
                        else if (Convert.ToInt32(dr[0]) == 4)
                        {
                            gnAndMwObtained += Convert.ToDouble(dr[1]);
                            gnAndMwTotal += Convert.ToDouble(dr[2]);
                            tm.mwObtained = Math.Round((Convert.ToDouble(gnAndMwObtained / gnAndMwTotal) * (gn + mw)), 2);
                        }
                        else if (Convert.ToInt32(dr[0]) == 5)
                        {
                            tm.bsnObtained = Math.Round(Convert.ToDouble(dr[3]) * bsn, 2);
                        }
                        else if (Convert.ToInt32(dr[0]) == 6 || Convert.ToInt32(dr[0]) == 7)
                        {
                            tm.extraObtained = Math.Round(Convert.ToDouble(dr[3]) * extraQ, 2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            if (exp != 0)
            {
                string expQuery = "select noOfDays from tblApplicantExperience where inductionId = 12 AND applicantId = " + applicantId + "";
                SqlCommand cmdExp = new SqlCommand(expQuery);
                cmdExp.Connection = con;
                SqlDataAdapter sdaExp = new SqlDataAdapter(cmdExp);
                DataTable dtExp = new DataTable();
                double totalExperienceYears = 0;
                try
                {
                    sdaExp.Fill(dtExp);
                    if (dtExp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtExp.Rows)
                        {
                            totalExperienceYears += Convert.ToDouble(dr[0]) / 365;
                        }
                    }
                    totalExperienceYears = totalExperienceYears - minExp;
                    if (totalExperienceYears < 0)
                    {
                        totalExperienceYears = 0;
                    }
                    tm.minExpRequired = minExp;
                    tm.expTotalWeightage = exp;
                    if (totalExperienceYears > exp)
                        totalExperienceYears = exp;
                    tm.expObtained = Math.Round(totalExperienceYears, 2);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

            }
            return Json(tm, JsonRequestBehavior.AllowGet);
        }


        #region Count


        [HttpPost]
        public JsonResult GetCountJobs(CountJobsEntity obj)
        {
            List<EntityCount> list = new CommonDAL().GetCountJobs(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }






        #endregion

        #region Applicant

        //LoginByPhfDeveloper

        [HttpGet]
        public JsonResult LoginByPhfDeveloper(string emailId)
        {
            Message msg = new Message();
            try
            {
                Applicant app = new ApplicantDAL().LoginByPhfDeveloper(emailId, loggedInUser.typeId);
                if (app != null && app.applicantId > 0)
                {
                    app.adminId = loggedInUser.userId;
                    ProjFunctions.RemoveCookies(ProjConstant.Cookies.loggedInApplicant);
                    ProjFunctions.CookieApplicantSet(app);
                    msg.id = app.applicantId;
                    msg.status = true;
                }
                else
                {
                    msg.id = 0;
                    msg.status = false;
                }
            }
            catch (Exception ex)
            {
                msg.id = 0;
                msg.status = false;
                msg.msg = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LoginByPhfDeveloperByApplicantId(int applicantId)
        {
            Message msg = new Message();
            try
            {
                Applicant app = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, applicantId);
                if (app != null && app.applicantId > 0)
                {
                    ProjFunctions.RemoveCookies(ProjConstant.Cookies.loggedInApplicant);
                    ProjFunctions.CookieApplicantSet(app);
                    msg.id = app.applicantId;
                    msg.status = true;
                }
                else
                {
                    msg.id = 0;
                    msg.status = false;
                }
            }
            catch (Exception ex)
            {
                msg.id = 0;
                msg.status = false;
                msg.msg = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult LoginByPhfByApplicantId(int applicantId)
        {
            Message msg = new Message();
            try
            {
                Applicant app = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, applicantId);
                if (app != null && app.applicantId > 0)
                {
                    app.adminId = loggedInUser.userId;
                    ProjFunctions.RemoveCookies(ProjConstant.Cookies.loggedInApplicant);
                    ProjFunctions.CookieApplicantSet(app);
                    msg.id = app.applicantId;
                    msg.status = true;
                }
                else
                {
                    msg.id = 0;
                    msg.status = false;
                }
            }
            catch (Exception ex)
            {
                msg.id = 0;
                msg.status = false;
                msg.msg = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ApplicantStatusUpdate(int applicantId)
        {
            Message msg = new ApplicantDAL().AccountStatusUpdate(applicantId, 1, loggedInUser.userId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SendAccountActivationEmail(int applicantId)
        {
            Message msg = new Message();// ApplicantDAL().ApplicantStatusUpdate(applicantId, 52, 1);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ApplicantUpdate(Applicant obj)
        {
            obj.adminId = loggedInUser.userId;

            Message msg = new ApplicantDAL().ApplicantUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ApplicantDelete(Applicant obj)
        {
            obj.adminId = loggedInUser.userId;

            Message msg = new ApplicantDAL().ApplicantDelete(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ApplicantSendMessage(int applicantId)
        {
            Message msg = new Message();
            try
            {
                Applicant applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, applicantId);

                string smsBody = "";
                int smsId = 0;
                try
                {
                    SMS sms = new SMSDAL().GetByTypeForApplicant(applicantId, ProjConstant.SMSType.sendPasswordAdmin);
                    smsBody = sms.detail;
                    smsId = sms.smsId;
                }
                catch (Exception)
                {
                    smsBody = "";
                }
                if (String.IsNullOrWhiteSpace(smsBody))
                {
                    smsId = 0;
                    smsBody = "Dear Applicant, Your current password  for PRP Induction January (2021) against emailId(" + applicant.emailId + ") is : " + applicant.passwordDecrypt + "";
                }
                msg = FunctionUI.SendSms(applicant.contactNumber, smsBody);

                try
                {
                    SmsProcess objProcess = msg.status.SmsProcessMakeDefaultObject(applicantId, smsId);
                    new SMSDAL().AddUpdateSmsProcess(objProcess);
                }
                catch (Exception)
                {


                }
            }
            catch (Exception)
            {

                msg.status = false;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Ticker
        [CheckHasRight]
        public ActionResult TickerManage()
        {
            TickerModelAdmin model = new TickerModelAdmin();
            model.inductionId = Request.QueryString["inductionId"].TooInt();
            model.typeId = Request.QueryString["typeId"].TooInt();
            if (model.inductionId == 0)
                model.inductionId = ProjConstant.inductionId;


            DDLConstants dDLConstant = new DDLConstants();
            dDLConstant.condition = "";
            dDLConstant.typeId = ProjConstant.Constant.tickerType;
            model.listType = new ConstantDAL().GetConstantDDL(dDLConstant);

            if (model.typeId == 0)
                model.typeId = model.listType.FirstOrDefault().id;
            model.list = new MasterSetupDAL().TickerGetByInduction(model.inductionId, model.typeId);
            return View(model);
        }

        [CheckHasRight]
        public ActionResult TickerSetup()
        {
            TickerModelAdmin model = new TickerModelAdmin();
            int tickerId = Request.QueryString["id"].TooInt();
            if (tickerId > 0)
            {
                model.ticker = new MasterSetupDAL().TickerGetById(tickerId);
                model.ticker.detailShort = model.ticker.detail;
                model.ticker.detailLong = model.ticker.detail;
            }

            DDLConstants dDLConstant = new DDLConstants();
            dDLConstant.condition = "";
            dDLConstant.typeId = ProjConstant.Constant.tickerType;
            model.listType = new ConstantDAL().GetConstantDDL(dDLConstant);

            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult TickerSaveData(TickerModelAdmin ModelSave, HttpPostedFileBase files)
        {
            Ticker obj = ModelSave.ticker;
            obj.name = obj.name.TooString();
            if (obj.typeId == 1)
                obj.detail = obj.detailShort.TooString();
            else obj.detail = obj.detailLong.TooString();
            obj.adminId = loggedInUser.userId;
            if (obj.inductionId == 0) obj.inductionId = ProjConstant.inductionId;
            Message m = new MasterSetupDAL().TickerAddUpdate(obj);
            return Redirect("/admin/ticker-manage?inductionId=" + obj.inductionId + "&typeId=" + obj.typeId);
        }

        #endregion

        #region Department

        [CheckHasRight]
        public ActionResult DepartmentManage()
        {
            DepartmentModelAdmin model = new DepartmentModelAdmin();
            model.typeId = Request.QueryString["typeId"].TooInt();
            model.listType = new ConstantDAL().GetAll(0).OrderBy(x => x.id).ToList();
            model.list = new CommonDAL().DepartmentGetAll(model.typeId).OrderBy(x => x.name).ToList();
            return View(model);
        }

        [CheckHasRight]
        public ActionResult DepartmentSetup()
        {
            DepartmentModelAdmin model = new DepartmentModelAdmin();
            int departmentId = Request.QueryString["id"].TooInt();
            if (departmentId > 0)
                model.department = new CommonDAL().DepartmentGetById(departmentId);

            model.listType = new ConstantDAL().GetAll(0);

            return View(model);
        }
        [ValidateInput(false)]
        public ActionResult SaveDepartmentData(DepartmentModelAdmin ModelSave, HttpPostedFileBase files)
        {
            Department obj = ModelSave.department;
            obj.name = obj.name.TooString();
            obj.code = obj.code.TooString();
            obj.dated = DateTime.Now;
            obj.adminId = loggedInUser.userId;
            Message m = new CommonDAL().DepartmentAddUpdate(obj);

            return Redirect(ModelSave.redirectUrl);
        }


        
        #endregion

    }
}