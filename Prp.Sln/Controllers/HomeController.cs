using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            HomeModel model = new HomeModel();
            return View(model);
        }

        public ActionResult LandingPage()
        {
            return View();
        }

        public ActionResult PNAS()
        {
            return View();
        }

        public ActionResult ChangePassword()
        {
            HomeModel model = new HomeModel();
            return View(model);
        }

        [HttpPost]
        public JsonResult ChangePasswordEvent(ChangePassword obj)
        {
            obj.id = loggedInUser.applicantId;
            Message msg = new ApplicantDAL().ChangePassword(obj);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [CheckHasRight]
        public ActionResult TestRighsPage()
        {
            HomeModel model = new HomeModel();
            return View(model);
        }

        public ActionResult NoRights()
        {
            HomeModel model = new HomeModel();
            return View(model);
        }
    }
}