using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public static class AdminHelper
    {
        public static int GetInductionId()
        {
            int inductionId = 0;
            try
            {

                if (HttpContext.Current.Request.RequestContext.RouteData.Values["induction"].TooInt()  > 0)
                    inductionId = HttpContext.Current.Request.RequestContext.RouteData.Values["induction"].TooInt();
            }
            catch (Exception)
            {
                inductionId = ProjConstant.inductionId;
            }
            if (inductionId == 0) inductionId = ProjConstant.inductionId;

            return inductionId;

        }

        public static int GetPhaseId()
        {
            int phaseId = 0;
            try
            {
                if (HttpContext.Current.Request.RequestContext.RouteData.Values["phase"].TooInt() > 0)
                    phaseId = HttpContext.Current.Request.RequestContext.RouteData.Values["phase"].TooInt();
            }
            catch (Exception)
            {
                phaseId = ProjConstant.phaseId;
            }
            if (phaseId == 0) phaseId = ProjConstant.phaseId;

            return phaseId;

        }
    }
}