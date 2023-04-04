using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Math;
using Prp.Data;
using Prp.Sln.Areas.nadmin.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
    public class ApplicationAdminController : BaseController
    {

        public ActionResult ApplicationUpdate()
        {
            HomeModel model = new HomeModel();
            if (loggedInUser.adminId==0)
            {
                return Redirect("/profile-view-print");
            }
            return View(model);
        }

        public ActionResult ProfileProcess()
        {
            ProfileModel model = new ProfileModel();
            if (loggedInUser.adminId == 0)
            {
                return Redirect("/profile-view-print");
            }

            try
            {
                model.listProvince = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.province
                    , ProjConstant.Constant.pakistan);

                model.listCountry = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.country
                  , 0, ProjConstant.Constant.Condition.GetAllCountry);

                model.listDegree = new ConstantDAL().GetAll(ProjConstant.Constant.degree);

                model.listInstituteType = new ConstantDAL().GetAll(ProjConstant.Constant.instituteType);
                model.listInstituteLevel = new ConstantDAL().GetAll(ProjConstant.Constant.instituteLevel);
                model.listInstitute = new InstitueDAL().GetAll(ProjConstant.Constant.InstituteType.govt);
                model.listSpeciality = new SpecialityDAL().GetAll();

                return View(model);
            }
            catch (Exception)
            {
                return View(model);
            }
        }

        public ActionResult EducationProcess()
        {
            ProfileModel model = new ProfileModel();

            if (loggedInUser.adminId == 0)
            {
                return Redirect("/profile-view-print");
            }


            try
            {
                model.listProvince = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.province
                  , ProjConstant.Constant.pakistan);

                model.listDegree = new ConstantDAL().GetAll(ProjConstant.Constant.degree);

                model.listInstituteType = new ConstantDAL().GetAll(ProjConstant.Constant.instituteType);
                model.listInstitute = new InstitueDAL().GetAll(ProjConstant.Constant.InstituteType.govt);

                model.listDiscipline = new CommonDAL().DisciplineGetAll();

                return View(model);
            }
            catch (Exception)
            {
                return View(model);
            }

        }

        public ActionResult ExprienceProcess()
        {
            ProfileModel model = new ProfileModel();

            model.listProvince = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.province
             , ProjConstant.Constant.pakistan);

            model.listInstituteType = new ConstantDAL().GetAll(ProjConstant.Constant.instituteType);

            if (loggedInUser.adminId == 0)
            {
                return Redirect("/profile-view-print");
            }

            return View(model);

        }

        public ActionResult ResearchPaperProcess()
        {
            ProfileModel model = new ProfileModel();

            DDLRegions dDLRegion = new DDLRegions();
            dDLRegion.condition = "ByType";
            dDLRegion.typeId = ProjConstant.Constant.Region.country;
            model.listRegion = new MasterSetupDAL().GetRegionDDL(dDLRegion);

            if (loggedInUser.adminId == 0)
            {
                return Redirect("/profile-view-print");
            }
            return View(model);

        }

        public ActionResult SpecialityProcess()
        {
            ProfileModel model = new ProfileModel();

            if (loggedInUser.adminId == 0)
            {
                return Redirect("/profile-view-print");
            }
            return View(model);


        }

        public ActionResult ApplicationVoucher()
        {
            if (loggedInUser.adminId == 0)
            {
                return Redirect("/profile-view-print");
            }

            ApplicantVoucherModel voucher = new ApplicantVoucherModel();
            return View(voucher);
        }


        [HttpPost]
        public JsonResult ApplicantInfoAddUpdate(ApplicantInfoParam objApplicantInfo)
        {
            Message msg = new Message();

            int applicationStatus = 1;

            try
            {
                ApplicantInfo obj = new ApplicantInfo();
                obj.applicantId = loggedInUser.applicantId;
                obj.fatherName = objApplicantInfo.fatherName.TooString();
                obj.genderId = objApplicantInfo.genderId;
                obj.disableId = objApplicantInfo.disableId;
                obj.countryIdDegreePassing = objApplicantInfo.countryIdDegreePassing;
                obj.countryId = objApplicantInfo.countryId;
                obj.districtId = objApplicantInfo.districtId;
                obj.districtName = objApplicantInfo.districtName;
                obj.domicileProvinceId = objApplicantInfo.domicileProvinceId;
                obj.domicileDistrictId = objApplicantInfo.domicileDistrictId;
                obj.address = objApplicantInfo.address;
                obj.cnicNo = objApplicantInfo.cnicNo;
                obj.cnicPicFront = objApplicantInfo.cnicPicFront;
                obj.cnicPicBack = objApplicantInfo.cnicPicBack;
                obj.domicilePicFront = objApplicantInfo.domicilePicFront;
                obj.domicilePicBack = objApplicantInfo.domicilePicBack;
                obj.pic = objApplicantInfo.pic;
                obj.disableImage = objApplicantInfo.disableImage;
                obj.dob = objApplicantInfo.dob.TooDate();
                obj.pmdcExpiryDate = objApplicantInfo.pmdcExpiryDate.TooDate();
                obj.mbbsPassingDate = objApplicantInfo.mbbsPassingDate.TooDate();
                obj.cnicExpiryDate = objApplicantInfo.cnicExpiryDate.TooDate();
                obj.adminId = loggedInUser.adminId;
                obj.inductionId = ProjConstant.inductionId;
                obj.phaseId = ProjConstant.phaseId;
                msg = new ApplicantAdminDAL().ApplicantInfoAddUpdate(obj);
                msg.id = applicationStatus;

            }
            catch (Exception)
            {
                msg.id = applicationStatus;

            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ApplicantEducationAddUpdate(ApplicantEducationParam objEducation)
        {


            ApplicantDegree degree = objEducation.applicantDegree;

            degree.typeIds = degree.typeIds.TooString().TrimStart(',').TrimEnd(',');
            degree.adminId = loggedInUser.adminId;
            degree.inductionId = ProjConstant.inductionId;
            degree.phaseId = ProjConstant.phaseId;

            List<ApplicantDegreeMark> listMarks = objEducation.listDegreeMarks;
            Message msg = new ApplicantAdminDAL().ApplicantDegreeAddUpdate(degree);
            if (msg.status == true)
            {
                if (listMarks != null && listMarks.Count > 0)
                    msg = new ApplicantAdminDAL().ApplicantDegreeMarksAddUpdate(listMarks, ProjConstant.inductionId, ProjConstant.phaseId, loggedInUser.adminId);

                if (objEducation.listCertificate != null && objEducation.listCertificate.Count > 0)
                {
                    foreach (var item in objEducation.listCertificate)
                    {
                        item.inductionId = ProjConstant.inductionId;
                        item.phaseId = ProjConstant.phaseId;
                        item.passingDate = item.datePassing.TooDate();
                        item.adminId = loggedInUser.adminId;
                    }
                    msg = new ApplicantAdminDAL().ApplicantCertificateAddUpdate(objEducation.listCertificate);
                }
            }


            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ApplicantHouseJobAddUpdate(ApplicantHouseJob obj)
        {

            obj.inductionId = ProjConstant.inductionId;
            obj.startDate = obj.startDateStr.TooDate();
            obj.endDate = obj.endDateStr.TooDate();
            obj.dated = DateTime.Now;
            obj.hospitalId = obj.hospitalId.TooInt();
            obj.hospital = obj.hospital.TooString();
            obj.adminId = loggedInUser.adminId;

            Message msg = new ApplicantAdminDAL().ApplicantHouseJobAddUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ApplicantExperienceAddUpdate(ApplicantExperienceParam objExperience)
        {
            ApplicantExperience obj = new ApplicantExperience();
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            obj.applicantExperienceId = objExperience.applicantExperienceId;
            obj.applicantId = loggedInUser.applicantId;
            obj.levelId = objExperience.levelId;
            obj.levelTypeId = objExperience.levelTypeId;
            obj.instituteId = objExperience.instituteId;
            obj.provinceId = objExperience.provinceId;
            obj.typeId = objExperience.typeId;
            obj.instituteName = objExperience.instituteName;
            obj.imageExperience = objExperience.imageExperience;
            obj.isCurrent = objExperience.isCurrent;
            obj.startDated = objExperience.startDated.TooDate();
            if (obj.isCurrent)
                obj.endDate = DateTime.Now;
            else
                obj.endDate = objExperience.endDate.TooDate();
            obj.dated = DateTime.Now;

            obj.adminId = loggedInUser.adminId;

            Message msg = new ApplicantAdminDAL().ApplicantExperienceAddUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ApplicantDistinctionAddUpdate(ApplicantDistinctionParam objExperience)
        {
            //imageDistinction
            ApplicantDistinction obj = new ApplicantDistinction();
            obj.applicantDistinctionId = objExperience.applicantDistinctionId;
            obj.applicantId = loggedInUser.applicantId;
            obj.subject = objExperience.subject;
            obj.year = objExperience.year;
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;

            obj.imageDistinction = objExperience.imageDistinction;
            obj.dated = DateTime.Now;
            obj.adminId = loggedInUser.adminId;

            Message msg = new ApplicantAdminDAL().ApplicantDistinctionAddUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ApplicantResearchPaperAddUpdate(ApplicantResearchPaperParam objExperience)
        {
            ApplicantResearchPaper obj = new ApplicantResearchPaper();
            obj.applicantResearchId = objExperience.applicantResearchId;
            obj.applicantId = loggedInUser.applicantId;
            obj.name = objExperience.name;
            obj.authorId = objExperience.authorId;
            obj.publishStatusId = objExperience.publishStatusId;
            obj.link = objExperience.link;
            obj.webLink = objExperience.webLink;
            obj.imageLetter = objExperience.imageLetter;
            obj.dated = DateTime.Now;
            obj.adminId = loggedInUser.adminId;
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            obj.researchJournalId = objExperience.researchJournalId;
            obj.isActive = objExperience.isActive;
            Message msg = new ApplicantAdminDAL().ApplicantResearchPaperAddUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ApplicantSpecilityAddUpdate(ApplicantSpecility objSpecility)
        {
            objSpecility.applicantId = loggedInUser.applicantId;
            objSpecility.dated = DateTime.Now;
            objSpecility.adminId = loggedInUser.adminId;
            objSpecility.inductionId = ProjConstant.inductionId;
            objSpecility.phaseId = ProjConstant.phaseId;
            Message msg = new ApplicantAdminDAL().ApplicantSpecilityAddUpdate(objSpecility);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ApplicantVoucherAddUpdate(ApplicantVoucherParam objParam)
        {

            ApplicantVoucher obj = new ApplicantVoucher();
            obj.applicantId = loggedInUser.applicantId;
            obj.applicantVoucherId = objParam.applicantVoucherId.TooInt();
            obj.branchCode = objParam.branchCode.TooString();
            obj.voucherImage = objParam.voucherImage.TooString();
            obj.ibn = objParam.ibn.TooString();
            obj.accountNo = objParam.accountNo.TooString();
            obj.accountTitle = objParam.accountTitle.TooString();
            obj.submittedDate = objParam.dateSubmitted.TooDate();
            obj.adminId = loggedInUser.adminId;
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            Message msg = new ApplicantAdminDAL().ApplicantVoucherAddUpdate(obj);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }


    }
}