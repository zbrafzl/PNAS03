using Prp.Data;
using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln
{
    public class ModelBaseAdmin
    {
        public User loggedInUser { get; set; }
        public List<Menu> listMenu { get; set; }

        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public List<EntityDDL> listInduction { get; set; }

        public string redirectUrl { get; set; }

        public ModelBaseAdmin()
        {
            inductionId = ProjConstant.inductionId;
            phaseId = ProjConstant.phaseId;

            loggedInUser = ProjFunctions.CookiesAdminGet();
            listMenu = new MenuDAL().GetByUser(loggedInUser.userId, 2);
            listInduction = DDLInduction.GetAll("GetActiveAndCompleted");
        }
    }

    public class HomeModelAdmin : ModelBaseAdmin
    {
        public int typeId { get; set; }

        public List<EntityCount> listDashBoard { get; set; }

        public HomeModelAdmin()
        {
        
            listDashBoard = new List<EntityCount>();
        }
    }

    public class ApplicantJoiningDsbModel : ModelBaseAdmin
    {
        public List<ApplicantJoiningDsb> listHospInst { get; set; }
        public ApplicantJoiningDsbModel()
        {
            listHospInst = new List<ApplicantJoiningDsb>();
        }

    }

    public class ConstantModelAdmin : ModelBaseAdmin
    {
        public int typeId { get; set; }

        public Constant constant { get; set; }
        public List<Constant> listType { get; set; }
        public List<Constant> list { get; set; }

        public ConstantModelAdmin()
        {
            list = new List<Constant>();
            listType = new List<Constant>();
            constant = new Constant();
        }
    }

    public class DepartmentModelAdmin : ModelBaseAdmin
    {
        public int typeId { get; set; }

        public Department department { get; set; }
        public List<Constant> listType { get; set; }
        public List<Department> list { get; set; }

        public DepartmentModelAdmin()
        {
            list = new List<Department>();
            listType = new List<Constant>();
            department = new Department();
        }
    }
    public class TickerModelAdmin : ModelBaseAdmin
    {
        public int typeId { get; set; }

        public Ticker ticker { get; set; }

        public List<EntityDDL> listType { get; set; }

        public List<Ticker> list { get; set; }

        public TickerModelAdmin()
        {
            list = new List<Ticker>();
            ticker = new Ticker();
        }
    }

    #region SMS/ Emails

    public class EmailTemplateModel : ModelBaseAdmin
    {
        public int typeId { get; set; }
        public EmailTemplate template { get; set; }
        public List<EmailTemplate> listTemplate { get; set; }
        public List<Constant> listType { get; set; }

        public EmailTemplateModel()
        {
            template = new EmailTemplate();
           
            listTemplate = new List<EmailTemplate>();
            listType = new List<Constant>();

        }

    }

    #endregion

    public class SMSModelAdmin : ModelBaseAdmin
    {
        public int applicantId { get; set; }
        public int typeId { get; set; }
        public int smsId { get; set; }
        public SMS sms { get; set; }


        public int totalSent { get; set; }
        public SmsEntity search { get; set; }
        public List<Constant> listType { get; set; }
        public List<SMS> list { get; set; }

        public List<SmsProcess> listProcess { get; set; }

        public SMSModelAdmin()
        {
            list = new List<SMS>();
            listType = new List<Constant>();
            sms = new SMS();
            listProcess = new List<SmsProcess>();
            search = new SmsEntity();
        }
    }

    public class EmployeeModelAdmin : ModelBaseAdmin
    {
        public int hospitalId { get; set; }
        public int id { get; set; }
        public int employeeId { get; set; }
        public Employee employee { get; set; }
        public EmployeeSpeciality employeeSpeciality { get; set; }
        public List<EntityDDL> listGender { get; set; }
        public List<EntityDDL> listDegree { get; set; }
        public List<EntityDDL> listProgram { get; set; }
        //
        public List<EntityDDL> listRelation { get; set; }
        public List<EntityDDL> listMartialStatus { get; set; }
        public List<EntityDDL> listDesignation { get; set; }

        public List<EntityDDL> listDispline { get; set; }
        public List<EntityDDL> listSpeciality { get; set; }
        public List<EntityDDL> listHospital { get; set; }
        public List<EntityDDL> listDistrict { get; set; }
        public List<spEmployeeSearch_Result> list { get; set; }

        public List<EmployeeExperience> listExperience { get; set; }

        public EmployeeExperience employeeExperience { get; set; }

        public List<EntityDDL> listTypeExperience { get; set; }

        public EmployeeModelAdmin()
        {
            employee = new Employee();
            employeeSpeciality= new EmployeeSpeciality();
            list = new List<spEmployeeSearch_Result>();

            listGender = new List<EntityDDL>();
            listDegree = new List<EntityDDL>();
            listProgram = new List<EntityDDL>();
            listRelation = new List<EntityDDL>();
            listMartialStatus = new List<EntityDDL>();
            listDesignation =  new List<EntityDDL>();

            listDistrict = new List<EntityDDL>();
            listDispline = new List<EntityDDL>();
            listHospital = new List<EntityDDL>();

            listExperience = new List<EmployeeExperience>();
            employeeExperience = new EmployeeExperience();
            listTypeExperience = new List<EntityDDL>();
            listSpeciality = new List<EntityDDL>();
        }
    }

    public class EmployeeSpecialityModelAdmin : ModelBaseAdmin
    {
        public int id { get; set; }
        public int employeeId { get; set; }
        public Employee employee { get; set; }
        public EmployeeSpeciality speciality { get; set; }
        public List<EntityDDL> listProgram { get; set; }
        public List<EmployeeSpeciality> list { get; set; }

        public EmployeeSpecialityModelAdmin()
        {
            employee = new Employee();
            speciality = new EmployeeSpeciality();
            list = new List<EmployeeSpeciality>();
            listProgram = new List<EntityDDL>();
        }
    }

    public class EmployeeApplicantAdmin : ModelBaseAdmin
    {
        public int hospitalId { get; set; }
        public int employeeId { get; set; }
        public List<EntityDDL> listEmployee { get; set; }
        public List<EntityDDL> listHospital { get; set; }

        public EmployeeApplicantAdmin()
        {
            listEmployee = new List<EntityDDL>();
            listHospital = new List<EntityDDL>();
        }

    }

    public class UnitModelAdmin : ModelBaseAdmin
    {
        public int hospitalId { get; set; }
        public int departmentId { get; set; }
        public int unitId { get; set; }
        public Unit unit { get; set; }
        public List<EntityDDL> listHospital { get; set; }
        public List<EntityDDL> listDepartment { get; set; }
        public List<Unit> list { get; set; }

        public UnitModelAdmin()
        {
            unit = new Unit();
            list = new List<Unit>();
            listDepartment = new List<EntityDDL>();
            listHospital = new List<EntityDDL>();
        }
    }
    public class HospitalDisciplineModelAdmin : ModelBaseAdmin
    {
        public int hospitalId { get; set; }

        public List<EntityDDL> listType { get; set; }

        public HospitalDiscipline discipline { get; set; }

        public List<EntityDDL> listHospital { get; set; }
        public List<EntityDDL> listDiscipline { get; set; }
        public List<HospitalDiscipline> list { get; set; }

        public HospitalDisciplineModelAdmin()
        {
            discipline = new HospitalDiscipline();
            list = new List<HospitalDiscipline>();

            listDiscipline = new List<EntityDDL>();
            listHospital = new List<EntityDDL>();
        }
    }

    public class BedModelAdmin : ModelBaseAdmin
    {
        public int hospitalId { get; set; }
        public int departmentId { get; set; }
        public int unitId { get; set; }
        public int bedId { get; set; }

        public Bed bed { get; set; }

        public List<EntityDDL> listDiscipline { get; set; }

        public List<EntityDDL> listSpeciality { get; set; }

        public List<EntityDDL> listHospital { get; set; }
        public List<EntityDDL> listDepartment { get; set; }
        public List<Bed> list { get; set; }

        public BedModelAdmin()
        {
            bed = new Bed();
            list = new List<Bed>();
            listSpeciality = new List<EntityDDL>();
            listDiscipline = new List<EntityDDL>();
            listDepartment = new List<EntityDDL>();
            listHospital = new List<EntityDDL>();
        }
    }

    public class InstituteModelAdmin : ModelBaseAdmin
    {
      
        public Institute institute { get; set; }
        public List<Constant> listType { get; set; }

        public List<Region> listProvince { get; set; }
        public List<Institute> list { get; set; }

        public List<Hospital> listHospital { get; set; }

        public InstituteModelAdmin()
        {
            list = new List<Institute>();
            listType = new List<Constant>();
            institute = new Institute();
            listProvince = new List<Region>();
            listHospital = new List<Hospital>();
            institute = new Institute();
        }
    }

    public class SpecialityModelAdmin : ModelBaseAdmin
    {
        public int typeId { get; set; }
        public Speciality speciality { get; set; }

        public List<EntityDDL> listInduction { get; set; }
        public List<Constant> listType { get; set; }

        public List<Region> listProvince { get; set; }
        public List<Speciality> list { get; set; }

        public SpecialityModelAdmin()
        {
            list = new List<Speciality>();
            listType = new List<Constant>();
            speciality = new Speciality();
            listProvince = new List<Region>();
        }
    }


    public class DisciplineModelAdmin : ModelBaseAdmin
    {
        public int disciplineId { get; set; }
        public Discipline category { get; set; }

        public List<Discipline> listParent = new List<Discipline>();

        public List<Speciality> listSpeciality = new List<Speciality>();
        public List<Discipline> list = new List<Discipline>();
        public DisciplineModelAdmin()
        {
            list = new List<Discipline>();
            listSpeciality = new List<Speciality>();
            listParent = new List<Discipline>();
        }

    }

    public class SpecialityJobModelAdmin : ModelBaseAdmin
    {
        public int typeId { get; set; }
        public SpecialityJob job { get; set; }
        public List<Constant> listDegreeType { get; set; }

        public List<Constant> listQouta { get; set; }

        public List<EntityDDL> listType { get; set; }
        public List<EntityDDL> listSpeciality { get; set; }

        public List<EntityDDL> listInstitute { get; set; }

        public List<SpecialityJob> listSpecialityJob { get; set; }


        public SpecialityJobModelAdmin()
        {
            job = new SpecialityJob();
            listSpeciality = new List<EntityDDL>();
            listInstitute = new List<EntityDDL>();
            listDegreeType = new List<Constant>();
        }
    }

    public class SpecialityJobModelAdminManage : ModelBaseAdmin
    {

        public List<Constant> listDegreeType { get; set; }
        public List<EntityDDL> listSpeciality { get; set; }

        public List<EntityDDL> listHospital { get; set; }
        public SpecialityJobModelAdminManage()
        {
            listDegreeType = new List<Constant>();
            listHospital = new List<EntityDDL>();
        }
    }


    public class HospitalModelAdmin : ModelBaseAdmin
    {
        public int hospitalId { get; set; }
        public int typeId { get; set; }
        public Hospital hospital { get; set; }

        public List<EntityDDL> listHospital { get; set; }

        public List<Region> listDistrict { get; set; }

        public List<EntityDDL> listLevel { get; set; }
        public List<EntityDDL> listType { get; set; }
        public List<EntityDDL> listRegion { get; set; }
        public List<EntityDDL> listInstitute { get; set; }
        public List<Hospital> list { get; set; }

        public HospitalModelAdmin()
        {
            list = new List<Hospital>();
            listType = new List<EntityDDL>();
            listLevel = new List<EntityDDL>();
            hospital = new Hospital();
            listInstitute = new List<EntityDDL>();
            listDistrict = new List<Region>();
            listHospital = new List<EntityDDL>();
        }
    }

    public class VerficationCheckListModel : ModelBaseAdmin
    {
        public List<EntityDDL> listType { get; set; }
        public VerficationCheckList checkList { get; set; }
        public List<VerficationCheckList> listCheckList { get; set; }

        public VerficationCheckListModel()
        {
            listType = new List<EntityDDL>();
            checkList = new VerficationCheckList();
            listCheckList = new List<VerficationCheckList>();

        }
    }
    public class ProfileModelAdmin : ModelBaseAdmin
    {
        public DataTable jsonTable { get; set; }
        public int applicantId { get; set; }
        public int hospitalId { get; set; }

        public Applicant applicant { get; set; }
        public int countryId { get; set; }

        public ApplicantInfo applicantInfo { get; set; }

        public List<EntityDDL> listCountry { get; set; }
        public List<EntityDDL> listProvince { get; set; }
        public List<EntityDDL> listDistrict { get; set; }

        public List<EntityDDL> listDegree { get; set; }

        public List<EntityDDL> listProgram { get; set; }

        public List<semesterData> semesterDatas { get; set; }
        public List<EntityDDL> listSpeciality { get; set; }

        public List<EntityDDL> listHospital { get; set; }


        public string countryJson { get; set; }
        public ProfileModelAdmin()
        {
            listProvince = new List<EntityDDL>();
            listCountry = new List<EntityDDL>();
            listDegree = new List<EntityDDL>();
            listSpeciality = new List<EntityDDL>();
            listHospital = new List<EntityDDL>();
            listDistrict = new List<EntityDDL>();
            countryId = 132;
        }

    }


    public class ApplicantCurrentStatusModelAdmin : ModelBaseAdmin
    {
        public int applicantId { get; set; }
        public int hospitalId { get; set; }
        public int statusId { get; set; }
        public int hospitalIdTo { get; set; }



        public List<EntityDDL> listHospitalFrom { get; set; }
        public List<EntityDDL> listHospitalTo { get; set; }
        public List<EntityDDL> listStatus { get; set; }


        public ApplicantCurrentStatusModelAdmin()
        {
            listHospitalFrom = new List<EntityDDL>();
            listHospitalTo = new List<EntityDDL>();
            listStatus = new List<EntityDDL>();


        }

    }

    public class ApplicantStatusModel : ModelBaseAdmin
    {
        public int statusId { get; set; }
        public int statusTypeId { get; set; }
        public ApplicantEntity search { get; set; }
        public List<Applicant> listApplicant { get; set; }

        public List<EntityDDL> listStatus { get; set; }
        public DataTable applicantStatus { get; set; }

        public ApplicantStatusModel()
        {
            search = new ApplicantEntity();
            listApplicant = new List<Applicant>();
            listStatus = new List<EntityDDL>();
            applicantStatus = new DataTable();
        }

    }


    public class ExperienceModelAdmin : ModelBaseAdmin
    {
        public Applicant applicant { get; set; }
        public List<ApplicantExperience> listExperience { get; set; }
        public ApplicantExperienceParam experience { get; set; }
        public List<Marks> listMarks { get; set; }

        public List<Region> listProvince { get; set; }

        public ExperienceModelAdmin()
        {
            applicant = new Applicant();
            experience = new ApplicantExperienceParam();
            listExperience = new List<ApplicantExperience>();
            listMarks = new List<Marks>();
        }
    }


    public class ProofReadingAdminModel : ModelBaseAdmin
    {
        public int requestType { get; set; }
        public int applicantId { get; set; }

        public int statusId { get; set; }

        public SearchClass search { get; set; }
        public Applicant applicant { get; set; }

        public ApplicantInfo applicantInfo { get; set; }
        public ApplicantDegree degree { get; set; }

        public List<ApplicantDegreeMark> listDegreeMark { get; set; }

        public List<ApplicantCertificate> listCertificate { get; set; }
        public List<ApplicantHouseJob> listJob { get; set; }

        public List<ApplicantExperience> listExprince { get; set; }

        public List<ApplicantDistinction> listDistinction { get; set; }

        public List<ApplicantResearchPaper> listResearchPaper { get; set; }

        public List<ApplicantSpecility> listSpecility { get; set; }

        public List<EntityDDL> listType { get; set; }

        public List<Marks> listMarks { get; set; }

        public ApplicantVoucher voucher { get; set; }

        public AmendmentsApplicantNursing amendmentsData { get; set; }
        public ApplicantApprovalStatus statusApproval { get; set; }

        public ApplicantApprovalStatus statusAmendment { get; set; }

        public TraineeInfo trainee { get; set; }


        public ProofReadingAdminModel()
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
            search = new SearchClass();
            statusApproval = new ApplicantApprovalStatus();
            statusAmendment = new ApplicantApprovalStatus();
        }
    }

    public class HardshipAdminModel : ModelBaseAdmin
    {
        public int requestType { get; set; }
        public int applicantId { get; set; }

        public int statusId { get; set; }

        public SearchClass search { get; set; }
        public Applicant applicant { get; set; }

        public ApplicantInfo applicantInfo { get; set; }
        public ApplicantDegree degree { get; set; }
        public ApplicantJoined joined { get; set; }
        public List<SpecialityJob> listJob { get; set; }
        public HardshipAdminModel()
        {
            search = new SearchClass();
            applicant = new Applicant();
            applicantInfo = new ApplicantInfo();
            degree = new ApplicantDegree();
            joined = new ApplicantJoined();
            listJob = new List<SpecialityJob>();
        }
    }
    public class MeritApplicantAdminModel : ModelBaseAdmin
    {
        public int requestType { get; set; }
        public int applicantId { get; set; }



        public int statusId { get; set; }

        public SearchClass search { get; set; }
        public Applicant applicant { get; set; }

        public ApplicantInfo applicantInfo { get; set; }
        public ApplicantDegree degree { get; set; }

        public List<ApplicantDegreeMark> listDegreeMark { get; set; }

        public List<ApplicantCertificate> listCertificate { get; set; }

        public List<ApplicantExperience> listExprince { get; set; }

        public List<ApplicantDistinction> listDistinction { get; set; }

        public List<ApplicantResearchPaper> listResearchPaper { get; set; }

        public List<ApplicantSpecility> listSpecility { get; set; }

        public List<ApplicantSpecilityMerit> listSpecilityMerit { get; set; }

        public List<EntityDDL> listType { get; set; }

        public List<Marks> listMarks { get; set; }

        public ApplicantVoucher voucher { get; set; }

        public ApplicantApprovalStatus statusApproval { get; set; }

        public MeritApplicantAdminModel()
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
            search = new SearchClass();
            statusApproval = new ApplicantApprovalStatus();
            listSpecilityMerit = new List<ApplicantSpecilityMerit>();
        }
    }


    public class HardshipModel : ModelBaseAdmin
    {
        public int statusId { get; set; }

        public SearchClass search { get; set; }
        public Applicant applicant { get; set; }

        public ApplicantInfo applicantInfo { get; set; }
        public ApplicantJoined join { get; set; }

        public List<SpecialityJob> listSpecialitySeats{ get; set; }

        public HardshipModel()
        { 
        
        }


    }

    public class SmsEmailAdminModel : ModelBaseAdmin
    {
        public int requestType { get; set; }
        public int applicantId { get; set; }

        public SearchClass search { get; set; }
        public Applicant applicant { get; set; }


        public SmsEmailAdminModel()
        {
            applicant = new Applicant();
            search = new SearchClass();
        }
    }


    public class VerificationModel : ModelBaseAdmin
    {

        public VerificationEntity verification { get; set; }

        public VerificationModel()
        {
            verification = new VerificationEntity();
        }
    }



    public class VoucherAdminModel : ModelBaseAdmin
    {
        public VoucherSearch search { get; set; }
        public List<VoucherInfo> listVoucher { get; set; }
        public Applicant applicant { get; set; }

        public ApplicantInfo applicantInfo { get; set; }

        public ApplicantVoucher voucher { get; set; }

        public ApplicationStatus voucherStatus { get; set; }

        public int countryType { get; set; }


        public VoucherAdminModel()
        {
            search = new VoucherSearch();
            listVoucher = new List<VoucherInfo>();
            voucherStatus = new ApplicationStatus();
            applicant = new Applicant();
            applicantInfo = new ApplicantInfo();

        }
    }

    public class MenuSetupModel : ModelBaseAdmin
    {
        public Menu menu { get; set; }
        public List<EntityDDL> listType { get; set; }

        public List<EntityDDL> listApp { get; set; }
        public List<EntityDDL> listParent { get; set; }
        public MenuSetupModel()
        {
            menu = new Menu();
            listType = new List<EntityDDL>();
            listParent = new List<EntityDDL>();
            listApp = new List<EntityDDL>();

        }
    }

    public class MenuManageModel : ModelBaseAdmin
    {
        public int appId { get; set; }
        public int parentId { get; set; }
        public List<EntityDDL> listType { get; set; }
        public List<Menu> listParent { get; set; }

        public List<Menu> listMenu { get; set; }


        public MenuManageModel()
        {
            listType = new List<EntityDDL>();
            listMenu = new List<Menu>();
        }
    }

    public class MenuAccessModel : ModelBaseAdmin
    {
        public List<Menu> listMenus { get; set; }
        public List<User> listUser { get; set; }

        public List<EntityDDL> listType { get; set; }

        public MenuAccessModel()
        {
            listMenus = new List<Menu>();
            listUser = new List<User>();
            listType = new List<EntityDDL>();
        }
    }

    public class UserSetupModel : ModelBaseAdmin
    {
        public User user { get; set; }
        public List<EntityDDL> listType { get; set; }
        public List<EntityDDL> listDepartment { get; set; }
        public List<EntityDDL> listDesgnation { get; set; }
        public UserSetupModel()
        {
            user = new User();
            listType = new List<EntityDDL>();

            listDepartment = new List<EntityDDL>();
            listDesgnation = new List<EntityDDL>();

        }
    }

    public class UserManageModel : ModelBaseAdmin
    {
        public int typeId { get; set; }
        public List<EntityDDL> listType { get; set; }

        public List<User> listUser { get; set; }


        public UserManageModel()
        {
            listType = new List<EntityDDL>();
            listUser = new List<User>();
        }
    }

    public class FeedbackAdminModel : ModelBaseAdmin
    {
        public FeedbackSearch search { get; set; }
        public List<FeedBack> listFeedback { get; set; }

    }

    public class ApplicantJoiningAdminModel : ModelBaseAdmin
    {
        public int instituteId { get; set; }
        public int hospitalId { get; set; }
        public bool isExist { get; set; }
        public ApplicantJoined joinApplicant { get; set; }
        public List<EntityDDL> listType { get; set; }
        public List<EntityDDL> listInstitute { get; set; }
        public List<ApplicantJoined> listApplicant { get; set; }
        public Applicant applicant { get; set; }

        public ApplicantInfo applicantInfo { get; set; }

        public ApplicantJoiningAdminModel()
        {
            joinApplicant = new ApplicantJoined();
            listApplicant = new List<ApplicantJoined>();
            applicant = new Applicant();
            applicantInfo = new ApplicantInfo();
        }
    }


    public class ApplicantActionAdminModel : ModelBaseAdmin
    {
        public int applicantId { get; set; }
        public int typeId { get; set; }
        public string heading { get; set; }
        public string url { get; set; }
        public int instituteId { get; set; }
        public int hospitalId { get; set; }
        public bool isExist { get; set; }
        public ApplicantJoined joinApplicant { get; set; }
        public ApplicantAction action { get; set; }
        public List<EntityDDL> listType { get; set; }
        public List<EntityDDL> listInstitute { get; set; }
        public List<ApplicantJoined> listApplicant { get; set; }
        public Applicant applicant { get; set; }
        public ApplicantInfo applicantInfo { get; set; }
        public ApplicantActionAdminModel()
        {
            action = new ApplicantAction();
            listApplicant = new List<ApplicantJoined>();
            applicant = new Applicant();
            applicantInfo = new ApplicantInfo();
            joinApplicant = new ApplicantJoined();
        }
    }

    public class ResearchJournalModel : ModelBaseAdmin
    {
        public int researchJournalId { get; set; }
        public List<Constant> listType { get; set; }
        public ResearchJournalModel()
        {
            listType = new List<Constant>();
        }
    }

    public class RegionModelModel : ModelBaseAdmin
    {
        public List<EntityDDL> listParent { get; set; }
        public List<EntityDDL> listType { get; set; }
        public RegionModelModel()
        {
            listParent = new List<EntityDDL>();
            listType = new List<EntityDDL>();
        }
    }

    public class GrievanceAdminModel : ModelBaseAdmin
    {
        public string searchTxt { get; set; }
        public int grievanceId { get; set; }

        public int appearanceId { get; set; }
        public bool isEditAble { get; set; }
        public Grievance grievance { get; set; }

        public List<EntityDDL> listAppearanceType { get; set; }
        public List<EntityDDL> listGuardian { get; set; }

        public List<EntityDDL> listActionType { get; set; }
        public List<EntityDDL> listType { get; set; }

        public Applicant applicant { get; set; }

        public GrievanceSearch search { get; set; }

        public List<Grievance> listGrievance { get; set; }

        public Grievance appearance { get; set; }

        public GrievanceAdminModel()
        {
            grievance = new Grievance();
            listGrievance = new List<Grievance>();
            appearance = new Grievance();
            listAppearanceType = new List<EntityDDL>();
            listGuardian = new List<EntityDDL>();
            listActionType = new List<EntityDDL>();
            listType = new List<EntityDDL>();
        }

    }

    public class GrievanceActionModel : ModelBaseAdmin
    {
        public GrievanceAction action { get; set; }
        public List<EntityDDL> listRelation { get; set; }
       
        public Grievance grievance { get; set; }
        public Applicant applicant { get; set; }
        public List<EntityDDL> listType { get; set; }
        public GrievanceActionModel()
        {
            action = new GrievanceAction();
            listRelation = new List<EntityDDL>();
          
            grievance = new Grievance();
            applicant = new Applicant();
        }
    }

    public class GrievanceRemarkModel : ModelBaseAdmin
    {
        public GrievanceAction action { get; set; }
        public GrievanceRemark remark { get; set; }
        public List<EntityDDL> listRelation { get; set; }
        public List<EntityDDL> listType { get; set; }

        public Grievance grievance { get; set; }
        public Applicant applicant { get; set; }
        public GrievanceRemarkModel()
        {
            action = new GrievanceAction();
            remark = new GrievanceRemark();
            listRelation = new List<EntityDDL>();
            listType = new List<EntityDDL>();
            grievance = new Grievance();
            applicant = new Applicant();
        }
    }

    public class JournalModelAdmin : ModelBaseAdmin
    {
        public List<EntityDDL> listType { get; set; }
        public List<EntityDDL> listBatch { get; set; }
        public List<EntityDDL> listDiscipline { get; set; }
        public List<EntityDDL> listRegion { get; set; }
        public ResearchJournal journal { get; set; }

        public JournalModelAdmin()
        {
            journal = new ResearchJournal();
            listType = new List<EntityDDL>();
            listBatch = new List<EntityDDL>();
            listDiscipline = new List<EntityDDL>();
            listRegion = new List<EntityDDL>();
        }

    }

    public class ContactAdminModel : ModelBaseAdmin
    {
        public int typeId { get; set; }
        public int applicantId { get; set; }
        public List<Contact>  listQuestion { get; set; }
        public List<ContactReply> listAnswer { get; set; }

        public List<ContactDoc> listDocs { get; set; }

        public ContactReply reply { get; set; }
        public Contact contact { get; set; }
        public Applicant applicant { get; set; }

        public List<EntityDDL> listRelation { get; set; }
        public List<EntityDDL> listStatus { get; set; }

        public ContactAttendence attendence { get; set; }

        public ContactStatus status { get; set; }
        public ContactAdminModel()
        {
            contact = new Contact();
            listQuestion = new List<Contact>();
            listAnswer = new List<ContactReply>();
            reply = new ContactReply();
            listDocs = new List<ContactDoc>();
            listRelation = new List<EntityDDL>();
            attendence = new ContactAttendence();
            status = new ContactStatus();
            listStatus = new List<EntityDDL>();
        }
    }

    #region Reports

    public class ReportApplicantModel : ModelBaseAdmin
    {
        public int hospitalId { get; set; }
        public int attachStatusId { get; set; }

        public List<EntityDDL> listHospital { get; set; }
        public List<EntityDDL> listAttachStatus { get; set; }
        public List<EntityDDL> listType { get; set; }
        public List<EntityDDL> listCountryType { get; set; }
        public List<EntityDDL> listSpeciality { get; set; }
        public ReportApplicantModel()
        {
            listType = new List<EntityDDL>();
            listAttachStatus = new List<EntityDDL>();
        }
    }

    #endregion

}