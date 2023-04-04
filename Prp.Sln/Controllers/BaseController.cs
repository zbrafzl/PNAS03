using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace Prp.Sln.Controllers
{
    [CheckLoginAction]
    public class BaseController : Controller
    {

        public Applicant loggedInUser { get; set; }
        public BaseController()
        {
            loggedInUser = ProjFunctions.CookieApplicantGet();


        }
    }


    public class BaseLoginController : Controller
    {
        public Applicant loggedInUser { get; set; }
        public BaseLoginController()
        {
            loggedInUser = ProjFunctions.CookieApplicantGet();


        }
    }

    public class CheckLoginActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            try
            {
                Applicant loggedInUser = ProjFunctions.CookieApplicantGet();

                if (loggedInUser == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "LoggedIn",
                        action = "Logout"
                    }));
                }
            }
            catch (Exception)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    controller = "LoggedIn",
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
                Applicant loggedInUser = ProjFunctions.CookieApplicantGet();
                if (loggedInUser == null)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        controller = "LoggedIn",
                        action = "Logout"
                    }));
                }
                else
                {
                    #region CheckRights

                    try
                    {
                        string url = HttpContext.Current.Request.Url.AbsolutePath.ToLower().TrimStart('/');
                        bool rightHas = new MenuDAL().CheckPageHasRight(1, loggedInUser.applicantId, url);

                        if (rightHas == false)
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                            {
                                controller = "Home",
                                action = "NoRights"
                            }));
                        }

                    }
                    catch (Exception)
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                        {
                            controller = "Home",
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
                    controller = "LoggedIn",
                    action = "Logout"
                }));
            }
            //var abc = new ApplicantDegrees();            
            //List<DegreeMarks> dmlist = new List<DegreeMarks>();
            //DegreeMarks dm = new DegreeMarks();
            //dm.applicantId = "1";
            //abc.DegreeMarks = dmlist;
        }
    }

    public class TentativeMarks { 
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

    //public class DegreeMarks {
    //    public string degreeTypeId { get; set; }
    //    public string applicantId { get; set; }
    //    public string obtainedMarks { get; set; }
    //    public string totalMarks { get; set; }
    //    public DateTime passingDate { get; set; }
    //    public string degreePicFront { get; set; }
    //}

    //public class ApplicantDegrees{
    //    public string applicantId { get; set; }
    //    public string matricImage { get; set; }
    //    public string faImage { get; set; }
    //    public string imageDegreeBSNursing { get; set; }
    //    public string imageDegreeGeneralNursing { get; set; }
    //    public string imageDegreeMidwifery { get; set; }
    //    public string imageDegreeBsc { get; set; }
    //    public string imageDegreeMsc { get; set; }
    //    public List<DegreeMarks> DegreeMarks = new List<DegreeMarks>();
    //}

   
}