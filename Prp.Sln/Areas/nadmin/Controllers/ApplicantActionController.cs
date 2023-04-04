using Newtonsoft.Json;
using Prp.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class ApplicantActionController : BaseAdminController
    {
        public ActionResult ActionSetup()
        {
            ApplicantActionAdminModel model = new ApplicantActionAdminModel();
            model.listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant);
            string url = Request.Url.AbsolutePath.ToLower(); ;
            if (url.Contains("reg-term") )
            {
                model.typeId = 0;
                model.heading = "Resignation/Termination";
            }
            else if (url.Contains("freez"))
            {
                model.typeId = ProjConstant.Constant.ActionStatusType.freez;
                model.heading = "Freez";
            }
            model.applicantId = Request.QueryString["applicantId"].TooInt();

            if (model.applicantId > 0)
            {
                model.joinApplicant = new JoiningDAL().GetJoinedApplicantDetailById(model.applicantId);

                if (model.joinApplicant.applicantId > 0)
                {
                    model.applicant = new ApplicantDAL().GetApplicant(0, model.applicantId);
                    model.applicantInfo = new ApplicantDAL().GetApplicantInfo(0, 0, model.applicantId);
                    model.action = new ActionDAL().GetById(model.applicantId, model.typeId);
                }
            }
            return View(model);
        }
      
        public ActionResult ActionLisiting()
        {
            ApplicantActionAdminModel model = new ApplicantActionAdminModel();
            model.listInstitute = DDLInstitute.GetAll(ProjConstant.DDL.Institute.hasJoinedApplicant);
            string url = Request.Url.AbsolutePath;
            if (url.Contains("reg-term"))
            {
                model.typeId = 0;
            }
            else if (url.Contains("freez"))
            {
                model.typeId = ProjConstant.Constant.ActionStatusType.freez;
                model.heading = "Freez";
            }
            return View( model);
        }
 
        [HttpPost]
        public ActionResult ActionSearch(SearchReport obj)
        {
            obj.adminId = loggedInUser.userId;
            obj.search = obj.search.TooString();
            DataTable dataTable = new ReportDAL().ActionSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }


        [HttpPost]
        public JsonResult ActionSetupSave(ApplicantAction ac)
        {
            ac.startDate = DateTime.Now;
            ac.endDate = DateTime.Now;
            if (!String.IsNullOrWhiteSpace(ac.dateStart))
                ac.startDate = ac.dateStart.TooDate();
            if (!String.IsNullOrWhiteSpace(ac.dateEnd))
                ac.endDate = ac.dateEnd.TooDate();
            ac.adminId = loggedInUser.userId;
            Message msg = new ActionDAL().AddUpdate(ac);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActionUpdateStatus(ApplicantAction ac)
        {
            ac.image = ac.image.TooString();
            ac.adminId = loggedInUser.userId;
            Message msg = new ActionDAL().AddUpdateStatus(ac);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }
    }
}