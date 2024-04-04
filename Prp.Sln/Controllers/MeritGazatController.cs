using Newtonsoft.Json;
using Prp.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Prp.Sln.Controllers
{
    public class MeritGazatController : Controller
    {

        #region Gazette

        public ActionResult GazatFCPS()
        {
            MeritGazatModel model = new MeritGazatModel();
            return View(model);
        }

        public ActionResult Gazette()
        {
            MeritGazatModel model = new MeritGazatModel();
            return View(model);
        }

        public ActionResult MeritList()
        {
            MeritGazatModel model = new MeritGazatModel();
            return View(model);
        }
        public ActionResult MeritListFemale()
        {
            MeritGazatModel model = new MeritGazatModel();
            return View(model);
        }

        public ActionResult MeritListEvening()
        {
            MeritGazatModel model = new MeritGazatModel();
            return View(model);
        }
        public ActionResult MeritListFemaleEvening()
        {
            MeritGazatModel model = new MeritGazatModel();
            return View(model);
        }

        public ActionResult GazatMS()
        {
            MeritGazatModel model = new MeritGazatModel();
            return View(model);
        }

        public ActionResult GazatMD()
        {
            MeritGazatModel model = new MeritGazatModel();
            return View(model);
        }

        public ActionResult GazatMDS()
        {
            MeritGazatModel model = new MeritGazatModel();
            return View(model);
        }

        public ActionResult GazatFCPSD()
        {
            MeritGazatModel model = new MeritGazatModel();
            return View(model);
        }

        #endregion

        #region Merit

        public ActionResult MeritFCPS()
        {
            MeritGazatModel model = new MeritGazatModel();
            return View(model);
        }

        public ActionResult MeritMS()
        {
            MeritGazatModel model = new MeritGazatModel();
            return View(model);
        }

        public ActionResult MeritMD()
        {
            MeritGazatModel model = new MeritGazatModel();
            return View(model);
        }

        public ActionResult MeritMDS()
        {
            MeritGazatModel model = new MeritGazatModel();
            return View(model);
        }

        public ActionResult MeritFCPSD()
        {
            MeritGazatModel model = new MeritGazatModel();
            return View(model);
        }

        #endregion

        public ActionResult JobListing()
        {
            MeritGazatModel model = new MeritGazatModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult GazatGetAllByTypeView(GazatMerit obj)
        {
            obj.search = obj.search.TooString();
            DataTable dataTable = new MeritDAL().GazatGetAllByTypeView(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult GazzetGetAllByTypeView(GazatMerit obj)
        {
            obj.search = obj.search.TooString();
            DataTable dataTable = new MeritDAL().GazetteGetAllByTypeView(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult MeritGetAllByTypeView(GazatMerit obj)
        {
            obj.search = obj.search.TooString();
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            obj.roundNo = ProjConstant.consentRound;
            DataTable dataTable = new MeritDAL().MeritGetAllByTypeView(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [ValidateInput(false)]
        public ActionResult ExportDataToExcelAndDownload(MeritGazatModel ModelSave)
        {
            Message msg = new Message();
            GazatMerit search = new GazatMerit();
            search.typeId = ModelSave.gazatMerit.typeId;
            try
            {

                string fileName = ModelSave.exportExcel.fileName + ".xlsx";
                string filePath = fileName.GenerateFilePath("/ExcelFiles/Gazat/");
                if (!String.IsNullOrWhiteSpace(filePath))
                {
                    System.Data.DataTable dt = new System.Data.DataTable();

                    dt = new MeritDAL().GazatGetAllByTypeExport(search);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        msg = filePath.ExcelFileWrite(dt);
                        filePath.FileDownload();
                    }
                    else
                    {
                        msg.status = false;
                        msg.msg = "";
                    }
                }
                else
                {
                    msg.status = false;
                    msg.msg = "Error : File path and name creating.";
                }
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = "Error in exported : " + ex.Message;
            }
            if (String.IsNullOrWhiteSpace(ModelSave.exportExcel.url))
                ModelSave.exportExcel.url = "/";
            return Redirect(ModelSave.exportExcel.url);
        }



    }
}