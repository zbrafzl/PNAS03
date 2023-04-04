using Prp.Data;
using Prp.Data.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebSockets;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class EmailAndSmsAminController : BaseAdminController
    {

        #region Email Template Management

        [CheckHasRight]
        public ActionResult TemplateManageEmail()
        {
            EmailTemplateModel model = new EmailTemplateModel();
            model.template.inductionId = ProjConstant.inductionId;
            model.template.search = "";
            model.listTemplate = new EmailDAL().EmailTemplateSearch(model.template);

            return View(model);
        }

        [CheckHasRight]
        public ActionResult TemplateSetupEmail()
        {
            EmailTemplateModel model = new EmailTemplateModel();

            model.listType = new ConstantDAL().GetAll(ProjConstant.Constant.emailTemplate).OrderBy(x => x.id).ToList();

            model.typeId = Request.QueryString["typeId"].TooInt();

            if (model.typeId == 0 && (model.listType != null && model.listType.Count > 0))
                model.typeId = model.listType.FirstOrDefault().id.TooInt();

            if (model.typeId > 0)
            {
                model.template = new EmailDAL().EmailTemplateByTypeId(model.typeId);
            }
            else model.template.isActive = true;

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult SaveEmailTemplateData(EmailTemplateModel ModelSave, HttpPostedFileBase files)
        {
            EmailTemplate obj = ModelSave.template;
            obj.typeId = obj.typeId.TooInt();
            obj.body = obj.body.TooString();
            obj.adminId = loggedInUser.userId;
            obj.inductionId = ProjConstant.inductionId;

            Message m = new EmailDAL().EmailTemplateAddUpdate(obj);

            return Redirect(ModelSave.redirectUrl);
        }


        [CheckHasRight]
        public ActionResult EmailProcessView()
        {
            SMSModelAdmin model = new SMSModelAdmin();
            model.listType = new ConstantDAL().GetAll(ProjConstant.Constant.emailTemplate).OrderBy(x => x.id).ToList();
            return View(model);
        }


        public ActionResult EmailProcessStart()
        {
            SMSModelAdmin model = new SMSModelAdmin();
            try
            {
                string emailProcessIds = "";
                List<EmailProcess> listProcess = new EmailDAL().EmailProcessGetAllRemaninig().ToList();
                if (listProcess != null && listProcess.Count > 0)
                {
                    foreach (var email in listProcess)
                    {
                        try
                        {
                            Message msgSms = email.SendEmail();
                            if (msgSms.status)
                            {
                                model.totalSent = model.totalSent + 1;
                                emailProcessIds = emailProcessIds + email.emailProcessId + ",";
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }

                    emailProcessIds = emailProcessIds.TrimEnd(',');

                    new EmailDAL().EmailStatusUpdateByProcessIds(emailProcessIds);
                }
            }
            catch (Exception)
            {


            }
            return View(model);
        }

        [HttpGet]
        public JsonResult SendEmailByType(int typeId)
        {
            Message msg = new Message();
            int smsTotal = 0, smsSent = 0, smsFaild = 0;
            try
            {
                List<EmailProcess> listProcess = new EmailDAL().EmailProcessGetByType(typeId).ToList();
                if (listProcess != null && listProcess.Count > 0)
                {
                    smsTotal = listProcess.Count;
                    foreach (var email in listProcess)
                    {
                        try
                        {
                            email.isProcess = 1;
                            Message msgSms = email.SendEmail();
                            email.isSent = 0;
                            if (msgSms.status)
                            {
                                email.isSent = 1;
                                smsSent = smsSent + 1;

                                new EmailDAL().EmailStatusAddUpdate(email);
                            }
                            else smsFaild = smsFaild + 1;
                        }
                        catch (Exception)
                        {
                        }
                    }

                    msg.id = listProcess.Count;
                }
            }
            catch (Exception)
            {
                msg.id = 0;
            }

            msg.message = "Total Fetch : " + smsTotal + " Email Sent : " + smsSent + " Email Failed: " + smsFaild;


            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region SMS Template Management

        [CheckHasRight]
        public ActionResult TemplateManageSMS()
        {
            SMSModelAdmin model = new SMSModelAdmin();
            model.listType = new ConstantDAL().GetAll(ProjConstant.Constant.smsType).OrderBy(x => x.id).ToList();
            return View(model);
        }


        #endregion

        [CheckHasRight]
        public ActionResult SMSProcessView()
        {
            SMSModelAdmin model = new SMSModelAdmin();
            model.listType = new ConstantDAL().GetAll(ProjConstant.Constant.smsType).OrderBy(x => x.id).ToList();
            return View(model);
        }

        [CheckHasRight]
        public ActionResult SMSProcessQuery()
        {
            SMSModelAdmin model = new SMSModelAdmin();
            model.listType = new ConstantDAL().GetAll(ProjConstant.Constant.smsType).OrderBy(x => x.id).ToList();
            return View(model);
        }

        [HttpGet]
        public JsonResult SMSProcessCreateListBySmsId(int typeId, int smsId)
        {
            Message msg = new SMSDAL().SMSProcessCreateListBySmsId(typeId, smsId);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SendSMSByTypeAndSms(int typeId, int smsId)
        {
            Message msg = new Message();
            int smsTotal = 0, smsSent = 0, smsFaild = 0;
            try
            {
                List<SmsProcess> listProcess = new SMSDAL().SMSProcessGetBySmsId(smsId, 0).Take(50).ToList();
                if (listProcess != null && listProcess.Count > 0)
                {
                    smsTotal = listProcess.Count;
                    foreach (var sms in listProcess)
                    {
                        Message msgSms = FunctionUI.SendSms(sms.contactNumber, sms.detail);
                        try
                        {
                            SmsProcess objProcess = msgSms.status.SmsProcessMakeDefaultObject(sms.applicantId, sms.smsId);
                            objProcess.smsProcessId = sms.smsProcessId;
                            new SMSDAL().AddUpdateSmsProcess(objProcess);

                            if (msgSms.status)
                                smsSent = smsSent + 1;
                            else smsFaild = smsFaild + 1;
                        }
                        catch (Exception)
                        {
                        }
                    }

                    msg.id = listProcess.Count;
                }
            }
            catch (Exception)
            {
                msg.id = 0;
            }

            msg.message = "Total Fetch : " + smsTotal + " SMS Sent : " + smsSent + " SMS Failed: " + smsFaild;


            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetSMSAllByType(int typeId)
        {
            List<SMS> list = new SMSDAL().GetAllByType(ProjConstant.inductionId, typeId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [CheckHasRight]
        public ActionResult SMSProcess()
        {
            ApplicantStatusModel model = new ApplicantStatusModel();
            model.search.applicationStatusId = Request.QueryString["statusId"].TooInt();

            if (model.search.applicationStatusId == 0)
                model.search.applicationStatusId = 8;

            model.search.condition = "GetByAccountApplicationStatusId";
            //model.listApplicant = new ApplicantDAL().GetApplicantList(model.search);

            return View(model);
        }


        [CheckHasRight]
        public ActionResult SMSProcessSingle()
        {
            SmsEmailAdminModel model = new SmsEmailAdminModel();
            try
            {
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

                        model.applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, applicantId);

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

        [CheckHasRight]
        public ActionResult EmailProcess()
        {
            ApplicantStatusModel model = new ApplicantStatusModel();
            model.search.applicationStatusId = Request.QueryString["statusId"].TooInt();

            if (model.search.applicationStatusId == 0)
                model.search.applicationStatusId = 8;

            model.search.condition = "GetByAccountApplicationStatusId";

            return View(model);
        }

        [CheckHasRight]
        public ActionResult EmailProcessSingle()
        {
            SmsEmailAdminModel model = new SmsEmailAdminModel();
            try
            {
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

                        model.applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, applicantId);

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


        [CheckHasRight]
        public ActionResult EmailProcessCustomeSingle()
        {
            SmsEmailAdminModel model = new SmsEmailAdminModel();
            try
            {
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

                        model.applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, applicantId);

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

        [HttpPost]
        public void SendEmailToApplicantSingle(int applicantId)
        {
            Message msg = new Message();
            string path = ProjConstant.Email.Path.registration;

            string filePath = Path.Combine(Server.MapPath(path));
            string body = filePath.ReadFile();

            msg.id = msg.id + 10011;

            string key = msg.id.TooString().Encrypt();

            body = body.Replace("{#name#}", "").Replace("{#key#}", key);

            try
            {

                //  obj.emailId.SendEmail(ProjConstant.Email.Subject.registration
                //, ProjConstant.Email.Title.registration, body);
            }
            catch (Exception)
            {
            }
        }



        [CheckHasRight]
        public ActionResult ApplicationAmendmentEmailSMS()
        {
            SmsEmailAdminModel model = new SmsEmailAdminModel();
            try
            {
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

                        model.applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, applicantId);

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


        public ActionResult EmailSMSAmendmentAlert()
        {
            ApplicantStatusModel model = new ApplicantStatusModel();

            DataTable dt = new VerificationDAL().GetApplicationHasAmedmentAndNotSentEmail();

            int applicantId = 0;

            if (dt != null && dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    Message msg = new Message();
                    try
                    {
                        string sms = "";
                        string emailBody = "";

                        applicantId = dr["applicantId"].TooInt();

                        Applicant applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, applicantId);
                        if (applicant != null && applicant.applicantId > 0 && applicant.applicationStatusId == 8)
                        {
                            ApplicationVerificationStatus statusAmendment = new VerificationDAL().GetApplicationAmendmentsStatus(applicantId);
                            if (statusAmendment.statusId == 1)
                            {

                                sms = "Dear " + applicant.name + "( PMDC No: " + applicant.pmdcNo + ", EmailId: " + applicant.emailId + " )."
                                        + " It is hereby informed that your Form for the PG Induction Aug-2020 has been approved by the Grievances Committee after due scrutiny."
                                        + " Regards PRP Support Team.";

                                FunctionUI.SendSms(applicant.contactNumber, sms);

                                try
                                {
                                    string path = ProjConstant.Email.Path.amendmentApprove;
                                    string filePath = path.GetServerPathFolder();
                                    string body = filePath.ReadFile();

                                    emailBody = body.Replace("{#name#}", applicant.name).Replace("{#pmdcNo#}", applicant.pmdcNo)
                                                   .Replace("{#emailId#}", applicant.emailId);

                                    Message msgEmail = SendAmendmentEmail(applicant, emailBody, 41);
                                    msg.status = msgEmail.status;
                                    msg.msg = msgEmail.msg;
                                }
                                catch (Exception)
                                {

                                    emailBody = "";
                                }

                            }
                            else if (statusAmendment.statusId == 2)
                            {

                                sms = "Dear " + applicant.name + "( PMDC No: " + applicant.pmdcNo + ", EmailId: " + applicant.emailId + " )."
                                      + " It is hereby informed that your Form for the PG Induction Aug-2020 has been rejected by the Grievances Committee after due scrutiny. "
                                      + " Regards PRP Support Team.";

                                FunctionUI.SendSms(applicant.contactNumber, sms);

                                try
                                {
                                    string path = ProjConstant.Email.Path.amendmentReject;
                                    string filePath = path.GetServerPathFolder();
                                    string body = filePath.ReadFile();

                                    emailBody = body.Replace("{#name#}", applicant.name).Replace("{#pmdcNo#}", applicant.pmdcNo)
                                                    .Replace("{#emailId#}", applicant.emailId);

                                    Message msgEmail = SendAmendmentEmail(applicant, emailBody, 46);

                                    msg.status = msgEmail.status;
                                    msg.msg = msgEmail.msg;
                                }
                                catch (Exception)
                                {
                                    emailBody = "";
                                }

                            }
                            else
                            {
                                msg.status = false;
                                msg.msg = "No Amendment.";
                            }
                        }
                        else
                        {
                            msg.status = false;
                            msg.msg = "Applicant not exist";
                        }
                    }
                    catch (Exception)
                    {
                        msg.status = false;
                    }


                }

            }

            return View(model);
        }

        public ActionResult EmailSMSGazetteMarksAlert()
        {
            ApplicantStatusModel model = new ApplicantStatusModel();

            DataTable dt = new EmailSMSDAL().GetEmailStatusRemaningRecords("GazetteRemaningApplicant");

            int applicantId = 0; string name = "", pmdcNo = "", emailId = "", contactNumber = "";

            if (dt != null && dt.Rows.Count > 0)
            {
                string sms = "";
                string emailBody = "";
                string path = ProjConstant.Email.Path.grievancesGazette;
                string filePath = path.GetServerPathFolder();
                string body = filePath.ReadFile();


                foreach (DataRow dr in dt.Rows)
                {
                    Message msg = new Message();
                    try
                    {


                        applicantId = dr["applicantId"].TooInt();
                        name = dr["name"].TooString();
                        pmdcNo = dr["pmdcNo"].TooString();
                        emailId = dr["emailId"].TooString();
                        contactNumber = dr["contactNumber"].TooString();

                        sms = "Dear " + name + "( PMDC No: " + pmdcNo + ", EmailId: " + emailId + " )."
                                       + " Grievances Committee Meeting will held: 28to30/09/2020 at KEMU. If any issue please appear. Check prp.phf.gop.pk for details.."
                                       + " Regards PRP Support Team.";

                        FunctionUI.SendSms(contactNumber, sms);

                        try
                        {


                            emailBody = body.Replace("{#name#}", name).Replace("{#pmdcNo#}", pmdcNo)
                                           .Replace("{#emailId#}", emailId);

                            Message msgEmail = SendAndSaveEmailStatus(applicantId, emailId, emailBody, ProjConstant.Email.Subject.grievancesGazette, 111);
                            msg.status = msgEmail.status;
                            msg.msg = msgEmail.msg;
                        }
                        catch (Exception)
                        {

                            emailBody = "";
                        }



                    }
                    catch (Exception)
                    {
                        msg.status = false;
                    }


                }

            }

            return View(model);
        }

        public ActionResult EmailSMSMeritMarksAlert()
        {
            ApplicantStatusModel model = new ApplicantStatusModel();

            DataTable dt = new EmailSMSDAL().GetEmailStatusRemaningRecords("MeritListRemaningApplicantRound03");

            int applicantId = 0; string name = "", pmdcNo = "", emailId = "", contactNumber = "";

            if (dt != null && dt.Rows.Count > 0)
            {
                string sms = "";
                string emailBody = "";
                string path = ProjConstant.Email.Path.meritListConsent;
                string filePath = path.GetServerPathFolder();
                string body = filePath.ReadFile();


                foreach (DataRow dr in dt.Rows)
                {
                    Message msg = new Message();
                    try
                    {


                        applicantId = dr["applicantId"].TooInt();
                        name = dr["name"].TooString();
                        pmdcNo = dr["pmdcNo"].TooString();
                        emailId = dr["emailId"].TooString();
                        contactNumber = dr["contactNumber"].TooString();

                        sms = "Dear,PMDC No:" + pmdcNo + "."
                               + " Please ensure consent before closing hours (23-10-2020 at 08PM).";


                        FunctionUI.SendSms(contactNumber, sms);

                        try
                        {


                            emailBody = body.Replace("{#name#}", name).Replace("{#pmdcNo#}", pmdcNo)
                                           .Replace("{#emailId#}", emailId);

                            Message msgEmail = SendAndSaveEmailStatus(applicantId, emailId, emailBody, ProjConstant.Email.Subject.meritList, 123);
                            msg.status = msgEmail.status;
                            msg.msg = msgEmail.msg;
                        }
                        catch (Exception)
                        {

                            emailBody = "";
                        }



                    }
                    catch (Exception)
                    {
                        msg.status = false;
                    }


                }

            }

            return View(model);
        }


        public ActionResult EmailSMSJoiningAlert()
        {
            ApplicantStatusModel model = new ApplicantStatusModel();

            DataTable dt = new EmailSMSDAL().GetEmailStatusRemaningRecords("JoningRemaningApplicant");

            int applicantId = 0; string name = "", pmdcNo = "", emailId = "", contactNumber = "";

            if (dt != null && dt.Rows.Count > 0)
            {
                string sms = "";
                string emailBody = "";
                string path = ProjConstant.Email.Path.joiningApplicant;
                string filePath = path.GetServerPathFolder();
                string body = filePath.ReadFile();


                foreach (DataRow dr in dt.Rows)
                {
                    Message msg = new Message();
                    try
                    {
                        string pathSMS = "/html/sms/joining.html";
                        string filePathSMS = pathSMS.GetServerPathFolder();
                        string bodySMS = filePathSMS.ReadFile();

                        applicantId = dr["applicantId"].TooInt();
                        name = dr["name"].TooString();
                        pmdcNo = dr["pmdcNo"].TooString();
                        emailId = dr["emailId"].TooString();
                        contactNumber = dr["contactNumber"].TooString();

                        sms = "Dear,PMDC No:" + pmdcNo + "." + bodySMS;


                        FunctionUI.SendSms(contactNumber, sms);

                        try
                        {
                            ApplicantSelected applicantFinal = new JoiningDAL().GetByApplicantDetailById(applicantId);
                            string hospitalName = applicantFinal.instituteName;

                            emailBody = body.Replace("{#name#}", name).Replace("{#pmdcNo#}", pmdcNo)
                                           .Replace("{#emailId#}", emailId).Replace("{#hospitalName#}", hospitalName);

                            Message msgEmail = SendAndSaveEmailStatus(applicantId, emailId, emailBody, ProjConstant.Email.Subject.joining, 131);
                            msg.status = msgEmail.status;
                            msg.msg = msgEmail.msg;
                        }
                        catch (Exception)
                        {

                            emailBody = "";
                        }



                    }
                    catch (Exception)
                    {
                        msg.status = false;
                    }


                }

            }

            return View(model);
        }


        public ActionResult EmailSMSSelectedCongratulationAlert()
        {
            ApplicantStatusModel model = new ApplicantStatusModel();

            DataTable dt = new EmailSMSDAL().GetEmailStatusRemaningRecords("CongratulationRemaningApplicant");

            int applicantId = 0; string name = "", pmdcNo = "", emailId = "", contactNumber = "";

            if (dt != null && dt.Rows.Count > 0)
            {
                string sms = "";
                string emailBody = "";
                string path = ProjConstant.Email.Path.congratulationApplicant;
                string filePath = path.GetServerPathFolder();
                string body = filePath.ReadFile();


                foreach (DataRow dr in dt.Rows)
                {
                    Message msg = new Message();
                    try
                    {
                        string pathSMS = "/html/sms/congratulationSMS.html";
                        string filePathSMS = pathSMS.GetServerPathFolder();
                        string bodySMS = filePathSMS.ReadFile();

                        applicantId = dr["applicantId"].TooInt();
                        name = dr["name"].TooString();
                        pmdcNo = dr["pmdcNo"].TooString();
                        emailId = dr["emailId"].TooString();
                        contactNumber = dr["contactNumber"].TooString();

                        sms = "Dear,PMDC No:" + pmdcNo + "." + bodySMS;


                        FunctionUI.SendSms(contactNumber, sms);

                        try
                        {
                            ApplicantSelected applicantFinal = new JoiningDAL().GetByApplicantDetailById(applicantId);
                            string hospitalName = applicantFinal.instituteName;

                            emailBody = body.Replace("{#name#}", name).Replace("{#pmdcNo#}", pmdcNo)
                                           .Replace("{#emailId#}", emailId).Replace("{#hospitalName#}", hospitalName);

                            Message msgEmail = SendAndSaveEmailStatus(applicantId, emailId, emailBody, ProjConstant.Email.Subject.congratulation, 151);
                            msg.status = msgEmail.status;
                            msg.msg = msgEmail.msg;
                        }
                        catch (Exception)
                        {

                            emailBody = "";
                        }



                    }
                    catch (Exception)
                    {
                        msg.status = false;
                    }


                }

            }

            return View(model);
        }

        public ActionResult EmailSMSBulkSendingToRemaning()
        {
            ApplicantStatusModel model = new ApplicantStatusModel();

            //DataTable dt = new EmailSMSDAL().GetEmailStatusRemaningRecords("RemaningRejectVerfication");

            //int applicantId = 0; string name = "", pmdcNo = "", emailId = "", contactNumber = "";

            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    string sms = "";
            //    string emailBody = "";
            //    string path = ProjConstant.Email.Path.meritListConsent;
            //    string filePath = path.GetServerPathFolder();
            //    string body = filePath.ReadFile();


            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        Message msg = new Message();
            //        try
            //        {


            //            applicantId = dr["applicantId"].TooInt();
            //            name = dr["name"].TooString();
            //            pmdcNo = dr["pmdcNo"].TooString();
            //            emailId = dr["emailId"].TooString();
            //            contactNumber = dr["contactNumber"].TooString();

            //            sms = "Dear,PMDC No:" + pmdcNo + "."
            //                   + "Pls login for merit, give consent (even already given).In case of any issue appear before Griev Comm on 06/10/20 (09-11AM) at KEMU";


            //            FunctionUI.SendSms(contactNumber, sms);

            //            try
            //            {


            //                emailBody = body.Replace("{#name#}", name).Replace("{#pmdcNo#}", pmdcNo)
            //                               .Replace("{#emailId#}", emailId);

            //                Message msgEmail = SendAndSaveEmailStatus(applicantId, emailId, emailBody, ProjConstant.Email.Subject.meritList, 121);
            //                msg.status = msgEmail.status;
            //                msg.msg = msgEmail.msg;
            //            }
            //            catch (Exception)
            //            {

            //                emailBody = "";
            //            }



            //        }
            //        catch (Exception)
            //        {
            //            msg.status = false;
            //        }


            //    }

            //}

            return View(model);
        }


        private Message SendAmendmentEmail(Applicant applicant, string emailBody, int emailTypeId)
        {
            Message msgEmail = new Message();
            try
            {
                msgEmail = applicant.emailId.SendEmail(ProjConstant.Email.Subject.verification
                        , ProjConstant.Email.Title.verification, emailBody);
            }
            catch (Exception ex)
            {
                msgEmail.status = false;
                msgEmail.msg = ex.Message;
            }

            #region StatusEmail

            try
            {
                StatusEmail objEmail = new StatusEmail();
                objEmail.emailStatusId = 0;
                objEmail.applicantId = applicant.applicantId;
                objEmail.typeId = emailTypeId;
                objEmail.body = emailBody;
                objEmail.sechduleDate = DateTime.Now;
                objEmail.adminId = loggedInUser.userId;

                if (msgEmail.status)
                {
                    objEmail.statusId = 1;
                    objEmail.statusMessage = msgEmail.msg;
                }
                else
                {
                    objEmail.statusMessage = msgEmail.msg;
                    objEmail.statusId = 2;
                }

                objEmail.statusMessage = objEmail.statusMessage.TooString();

                new EmailDAL().EmailStatusAddUpdate(objEmail);
            }
            catch (Exception)
            {
            }

            #endregion


            return msgEmail;
        }


        private Message SendAndSaveEmailStatus(int applicantId, string emailId, string emailBody, string emailSubject, int emailTypeId)
        {
            Message msgEmail = new Message();
            try
            {
                msgEmail = emailId.SendEmail(emailSubject, ProjConstant.Email.Title.verification, emailBody);
            }
            catch (Exception ex)
            {
                msgEmail.status = false;
                msgEmail.msg = ex.Message;
            }

            #region StatusEmail

            try
            {
                StatusEmail objEmail = new StatusEmail();
                objEmail.emailStatusId = 0;
                objEmail.applicantId = applicantId;
                objEmail.typeId = emailTypeId;
                objEmail.body = emailBody;
                objEmail.sechduleDate = DateTime.Now;
                objEmail.adminId = loggedInUser.userId;

                if (msgEmail.status)
                {
                    objEmail.statusId = 1;
                    objEmail.statusMessage = msgEmail.msg;
                }
                else
                {
                    objEmail.statusMessage = msgEmail.msg;
                    objEmail.statusId = 2;
                }

                objEmail.statusMessage = objEmail.statusMessage.TooString();

                new EmailDAL().EmailStatusAddUpdate(objEmail);
            }
            catch (Exception)
            {
            }

            #endregion


            return msgEmail;
        }

    }
}