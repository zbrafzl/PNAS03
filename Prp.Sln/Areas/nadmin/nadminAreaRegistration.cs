using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin
{
    public class nadminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "nadmin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                name: "LoginAdmin01",
                url: "admin",
                defaults: new { controller = "LoggedInAdmin", action = "Login", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "LoginAdmin02",
                url: "admin/login",
                defaults: new { controller = "LoggedInAdmin", action = "Login", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "AdminLogout",
               url: "admin/logout",
               defaults: new { controller = "LoggedInAdmin", action = "Logout", id = UrlParameter.Optional }
           );

            #region  Dash board

            context.MapRoute(
               name: "DashboardAdmin",
               url: "admin/index",
               defaults: new { controller = "HomeAdmin", action = "HomePageAdmin", id = UrlParameter.Optional }
           );
            context.MapRoute(
            name: "DashboardStatusInductionWiseAdmin",
            url: "admin/dashboard-status",
            defaults: new { controller = "HomeAdmin", action = "DashboardInductionWise", id = UrlParameter.Optional }
        );

            context.MapRoute(
               name: "DsbAd_JoiningInstituteWise",
               url: "admin/dsb/joining-institute",
               defaults: new { controller = "HomeAdmin", action = "DsbJoiningInstitute", id = UrlParameter.Optional }
           );

            context.MapRoute(
            name: "Ad_JoiningInstituteWise",
            url: "admin/joining-institute",
            defaults: new { controller = "HomeAdmin", action = "DsbJoiningInstitute", id = UrlParameter.Optional }
        );

            context.MapRoute(
             name: "DsbAd_JoiningHospitalWise",
             url: "admin/dsb/joining-hospital",
             defaults: new { controller = "HomeAdmin", action = "DsbJoiningHospital", id = UrlParameter.Optional }
         );


            #endregion

            #region Extra Constant 

            context.MapRoute(
                name: "AdminConstantSetup",
                url: "admin/constant-setup",
                defaults: new { controller = "CommonAdmin", action = "ConstantSetup", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "AdminConstantManage",
                url: "admin/constant-manage",
                defaults: new { controller = "CommonAdmin", action = "ConstantManage", id = UrlParameter.Optional }
            );

            #endregion

            context.MapRoute(
               name: "AdminDepartmentSetup",
               url: "admin/department-setup",
               defaults: new { controller = "CommonAdmin", action = "DepartmentSetup", id = UrlParameter.Optional }
           );

            context.MapRoute(
                name: "AdminDepartmentManage",
                url: "admin/department-manage",
                defaults: new { controller = "CommonAdmin", action = "DepartmentManage", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "AdminDepartmentAssociation",
               url: "admin/department-association",
               defaults: new { controller = "Hospitals", action = "DepartmentAssociation", id = UrlParameter.Optional }
           );

            #region Menu Management

            context.MapRoute(
               name: "MenusAdminSetup",
               url: "admin/menu-setup",
               defaults: new { controller = "MenusAdmin", action = "Setup", id = UrlParameter.Optional }
           );

            context.MapRoute(
                name: "MenusAdminManage",
                url: "admin/menu-manage",
                defaults: new { controller = "MenusAdmin", action = "Manage", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "MenusAdminAccessUserType",
                url: "admin/menu-access-type",
                defaults: new { controller = "MenusAdmin", action = "AccessUserType", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "MenusAdminAccessUser",
                url: "admin/menu-access-user",
                defaults: new { controller = "MenusAdmin", action = "AccessUser", id = UrlParameter.Optional }
            );

            #endregion

            #region User Management

            context.MapRoute(
                name: "UsersAdminSetup",
                url: "admin/user-setup",
                defaults: new { controller = "UsersAdmin", action = "Setup", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "ChangePasswordAdmin",
               url: "admin/change-password",
               defaults: new { controller = "UsersAdmin", action = "ChangePassword", id = UrlParameter.Optional }
           );

            context.MapRoute(
                name: "UsersAdminManage",
                url: "admin/user-manage",
                defaults: new { controller = "UsersAdmin", action = "Manage", id = UrlParameter.Optional }
            );

            #endregion

            #region Master Setups

            context.MapRoute(
                name: "AdmininstituteSetup",
                url: "admin/institute-setup",
                defaults: new { controller = "InstitueAdmin", action = "InstituteSetup", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "AdmininstituteManage",
                url: "admin/institute-manage",
                defaults: new { controller = "InstitueAdmin", action = "InstituteManage", id = UrlParameter.Optional }
            );

            context.MapRoute(
              name: "AdminiHospitalsSetup",
              url: "admin/hospital-setup",
              defaults: new { controller = "Hospitals", action = "HospitalSetup", id = UrlParameter.Optional }
          );

            context.MapRoute(
                name: "AdminHospitalsManage",
                url: "admin/hospital-manage",
                defaults: new { controller = "Hospitals", action = "HospitalManage", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "AdminHospitalBedsManagement",
               url: "admin/hospital-bed-management",
               defaults: new { controller = "Hospitals", action = "BedsManagement", id = UrlParameter.Optional }
           );

            context.MapRoute(
              name: "AdminHospitalDiscipline",
              url: "admin/hospital-discipline",
              defaults: new { controller = "Hospitals", action = "DisciplineAssociation", id = UrlParameter.Optional }
          );





            context.MapRoute(
                name: "AdminispecialitySetup",
                url: "admin/speciality-setup",
                defaults: new { controller = "SubjectAdmin", action = "SubjectSetup", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "AdminspecialityManage",
                url: "admin/speciality-manage",
                defaults: new { controller = "SubjectAdmin", action = "SubjectManage", id = UrlParameter.Optional }
            );

            context.MapRoute(
                 name: "DisciplineManage",
                 url: "admin/discipline-manage",
                 defaults: new { controller = "CommonAdmin", action = "DisciplineManage", id = UrlParameter.Optional }
             );

            context.MapRoute(
               name: "SpecialityJobSetup",
               url: "admin/speciality-job",
               defaults: new { controller = "SpecialityAdmin", action = "SpecialityJobSetup", id = UrlParameter.Optional }
           );

            context.MapRoute(
                name: "JobInstituteWise",
                url: "admin/institute-job",
                defaults: new { controller = "SpecialityAdmin", action = "JobInstituteWise", id = UrlParameter.Optional }
            );

            #endregion

            #region Employee

            context.MapRoute(
              name: "EmployeeAdminSetup",
              url: "admin/employee-setup",
              defaults: new { controller = "EmployeeAdmin", action = "Setup", id = UrlParameter.Optional }
             );

            context.MapRoute(
                name: "EmployeeAdminManage",
                url: "admin/employee-manage",
                defaults: new { controller = "EmployeeAdmin", action = "Manage", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "EmployeeManageExperience",
               url: "admin/employee-experience-manage",
               defaults: new { controller = "EmployeeAdmin", action = "ManageExperience", id = UrlParameter.Optional }
           );

            context.MapRoute(
              name: "EmployeeApplicantAssign",
              url: "admin/employee-applicant-association",
              defaults: new { controller = "EmployeeAdmin", action = "ApplicantAssign", id = UrlParameter.Optional }
          );

            context.MapRoute(
              name: "EmployeeSpecialization",
              url: "admin/employee-specialization",
              defaults: new { controller = "EmployeeAdmin", action = "ManageSpecialization", id = UrlParameter.Optional }
          );


            #endregion

            #region Seats


            context.MapRoute(
              name: "AdminJobListing",
              url: "admin/job-list",
              defaults: new { controller = "SpecialityAdmin", action = "JobListing", id = UrlParameter.Optional }
          );

            context.MapRoute(
              name: "AdminJobListingApplication",
              url: "admin/job-list-application",
              defaults: new { controller = "SpecialityAdmin", action = "JobApplicationSearch", id = UrlParameter.Optional }
          );


            #endregion

            #region Sms Setup

            context.MapRoute(
                  name: "AdminSMSSetup",
                  url: "admin/sms-setup",
                  defaults: new { controller = "CommonAdmin", action = "SMSSetup", id = UrlParameter.Optional }
              );

            context.MapRoute(
                name: "AdminSMSManage",
                url: "admin/sms-manage",
                defaults: new { controller = "CommonAdmin", action = "SMSManage", id = UrlParameter.Optional }
            );
            #endregion


            #region Applicant Status

            //ApplicantAddition

            context.MapRoute(
                 name: "ApplicantAddUpdate",
                 url: "admin/applicant-setup",
                 defaults: new { controller = "ApplicantAdmin", action = "ApplicantAddition", id = UrlParameter.Optional }
             );

            context.MapRoute(
                 name: "ApplicantAddMigrationCandidate",
                 url: "admin/addMigrationCandidate",
                 defaults: new { controller = "ApplicantAdmin", action = "MigrationCandidateAddition", id = UrlParameter.Optional }
             );

            context.MapRoute(
                 name: "AddSeats",
                 url: "admin/addSeats",
                 defaults: new { controller = "ApplicantAdmin", action = "AddSeats", id = UrlParameter.Optional }
             );

            context.MapRoute(
                name: "ApplicantActionStatus",
                url: "admin/applicant-status-update",
                defaults: new { controller = "ApplicantAdmin", action = "ApplicantActionStatus", id = UrlParameter.Optional }
            );

            context.MapRoute(
              name: "ApplicantStatusComplete",
              url: "admin/applicant-status",
              defaults: new { controller = "ApplicantAdmin", action = "ApplicantStatus", id = UrlParameter.Optional }
          );

            context.MapRoute(
                name: "ApplicantAccountStatus",
                url: "admin/applicant-account-status",
                defaults: new { controller = "ApplicantAdmin", action = "ApplicantAccountStatus", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "ApplicantApplicationStatus",
                url: "admin/applicant-application-status",
                defaults: new { controller = "ApplicantAdmin", action = "ApplicantApplicationStatus", id = UrlParameter.Optional }
            );


            context.MapRoute(
               name: "ApplicantAdminSearch",
               url: "admin/applicant-search-verify",
               defaults: new { controller = "ApplicantAdmin", action = "ApplicantSearchVerify", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "ApplicantAdminSearchJoin",
               url: "admin/applicant-search-join",
               defaults: new { controller = "ApplicantAdmin", action = "ApplicantSearchJoin", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "ApplicantAdminSearchVerify",
               url: "admin/applicant-search",
               defaults: new { controller = "ApplicantAdmin", action = "ApplicantSearch", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "ApplicantAdminSearchToVerify",
               url: "admin/applicant-search-to-verify",
               defaults: new { controller = "ApplicantAdmin", action = "ApplicantSearchToVeify", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "ApplicantAdminSearchToAmmendment",
               url: "admin/applicant-search-to-ammendment",
               defaults: new { controller = "ApplicantAdmin", action = "ApplicantSearchToAmmendment", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "ApplicantListCollegeWise",
               url: "admin/applicant-list-college-wise",
               defaults: new { controller = "ApplicantAdmin", action = "ApplicantListCollegeWise", id = UrlParameter.Optional }
            );

            context.MapRoute(
             name: "ApplicantAdminSearchInducion",
             url: "admin/applicant-search-induction-{induction}",
             defaults: new { controller = "ApplicantAdmin", action = "ApplicantSearch", id = UrlParameter.Optional }
         );


            context.MapRoute(
             name: "ApplicantAdminPic",
             url: "admin/applicant-pic",
             defaults: new { controller = "ApplicantAdmin", action = "ApplicantPic", id = UrlParameter.Optional }
         );

            #endregion


            #region Application Review

            context.MapRoute(
                name: "ApplicationReviewAdmin",
                url: "admin/application-review",
                defaults: new { controller = "ApplicationReviewAdmin", action = "ApplicantList", id = UrlParameter.Optional }
            );


            context.MapRoute(
                name: "ApplicantViewDetail",
                url: "admin/application-view-detail",
                defaults: new { controller = "ApplicationReviewAdmin", action = "ApplicantViewDetail", id = UrlParameter.Optional }
            );


            context.MapRoute(
               name: "ApplicantViewDetailView",
               url: "admin/application-detail",
               defaults: new { controller = "ApplicationReviewAdmin", action = "ApplicantViewDetailView", id = UrlParameter.Optional }
           );

            context.MapRoute(
             name: "ApplicantViewOnlyView",
             url: "admin/application-view",
             defaults: new { controller = "ApplicationReviewAdmin", action = "ApplicantView", id = UrlParameter.Optional }
         );
            context.MapRoute(
               name: "ApplicantDatailViewSupport",
               url: "admin/applicant-support",
               defaults: new { controller = "ApplicationReviewAdmin", action = "ApplicantSupportView", id = UrlParameter.Optional }
           );

            context.MapRoute(
                name: "ApplicantDatailViewCallCenter",
                url: "admin/applicant-detail",
                defaults: new { controller = "ApplicationReviewAdmin", action = "ApplicantDetail", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "ApplicantDatailViewCallCenterInduction",
               url: "admin/applicant-detail-induction-{induction}",
               defaults: new { controller = "ApplicationReviewAdmin", action = "ApplicantDetail", id = UrlParameter.Optional }
           );

            context.MapRoute(
               name: "ApplicantAmendmentsNursing",
               url: "admin/applicant-verification-nursing",
               defaults: new { controller = "ApplicationReviewAdmin", action = "AmendmentsApplicantNursing", id = UrlParameter.Optional }
           );

            context.MapRoute(
               name: "ApplicantAmendmentsNursingAfterVerification",
               url: "admin/applicant-amendment-nursing",
               defaults: new { controller = "ApplicationReviewAdmin", action = "AmendmentsNursing", id = UrlParameter.Optional }
           );


            context.MapRoute(
               name: "VerificationTeam",
               url: "admin/applicant/verify-ke",
               defaults: new { controller = "ApplicationReviewAdmin", action = "ApplicantDetail", id = UrlParameter.Optional }
           );

            context.MapRoute(
               name: "VerificationTeamCON",
               url: "admin/applicant/verify-con",
               defaults: new { controller = "ApplicationReviewAdmin", action = "ApplicantDetailVerify", id = UrlParameter.Optional }
           );

            context.MapRoute(
              name: "VerificationTeamPhf",
              url: "admin/applicant/verify",
              defaults: new { controller = "ApplicationReviewAdmin", action = "ApplicantDetail", id = UrlParameter.Optional }
          );

            context.MapRoute(
              name: "AmmendmentProcess",
              url: "admin/applicant/ammendment",
              defaults: new { controller = "ApplicationReviewAdmin", action = "ApplicantDetail", id = UrlParameter.Optional }
          );

            context.MapRoute(
              name: "VerificationViewSingle",
              url: "admin/applicant/verify-ke-view",
              defaults: new { controller = "VerficationProcess", action = "VerificationView", id = UrlParameter.Optional }
          );

            context.MapRoute(
                name: "ApplicantListForVerificationWithDownload",
                url: "admin/applicant/verify-list-view",
                defaults: new { controller = "VerficationProcess", action = "ApplicantListForVerificationWithDownload", id = UrlParameter.Optional }
            );

            //


            #endregion

            #region Vouchers


            context.MapRoute(
                name: "VoucherList",
                url: "admin/voucher-list",
                defaults: new { controller = "VoucherAdmin", action = "VoucherList", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "VoucherBankList",
                url: "admin/voucher-list-bank",
                defaults: new { controller = "VoucherAdmin", action = "VoucherBankList", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "VoucherListAll",
                url: "admin/voucher-list-all",
                defaults: new { controller = "VoucherAdmin", action = "VoucherListAll", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "VoucherBankVarifacation",
               url: "admin/voucher-bank-verification",
               defaults: new { controller = "VoucherAdmin", action = "VoucherListNotVerified", id = UrlParameter.Optional }
           );

            context.MapRoute(
              name: "VoucherDetail",
              url: "admin/voucher-detail",
              defaults: new { controller = "VoucherAdmin", action = "VoucherDetail", id = UrlParameter.Optional }
          );




            #endregion


            #region SMS And Email


            context.MapRoute(
                  name: "EmailTempalteSetup",
                  url: "admin/email-template-setup",
                  defaults: new { controller = "EmailAndSmsAmin", action = "TemplateSetupEmail", id = UrlParameter.Optional }
             );

            context.MapRoute(
                  name: "EmailTempalteManage",
                  url: "admin/email-template-manage",
                  defaults: new { controller = "EmailAndSmsAmin", action = "TemplateManageEmail", id = UrlParameter.Optional }
             );


            context.MapRoute(
             name: "SMSProcessQueryRun",
             url: "admin/sms-run-query",
             defaults: new { controller = "EmailAndSmsAmin", action = "SMSProcessQuery", id = UrlParameter.Optional }
         );

            context.MapRoute(
               name: "SMSProcessViewAdmin",
               url: "admin/sms-send-type",
               defaults: new { controller = "EmailAndSmsAmin", action = "SMSProcessView", id = UrlParameter.Optional }
           );

            context.MapRoute(
                name: "SMSProcessAdmin",
                url: "admin/applicant-send-sms",
                defaults: new { controller = "EmailAndSmsAmin", action = "SMSProcess", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "SMSProcessSingleAdmin",
                url: "admin/applicant-send-sms-single",
                defaults: new { controller = "EmailAndSmsAmin", action = "SMSProcessSingle", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "SMSProcessSingleRejectAdmin",
               url: "admin/applicant-send-sms-reject",
               defaults: new { controller = "EmailAndSmsAmin", action = "SMSProcessSingle", id = UrlParameter.Optional }
           );




            context.MapRoute(
                name: "EmailProcessAdmin",
                url: "admin/applicant-send-email",
                defaults: new { controller = "EmailAndSmsAmin", action = "EmailProcess", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "EmailProcessAdminStart",
                url: "admin/email-send-start",
                defaults: new { controller = "EmailAndSmsAmin", action = "EmailProcessStart", id = UrlParameter.Optional }
            );

            context.MapRoute(
              name: "EmailProcessAdminByType",
              url: "admin/email-send-type",
              defaults: new { controller = "EmailAndSmsAmin", action = "EmailProcessView", id = UrlParameter.Optional }
          );


            context.MapRoute(
                name: "EmailProcessSingleAdmin",
                url: "admin/applicant-send-email-single",
                defaults: new { controller = "EmailAndSmsAmin", action = "EmailProcessSingle", id = UrlParameter.Optional }
            );


            context.MapRoute(
                name: "EmailProcessSingleAdminCustom",
                url: "admin/applicant-send-email-single-custom",
                defaults: new { controller = "EmailAndSmsAmin", action = "EmailProcessCustomeSingle", id = UrlParameter.Optional }
            );

            context.MapRoute(
              name: "EmailSMSAmendmentAlertSend",
              url: "admin/send-amendment-email",
              defaults: new { controller = "EmailAndSmsAmin", action = "EmailSMSAmendmentAlert", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "EmailSMSGazetteMarksAlertSend",
                url: "admin/send-gazette-email",
                defaults: new { controller = "EmailAndSmsAmin", action = "EmailSMSGazetteMarksAlert", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "EmailSMSMeritMarksAlertSend",
                url: "admin/send-merit-consent-email",
                defaults: new { controller = "EmailAndSmsAmin", action = "EmailSMSMeritMarksAlert", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "EmailJoning",
                url: "admin/send-email-joining",
                defaults: new { controller = "EmailAndSmsAmin", action = "EmailSMSJoiningAlert", id = UrlParameter.Optional }
            );
            context.MapRoute(
                name: "EmailSMSSelectedCongratulationAlert",
                url: "admin/send-email-congratulation",
                defaults: new { controller = "EmailAndSmsAmin", action = "EmailSMSSelectedCongratulationAlert", id = UrlParameter.Optional }
            );

            context.MapRoute(
              name: "EmailSMSBulkSendingToRemaningOld",
              url: "admin/send-email-remaning-old",
              defaults: new { controller = "EmailAndSmsAmin", action = "EmailSMSBulkSendingToRemaning", id = UrlParameter.Optional }
            );

            //
            #endregion

            #region Contact

            context.MapRoute(
                 name: "ContactStatus",
                 url: "admin/contact-status",
                 defaults: new { controller = "ContactUsAdmin", action = "ContactStatus", id = UrlParameter.Optional }
             );

            context.MapRoute(
                 name: "ContactList",
                 url: "admin/contact/question-list",
                 defaults: new { controller = "ContactUsAdmin", action = "ContactListing", id = UrlParameter.Optional }
             );

            context.MapRoute(
                 name: "ContactReply",
                 url: "admin/contact/reply",
                 defaults: new { controller = "ContactUsAdmin", action = "ContactAnswer", id = UrlParameter.Optional }
             );

            #endregion


            #region Application Edit

            context.MapRoute(
                name: "EditExperienceAdmin",
                url: "admin/application-edit-experience",
                defaults: new { controller = "ApplicationUpdate", action = "ExperienceAdmin", id = UrlParameter.Optional }
            );

            #endregion


            #region Merit

            context.MapRoute(
                name: "AdminApplicantMeritView",
                url: "admin/applicant-merit-check",
                defaults: new { controller = "MeritAdmin", action = "ApplicantPreference", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "AdminApplicantMeritViewRound02",
               url: "admin/applicant-merit-check-round-02",
               defaults: new { controller = "MeritAdmin", action = "ApplicantMeritRound2", id = UrlParameter.Optional }
           );


            context.MapRoute(
                name: "AdminApplicantMeritViewRound03",
                url: "admin/applicant-merit-check-round-03",
                defaults: new { controller = "MeritAdmin", action = "ApplicantMeritRound3", id = UrlParameter.Optional }
            );


            context.MapRoute(
              name: "AdminInductionApplicantMeritViewRound02",
              url: "admin/applicant-merit-check-round-02-induction{induction}",
              defaults: new { controller = "MeritAdmin", action = "ApplicantMeritRound2", id = UrlParameter.Optional }
          );

            context.MapRoute(
             name: "AdminInductionApplicantMeritViewRound03",
             url: "admin/applicant-merit-check-round-03-induction-{induction}",
             defaults: new { controller = "MeritAdmin", action = "ApplicantMeritRound3", id = UrlParameter.Optional }
         );

            #endregion


            #region Feedback

            context.MapRoute(
                name: "AdminFeedbackList",
                url: "admin/feedback-list",
                defaults: new { controller = "FeedbackAdmin", action = "FeebBackList", id = UrlParameter.Optional }
            );

            #endregion


            #region Grievance

            #region Attendence

            context.MapRoute(
                    name: "ContactAttendenceVerificationCurrent",
                    url: "admin/attendence-verification-current",
                    defaults: new { controller = "ContactUsAdmin", action = "ContactAttendenceCurrent", id = UrlParameter.Optional }
                );

            context.MapRoute(
                    name: "ContactAttendenceGazetteCurrent",
                    url: "admin/attendence-gazette-current",
                    defaults: new { controller = "ContactUsAdmin", action = "ContactAttendenceCurrent", id = UrlParameter.Optional }
                );

            context.MapRoute(
                      name: "ContactAttendenceverification",
                      url: "admin/attendence-verification",
                      defaults: new { controller = "ContactUsAdmin", action = "ContactAttendence", id = UrlParameter.Optional }
                  );



            context.MapRoute(
                 name: "ContactAttendencegazette",
                 url: "admin/attendence-gazette",
                 defaults: new { controller = "ContactUsAdmin", action = "ContactAttendence", id = UrlParameter.Optional }
             );


            context.MapRoute(
                name: "PrintAttendenceVerification",
                url: "admin/print-attendence-verification",
                defaults: new { controller = "ContactUsAdmin", action = "ContactAttendenceView", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "PrintAttendenceGazette",
                url: "admin/print-attendence-gazette",
                defaults: new { controller = "ContactUsAdmin", action = "ContactAttendenceView", id = UrlParameter.Optional }
            );


            context.MapRoute(
                name: "AdminAttendenceMark",
                url: "admin/attendence-mark",
                defaults: new { controller = "ContactUsAdmin", action = "ContactAttendenceMark", id = UrlParameter.Optional }
            );

            #endregion

            #region Grievance Action/ Comments and Status

            context.MapRoute(
                name: "ContactStatusListCurrentVerification",
                url: "admin/attendence-list-verification",
                defaults: new { controller = "ContactUsAdmin", action = "ContactStatusListCurrent", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "ContactStatusListCurrentGazette",
                 url: "admin/attendence-list-gazette",
                defaults: new { controller = "ContactUsAdmin", action = "ContactStatusListCurrent", id = UrlParameter.Optional }
            );


            context.MapRoute(
               name: "ContactStatusListCurrentVerificationAll",
               url: "admin/attendence-list-all-verification",
               defaults: new { controller = "ContactUsAdmin", action = "ContactStatusListAll", id = UrlParameter.Optional }
           );

            context.MapRoute(
                name: "ContactStatusListCurrentGazetteAll",
                 url: "admin/attendence-list-all-gazette",
                defaults: new { controller = "ContactUsAdmin", action = "ContactStatusListAll", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "AdminGrievanceComments",
                url: "admin/contact-comments",
                defaults: new { controller = "ContactUsAdmin", action = "ContactAttendenceComment", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "ContactAttendencePrintverification",
               url: "admin/print-status-verification",
               defaults: new { controller = "ContactUsAdmin", action = "ContactStatusPrintList", id = UrlParameter.Optional }
           );

            context.MapRoute(
                 name: "ContactAttendencePrintgazette",
                 url: "admin/print-status-gazette",
                 defaults: new { controller = "ContactUsAdmin", action = "ContactStatusPrintList", id = UrlParameter.Optional }
             );



            #endregion

            context.MapRoute(
                name: "GrievanceAdminGazzette",
                url: "admin/grievance-gazzette",
                defaults: new { controller = "GrievanceAdmin", action = "GrievanceManage", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "GrievanceAdminAttendenceRemarks",
                url: "admin/grievance-attendence-remarks",
                defaults: new { controller = "GrievanceAdmin", action = "GrievanceAttendenceRemarks", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "GrievanceAdminMarksList",
                url: "admin/grievance-marks-list",
                defaults: new { controller = "GrievanceAdmin", action = "GrievanceGazetteMarksList", id = UrlParameter.Optional }
            );

            context.MapRoute(
              name: "GrievanceAdminMarksDetail",
              url: "admin/grievance-marks-detail",
              defaults: new { controller = "GrievanceAdmin", action = "GrievanceGazetteMarksDetail", id = UrlParameter.Optional }
          );


            context.MapRoute(
              name: "GrievanceAdminPrint",
              url: "admin/grievance-print",
              defaults: new { controller = "GrievanceAdmin", action = "GrievancePrint", id = UrlParameter.Optional }
          );

            #endregion

            #region Joining

            context.MapRoute(
                name: "ApplicantJoinedInduction",
                url: "admin/applicant-join-list",
                defaults: new { controller = "JoiningAdmin", action = "ApplicantJoined", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "ApplicantListFinalJoinList",
                url: "admin/applicant-join",
                defaults: new { controller = "JoiningAdmin", action = "ApplicantListFinal", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "ApplicantJoiningFullList",
               url: "admin/applicant-all-join",
               defaults: new { controller = "JoiningAdmin", action = "ApplicantJoiningFullList", id = UrlParameter.Optional }
           );

            context.MapRoute(
                   name: "ApplicantListFinalJoining",
                   url: "admin/applicant-join-add",
                   defaults: new { controller = "JoiningAdmin", action = "ApplicantTakeJoining", id = UrlParameter.Optional }
           );

            context.MapRoute(
                  name: "ApplicantJoiningListByInstitute",
                  url: "admin/joining-list-institute",
                  defaults: new { controller = "JoiningAdmin", action = "ApplicantJoiningListHospInst", id = UrlParameter.Optional }
          );

            context.MapRoute(
                 name: "ApplicantJoiningListByHospital",
                 url: "admin/joining-list-hospital",
                 defaults: new { controller = "JoiningAdmin", action = "ApplicantJoiningListHospInst", id = UrlParameter.Optional }
         );


            #endregion

            #region Resignation

            context.MapRoute(
                  name: "ActionSetupResignation",
                  url: "admin/reg-term-listing",
                  defaults: new { controller = "ApplicantAction", action = "ActionLisiting", id = UrlParameter.Optional }
              );

            context.MapRoute(
                name: "ActionLisitingResignation",
                url: "admin/reg-term-setup",
                defaults: new { controller = "ApplicantAction", action = "ActionSetup", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "ActionSetupFreez",
                url: "admin/freez-listing",
                defaults: new { controller = "ApplicantAction", action = "ActionLisiting", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "ActionLisitingFreez",
                url: "admin/freez-setup",
                defaults: new { controller = "ApplicantAction", action = "ActionSetup", id = UrlParameter.Optional }
            );

            #endregion

            #region Hardship
            context.MapRoute(
               name: "ApplicantDetail",
               url: "admin/hardship-applicant-status",
               defaults: new { controller = "Hardship", action = "ApplicantDetail", id = UrlParameter.Optional }
           );

            #endregion

            #region SMS 

            context.MapRoute(
               name: "SMSAdminSearch",
               url: "admin/sms-search",
               defaults: new { controller = "SMSAdmin", action = "SearchSms", id = UrlParameter.Optional }
           );


            //

            #endregion

            #region Master Setup


            context.MapRoute(
              name: "AdminRegionManage",
              url: "admin/region-manage",
              defaults: new { controller = "MasterSetups", action = "RegionManage", id = UrlParameter.Optional }
          );

            context.MapRoute(
                name: "AdminRegionSetup",
                url: "admin/region-setup",
                defaults: new { controller = "MasterSetups", action = "RegionSetup", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "AdminResearchJournalApplicantList",
               url: "admin/research-journal-applicants",
               defaults: new { controller = "JournalAdmin", action = "JournalApplicants", id = UrlParameter.Optional }
           );

            context.MapRoute(
                name: "AdminResearchJournalManage",
                url: "admin/research-journal-manage",
                defaults: new { controller = "JournalAdmin", action = "JournalManage", id = UrlParameter.Optional }
            );

            context.MapRoute(
                name: "AdminResearchJournalSetup",
                url: "admin/research-journal-setup",
                defaults: new { controller = "JournalAdmin", action = "JournalSetup", id = UrlParameter.Optional }
            );

            #endregion

            #region Ticker

            context.MapRoute(
               name: "AdminTickerSetup",
               url: "admin/ticker-setup",
               defaults: new { controller = "CommonAdmin", action = "TickerSetup", id = UrlParameter.Optional }
           );

            context.MapRoute(
                 name: "AdminTickerManage",
                 url: "admin/ticker-manage",
                 defaults: new { controller = "CommonAdmin", action = "TickerManage", id = UrlParameter.Optional }
             );
            #endregion


            #region Print

            context.MapRoute(
                  name: "PrintApplicantListJoiningInstitute",
                  url: "admin/print-applicant-join-institute",
                  defaults: new { controller = "PrintAdmin", action = "PrintJoiningApplicantInstitute", id = UrlParameter.Optional }
              );


            context.MapRoute(
                name: "PrintApplicantAttachedHospital",
                url: "admin/print-applicant-attach-hospital",
                defaults: new { controller = "PrintAdmin", action = "PrintApplicantAttachedHospital", id = UrlParameter.Optional }
            );

            #endregion


            #region Reports

            context.MapRoute(
                name: "ReportRptSeatsStatus",
                url: "admin/report-seat-status",
                defaults: new { controller = "Reportss", action = "SeatsReport", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "ReportJoinedApplicantHospital",
               url: "admin/report-applicant-hospital",
               defaults: new { controller = "Reportss", action = "JoinedApplicantHospital", id = UrlParameter.Optional }
           );


            context.MapRoute(
                name: "ReportJoinedApplicantHospitalStatus",
                url: "admin/report-applicant-hospital-status",
                defaults: new { controller = "Reportss", action = "JoinedApplicantHospitalStatus", id = UrlParameter.Optional }
            );

            context.MapRoute(
               name: "TraineeAttachReportHospitalWise",
               url: "admin/hospital-trainee-status",
               defaults: new { controller = "Reportss", action = "TraineeAttachReportHospitalWise", id = UrlParameter.Optional }
           );




            #endregion

            context.MapRoute(
                "nadmin_default",
                "admin/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}