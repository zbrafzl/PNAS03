using Newtonsoft.Json;
using Prp.Data;
using Prp.Data.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class VoucherAdminController : BaseAdminController
    {
        // GET: nadmin/VoucherAdmin
        [CheckHasRight]
        public ActionResult VoucherList()
        {
            VoucherAdminModel model = new VoucherAdminModel();
            return View(model);
        }


        public ActionResult VoucherDetail()
        {
            VoucherAdminModel model = new VoucherAdminModel();

            int applicantId = Request.QueryString["applicantId"].TooInt();
            int inductionId = ProjConstant.inductionId;
            int phaseId = ProjConstant.phaseId;

          
            model.applicant = new ApplicantDAL().GetApplicant(inductionId, applicantId);
            model.applicantInfo = new ApplicantDAL().GetApplicantInfoDetail(inductionId, phaseId, applicantId);
            model.voucher = new ApplicantDAL().GetApplicantVoucher(inductionId, ProjConstant.phaseId, applicantId);
            model.voucherStatus = new ApplicantDAL().GetApplicationStatus(inductionId, ProjConstant.phaseId
                     , applicantId, ProjConstant.Constant.ApplicationStatusType.voucherPhf).FirstOrDefault();

            return View(model);
        }

        [CheckHasRight]
        public ActionResult VoucherBankList()
        {
            VoucherAdminModel model = new VoucherAdminModel();
            return View(model);
        }

        public ActionResult VoucherListNotVerified()
        {
            VoucherAdminModel model = new VoucherAdminModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult VoucherListSearch(VoucherSearch obj)
        {

            obj.startDate = obj.dateStart.TooDate();
            obj.endDate = obj.dateEnd.TooDate();
            DataTable dataTable = new VoucherDAL().VoucherSearch(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult VoucherSearchWithBank(VoucherSearch obj)
        {
            obj.search = obj.search.TooString();

            DataTable dataTable = new VoucherDAL().VoucherSearchWithBank(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult VoucherListSearchBank(VoucherSearch obj)
        {

            obj.startDate = obj.dateStart.TooDate();
            obj.endDate = obj.dateEnd.TooDate();
            DataTable dataTable = new VoucherDAL().VoucherSearchBank(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }


        public ActionResult VoucherListAll()
        {
            VoucherAdminModel model = new VoucherAdminModel();

            string condition = "GetByStatus";
            model.countryType = Request.QueryString["id"].TooInt();
            if (model.countryType == 0)
                model.countryType = 1;

            model.listVoucher = new VoucherDAL().GetVoucherByCondtion(ProjConstant.Constant.ApplicationStatus.completed, model.countryType, condition);
            return View(model);
        }


        [HttpPost]
        public JsonResult UpdateStatus(ApplicationStatus obj)
        {
            obj.inductionId = ProjConstant.inductionId;

            Message msg = new ApplicantDAL().ApplicantStatusUpdate(obj.applicantId
                , ProjConstant.Constant.ApplicationStatusType.voucherPhf, obj.statusId);
           return Json(msg, JsonRequestBehavior.AllowGet);
        }



        [ValidateInput(false)]
        public ActionResult ExportDataToExcelAndDownload(VoucherAdminModel ModelSave)
        {
            Message msg = new Message();
            VoucherSearch search = ModelSave.search;

            search.startDate = search.dateStart.TooDate();
            search.endDate = search.dateEnd.TooDate();
            search.cnicNo = search.cnicNo.TooString();
            search.applicantNo = search.applicantNo.TooString();




            try
            {
                string fileName = "VoucherList.xlsx";

                if (search.reportType == "Voucher")
                {
                    fileName = "VoucherList.xlsx";
                    if (search.countryTypeId == 1)
                        fileName = "National" + fileName;
                    else if (search.countryTypeId == 2)
                        fileName = "Foreigner" + fileName;
                }
                else
                {
                    fileName = "VoucherBankList.xlsx";
                }

                string filePath = fileName.GenerateFilePath(loggedInUser);
                if (!String.IsNullOrWhiteSpace(filePath))
                {
                    System.Data.DataTable dt = new System.Data.DataTable();
                    if (search.reportType == "Voucher")
                        dt = new VoucherDAL().VoucherExport(search);
                    else dt = new VoucherDAL().VoucherExportBank(search);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        msg = filePath.ExcelFileWrite(dt);
                        //msg = filePath.ExcelFileWrite(dt, "Sheet1", "B1");
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
            if (String.IsNullOrWhiteSpace(search.pageUrl))
                search.pageUrl = "/admin/voucher-list";
            return Redirect(search.pageUrl);
        }


    }
}