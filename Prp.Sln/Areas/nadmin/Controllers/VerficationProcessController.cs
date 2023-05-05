using OfficeOpenXml.Drawing.Chart;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;
using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class VerficationProcessController : BaseAdminController
    {
        [CheckHasRight]
        public ActionResult FinanceTeam()
        {
            ProofReadingAdminModel model = new ProofReadingAdminModel();
            try
            {
                int inductionId = AdminHelper.GetInductionId();
                int phaseId = AdminHelper.GetPhaseId();
                model.applicantId = Request.QueryString["applicantid"].TooInt();
                if (model.applicantId > 0)
                {
                    model = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, model.applicantId);
                }
            }
            catch (Exception)
            {
                model.applicantId = 0;
            }
            return View(model);
        }


        [CheckHasRight]
        public ActionResult ApplicantListForVerification()
        {

            if (loggedInUser.typeId == ProjConstant.Constant.UserType.applicant)
            {

            }

            ProofReadingAdminModel model = new ProofReadingAdminModel();
            try
            {
                int inductionId = AdminHelper.GetInductionId();
                int phaseId = AdminHelper.GetPhaseId();
                model.applicantId = Request.QueryString["applicantid"].TooInt();
                if (model.applicantId > 0)
                {
                    model = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, model.applicantId);
                }
            }
            catch (Exception)
            {
                model.applicantId = 0;
            }
            return View(model);
        }

        [CheckHasRight]
        public ActionResult ApplicantListForVerificationWithDownload()
        {
            VerificationModel model = new VerificationModel();
            return View(model);
        }

        [CheckHasRight]
        public ActionResult VerificationTeam()
        {
            ProofReadingAdminModel model = new ProofReadingAdminModel();
            try
            {
                string key = Request.QueryString["key"].TooString();
                string value = Request.QueryString["value"].TooString();

                model.search.key = key;
                model.search.value = value;

                if (!String.IsNullOrEmpty(key) && !String.IsNullOrWhiteSpace(value))
                {

                    Message msg = new VerificationDAL().GetApplicantIdBySearchVerification(value, key);
                    int applicantId = msg.id.TooInt();
                    if (applicantId > 0)
                    {
                        int inductionId = AdminHelper.GetInductionId();
                        int phaseId = AdminHelper.GetPhaseId();
                        model = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, applicantId);


                        model.search.key = key;
                        model.search.value = value;

                    }

                    model.statusId = msg.statusId;

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
        public ActionResult VerificationView()
        {
            ProofReadingAdminModel model = new ProofReadingAdminModel();
            try
            {
                string key = Request.QueryString["key"].TooString();
                string value = Request.QueryString["value"].TooString();

                model.search.key = key;
                model.search.value = value;

                if (!String.IsNullOrEmpty(key) && !String.IsNullOrWhiteSpace(value))
                {
                    Message msg = new VerificationDAL().GetApplicantIdBySearchVerification(value, key);
                    int applicantId = msg.id.TooInt();
                    if (applicantId > 0)
                    {
                        int inductionId = AdminHelper.GetInductionId();
                        int phaseId = AdminHelper.GetPhaseId();
                        model = AdminFunctions.GenerateModelProofReading(inductionId, phaseId, applicantId);
                        model.search.key = key;
                        model.search.value = value;


                    }
                    model.statusId = msg.statusId;
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
        public JsonResult AddUpdateVerficationStatus(VerificationEntity obj)
        {
            obj.adminId = loggedInUser.userId;
            obj.comments = obj.comments.TooString();
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;

            bool voucherStatus = true;

            //try
            //{
            //    ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
            //    , obj.applicantId, ProjConstant.Constant.ApplicationStatusType.voucherPhf).FirstOrDefault();

            //    if (objStatus != null && (objStatus.statusId == 6 || objStatus.statusId == 9))
            //    {
            //        voucherStatus = false;
            //    }
            //}
            //catch (Exception)
            //{
            //}

            //if (voucherStatus == false)
            //{
            //    obj.approvalStatusId = 2;
            //    if (!obj.comments.Contains("Voucher/Payment"))
            //    {
            //        if (String.IsNullOrWhiteSpace(obj.comments))
            //            obj.comments = " Voucher/Payment Unverified";
            //        else
            //            obj.comments = obj.comments + ", Voucher/Payment Unverified";
            //    }

            //}

            Message msg = new VerificationDAL().AddUpdateVerficationStatus(obj);

            //Applicant applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, obj.applicantId);
            //ApplicantInfo appInfo = new ApplicantDAL().GetApplicantInfoDetail(ProjConstant.inductionId, ProjConstant.phaseId, obj.applicantId);






            //string dateTime = "";
            //int emailTypeId = 0;
            //if (obj.approvalStatusId == 1)
            //    emailTypeId = ProjConstant.EmailTemplateType.varificationAccepted;
            //else if (obj.approvalStatusId == 2)
            //{
            //    emailTypeId = ProjConstant.EmailTemplateType.varificationRejected;
            //    try
            //    {
            //        ApplicantApprovalStatus objStatus = new VerificationDAL().GetApplicationApprovalStatusGetById(ProjConstant.inductionId
            //                                                                , ProjConstant.phaseId, obj.applicantId)
            //                                                                .FirstOrDefault(x => x.approvalStatusTypeId == 131);
            //        dateTime = objStatus.dateMessage;
            //    }
            //    catch (Exception)
            //    {
            //        dateTime = "";
            //    }

            //}
            //try
            //{
            //    EmailProcess objEmailProcess = new EmailProcess();

            //    objEmailProcess.applicantId = obj.applicantId;
            //    objEmailProcess.keyword = "";
            //    objEmailProcess.typeId = emailTypeId;
            //    objEmailProcess.adminId = loggedInUser.adminId;
            //    Message mmss = new EmailDAL().EmailProcessAdd(objEmailProcess);
            //}
            //catch (Exception)
            //{
            //}

            //#region SMS Process

            //int smsTypeId = 0;

            //if (obj.approvalStatusId == 1)
            //    smsTypeId = ProjConstant.SMSType.applicationApproved;
            //else if (obj.approvalStatusId == 2)
            //    smsTypeId = ProjConstant.SMSType.applicationReject;

            //SMS sms = new SMSDAL().GetByTypeForApplicant(applicant.applicantId, smsTypeId);
            //sms.detail = sms.detail.Replace("{date}", dateTime);
            //Message msgSms = new Message();
            //try
            //{
            //    msgSms = FunctionUI.SendSms(applicant.contactNumber, sms.detail);
            //}
            //catch (Exception)
            //{
            //}

            //try
            //{
            //    SmsProcess objProcess = msgSms.status.SmsProcessMakeDefaultObject(obj.applicantId, sms.smsId);
            //    new SMSDAL().AddUpdateSmsProcess(objProcess);
            //}
            //catch (Exception)
            //{
            //}

            //#endregion


            //EmailProcess objProcessEmail = new EmailDAL().EmailProcessGetByApplicantAndType(obj.applicantId, emailTypeId);

            //#region Email Sending And DB Process


            //Message msgEmail = new Message();
            //try
            //{
            //    objProcessEmail.emailId = applicant.emailId;
            //    objProcessEmail.isProcess = 1;
            //    msgEmail = objProcessEmail.SendEmail();

            //}
            //catch (Exception ex)
            //{
            //    msgEmail.status = false;
            //    msgEmail.msg = ex.Message;
            //}

            //try
            //{

            //    objProcessEmail.isSent = 0;
            //    if (msgEmail.status == true)
            //        objProcessEmail.isSent = 1;
            //    new EmailDAL().EmailStatusAddUpdate(objProcessEmail);
            //}
            //catch (Exception)
            //{
            //}

            //#endregion


            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddUpdateVerficationStatusJoining(VerificationEntity obj)
        {
            obj.adminId = loggedInUser.userId;
            obj.comments = obj.comments.TooString();
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            bool voucherStatus = true;
            Message msg = new VerificationDAL().AddUpdateVerficationStatusJoining(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult AddUpdateVerficationAmmenmentStatus(VerificationEntity obj)
        {
            obj.adminId = loggedInUser.userId;
            obj.comments = obj.comments.TooString();
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            Message msg = new VerificationDAL().AddUpdateVerficationStatus(obj);

            int emailTypeId = 0;
            try
            {
                Applicant applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, obj.applicantId);
                string message = "";
                int smsId = 0;



                //if (obj.approvalStatusId == 1)
                //    emailTypeId = ProjConstant.EmailTemplateType.varificationAccepted;
                //else if (obj.approvalStatusId == 2)
                //{
                //    emailTypeId = ProjConstant.EmailTemplateType.varificationRejected;


                if (obj.approvalStatusId == 1)
                {
                    emailTypeId = ProjConstant.EmailTemplateType.ammendmentAccepted;

                    #region SMS Body
                    try
                    {
                        SMS sms = new SMSDAL().GetByTypeForApplicant(applicant.applicantId, ProjConstant.SMSType.amendmentApproved);
                        message = sms.detail;
                        smsId = sms.smsId;
                    }
                    catch (Exception)
                    {
                        message = "";
                    }
                    if (String.IsNullOrWhiteSpace(message))
                    {
                        smsId = 0;
                        message = "Your application form has been amended as per request presented before the Grievances Committee. For detail please visit portal PRP.";
                    }

                    #endregion
                }
                else if (obj.approvalStatusId == 2)
                {
                    emailTypeId = ProjConstant.EmailTemplateType.ammendmentRejected;

                    #region Rejection Process
                    try
                    {
                        #region SMS Body

                        try
                        {
                            SMS sms = new SMSDAL().GetByTypeForApplicant(applicant.applicantId, ProjConstant.SMSType.amendmentReject);
                            message = sms.detail;
                            smsId = sms.smsId;
                        }
                        catch (Exception)
                        {
                            smsId = 0;
                            message = "Your application form has been amended as per request presented before the Grievances Committee. For detail please visit portal PRP.";
                        }

                        #endregion
                    }
                    catch (Exception)
                    {

                    }
                    #endregion
                }


                #region Email Body


                try
                {
                    EmailProcess objEmailProcess = new EmailProcess();

                    objEmailProcess.applicantId = obj.applicantId;
                    objEmailProcess.keyword = "";
                    objEmailProcess.typeId = emailTypeId;
                    objEmailProcess.adminId = loggedInUser.adminId;
                    Message mmss = new EmailDAL().EmailProcessAdd(objEmailProcess);
                }
                catch (Exception)
                {
                }

                //try
                //{
                //    string path = ProjConstant.Email.Path.amendmentApprove;
                //    string filePath = path.GetServerPathFolder();
                //    string body = filePath.ReadFile();

                //    emailBody = body.Replace("{#name#}", applicant.name).Replace("{#pmdcNo#}", applicant.pmdcNo)
                //        .Replace("{#emailId#}", applicant.emailId);

                //}
                //catch (Exception)
                //{
                //    emailBody = "";
                //}
                #endregion

                #region SMS Sending And DB Process

                try
                {
                    Message msgSms = new Message();
                    try
                    {
                        msgSms = FunctionUI.SendSms(applicant.contactNumber, message);
                    }
                    catch (Exception)
                    {
                    }


                    try
                    {
                        SmsProcess objProcess = msgSms.status.SmsProcessMakeDefaultObject(obj.applicantId, smsId);
                        new SMSDAL().AddUpdateSmsProcess(objProcess);
                    }
                    catch (Exception)
                    {


                    }
                }
                catch (Exception)
                {

                }

                #endregion

                EmailProcess objProcessEmail = new EmailDAL().EmailProcessGetByApplicantAndType(obj.applicantId, emailTypeId);

                #region Email Sending And DB Process


                Message msgEmail = new Message();
                try
                {
                    objProcessEmail.emailId = applicant.emailId;
                    objProcessEmail.isProcess = 1;
                    msgEmail = objProcessEmail.SendEmail();

                }
                catch (Exception ex)
                {
                    msgEmail.status = false;
                    msgEmail.msg = ex.Message;
                }

                try
                {

                    objProcessEmail.isSent = 0;
                    if (msgEmail.status == true)
                        objProcessEmail.isSent = 1;
                    new EmailDAL().EmailStatusAddUpdate(objProcessEmail);
                }
                catch (Exception)
                {
                }

                #endregion

            }
            catch (Exception)
            {

            }


            return Json(msg, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public JsonResult AmmendmetsProcess(AmmendmentsEntitymodel obj)
        {
            obj.adminId = loggedInUser.userId;
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            Message msg = new VerificationDAL().AddUpdateAmmendmentsStatus(obj);

            int emailTypeId = 0;
            try
            {
                Applicant applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, obj.applicantId);
                string message = "";
                int smsId = 0;





                ////if (obj.approvalStatusId == 1)
                ////{
                ////    emailTypeId = ProjConstant.EmailTemplateType.ammendmentAccepted;

                ////    #region SMS Body
                ////    try
                ////    {
                ////        SMS sms = new SMSDAL().GetByTypeForApplicant(applicant.applicantId, ProjConstant.SMSType.amendmentApproved);
                ////        message = sms.detail;
                ////        smsId = sms.smsId;
                ////    }
                ////    catch (Exception)
                ////    {
                ////        message = "";
                ////    }
                ////    if (String.IsNullOrWhiteSpace(message))
                ////    {
                ////        smsId = 0;
                ////        message = "Your application form has been amended as per request presented before the Grievances Committee. For detail please visit portal PRP.";
                ////    }

                ////    #endregion
                ////}
                ////else if (obj.approvalStatusId == 2)
                ////{
                ////    emailTypeId = ProjConstant.EmailTemplateType.ammendmentRejected;

                ////    #region Rejection Process
                ////    try
                ////    {
                ////        #region SMS Body

                ////        try
                ////        {
                ////            SMS sms = new SMSDAL().GetByTypeForApplicant(applicant.applicantId, ProjConstant.SMSType.amendmentReject);
                ////            message = sms.detail;
                ////            smsId = sms.smsId;
                ////        }
                ////        catch (Exception)
                ////        {
                ////            smsId = 0;
                ////            message = "Your application form has been amended as per request presented before the Grievances Committee. For detail please visit portal PRP.";
                ////        }

                ////        #endregion
                ////    }
                ////    catch (Exception)
                ////    {

                ////    }
                ////    #endregion
                ////}


                ////#region Email Body


                ////try
                ////{
                ////    EmailProcess objEmailProcess = new EmailProcess();

                ////    objEmailProcess.applicantId = obj.applicantId;
                ////    objEmailProcess.keyword = "";
                ////    objEmailProcess.typeId = emailTypeId;
                ////    objEmailProcess.adminId = loggedInUser.adminId;
                ////    Message mmss = new EmailDAL().EmailProcessAdd(objEmailProcess);
                ////}
                ////catch (Exception)
                ////{
                ////}


                ////#endregion

                ////#region SMS Sending And DB Process

                ////try
                ////{
                ////    Message msgSms = new Message();
                ////    try
                ////    {
                ////        msgSms = FunctionUI.SendSms(applicant.contactNumber, message);
                ////    }
                ////    catch (Exception)
                ////    {
                ////    }


                ////    try
                ////    {
                ////        SmsProcess objProcess = msgSms.status.SmsProcessMakeDefaultObject(obj.applicantId, smsId);
                ////        new SMSDAL().AddUpdateSmsProcess(objProcess);
                ////    }
                ////    catch (Exception)
                ////    {


                ////    }
                ////}
                ////catch (Exception)
                ////{

                ////}

                ////#endregion

                ////EmailProcess objProcessEmail = new EmailDAL().EmailProcessGetByApplicantAndType(obj.applicantId, emailTypeId);

                ////#region Email Sending And DB Process


                ////Message msgEmail = new Message();
                ////try
                ////{
                ////    objProcessEmail.emailId = applicant.emailId;
                ////    objProcessEmail.isProcess = 1;
                ////    msgEmail = objProcessEmail.SendEmail();

                ////}
                ////catch (Exception ex)
                ////{
                ////    msgEmail.status = false;
                ////    msgEmail.msg = ex.Message;
                ////}

                ////try
                ////{

                ////    objProcessEmail.isSent = 0;
                ////    if (msgEmail.status == true)
                ////        objProcessEmail.isSent = 1;
                ////    new EmailDAL().EmailStatusAddUpdate(objProcessEmail);
                ////}
                ////catch (Exception)
                ////{
                ////}

                ////#endregion

            }
            catch (Exception)
            {

            }


            return Json(msg, JsonRequestBehavior.AllowGet);


        }
        [ValidateInput(false)]
        public ActionResult ExportDataToExcelAndDownload(VerificationModel ModelSave)
        {
            Message msg = new Message();
            VerificationEntity search = ModelSave.verification;

            try
            {

                search.statusId = search.statusId.TooInt();
                string fileName = "ApplcantList.xlsx";

                if (search.statusId == 1)
                    fileName = "Approved" + fileName;
                else if (search.statusId == 2)
                    fileName = "Rejected" + fileName;
                else fileName = "All" + fileName;

                search.inductionId = ProjConstant.inductionId;
                search.phaseId = ProjConstant.phaseId;

                string filePath = fileName.GenerateFilePath(loggedInUser);
                if (!String.IsNullOrWhiteSpace(filePath))
                {
                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt = new VerificationDAL().ApplicantListVerifyExport(search);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        msg = filePath.ExcelFileWrite(dt);
                        filePath.FileDownload();
                    }
                    else
                    {
                        msg.status = false;
                        msg.msg = "";
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
            if (String.IsNullOrWhiteSpace(search.pageUrl))
                search.pageUrl = "/admin/voucher-list";
            return Redirect(search.pageUrl);
        }
    }
}