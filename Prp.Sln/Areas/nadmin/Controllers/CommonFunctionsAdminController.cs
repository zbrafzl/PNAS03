using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Mozilla;
using Prp.Data;
using Prp.Data.DAL;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class CommonFunctionsAdminController : BaseAdminController
    {
      

        // GET: nadmin/CommonFunctions
        [HttpGet]
        public JsonResult RegionGetByCondition(int typeId, int parentId, string condition)
        {
            List<Region> list = new RegionDAL().RegionGetByCondition(typeId, parentId, condition);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult InstitueGetByType(int typeId)
        {
            List<Institute> list = new InstitueDAL().GetAll(typeId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ConstantGetByType(int typeId)
        {
            List<Constant> list = new ConstantDAL().GetAll(typeId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ConstantGetForDDL(DDLConstants obj)
        {
            List<EntityDDL> list = new ConstantDAL().GetConstantDDL(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SpecialityGetForDDL(DDLSpecialitys obj)
        {
            List<EntityDDL> list = new SpecialityDAL().GetSpecialityDDL(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

      
        [HttpPost]
        public JsonResult HospitalGetForDDL(DDLHospitals obj)
        {
            List<EntityDDL> list = new HospitalDAL().GetHospitalDDL(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SendSmsAdminSingle(SMSEntity obj)
        {
            Message msg = new Message();
            try
            {
                msg = FunctionUI.SendSms(obj.contactNo, obj.message);
                msg.status = true;
            }
            catch (Exception)
            {
                msg.status = false;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult SendSmsAdminByStatus(SMSEntity obj)
        {
            Message msg = new Message();
            try
            {
                int totalSend = 0;
                ApplicantEntity search = new ApplicantEntity();
                search.applicationStatusId = obj.statusId;
                search.condition = "GetByAccountApplicationStatusId";
                List<Applicant> listApplicant = new ApplicantDAL().GetApplicantList(search);

                if (listApplicant != null && listApplicant.Count > 0)
                {

                    foreach (var item in listApplicant)
                    {
                        try
                        {
                            msg = FunctionUI.SendSms(item.contactNumber, obj.message);
                            totalSend = totalSend + 1;
                        }
                        catch (Exception)
                        {

                        }

                    }

                    msg.msg = "Total Messeage Sent are  : " + totalSend;
                }

            }
            catch (Exception)
            {

                msg.status = false;
            }

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SendSmsAdminBulk(SMSEntity obj)
        {
            Message msg = new Message();
            try
            {
                int totalSend = 0;
                foreach (var item in obj.contactList)
                {
                    try
                    {
                        msg = FunctionUI.SendSms(item, obj.message);
                        totalSend = totalSend + 1;
                    }
                    catch (Exception)
                    {
                    }
                }
                msg.status = true;
                msg.msg = "Total Messeage Sent are  : " + totalSend;
            }
            catch (Exception)
            {
                msg.status = false;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult SendEmailAdminCustom(EmailEntity obj)
        {
            Message msg = new Message();
            try
            {
                StatusEmail objEmail = new StatusEmail();
                objEmail.emailStatusId = 0;
                objEmail.applicantId = obj.applicantId;
                objEmail.typeId = obj.typeId;
                objEmail.body = obj.body;
                objEmail.sechduleDate = DateTime.Now;
                objEmail.adminId = loggedInUser.userId;
                objEmail.applicantId = obj.applicantId;

                msg = obj.emailId.SendEmail(obj.subject, obj.title, obj.body);

                if (msg.status)
                {
                    objEmail.statusId = 1;
                    objEmail.statusMessage = msg.msg;
                }
                else
                {
                    objEmail.statusMessage = msg.msg;
                    objEmail.statusId = 2;
                }

                objEmail.statusMessage = objEmail.statusMessage.TooString();
                objEmail.typeId = 99;

                try
                {
                    new EmailDAL().EmailStatusAddUpdate(objEmail);
                }
                catch (Exception)
                {
                }

                if (objEmail.statusId == 1)
                {
                    msg.status = true;
                }
                else
                    msg.status = false;

            }
            catch (Exception)
            {
                msg.status = false;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]
        public JsonResult SendEmailToApplicantAdmin(EmailEntity obj)
        {
            Message msg = new Message();
            try
            {
                Applicant applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, obj.applicantId);
                if (applicant.applicationStatusId == 7 || applicant.applicationStatusId == 8)
                {

                    msg = obj.emailId.SendEmail(obj.subject, obj.title, obj.body);

                   

                    StatusEmail objEmail = new StatusEmail();
                    objEmail.emailStatusId = 0;
                    objEmail.applicantId = obj.applicantId;
                    objEmail.typeId = 16;
                    objEmail.body = obj.body;
                    objEmail.sechduleDate = DateTime.Now;
                    objEmail.adminId = loggedInUser.userId;
                    objEmail.applicantId = obj.applicantId;

                    if (msg.status)
                    {
                        objEmail.statusId = 1;
                        objEmail.statusMessage = msg.msg;
                    }
                    else
                    {
                        objEmail.statusMessage = msg.msg;
                        objEmail.statusId = 2;
                    }

                    objEmail.statusMessage = objEmail.statusMessage.TooString();


                    try
                    {
                        new EmailDAL().EmailStatusAddUpdate(objEmail);
                    }
                    catch (Exception)
                    {
                    }

                }
                msg.status = true;
            }
            catch (Exception)
            {
                msg.status = false;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult EmailSMSBulkSendingToRemaning(EmailEntity obj)
        {
            Message msgg = new Message();
            ApplicantStatusModel model = new ApplicantStatusModel();

            DataTable dt = new EmailSMSDAL().GetEmailStatusRemaningRecords(obj.type);

            int applicantId = 0; string name = "", pmdcNo = "", emailId = "", contactNumber = "";

            if (dt != null && dt.Rows.Count > 0)
            {
                
                string emailBody = "";
                string emailSubject = obj.subject;

               

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

                        #region SMS
                        string message = "";
                        int smsId = 0;
                        //#region SMS Body
                        //try
                        //{
                        //    SMS sms = new SMSDAL().GetByTypeForApplicant(applicantId, ProjConstant.Constant.SMSType.round03);
                        //    message = sms.detail;
                        //    smsId = sms.smsId;
                        //}
                        //catch (Exception)
                        //{
                        //    message = "Pls login to PRP portal. View merit and give consent in round 03 before 27-02-2021 (01:50PM).";
                        //}
                        //if (String.IsNullOrWhiteSpace(message))
                        //{
                        //    smsId = 0;
                          
                        //}

                        //#endregion

                        try
                        {
                            //Message msgSms = new Message();
                            //try
                            //{
                            //    msgSms = FunctionUI.SendSms(contactNumber, message);
                            //}
                            //catch (Exception)
                            //{
                            //}


                            //try
                            //{
                            //    SmsProcess objProcess = msgSms.status.SmsProcessMakeDefaultObject(obj.applicantId, smsId);
                            //    new SMSDAL().AddUpdateSmsProcess(objProcess);
                            //}
                            //catch (Exception)
                            //{


                            //}
                        }
                        catch (Exception)
                        {

                        }

                        #endregion

                        #region Email

                        string path = "";

                        path = ProjConstant.Email.Path.joiningApplicant;

                        string filePath = path.GetServerPathFolder();
                        string body = filePath.ReadFile();

                        try
                        {
                            string hospitalName = "";

                            try
                            {
                                ApplicantSelected af = new JoiningDAL().GetFinalApplicantById(ProjConstant.inductionId, applicantId);
                                if (af.applicantId > 0)
                                    hospitalName = af.instituteName;
                                else hospitalName = "";
                            }
                            catch (Exception)
                            {

                                hospitalName = "";
                            }


                            emailBody = body.Replace("{#name#}", name).Replace("{#pmdcNo#}", pmdcNo)
                                           .Replace("{#emailId#}", emailId).Replace("{#hospitalName#}", hospitalName);

                            Message msgEmail = SendAndSaveEmailStatus(applicantId, emailId, emailBody, emailSubject, obj.statusId);
                            msg.status = msgEmail.status;
                            msg.msg = msgEmail.msg;
                        }
                        catch (Exception)
                        {

                            emailBody = "";
                        }

                        #endregion

                    }
                    catch (Exception)
                    {
                        msg.status = false;
                    }


                }

            }

            return Json(msgg, JsonRequestBehavior.AllowGet);
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


        [HttpGet]
        public JsonResult SendAmendmentEmailSmsAdminByApplicant(int applicantId)
        {
            Message msg = new Message();
            try
            {
                string sms = "";
                string emailBody = "";



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
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        private Message SendAmendmentEmail(Applicant applicant, string emailBody, int emailTypeId, string subject = "")
        {
            Message msgEmail = new Message();
            try
            {
                if (String.IsNullOrWhiteSpace(subject))
                    subject = ProjConstant.Email.Subject.verification;
                msgEmail = applicant.emailId.SendEmail(subject, ProjConstant.Email.Title.verification, emailBody);
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
                objEmail.applicantId = applicant.applicantId;

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



        private Message SendFeedbackEmail(Applicant applicant, string emailBody, int emailTypeId, string subject, int detailId)
        {
            Message msgEmail = new Message();
            try
            {
                msgEmail = applicant.emailId.SendEmail(subject, ProjConstant.Email.Title.verification, emailBody);
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
                objEmail.applicantId = detailId;
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


        [HttpPost]
        public JsonResult SendFeedbackEmailAdminByApplicant(EmailEntity obj)
        {
            Message msg = new Message();
            try
            {
                string sms = "";
                string emailBody = "";

                Applicant applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, obj.applicantId);
                if (applicant != null && applicant.applicantId > 0)
                {
                    try
                    {

                        FeedBack feedBack = new FeedBackDAL().GetById(obj.detailId);

                        feedBack.datedReply = DateTime.Now;
                        feedBack.adminId = loggedInUser.userId;
                        feedBack.reply = obj.body;
                        new FeedBackDAL().UpdateFeedback(feedBack);

                        if (obj.status)
                        {

                            string path = ProjConstant.Email.Path.feedbackReply;
                            string filePath = path.GetServerPathFolder();
                            string body = filePath.ReadFile();

                            emailBody = body.Replace("{#name#}", applicant.name).Replace("{#pmdcNo#}", applicant.pmdcNo)
                                           .Replace("{#emailId#}", applicant.emailId).Replace("{#question#}", feedBack.comments)
                                           .Replace("{#answer#}", obj.body);

                            Message msgEmail = SendFeedbackEmail(applicant, emailBody, 101, ProjConstant.Email.Subject.feedbackReply, feedBack.feedbackId);
                            msg.status = msgEmail.status;
                            msg.msg = msgEmail.msg;
                        }
                    }
                    catch (Exception)
                    {

                        emailBody = "";
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
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #region Image

        [HttpPost]
        public ActionResult UploadImage()
        {
            Message msg = new Message();
            string imageName = "";
            string folderPath = "/Images/Applicant/";

            try
            {
                string imageType = Request.Form["imageType"].TooString();
                int applicantId = Request.Form["applicantId"].TooInt();

                if (applicantId > 0)
                    folderPath = folderPath + "/" + applicantId;
                else
                    folderPath = Request.Form["folderPath"].TooString();

                CreateDirectory(folderPath);

                // Checking no of files injected in Request object  
                if (Request.Files.Count > 0)
                {
                    try
                    {
                        //  Get all files from Request object  
                        HttpFileCollectionBase files = Request.Files;
                        for (int i = 0; i < files.Count; i++)
                        {
                            //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                            //string filename = Path.GetFileName(Request.Files[i].FileName);  

                            HttpPostedFileBase file = files[i];
                            string fname;

                            // Checking for Internet Explorer  
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                            }

                            imageName = imageType + Path.GetExtension(fname);


                            //imageName = fname;

                            //int number = 1;
                            //imageName = MakeUniqueFileName(folderPath, imageName, number);
                            //if (String.IsNullOrWhiteSpace(imageName))
                            //    imageName = fname;

                            string imagePath = Path.Combine(Server.MapPath(folderPath), imageName);
                            if (System.IO.File.Exists(imagePath))
                            {
                                System.IO.File.Delete(imagePath);
                            }

                            // Get the complete folder path and store the file inside it.  
                            //imagePath = Path.Combine(Server.MapPath(folderPath), imageName);

                            file.SaveAs(imagePath);
                        }

                        msg.id = 1;
                        msg.msg = imageName;
                    }
                    catch (Exception ex)
                    {
                        msg.id = 0;
                        msg.msg = ex.Message;
                    }
                }
                else
                {
                    msg.id = 0;
                    msg.msg = "No image selected";
                }
            }
            catch (Exception ex)
            {

                msg.id = 0;
                msg.msg = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        private string MakeUniqueFileName(string folderPath, string imageName, int number)
        {
            string imageFileName = imageName;
            try
            {
                string imagePath = Path.Combine(Server.MapPath(folderPath), imageName);
                if (System.IO.File.Exists(imagePath))
                {
                    imageName = Path.GetFileNameWithoutExtension(imageName) + number + Path.GetExtension(imageName);
                    imageFileName = MakeUniqueFileName(folderPath, imageName, number);
                }
            }
            catch (Exception)
            {
            }
            return imageFileName;
        }

        public void CreateDirectory(string subPath)
        {
            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

        }


        #endregion
    }
}