using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prp.Sln
{
    public class ModelBase
    {
        //public List<Menu> listMenu { get; set; }
        //public Hospital hospital { get; set; }
        public int applicationStatusId { get; set; }
        public int validStatus { get; set; }
        public Applicant loggedInUser { get; set; }

        public string redirectUrl { get; set; }

        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public int consentRound { get; set; }
        public List<Ticker> listTicker { get; set; }
        public ModelBase()
        {
            loggedInUser = ProjFunctions.CookieApplicantGet();
            applicationStatusId = 0;

            try
            {
                listTicker = new MasterSetupDAL().TickerGetByInduction(ProjConstant.inductionId);
            }
            catch (Exception)
            {
            }

            if (loggedInUser != null && loggedInUser.applicantId > 0)
            {
                try
                {
                    ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                        , loggedInUser.applicantId, ProjConstant.Constant.ApplicationStatusType.application).FirstOrDefault();

                    applicationStatusId = objStatus.statusId;
                }
                catch (Exception)
                {
                    applicationStatusId = 0;
                }

                try
                {
                    ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                        , loggedInUser.applicantId, ProjConstant.Constant.ApplicationStatusType.validApplication).FirstOrDefault();

                    validStatus = objStatus.statusId;
                }
                catch (Exception)
                {
                    validStatus = 0;
                }

              
            }
        }
    }

    #region Home

    public class HomeModel : ModelBase
    {


    }

    public class ChangePasswordModel
    {
        public ChangePassword password { get; set; }

        public ChangePasswordModel()
        {
            password = new ChangePassword();
        }
    }

    public class LoginModel : ModelBase
    {
        public int applicationId { get; set; }
        public bool isAlreadyConfirmed { get; set; }

        public Message msg { get; set; }

        public string redirectUrl { get; set; }

        public LoginModel()
        {

            msg = new Message();
        }


    }

    public class ContactModel : ModelBase
    {
        public int typeId { get; set; }
        public bool isChangeAble { get; set; }
        public Contact contact { get; set; }

        public List<Contact> listQuestion { get; set; }
        public List<ContactReply> listAnswer { get; set; }

        public List<ContactDoc> listDocs { get; set; }

        public Applicant applicant { get; set; }

        public List<ContactStatus> listContactStatus { get; set; }

        public List<ApplicantApprovalStatus> listStatusApproval { get; set; }

        public ContactModel()
        {
            contact = new Contact();
            listQuestion = new List<Contact>();
            applicant = new Applicant();
            listContactStatus = new List<ContactStatus>();
            listStatusApproval = new List<ApplicantApprovalStatus>();
        }
    }

    public class RegistrationModel : ModelBase
    {
        public Applicant applicant { get; set; }

        public RegistrationModel()
        {
            applicant = new Applicant();

        }

    }

    public class ContentPageModel : ModelBase
    {
        public string key { get; set; }
        public string msg { get; set; }

        public string content { get; set; }
        public EmailEntity email { get; set; }

        public ContentPageModel()
        {
            email = new EmailEntity();
        }

    }

    public class SpecialityJobModel : ModelBase
    {
        public int typeId { get; set; }
        public List<Constant> listDegreeType { get; set; }

        public List<Constant> listQouta { get; set; }
        public List<SpecialityJob> listSpecialityJob { get; set; }

    }

    public class ProfileModel : ModelBase
    {
        public List<Region> listCountry { get; set; }
        public List<Region> listProvince { get; set; }

        public List<Constant> listDegree { get; set; }

        public List<Discipline> listDiscipline { get; set; }

        public List<Constant> listInstituteType { get; set; }
        public List<Constant> listInstituteLevel { get; set; }

        public List<Institute> listInstitute { get; set; }

        public List<Speciality> listSpeciality { get; set; }

        public List<EntityDDL> listRegion { get; set; }
        public List<EntityDDL> listJournal { get; set; }

        public string countryJson { get; set; }
        public string degreeAcheived { get; set; }
        public string levelId { get; set; }
        public ProfileModel()
        {
            listProvince = new List<Region>();
            listCountry = new List<Region>();
            listDegree = new List<Constant>();
            listInstituteType = new List<Constant>();
            listInstitute = new List<Institute>();
            listInstituteLevel = new List<Constant>();
            listSpeciality = new List<Speciality>();
            listDiscipline = new List<Discipline>();
            listRegion = new List<EntityDDL>();
            listJournal = new List<EntityDDL>();
        }
    }


    public class ProofReadingModel : ModelBase
    {
        public Applicant applicant { get; set; }

        public ApplicantInfo applicantInfo { get; set; }
        public ApplicantDegree degree { get; set; }

        public List<ApplicantDegreeMark> listDegreeMark { get; set; }

        public List<ApplicantHouseJob> listJob { get; set; }

        public List<ApplicantCertificate> listCertificate { get; set; }

        public List<ApplicantExperience> listExprince { get; set; }

        public List<ApplicantDistinction> listDistinction { get; set; }

        public List<ApplicantResearchPaper> listResearchPaper { get; set; }

        public List<ApplicantSpecility> listSpecility { get; set; }

        public ApplicantVoucher voucher { get; set; }

        public List<EntityDDL> listType { get; set; }

        public List<Marks> listMarks { get; set; }

        public Message distinctionMessage { get; set; }

        public List<ApplicantSpecilityMerit> listSpecilityMerit { get; set; }

        public ProofReadingModel()
        {
            applicant = new Applicant();
            applicantInfo = new ApplicantInfo();
            degree = new ApplicantDegree();
            listDegreeMark = new List<ApplicantDegreeMark>();
            listExprince = new List<ApplicantExperience>();
            listDistinction = new List<ApplicantDistinction>();
            listResearchPaper = new List<ApplicantResearchPaper>();
            listSpecility = new List<ApplicantSpecility>();
            listMarks = new List<Marks>();
            voucher = new ApplicantVoucher();
            listSpecilityMerit = new List<ApplicantSpecilityMerit>();
        }
    }


    public class CompletedDistictionModel : ModelBase
    {
        public Applicant applicant { get; set; }

        public CompletedDistictionModel()
        {
            applicant = new Applicant();
        }
    }

    public class ApplicantApplicationModel : ModelBase
    {
        public Applicant applicant { get; set; }

        public ApplicantInfo applicantInfo { get; set; }
        public ApplicantDegree degree { get; set; }

        public ApplicationStatus status { get; set; }

        public List<ApplicantDegreeMark> listDegreeMark { get; set; }


        public List<ApplicantExperience> listExprince { get; set; }

        public List<ApplicantDistinction> listDistinction { get; set; }

        public List<ApplicantResearchPaper> listResearchPaper { get; set; }

        public List<ApplicantSpecility> listSpecility { get; set; }

        public List<EntityDDL> listType { get; set; }

        public ApplicantApplicationModel()
        {
            applicant = new Applicant();
            applicantInfo = new ApplicantInfo();
            degree = new ApplicantDegree();
            listDegreeMark = new List<ApplicantDegreeMark>();
            listExprince = new List<ApplicantExperience>();
            listDistinction = new List<ApplicantDistinction>();
            listResearchPaper = new List<ApplicantResearchPaper>();
            listSpecility = new List<ApplicantSpecility>();
            status = new ApplicationStatus();
        }
    }


    public class ApplicantVoucherModel : ModelBase
    {
        public ApplicantVoucherParam voucher { get; set; }
    }

    public class ApplicationStatusModel : ModelBase
    {
        public List<ApplicationStatus> listStatus { get; set; }

        public List<ApplicantApprovalStatus> listStatusApproval { get; set; }

        public ApplicantInfo info { get; set; }
        public ApplicantSelected final { get; set; }

        public ApplicationStatusModel()
        {
            listStatus = new List<ApplicationStatus>();
            listStatusApproval = new List<ApplicantApprovalStatus>();
            final = new ApplicantSelected();
        }
    }

    public class ApplicantApprovalStatusModel : ModelBase
    {
        public List<ApplicantApprovalStatus> listStatus { get; set; }

    }




    public class MeritGazatModel : ModelBase
    {

        public ExportExcel exportExcel { get; set; }
        public GazatMerit gazatMerit { get; set; }
        public Applicant applicant { get; set; }
        public string search { get; set; }

        public MeritGazatModel()
        {
            inductionId = ProjConstant.inductionId;
            phaseId = ProjConstant.phaseId;
            consentRound = ProjConstant.consentRound;
        }


    }

    public class JobModel
    {

        public ExportExcel exportExcel { get; set; }
        public GazatMerit gazatMerit { get; set; }
        public Applicant applicant { get; set; }
        public string search { get; set; }

    }


    public class FeedBackModel : ModelBase
    {

        public FeedBack feedBack { get; set; }

        public List<FeedBack> listFeedBack { get; set; }

        public FeedBackModel()
        {
            feedBack = new FeedBack();
            listFeedBack = new List<FeedBack>();
        }

    }


    public class GrievanceModel : ModelBase
    {
        public int grievanceId { get; set; }
        public bool isEditAble { get; set; }
        public Grievance grievance { get; set; }

        public GrievanceModel()
        {
            grievance = new Grievance();
        }

    }

    public class VoucherModel : ModelBase
    {


    }

    #endregion

    public class ConsentModel : ModelBase
    {
        public int roundId { get; set; }
        public int consentId { get; set; }
        public bool isEditAble { get; set; }
        public Consent consent { get; set; }

        public List<Consent> listConsent { get; set; }
        public Applicant applicant { get; set; }

        public List<EntityDDL> listType { get; set; }

        public List<EntityDDL> listConsentStatus { get; set; }

        public ConsentModel()
        {
            consent = new Consent();
            applicant = new Applicant();
            listType = new List<EntityDDL>();
            listConsentStatus = new List<EntityDDL>();
            listConsent = new List<Consent>();

        }

    }

    public class ApplicantJoiningModel : ModelBase
    {
        public Applicant applicant { get; set; }
        public ApplicantInfo info { get; set; }
        public ApplicantSelected applicantFinal { get; set; }
        public ApplicantJoiningModel()
        {
            applicantFinal = new ApplicantSelected();
            applicant = new Applicant();
            info = new ApplicantInfo();
        }

    }
}