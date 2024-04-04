using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prp.Sln
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            string preUrl = ProjConstant.preUrl;

            #region Without Login

            routes.MapRoute(
                name: "index",
                url: "",
                defaults: new { controller = "LoggedIn", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "LoggedIn", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "LoginMigrant",
                url: "loginMigrant",
                defaults: new { controller = "LoggedIn", action = "LoginMigrant", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Register",
                url: "register",
                defaults: new { controller = "LoggedIn", action = "Register", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "RegisterComplete",
                url: "register-complete",
                defaults: new { controller = "LoggedIn", action = "RegisterComplete", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "RegisterVerify",
                url: "registration-verify",
                defaults: new { controller = "LoggedIn", action = "RegisterVerify", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "VerifyRegister",
                url: "verify-registration",
                defaults: new { controller = "LoggedIn", action = "VerifyRegister", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ForgotPassword",
                url: "forgot-password",
                defaults: new { controller = "LoggedIn", action = "ForgotPassword", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "logout",
                url: "logout",
                defaults: new { controller = "LoggedIn", action = "Logout", id = UrlParameter.Optional }
            );

            #endregion
            routes.MapRoute(
               name: "HomePage",
               url: "home",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
        );

            


            #region Applicant Profile Bulider

            routes.MapRoute(
               name: "ProfileProcessCreate",
               url: "applicant-profile-create",
               defaults: new { controller = "ApplicantProfile", action = "ProfileBuilder", id = UrlParameter.Optional }
           );


            routes.MapRoute(
                name: "ProfileProcess",
                url: "applicant-profile",
                defaults: new { controller = "ApplicantProfile", action = "ProfileProcess", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EducationProcess",
                url: "applicant-education",
                defaults: new { controller = "ApplicantProfile", action = "EducationProcess", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ExprienceProcess",
                url: "applicant-exprience",
                defaults: new { controller = "ApplicantProfile", action = "ExprienceProcess", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ResearchPaperProcess",
                url: "applicant-research-paper",
                defaults: new { controller = "ApplicantProfile", action = "ResearchPaperProcess", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SpecialityProcess",
                url: "applicant-speciality",
                defaults: new { controller = "ApplicantProfile", action = "SpecialityProcess", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "MigrantPreferences",
                url: "migrant-preferences",
                defaults: new { controller = "LoggedIn", action = "AddMigrationPreferences", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "MigrantProofReading",
                url: "migrant-proof-reading",
                defaults: new { controller = "LoggedIn", action = "MigrantProofReading", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "ProofReadingProcess",
                url: "applicant-proof-reading",
                defaults: new { controller = "ApplicantProfile", action = "ProofReadingProcess", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ApplicationVoucher",
                url: "applicant-voucher",
                defaults: new { controller = "ApplicantProfile", action = "ApplicationVoucher", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ProofReadingView",
                url: "profile-view",
                defaults: new { controller = "ApplicantProfile", action = "ProofReadingView", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "ProofReadingViewPrint",
                url: "profile-view-print",
                defaults: new { controller = "ApplicantProfile", action = "ApplicationViewAnPrint", id = UrlParameter.Optional }
            );




            routes.MapRoute(
                name: "ApplicationStatusView",
                url: "application-status",
                defaults: new { controller = "ApplicantProfile", action = "ApplicationStatusView", id = UrlParameter.Optional }
            );


            routes.MapRoute(
                name: "ApplicationDistinctionAgain",
                url: "complted/applicant-distinction",
                defaults: new { controller = "ApplicantProfile", action = "ApplicationDistinctionAgain", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "ApplicationProcessComplete",
               url: "application-completed",
               defaults: new { controller = "ApplicantProfile", action = "ApplicationProcessComplete", id = UrlParameter.Optional }
           );


            routes.MapRoute(
                name: "ApplicationListView",
                url: "application-view",
                defaults: new { controller = "ApplicantProfile", action = "ApplicationListView", id = UrlParameter.Optional }
            );


            routes.MapRoute(
             name: "ApplicantMeritView",
             url: preUrl + "/applicant-merit",
             defaults: new { controller = "ApplicantProfile", action = "ApplicantMerit", id = UrlParameter.Optional }
         );

            #endregion

            #region Application Profile Page by Admin

            routes.MapRoute(
              name: "ApplicationUpdateByAdmin",
              url: "ad/application",
              defaults: new { controller = "ApplicationAdmin", action = "ApplicationUpdate", id = UrlParameter.Optional }
          );

            routes.MapRoute(
                name: "ProfileProcessAdmin",
                url: "ad/applicant-profile",
                defaults: new { controller = "ApplicationAdmin", action = "ProfileProcess", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EducationProcessAdmin",
                url: "ad/applicant-education",
                defaults: new { controller = "ApplicationAdmin", action = "EducationProcess", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ExprienceProcessAdmin",
                url: "ad/applicant-exprience",
                defaults: new { controller = "ApplicationAdmin", action = "ExprienceProcess", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "ResearchPaperProcessAdmin",
                url: "ad/applicant-research-paper",
                defaults: new { controller = "ApplicationAdmin", action = "ResearchPaperProcess", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "SpecialityProcessAdmin",
                url: "ad/applicant-speciality",
                defaults: new { controller = "ApplicationAdmin", action = "SpecialityProcess", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "ApplicationVoucherAdmin",
               url: "ad/applicant-voucher",
               defaults: new { controller = "ApplicationAdmin", action = "ApplicationVoucher", id = UrlParameter.Optional }
           );

            #endregion

            #region contect Pages
            routes.MapRoute(
               name: "PrivatePolicy",
               url: "private-policy",
               defaults: new { controller = "ContentPage", action = "PrivatePolicy", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "TermAndCondition",
                url: "term-and-condition",
                defaults: new { controller = "ContentPage", action = "TermAndCondition", id = UrlParameter.Optional }
            );

            routes.MapRoute(
               name: "Notifications",
               url: "notification",
               defaults: new { controller = "ContentPage", action = "Notifications", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "EmailTest",
                url: "email-test",
                defaults: new { controller = "ContentPage", action = "TestEmail", id = UrlParameter.Optional }
            );
            routes.MapRoute(
              name: "TestEmailCredentials",
              url: "email-test-credentials",
              defaults: new { controller = "ContentPage", action = "TestEmailCredentials", id = UrlParameter.Optional }
          );

            routes.MapRoute(
               name: "TestSMS",
               url: "test-sms",
               defaults: new { controller = "ContentPage", action = "TestSMS", id = UrlParameter.Optional }
           );

            #endregion

            #region Contact Us, FeedBack Gazette etc

            routes.MapRoute(
               name: "ContactUs",
               url: "contactus",
               defaults: new { controller = "LoggedIn", action = "ConactUs", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "ContactUsAfterLogin",
               url: "contact-us",
               defaults: new { controller = "Feedbacks", action = "ConactUs", id = UrlParameter.Optional }
           );
            //contact-detail

            routes.MapRoute(
              name: "ContactUsDetail",
              url: "contact-detail",
              defaults: new { controller = "Feedbacks", action = "ConactUsDetail", id = UrlParameter.Optional }
          );

            routes.MapRoute(
               name: "ApplicantFeedBack",
               url: preUrl + "/applicant/feedback",
               defaults: new { controller = "Feedbacks", action = "FeedBackByApplicant", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "GrievanceGazzetteView",
                url: preUrl + "/grievance-gazzette",
                defaults: new { controller = "Feedbacks", action = "GrievanceGazzette", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "GrievanceApplicationverification",
                url: preUrl + "/grievance-verification-application",
                defaults: new { controller = "Feedbacks", action = "GrievanceApplication", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "GrievanceApplicationgazette",
               url: preUrl + "/grievance-gazette-application",
               defaults: new { controller = "Feedbacks", action = "GrievanceApplication", id = UrlParameter.Optional }
           );

            #endregion

            #region Gazette

            routes.MapRoute(
                name: "MeritGazatFCPS",
                url: preUrl + "/rejected-list",
                defaults: new { controller = "MeritGazat", action = "GazatFCPS", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "MeritGazette",
                url: preUrl + "/gazette",
                defaults: new { controller = "MeritGazat", action = "Gazette", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "MeritListMale",
                url: preUrl + "/meritlist-male",
                defaults: new { controller = "MeritGazat", action = "MeritList", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "MeritListFemale",
                url: preUrl + "/meritlist-female",
                defaults: new { controller = "MeritGazat", action = "MeritListFemale", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "MeritListMaleEvening",
                url: preUrl + "/meritlist-male-evening",
                defaults: new { controller = "MeritGazat", action = "MeritListEvening", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "MeritListFemaleEvening",
                url: preUrl + "/meritlist-female-evening",
                defaults: new { controller = "MeritGazat", action = "MeritListFemaleEvening", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                 name: "MeritGazatMS",
                 url: preUrl + "/gazatte-ms",
                 defaults: new { controller = "MeritGazat", action = "GazatMS", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "MeritGazatMD",
                url: preUrl + "/gazatte-md",
                defaults: new { controller = "MeritGazat", action = "GazatMD", id = UrlParameter.Optional }
           );

            routes.MapRoute(
               name: "MeritGazatMDS",
               url: preUrl + "/gazatte-mds",
               defaults: new { controller = "MeritGazat", action = "GazatMDS", id = UrlParameter.Optional }
            );


            routes.MapRoute(
              name: "MeritGazatFCPSD",
              url: preUrl + "/gazatte-fcpsd",
              defaults: new { controller = "MeritGazat", action = "GazatFCPSD", id = UrlParameter.Optional }
            );

            #endregion

            #region Merit List

            routes.MapRoute(
           name: "FCPSMeritListRound",
           url: preUrl + "/merit/list-fcps",
           defaults: new { controller = "MeritGazat", action = "MeritFCPS", id = UrlParameter.Optional }
       );

            routes.MapRoute(
            name: "MSMeritListRound",
            url: preUrl + "/merit/list-ms",
            defaults: new { controller = "MeritGazat", action = "MeritMS", id = UrlParameter.Optional }
        );

            routes.MapRoute(
           name: "MDMeritListRound",
           url: preUrl + "/merit/list-md",
           defaults: new { controller = "MeritGazat", action = "MeritMD", id = UrlParameter.Optional }
       );

            routes.MapRoute(
           name: "MDSMeritListRound",
           url: preUrl + "/merit/list-mds",
           defaults: new { controller = "MeritGazat", action = "MeritMDS", id = UrlParameter.Optional }
       );

            routes.MapRoute(
                name: "FCPSDMeritListRound",
                url: preUrl + "/merit/list-fcpsd",
                defaults: new { controller = "MeritGazat", action = "MeritFCPSD", id = UrlParameter.Optional }
            );

            #endregion

            #region Conest and Joining

            routes.MapRoute(
                 name: "ApplicantConsent",
                 url: preUrl + "/applicant/consent",
                 defaults: new { controller = "ConsentUI", action = "ApplicantConsent", id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "ApplicantJoining",
                url: preUrl + "/applicant/joining",
                defaults: new { controller = "ConsentUI", action = "ApplicantJoining", id = UrlParameter.Optional }
            );



            #endregion

            #region Print Pages

            routes.MapRoute(
               name: "print_voucher",
               url: "print/voucher",
               defaults: new { controller = "Prints", action = "Vouchers", id = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "print_gr_Verification",
              url: "print/grievance-verification",
              defaults: new { controller = "Prints", action = "GrievanceGazzettePrint", id = UrlParameter.Optional }
          );

            routes.MapRoute(
           name: "print_gr_gazzette",
           url: "print/grievance-gazzette",
           defaults: new { controller = "Prints", action = "GrievanceGazzettePrint", id = UrlParameter.Optional }
       );

            routes.MapRoute(
                name: "print_application",
                url: "print/application",
                defaults: new { controller = "Prints", action = "Application", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "print_apptest",
                url: "print/application-test",
                defaults: new { controller = "Prints", action = "ApplicationTest", id = UrlParameter.Optional }
            );

            #endregion

            routes.MapRoute(
               name: "ChangePassword",
               url: "change-password",
               defaults: new { controller = "Home", action = "ChangePassword", id = UrlParameter.Optional }
           );

            routes.MapRoute(
            name: "GalleryLayout",
            url: "gallery-images",
            defaults: new { controller = "LoggedIn", action = "GalleryLayout", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "ApplyGrievnce",
            url: "apply-grievance",
            defaults: new { controller = "LoggedIn", action = "applyGrievance", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "ApplyFscGrievnce",
            url: "apply-fsc-grievance",
            defaults: new { controller = "LoggedIn", action = "FscGrievance", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "GiveConsent",
            url: "give-consent",
            defaults: new { controller = "LoggedIn", action = "GiveConsent", id = UrlParameter.Optional }
        );
            routes.MapRoute(
            name: "TestRighsPage",
            url: "test-rights",
            defaults: new { controller = "Home", action = "TestRighsPage", id = UrlParameter.Optional }
        );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
