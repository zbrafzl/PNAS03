using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;


namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class MeritAdminController : BaseAdminController
    {

        [CheckHasRight]
        public ActionResult ApplicantPreference()
        {
            MeritApplicantAdminModel model = new MeritApplicantAdminModel();
            try
            {
                string key = Request.QueryString["key"].TooString();
                string value = Request.QueryString["value"].TooString();

                model.search.key = key;
                model.search.value = value;

                if (!String.IsNullOrEmpty(key) && !String.IsNullOrWhiteSpace(value))
                {
                    Message msg = new ApplicantDAL().GetApplicantIdBySearch(value, key);
                    int applicantId = msg.id.TooInt();
                    if (applicantId > 0)
                    {
                        int roundId = 1;

                        int inductionId = ProjConstant.inductionId;
                        int phaseId = ProjConstant.phaseId;

                        try
                        {
                            if (RouteData.Values["induction"].TooInt() > 0)
                                inductionId = RouteData.Values["induction"].TooInt();
                        }
                        catch (Exception)
                        {
                            inductionId = ProjConstant.inductionId;
                        }

                        model.applicant = new ApplicantDAL().GetApplicant(0, applicantId);
                        model.applicantInfo = new ApplicantDAL().GetApplicantInfoDetail(0, phaseId, applicantId);
                        model.degree = new ApplicantDAL().GetApplicantDegreeDetail(0, phaseId, applicantId);
                        model.listSpecilityMerit = new MeritDAL().GetApplicantSpecialityWithMeritMarks(inductionId, applicantId, roundId);
                        model.search.key = key;
                        model.search.value = value;

                        DDLConstants dDLConstant = new DDLConstants();
                        dDLConstant.condition = "ByType";
                        dDLConstant.typeId = ProjConstant.Constant.degreeType;
                        model.listType = new ConstantDAL().GetConstantDDL(dDLConstant).OrderBy(x => x.id).ToList();
                    }

                    model.applicantId = applicantId;
                    model.requestType = 1;
                }
                else
                {

                    model.applicantId = 0;
                    model.requestType = 2;
                }

            }
            catch (Exception)
            {
                model.applicantId = 0;
                model.requestType = 3;
            }


            return View(model);
        }


        [CheckHasRight]
        public ActionResult ApplicantMeritRound2()
        {
            MeritApplicantAdminModel model = new MeritApplicantAdminModel();
            try
            {
                string key = Request.QueryString["key"].TooString();
                string value = Request.QueryString["value"].TooString();

                model.search.key = key;
                model.search.value = value;

                if (!String.IsNullOrEmpty(key) && !String.IsNullOrWhiteSpace(value))
                {
                    Message msg = new ApplicantDAL().GetApplicantIdBySearch(value, key);
                    int applicantId = msg.id.TooInt();
                    if (applicantId > 0)
                    {
                        int roundId = 2;

                        int inductionId = ProjConstant.inductionId;
                        int phaseId = ProjConstant.phaseId;

                        try
                        {
                            if (RouteData.Values["induction"].TooInt() > 0)
                                inductionId = RouteData.Values["induction"].TooInt();
                        }
                        catch (Exception)
                        {
                            inductionId = ProjConstant.inductionId;
                        }

                        model.applicant = new ApplicantDAL().GetApplicant(0, applicantId);
                        model.applicantInfo = new ApplicantDAL().GetApplicantInfoDetail(0, 0, applicantId);
                        model.degree = new ApplicantDAL().GetApplicantDegreeDetail(0, 0, applicantId);
                        model.listSpecilityMerit = new MeritDAL().GetApplicantSpecialityWithMeritMarks(inductionId, applicantId, roundId);
                        model.search.key = key;
                        model.search.value = value;

                        DDLConstants dDLConstant = new DDLConstants();
                        dDLConstant.condition = "ByType";
                        dDLConstant.typeId = ProjConstant.Constant.degreeType;
                        model.listType = new ConstantDAL().GetConstantDDL(dDLConstant).OrderBy(x => x.id).ToList();
                    }

                    model.applicantId = applicantId;
                    model.requestType = 1;
                }
                else
                {

                    model.applicantId = 0;
                    model.requestType = 2;
                }

            }
            catch (Exception)
            {
                model.applicantId = 0;
                model.requestType = 3;
            }


            return View(model);
        }

        [CheckHasRight]
        public ActionResult ApplicantMeritRound3()
        {
            MeritApplicantAdminModel model = new MeritApplicantAdminModel();
            try
            {
                string key = Request.QueryString["key"].TooString();
                string value = Request.QueryString["value"].TooString();

                model.search.key = key;
                model.search.value = value;

                if (!String.IsNullOrEmpty(key) && !String.IsNullOrWhiteSpace(value))
                {
                    Message msg = new ApplicantDAL().GetApplicantIdBySearch(value, key);
                    int applicantId = msg.id.TooInt();
                    if (applicantId > 0)
                    {
                        int roundId = 3;

                        int inductionId = ProjConstant.inductionId;
                        int phaseId = ProjConstant.phaseId;

                        try
                        {
                            if (RouteData.Values["induction"].TooInt() > 0)
                                inductionId = RouteData.Values["induction"].TooInt();

                           
                        }
                        catch (Exception)
                        {
                            inductionId = ProjConstant.inductionId;
                        }

                        if(inductionId==0) inductionId = ProjConstant.inductionId;


                        model.inductionId = inductionId;
                        model.applicant = new ApplicantDAL().GetApplicant(0, applicantId);
                        model.applicantInfo = new ApplicantDAL().GetApplicantInfoDetail(0, 0, applicantId);
                        model.degree = new ApplicantDAL().GetApplicantDegreeDetail(0, 0, applicantId);
                        model.listSpecilityMerit = new MeritDAL().GetApplicantSpecialityWithMeritMarks(inductionId, applicantId, roundId);
                        model.search.key = key;
                        model.search.value = value;

                        DDLConstants dDLConstant = new DDLConstants();
                        dDLConstant.condition = "ByType";
                        dDLConstant.typeId = ProjConstant.Constant.degreeType;
                        model.listType = new ConstantDAL().GetConstantDDL(dDLConstant).OrderBy(x => x.id).ToList();
                    }

                    model.applicantId = applicantId;
                    model.requestType = 1;
                }
                else
                {

                    model.applicantId = 0;
                    model.requestType = 2;
                }

            }
            catch (Exception)
            {
                model.applicantId = 0;
                model.requestType = 3;
            }


            return View(model);
        }
    }
}