using Prp.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class FeedbackAdminController : BaseAdminController
    {
        public ActionResult FeebBackList()
        {
            FeedbackAdminModel model = new FeedbackAdminModel();

            FeedbackSearch obj = new FeedbackSearch();

            int top = Request.QueryString["top"].TooInt();

            int page = Request.QueryString["page"].TooInt();

            string search = Request.QueryString["search"].TooString();
            string start = Request.QueryString["start"].TooString();
            string end = Request.QueryString["end"].TooString();

            if (String.IsNullOrWhiteSpace(start))
                start = "21-09-2020";

            if (String.IsNullOrWhiteSpace(end))
                end = start;

            obj.search = search;
            obj.startDate = start.TooDate('-');
            obj.endDate = end.TooDate('-');


            obj.top = top;
            obj.pageNum = page;

            if (top == 0)
                obj.top = 5000;

            if (page == 0)
                obj.pageNum = 1;


            model.listFeedback = new FeedBackDAL().GetBySearch(obj);
            return View(model);
        }
    }
}