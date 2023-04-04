using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class HardshipController : BaseAdminController
    {
        // GET: nadmin/Hardship
        [CheckHasRight]
        public ActionResult ApplicantDetail()
        {
            HardshipAdminModel model = new HardshipAdminModel();
            try
            {
                int inductionId = AdminHelper.GetInductionId();
                int phaseId = AdminHelper.GetPhaseId();

                string key = Request.QueryString["key"].TooString();
                string value = Request.QueryString["value"].TooString();

                if (!String.IsNullOrEmpty(key) && !String.IsNullOrWhiteSpace(value))
                {

                    Message msg = new ApplicantDAL().GetApplicantIdBySearch(value, key);
                    int applicantId = msg.id.TooInt();
                    if (applicantId > 0)
                    {
                        model = AdminFunctions.GenerateModelHardship(applicantId);
                        model.listInduction = DDLInduction.GetAll("All");
                        model.applicantId = applicantId;
                        model.search.key = key;
                        model.search.value = value;
                    }
                    model.applicantId = applicantId;
                    model.requestType = 1;
                }
                else
                {
                    model.applicantId = 0;
                    model.requestType = 2;
                }
                model.inductionId = inductionId;
                model.search.key = key;
                model.search.value = value;
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