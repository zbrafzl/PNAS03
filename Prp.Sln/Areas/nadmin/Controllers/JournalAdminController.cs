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
    public class JournalAdminController : BaseAdminController
    {
        [CheckHasRight]
        public ActionResult JournalSetup()
        {
            JournalModelAdmin model = new JournalModelAdmin();

            int id = Request.QueryString["id"].TooInt();
            model.journal = new MasterSetupDAL().JournalGetById(id);


            DDLConstants dDLConstant = new DDLConstants();
            dDLConstant.condition = "";
            dDLConstant.typeId = ProjConstant.Constant.metalTiers;
            model.listBatch = new ConstantDAL().GetConstantDDL(dDLConstant);

            dDLConstant = new DDLConstants();
            dDLConstant.condition = "";
            dDLConstant.typeId = ProjConstant.Constant.journalDispline;
            model.listDiscipline = new ConstantDAL().GetConstantDDL(dDLConstant);


            dDLConstant = new DDLConstants();
            dDLConstant.condition = "";
            dDLConstant.typeId = ProjConstant.Constant.journalImpactFactorType;
            model.listType = new ConstantDAL().GetConstantDDL(dDLConstant);

            DDLRegions dDLRegion = new DDLRegions();
            dDLRegion.condition = "ByType";
            dDLRegion.typeId = ProjConstant.Constant.Region.country;
            model.listRegion = new MasterSetupDAL().GetRegionDDL(dDLRegion);

            return View(model);
        }


        [ValidateInput(false)]
        public ActionResult SaveJournalData(JournalModelAdmin ModelSave, HttpPostedFileBase files)
        {
            ResearchJournal obj = ModelSave.journal;
            obj.researchJournalId = obj.researchJournalId.TooInt();
            obj.typeId = obj.typeId.TooInt();
            obj.name = obj.name.TooString();
            obj.code = obj.code.TooString();
            obj.url = obj.url.TooString();
            obj.regionId = obj.regionId.TooInt();
            obj.batchId = obj.batchId.TooInt();
            obj.displineId = obj.displineId.TooInt();
            obj.isActive = obj.isActive.TooBoolean();
            obj.dated = DateTime.Now;
            obj.adminId = loggedInUser.userId;
            Message m = new MasterSetupDAL().JournalAddUpdate(obj);

            return Redirect("/admin/research-journal-manage");
        }




        [CheckHasRight]
        public ActionResult JournalManage()
        {
            ResearchJournalModel model = new ResearchJournalModel();
            model.listType = new ConstantDAL().GetAll(ProjConstant.Constant.journalDispline).OrderBy(x => x.id).ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult JournalSearch(ResearchJournalSearch obj)
        {
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            DataTable dataTable = new MasterSetupDAL().ResearchJournalSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [CheckHasRight]
        public ActionResult JournalApplicants()
        {
            ResearchJournalModel model = new ResearchJournalModel();
            model.researchJournalId = Request.QueryString["id"].TooInt();
            return View(model);
        }


        [HttpPost]
        public ActionResult ApplicantResearchSearch(ResearchJournalSearch obj)
        {
            obj.inductionId = ProjConstant.inductionId;
            obj.phaseId = ProjConstant.phaseId;
            DataTable dataTable = new MasterSetupDAL().ApplicantResearchSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        //
    }
}