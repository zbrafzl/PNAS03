using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class LoggedInAdminController : Controller
    {
        // GET: nadmin/LoggedInAdmin
        public ActionResult Login()
        {
            User app = ProjFunctions.CookiesAdminGet();
            if (app != null && app.userId > 0)
            {
                if (app.typeId == ProjConstant.Constant.UserType.bank)
                {
                    return Redirect("/admin/voucher-list-bank");
                }
                else
                {
                    return Redirect("/admin/index");
                }
            }
            return View();
        }



        [HttpPost]
        public JsonResult LoggedInUser(LoginUser login)
        {
            Message msg = new Message();

            User app = ProjFunctions.CookiesAdminGet();
            if (app == null)
            {
                try
                {
                    app = new UserDAL().Login(login.userName, login.password);

                    if (app != null && app.userId > 0)
                    {

                        if (app.typeId == ProjConstant.Constant.UserType.institute)
                        {
                            Institute inst = new InstitueDAL().GetByUserId(app.userId);
                            app.reffId = inst.instituteId;
                            app.displayName = inst.name;
                        }
                        else if (app.typeId == ProjConstant.Constant.UserType.hospital)
                        {
                            Hospital inst = new HospitalDAL().GetByUserId(app.userId);
                            app.reffId = inst.hospitalId;
                            app.displayName = inst.name;
                        }
                        else
                        {
                            app.reffId = 0;
                            app.displayName = app.firstName + " " + app.lastName + " - " + app.typeName;
                        }
                        app.displayName = app.displayName.TooString();
                        ProjFunctions.CookiesAdminSet(app);
                        Session["LoggedinAdmin"] = app.userId;
                        msg.id = app.userId;
                        msg.status = true;
                    }
                }
                catch (Exception ex)
                {
                    msg.id = 0;
                    msg.status = false;
                    msg.msg = ex.Message;
                }
            }
            else
            {
                msg.id = app.userId;
                msg.status = true;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Logout()
        {
            try
            {
                ProjFunctions.RemoveCookies(ProjConstant.Cookies.loggedInAdmin);
            }
            catch (Exception)
            {
            }

            return Redirect("/admin");

        }

        public ActionResult NoRights()
        {
            return View();

        }
    }
}