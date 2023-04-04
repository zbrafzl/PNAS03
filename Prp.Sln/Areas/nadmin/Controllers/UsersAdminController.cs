using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class UsersAdminController : BaseAdminController
    {
        // GET: nadmin/UsersAdmin
        [CheckHasRight]
        public ActionResult Setup()
        {
            UserSetupModel model = new UserSetupModel();

            DDLConstants ddl = new DDLConstants();
            ddl.typeId = ProjConstant.Constant.userType;
            model.listType = new ConstantDAL().GetConstantDDL(ddl);

            ddl = new DDLConstants();
            ddl.typeId = ProjConstant.Constant.department;
            model.listDepartment = new ConstantDAL().GetConstantDDL(ddl);

            ddl = new DDLConstants();
            ddl.typeId = ProjConstant.Constant.designantion;
            model.listDesgnation = new ConstantDAL().GetConstantDDL(ddl);



            int userId = Request.QueryString["id"].TooInt();
            if (userId > 0)
                model.user = new UserDAL().GetById(userId);

            return View(model);
        }

        [CheckHasRight]
        public ActionResult Manage()
        {
            UserManageModel model = new UserManageModel();

            model.typeId = Request.QueryString["typeId"].TooInt();

            DDLConstants ddl = new DDLConstants();
            ddl.typeId = ProjConstant.Constant.userType;
            model.listType = new ConstantDAL().GetConstantDDL(ddl);

            //if (model.typeId == 0)
                //model.typeId = model.listType.FirstOrDefault().id.TooInt();

            model.listUser = new UserDAL().GetByTypeId(model.typeId);

            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult SaveUserData(UserSetupModel ModelSave, HttpPostedFileBase files)
        {
            User obj = ModelSave.user;

            obj.userId = obj.userId.TooInt();
            obj.typeId = obj.typeId.TooInt();
            obj.parentId = obj.parentId.TooInt();
            obj.firstName = obj.firstName.TooString();
            obj.lastName = obj.lastName.TooString();
            obj.emailId = obj.emailId.TooString();
            obj.password = obj.password.TooString();
            obj.isActive = obj.isActive.TooBoolean();
            obj.dated = DateTime.Now;
            obj.adminId = loggedInUser.userId;
            Message m = new UserDAL().AddUpdate(obj);

            return Redirect("/admin/user-manage?typeId=" + obj.typeId);
        }

        [CheckHasRight]
        public ActionResult ChangePassword()
        {
            UserSetupModel model = new UserSetupModel();
            return View(model);
        }

        [ValidateInput(false)]
        public ActionResult ChangePasswordData(UserSetupModel ModelSave, HttpPostedFileBase files)
        {
            User obj = ModelSave.user;

            obj.userId = loggedInUser.userId;
            obj.password = obj.password.TooString();
            obj.passwordNew = obj.passwordNew.TooString();
            Message m = new UserDAL().UpdatePassword(obj);

            if (m.status)
                return Redirect("/admin/logout");
            else return Redirect("/admin/change-password");
        }


       
    }
}