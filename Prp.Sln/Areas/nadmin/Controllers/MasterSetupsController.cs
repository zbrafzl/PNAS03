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
    public class MasterSetupsController : Controller
    {
        #region Region

        [CheckHasRight]
        public ActionResult RegionSetup()
        {
            return View();
        }

        [CheckHasRight]
        public ActionResult RegionManage()
        {
            RegionModelModel model = new RegionModelModel();

            DDLConstants dDLConstant = new DDLConstants();
            dDLConstant.condition = "";
            dDLConstant.typeId = ProjConstant.Constant.regionType;
            model.listType = new ConstantDAL().GetConstantDDL(dDLConstant);

            return View(model);
        }

        [HttpPost]
        public ActionResult RegionSearch(RegionSearch obj)
        {
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            DataTable dataTable = new MasterSetupDAL().RegionSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        #endregion
    }
}