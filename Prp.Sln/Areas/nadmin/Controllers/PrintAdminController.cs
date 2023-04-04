using Prp.Data;
using Prp.Data.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class PrintAdminController : BaseAdminController
    {
        // GET: nadmin/PrintAdmin
        public ActionResult PrintJoiningApplicantInstitute()
        {
            ApplicantJoiningAdminModel model = new ApplicantJoiningAdminModel();
            List<ApplicantJoined> list = new JoiningDAL().GetAllByInstiteUser(loggedInUser.userId,0, "");
            model.listApplicant = list.Where(x => x.joiningId > 0).ToList();
            return View(model);
        }

        [CheckHasRight]
        public ActionResult PrintApplicantAttachedHospital()
        {
            ReportApplicantModel model = new ReportApplicantModel();
            model.hospitalId = Request.QueryString["hospitalId"].TooInt();
            model.attachStatusId = Request.QueryString["attachStatusId"].TooInt();
            return View(model);
        }
    }
}