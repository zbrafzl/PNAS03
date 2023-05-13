using Newtonsoft.Json;
using Prp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prp.Data
{
    public class BasedEntity
    {
    }

    public class LoginUser
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string emailId { get; set; }
    }

    public class Message
    {
        public int id { get; set; }
        public string value { get; set; }
        public string msg { get; set; }
        public int statusId { get; set; }
        public bool status { get; set; }
        public string message { get; set; }
        public Message()
        {
            status = false;
        }
    }

    public class CaptchaResponse
    {
        [JsonProperty("success")]
        public bool Success
        {
            get;
            set;
        }
        [JsonProperty("error-codes")]
        public List<string> ErrorMessage
        {
            get;
            set;
        }
    }



    public class ChangePassword
    {
        public int id { get; set; }
        public string passwordOld { get; set; }
        public string passwordNew { get; set; }
        public ChangePassword()
        {

        }
    }


    public class SMSEntity
    {
        public int statusId { get; set; }
        public string message { get; set; }
        public string contactNo { get; set; }
        public List<string> contactList { get; set; }
    }


    public class EmailEntity
    {
        public int applicantId { get; set; }
        public int statusId { get; set; }
        public int typeId { get; set; }

        public string type { get; set; }

        public string message { get; set; }

        public string title { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public string emailId { get; set; }

        public int detailId { get; set; }

        public bool status { get; set; }

        public string filePath { get; set; }
        public List<string> contactList { get; set; }

        public string emailFrom { get; set; }
        public string password { get; set; }
        public string port { get; set; }
    }


    public class EntityDDL
    {
        public int id { get; set; }
        public string name { get; set; }

        public string key { get; set; }
        public string value { get; set; }
        public int typeId { get; set; }

    }


    public class Search
    {
        public int inductionId { get; set; }
        public int parentId { get; set; }
        public int typeId { get; set; }
        public int sectionId { get; set; }
        public int reffId { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string reffIds { get; set; }

        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string search { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string section { get; set; }

        public Search()
        {
            reffIds = "";
            search = "";
            section = "";
        }
    }

    public class EntitySearch : Search
    {
        public int top { get; set; }
        public int pageNo { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string table { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public int userId { get; set; }
        public int isActive { get; set; }

        public EntitySearch()
        {
            reffIds = "";
            search = "";
            section = "";
        }
    }
    public class DDLSearch : Search
    {
        public int levelId { get; set; }
        public DDLSearch()
        {
        }
    }

    public class DDL
    {
        public int userId { get; set; }
        public int typeId { get; set; }
        public int reffId { get; set; }
        public string reffIds { get; set; }
        public string condition { get; set; }

        public DDL()
        {
            reffIds = "";
            condition = "";
        }
    }
    public class DDLInstitutes : DDL
    {
        public int hospitalId { get; set; }
        public int regionId { get; set; }
    }

    public class DDLHospitals : DDL
    {
        public int instituteId { get; set; }

    }



    public class DDLSpecialitys : DDL
    {
        public int instituteId { get; set; }

    }

    public class DDLInduction : DDL
    {

    }

    public class DDLConstants : DDL
    {
        public int parentId { get; set; }

        public DDLConstants()
        {

        }

    }

    public class DDLRegions : DDL
    {
        public int parentId { get; set; }

        public DDLRegions()
        {

        }

    }

    public class DDLJournal : DDL
    {
        public int parentId { get; set; }

        public DDLJournal()
        {

        }

    }

    public class DDLCheckList : DDL
    {
        public int parentId { get; set; }

        public DDLCheckList()
        {

        }

    }

    public class DDLMenu : DDL
    {
        public int parentId { get; set; }

        public DDLMenu()
        {

        }

    }


    public class ExportExcel
    {
        public string url { get; set; }
        public string fileName { get; set; }


    }

    public class IsExists
    {
        public int id { get; set; }
        public string search { get; set; }
        public string condition { get; set; }
    }

    public class CountEntity
    {
        public int userId { get; set; }
        public int typeId { get; set; }
        public int reffId { get; set; }
        public string reffIds { get; set; }
        public string condition { get; set; }
    }

    public class CountJobsEntity : CountEntity
    {
        public int hospitalId { get; set; }
        public int specialityId { get; set; }

    }

    public class CountApplicantStatusEntity : CountEntity
    {
        public int hospitalId { get; set; }
        public int specialityId { get; set; }

    }

    public class EntityCount
    {
        public int id { get; set; }
        public string name { get; set; }
        public int totalCount { get; set; }

        public int statusTypeId { get; set; }
        public string statusType { get; set; }
        public int statusId { get; set; }
        public string status { get; set; }

        public string color { get; set; }
    }


    public class SearchReport
    {

        public int top { get; set; }
        public int pageNum { get; set; }
        public int inductionId { get; set; }
        public string inductionIds { get; set; }
        public int phaseId { get; set; }
        public int userId { get; set; }
        public int adminId { get; set; }


        public int countryId { get; set; }
        public string countryIds { get; set; }

        public int provinceId { get; set; }
        public string provinceIds { get; set; }

        public int distirctId { get; set; }
        public string distirctIds { get; set; }

        public int degreeId { get; set; }
        public int typeId { get; set; }
        public string typeIds { get; set; }
        public int qoutaId { get; set; }
        public string qoutaIds { get; set; }
        public int instituteId { get; set; }
        public string instituteIds { get; set; }

        public int specialityId { get; set; }
        public string specialityIds { get; set; }

        public int hospitalId { get; set; }
        public string hospitalIds { get; set; }

        public int reffId { get; set; }
        public string reffIds { get; set; }


        public string condition { get; set; }
        public string search { get; set; }
        public string reportType { get; set; }
        public string fileName { get; set; }
        public string pageUrl { get; set; }

    }

    public class SearchEntity
    {
        public int top { get; set; }
        public int pageNum { get; set; }
        public string condition { get; set; }

        public string reportType { get; set; }

        public string pageUrl { get; set; }

        public int parentId { get; set; }

        public int inductionId { get; set; }
        public int phaseId { get; set; }

    }
    public class ApplicantEntity : SearchEntity
    {
        public int accountStatusId { get; set; }
        public int applicationStatusId { get; set; }
        public int facultyId { get; set; }


    }
    public class SmsEntity : SearchEntity
    {
        public int applicantId { get; set; }



    }

    public class EmployeeSearch
    {
        public int top { get; set; }
        public int pageNum { get; set; }

        public int hospitalId { get; set; }

        public int genderId { get; set; }
        public int degreeId { get; set; }
        public int designationId { get; set; }
        public int specialityId { get; set; }
        public int reportTypeId { get; set; }
        public int adminId { get; set; }
        public string search { get; set; }
    }

    public class AmendmentsApplicantNursing
    {
        public int amendmentId { get; set; }
        public int adminId { get; set; }
        public int applicantId { get; set; }
        public int isAdmin { get; set; }
        public int isApplicant { get; set; }
        public DateTime dated { get; set; }
        public bool txtNameValidty { get; set; }
        public bool txtFatherNameValidty { get; set; }
        public bool txtDobValidty { get; set; }
        public bool ddlDistrictValidty { get; set; }
        public bool ddlcnicSelectValidty { get; set; }
        public bool txtCNICValidty { get; set; }
        public bool txtCNICExpiryDateValidty { get; set; }
        public bool ddlDomicileDistrictValidty { get; set; }
        public bool txtAddressValidty { get; set; }
        public bool imgPicValidty { get; set; }
        public bool imgCnicFrontValidty { get; set; }
        public bool imgCnicBackValidty { get; set; }
        public bool imgDomicileFrontValidty { get; set; }
        public bool ddlMatricBoardValidty { get; set; }
        public bool txtRowDegree1MarksObtainValidty { get; set; }
        public bool txtRowDegree1MarksTotalValidty { get; set; }
        public bool txtDateOfPassingMatricValidty { get; set; }
        public bool imgRowDegree1Validty { get; set; }
        public bool ddlFABoardValidty { get; set; }
        public bool txtRowDegree2MarksObtainValidty { get; set; }
        public bool txtRowDegree2MarksTotalValidty { get; set; }
        public bool txtDateOfPassingInterValidty { get; set; }
        public bool imgRowDegree2Validty { get; set; }
        public bool txtBranchCodeValidty { get; set; }
        public bool txtSubmittedDateVoucherValidty { get; set; }
        public bool imgVoucherValidty { get; set; }
        public string txtNameRemarks { get; set; }
        public string txtFatherNameRemarks { get; set; }
        public string txtDobRemarks { get; set; }
        public string ddlDistrictRemarks { get; set; }
        public string ddlcnicSelectRemarks { get; set; }
        public string txtCNICRemarks { get; set; }
        public string txtCNICExpiryDateRemarks { get; set; }
        public string ddlDomicileDistrictRemarks { get; set; }
        public string txtAddressRemarks { get; set; }
        public string imgPicRemarks { get; set; }
        public string imgCnicFrontRemarks { get; set; }
        public string imgCnicBackRemarks { get; set; }
        public string imgDomicileFrontRemarks { get; set; }
        public string ddlMatricBoardRemarks { get; set; }
        public string txtRowDegree1MarksObtainRemarks { get; set; }
        public string txtRowDegree1MarksTotalRemarks { get; set; }
        public string txtDateOfPassingMatricRemarks { get; set; }
        public string imgRowDegree1Remarks { get; set; }
        public string ddlFABoardRemarks { get; set; }
        public string txtRowDegree2MarksObtainRemarks { get; set; }
        public string txtRowDegree2MarksTotalRemarks { get; set; }
        public string txtDateOfPassingInterRemarks { get; set; }
        public string imgRowDegree2Remarks { get; set; }
        public string txtBranchCodeRemarks { get; set; }
        public string txtSubmittedDateVoucherRemarks { get; set; }
        public string imgVoucherRemarks { get; set; }
        public int approvalStatus { get; set; }
        public string approvalRemarks { get; set; }
        public int approvedBy { get; set; }
        public bool ddlMaritalStatusValidity { get; set; }
        public string ddlMaritalStatusRemarks { get; set; }
        public int amendmentStatus { get; set; }
    }

    public class VerificationEntity
    {
        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public int top { get; set; }
        public int pageNum { get; set; }
        public string condition { get; set; }

        public string pageUrl { get; set; }
        public string dateStr { get; set; }
        public DateTime dated { get; set; }
        public int statusId { get; set; }

        public int applicantId { get; set; }
        public int approvalStatusTypeId { get; set; }
        public int approvalStatusId { get; set; }
        public string comments { get; set; }
        public int adminId { get; set; }
    }

    public class AmmendmentsEntitymodel
    {
        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public int top { get; set; }
        public int userId { get; set; }
        public string dOB { get; set; }
        public int applicantId { get; set; }
        public string cnicNo { get; set; }
        public string cnicExpiryDate { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string imgCnicFront { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string imgCnicBack { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]

        public string imgDomicile { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]

        public int domicileProvinceId { get; set; }
        public int domicileDistrictId { get; set; }
        [DisplayFormat(ConvertEmptyStringToNull = false)]
        public string address { get; set; }
        public string fatherName { get; set; }
        public int matricMarksObtain { get; set; }
        public int matricTotalMarks { get; set; }
        public string matricBoard { get; set; }
        public string imgMatricDegree { get; set; }

        public int fscObtainedMarks { get; set; }
        public int fscTotalMarks { get; set; }
        public string fscBoard { get; set; }
        public string imgFscDegree { get; set; }

        public string imgVoucher { get; set; }

        public string dated { get; set; }
        public int adminId { get; set; }
    }

    public class VoucherSearch
    {

        public int countryTypeId { get; set; }
        public int applicantId { get; set; }
        public string applicantNo { get; set; }
        public string cnicNo { get; set; }
        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public string dateStart { get; set; }
        public string dateEnd { get; set; }
        public int applicationStatusId { get; set; }
        public int voucherStatusId { get; set; }

        public int top { get; set; }
        public int pageNum { get; set; }
        public string condition { get; set; }

        public string reportType { get; set; }

        public string pageUrl { get; set; }
        public string search { get; set; }
        public int statusId { get; set; }
        public int statusIdBank { get; set; }

    }

    public class ApplicantSearch
    {
        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public int top { get; set; }
        public int pageNum { get; set; }
        public string search { get; set; }
        public string reportType { get; set; }

        public string pageUrl { get; set; }
        public int statusTypeId { get; set; }
        public int statusId { get; set; }
        public int userId { get; set; }
        public int hospitalId { get; set; }

        public ApplicantSearch()
        {
            statusTypeId = 0; statusId = 0;
        }
    }

    public class SmsSearch
    {
        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public int top { get; set; }
        public int pageNum { get; set; }
        public string search { get; set; }
        public string reportType { get; set; }

        public string pageUrl { get; set; }
        public int applicantId { get; set; }

        public SmsSearch()
        {
            applicantId = 0;
        }
    }


    public class ResearchJournalSearch
    {
        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public int top { get; set; }
        public int pageNum { get; set; }
        public string search { get; set; }
        public string reportType { get; set; }

        public string pageUrl { get; set; }
        public int regionId { get; set; }
        public int displineId { get; set; }
        public int isActive { get; set; }

        public int researchJournalId { get; set; }

        public ResearchJournalSearch()
        {
            regionId = 0; displineId = 0;
        }
    }

    public class HospitalSearch
    {
        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public int top { get; set; }
        public int pageNum { get; set; }
        public string search { get; set; }
        public string reportType { get; set; }

        public string pageUrl { get; set; }
        public int regionId { get; set; }
        public int levelId { get; set; }
        public int typeId { get; set; }
        public int isActive { get; set; }

        public HospitalSearch()
        {
            regionId = 0; levelId = 0; typeId = 0;
        }
    }


    public class RegionSearch
    {
        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public int top { get; set; }
        public int pageNum { get; set; }
        public string search { get; set; }
        public string reportType { get; set; }

        public string pageUrl { get; set; }
        public int regionId { get; set; }
        public int typeId { get; set; }
        public int isActive { get; set; }

        public RegionSearch()
        {
            regionId = 0; typeId = 0;
        }
    }

    public class SpecialityJobSearch
    {
        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public int top { get; set; }
        public int pageNum { get; set; }
        public string search { get; set; }
        public string reportType { get; set; }

        public string pageUrl { get; set; }
        public int statusTypeId { get; set; }
        public int specialityId { get; set; }
        public int quotaId { get; set; }
        public int typeId { get; set; }
        public int hospitalId { get; set; }
        public int isActive { get; set; }
        public int isExport { get; set; }
        public int userId { get; set; }

        public int attachStatusId { get; set; }

        public SpecialityJobSearch()
        {

        }
    }

    public class ApplicantSearchEntity
    {
        public int applicantId { get; set; }
        public string applicantNo { get; set; }

    }

    public class FeedbackSearch
    {
        public int top { get; set; }
        public int pageNum { get; set; }
        public string dateStart { get; set; }
        public string dateEnd { get; set; }
        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public string search { get; set; }
    }

    public class GrievanceSearch
    {
        public int top { get; set; }
        public int pageNum { get; set; }
        public string dateStart { get; set; }
        public string dateEnd { get; set; }
        public DateTime startDate { get; set; }

        public int typeId { get; set; }

        public DateTime endDate { get; set; }

        public string search { get; set; }
    }

    public class JoiningApplicantSearch
    {
        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public int top { get; set; }
        public int pageNum { get; set; }
        public string search { get; set; }
        public string reportType { get; set; }

        public string pageUrl { get; set; }

        public int isActive { get; set; }

        public int typeId { get; set; }
        public int userId { get; set; }

        public int instituteId { get; set; }

        public JoiningApplicantSearch()
        {

        }
    }


    public class SearchClass
    {
        public string key { get; set; }
        public string value { get; set; }
        public string prop1 { get; set; }
        public string prop2 { get; set; }
        public string prop3 { get; set; }
        public string prop4 { get; set; }

    }

    public class TraineeSearch
    {
        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public int top { get; set; }
        public int pageNum { get; set; }
        public string search { get; set; }
        public int hospitalId { get; set; }
        public int instituteId { get; set; }
        public int employeeId { get; set; }
    }


    public class Autocomplete
    {
        public string label { get; set; }
        public string val { get; set; }
    }
}
