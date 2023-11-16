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
    public class ConsentUIController : BaseController
    {
        // GET: ConsentUI
        public ActionResult ApplicantConsent()
        {
            ConsentModel model = new ConsentModel();


            int roundId = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();
            model.roundId = roundId;

            model.applicant = new ApplicantDAL().GetApplicant(ProjConstant.inductionId, loggedInUser.applicantId);
            model.consentId = Request.QueryString["consentId"].TooInt();

            DDLConstants dDLConstant = new DDLConstants();
            dDLConstant.condition = "ByType";
            dDLConstant.typeId = ProjConstant.Constant.consentType;
            model.listConsentStatus = new ConstantDAL().GetConstantDDL(dDLConstant).OrderBy(x => x.id).ToList();

            model.validStatus = 1;
            if (model.validStatus == 1)
            {

                if (model.consentId > 0)
                {
                    model.consent = new ConsentDAL().GetById(model.consentId);

                    if (model.consent.applicantId != loggedInUser.applicantId)
                        model.validStatus = 0;
                }
                if (model.validStatus == 1)
                {
                    model.listType = new ConsentDAL().GetTypeApplicantHasMerit(loggedInUser.applicantId,0);
                    SqlConnection con = new SqlConnection(PrpDbConnectADO.Conn);
                    //string query = "select a.[applicantVoucherId],a.[inductionId],a.[phaseId],a.[applicantId],a.[amount], " +
                    //    " a.[branchCode],a.[voucherImage],a.[ibn],a.[accountNo],a.[accountTitle],a.[submittedDate],a.[dated],a.[testingCenterID], h.name" +
                    //    " from tblApplicantVoucher a inner join (select name,hospitalId from tblHospital where levelId = 25) h on a.testingCenterID = h.hospitalId "+
                    //    " where a.inductionId = " + inductionId + " and a.applicantId = " + applicantId + "";
                    string query = "Select Top 1 1, '' +' BNS Generic - ' +  i.name From tblMeritApplicantMain2 m Inner Join tblSpecialityJob j ON m.specialityJobId= j.specialityJobId inner join tblInstitute i on j.instituteId = i.instituteId Where applicantId = " + loggedInUser.applicantId+" ";
                    SqlCommand cmd = new SqlCommand(query, con);
                    DataTable dt = new DataTable();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    con.Open();
                    sda.Fill(dt);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        EntityDDL obj = new EntityDDL();
                        obj.id = 1;
                        obj.value = dr[1].TooString();
                        model.listType.Add(obj);
                        //obj.testingCenterName = dr["name"].TooString();
                    }
                    model.listType = new ConsentDAL().GetTypeApplicantHasMerit(loggedInUser.applicantId, roundId);
                    //if (model.listType != null && model.listType.Count > 0)
                    model.listConsent = new ConsentDAL().GetAllByApplicant(loggedInUser.applicantId);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        Consent obj = new Consent();
                        obj.consentId = 1;
                        obj.consentType = dr[1].TooString();
                        model.listConsent.Add(obj);
                        //obj.testingCenterName = dr["name"].TooString();
                    }
                }


            }

            return View(model);
        }


        public ActionResult ApplicantJoining()
        {
            ApplicantJoiningModel model = new ApplicantJoiningModel();
            model.applicantFinal = new JoiningDAL().GetByApplicantDetailById(loggedInUser.applicantId);
            model.info = new ApplicantDAL().GetApplicantInfo(ProjConstant.inductionId, ProjConstant.phaseId, loggedInUser.applicantId);
            return View(model);
        }


        [ValidateInput(false)]
        public ActionResult SaveConsentData(ConsentModel ModelSave, HttpPostedFileBase files)
        {

            int roundId = WebConfigurationManager.AppSettings["ConsentRound"].TooInt();
            Consent obj = ModelSave.consent;
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            obj.consentId = obj.consentId.TooInt();
            obj.applicantId = loggedInUser.applicantId;
            obj.typeId = obj.typeId.TooInt();
            obj.consentTypeId = obj.consentTypeId.TooInt();
            obj.roundId = roundId;
            Message m = new ConsentDAL().AddUpdate(obj);
            return Redirect("/"+ProjConstant.preUrl+"/applicant/consent");
        }
    }
}