using Newtonsoft.Json;
using Prp.Data;
using Prp.Data.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class SMSAdminController : Controller
    {
        [CheckHasRight]
        public ActionResult SearchSms()
        {
            SMSModelAdmin model = new SMSModelAdmin();
            model.search.applicantId= Request.QueryString["applicantId"].TooInt();
            return View(model);
        }


        [HttpPost]
        public ActionResult SearchSMSByApplicant(SmsEntity obj)
        {
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            DataTable dataTable = new SMSDAL().SearchSMSByApplicant(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }
    }
}