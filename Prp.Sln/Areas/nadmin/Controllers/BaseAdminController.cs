using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    [CheckLoginAction]
    public class BaseAdminController : Controller
    {
        public User loggedInUser { get; set; }
        public BaseAdminController()
        {
            loggedInUser = ProjFunctions.CookiesAdminGet();

        }

        
    }

    

    public class TentativeMarks
    {
        public int applicantId { get; set; }
        public double fscTotalWeightage { get; set; }
        public double fscObtained { get; set; }
        public double gnTotalWeightage { get; set; }
        public double gnObtained { get; set; }
        public double mwTotalWeightage { get; set; }
        public double mwObtained { get; set; }
        public double bsnTotalWeightage { get; set; }
        public double bsnObtained { get; set; }
        public double extraTotalWeightage { get; set; }
        public double extraObtained { get; set; }
        public double minExpRequired { get; set; }
        public double expTotalWeightage { get; set; }
        public double expObtained { get; set; }
        public double entryTestTotalWeightage { get; set; }
        public double entryTestObtained { get; set; }
    }

    public class CheckLoginActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            try
            {
                User loggedInUser = ProjFunctions.CookiesAdminGet();

                if (loggedInUser == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "LoggedInAdmin",
                        action = "Logout"
                    }));
                }
            }
            catch (Exception)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "LoggedInAdmin",
                    action = "Logout"
                }));
            }
        }
    }


    public class CheckHasRightAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            try
            {
                User loggedInUser = ProjFunctions.CookiesAdminGet();
                if (loggedInUser == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "LoggedInAdmin",
                        action = "Logout"
                    }));
                }
                else
                {
                    #region CheckRights

                    try
                    {
                        string url = HttpContext.Current.Request.Url.AbsolutePath.ToLower().TrimStart('/');

                        if (url.Contains("-induction"))
                        {
                            int index = url.IndexOf("-induction");
                            if (index > 0)
                            {
                                url = url.Substring(0, index);
                            }
                        }

                        if (loggedInUser.typeId == 86)
                        {
                            loggedInUser.userId = 1;
                        }

                        bool rightHas = new MenuDAL().CheckPageHasRight(2,loggedInUser.userId, url);

                        if (loggedInUser.userId > 1 && rightHas == false)
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                            {
                                controller = "LoggedInAdmin",
                                action = "NoRights"
                            }));
                        }

                    }
                    catch (Exception)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "LoggedInAdmin",
                            action = "NoRights"
                        }));
                    }

                    #endregion
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "LoggedInAdmin",
                    action = "Logout"
                }));
            }

        }
    }
}