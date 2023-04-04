using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class SubjectAdminController : BaseAdminController
    {
        // GET: nadmin/SubjectAdmin
        public ActionResult SubjectSetup()
        {
            SpecialityModelAdmin model = new SpecialityModelAdmin();
            int subjectId = Request.QueryString["id"].TooInt();
            if (subjectId > 0)
                model.speciality = new SpecialityDAL().GetById(subjectId);
            return View(model);
        }


       

        public ActionResult SubjectManage()
        {
            SpecialityModelAdmin model = new SpecialityModelAdmin();

            model.inductionId = Request.QueryString["inductionId"].TooInt();
            model.listInduction = DDLInduction.GetAll("GetAllActive");

            if (model.inductionId == 0)
            {
                if (model.listInduction != null && model.listInduction.Count == 1)
                    model.inductionId = model.listInduction.FirstOrDefault().id.TooInt();
            }
            if (model.inductionId > 0)
                model.list = new SpecialityDAL().GetWithJobByInduction(model.inductionId);
            else
                model.list = new SpecialityDAL().GetAll();

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult SaveSubjectData(SpecialityModelAdmin ModelSave, HttpPostedFileBase files)
        {
            Speciality obj = ModelSave.speciality;

            obj.name = obj.name.TooString();
            obj.isActive = obj.isActive.TooBoolean();
            obj.sortOrder = obj.sortOrder.TooInt();
            obj.dated = DateTime.Now;
            obj.adminId = loggedInUser.userId;

            Message m = new SpecialityDAL().AddUpdate(obj);

            return Redirect("/admin/speciality-manage");
        }
    }
}