using Newtonsoft.Json;
using Prp.Data;
using Prp.Data.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
    public class ApplicantProfileController : BaseController
    {
        #region Views

        public ActionResult  ProfileBuilder()
            {
            DataTable dtCheckStatus = new DataTable();
            string query = "Select top(1) tas.applicationStatusId, tas.inductionId, tas.applicantId,tas.statusTypeId, tas.statusId, tas.dated , a.facultyId, a.genderID, a.levelId, a.pncNo, a.contactNumber from tblApplicationStatus tas inner join tblApplicant a on tas.applicantId = a.applicantId where a.applicantId = " + loggedInUser.applicantId + " order by dated DESC";
            SqlConnection con = new SqlConnection();
            Message msg = new Message();
            SqlCommand cmd = new SqlCommand(query);
            try
            {
                con = new SqlConnection(PrpDbConnectADO.Conn);
                con.Open();
                cmd.Connection = con;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtCheckStatus);
                cmd.ExecuteNonQuery();

                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            finally
            {
                con.Close();
            }

            DataRow dr = dtCheckStatus.Rows[0];
            Session["degreeAchieved"] = dr[6].ToString();
            ProjConstant.Constant.statusApplicantApplication = Convert.ToInt32(dr[3]);
            #region Profile Model


            ProfileModel model = new ProfileModel();
            model.listProvince = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.province
                , ProjConstant.Constant.pakistan);

            model.listCountry = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.country
              , 0, ProjConstant.Constant.Condition.GetAllCountry);
            model.countryJson = JsonConvert.SerializeObject(model.listCountry);

            model.listDegree = new ConstantDAL().GetAll(ProjConstant.Constant.degree);

            model.listInstituteType = new ConstantDAL().GetAll(ProjConstant.Constant.instituteType);
            model.listInstituteLevel = new ConstantDAL().GetAll(ProjConstant.Constant.instituteLevel);
            model.listInstitute = new InstitueDAL().GetAll(ProjConstant.Constant.InstituteType.govt);
            model.listSpeciality = new SpecialityDAL().GetAll();
            model.listDiscipline = new CommonDAL().DisciplineGetAll();

            #endregion

            try
            {
                model.degreeAcheived = dr[6].ToString();
                Session["genderID"] = dr[7].ToString();
                model.levelId = dr[8].ToString();
                Session["appliedFor"] = model.levelId.ToString();
                Session["degreeAchieved"] = model.degreeAcheived.ToString();
                ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                          , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);
                if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.profile)
                {
                    Session["applicantExist"] = 0;
                    Session["Name"] = loggedInUser.name;
                    Session["email"] = loggedInUser.emailId;
                    Session["pncNumber"] = dr["pncNo"].ToString();
                    Session["contactNumber"] = dr["contactNumber"].ToString();
                    Session["applicantExist"] = 0;
                    Session["degreeAchieved"] = model.degreeAcheived.ToString();
                    return View("ProfileProcess", model);
                }
                else if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.education)
                {
                    Session["degreeAchieved"] = model.degreeAcheived.ToString();
                    return View("EducationProcess", model);
                }
                else if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.experience)
                {
                    Session["degreeAchieved"] = model.degreeAcheived.ToString();
                    return View("ExprienceProcess", model);
                }
                else if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.researchPaper)
                {
                    Session["degreeAchieved"] = model.degreeAcheived.ToString();
                    return View("SpecialityProcess", model);
                    //return View("ResearchPaperProcess", model);
                }
                else if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.specility)
                {
                    Session["degreeAchieved"] = model.degreeAcheived.ToString();
                    return View("SpecialityProcess", model);
                }
                else if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.paymentDone)
                {
                    Session["degreeAchieved"] = model.degreeAcheived.ToString();
                    ApplicantVoucherModel voucher = new ApplicantVoucherModel();
                    return View("ApplicationVoucher", voucher);
                }
                else if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.proofReading)
                {
                    Session["degreeAchieved"] = model.degreeAcheived.ToString();
                    ProofReadingModel modelProof = GenerateModelProofReading();
                    return View("ProofReadingProcess", modelProof);
                }
                else if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.completed)
                {
                    Session["degreeAchieved"] = model.degreeAcheived.ToString();
                    ProofReadingModel modelProof = GenerateModelProofReading();
                    return View("ApplicationViewAnPrint", modelProof);
                }
                else
                {
                    return View("ProfileProcess", model);
                }
            }
            catch (Exception)
            {
                return View("ProfileProcess", model);
            }
        }

        public JsonResult removeBsc(int applicantId)
        {
            Message message = new Message();
            string query = "delete from tblNursingApplicantDegreeData where degreeTypeID = 6 and applicantID = "+applicantId+"";
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand(query);
            try
            {
                con = new SqlConnection(PrpDbConnectADO.Conn);
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                message.status = true;
                message.message = "Removed BSc Degree";
            }
            catch (Exception ex)
            {
                message.status = false;
                message.msg = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult removeMsc(int applicantId)
        {
            Message message = new Message();
            string query = "delete from tblNursingApplicantDegreeData where degreeTypeID = 7 and applicantID = " + applicantId + "";
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand(query);
            try
            {
                con = new SqlConnection(PrpDbConnectADO.Conn);
                con.Open();
                cmd.Connection = con;
                cmd.ExecuteNonQuery();
                message.status = true;
                message.message = "Removed MSc Degree";
            }
            catch (Exception ex)
            {
                message.status = false;
                message.msg = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProfileProcess()
        
        {
            //Session["applicantExist"] = 0;
            ProfileModel model = new ProfileModel();
            string picSource = "";
            try
            {
                ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                     , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();
                int facultyId = loggedInUser.facultyId;

                DataTable dtCheckStatus = new DataTable();
                string query = "Select top(1) tas.applicationStatusId, tas.inductionId, tas.applicantId,tas.statusTypeId, tas.statusId, tas.dated , a.facultyId,  a.levelId, a.pncNo, a.pic, a.contactNumber from tblApplicationStatus tas inner join tblApplicant a on tas.applicantId = a.applicantId where a.applicantId = " + loggedInUser.applicantId + " order by dated DESC";
                SqlConnection con = new SqlConnection();
                Message msg = new Message();
                SqlCommand cmd = new SqlCommand(query);
                try
                {
                    con = new SqlConnection(PrpDbConnectADO.Conn);
                    con.Open();
                    cmd.Connection = con;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dtCheckStatus);
                    if (dtCheckStatus.Rows.Count > 0)
                    {
                        Session["applicantExist"] = 1;
                        Session["profilePic"] = dtCheckStatus.Rows[0]["pic"].ToString();
                        Session["applicantExist"] = 0;
                        Session["Name"] = loggedInUser.name;
                        Session["email"] = loggedInUser.emailId;
                        Session["pncNumber"] = dtCheckStatus.Rows[0]["pncNo"].ToString();
                        Session["contactNumber"] = dtCheckStatus.Rows[0]["contactNumber"].ToString();
                        Session["applicantExist"] = 0;
                        //Session["degreeAchieved"] = facultyId.ToString();
                        string facultyidd = dtCheckStatus.Rows[0]["facultyId"].ToString();
                        Session["degreeAchieved"] = facultyidd;
                    }
                    //cmd.ExecuteNonQuery();

                    msg.status = true;
                }
                catch (Exception ex)
                {
                    msg.status = false;
                    msg.msg = ex.Message;
                }
                finally
                {
                    con.Close();
                }

                //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);

                if (objStatus.statusId >= ProjConstant.Constant.ApplicationStatus.completed)
                {
                    ProofReadingModel modelProof = GenerateModelProofReading();
                    return View("ApplicationViewAnPrint", modelProof);
                }
                else
                {
                    DataTable dtApplicantRecord = new DataTable();
                    string selectQuery = $@"select
                    inductionId, phaseId, [applicantId],[fatherName],[genderId],[disableId]
                    ,[dob],[pmdcExpiryDate],[mbbsPassingDate], countryIdDegreePassing
                    , dualNationalityType, countryId,  [districtId], districtName
                    ,[domicileProvinceId], domicileDistrictId,[address],[cnicNo],[cnicExpiryDate],[cnicPicFront]
                    ,[cnicPicBack],[domicilePicFront],[domicilePicBack], disableImage
                    , pncPicFront, pncPicBack, medicalFitnessCertificate, pncExpiryDate
                    , generalNursingPassingDate, midWiferyPassingDate, genericBSNPassingDate
                    , meritalStatus, currentPosition, currentBPS, currentPostingAddress, nocCertificate
					, nicCertificate, recommendationLetter, recommendationLetter2, regularizationOrder
                    , postingInstituteLevelId, postingInstituteTypeId, postingInstituteId,currentPostingAddress,joiningDate
                    from tblApplicantInfo where applicantId = " + loggedInUser.applicantId + "";

                    SqlCommand cmdApplicantData = new SqlCommand(selectQuery);
                    try
                    {
                        con = new SqlConnection(PrpDbConnectADO.Conn);
                        con.Open();
                        cmdApplicantData.Connection = con;
                        SqlDataAdapter sda = new SqlDataAdapter(cmdApplicantData);
                        sda.Fill(dtApplicantRecord);
                        DataRow dr = dtApplicantRecord.Rows[0];
                        Session["fatherName"] = dr["fatherName"].ToString();
                        Session["genderId"] = dr["genderId"].ToString();
                        Session["districtId"] = dr["districtId"].ToString();
                        Session["domicileDistrictId"] = dr["domicileDistrictId"].ToString();
                        Session["address"] = dr["address"].ToString();
                        Session["cnicNo"] = dr["cnicNo"].ToString();
                        Session["cnicExpiryDate"] = dr["cnicExpiryDate"].ToString();
                        Session["cnicPicFront"] = dr["cnicPicFront"].ToString();
                        Session["cnicPicBack"] = dr["cnicPicBack"].ToString();
                        Session["domicilePicFront"] = dr["domicilePicFront"].ToString();
                        Session["pncPicFront"] = dr["pncPicFront"].ToString();
                        Session["pncPicBack"] = dr["pncPicBack"].ToString();
                        Session["medicalFitnessCertificate"] = dr["medicalFitnessCertificate"].ToString();
                        Session["pncExpiryDate"] = dr["pncExpiryDate"].ToString();
                        Session["joiningDate"] = dr["joiningDate"].ToString();
                        Session["generalNursingPassingDate"] = dr["generalNursingPassingDate"].ToString();
                        Session["midWiferyPassingDate"] = dr["midWiferyPassingDate"].ToString();
                        Session["genericBSNPassingDate"] = dr["genericBSNPassingDate"].ToString();
                        Session["meritalStatus"] = dr["meritalStatus"].ToString();
                        Session["currentPosition"] = dr["currentPosition"].ToString();
                        Session["currentBPS"] = dr["currentBPS"].ToString();
                        Session["currentPostingAddress"] = dr["currentPostingAddress"].ToString();
                        Session["nocCertificate"] = dr["nocCertificate"].TooString();
                        Session["nicCertificate"] = dr["nicCertificate"].TooString();
                        Session["recommendationLetter"] = dr["recommendationLetter"].TooString();
                        Session["recommendationLetter2"] = dr["recommendationLetter2"].TooString();
                        Session["regularizationOrder"] = dr["regularizationOrder"].TooString();
                        Session["levelId"] = dr["postingInstituteLevelId"].TooInt();
                        Session["levelTypeId"] = dr["postingInstituteTypeId"].TooInt();
                        Session["instituteId"] = dr["postingInstituteId"].TooInt();
                        Session["instituteName"] = dr["currentPostingAddress"].TooString();
                        msg.status = true;
                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.msg = ex.Message;
                    }
                    finally
                    {
                        con.Close();
                    }

                    //

                    model.listProvince = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.province
                        , ProjConstant.Constant.pakistan);

                    model.listCountry = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.country
                      , 0, ProjConstant.Constant.Condition.GetAllCountry);
                    model.countryJson = JsonConvert.SerializeObject(model.listCountry);

                    model.listDegree = new ConstantDAL().GetAll(ProjConstant.Constant.degree);

                    model.listInstituteType = new ConstantDAL().GetAll(ProjConstant.Constant.instituteType);
                    model.listInstituteLevel = new ConstantDAL().GetAll(ProjConstant.Constant.instituteLevel);
                    model.listInstitute = new InstitueDAL().GetAll(ProjConstant.Constant.InstituteType.govt);
                    model.listSpeciality = new SpecialityDAL().GetAll();
                    model.listDiscipline = new CommonDAL().DisciplineGetAll();
                    string facultyidd = dtCheckStatus.Rows[0]["facultyId"].ToString();
                    Session["degreeAchieved"] = facultyidd;
                    //

                    return View(model);
                }
            }
            catch (Exception)
            {
                return View(model);
            }
        }

        public ActionResult EducationProcess()
        {

            DataTable dtCheckStatus = new DataTable();
            string query = "Select top(1) tas.applicationStatusId, tas.inductionId, tas.applicantId,tas.statusTypeId, tas.statusId, tas.dated , a.facultyId, a.levelId, a.pncNo, a.contactNumber from tblApplicationStatus tas inner join tblApplicant a on tas.applicantId = a.applicantId where a.applicantId = " + loggedInUser.applicantId + " order by dated DESC";
            SqlConnection con = new SqlConnection();
            Message msg = new Message();
            SqlCommand cmd = new SqlCommand(query);
            try
            {
                con = new SqlConnection(PrpDbConnectADO.Conn);
                con.Open();
                cmd.Connection = con;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtCheckStatus);
                cmd.ExecuteNonQuery();

                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            finally
            {
                con.Close();
            }

            DataRow dr = dtCheckStatus.Rows[0];
            ProjConstant.Constant.statusApplicantApplication = Convert.ToInt32(dr[3]);
            ProfileModel model = new ProfileModel();
            model.degreeAcheived = dr[6].ToString();
            model.levelId = dr[7].ToString();
            try
            {
                ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                     , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);

                if (objStatus.statusId >= ProjConstant.Constant.ApplicationStatus.completed)
                {
                    ProofReadingModel modelProof = GenerateModelProofReading();
                    return View("ApplicationViewAnPrint", modelProof);
                }
                else
                {
                    model.listProvince = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.province
                   , ProjConstant.Constant.pakistan);

                    model.listDegree = new ConstantDAL().GetAll(ProjConstant.Constant.degree);

                    model.listInstituteType = new ConstantDAL().GetAll(ProjConstant.Constant.instituteType);
                    model.listInstitute = new InstitueDAL().GetAll(ProjConstant.Constant.InstituteType.govt);
                    model.listDiscipline = new CommonDAL().DisciplineGetAll();
                    return View(model);
                }
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
            try
            {
                ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                     , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);

                if (objStatus.statusId >= ProjConstant.Constant.ApplicationStatus.completed)
                {
                    ProofReadingModel modelProof = GenerateModelProofReading();
                    return View("ApplicationViewAnPrint", modelProof);
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception)
            {

                return View(model);
            }
        }

        public ActionResult ResearchPaperProcess()

        {
            DataTable dtCheckStatus = new DataTable();
            string query = "Select top(1) tas.applicationStatusId, tas.inductionId, tas.applicantId,tas.statusTypeId, tas.statusId, tas.dated , a.facultyId, a.levelId, a.pncNo, a.contactNumber from tblApplicationStatus tas inner join tblApplicant a on tas.applicantId = a.applicantId where a.applicantId = " + loggedInUser.applicantId + " order by dated DESC";
            SqlConnection con = new SqlConnection();
            Message msg = new Message();
            SqlCommand cmd = new SqlCommand(query);
            try
            {
                con = new SqlConnection(PrpDbConnectADO.Conn);
                con.Open();
                cmd.Connection = con;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtCheckStatus);
                cmd.ExecuteNonQuery();

                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            finally
            {
                con.Close();
            }

            DataRow dr = dtCheckStatus.Rows[0];
            ProjConstant.Constant.statusApplicantApplication = Convert.ToInt32(dr[3]);
            #region Profile Model


            ProfileModel model = new ProfileModel();
            model.listProvince = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.province
                , ProjConstant.Constant.pakistan);

            model.listCountry = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.country
              , 0, ProjConstant.Constant.Condition.GetAllCountry);
            model.countryJson = JsonConvert.SerializeObject(model.listCountry);

            model.listDegree = new ConstantDAL().GetAll(ProjConstant.Constant.degree);

            model.listInstituteType = new ConstantDAL().GetAll(ProjConstant.Constant.instituteType);
            model.listInstituteLevel = new ConstantDAL().GetAll(ProjConstant.Constant.instituteLevel);
            model.listInstitute = new InstitueDAL().GetAll(ProjConstant.Constant.InstituteType.govt);
            model.listSpeciality = new SpecialityDAL().GetAll();
            model.listDiscipline = new CommonDAL().DisciplineGetAll();

            #endregion

            //model.degreeAcheived = dr[6].ToString();
            //model.levelId = dr[7].ToString();

            ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                      , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

            return View("SpecialityProcess", model);
        }

        public ActionResult SpecialityProcess()
        {
            ProfileModel model = new ProfileModel();


            try
            {

                ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                     , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);

                if (objStatus.statusId >= ProjConstant.Constant.ApplicationStatus.completed)
                {
                    ProofReadingModel modelProof = GenerateModelProofReading();
                    return View("ApplicationViewAnPrint", modelProof);
                }
                else
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
            }
            catch (Exception)
            {

                return View(model);
            }



        }

        public ActionResult ProofReadingProcess()
        {
            ProofReadingModel model = GenerateModelProofReading();

            try
            {
                ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                                    , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);

                if (objStatus.statusId >= ProjConstant.Constant.ApplicationStatus.completed)
                {
                    ProofReadingModel modelProof = GenerateModelProofReading();
                    return View("ApplicationViewAnPrint", modelProof);
                }
                else
                {
                    return View(model);
                }
            }
            catch (Exception)
            {

                return View(model);
            }

        }

        public ActionResult ProofReadingView()
        {
            ProofReadingModel model = GenerateModelProofReading();
            return View(model);
        }

        public ActionResult ApplicationViewAnPrint()
        {
            ProofReadingModel model = GenerateModelProofReading();
            Session["appliedFor"] = loggedInUser.levelId;
            ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                   , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();
            DataTable dtCheckStatus = new DataTable();
            string query = "Select top(1) tas.applicationStatusId, tas.inductionId, tas.applicantId,tas.statusTypeId, tas.statusId, tas.dated , a.facultyId, a.genderID, a.levelId, a.pncNo, a.contactNumber from tblApplicationStatus tas inner join tblApplicant a on tas.applicantId = a.applicantId where a.applicantId = " + loggedInUser.applicantId + " order by dated DESC";
            SqlConnection con = new SqlConnection();
            Message msg = new Message();
            SqlCommand cmd = new SqlCommand(query);
            try
            {
                con = new SqlConnection(PrpDbConnectADO.Conn);
                con.Open();
                cmd.Connection = con;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtCheckStatus);
                cmd.ExecuteNonQuery();

                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            finally
            {
                con.Close();
            }
            DataRow dr = dtCheckStatus.Rows[0];
            Session["appliedFor"] = dr[8].ToString();
            Session["degreeAchieved"] = dr[6].ToString();
            //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);

            if (objStatus.statusId < ProjConstant.Constant.ApplicationStatus.completed)
            {
                return Redirect("/applicant-profile-create");
            }

            return View(model);
        }

        public ActionResult ApplicantMerit()
        {
            ProofReadingModel model = GenerateModelProofReading();
            int roundId = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();

            int inductionId = ProjConstant.inductionId;

            model.listSpecilityMerit = new MeritDAL().GetApplicantSpecialityWithMeritMarks(inductionId, loggedInUser.applicantId, roundId);


            ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                   , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

            //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);


            if (objStatus.statusId < ProjConstant.Constant.ApplicationStatus.completed)
            {
                return Redirect("/applicant-profile-create");
            }

            return View(model);
        }

        public ActionResult ApplicationProcessComplete()
        {
            LoginModel model = new LoginModel();
            return View(model);
        }

        public ActionResult ApplicationListView()
        {
            ApplicantApplicationModel model = GenerateModelApplicantApplication();

            return View(model);
        }

        public ActionResult ApplicationVoucher()
        {
            ApplicantVoucherModel model = new ApplicantVoucherModel();

            ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                   , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

            //ApplicantStatus objStatus = new ApplicantDAL().GetApplicantStatus(loggedInUser.applicantId);

            if (objStatus.statusId == ProjConstant.Constant.ApplicationStatus.completed)
            {
                ProofReadingModel modelProof = GenerateModelProofReading();
                return View("ApplicationViewAnPrint", modelProof);
            }
            else
            {
                return View(model);
            }
        }

        public ApplicantApplicationModel GenerateModelApplicantApplication()
        {
            ApplicantApplicationModel model = new ApplicantApplicationModel();
            try
            {
                model.applicant = new ApplicantDAL().GetApplicant(0, loggedInUser.applicantId);
                model.applicantInfo = new ApplicantDAL().GetApplicantInfoDetail(0, ProjConstant.phaseId, loggedInUser.applicantId);
                model.status = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                   , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();
            }
            catch (Exception)
            {

            }

            return model;
        }

        public ProofReadingModel GenerateModelProofReading()
        {
            ProofReadingModel model = new ProofReadingModel();
            try
            {
                DDLConstants dDLConstant = new DDLConstants();
                dDLConstant.condition = "ByType";
                dDLConstant.typeId = ProjConstant.Constant.degreeType;
                model.listType = new ConstantDAL().GetConstantDDL(dDLConstant).OrderBy(x => x.id).ToList();

                int inductionId = ProjConstant.inductionId;
                int phaseId = ProjConstant.phaseId;

                model.applicant = new ApplicantDAL().GetApplicant(0, loggedInUser.applicantId);
                model.applicantInfo = new ApplicantDAL().GetApplicantInfoDetail(0,0, loggedInUser.applicantId);
                model.degree = new ApplicantDAL().GetApplicantDegreeDetail(0,0, loggedInUser.applicantId);
                //model.listJob = new ApplicantDAL().GetApplicantHouseJobList(0, 0, loggedInUser.applicantId);
                model.listDegreeMark = new ApplicantDAL().GetApplicantDegreeMarkList(0, 0, loggedInUser.applicantId);
                //model.listCertificate = new ApplicantDAL().GetApplicantCertificateListDetail(0, 0, loggedInUser.applicantId);
                model.listExprince = new ApplicantDAL().GetApplicantExperienceListDetail(0, 0, loggedInUser.applicantId);
                //model.listDistinction = new ApplicantDAL().GetApplicantDistinctionList(0, 0, loggedInUser.applicantId);
                //model.listResearchPaper = new ApplicantDAL().GetApplicantResearchPaperListDetail(0, 0, loggedInUser.applicantId);
                model.listSpecility = new ApplicantDAL().GetApplicantSpecilityList(12, 0, loggedInUser.applicantId);
                model.voucher = new ApplicantDAL().GetApplicantVoucher(0, 0, loggedInUser.applicantId);
                model.listMarks = new MarksDAL().GetMarksDetaikByApplicant(12,loggedInUser.applicantId);

            }
            catch (Exception)
            {
            }
            return model;
        }

        public ActionResult ApplicationStatusView()
        {

            ApplicationStatusModel model = new ApplicationStatusModel();
            model.listStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId, loggedInUser.applicantId);
            model.listStatusApproval = new VerificationDAL().GetApplicationApprovalStatusGetById(ProjConstant.inductionId, ProjConstant.phaseId, loggedInUser.applicantId);
            model.info = new ApplicantDAL().GetApplicantInfo(ProjConstant.inductionId, ProjConstant.phaseId, loggedInUser.applicantId);
            try
            {
                model.final = new JoiningDAL().GetFinalApplicantById(ProjConstant.inductionId, loggedInUser.applicantId);
            }
            catch (Exception)
            {
                model.final = new ApplicantSelected();
            }
            return View(model);
        }


        public ActionResult ApplicationApprovalStatusView()
        {

            ApplicantApprovalStatusModel model = new ApplicantApprovalStatusModel();
            model.listStatus = new VerificationDAL().GetApplicationApprovalStatusGetById(ProjConstant.inductionId, ProjConstant.phaseId, loggedInUser.applicantId);
            return View(model);
        }

        #endregion

        #region Personal Information

        [HttpGet]
        public JsonResult GetApplicantInfo(int applicantId)
        {
            Applicant applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, applicantId);
            return Json(applicant, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetApplicantProfileData(int applicantId)
        {
            ApplicantInfo applicantInfo = new ApplicantInfo();

            try
            {
                applicantInfo = new ApplicantDAL().GetApplicantInfo(ProjConstant.inductionId, ProjConstant.phaseId, applicantId);
                applicantInfo.bod = applicantInfo.dob.ToString("dd/MM/yyyy");
            }
            catch (Exception)
            {
            } 



            return Json(applicantInfo, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetApplicantInfoDualNational(int applicantId)
        {
            ApplicantInfoDualNational applicantInfo = new ApplicantDAL().GetApplicantInfoDualNational(ProjConstant.inductionId, ProjConstant.phaseId, applicantId);
            return Json(applicantInfo, JsonRequestBehavior.AllowGet);
        }

        

        [HttpGet]
        public JsonResult GetApplicantInfoDetail(int applicantId)
        {
            ApplicantInfo applicantInfo = new ApplicantDAL().GetApplicantInfoDetail(0, 0, applicantId);
            return Json(applicantInfo, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ApplicantInfoAddUpdate(ApplicantInfoParam objApplicantInfo)
        {
            Message msg = new Message();
            string objeeect = JsonConvert.SerializeObject(objApplicantInfo).ToString();
            objApplicantInfo.applicantId = loggedInUser.applicantId;
            objApplicantInfo.pncNo = loggedInUser.pncNo;
            int applicationStatus = 1;
            string theDate = objApplicantInfo.pncExpiryDate.ToString("d");
            try
            {
                ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(13, ProjConstant.phaseId
                  , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();
                if (objStatus.statusId == 8)
                {
                    objStatus.statusId = 1;
                }
                if (objStatus.statusId < 8)
                {

                    ApplicantInfo obj = new ApplicantInfo();
                    obj.inductionId = ProjConstant.inductionId;
                    obj.phaseId = ProjConstant.phaseId;
                    obj.applicantId = loggedInUser.applicantId;
                    obj.fatherName = objApplicantInfo.fatherName.TooString();
                    obj.genderId = objApplicantInfo.genderId;
                    obj.disableId = objApplicantInfo.disableId;
                    obj.countryIdDegreePassing = objApplicantInfo.countryIdDegreePassing;
                    obj.dualNationalityType = objApplicantInfo.dualNationalityType;
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
                    //obj.pmdcExpiryDate = objApplicantInfo.pmdcExpiryDate.TooDate();
                    //obj.mbbsPassingDate = objApplicantInfo.mbbsPassingDate.TooDate();
                    obj.cnicExpiryDate = objApplicantInfo.cnicExpiryDate.TooDate();
                    obj.pncExpiryDate = objApplicantInfo.pncExpiryDate.TooDate();
                    obj.joiningDate = objApplicantInfo.joiningDate.TooDate();
                    obj.generalNursingPassingDate = objApplicantInfo.generalNursingPassingDate.TooDate();
                    obj.genericBSNPassingDate = objApplicantInfo.genericBSNPassingDate.TooDate();
                    obj.midWiferyPassingDate = objApplicantInfo.midWiferyPassingDate.TooDate();
                    obj.pncPicFront = objApplicantInfo.pncPicFront;
                    obj.pncPicBack = objApplicantInfo.pncPicBack;
                    obj.medicalFitnessCertificate = objApplicantInfo.medicalFitnessCertificate;
                    obj.meritalStatus = objApplicantInfo.maritalStatus;
                    obj.currentDesignation = objApplicantInfo.currentDesignation;
                    obj.currentBPS = objApplicantInfo.currentBPS;
                    obj.currentPosting = objApplicantInfo.currentPosting;
                    obj.nocCertificate = objApplicantInfo.nocCertificate;
                    obj.nicCertificate = objApplicantInfo.nicCertificate;
                    obj.recommendationLetter = objApplicantInfo.recommendationLetter;
                    obj.recommendationLetter2 = objApplicantInfo.recommendationLetter2;
                    obj.regularizationOrder = objApplicantInfo.regularizationOrder;
                    obj.levelId = objApplicantInfo.levelId;
                    obj.levelTypeId = objApplicantInfo.levelTypeId;
                    obj.instituteId = objApplicantInfo.instituteId;
                    obj.instituteName = objApplicantInfo.instituteName;
                    if (loggedInUser.facultyId == 11)
                    {
                        obj.midWiferyPassingDate = DateTime.Now;
                    }
                    else if (loggedInUser.facultyId == 12)
                    {
                        obj.genericBSNPassingDate = DateTime.Now;
                    }
                    obj.generalNursingPassingDate = DateTime.Now;
                    obj.generalNursingPassingDate = DateTime.Now;
                    obj.midWiferyPassingDate = DateTime.Now;
                    msg = new ApplicantDAL().ApplicantInfoAddUpdate(obj);
                    msg.id = applicationStatus;

                    if (msg.status)
                        ApplicantStatusUpdate(loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication, ProjConstant.Constant.ApplicationStatus.education);

                }
                else
                {
                    msg.id = applicationStatus;
                }
            }
            catch (Exception)
            {
                msg.id = applicationStatus;

            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ApplicantInfoAddUpdateDualNational(ApplicantInfoDualNational obj)
        {
            Message msg = new Message();

           

            try
            {
                ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(ProjConstant.inductionId, ProjConstant.phaseId
                  , loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                if (objStatus.statusId < 8)
                {
                    obj.applicantId = loggedInUser.applicantId;
                    obj.countryId = obj.countryId;
                    obj.embassyCertificate = obj.embassyCertificate.TooString();
                    obj.languageCertificate = obj.languageCertificate.TooString();
                    obj.policeCertificate = obj.policeCertificate.TooString();
                    obj.affidavitCertificate = obj.affidavitCertificate.TooString();
                    msg = new ApplicantDAL().ApplicantInfoDualNationalAddUpdate(obj);
                }
                
            }
            catch (Exception)
            {
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Education/Degree

        [HttpPost]
        public JsonResult ApplicantEducationAddUpdate(ApplicantDegrees objEducation)
        {
            Message msg = new Message();

            //ApplicantDegrees degree = objEducation.applicantDegrees;

            //degree.applicantId = degree.typeIds.TooString().TrimStart(',').TrimEnd(',');
            //degree.inductionId = ProjConstant.inductionId;
            //degree.phaseId = ProjConstant.phaseId;

            List<DegreeMarks> marksList = objEducation.DegreeMarks;
            //delete existing degree records
            foreach (var item in objEducation.DegreeMarks)
            {
                if (item.degreeTypeId == 1 || item.degreeTypeId == 8)
                {
                    item.passingDate = objEducation.matricPassindDate.TooDate();
                }
                if (item.degreeTypeId == 2 || item.degreeTypeId == 9)
                {
                    item.passingDate = objEducation.interPassingDate.TooDate();
                }

                int countRows = 0;
                string checkQuery = "";
                checkQuery = "select count(*) from tblNursingApplicantDegreeData" +
                    " where applicantID = " + item.applicantId + " and degreeTypeID = "+ item.degreeTypeId + " ";
                SqlCommand cmd = new SqlCommand(checkQuery);
                SqlConnection con = new SqlConnection(PrpDbConnectADO.Conn);
                con.Open();
                cmd.Connection = con;
                try
                {
                    countRows = Convert.ToInt32(cmd.ExecuteScalar());
                    if (countRows > 0)
                    {
                        string query = "";
                        query = "update tblNursingApplicantDegreeData " +
                            "set [obtainedMarks] = " + Convert.ToDouble(item.obtainedMarks).TooDecimal() + "," +
                            " [totalMarks] = " + Convert.ToDouble(item.totalMarks).TooDecimal() + "," +
                            " [passingDate] = CONVERT(datetime,'" + (item.passingDate).TooDate() + "')," +
                            " [degreeInstitute] = '" + item.degreeInstituteName.TooString() + "'," +
                            " [degreePicFront] = '" + item.degreePicFront.TooString() + "' " +
                            " where applicantID = " + item.applicantId + " and degreeTypeID = " + item.degreeTypeId + " ";
                        SqlCommand cmdUpdate = new SqlCommand(query, con);
                        cmdUpdate.ExecuteNonQuery();
                        msg.status = true;
                    }
                    else
                    {
                        string query = "";
                        query = "insert into tblNursingApplicantDegreeData " +
                            "([applicantID] ,[degreeTypeID] ,[obtainedMarks] ,[totalMarks] ,[passingDate] ,[degreePicFront],[degreeInstitute])" +
                            " values " +
                            "(" + item.applicantId.TooInt() + "," + item.degreeTypeId.TooInt() + "," + Convert.ToDouble(item.obtainedMarks).TooDecimal() + "," + Convert.ToDouble(item.totalMarks).TooDecimal() + ", " +
                            " CONVERT(datetime,'" + Convert.ToDateTime(item.passingDate).TooDate() + "'),'" + item.degreePicFront.TooString() + "','" + item.degreeInstituteName.TooString() + "'" +
                            ")";
                        SqlCommand cmdInsert = new SqlCommand(query, con);
                        cmdInsert.ExecuteNonQuery();
                        msg.status = true;
                    }
                    msg.status = true;
                }
                catch (Exception ex)
                {
                    msg.status = false;
                }
                finally
                {
                    con.Close();
                }
            }



            //List<ApplicantDegreeMark> listMarks = objEducation.listDegreeMarks;
            //Message msg = new ApplicantDAL().ApplicantDegreeAddUpdate(degree);
            //if (msg.status == true)
            //{
            //    if (listMarks != null && listMarks.Count > 0)
            //        msg = new ApplicantDAL().ApplicantDegreeMarksAddUpdate(ProjConstant.inductionId, ProjConstant.phaseId, listMarks);

            //    if (objEducation.listCertificate != null && objEducation.listCertificate.Count > 0)
            //    {
            //        foreach (var item in objEducation.listCertificate)
            //        {
            //            item.inductionId = ProjConstant.inductionId;
            //            item.phaseId = ProjConstant.phaseId;
            //            item.passingDate = item.datePassing.TooDate();
            //        }
            //        msg = new ApplicantDAL().ApplicantCertificateAddUpdate(objEducation.listCertificate);
            //    }
            //}

            if (Convert.ToString(Session["degreeAchieved"]) != "13")
            {
                if (msg.status)
                    ApplicantStatusUpdate(loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication, ProjConstant.Constant.ApplicationStatus.experience);

            }
            else
            {
                if (msg.status)
                    ApplicantStatusUpdate(loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication, 5);

            }


            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetApplicantEducationData(int applicantId)
        {
            ApplicantDegrees appdegress = new ApplicantDegrees();
            appdegress.applicantId = applicantId.ToString();
            appdegress.DegreeMarks = new List<DegreeMarks>();
            string query = "";
            DataTable dt = new DataTable();
            query += "SELECT [applicantID], [degreeTypeID],[obtainedMarks],[totalMarks],[passingDate],[degreePicFront],[degreeInstitute] " +
                "FROM tblNursingApplicantDegreeData where applicantID = "+ loggedInUser.applicantId + "";
            SqlCommand cmd = new SqlCommand(query);
            SqlConnection con = new SqlConnection(PrpDbConnectADO.Conn);
            con.Open();
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            
            try
            {
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                  {
                    foreach (DataRow dr in dt.Rows)
                    {
                        DegreeMarks dm = new DegreeMarks();
                        dm.applicantId = Convert.ToInt32(dr[0]);
                        dm.degreeTypeId = Convert.ToInt32(dr[1]);
                        dm.obtainedMarks = dr[2].ToString();
                        dm.totalMarks = dr[3].ToString();
                        //Convert.ToDateTime(dr[22]);
                        dm.passingDate = Convert.ToDateTime(dr[4]);
                        dm.degreePicFront = dr[5].ToString();
                        dm.degreeInstituteName = dr[6].ToString();
                        appdegress.DegreeMarks.Add(dm);
                        if (dm.degreeTypeId == 1 || dm.degreeTypeId == 8)
                        {
                            Session["MatricDegreeImg"] = dm.degreePicFront.ToString();
                            //Session["PassingDate1"] = Convert.ToDateTime(dm.passingDate);
                        }
                        else if (dm.degreeTypeId == 2 || dm.degreeTypeId == 9)
                        {
                            Session["FADegreeImg"] = dm.degreePicFront.ToString();
                            //Session["PassingDate2"] = Convert.ToDateTime(dm.passingDate);
                        }
                        else if (dm.degreeTypeId == 3)
                        {
                            Session["GeneralNursingDegreeImg"] = dm.degreePicFront.ToString();
                            //Session["PassingDate3"] = Convert.ToDateTime(dm.passingDate);
                        }
                        else if (dm.degreeTypeId == 4)
                        {
                            Session["MidwiferyDegreeImg"] = dm.degreePicFront.ToString();
                            //Session["PassingDate4"] = Convert.ToDateTime(dm.passingDate);
                        }
                        else if (dm.degreeTypeId == 5)
                        {
                            Session["BSNDegreeImg"] = dm.degreePicFront.ToString();
                            //Session["PassingDate5"] = Convert.ToDateTime(dm.passingDate);
                        }
                        else if (dm.degreeTypeId == 6)
                        {
                            Session["PostRnBScDegreeImg"] = dm.degreePicFront.ToString();
                            //Session["PassingDate6"] = Convert.ToDateTime(dm.passingDate);
                        }
                        else if (dm.degreeTypeId == 7)
                        {
                            Session["PostRnMScDegreeImg"] = dm.degreePicFront.ToString();
                            //Session["PassingDate7"] = Convert.ToDateTime(dm.passingDate);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            try
            {
                new ApplicantDAL().ApplicantDegreeMarksMakeAccurate(0, 0, applicantId);
            }
            catch (Exception)
            {
            }

            ApplicantEducation objEducation = new ApplicantEducation();
            try
            {
                try
                {
                    objEducation.applicantDegree = new ApplicantDAL().GetApplicantDegree(0, 0, applicantId);
                }
                catch (Exception)
                {
                }

                try
                {
                    objEducation.listDegreeMarks = new ApplicantDAL().GetApplicantDegreeMarkList(0, 0, applicantId);
                }
                catch (Exception)
                {
                }

                try
                {
                    objEducation.listCertificate = new ApplicantDAL().GetApplicantCertificateList(0, 0, applicantId);
                }
                catch (Exception)
                {
                }

            }
            catch (Exception)
            {
            }
            return Json(appdegress, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetApplicantCertificateList(int applicantId)
        {
            List<ApplicantCertificate> list = new ApplicantDAL().GetApplicantCertificateList(0, 0, applicantId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region House Job

        [HttpPost]
        public JsonResult ApplicantHouseJobAddUpdate(ApplicantHouseJob obj)
        {

            obj.inductionId = ProjConstant.inductionId;
            obj.startDate = obj.startDateStr.TooDate();
            obj.endDate = obj.endDateStr.TooDate();
            obj.dated = DateTime.Now;
            obj.hospitalId = obj.hospitalId.TooInt();
            obj.hospital = obj.hospital.TooString();
            obj.isSame = obj.isSame.TooBoolean();

            Message msg = new ApplicantDAL().ApplicantHouseJobAddUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ApplicantHouseJobDeleteSingle(int houseJodId)
        {
            Message msg = new ApplicantDAL().ApplicantHouseJobDeleteSingle(houseJodId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetApplicantHouseJobByApplicant(int applicantId)
        {
            List<ApplicantHouseJob> list = new ApplicantDAL().GetApplicantHouseJobList(0, 0, applicantId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Exprience

        [HttpPost]
        public JsonResult ApplicantExperienceAddUpdate(ApplicantExperienceParam objExperience)
        {
            ApplicantExperience obj = new ApplicantExperience();
            obj.applicantExperienceId = objExperience.applicantExperienceId;
            obj.applicantId = loggedInUser.applicantId;
            obj.levelId = objExperience.levelId;
            obj.levelTypeId = objExperience.levelTypeId;
            obj.instituteId = objExperience.instituteId;
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
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

            Message msg = new ApplicantDAL().ApplicantExperienceAddUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ApplicantExperienceDeleteSingle(int applicantExperienceId)
        {
            Message msg = new ApplicantDAL().ApplicantExperienceDeleteSingle(ProjConstant.inductionId, ProjConstant.phaseId, applicantExperienceId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetApplicantExperienceData(int applicantId)
        {
            List<ApplicantExperience> list = new List<ApplicantExperience>();
            DataTable dt = new DataTable();
            string updateQuery = "Update tblApplicantExperience Set noOfDays= DATEDIFF(DAY, startDated, endDate) Where applicantId = "+applicantId+"";
            updateQuery += " Update tblApplicantExperience Set noOfMonth " +
                " = Case " +
                " when noOfDays = 365 then 12 " +
                " When noOfDays = 364  then 12 " +
                " Else  (Select dbo.funGetMonthCount(startDated, endDate) ) " +
                " End " +
                " Where  applicantId = " + applicantId + " ";
            string query = "select a.[applicantExperienceId],a.[inductionId],a.[phaseId],a.[applicantId],a.[levelId],a.[levelTypeId],a.[instituteName],a.[instituteId],a.[provinceId],a.[typeId],a.[startDated],a.[endDate],a.[noOfMonth],a.[noOfDays],a.[isCurrent],a.[imageExperience],a.[dated] ";
            query += " , c.name as NameofInstitute , r.name as ProvinceName , cc.name as levelName ";
            query += " from tblApplicantExperience a";
            query += "  inner join tblConstant cc on a.typeId = cc.id and a.levelId = cc.typeId ";
            query += "  inner join tblHospital c on a.instituteId = c.hospitalId ";
            query += "  inner join tblRegion r on a.provinceId = r.regionId ";
            query += " where a.applicantId = " + applicantId + "";
            SqlConnection con = new SqlConnection();
            Message msg = new Message();
            
            SqlCommand cmd = new SqlCommand(query);
            try
            {
                con = new SqlConnection(PrpDbConnectADO.Conn);
                SqlCommand cmdUpdate = new SqlCommand(updateQuery, con);
                con.Open();
                cmdUpdate.ExecuteNonQuery();
                cmd.Connection = con;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        ApplicantExperience ae = new ApplicantExperience();
                        ae.applicantExperienceId = dr[0].TooInt();
                        ae.inductionId = dr[1].TooInt();
                        ae.phaseId = dr[2].TooInt();
                        ae.applicantId = dr[3].TooInt();
                        ae.levelId = dr[4].TooInt();
                        ae.levelTypeId = dr[5].TooInt();
                        ae.instituteName = dr[6].TooString();
                        ae.instituteId = dr[7].TooInt();
                        ae.provinceId = dr[8].TooInt();
                        ae.typeId = dr[9].TooInt();
                        ae.startDated = Convert.ToDateTime(dr[10]);
                        ae.endDate = Convert.ToDateTime(dr[11]);
                        ae.noOfMonth = dr[12].TooInt();
                        ae.noOfDays = dr[13].TooInt();
                        ae.isCurrent = dr[14].TooBoolean();
                        ae.imageExperience = dr[15].TooString();
                        ae.dated = Convert.ToDateTime(dr[16]);
                        ae.instituteName = dr[17].TooString();
                        ae.provinceName = dr[18].TooString();
                        ae.levelName = dr[19].TooString();
                        ae.typeName = "Nurse";
                        ae.levelTypeName = "";
                        list.Add(ae);
                        msg.status = true;
                    }
                }
                else
                {
                    msg.status = false;
                }
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            finally
            {
                con.Close();
            }
            //List<ApplicantExperience> list = new ApplicantDAL().GetApplicantExperienceListByApplicant(0, 0, applicantId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Distinction

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

            Message msg = new ApplicantDAL().ApplicantDistinctionAddUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetApplicantDistinctionData(int applicantId)
        {
            List<ApplicantDistinction> list = new ApplicantDAL().GetApplicantDistinctionList(0, 0, applicantId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult ApplicantDistinctionDeleteSingle(int applicantDistinctionId)
        {
            Message msg = new ApplicantDAL().ApplicantDistinctionDeleteSingle(ProjConstant.inductionId, ProjConstant.phaseId, applicantDistinctionId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ResearchPaper

        [HttpPost]
        public JsonResult ApplicantResearchPaperAddUpdate(ApplicantResearchPaperParam objExperience)
        {
            ApplicantResearchPaper obj = new ApplicantResearchPaper();
            obj.applicantResearchId = objExperience.applicantResearchId;
            obj.researchJournalId = objExperience.researchJournalId;
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            obj.applicantId = loggedInUser.applicantId;
            obj.name = objExperience.name;
            obj.authorId = objExperience.authorId;
            obj.publishStatusId = objExperience.publishStatusId;
            obj.link = objExperience.link;
            obj.webLink = objExperience.webLink;
            obj.imageLetter = objExperience.imageLetter;
            obj.dated = DateTime.Now;
            Message msg = new ApplicantDAL().ApplicantResearchPaperAddUpdate(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ApplicantResearchPaperDeleteSingle(int applicantResearchId)
        {
            Message msg = new ApplicantDAL().ApplicantResearchPaperDeleteSingle(ProjConstant.inductionId, ProjConstant.phaseId, applicantResearchId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetApplicantResearchPaperData(int applicantId)
        {
            List<ApplicantResearchPaper> list = new ApplicantDAL().GetApplicantResearchPaperList(0, 0, applicantId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Specility

        [HttpPost]
        public JsonResult ApplicantSpecilityCheckPreferenceNo(ApplicantSpecility objSpecility)
        {
            objSpecility.dated = DateTime.Now;
            objSpecility.inductionId = ProjConstant.inductionId;
            objSpecility.phaseId = ProjConstant.phaseId;
            Message msg = new ApplicantDAL().ApplicantSpecilityCheckPreferenceNo(objSpecility);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ApplicantSpecilityAddUpdate(ApplicantSpecility objSpecility)
        {
            objSpecility.dated = DateTime.Now;

            objSpecility.inductionId = ProjConstant.inductionId;
            objSpecility.phaseId = ProjConstant.phaseId;

            Message msg = new ApplicantDAL().ApplicantSpecilityAddUpdate(objSpecility);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ApplicantSpecilityDeleteSingle(int applicantSpecilityId)
        {
            Message msg = new ApplicantDAL().ApplicantSpecilityDeleteSingle(ProjConstant.inductionId, ProjConstant.phaseId, applicantSpecilityId);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetApplicantSpecilityData(int applicantId)
        {
            List<ApplicantSpecility> list = new ApplicantDAL().GetApplicantSpecilityList(0, 0, applicantId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Voucher

        [HttpGet]
        public JsonResult GetApplicantVoucher(int applicantId)
        {
            ApplicantVoucher applicant = new ApplicantDAL().GetApplicantVoucher(0, 0, applicantId);
            return Json(applicant, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ApplicantVoucherAddUpdate(ApplicantVoucherParam objParam)
        {

            ApplicantVoucher obj = new ApplicantVoucher();
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            obj.applicantId = loggedInUser.applicantId;
            obj.applicantVoucherId = objParam.applicantVoucherId.TooInt();
            obj.branchCode = objParam.branchCode.TooString();
            obj.voucherImage = objParam.voucherImage.TooString();
            obj.ibn = objParam.ibn.TooString();
            obj.accountNo = objParam.accountNo.TooString();
            obj.accountTitle = objParam.accountTitle.TooString();
            obj.submittedDate = objParam.dateSubmitted.TooDate();
            obj.testingCenter = objParam.testingCenter.TooInt();

            Message msg = new ApplicantDAL().ApplicantVoucherAddUpdate(obj);

            if (msg.status)
                ApplicantStatusUpdate(loggedInUser.applicantId, ProjConstant.Constant.statusApplicantApplication, ProjConstant.Constant.ApplicationStatus.paymentDone);

            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Common

        ///
        ///

        [HttpGet]
        public JsonResult ReopenApplication()
        {
            Message msg = new Message();
            if (loggedInUser.applicantId > 0)
            {
                msg = new ApplicantDAL().ReopenApplication(loggedInUser.applicantId, ProjConstant.Constant.ApplicationStatus.proofReading);
            }
            else
            {
                msg.status = false;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UpdateApplicantStatus(int applicantId, int applicationStatus)
        {
            Message msg = ApplicantStatusUpdate(applicantId, ProjConstant.Constant.statusApplicantApplication, applicationStatus);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetApplicantTentativeMarks(int applicantId, double fsc, double gn, double mw, double bsn, double extraQ, double minExp, double exp,double entryTest)
        {
            TentativeMarks tm = new TentativeMarks();
            tm.applicantId = applicantId;
            tm.fscTotalWeightage = fsc;
            tm.gnTotalWeightage = gn;
            tm.mwTotalWeightage = mw;
            tm.bsnTotalWeightage = bsn;
            tm.expTotalWeightage = extraQ;
            tm.minExpRequired = minExp;
            tm.expTotalWeightage = exp;
            tm.entryTestTotalWeightage = entryTest;
            tm.entryTestObtained = 0;
            string qualification = "(";
            if (fsc != 0)
            {
                qualification += "2,9,";
            }
            if (gn != 0)
            {
                qualification += "3,";
            }
            if (mw != 0)
            {
                qualification += "4,";
            }
            if (bsn != 0)
            {
                qualification += "5,";
            }
            if (extraQ != 0)
            {
                qualification += "6,7,";
            }
            qualification = qualification.Remove(qualification.Length - 1);
            qualification += ")";
            string marksQuery = "select degreeTypeID, obtainedMarks,totalMarks, obtainedMarks/totalMarks as percentageMarks from tblNursingApplicantDegreeData where applicantID = " + applicantId+" and degreeTypeID in "+qualification+"";
            double gnAndMwObtained = 0;
            double gnAndMwTotal = 0;
            SqlCommand cmd = new SqlCommand(marksQuery);
            SqlConnection con = new SqlConnection(PrpDbConnectADO.Conn);
            con.Open();
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            
            try
            {
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (Convert.ToInt32(dr[0]) == 2 || Convert.ToInt32(dr[0]) == 9)
                        { 
                            tm.fscObtained = Math.Round(Convert.ToDouble(dr[3])*fsc,2);
                        }
                        else if (Convert.ToInt32(dr[0]) == 3)
                        {
                            gnAndMwObtained += Convert.ToDouble(dr[1]);
                            gnAndMwTotal += Convert.ToDouble(dr[2]);
                            //tm.gnObtained = Math.Round(Convert.ToDouble(dr[3]) * gn, 2);
                            tm.gnObtained = 0;
                        }
                        else if (Convert.ToInt32(dr[0]) == 4)
                        {
                            gnAndMwObtained += Convert.ToDouble(dr[1]);
                            gnAndMwTotal += Convert.ToDouble(dr[2]);
                            tm.mwObtained = Math.Round((Convert.ToDouble(gnAndMwObtained / gnAndMwTotal) * (gn+mw)), 2);
                        }
                        else if (Convert.ToInt32(dr[0]) == 5)
                        {
                            tm.bsnObtained = Math.Round(Convert.ToDouble(dr[3]) * bsn,2);
                        }
                        else if ( Convert.ToInt32(dr[0]) == 6 || Convert.ToInt32(dr[0]) == 7)
                        {
                            tm.extraObtained = Math.Round(Convert.ToDouble(dr[3]) * extraQ, 2);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            if (exp != 0)
            {
                string expQuery = "select noOfDays from tblApplicantExperience where inductionId = 12 AND applicantId = " + applicantId + "";
                SqlCommand cmdExp = new SqlCommand(expQuery);
                cmdExp.Connection = con;
                SqlDataAdapter sdaExp = new SqlDataAdapter(cmdExp);
                DataTable dtExp = new DataTable();
                double totalExperienceYears = 0;
                try
                {
                    sdaExp.Fill(dtExp);
                    if (dtExp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtExp.Rows)
                        {
                            totalExperienceYears += Convert.ToDouble(dr[0])/365;
                        }
                    }
                    totalExperienceYears = totalExperienceYears - minExp;
                    if (totalExperienceYears < 0)
                    {
                        totalExperienceYears = 0;
                    }
                    tm.minExpRequired = minExp;
                    tm.expTotalWeightage = exp;
                    if (totalExperienceYears > exp)
                        totalExperienceYears = exp;
                    tm.expObtained = Math.Round(totalExperienceYears,2);
                }
                catch (Exception ex)
                {

                }
                finally
                {
                    con.Close();
                }

            }
            return Json(tm, JsonRequestBehavior.AllowGet);
        }

        public Message ApplicantStatusUpdate(int applicantId, int statusTypeId, int statusId)
        {
            Message msg = new Message();
            try
            {
                msg = new ApplicantDAL().ApplicantStatusUpdate(applicantId, statusTypeId, statusId);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.message = ex.Message;
            }
            return msg;
        }

        [HttpGet]
        public JsonResult GetApplicationDetailData(int applicantId)
        {
            Application obj = new Application();

            Applicant applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, applicantId);
            if (applicant != null && applicant.applicantId > 0)
                obj.applicant = applicant;

            int inductionId = ProjConstant.inductionId;
            int phaseId = ProjConstant.phaseId;

            ApplicantInfo applicantInfo = new ApplicantDAL().GetApplicantInfo(inductionId, phaseId, applicantId);
            if (applicantInfo != null && applicantInfo.applicantDetailId > 0)
                obj.applicantInfo = applicantInfo;

            obj.applicantDegree = new ApplicantDAL().GetApplicantDegree(inductionId, phaseId, applicantId);

            obj.listDegreeMarks = new ApplicantDAL().GetApplicantDegreeMarkList(inductionId, phaseId, applicantId);

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}