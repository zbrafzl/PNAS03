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
    public class InstitueAdminController : BaseAdminController
    {
        public ActionResult InstituteManage()
        {
            InstituteModelAdmin model = new InstituteModelAdmin();
            model.institute.instituteTypeId = Request.QueryString["typeId"].TooInt();
            model.institute.provinceId = Request.QueryString["provinceId"].TooInt();
            model.listType = new ConstantDAL().GetAll(ProjConstant.Constant.instituteType);

            model.listProvince = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.province
                 , ProjConstant.Constant.pakistan);

            if (model.institute.instituteTypeId == 0)
                model.institute.instituteTypeId = model.listType.FirstOrDefault().id.TooInt();


            if (model.institute.provinceId == 0)
                model.institute.provinceId = 196;

            model.list = new InstitueDAL().GetAll(model.institute.instituteTypeId, model.institute.provinceId).OrderBy(x => x.name).ToList();

            return View(model);
        }

        public ActionResult InstituteSetup()
        {
            InstituteModelAdmin model = new InstituteModelAdmin();
            int instituteId = Request.QueryString["id"].TooInt();
            if (instituteId > 0)
                model.institute = new InstitueDAL().GetById(instituteId);


            model.listHospital = new HospitalDAL().GetHospitalForInstitute(instituteId);

            model.listType = new ConstantDAL().GetAll(ProjConstant.Constant.instituteType);

            model.listProvince = new RegionDAL().RegionGetByCondition(ProjConstant.Constant.Region.province, ProjConstant.Constant.pakistan);

            return View(model);
        }

        //[HttpPost]
        //public JsonResult GetInstituteDDL(DDLInstitute obj)
        //{
        //    List<EntityDDL> list = new InstitueDAL().GetInstituteDDL(obj);
        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}

        [ValidateInput(false)]
        public ActionResult SaveInstituteData(InstituteModelAdmin ModelSave, HttpPostedFileBase files)
        {
            Institute obj = ModelSave.institute;

            obj.name = obj.name.TooString();
            obj.code = "0000";
            obj.instituteTypeId = obj.instituteTypeId.TooInt();
            obj.districtId = obj.districtId.TooInt();
            obj.provinceId = obj.provinceId.TooInt();
            obj.isActive = obj.isActive.TooBoolean();
            obj.sortOrder = obj.sortOrder.TooInt();
            obj.dated = DateTime.Now;
            obj.adminId = loggedInUser.userId;

            obj.hospitalIds = obj.hospitalIds.TooString().TrimStart(',').TrimEnd(',');

            Message m = new InstitueDAL().AddUpdate(obj);

            return Redirect("/admin/institute-manage?typeId=" + obj.instituteTypeId);
        }


        #region Joined Applicant


       
        #endregion

    }
}