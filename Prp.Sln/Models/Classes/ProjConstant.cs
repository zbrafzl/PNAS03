using Prp.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace Prp.Sln
{
    public static class ProjConstant
    {
        //public static int inductionId = WebConfigurationManager.AppSettings["InductionId"].TooInt();
        public static int inductionId = 15;
        public static int phaseId = WebConfigurationManager.AppSettings["PhaseId"].TooInt();
        public static int consentRound = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();
        public static string preUrl = WebConfigurationManager.AppSettings["PreUrl"].TooString();

        public static int emailInterval = WebConfigurationManager.AppSettings["emailInterval"].TooInt();
        public static int smsInterval = WebConfigurationManager.AppSettings["smsInterval"].TooInt();

        public static class DDL
        {
            public static string constant = "constant";
            public static string induction = "induction";
            public static string menu = "menu";
            public static string region = "region";

            public static string institute = "institute";
            public static string hospital = "hospital";
            public static string department = "department";
            public static string unit = "unit";

            public static string employee = "employee";



            public static string program = "program";
            public static string discipline = "discipline";
            public static string speciality = "speciality";


            public static class Institute
            {
                public static string empty = "";
                public static string getAll = "GetAll";
                public static string remaningInstitute = "RemaningInstitute";
                public static string byHospital = "ByHospital";
                public static string hasHospital = "HasHospital";
                public static string hasHospitalByUserId = "HasHospitalByUserId";
                public static string getGovtInstitute = "GetGovtInstitute";

                public static string getInstituteByTypeAndProvince = "GetInstituteByTypeAndProvince";

                public static string hasJoinedApplicant = "HasJoinedApplicant";

            }
        }

        public static class config
        {
            public static class regions
            {
                public static int country = 1;
                public static int province = 2;
                public static int division = 3;
                public static int distric = 4;
                public static int tehsil = 5;
            }
            public static class section
            {
                public static string hospitalDept = "HospitalDept";
                public static string definition = "Definition";
                public static string hospitalStation = "HospitalStation";
                public static string sationStatus = "SationStatus";
                public static string stationWorkingStatus = "StationWorkingStatus";
            }
        }
        public static class Constant
        {
            public static int userType = 1;
            public static int department = 2;
            public static int designantion = 3;
            public static int region = 6;
            public static int degree = 11;
            public static int degreeType = 12;
            public static int instituteType = 21;
            public static int instituteLevel = 22;
            public static int primaryLevel = 23;
            public static int secondaryLevel = 24;

            public static int metalTiers = 33;
            public static int journalDispline = 34;
            public static int journalImpactFactorType = 35;
            public static int guardianType = 36;
            public static int regionType = 37;
            public static int tickerType = 38;

            public static int grievanceType = 46;
            public static int appearanceType = 47;

            public static int consentType = 65;

            public static int appType = 70;
            public static int menuType = 71;

            public static int qoutaType = 41;
            public static int statusApplicant = 51;
            public static int statusApplicantAccount = 52;
            public static int statusApplicantApplication = 53;

            public static int smsType = 104;
            public static int emailTemplate = 106;
            public static int emailType = 108;

            public static int pakistan = 132;
            public static int grievanceStatusType = 141;
            public static int grievanceActionType = 151;

            public static int actionStatus = 600;
            public static int applicationAction = 601;
            public static int leaveType = 611;

            public static int experienceEmployee = 711;

            public static int bedsApprovalType = 801;

            public static int currentStatus = 201;

            public static class UserType
            {

                public static int seniorDeveloper = 1;
                public static int juniorDeveloper = 2;
                public static int phfITManager = 11;
                public static int callCenter = 16;
                public static int phfAccountant = 21;

                public static int keSenior = 30;
                public static int keVerification = 31;
                public static int keJournalTeam = 32;

                public static int verfication = 41;

                public static int hospital = 81;
                public static int institute = 86;

                public static int bank = 101;

                public static int applicant = 132;
            }


            public static class InstituteType
            {
                public static int govt = 1;
                public static int privatee = 2;

            }

            public static class Country
            {
                public static int pakistan = 132;

            }
            public static class Region
            {
                public static int country = 1;
                public static int province = 2;
                public static int division = 3;
                public static int distric = 4;
                public static int tehsil = 5;
            }

            public static class GrievanceType
            {
                public static int verification = 1;
                public static int gazatMarks = 6;

            }

            public static class Condition
            {

                public static string GetProvincePakistan = "GetProvincePakistan";
                public static string GetAllCountry = "GetAllCountry";
            }

            public static class ApplicationStatus
            {
                public static int profile = 1;
                public static int education = 2;
                public static int experience = 3;
                public static int researchPaper = 4;
                public static int specility = 5;
                public static int paymentDone = 6;
                public static int proofReading = 7;
                public static int completed = 8;

            }

            public static class ApplicationStatusType
            {
                public static int account = 52;
                public static int accountType = 54;
                public static int application = 53;
                public static int voucherPhf = 73;
                public static int voucherBank = 72;
                public static int grievanceAttendence1st = 126;
                public static int verification = 131;
                public static int grievanceVerificationStatus = 136;
                public static int ammendment = 132;
                public static int validApplication = 138;
                public static int round01 = 181;
                public static int round02 = 184;
                public static int round03 = 188;
                public static int round04 = 190;
                public static int join = 191;
                public static int debar = 195;

            }

            public static class ActionStatusType
            {
                public static int Resignation = 1;
                public static int Termination = 6;
                public static int freez = 11;
            }

        }

        public static class EmailTemplateType
        {
            public static int registration = 1;
            public static int forgotPassword = 6;
            public static int applicationCompleted = 11;

            public static int voucherValid = 21;
            public static int voucherInValid = 26;

            public static int varificationAccepted = 31;
            public static int varificationRejected = 36;

            public static int ammendmentAccepted = 41;
            public static int ammendmentRejected = 46;

            public static int consentRound01 = 61;
            public static int consentRound02 = 66;
            public static int consentRound03 = 71;

            public static int joining = 91;

            public static int debar = 96;

            public static int grVerificationQuestion = 111;
            public static int grVerificationAnswer = 116;
            public static int grGazetterQuestion = 141;
            public static int grGazetterAnswer = 146;
            public static int feedbackQuestion = 151;
            public static int feedbackAnswer = 156;

            public static int contactUsQuestion = 301;
            public static int contactUsAnswer = 306;



        }

        public static class SMSType
        {

            public static int registration = 1;
            public static int forgotPassword = 2;
            public static int sendPasswordAdmin = 3;
            public static int applicationCompleted = 11;


            public static int applicationApproved = 21;
            public static int applicationReject = 26;


            public static int amendmentApproved = 31;
            public static int amendmentReject = 36;


            public static int round01 = 71;
            public static int round02 = 73;
            public static int round03 = 76;

        }

        public static class SMS
        {
            public static class Path
            {
                public static string smsPassword = "/html/smsPassword.html";
            }
        }

        public static class Email
        {
            public static class Path
            {
                public static string smsPassword = "/html/smsPassword.html";

                public static string registration = "/html/registration.html";

                public static string amendmentApprove = "/html/amendmentApprove.html";

                public static string verificationAccept = "/html/verificationAccept.html";
                public static string verificationReject = "/html/verificationReject.html";

                public static string question = "/html/question.html";
                public static string replyQuestion = "/html/ReplyQuestion.html";

                public static string grievancesGazette = "/html/grievancesGazette.html";

                public static string meritListConsent = "/html/meritListConsent.html";

                public static string joiningApplicant = "/html/joiningAlert.html";

                public static string congratulationApplicant = "/html/congratulationAlert.html";


                public static string wrongPreferenceDelete = "/html/wrongPreferenceDelete.html";

                public static string openQuotaPreferenceDelete = "/html/openQuotaPreferenceDelete.html";

                public static string amendmentReject = "/html/amendmentReject.html";


                public static string feedbackReply = "/html/feedbackReply.html";

                public static string forgotPassword = "/html/forgotPassword.html";
                public static string application = "/html/application.html";
                public static string applicantProfile = "/html/applicantProfile.html";
                public static string applicantDegree = "/html/applicantDegree.html";
                public static string applicantCertification = "/html/applicantCertification.html";
                public static string applicantNational = "/html/applicantNational.html";
                public static string applicantNEB = "/html/applicantNEB.html";
                public static string applicantExperience = "/html/applicantExperience.html";
                public static string applicantDistinction = "/html/applicantDistinction.html";
                public static string applicantResearchPaper = "/html/applicantResearchPaper.html";

                public static string applicantSpecialityHeader = "/html/applicantSpecialityHeader.html";
                public static string applicantSpeciality = "/html/applicantSpeciality.html";

                public static string applicantAggrigate = "/html/applicantAggrigate.html";
                public static string applicantVoucher = "/html/applicantVoucher.html";
            }

            public static class Subject
            {
                public static string registration = "Registration";
                public static string forgotPassword = "Forgot Password";

                public static string contactUs = "Contact Us";

                public static string verification = "PRP Application Verification";

                public static string feedbackReply = "PRP Feedback";

                public static string amendmentProcess = "PRP Application Amendment Process";

                public static string grievancesGazette = "PRP Grievances - Gazette Marks";

                public static string meritList = "PRP Merit List";

                public static string joining = "PRP Joinig";

                public static string congratulation = "PRP congratulation";


                public static string applicationCompleted = "Application Completed";
            }

            public static class Title
            {
                public static string registration = "PNAS";
                public static string forgotPassword = "PNAS";


                public static string verification = "PNAS";

                public static string amendmentProcess = "PNAS";

                public static string applicationCompleted = "PNAS";
            }

        }

        public static class Cookies
        {
            public static string loggedInAdmin = "Prp.loggedInAdmin";
            public static string loggedInApplicant = "Prp.loggedInApplicant";
            public static string loggedInAdminConstant = "Prp.loggedInAdminConstant";
            public static string loggedInAdminConfig = "Prp.loggedInAdminConfig";
        }
        public static class path
        {
            public static string regionAllData = "/ExtraFiles/regionAllData.html";
        }

    }
    public static class ProjFunctions
    {

        #region Admin

        public static void CookiesAdminSet(User ss)
        {
            HttpCookie cookie = new HttpCookie(ProjConstant.Cookies.loggedInAdmin);
            cookie["userId"] = ss.userId.TooString();
            cookie["firstName"] = ss.firstName.TooString();
            cookie["lastName"] = ss.lastName.TooString();
            cookie["emailId"] = ss.emailId.TooString();
            cookie["typeId"] = ss.typeId.TooString();
            cookie["displayName"] = ss.displayName.TooString();
            cookie["reffId"] = ss.reffId.TooString();
            cookie.Expires = DateTime.Now.AddHours(12);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static User CookiesAdminGet()
        {
            User user = new User();
            try
            {
                HttpCookie loggedInAdmin = HttpContext.Current.Request.Cookies[ProjConstant.Cookies.loggedInAdmin];
                if (loggedInAdmin != null)
                {
                    user.userId = loggedInAdmin["userId"].TooInt();
                    user.emailId = loggedInAdmin["emailId"].TooString();
                    user.firstName = loggedInAdmin["firstName"].TooString();
                    user.lastName = loggedInAdmin["lastName"].TooString();
                    user.typeId = loggedInAdmin["typeId"].TooInt();

                    try
                    {
                        user.displayName = loggedInAdmin["displayName"].TooString();
                    }
                    catch (Exception)
                    {
                        user.displayName = "";
                    }

                    try
                    {
                        user.reffId = loggedInAdmin["reffId"].TooInt();
                    }
                    catch (Exception)
                    {
                        user.reffId = 0;
                    }

                }
                else
                {
                    user = null;
                }
            }
            catch (Exception)
            {
                user = null;
            }
            return user;
        }

        #endregion

        #region Applicant

        public static void CookieApplicantSet(Applicant ss)
        {
            HttpCookie cookie = new HttpCookie(ProjConstant.Cookies.loggedInApplicant);
            cookie["applicantId"] = ss.applicantId.TooString();
            cookie["name"] = ss.name.TooString();
            cookie["pmdcNo"] = ss.pmdcNo.TooString();
            cookie["pic"] = ss.pic.TooString();
            cookie["emailId"] = ss.emailId.TooString();
            cookie["adminId"] = ss.adminId.TooString();
            cookie.Expires = DateTime.Now.AddHours(12);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static Applicant CookieApplicantGet()
        {
            Applicant user = new Applicant();
            try
            {
                HttpCookie loggedInAdmin = HttpContext.Current.Request.Cookies[ProjConstant.Cookies.loggedInApplicant];
                if (loggedInAdmin != null)
                {
                    user.applicantId = loggedInAdmin["applicantId"].TooInt();
                    user.emailId = loggedInAdmin["emailId"].TooString();
                    user.name = loggedInAdmin["name"].TooString();
                    user.adminId = loggedInAdmin["adminId"].TooInt();
                    user.pic = loggedInAdmin["pic"].TooString();
                    try
                    {
                        user.pmdcNo = loggedInAdmin["pmdcNo"].TooString();
                    }
                    catch (Exception)
                    {
                    }
                    if (String.IsNullOrWhiteSpace(user.pic))
                    {
                        user.pathProfilePic = "/Images/pic.jpg";
                    }
                    else
                    {
                        try
                        {
                            string imagePath = "/Images/Applicant/" + user.applicantId.TooString() + "/" + user.pic;
                            string completeUlr = imagePath.GetServerPathFolder();

                            if (File.Exists(completeUlr))
                            {
                                user.pathProfilePic = imagePath;
                            }
                            else
                                user.pathProfilePic = "/Images/pic.jpg";
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
                else
                {
                    user = null;
                }
            }
            catch (Exception)
            {
                user = null;
            }
            return user;
        }


        #endregion

        public static void RemoveCookies(string name)
        {
            HttpCookie cookie = HttpContext.Current.Response.Cookies[name];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Response.Cookies.Set(cookie);
            }
        }

        public static string GetCaptchaText()
        {
            StringBuilder randomText = new StringBuilder();
            string alphabets = "012345679ACEFGHKLMNPRSWXZabcdefghijkhlmnopqrstuvwxyz";
            Random r = new Random();
            for (int j = 0; j <= 5; j++)
            {
                randomText.Append(alphabets[r.Next(alphabets.Length)]);
            }
            return randomText.ToString();
        }

    }
}