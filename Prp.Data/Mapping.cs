using AutoMapper;
using Prp.Data.DAL;
using Prp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prp.Data
{
    public class Mapping
    {
    }

    #region 1. Constant

    public static class MapConstant
    {
        public static tblConstant ToTable(Constant obj)
        {
            Mapper.CreateMap<Constant, tblConstant>();
            return Mapper.Map<Constant, tblConstant>(obj);

        }

        public static Constant ToEntity(tblConstant obj)
        {
            Mapper.CreateMap<tblConstant, Constant>();
            return Mapper.Map<tblConstant, Constant>(obj);

        }

        public static List<Constant> ToEntityList(List<tblConstant> listConfig)
        {
            List<Constant> list = new List<Constant>();
            foreach (var item in listConfig)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Constant ToEntity(tvwConstant obj)
        {
            Mapper.CreateMap<tvwConstant, Constant>();
            return Mapper.Map<tvwConstant, Constant>(obj);

        }
        public static List<Constant> ToEntityList(List<tvwConstant> listt)
        {
            List<Constant> list = new List<Constant>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static EntityDDL ToEntity(spConstantForDDL_Result obj)
        {
            Mapper.CreateMap<spConstantForDDL_Result, EntityDDL>();
            return Mapper.Map<spConstantForDDL_Result, EntityDDL>(obj);

        }
        public static List<EntityDDL> ToEntityList(List<spConstantForDDL_Result> listt)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


    }

    #endregion

    #region 1. Constant

    public static class MapDiscipline
    {
        public static tblDiscipline ToTable(Discipline obj)
        {
            Mapper.CreateMap<Discipline, tblDiscipline>();
            return Mapper.Map<Discipline, tblDiscipline>(obj);

        }

        public static Discipline ToEntity(tblDiscipline obj)
        {
            Mapper.CreateMap<tblDiscipline, Discipline>();
            return Mapper.Map<tblDiscipline, Discipline>(obj);

        }

        public static List<Discipline> ToEntityList(List<tblDiscipline> listt)
        {
            List<Discipline> list = new List<Discipline>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

    }

    #endregion

    //

    #region 1. Ticker

    public static class MapTicker
    {
        public static tblTicker ToTable(Ticker obj)
        {
            Mapper.CreateMap<Ticker, tblTicker>();
            return Mapper.Map<Ticker, tblTicker>(obj);

        }

        public static Ticker ToEntity(tblTicker obj)
        {
            Mapper.CreateMap<tblTicker, Ticker>();
            return Mapper.Map<tblTicker, Ticker>(obj);

        }

        public static List<Ticker> ToEntityList(List<tblTicker> listt)
        {
            List<Ticker> list = new List<Ticker>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Ticker ToEntity(tvwTicker obj)
        {
            Mapper.CreateMap<tvwTicker, Ticker>();
            return Mapper.Map<tvwTicker, Ticker>(obj);

        }

        public static List<Ticker> ToEntityList(List<tvwTicker> listt)
        {
            List<Ticker> list = new List<Ticker>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

    }

    #endregion

    #region 1.1 Common

    public static class MapCommon
    {

        public static EntityCount ToEntity(spJobGetCount_Result obj)
        {
            Mapper.CreateMap<spJobGetCount_Result, EntityCount>();
            return Mapper.Map<spJobGetCount_Result, EntityCount>(obj);
        }

        public static List<EntityCount> ToEntityList(List<spJobGetCount_Result> listt)
        {
            List<EntityCount> list = new List<EntityCount>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static EntityCount ToEntity(spInstituteHospitalGetDashBoardData_Result obj)
        {
            Mapper.CreateMap<spInstituteHospitalGetDashBoardData_Result, EntityCount>();
            return Mapper.Map<spInstituteHospitalGetDashBoardData_Result, EntityCount>(obj);
        }

        public static List<EntityCount> ToEntityList(List<spInstituteHospitalGetDashBoardData_Result> listt)
        {
            List<EntityCount> list = new List<EntityCount>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static EntityCount ToEntity(spApplicantStatusGetAll_Result obj)
        {
            Mapper.CreateMap<spApplicantStatusGetAll_Result, EntityCount>();
            return Mapper.Map<spApplicantStatusGetAll_Result, EntityCount>(obj);
        }

        public static List<EntityCount> ToEntityList(List<spApplicantStatusGetAll_Result> listt)
        {
            List<EntityCount> list = new List<EntityCount>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }



        //public static EntityCount ToEntity(spApplicantApprovalStatusCount_Result obj)
        //{
        //    Mapper.CreateMap<spApplicantApprovalStatusCount_Result, EntityCount>();
        //    return Mapper.Map<spApplicantApprovalStatusCount_Result, EntityCount>(obj);
        //}

        //public static List<EntityCount> ToEntityList(List<spApplicantApprovalStatusCount_Result> listt)
        //{
        //    List<EntityCount> list = new List<EntityCount>();
        //    foreach (var item in listt)
        //    {
        //        list.Add(ToEntity(item));
        //    }
        //    return list;
        //}


        public static ApplicantInfoAPI ToEntity(spApplicantGetByApplicantNo_Result obj)
        {
            Mapper.CreateMap<spApplicantGetByApplicantNo_Result, ApplicantInfoAPI>();
            return Mapper.Map<spApplicantGetByApplicantNo_Result, ApplicantInfoAPI>(obj);
        }


    }

    #endregion


    #region 2. Induction

    public static class MapInduction
    {
        public static tblInduction ToTable(Induction obj)
        {
            Mapper.CreateMap<Induction, tblInduction>();
            return Mapper.Map<Induction, tblInduction>(obj);

        }

        public static Induction ToEntity(tblInduction obj)
        {
            Mapper.CreateMap<tblInduction, Induction>();
            return Mapper.Map<tblInduction, Induction>(obj);

        }

        public static List<Induction> ToEntityList(List<tblInduction> listConfig)
        {
            List<Induction> list = new List<Induction>();
            foreach (var item in listConfig)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static EntityDDL ToEntity(spInductionForDDL_Result obj)
        {
            Mapper.CreateMap<spInductionForDDL_Result, EntityDDL>();
            return Mapper.Map<spInductionForDDL_Result, EntityDDL>(obj);

        }
        public static List<EntityDDL> ToEntityList(List<spInductionForDDL_Result> listt)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }
    }

    #endregion

    #region Users

    public static class MapUser
    {
        public static tblUser ToTable(User objt)
        {
            Mapper.CreateMap<User, tblUser>();
            return Mapper.Map<User, tblUser>(objt);
        }

        public static User ToEntity(spAdminLogin_Result objt)
        {
            Mapper.CreateMap<spAdminLogin_Result, User>();
            return Mapper.Map<spAdminLogin_Result, User>(objt);
        }

        public static Message ToEntity(spUserAddUpdate_Result objt)
        {
            Mapper.CreateMap<spUserAddUpdate_Result, Message>();
            return Mapper.Map<spUserAddUpdate_Result, Message>(objt);
        }

        public static Message ToEntity(spUserChangePassword_Result objt)
        {
            Mapper.CreateMap<spUserChangePassword_Result, Message>();
            return Mapper.Map<spUserChangePassword_Result, Message>(objt);
        }

        public static User ToEntity(tblUser objt)
        {
            Mapper.CreateMap<tblUser, User>();
            return Mapper.Map<tblUser, User>(objt);
        }

        public static List<User> ToEntityList(List<tblUser> listt)
        {
            List<User> list = new List<User>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static User ToEntity(tvwUser objt)
        {
            Mapper.CreateMap<tvwUser, User>();
            return Mapper.Map<tvwUser, User>(objt);
        }

        public static List<User> ToEntityList(List<tvwUser> listt)
        {
            List<User> list = new List<User>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static User ToEntity(spUserGetByType_Result objt)
        {
            Mapper.CreateMap<spUserGetByType_Result, User>();
            return Mapper.Map<spUserGetByType_Result, User>(objt);
        }

        public static List<User> ToEntityList(List<spUserGetByType_Result> listt)
        {
            List<User> list = new List<User>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


    }

    #endregion

    #region Department

    public static class MapDepartment
    {
        public static tblDepartment ToTable(Department obj)
        {
            Mapper.CreateMap<Department, tblDepartment>();
            return Mapper.Map<Department, tblDepartment>(obj);

        }

        public static Department ToEntity(tblDepartment obj)
        {
            Mapper.CreateMap<tblDepartment, Department>();
            return Mapper.Map<tblDepartment, Department>(obj);

        }

        public static List<Department> ToEntityList(List<tblDepartment> listConfig)
        {
            List<Department> list = new List<Department>();
            foreach (var item in listConfig)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Department ToEntity(tvwDepartment obj)
        {
            Mapper.CreateMap<tvwDepartment, Department>();
            return Mapper.Map<tvwDepartment, Department>(obj);

        }
        public static List<Department> ToEntityList(List<tvwDepartment> listt)
        {
            List<Department> list = new List<Department>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

      


    }
    #endregion

    #region Menu

    public static class MapMenu
    {
        public static tblMenu ToTable(Menu objt)
        {
            Mapper.CreateMap<Menu, tblMenu>();
            return Mapper.Map<Menu, tblMenu>(objt);
        }

        public static Menu ToEntity(spAdminLogin_Result objt)
        {
            Mapper.CreateMap<spAdminLogin_Result, Menu>();
            return Mapper.Map<spAdminLogin_Result, Menu>(objt);
        }

        public static Menu ToEntity(tblMenu objt)
        {
            Mapper.CreateMap<tblMenu, Menu>();
            return Mapper.Map<tblMenu, Menu>(objt);
        }

        public static List<Menu> ToEntityList(List<tblMenu> listt)
        {
            List<Menu> list = new List<Menu>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Menu ToEntity(tvwMenu objt)
        {
            Mapper.CreateMap<tvwMenu, Menu>();
            return Mapper.Map<tvwMenu, Menu>(objt);
        }

        public static List<Menu> ToEntityList(List<tvwMenu> listt)
        {
            List<Menu> list = new List<Menu>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static EntityDDL ToEntity(spMenuForDDL_Result objt)
        {
            Mapper.CreateMap<spMenuForDDL_Result, EntityDDL>();
            return Mapper.Map<spMenuForDDL_Result, EntityDDL>(objt);
        }

        public static List<EntityDDL> ToEntityList(List<spMenuForDDL_Result> listt)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static Menu ToEntity(spMenuGetAllForUserType_Result objt)
        {
            Mapper.CreateMap<spMenuGetAllForUserType_Result, Menu>();
            return Mapper.Map<spMenuGetAllForUserType_Result, Menu>(objt);
        }

        public static List<Menu> ToEntityList(List<spMenuGetAllForUserType_Result> listt)
        {
            List<Menu> list = new List<Menu>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Menu ToEntity(spMenuGetAllForUser_Result objt)
        {
            Mapper.CreateMap<spMenuGetAllForUser_Result, Menu>();
            return Mapper.Map<spMenuGetAllForUser_Result, Menu>(objt);
        }

        public static List<Menu> ToEntityList(List<spMenuGetAllForUser_Result> listt)
        {
            List<Menu> list = new List<Menu>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static Menu ToEntity(spMenuGetByUserId_Result objt)
        {
            Mapper.CreateMap<spMenuGetByUserId_Result, Menu>();
            return Mapper.Map<spMenuGetByUserId_Result, Menu>(objt);
        }

        public static List<Menu> ToEntityList(List<spMenuGetByUserId_Result> listt)
        {
            List<Menu> list = new List<Menu>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Menu ToEntity(spMenuSearch_Result objt)
        {
            Mapper.CreateMap<spMenuSearch_Result, Menu>();
            return Mapper.Map<spMenuSearch_Result, Menu>(objt);
        }

        public static List<Menu> ToEntityList(List<spMenuSearch_Result> listt)
        {
            List<Menu> list = new List<Menu>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


    }

    #endregion

    #region Email

    public static class MapEmail
    {
        public static tblEmailTemplate ToTable(EmailTemplate obj)
        {
            Mapper.CreateMap<EmailTemplate, tblEmailTemplate>();
            return Mapper.Map<EmailTemplate, tblEmailTemplate>(obj);

        }

        public static EmailTemplate ToEntity(tblEmailTemplate obj)
        {
            Mapper.CreateMap<tblEmailTemplate, EmailTemplate>();
            return Mapper.Map<tblEmailTemplate, EmailTemplate>(obj);

        }

        public static List<EmailTemplate> ToEntityList(List<tblEmailTemplate> listt)
        {
            List<EmailTemplate> list = new List<EmailTemplate>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static EmailTemplate ToEntity(tvwEmailTemplate obj)
        {
            Mapper.CreateMap<tvwEmailTemplate, EmailTemplate>();
            return Mapper.Map<tvwEmailTemplate, EmailTemplate>(obj);

        }

        public static List<EmailTemplate> ToEntityList(List<tvwEmailTemplate> listt)
        {
            List<EmailTemplate> list = new List<EmailTemplate>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static EmailTemplate ToEntity(spEmailTemplateSearch_Result obj)
        {
            Mapper.CreateMap<spEmailTemplateSearch_Result, EmailTemplate>();
            return Mapper.Map<spEmailTemplateSearch_Result, EmailTemplate>(obj);

        }

        public static List<EmailTemplate> ToEntityList(List<spEmailTemplateSearch_Result> listt)
        {
            List<EmailTemplate> list = new List<EmailTemplate>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static EmailTemplate ToEntity(spEmailTemplateByTypeId_Result obj)
        {
            Mapper.CreateMap<spEmailTemplateByTypeId_Result, EmailTemplate>();
            return Mapper.Map<spEmailTemplateByTypeId_Result, EmailTemplate>(obj);
        }

        public static EmailProcess ToEntity(spEmailProcessGetByApplicantAndType_Result obj)
        {
            Mapper.CreateMap<spEmailProcessGetByApplicantAndType_Result, EmailProcess>();
            return Mapper.Map<spEmailProcessGetByApplicantAndType_Result, EmailProcess>(obj);
        }


        public static EmailProcess ToEntity(spEmailProcessGetByType_Result obj)
        {
            Mapper.CreateMap<spEmailProcessGetByType_Result, EmailProcess>();
            return Mapper.Map<spEmailProcessGetByType_Result, EmailProcess>(obj);

        }

        public static List<EmailProcess> ToEntityList(List<spEmailProcessGetByType_Result> listt)
        {
            List<EmailProcess> list = new List<EmailProcess>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static EmailProcess ToEntity(spEmailProcessGetAllRemaninig_Result obj)
        {
            Mapper.CreateMap<spEmailProcessGetAllRemaninig_Result, EmailProcess>();
            return Mapper.Map<spEmailProcessGetAllRemaninig_Result, EmailProcess>(obj);

        }

        public static List<EmailProcess> ToEntityList(List<spEmailProcessGetAllRemaninig_Result> listt)
        {
            List<EmailProcess> list = new List<EmailProcess>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


    }
    #endregion

    #region SMS

    public static class MapSMS
    {
        public static tblSM ToTable(SMS obj)
        {
            Mapper.CreateMap<SMS, tblSM>();
            return Mapper.Map<SMS, tblSM>(obj);

        }

        public static SMS ToEntity(tblSM obj)
        {
            Mapper.CreateMap<tblSM, SMS>();
            return Mapper.Map<tblSM, SMS>(obj);

        }

        public static List<SMS> ToEntityList(List<tblSM> listt)
        {
            List<SMS> list = new List<SMS>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static SMS ToEntity(tvwSM obj)
        {
            Mapper.CreateMap<tvwSM, SMS>();
            return Mapper.Map<tvwSM, SMS>(obj);

        }

        public static List<SMS> ToEntityList(List<tvwSM> listt)
        {
            List<SMS> list = new List<SMS>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static SMS ToEntity(spSMSGetByTypeForApplicant_Result obj)
        {
            Mapper.CreateMap<spSMSGetByTypeForApplicant_Result, SMS>();
            return Mapper.Map<spSMSGetByTypeForApplicant_Result, SMS>(obj);

        }


        public static SmsProcess ToEntity(spSMSProcessGetByType_Result obj)
        {
            Mapper.CreateMap<spSMSProcessGetByType_Result, SmsProcess>();
            return Mapper.Map<spSMSProcessGetByType_Result, SmsProcess>(obj);

        }

        public static List<SmsProcess> ToEntityList(List<spSMSProcessGetByType_Result> listt)
        {
            List<SmsProcess> list = new List<SmsProcess>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        //

        public static SmsProcess ToEntity(spSMSProcessGetBySmsId_Result obj)
        {
            Mapper.CreateMap<spSMSProcessGetBySmsId_Result, SmsProcess>();
            return Mapper.Map<spSMSProcessGetBySmsId_Result, SmsProcess>(obj);

        }

        public static List<SmsProcess> ToEntityList(List<spSMSProcessGetBySmsId_Result> listt)
        {
            List<SmsProcess> list = new List<SmsProcess>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static SmsProcess ToEntity(spSMSProcessGetRemaning_Result obj)
        {
            Mapper.CreateMap<spSMSProcessGetRemaning_Result, SmsProcess>();
            return Mapper.Map<spSMSProcessGetRemaning_Result, SmsProcess>(obj);

        }

        public static List<SmsProcess> ToEntityList(List<spSMSProcessGetRemaning_Result> listt)
        {
            List<SmsProcess> list = new List<SmsProcess>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

    }
    #endregion

    #region Employee
    public static class MapEmployee
    {
        public static tblEmployee ToTable(Employee obj)
        {
            Mapper.CreateMap<Employee, tblEmployee>();
            return Mapper.Map<Employee, tblEmployee>(obj);

        }

        public static Employee ToEntity(tblEmployee obj)
        {
            Mapper.CreateMap<tblEmployee, Employee>();
            return Mapper.Map<tblEmployee, Employee>(obj);

        }

        public static List<Employee> ToEntityList(List<tblEmployee> listt)
        {
            List<Employee> list = new List<Employee>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Employee ToEntity(tvwEmployee obj)
        {
            Mapper.CreateMap<tvwEmployee, Employee>();
            return Mapper.Map<tvwEmployee, Employee>(obj);

        }

        public static List<Employee> ToEntityList(List<tvwEmployee> listt)
        {
            List<Employee> list = new List<Employee>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Employee ToEntity(spEmployeeSearch_Result obj)
        {
            Mapper.CreateMap<spEmployeeSearch_Result, Employee>();
            return Mapper.Map<spEmployeeSearch_Result, Employee>(obj);

        }

        public static List<Employee> ToEntityList(List<spEmployeeSearch_Result> listt)
        {
            List<Employee> list = new List<Employee>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        


        public static EmployeeExperience ToEntity(tblEmployeeExperience obj)
        {
            Mapper.CreateMap<tblEmployeeExperience, EmployeeExperience>();
            return Mapper.Map<tblEmployeeExperience, EmployeeExperience>(obj);

        }

        public static EmployeeExperience ToEntity(spEmployeeExperienceGet_Result obj)
        {
            Mapper.CreateMap<spEmployeeExperienceGet_Result, EmployeeExperience>();
            return Mapper.Map<spEmployeeExperienceGet_Result, EmployeeExperience>(obj);

        }
        public static List<EmployeeExperience> ToEntityList(List<spEmployeeExperienceGet_Result> listt)
        {
            List<EmployeeExperience> list = new List<EmployeeExperience>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static EmployeeSpeciality ToEntity(tblEmployeeSpeciality obj)
        {
            Mapper.CreateMap<tblEmployeeSpeciality, EmployeeSpeciality>();
            return Mapper.Map<tblEmployeeSpeciality, EmployeeSpeciality>(obj);

        }

        public static List<EmployeeSpeciality> ToEntityList(List<tblEmployeeSpeciality> listt)
        {
            List<EmployeeSpeciality> list = new List<EmployeeSpeciality>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static EmployeeSpeciality ToEntity(tvwEmployeeSpeciality obj)
        {
            Mapper.CreateMap<tvwEmployeeSpeciality, EmployeeSpeciality>();
            return Mapper.Map<tvwEmployeeSpeciality, EmployeeSpeciality>(obj);

        }

        public static List<EmployeeSpeciality> ToEntityList(List<tvwEmployeeSpeciality> listt)
        {
            List<EmployeeSpeciality> list = new List<EmployeeSpeciality>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

    }
    #endregion

    #region Applicant

    public static class MapApplicant
    {
        public static tblApplicant ToTable(Applicant objt)
        {
            Mapper.CreateMap<Applicant, tblApplicant>();
            return Mapper.Map<Applicant, tblApplicant>(objt);
        }

        public static Applicant ToEntity(tblApplicant objt)
        {
            Mapper.CreateMap<tblApplicant, Applicant>();
            return Mapper.Map<tblApplicant, Applicant>(objt);
        }



        //public static ApplicantStatus ToEntity(spApplicantStatusGet_Result objt)
        //{
        //    Mapper.CreateMap<spApplicantStatusGet_Result, ApplicantStatus>();
        //    return Mapper.Map<spApplicantStatusGet_Result, ApplicantStatus>(objt);
        //}

        //public static Message ToEntity(spApplicantRegister_Result objt)
        //{
        //    Mapper.CreateMap<spApplicantRegister_Result, Message>();
        //    return Mapper.Map<spApplicantRegister_Result, Message>(objt);
        //}

        public static Message ToEntity(spApplicantUpdate_Result objt)
        {
            Mapper.CreateMap<spApplicantUpdate_Result, Message>();
            return Mapper.Map<spApplicantUpdate_Result, Message>(objt);
        }

        public static Message ToEntity(spApplicantGetBySearch_Result objt)
        {
            Mapper.CreateMap<spApplicantGetBySearch_Result, Message>();
            return Mapper.Map<spApplicantGetBySearch_Result, Message>(objt);
        }


        public static Message ToEntity(spApplicantChangePassword_Result objt)
        {
            Mapper.CreateMap<spApplicantChangePassword_Result, Message>();
            return Mapper.Map<spApplicantChangePassword_Result, Message>(objt);
        }

        public static Message ToEntity(spApplicantSpecilityCheckPreferenceNo_Result objt)
        {
            Mapper.CreateMap<spApplicantSpecilityCheckPreferenceNo_Result, Message>();
            return Mapper.Map<spApplicantSpecilityCheckPreferenceNo_Result, Message>(objt);
        }

        public static Message ToEntity(spApplicantSpecilityAddUpdate_Result objt)
        {
            Mapper.CreateMap<spApplicantSpecilityAddUpdate_Result, Message>();
            return Mapper.Map<spApplicantSpecilityAddUpdate_Result, Message>(objt);
        }

        public static Applicant ToEntity(tvwApplicant objt)
        {
            Mapper.CreateMap<tvwApplicant, Applicant>();
            return Mapper.Map<tvwApplicant, Applicant>(objt);
        }

        public static List<Applicant> ToEntityList(List<tvwApplicant> listt)
        {
            List<Applicant> list = new List<Applicant>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Applicant ToEntity(tvwApplicantNew objt)
        {
            Mapper.CreateMap<tvwApplicantNew, Applicant>();
            return Mapper.Map<tvwApplicantNew, Applicant>(objt);
        }

        public static List<Applicant> ToEntityList(List<tvwApplicantNew> listt)
        {
            List<Applicant> list = new List<Applicant>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static List<Applicant> ToEntityList(List<tblApplicant> listt)
        {
            List<Applicant> list = new List<Applicant>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        //public static Applicant ToEntity(spApplicantLogin_Result objt)
        //{
        //    Mapper.CreateMap<spApplicantLogin_Result, Applicant>();
        //    return Mapper.Map<spApplicantLogin_Result, Applicant>(objt);
        //}

        //public static Applicant ToEntity(spApplicantLoginByPhf_Result objt)
        //{
        //    Mapper.CreateMap<spApplicantLoginByPhf_Result, Applicant>();
        //    return Mapper.Map<spApplicantLoginByPhf_Result, Applicant>(objt);
        //}

        public static Applicant ToEntity(spApplicantGetList_Result objt)
        {
            Mapper.CreateMap<spApplicantGetList_Result, Applicant>();
            return Mapper.Map<spApplicantGetList_Result, Applicant>(objt);
        }

        public static List<Applicant> ToEntityList(List<spApplicantGetList_Result> listt)
        {
            List<Applicant> list = new List<Applicant>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

    }

    public static class MapApplicantInfo
    {
        public static tblApplicantInfo ToTable(ApplicantInfo objt)
        {
            Mapper.CreateMap<ApplicantInfo, tblApplicantInfo>();
            return Mapper.Map<ApplicantInfo, tblApplicantInfo>(objt);
        }

        public static ApplicantInfo ToEntity(tblApplicantInfo objt)
        {
            Mapper.CreateMap<tblApplicantInfo, ApplicantInfo>();
            return Mapper.Map<tblApplicantInfo, ApplicantInfo>(objt);
        }

        public static ApplicantInfo ToEntity(tvwApplicantInfo objt)
        {
            Mapper.CreateMap<tvwApplicantInfo, ApplicantInfo>();
            return Mapper.Map<tvwApplicantInfo, ApplicantInfo>(objt);
        }

        public static ApplicantInfoDualNational ToEntity(tvwApplicantInfoDualNational objt)
        {
            Mapper.CreateMap<tvwApplicantInfoDualNational, ApplicantInfoDualNational>();
            return Mapper.Map<tvwApplicantInfoDualNational, ApplicantInfoDualNational>(objt);
        }

        #region Privious Induction Data


        public static ApplicantInfo ToEntity(tvwfApplicantInfo objt)
        {
            Mapper.CreateMap<tvwfApplicantInfo, ApplicantInfo>();
            return Mapper.Map<tvwfApplicantInfo, ApplicantInfo>(objt);
        }

       

        #endregion

    }

    public static class MapApplicantDegree
    {
        public static tblApplicantDegree ToTable(ApplicantDegree objt)
        {
            Mapper.CreateMap<ApplicantDegree, tblApplicantDegree>();
            return Mapper.Map<ApplicantDegree, tblApplicantDegree>(objt);
        }

        public static ApplicantDegree ToEntity(tblApplicantDegree objt)
        {
            Mapper.CreateMap<tblApplicantDegree, ApplicantDegree>();
            return Mapper.Map<tblApplicantDegree, ApplicantDegree>(objt);
        }

        public static List<ApplicantDegree> ToEntityList(List<tblApplicantDegree> listt)
        {
            List<ApplicantDegree> list = new List<ApplicantDegree>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ApplicantDegree ToEntity(tvwApplicantDegree objt)
        {
            Mapper.CreateMap<tvwApplicantDegree, ApplicantDegree>();
            return Mapper.Map<tvwApplicantDegree, ApplicantDegree>(objt);
        }

        public static ApplicantDegree ToEntity(tvwfApplicantDegree objt)
        {
            Mapper.CreateMap<tvwfApplicantDegree, ApplicantDegree>();
            return Mapper.Map<tvwfApplicantDegree, ApplicantDegree>(objt);
        }
    }

    public static class MapApplicantDegreeMark
    {
        public static tblApplicantDegreeMark ToTable(ApplicantDegreeMark objt)
        {
            Mapper.CreateMap<ApplicantDegreeMark, tblApplicantDegreeMark>();
            return Mapper.Map<ApplicantDegreeMark, tblApplicantDegreeMark>(objt);
        }

        public static ApplicantDegreeMark ToEntity(tblApplicantDegreeMark objt)
        {
            Mapper.CreateMap<tblApplicantDegreeMark, ApplicantDegreeMark>();
            return Mapper.Map<tblApplicantDegreeMark, ApplicantDegreeMark>(objt);
        }

        public static List<ApplicantDegreeMark> ToEntityList(List<tblApplicantDegreeMark> listt)
        {
            List<ApplicantDegreeMark> list = new List<ApplicantDegreeMark>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ApplicantDegreeMark ToEntity(tblApplicantDegreeAttemptFinal objt)
        {
            Mapper.CreateMap<tblApplicantDegreeAttemptFinal, ApplicantDegreeMark>();
            return Mapper.Map<tblApplicantDegreeAttemptFinal, ApplicantDegreeMark>(objt);
        }

        public static List<ApplicantDegreeMark> ToEntityList(List<tblApplicantDegreeAttemptFinal> listt)
        {
            List<ApplicantDegreeMark> list = new List<ApplicantDegreeMark>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }
    }

    public static class MapApplicantCertificate
    {
        public static tblApplicantCertificate ToTable(ApplicantCertificate objt)
        {
            Mapper.CreateMap<ApplicantCertificate, tblApplicantCertificate>();
            return Mapper.Map<ApplicantCertificate, tblApplicantCertificate>(objt);
        }

        public static ApplicantCertificate ToEntity(tblApplicantCertificate objt)
        {
            Mapper.CreateMap<tblApplicantCertificate, ApplicantCertificate>();
            return Mapper.Map<tblApplicantCertificate, ApplicantCertificate>(objt);
        }

        public static List<ApplicantCertificate> ToEntityList(List<tblApplicantCertificate> listt)
        {
            List<ApplicantCertificate> list = new List<ApplicantCertificate>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ApplicantCertificate ToEntity(tvwApplicantCertificate objt)
        {
            Mapper.CreateMap<tvwApplicantCertificate, ApplicantCertificate>();
            return Mapper.Map<tvwApplicantCertificate, ApplicantCertificate>(objt);
        }

        public static List<ApplicantCertificate> ToEntityList(List<tvwApplicantCertificate> listt)
        {
            List<ApplicantCertificate> list = new List<ApplicantCertificate>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static ApplicantCertificate ToEntity(tvwfApplicantCertificate objt)
        {
            Mapper.CreateMap<tvwfApplicantCertificate, ApplicantCertificate>();
            return Mapper.Map<tvwfApplicantCertificate, ApplicantCertificate>(objt);
        }

        public static List<ApplicantCertificate> ToEntityList(List<tvwfApplicantCertificate> listt)
        {
            List<ApplicantCertificate> list = new List<ApplicantCertificate>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }
    }

    public static class MapApplicantHouseJob
    {
        public static tblApplicantExperience ToTable(ApplicantHouseJob objt)
        {
            Mapper.CreateMap<ApplicantHouseJob, tblApplicantExperience>();
            return Mapper.Map<ApplicantHouseJob, tblApplicantExperience>(objt);
        }

        public static Message ToEntity(spApplicantExperienceAddUpdate_Result objt)
        {
            Mapper.CreateMap<spApplicantExperienceAddUpdate_Result, Message>();
            return Mapper.Map<spApplicantExperienceAddUpdate_Result, Message>(objt);
        }

        public static Message ToEntity(spApplicantHouseJobAddUpdate_Result objt)
        {
            Mapper.CreateMap<spApplicantHouseJobAddUpdate_Result, Message>();
            return Mapper.Map<spApplicantHouseJobAddUpdate_Result, Message>(objt);
        }

        public static ApplicantHouseJob ToEntity(tblApplicantHouseJob objt)
        {
            Mapper.CreateMap<tblApplicantHouseJob, ApplicantHouseJob>();
            return Mapper.Map<tblApplicantHouseJob, ApplicantHouseJob>(objt);
        }

        public static List<ApplicantHouseJob> ToEntityList(List<tblApplicantHouseJob> listt)
        {
            List<ApplicantHouseJob> list = new List<ApplicantHouseJob>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ApplicantHouseJob ToEntity(tvwApplicantHouseJob objt)
        {
            Mapper.CreateMap<tvwApplicantHouseJob, ApplicantHouseJob>();
            return Mapper.Map<tvwApplicantHouseJob, ApplicantHouseJob>(objt);
        }

        public static List<ApplicantHouseJob> ToEntityList(List<tvwApplicantHouseJob> listt)
        {
            List<ApplicantHouseJob> list = new List<ApplicantHouseJob>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

    }

    public static class MapApplicantExperience
    {
        public static tblApplicantExperience ToTable(ApplicantExperience objt)
        {
            Mapper.CreateMap<ApplicantExperience, tblApplicantExperience>();
            return Mapper.Map<ApplicantExperience, tblApplicantExperience>(objt);
        }

        public static Message ToEntity(spApplicantExperienceAddUpdate_Result objt)
        {
            Mapper.CreateMap<spApplicantExperienceAddUpdate_Result, Message>();
            return Mapper.Map<spApplicantExperienceAddUpdate_Result, Message>(objt);
        }

        public static Message ToEntity(spApplicantExperienceAddUpdateAdmin_Result objt)
        {
            Mapper.CreateMap<spApplicantExperienceAddUpdateAdmin_Result, Message>();
            return Mapper.Map<spApplicantExperienceAddUpdateAdmin_Result, Message>(objt);
        }

        public static ApplicantExperience ToEntity(tblApplicantExperience objt)
        {
            Mapper.CreateMap<tblApplicantExperience, ApplicantExperience>();
            return Mapper.Map<tblApplicantExperience, ApplicantExperience>(objt);
        }

        public static List<ApplicantExperience> ToEntityList(List<tblApplicantExperience> listt)
        {
            List<ApplicantExperience> list = new List<ApplicantExperience>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ApplicantExperience ToEntity(tvwApplicantExperience objt)
        {
            Mapper.CreateMap<tvwApplicantExperience, ApplicantExperience>();
            return Mapper.Map<tvwApplicantExperience, ApplicantExperience>(objt);
        }

        public static List<ApplicantExperience> ToEntityList(List<tvwApplicantExperience> listt)
        {
            List<ApplicantExperience> list = new List<ApplicantExperience>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ApplicantExperience ToEntity(spApplicantExperienceByApplicant_Result objt)
        {
            Mapper.CreateMap<spApplicantExperienceByApplicant_Result, ApplicantExperience>();
            return Mapper.Map<spApplicantExperienceByApplicant_Result, ApplicantExperience>(objt);
        }

        public static List<ApplicantExperience> ToEntityList(List<spApplicantExperienceByApplicant_Result> listt)
        {
            List<ApplicantExperience> list = new List<ApplicantExperience>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        /////////////////


        public static ApplicantExperience ToEntity(tvwfApplicantExperience objt)
        {
            Mapper.CreateMap<tvwfApplicantExperience, ApplicantExperience>();
            return Mapper.Map<tvwfApplicantExperience, ApplicantExperience>(objt);
        }

        public static List<ApplicantExperience> ToEntityList(List<tvwfApplicantExperience> listt)
        {
            List<ApplicantExperience> list = new List<ApplicantExperience>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }
    }

    public static class MapApplicantDistinction
    {
        public static tblApplicantDistinction ToTable(ApplicantDistinction objt)
        {
            Mapper.CreateMap<ApplicantDistinction, tblApplicantDistinction>();
            return Mapper.Map<ApplicantDistinction, tblApplicantDistinction>(objt);
        }

        public static Message ToEntity(spApplicantDistinctionAddUpdate_Result objt)
        {
            Mapper.CreateMap<spApplicantDistinctionAddUpdate_Result, Message>();
            return Mapper.Map<spApplicantDistinctionAddUpdate_Result, Message>(objt);
        }

        public static ApplicantDistinction ToEntity(tblApplicantDistinction objt)
        {
            Mapper.CreateMap<tblApplicantDistinction, ApplicantDistinction>();
            return Mapper.Map<tblApplicantDistinction, ApplicantDistinction>(objt);
        }

        public static List<ApplicantDistinction> ToEntityList(List<tblApplicantDistinction> listt)
        {
            List<ApplicantDistinction> list = new List<ApplicantDistinction>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        ///
        public static ApplicantDistinction ToEntity(tblApplicantDistinctionFinal objt)
        {
            Mapper.CreateMap<tblApplicantDistinctionFinal, ApplicantDistinction>();
            return Mapper.Map<tblApplicantDistinctionFinal, ApplicantDistinction>(objt);
        }

        public static List<ApplicantDistinction> ToEntityList(List<tblApplicantDistinctionFinal> listt)
        {
            List<ApplicantDistinction> list = new List<ApplicantDistinction>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }
    }

    public static class MapApplicantResearchPaper
    {
        public static tblApplicantResearchPaper ToTable(ApplicantResearchPaper objt)
        {
            Mapper.CreateMap<ApplicantResearchPaper, tblApplicantResearchPaper>();
            return Mapper.Map<ApplicantResearchPaper, tblApplicantResearchPaper>(objt);
        }

        public static ApplicantResearchPaper ToEntity(tblApplicantResearchPaper objt)
        {
            Mapper.CreateMap<tblApplicantResearchPaper, ApplicantResearchPaper>();
            return Mapper.Map<tblApplicantResearchPaper, ApplicantResearchPaper>(objt);
        }

        public static List<ApplicantResearchPaper> ToEntityList(List<tblApplicantResearchPaper> listt)
        {
            List<ApplicantResearchPaper> list = new List<ApplicantResearchPaper>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ApplicantResearchPaper ToEntity(spApplicantResearchPaperByApplicant_Result objt)
        {
            Mapper.CreateMap<spApplicantResearchPaperByApplicant_Result, ApplicantResearchPaper>();
            return Mapper.Map<spApplicantResearchPaperByApplicant_Result, ApplicantResearchPaper>(objt);
        }

        public static List<ApplicantResearchPaper> ToEntityList(List<spApplicantResearchPaperByApplicant_Result> listt)
        {
            List<ApplicantResearchPaper> list = new List<ApplicantResearchPaper>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ApplicantResearchPaper ToEntity(tvwApplicantResearchPaper objt)
        {
            Mapper.CreateMap<tvwApplicantResearchPaper, ApplicantResearchPaper>();
            return Mapper.Map<tvwApplicantResearchPaper, ApplicantResearchPaper>(objt);
        }

        public static List<ApplicantResearchPaper> ToEntityList(List<tvwApplicantResearchPaper> listt)
        {
            List<ApplicantResearchPaper> list = new List<ApplicantResearchPaper>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static ApplicantResearchPaper ToEntity(tblApplicantResearchPaperFinal objt)
        {
            Mapper.CreateMap<tblApplicantResearchPaperFinal, ApplicantResearchPaper>();
            return Mapper.Map<tblApplicantResearchPaperFinal, ApplicantResearchPaper>(objt);
        }

        public static List<ApplicantResearchPaper> ToEntityList(List<tblApplicantResearchPaperFinal> listt)
        {
            List<ApplicantResearchPaper> list = new List<ApplicantResearchPaper>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

    }

    public static class MapApplicantSpecility
    {
        public static tblApplicantSpecility ToTable(ApplicantSpecility objt)
        {
            Mapper.CreateMap<ApplicantSpecility, tblApplicantSpecility>();
            return Mapper.Map<ApplicantSpecility, tblApplicantSpecility>(objt);
        }

        public static ApplicantSpecility ToEntity(tblApplicantSpecility objt)
        {
            Mapper.CreateMap<tblApplicantSpecility, ApplicantSpecility>();
            return Mapper.Map<tblApplicantSpecility, ApplicantSpecility>(objt);
        }

        public static List<ApplicantSpecility> ToEntityList(List<tblApplicantSpecility> listt)
        {
            List<ApplicantSpecility> list = new List<ApplicantSpecility>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ApplicantSpecility ToEntity(spApplicantSpecilityByApplicant_Result objt)
        {
            Mapper.CreateMap<spApplicantSpecilityByApplicant_Result, ApplicantSpecility>();
            return Mapper.Map<spApplicantSpecilityByApplicant_Result, ApplicantSpecility>(objt);
        }

        public static List<ApplicantSpecility> ToEntityList(List<spApplicantSpecilityByApplicant_Result> listt)
        {
            List<ApplicantSpecility> list = new List<ApplicantSpecility>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static ApplicantSpecility ToEntity(spApplicantSpecilityWithMarksByApplicant_Result objt)
        {
            Mapper.CreateMap<spApplicantSpecilityWithMarksByApplicant_Result, ApplicantSpecility>();
            return Mapper.Map<spApplicantSpecilityWithMarksByApplicant_Result, ApplicantSpecility>(objt);
        }

        public static List<ApplicantSpecility> ToEntityList(List<spApplicantSpecilityWithMarksByApplicant_Result> listt)
        {
            List<ApplicantSpecility> list = new List<ApplicantSpecility>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ApplicantSpecility ToEntity(spApplicantSpecilityWithMarksByApplicantFinal_Result objt)
        {
            Mapper.CreateMap<spApplicantSpecilityWithMarksByApplicantFinal_Result, ApplicantSpecility>();
            return Mapper.Map<spApplicantSpecilityWithMarksByApplicantFinal_Result, ApplicantSpecility>(objt);
        }

        public static List<ApplicantSpecility> ToEntityList(List<spApplicantSpecilityWithMarksByApplicantFinal_Result> listt)
        {
            List<ApplicantSpecility> list = new List<ApplicantSpecility>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ApplicantSpecility ToEntity(tvwApplicantSpecility objt)
        {
            Mapper.CreateMap<tvwApplicantSpecility, ApplicantSpecility>();
            return Mapper.Map<tvwApplicantSpecility, ApplicantSpecility>(objt);
        }

        public static List<ApplicantSpecility> ToEntityList(List<tvwApplicantSpecility> listt)
        {
            List<ApplicantSpecility> list = new List<ApplicantSpecility>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ApplicantSpecility ToEntity(tblApplicantSpecilityFinal objt)
        {
            Mapper.CreateMap<tblApplicantSpecilityFinal, ApplicantSpecility>();
            return Mapper.Map<tblApplicantSpecilityFinal, ApplicantSpecility>(objt);
        }

        public static List<ApplicantSpecility> ToEntityList(List<tblApplicantSpecilityFinal> listt)
        {
            List<ApplicantSpecility> list = new List<ApplicantSpecility>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }
    }

    #endregion

    #region Region


    public static class MapRegion
    {
        public static Region ToEntity(tblRegion objt)
        {
            Mapper.CreateMap<tblRegion, Region>();
            return Mapper.Map<tblRegion, Region>(objt);
        }
        public static List<Region> ToEntityList(List<tblRegion> listt)
        {
            List<Region> list = new List<Region>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Region ToEntity(spRegionGetByCondition_Result objt)
        {
            Mapper.CreateMap<spRegionGetByCondition_Result, Region>();
            return Mapper.Map<spRegionGetByCondition_Result, Region>(objt);
        }
        public static List<Region> ToEntityList(List<spRegionGetByCondition_Result> listt)
        {
            List<Region> list = new List<Region>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static EntityDDL ToEntity(spRegionForDDL_Result obj)
        {
            Mapper.CreateMap<spRegionForDDL_Result, EntityDDL>();
            return Mapper.Map<spRegionForDDL_Result, EntityDDL>(obj);

        }
        public static List<EntityDDL> ToEntityList(List<spRegionForDDL_Result> listt)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


    }
    #endregion

    #region  Institue

    public static class MapInstitue
    {
        public static tblInstitute ToTable(Institute obj)
        {
            Mapper.CreateMap<Institute, tblInstitute>();
            return Mapper.Map<Institute, tblInstitute>(obj);

        }

        public static Institute ToEntity(tblInstitute obj)
        {
            Mapper.CreateMap<tblInstitute, Institute>();
            return Mapper.Map<tblInstitute, Institute>(obj);

        }

        public static List<Institute> ToEntityList(List<tblInstitute> listConfig)
        {
            List<Institute> list = new List<Institute>();
            foreach (var item in listConfig)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Institute ToEntity(tvwInstitute obj)
        {
            Mapper.CreateMap<tvwInstitute, Institute>();
            return Mapper.Map<tvwInstitute, Institute>(obj);

        }
        public static List<Institute> ToEntityList(List<tvwInstitute> listt)
        {
            List<Institute> list = new List<Institute>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static EntityDDL ToEntity(spInstituteForDDL_Result obj)
        {
            Mapper.CreateMap<spInstituteForDDL_Result, EntityDDL>();
            return Mapper.Map<spInstituteForDDL_Result, EntityDDL>(obj);

        }
        public static List<EntityDDL> ToEntityList(List<spInstituteForDDL_Result> listt)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }
    }

    #endregion

    #region  Hospital

    public static class MapHospital
    {
        public static tblHospital ToTable(Hospital obj)
        {
            Mapper.CreateMap<Hospital, tblHospital>();
            return Mapper.Map<Hospital, tblHospital>(obj);

        }

        public static Hospital ToEntity(tblHospital obj)
        {
            Mapper.CreateMap<tblHospital, Hospital>();
            return Mapper.Map<tblHospital, Hospital>(obj);

        }

        public static List<Hospital> ToEntityList(List<tblHospital> listConfig)
        {
            List<Hospital> list = new List<Hospital>();
            foreach (var item in listConfig)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Hospital ToEntity(tvwHospitalUser obj)
        {
            Mapper.CreateMap<tvwHospitalUser, Hospital>();
            return Mapper.Map<tvwHospitalUser, Hospital>(obj);

        }

        public static List<Hospital> ToEntityList(List<tvwHospitalUser> listConfig)
        {
            List<Hospital> list = new List<Hospital>();
            foreach (var item in listConfig)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Hospital ToEntity(tvwHospital obj)
        {
            Mapper.CreateMap<tvwHospital, Hospital>();
            return Mapper.Map<tvwHospital, Hospital>(obj);

        }

        public static List<Hospital> ToEntityList(List<tvwHospital> listConfig)
        {
            List<Hospital> list = new List<Hospital>();
            foreach (var item in listConfig)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Hospital ToEntity(tvwInstitute obj)
        {
            Mapper.CreateMap<tvwInstitute, Hospital>();
            return Mapper.Map<tvwInstitute, Hospital>(obj);

        }
        public static List<Hospital> ToEntityList(List<tvwInstitute> listt)
        {
            List<Hospital> list = new List<Hospital>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static EntityDDL ToEntity(spHospitalForDDL_Result obj)
        {
            Mapper.CreateMap<spHospitalForDDL_Result, EntityDDL>();
            return Mapper.Map<spHospitalForDDL_Result, EntityDDL>(obj);

        }
        public static List<EntityDDL> ToEntityList(List<spHospitalForDDL_Result> listt)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Hospital ToEntity(spHospitalForInstitute_Result obj)
        {
            Mapper.CreateMap<spHospitalForInstitute_Result, Hospital>();
            return Mapper.Map<spHospitalForInstitute_Result, Hospital>(obj);

        }
        public static List<Hospital> ToEntityList(List<spHospitalForInstitute_Result> listt)
        {
            List<Hospital> list = new List<Hospital>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static HospitalDiscipline ToEntity(tblHospitalDiscipline obj)
        {
            Mapper.CreateMap<tblHospitalDiscipline, HospitalDiscipline>();
            return Mapper.Map<tblHospitalDiscipline, HospitalDiscipline>(obj);

        }
        public static List<HospitalDiscipline> ToEntityList(List<tblHospitalDiscipline> listt)
        {
            List<HospitalDiscipline> list = new List<HospitalDiscipline>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static HospitalDiscipline ToEntity(spHospitalDisciplineSearch_Result obj)
        {
            Mapper.CreateMap<spHospitalDisciplineSearch_Result, HospitalDiscipline>();
            return Mapper.Map<spHospitalDisciplineSearch_Result, HospitalDiscipline>(obj);

        }
        public static List<HospitalDiscipline> ToEntityList(List<spHospitalDisciplineSearch_Result> listt)
        {
            List<HospitalDiscipline> list = new List<HospitalDiscipline>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

    }

    #endregion

    #region Speciality

    public static class MapSpeciality
    {
        public static tblSpeciality ToTable(Speciality obj)
        {
            Mapper.CreateMap<Speciality, tblSpeciality>();
            return Mapper.Map<Speciality, tblSpeciality>(obj);

        }

        public static Speciality ToEntity(tblSpeciality obj)
        {
            Mapper.CreateMap<tblSpeciality, Speciality>();
            return Mapper.Map<tblSpeciality, Speciality>(obj);

        }

        public static List<Speciality> ToEntityList(List<tblSpeciality> listt)
        {
            List<Speciality> list = new List<Speciality>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static EntityDDL ToEntity(spSpecialityForDDL_Result obj)
        {
            Mapper.CreateMap<spSpecialityForDDL_Result, EntityDDL>();
            return Mapper.Map<spSpecialityForDDL_Result, EntityDDL>(obj);

        }

        public static List<EntityDDL> ToEntityList(List<spSpecialityForDDL_Result> listt)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Speciality ToEntity(spSpecialityJobByInduction_Result obj)
        {
            Mapper.CreateMap<spSpecialityJobByInduction_Result, Speciality>();
            return Mapper.Map<spSpecialityJobByInduction_Result, Speciality>(obj);

        }

        public static List<Speciality> ToEntityList(List<spSpecialityJobByInduction_Result> listt)
        {
            List<Speciality> list = new List<Speciality>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }




        public static SpecialityJob ToEntity(tblSpecialityJob obj)
        {
            Mapper.CreateMap<tblSpecialityJob, SpecialityJob>();
            return Mapper.Map<tblSpecialityJob, SpecialityJob>(obj);

        }
        public static SpecialityJob ToEntity(spSpecialityJobByParameters_Result obj)
        {
            Mapper.CreateMap<spSpecialityJobByParameters_Result, SpecialityJob>();
            return Mapper.Map<spSpecialityJobByParameters_Result, SpecialityJob>(obj);

        }

        public static List<SpecialityJob> ToEntityList(List<spSpecialityJobByParameters_Result> listt)
        {
            List<SpecialityJob> list = new List<SpecialityJob>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static SpecialityJob ToEntity(spSpecialityJobByTypeId_Result obj)
        {
            Mapper.CreateMap<spSpecialityJobByTypeId_Result, SpecialityJob>();
            return Mapper.Map<spSpecialityJobByTypeId_Result, SpecialityJob>(obj);

        }

        public static List<SpecialityJob> ToEntityList(List<spSpecialityJobByTypeId_Result> listt)
        {
            List<SpecialityJob> list = new List<SpecialityJob>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }



    }
    #endregion


    #region Research Journal

    public static class MapResearchJournal
    {
        public static tblResearchJournal ToTable(ResearchJournal obj)
        {
            Mapper.CreateMap<ResearchJournal, tblResearchJournal>();
            return Mapper.Map<ResearchJournal, tblResearchJournal>(obj);

        }

        public static ResearchJournal ToEntity(tblResearchJournal obj)
        {
            Mapper.CreateMap<tblResearchJournal, ResearchJournal>();
            return Mapper.Map<tblResearchJournal, ResearchJournal>(obj);

        }

        public static List<ResearchJournal> ToEntityList(List<tblResearchJournal> listConfig)
        {
            List<ResearchJournal> list = new List<ResearchJournal>();
            foreach (var item in listConfig)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ResearchJournal ToEntity(tvwResearchJournal obj)
        {
            Mapper.CreateMap<tvwResearchJournal, ResearchJournal>();
            return Mapper.Map<tvwResearchJournal, ResearchJournal>(obj);

        }
        public static List<ResearchJournal> ToEntityList(List<tvwResearchJournal> listt)
        {
            List<ResearchJournal> list = new List<ResearchJournal>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static EntityDDL ToEntity(spResearchJournalForDDL_Result obj)
        {
            Mapper.CreateMap<spResearchJournalForDDL_Result, EntityDDL>();
            return Mapper.Map<spResearchJournalForDDL_Result, EntityDDL>(obj);

        }
        public static List<EntityDDL> ToEntityList(List<spResearchJournalForDDL_Result> listt)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

    }

    #endregion

    #region Research Journal

    public static class MapCheckList
    {
        public static tblResearchJournal ToTable(ResearchJournal obj)
        {
            Mapper.CreateMap<ResearchJournal, tblResearchJournal>();
            return Mapper.Map<ResearchJournal, tblResearchJournal>(obj);

        }

        public static ResearchJournal ToEntity(tblResearchJournal obj)
        {
            Mapper.CreateMap<tblResearchJournal, ResearchJournal>();
            return Mapper.Map<tblResearchJournal, ResearchJournal>(obj);

        }

        public static List<ResearchJournal> ToEntityList(List<tblResearchJournal> listConfig)
        {
            List<ResearchJournal> list = new List<ResearchJournal>();
            foreach (var item in listConfig)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ResearchJournal ToEntity(tvwResearchJournal obj)
        {
            Mapper.CreateMap<tvwResearchJournal, ResearchJournal>();
            return Mapper.Map<tvwResearchJournal, ResearchJournal>(obj);

        }
        public static List<ResearchJournal> ToEntityList(List<tvwResearchJournal> listt)
        {
            List<ResearchJournal> list = new List<ResearchJournal>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static EntityDDL ToEntity(spResearchJournalForDDL_Result obj)
        {
            Mapper.CreateMap<spResearchJournalForDDL_Result, EntityDDL>();
            return Mapper.Map<spResearchJournalForDDL_Result, EntityDDL>(obj);

        }
        public static List<EntityDDL> ToEntityList(List<spResearchJournalForDDL_Result> listt)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

    }

    #endregion


    #region SpecialityJob

    public static class MapSpecialityJob
    {
        public static tblSpecialityJob ToTable(SpecialityJob obj)
        {
            Mapper.CreateMap<SpecialityJob, tblSpecialityJob>();
            return Mapper.Map<SpecialityJob, tblSpecialityJob>(obj);

        }

        public static SpecialityJob ToEntity(tblSpecialityJob obj)
        {
            Mapper.CreateMap<tblSpecialityJob, SpecialityJob>();
            return Mapper.Map<tblSpecialityJob, SpecialityJob>(obj);

        }

        public static List<SpecialityJob> ToEntityList(List<tblSpecialityJob> listt)
        {
            List<SpecialityJob> list = new List<SpecialityJob>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }





    }
    #endregion

    #region Marks

    public static class MapMark
    {
        public static tblMark ToTable(Marks obj)
        {
            Mapper.CreateMap<Marks, tblMark>();
            return Mapper.Map<Marks, tblMark>(obj);

        }

        public static Marks ToEntity(tblMark obj)
        {
            Mapper.CreateMap<tblMark, Marks>();
            return Mapper.Map<tblMark, Marks>(obj);

        }

        public static List<Marks> ToEntityList(List<tblMark> listt)
        {
            List<Marks> list = new List<Marks>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Marks ToEntity(spMarksGetAccumulativeByApplicant_Result obj)
        {
            Mapper.CreateMap<spMarksGetAccumulativeByApplicant_Result, Marks>();
            return Mapper.Map<spMarksGetAccumulativeByApplicant_Result, Marks>(obj);

        }

        public static List<Marks> ToEntityList(List<spMarksGetAccumulativeByApplicant_Result> listt)
        {
            List<Marks> list = new List<Marks>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static Marks ToEntity(spMarksDetailByApplicant_Result obj)
        {
            Mapper.CreateMap<spMarksDetailByApplicant_Result, Marks>();
            return Mapper.Map<spMarksDetailByApplicant_Result, Marks>(obj);

        }

        public static List<Marks> ToEntityList(List<spMarksDetailByApplicant_Result> listt)
        {
            List<Marks> list = new List<Marks>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }



    }
    #endregion

    #region MapVoucher

    public static class MapVoucher
    {

        public static ApplicantVoucher ToEntity(tblApplicantVoucher obj)
        {
            Mapper.CreateMap<tblApplicantVoucher, ApplicantVoucher>();
            return Mapper.Map<tblApplicantVoucher, ApplicantVoucher>(obj);
        }

        public static ApplicantVoucherBank ToEntity(spApplicantVoucherGetByApplicantNo_Result obj)
        {
            Mapper.CreateMap<spApplicantVoucherGetByApplicantNo_Result, ApplicantVoucherBank>();
            return Mapper.Map<spApplicantVoucherGetByApplicantNo_Result, ApplicantVoucherBank>(obj);
        }


        //public static ApplicantVoucher ToEntity(spApplicantVoucherGetByApplicantNo_Result obj)
        //{
        //    Mapper.CreateMap<spApplicantVoucherGetByApplicantNo_Result, ApplicantVoucher>();
        //    return Mapper.Map<spApplicantVoucherGetByApplicantNo_Result, ApplicantVoucher>(obj);
        //}

        //

        public static VoucherInfo ToEntity(tvwVoucherInfo obj)
        {
            Mapper.CreateMap<tvwVoucherInfo, VoucherInfo>();
            return Mapper.Map<tvwVoucherInfo, VoucherInfo>(obj);

        }

        public static List<VoucherInfo> ToEntityList(List<tvwVoucherInfo> listt)
        {
            List<VoucherInfo> list = new List<VoucherInfo>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static VoucherInfo ToEntity(spVoucherGetList_Result obj)
        {
            Mapper.CreateMap<spVoucherGetList_Result, VoucherInfo>();
            return Mapper.Map<spVoucherGetList_Result, VoucherInfo>(obj);

        }

        public static List<VoucherInfo> ToEntityList(List<spVoucherGetList_Result> listt)
        {
            List<VoucherInfo> list = new List<VoucherInfo>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        ///

        public static ApplicantVoucher ToEntity(tblApplicantVoucherFinal obj)
        {
            Mapper.CreateMap<tblApplicantVoucherFinal, ApplicantVoucher>();
            return Mapper.Map<tblApplicantVoucherFinal, ApplicantVoucher>(obj);
        }

    }




    #endregion

    #region StatusEmail

    public static class MapStatusEmail
    {

        public static StatusEmail ToEntity(tblStatusEmail obj)
        {
            Mapper.CreateMap<tblStatusEmail, StatusEmail>();
            return Mapper.Map<tblStatusEmail, StatusEmail>(obj);
        }

        public static Message ToEntity(spEmailStatusAddUpdate_Result obj)
        {
            Mapper.CreateMap<spEmailStatusAddUpdate_Result, Message>();
            return Mapper.Map<spEmailStatusAddUpdate_Result, Message>(obj);
        }


        public static StatusEmail ToEntity(tvwVoucherInfo obj)
        {
            Mapper.CreateMap<tvwVoucherInfo, StatusEmail>();
            return Mapper.Map<tvwVoucherInfo, StatusEmail>(obj);

        }

        public static List<StatusEmail> ToEntityList(List<tvwVoucherInfo> listt)
        {
            List<StatusEmail> list = new List<StatusEmail>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

    }
    #endregion


    #region Verification

    public class MapVerification
    {

        //public static Message ToEntity(spApplicantGetBySearchVerification_Result obj)
        //{
        //    Mapper.CreateMap<spApplicantGetBySearchVerification_Result, Message>();
        //    return Mapper.Map<spApplicantGetBySearchVerification_Result, Message>(obj);
        //}

        //public static Message ToEntity(spApplicantApprovalStatusAddUpdate_Result obj)
        //{
        //    Mapper.CreateMap<spApplicantApprovalStatusAddUpdate_Result, Message>();
        //    return Mapper.Map<spApplicantApprovalStatusAddUpdate_Result, Message>(obj);
        //}

        public static ApplicantApprovalStatus ToEntity(spApplicationApprovalStatusGetById_Result obj)
        {
            Mapper.CreateMap<spApplicationApprovalStatusGetById_Result, ApplicantApprovalStatus>();
            return Mapper.Map<spApplicationApprovalStatusGetById_Result, ApplicantApprovalStatus>(obj);
        }

        public static List<ApplicantApprovalStatus> ToEntityList(List<spApplicationApprovalStatusGetById_Result> listt)
        {
            List<ApplicantApprovalStatus> list = new List<ApplicantApprovalStatus>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ApplicationVerificationStatus ToEntity(tvwApplicationAmendment obj)
        {
            Mapper.CreateMap<tvwApplicationAmendment, ApplicationVerificationStatus>();
            return Mapper.Map<tvwApplicationAmendment, ApplicationVerificationStatus>(obj);
        }



        public static ApplicantApprovalStatus ToEntity(spApplicationApprovalStatusGetByTypeAndId_Result obj)
        {
            Mapper.CreateMap<spApplicationApprovalStatusGetByTypeAndId_Result, ApplicantApprovalStatus>();
            return Mapper.Map<spApplicationApprovalStatusGetByTypeAndId_Result, ApplicantApprovalStatus>(obj);
        }
        public static List<ApplicantApprovalStatus> ToEntityList(List<spApplicationApprovalStatusGetByTypeAndId_Result> listt)
        {
            List<ApplicantApprovalStatus> list = new List<ApplicantApprovalStatus>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }
    }
    #endregion


    #region Merit

    public class MapMerit
    {
        public static ApplicantSpecilityMerit ToEntity(spApplicantSpecialityWithMeritMarks_Result obj)
        {
            Mapper.CreateMap<spApplicantSpecialityWithMeritMarks_Result, ApplicantSpecilityMerit>();
            return Mapper.Map<spApplicantSpecialityWithMeritMarks_Result, ApplicantSpecilityMerit>(obj);
        }


        public static List<ApplicantSpecilityMerit> ToEntityList(List<spApplicantSpecialityWithMeritMarks_Result> listt)
        {
            List<ApplicantSpecilityMerit> list = new List<ApplicantSpecilityMerit>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ApplicantSpecilityMerit ToEntity(spApplicantSpecialityWithMeritMarksFCPS_Result obj)
        {
            Mapper.CreateMap<spApplicantSpecialityWithMeritMarksFCPS_Result, ApplicantSpecilityMerit>();
            return Mapper.Map<spApplicantSpecialityWithMeritMarksFCPS_Result, ApplicantSpecilityMerit>(obj);
        }


        public static List<ApplicantSpecilityMerit> ToEntityList(List<spApplicantSpecialityWithMeritMarksFCPS_Result> listt)
        {
            List<ApplicantSpecilityMerit> list = new List<ApplicantSpecilityMerit>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


    }
    #endregion

    #region Contact

    public class MapContact
    {
        public static tblContact ToTable(Contact obj)
        {
            Mapper.CreateMap<Contact, tblContact>();
            return Mapper.Map<Contact, tblContact>(obj);
        }

        public static Contact ToEntity(tblContact obj)
        {
            Mapper.CreateMap<tblContact, Contact>();
            return Mapper.Map<tblContact, Contact>(obj);
        }


        public static Contact ToEntity(spContactQuestionGetById_Result obj)
        {
            Mapper.CreateMap<spContactQuestionGetById_Result, Contact>();
            return Mapper.Map<spContactQuestionGetById_Result, Contact>(obj);
        }


        public static List<Contact> ToEntityList(List<tblContact> listt)
        {
            List<Contact> list = new List<Contact>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Contact ToEntity(tvwContact obj)
        {
            Mapper.CreateMap<tvwContact, Contact>();
            return Mapper.Map<tvwContact, Contact>(obj);
        }

        public static List<Contact> ToEntityList(List<tvwContact> listt)
        {
            List<Contact> list = new List<Contact>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Contact ToEntity(spFeedbackSearch_Result obj)
        {
            Mapper.CreateMap<spFeedbackSearch_Result, Contact>();
            return Mapper.Map<spFeedbackSearch_Result, Contact>(obj);
        }

        public static List<Contact> ToEntityList(List<spFeedbackSearch_Result> listt)
        {
            List<Contact> list = new List<Contact>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static Message ToEntity(spContactQuestion_Result obj)
        {
            Mapper.CreateMap<spContactQuestion_Result, Message>();
            return Mapper.Map<spContactQuestion_Result, Message>(obj);
        }

        public static Message ToEntity(spContactDocsAdd_Result obj)
        {
            Mapper.CreateMap<spContactDocsAdd_Result, Message>();
            return Mapper.Map<spContactDocsAdd_Result, Message>(obj);
        }

        public static ContactReply ToEntity(spContactAnswerListGetById_Result obj)
        {
            Mapper.CreateMap<spContactAnswerListGetById_Result, ContactReply>();
            return Mapper.Map<spContactAnswerListGetById_Result, ContactReply>(obj);
        }


        public static List<ContactReply> ToEntityList(List<spContactAnswerListGetById_Result> listt)
        {
            List<ContactReply> list = new List<ContactReply>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ContactDoc ToEntity(tblContactDoc obj)
        {
            Mapper.CreateMap<tblContactDoc, ContactDoc>();
            return Mapper.Map<tblContactDoc, ContactDoc>(obj);
        }


        public static List<ContactDoc> ToEntityList(List<tblContactDoc> listt)
        {
            List<ContactDoc> list = new List<ContactDoc>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ContactReply ToEntity(tblContactReply obj)
        {
            Mapper.CreateMap<tblContactReply, ContactReply>();
            return Mapper.Map<tblContactReply, ContactReply>(obj);
        }


        public static List<ContactReply> ToEntityList(List<tblContactReply> listt)
        {
            List<ContactReply> list = new List<ContactReply>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ContactStatus ToEntity(tblContactStatu obj)
        {
            Mapper.CreateMap<tblContactStatu, ContactStatus>();
            return Mapper.Map<tblContactStatu, ContactStatus>(obj);
        }

    }

    #endregion

    #region FeedBack

    public class MapFeedBack
    {
        public static tblFeedBack ToTable(FeedBack obj)
        {
            Mapper.CreateMap<FeedBack, tblFeedBack>();
            return Mapper.Map<FeedBack, tblFeedBack>(obj);
        }

        public static FeedBack ToEntity(tblFeedBack obj)
        {
            Mapper.CreateMap<tblFeedBack, FeedBack>();
            return Mapper.Map<tblFeedBack, FeedBack>(obj);
        }


        public static List<FeedBack> ToEntityList(List<tblFeedBack> listt)
        {
            List<FeedBack> list = new List<FeedBack>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static FeedBack ToEntity(tvwFeedback obj)
        {
            Mapper.CreateMap<tvwFeedback, FeedBack>();
            return Mapper.Map<tvwFeedback, FeedBack>(obj);
        }


        public static List<FeedBack> ToEntityList(List<tvwFeedback> listt)
        {
            List<FeedBack> list = new List<FeedBack>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static FeedBack ToEntity(spFeedbackSearch_Result obj)
        {
            Mapper.CreateMap<spFeedbackSearch_Result, FeedBack>();
            return Mapper.Map<spFeedbackSearch_Result, FeedBack>(obj);
        }


        public static List<FeedBack> ToEntityList(List<spFeedbackSearch_Result> listt)
        {
            List<FeedBack> list = new List<FeedBack>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


    }
    #endregion


    #region Grievance

    public class MapGrievance
    {
        public static tblGrievance ToTable(Grievance obj)
        {
            Mapper.CreateMap<Grievance, tblGrievance>();
            return Mapper.Map<Grievance, tblGrievance>(obj);
        }



        public static Grievance ToEntity(tblGrievance obj)
        {
            Mapper.CreateMap<tblGrievance, Grievance>();
            return Mapper.Map<tblGrievance, Grievance>(obj);
        }


        public static List<Grievance> ToEntityList(List<tblGrievance> listt)
        {
            List<Grievance> list = new List<Grievance>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        //public static Grievance ToEntity(tvwGrievance obj)
        //{
        //    Mapper.CreateMap<tvwGrievance, Grievance>();
        //    return Mapper.Map<tvwGrievance, Grievance>(obj);
        //}


        //public static List<Grievance> ToEntityList(List<tvwGrievance> listt)
        //{
        //    List<Grievance> list = new List<Grievance>();
        //    foreach (var item in listt)
        //    {
        //        list.Add(ToEntity(item));
        //    }
        //    return list;
        //}



    }

    public class MapGrievanceAction
    {
        public static tblGrievanceAction ToTable(GrievanceAction obj)
        {
            Mapper.CreateMap<GrievanceAction, tblGrievanceAction>();
            return Mapper.Map<GrievanceAction, tblGrievanceAction>(obj);
        }



        public static GrievanceAction ToEntity(tblGrievanceAction obj)
        {
            Mapper.CreateMap<tblGrievanceAction, GrievanceAction>();
            return Mapper.Map<tblGrievanceAction, GrievanceAction>(obj);
        }


        public static List<GrievanceAction> ToEntityList(List<tblGrievanceAction> listt)
        {
            List<GrievanceAction> list = new List<GrievanceAction>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

    }

    public class MapGrievanceRemark
    {
        public static tblGrievanceRemark ToTable(GrievanceRemark obj)
        {
            Mapper.CreateMap<GrievanceRemark, tblGrievanceRemark>();
            return Mapper.Map<GrievanceRemark, tblGrievanceRemark>(obj);
        }



        public static GrievanceRemark ToEntity(tblGrievanceRemark obj)
        {
            Mapper.CreateMap<tblGrievanceRemark, GrievanceRemark>();
            return Mapper.Map<tblGrievanceRemark, GrievanceRemark>(obj);
        }


        public static List<GrievanceRemark> ToEntityList(List<tblGrievanceRemark> listt)
        {
            List<GrievanceRemark> list = new List<GrievanceRemark>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

    }
    #endregion


    #region Consent

    public class MapConsent
    {
        public static tblConsent ToTable(Consent obj)
        {
            Mapper.CreateMap<Consent, tblConsent>();
            return Mapper.Map<Consent, tblConsent>(obj);
        }

        public static Consent ToEntity(tblConsent obj)
        {
            Mapper.CreateMap<tblConsent, Consent>();
            return Mapper.Map<tblConsent, Consent>(obj);
        }

        public static Message ToEntity(spConsentAddUpdate_Result obj)
        {
            Mapper.CreateMap<spConsentAddUpdate_Result, Message>();
            return Mapper.Map<spConsentAddUpdate_Result, Message>(obj);
        }


        public static List<Consent> ToEntityList(List<tblConsent> listt)
        {
            List<Consent> list = new List<Consent>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static Consent ToEntity(spConsentGetByApplicant_Result obj)
        {
            Mapper.CreateMap<spConsentGetByApplicant_Result, Consent>();
            return Mapper.Map<spConsentGetByApplicant_Result, Consent>(obj);
        }


        public static List<Consent> ToEntityList(List<spConsentGetByApplicant_Result> listt)
        {
            List<Consent> list = new List<Consent>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static EntityDDL ToEntity(spGetTypeApplicantHasMerit_Result obj)
        {
            Mapper.CreateMap<spGetTypeApplicantHasMerit_Result, EntityDDL>();
            return Mapper.Map<spGetTypeApplicantHasMerit_Result, EntityDDL>(obj);

        }
        public static List<EntityDDL> ToEntityList(List<spGetTypeApplicantHasMerit_Result> listt)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }





    }
    #endregion

    #region Final

    public class MapApplicantFinal
    {


        public static tblApplicantSelected ToTable(ApplicantSelected obj)
        {
            Mapper.CreateMap<ApplicantSelected, tblApplicantSelected>();
            return Mapper.Map<ApplicantSelected, tblApplicantSelected>(obj);
        }

        public static ApplicantSelected ToEntity(tvwApplicantSelected obj)
        {
            Mapper.CreateMap<tvwApplicantSelected, ApplicantSelected>();
            return Mapper.Map<tvwApplicantSelected, ApplicantSelected>(obj);
        }





        public static List<ApplicantSelected> ToEntityList(List<tvwApplicantSelected> listt)
        {
            List<ApplicantSelected> list = new List<ApplicantSelected>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


    }
    #endregion

    #region Joining

    public class MapApplicantJoining
    {
        #region DashBoard

        public static ApplicantJoiningDsb ToEntity(spJoiningCountInstituteHospitalWise_Result obj)
        {
            Mapper.CreateMap<spJoiningCountInstituteHospitalWise_Result, ApplicantJoiningDsb>();
            return Mapper.Map<spJoiningCountInstituteHospitalWise_Result, ApplicantJoiningDsb>(obj);
        }


        public static List<ApplicantJoiningDsb> ToEntityList(List<spJoiningCountInstituteHospitalWise_Result> listt)
        {
            List<ApplicantJoiningDsb> list = new List<ApplicantJoiningDsb>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ApplicantJoiningDsb ToEntity(spJoiningCountInstituteWise_Result obj)
        {
            Mapper.CreateMap<spJoiningCountInstituteWise_Result, ApplicantJoiningDsb>();
            return Mapper.Map<spJoiningCountInstituteWise_Result, ApplicantJoiningDsb>(obj);
        }


        public static List<ApplicantJoiningDsb> ToEntityList(List<spJoiningCountInstituteWise_Result> listt)
        {
            List<ApplicantJoiningDsb> list = new List<ApplicantJoiningDsb>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ApplicantJoiningDsb ToEntity(spJoiningCountInstituteWiseNew_Result obj)
        {
            Mapper.CreateMap<spJoiningCountInstituteWiseNew_Result, ApplicantJoiningDsb>();
            return Mapper.Map<spJoiningCountInstituteWiseNew_Result, ApplicantJoiningDsb>(obj);
        }


        public static List<ApplicantJoiningDsb> ToEntityList(List<spJoiningCountInstituteWiseNew_Result> listt)
        {
            List<ApplicantJoiningDsb> list = new List<ApplicantJoiningDsb>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static ApplicantJoiningDsb ToEntity(spJoiningCountByInstituteHospitalWise_Result obj)
        {
            Mapper.CreateMap<spJoiningCountByInstituteHospitalWise_Result, ApplicantJoiningDsb>();
            return Mapper.Map<spJoiningCountByInstituteHospitalWise_Result, ApplicantJoiningDsb>(obj);
        }


        public static List<ApplicantJoiningDsb> ToEntityList(List<spJoiningCountByInstituteHospitalWise_Result> listt)
        {
            List<ApplicantJoiningDsb> list = new List<ApplicantJoiningDsb>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        #endregion

        public static tblApplicantJoined ToTable(ApplicantJoined obj)
        {
            Mapper.CreateMap<ApplicantJoined, tblApplicantJoined>();
            return Mapper.Map<ApplicantJoined, tblApplicantJoined>(obj);
        }

        public static ApplicantJoined ToEntity(tblApplicantJoined obj)
        {
            Mapper.CreateMap<tblApplicantJoined, ApplicantJoined>();
            return Mapper.Map<tblApplicantJoined, ApplicantJoined>(obj);
        }


        public static Message ToEntity(spJoiningAddUpdate_Result obj)
        {
            Mapper.CreateMap<spJoiningAddUpdate_Result, Message>();
            return Mapper.Map<spJoiningAddUpdate_Result, Message>(obj);
        }


        public static List<ApplicantJoined> ToEntityList(List<ApplicantJoined> listt)
        {
            List<ApplicantJoined> list = new List<ApplicantJoined>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        public static ApplicantSelected ToEntity(tvwApplicantSelected obj)
        {
            Mapper.CreateMap<tvwApplicantSelected, ApplicantSelected>();
            return Mapper.Map<tvwApplicantSelected, ApplicantSelected>(obj);
        }

        public static List<ApplicantSelected> ToEntityList(List<tvwApplicantSelected> listt)
        {
            List<ApplicantSelected> list = new List<ApplicantSelected>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static ApplicantJoined ToEntity(spJoiningGetByHospital_Result obj)
        {
            Mapper.CreateMap<spJoiningGetByHospital_Result, ApplicantJoined>();
            return Mapper.Map<spJoiningGetByHospital_Result, ApplicantJoined>(obj);
        }


        public static List<ApplicantJoined> ToEntityList(List<spJoiningGetByHospital_Result> listt)
        {
            List<ApplicantJoined> list = new List<ApplicantJoined>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }


        public static ApplicantJoined ToEntity(spJoiningGetByInstitute_Result obj)
        {
            Mapper.CreateMap<spJoiningGetByInstitute_Result, ApplicantJoined>();
            return Mapper.Map<spJoiningGetByInstitute_Result, ApplicantJoined>(obj);
        }


        public static List<ApplicantJoined> ToEntityList(List<spJoiningGetByInstitute_Result> listt)
        {
            List<ApplicantJoined> list = new List<ApplicantJoined>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }



        public static ApplicantJoined ToEntity(spJoiningGetAll_Result obj)
        {
            Mapper.CreateMap<spJoiningGetAll_Result, ApplicantJoined>();
            return Mapper.Map<spJoiningGetAll_Result, ApplicantJoined>(obj);
        }


        public static List<ApplicantJoined> ToEntityList(List<spJoiningGetAll_Result> listt)
        {
            List<ApplicantJoined> list = new List<ApplicantJoined>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }



        public static ApplicantJoined ToEntity(tvwApplicantJoining obj)
        {
            Mapper.CreateMap<tvwApplicantJoining, ApplicantJoined>();
            return Mapper.Map<tvwApplicantJoining, ApplicantJoined>(obj);
        }

        public static List<ApplicantJoined> ToEntityList(List<tvwApplicantJoining> listt)
        {
            List<ApplicantJoined> list = new List<ApplicantJoined>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

        //
        public static SpecialityJob ToEntity(spHardshipSeatsStatusByApplicant_Result obj)
        {
            Mapper.CreateMap<spHardshipSeatsStatusByApplicant_Result, SpecialityJob>();
            return Mapper.Map<spHardshipSeatsStatusByApplicant_Result, SpecialityJob>(obj);
        }

        public static List<SpecialityJob> ToEntityList(List<spHardshipSeatsStatusByApplicant_Result> listt)
        {
            List<SpecialityJob> list = new List<SpecialityJob>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

    }
    #endregion

    public class MapResignation
    {
        

        public static tblApplicantAction ToTable(ApplicantAction obj)
        {
            Mapper.CreateMap<ApplicantAction, tblApplicantAction>();
            return Mapper.Map<ApplicantAction, tblApplicantAction>(obj);
        }

        public static ApplicantAction ToEntity(tblApplicantAction obj)
        {
            Mapper.CreateMap<tblApplicantAction, ApplicantAction>();
            return Mapper.Map<tblApplicantAction, ApplicantAction>(obj);
        }

        public static ApplicantAction ToEntity(spApplicantActionGetByApplicantId_Result obj)
        {
            Mapper.CreateMap<spApplicantActionGetByApplicantId_Result, ApplicantAction>();
            return Mapper.Map<spApplicantActionGetByApplicantId_Result, ApplicantAction>(obj);
        }

        public static Message ToEntity(spJoiningAddUpdate_Result obj)
        {
            Mapper.CreateMap<spJoiningAddUpdate_Result, Message>();
            return Mapper.Map<spJoiningAddUpdate_Result, Message>(obj);
        }


        public static List<ApplicantAction> ToEntityList(List<tblApplicantAction> listt)
        {
            List<ApplicantAction> list = new List<ApplicantAction>();
            foreach (var item in listt)
            {
                list.Add(ToEntity(item));
            }
            return list;
        }

       


       








    }
}
