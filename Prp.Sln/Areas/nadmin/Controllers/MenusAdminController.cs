using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class MenusAdminController : BaseAdminController
    {
        #region Menu
        [CheckHasRight]
        public ActionResult Setup()
        {
            MenuSetupModel model = new MenuSetupModel();

            DDLConstants ddl = new DDLConstants();
            ddl.typeId = ProjConstant.Constant.menuType;
            model.listType = new ConstantDAL().GetConstantDDL(ddl);


            ddl = new DDLConstants();
            ddl.typeId = ProjConstant.Constant.appType;
            model.listApp = new ConstantDAL().GetConstantDDL(ddl);

            DDLMenu ddlp = new DDLMenu();
            ddlp.condition = "GetParent";
            model.listParent = new MenuDAL().GetMenuForDDL(ddlp);

            int userId = Request.QueryString["id"].TooInt();
            if (userId > 0)
                model.menu = new MenuDAL().GetById(userId);

            return View(model);
        }
        [CheckHasRight]
        public ActionResult Manage()
        {
            MenuManageModel model = new MenuManageModel();

            model.appId = Request.QueryString["appId"].TooInt();
            model.parentId = Request.QueryString["parentId"].TooInt();

            DDLConstants ddl = new DDLConstants();
            ddl.typeId = ProjConstant.Constant.appType;
            model.listType = new ConstantDAL().GetConstantDDL(ddl);

            if (model.appId == 0)
                model.appId = 2;// model.listType.FirstOrDefault().id.TooInt();


            model.listParent = new MenuDAL().GetParent(model.appId);

            model.listMenu = new MenuDAL().Search(model.appId, model.parentId);

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult SaveMenuData(MenuSetupModel ModelSave, HttpPostedFileBase files)
        {
            Menu obj = ModelSave.menu;

            obj.menuId = obj.menuId.TooInt();
            obj.name = obj.name.TooString();
            obj.nameDisplay = obj.nameDisplay.TooString();
            obj.url = obj.url.TooString();
            obj.iconClass = obj.iconClass.TooString();
            obj.isMenu = obj.isMenu.TooBoolean();
            obj.isActive = obj.isActive.TooBoolean();
            obj.typeId = obj.typeId.TooInt();
            obj.appId = obj.appId.TooInt();
            obj.parentId = obj.parentId.TooInt();
            obj.dated = DateTime.Now;
            obj.adminId = loggedInUser.userId;
            Message m = new MenuDAL().AddUpdate(obj);

            return Redirect("/admin/menu-manage?typeId=" + obj.appId);
        }

        #endregion

        #region Menu Association
        [CheckHasRight]
        public ActionResult AccessUserType()
        {
            MenuAccessModel model = new MenuAccessModel();
            DDLConstants ddl = new DDLConstants();
            ddl.typeId = ProjConstant.Constant.userType;
            model.listType = new ConstantDAL().GetConstantDDL(ddl);
            return View(model);
        }
        [CheckHasRight]
        public ActionResult AccessUser()
        {

            MenuAccessModel model = new MenuAccessModel();
            DDLConstants ddl = new DDLConstants();
            ddl.typeId = ProjConstant.Constant.userType;
            model.listType = new ConstantDAL().GetConstantDDL(ddl);

            return View(model);
        }


        [HttpGet]
        public JsonResult GetMenuAccessListForUser(int userId)
        {
            List<Menu> list = new MenuDAL().GetMenuListForUserId(userId).OrderBy(x => x.nameDisplay).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMenuAccessListForUserType(int typeId)
        {
            List<Menu> list = new MenuDAL().GetMenuListForUserType(typeId).OrderBy(x => x.nameDisplay).ToList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UserAccessAddUpdate(int userId, string menuIds)
        {
            menuIds = menuIds.TooString().TrimStart(',').TrimEnd(',');
            Message msg = new MenuDAL().AddUpdateUserAccess(userId, menuIds);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UserTypeAccessAddUpdate(int typeId, string menuIds)
        {
            menuIds = menuIds.TooString().TrimStart(',').TrimEnd(',');
            Message msg = new MenuDAL().AddUpdateUserTypeAccess(typeId, menuIds);
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}