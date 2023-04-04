using Prp.Data;
using Prp.Data.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Prp.Sln.Areas.nadmin.Controllers
{
    public class HomeAdminController : BaseAdminController
    {

        public ActionResult HomePageAdmin()
        {
            HomeModelAdmin model = new HomeModelAdmin();
            model.typeId = loggedInUser.typeId;
            model.inductionId = ProjConstant.inductionId;

            if (loggedInUser.typeId == ProjConstant.Constant.UserType.hospital)
            {
                //model.listInduction = DDLInduction.GetAll("AllCompleted");

                //int inductionId = 0;
                //try
                //{
                //    inductionId = model.listInduction.OrderBy(x => x.value).FirstOrDefault().value.TooInt();
                //}
                //catch (Exception)
                //{
                //    inductionId = 0;
                //}

                //if (inductionId > 0 && model.inductionId != inductionId)
                //    model.inductionId = inductionId;

                model.listDashBoard = new CommonDAL().GetDashboardCountInstituteHospital(0, loggedInUser.userId, "");
                //model.listDashBoard = new CommonDAL().GetDashboardCount(ProjConstant.inductionId, ProjConstant.phaseId);
                return View("InstituteHospitalBoard", model);
            }
            else if (loggedInUser.typeId == ProjConstant.Constant.UserType.institute)
            {
                model.listDashBoard = new CommonDAL().GetDashboardCount(ProjConstant.inductionId, ProjConstant.phaseId);
                //model.listDashBoard = new CommonDAL().GetDashboardCountInstituteHospital(loggedInUser.userId, 0, "");
                return View("InstituteDashBoard", model);
            }
            else if (loggedInUser.typeId == ProjConstant.Constant.UserType.bank)
            {
                return Redirect("/admin/voucher-list-bank");
            }
            else if (loggedInUser.typeId == ProjConstant.Constant.UserType.keJournalTeam)
            {
                return Redirect("/admin/research-journal-manage");
            }
            else if (loggedInUser.typeId == ProjConstant.Constant.UserType.keSenior)
            {
                model.listDashBoard = new CommonDAL().GetDashboardCount(ProjConstant.inductionId, ProjConstant.phaseId);
                return View("KeSeniorDashboard", model);
            }
            else if (loggedInUser.typeId == ProjConstant.Constant.UserType.keVerification)
            {
                model.listDashBoard = new CommonDAL().GetDashboardCount(ProjConstant.inductionId, ProjConstant.phaseId);
                return View("KeVerifyDashboard", model);
            }
            else if (loggedInUser.typeId == ProjConstant.Constant.UserType.phfAccountant)
            {
                model.listDashBoard = new CommonDAL().GetDashboardCount(ProjConstant.inductionId, ProjConstant.phaseId);
                return View("DashboardAccountPHF", model);
            }
            else
            {
                model.listDashBoard = new CommonDAL().GetDashboardCount(ProjConstant.inductionId, ProjConstant.phaseId);
                return View("DashboardInductionWise", model);
            }
        }

        [CheckHasRight]
        public ActionResult DashboardInductionWise()
        {
            HomeModelAdmin model = new HomeModelAdmin();
            model.inductionId = Request.QueryString["inductionId"].TooInt();
            if (model.inductionId == 0) model.inductionId = ProjConstant.inductionId;
            model.listDashBoard = new CommonDAL().GetDashboardCount(model.inductionId, ProjConstant.phaseId);

            return View(model);
        }

        public ActionResult Index()
         {
            HomeModelAdmin model = new HomeModelAdmin();

            //CountApplicantStatusEntity countObj = new CountApplicantStatusEntity();
            //countObj.condition = "GetApplicantStatusCount";
            //model.listStatusApplicant = new CommonDAL().GetCountApplicantStatus(countObj);

            //countObj.condition = "GetApplicationStatusCount";
            //model.listStatusApplication = new CommonDAL().GetCountApplicantStatus(countObj);

            return View(model);
        }


        public ActionResult StatusView()
        {
            HomeModelAdmin model = new HomeModelAdmin();

            //CountApplicantStatusEntity countObj = new CountApplicantStatusEntity();
            //countObj.condition = "GetApplicantStatusCount";
            //model.listStatusApplicant = new CommonDAL().GetCountApplicantStatus(countObj);

            //countObj.condition = "GetApplicationStatusCount";
            //model.listStatusApplication = new CommonDAL().GetCountApplicantStatus(countObj);

            return View(model);
        }


        public ActionResult DashboardApplicantion()
        {
            HomeModelAdmin model = new HomeModelAdmin();

            //CountApplicantStatusEntity countObj = new CountApplicantStatusEntity();
            //countObj.condition = "GetApplicantStatusCount";
            //model.listStatusApplicant = new CommonDAL().GetCountApplicantStatus(countObj);

            //countObj.condition = "GetApplicationStatusCount";
            //model.listStatusApplication = new CommonDAL().GetCountApplicantStatus(countObj);

            return View(model);
        }

        public ActionResult Welcome()
        {
            HomeModelAdmin model = new HomeModelAdmin();
            return View(model);
        }

        public ActionResult InstituteDashBoard()
        {
            HomeModelAdmin model = new HomeModelAdmin();
            return View(model);
        }

        public ActionResult HospitalDashBoard()
        {
            HomeModelAdmin model = new HomeModelAdmin();
            return View(model);
        }

        public ActionResult InstituteHospitalBoard()
        {
            HomeModelAdmin model = new HomeModelAdmin();
            return View(model);
        }

        public ActionResult KeSeniorDashboard()
        {
            HomeModelAdmin model = new HomeModelAdmin();
            return View(model);
        }


        public ActionResult KeVerifyDashboard()
        {
            HomeModelAdmin model = new HomeModelAdmin();
            return View(model);
        }

        public ActionResult DashboardAccountPHF()
        {
            HomeModelAdmin model = new HomeModelAdmin();
            return View(model);
        }


        public ActionResult DsbJoiningInstitute()
        {
            ApplicantJoiningDsbModel model = new ApplicantJoiningDsbModel();
            model.listHospInst = new JoiningDAL().GetCountInstituteHospitalWise();

            return View(model);
        }

        [HttpGet]
        public JsonResult GetCountInstituteWise()
        {
            List<ApplicantJoiningDsb> list = new JoiningDAL().GetCountInstituteHospitalWise();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTotalApplicantsWithVoucher125()
        {
            string query2 = "select av.applicantNo appNo, av.transactionIdBank transID, "
            + " FORMAT (av.dated, 'dd/MM/yyyy ') submitDate, av.amount amount from "
            + " tblApplicantVoucherBank av "
            + " where av.applicantId > 12500000 and av.applicantId < 12600000 order by av.dated";
            //query = query2;

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd2 = new SqlCommand(query2);
            DataTable table = new DataTable();
            table.Columns.Add("appNo");
            table.Columns.Add("fullName");
            table.Columns.Add("cnic");
            table.Columns.Add("transID");
            table.Columns.Add("submitDate");
            table.Columns.Add("amount");
            DataTable table2 = new DataTable();
            var JSONString = new StringBuilder();
            var JSONString2 = new StringBuilder();
            JavaScriptSerializer jk = new JavaScriptSerializer();
            try
            {
                string Conn = ConfigurationManager.ConnectionStrings["DbPrpConn"].ConnectionString;
                con = new SqlConnection(Conn);
                con.Open();
                cmd2.Connection = con;
                SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                sda2.Fill(table2);
                foreach (DataRow dr in table2.Rows)
                {
                    DataRow newRow = table.NewRow();
                    newRow[0] = dr[0];
                    string strurltest = String.Format("http://pnas2.phf.gop.pk/api/Public/GetApplicationPNAS/" + dr[0].ToString() + "");
                    WebRequest requestObjGet = WebRequest.Create(strurltest);
                    requestObjGet.Method = "GET";
                    requestObjGet.ContentType = "application/json";
                    WebResponse responseObjGet = null;
                    responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();
                    string strresulttest = null;
                    StreamReader reader = new StreamReader(responseObjGet.GetResponseStream());
                    string jsons = reader.ReadToEnd();
                    //{"$id":"1","applicantId":0,"applicationNo":0,"name":null,"pmdcNo":null,"cnicNo":null,"amount":0,"dueDate":"0001-01-01T00:00:00","message":"Applicant Not Exists","status":false}
                    string[] packets = jsons.Split(':');
                    string iddd = "";
                    iddd = packets[4].Split(',')[0].Trim();
                    iddd = iddd.Split(',')[0];
                    iddd = iddd.Split('"')[1];
                    iddd = iddd.Split('\\')[0];
                    newRow[1] = iddd;

                    iddd = packets[6].Split(',')[0].Trim();
                    iddd = iddd.Split(',')[0];
                    iddd = iddd.Split('"')[1];
                    iddd = iddd.Split('\\')[0];
                    newRow[2] = iddd;

                    newRow[3] = dr[1];
                    newRow[4] = dr[2];
                    newRow[5] = dr[3];
                    table.Rows.Add(newRow);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return Json(parentRow, JsonRequestBehavior.AllowGet);
            //return jsSerializer.Serialize(parentRow);
        }

        [HttpGet]
        public JsonResult GetTotalApplicantsWithVoucher126()
        {
            string query2 = "select av.applicantNo appNo, av.transactionIdBank transID, "
            + " FORMAT (av.dated, 'dd/MM/yyyy ') submitDate, av.amount amount from "
            + " tblApplicantVoucherBank av "
            + " where av.applicantId > 12600000 and av.applicantId < 12700000 order by av.dated";
            //query = query2;

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd2 = new SqlCommand(query2);
            DataTable table = new DataTable();
            table.Columns.Add("appNo");
            table.Columns.Add("fullName");
            table.Columns.Add("cnic");
            table.Columns.Add("transID");
            table.Columns.Add("submitDate");
            table.Columns.Add("amount");
            DataTable table2 = new DataTable();
            var JSONString = new StringBuilder();
            var JSONString2 = new StringBuilder();
            JavaScriptSerializer jk = new JavaScriptSerializer();
            try
            {
                string Conn = ConfigurationManager.ConnectionStrings["DbPrpConn"].ConnectionString;
                con = new SqlConnection(Conn);
                con.Open();
                cmd2.Connection = con;
                SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                sda2.Fill(table2);
                foreach (DataRow dr in table2.Rows)
                {
                    DataRow newRow = table.NewRow();
                    newRow[0] = dr[0];
                    string strurltest = String.Format("http://pnas2.phf.gop.pk/api/Public/GetApplicationPNAS/" + dr[0].ToString() + "");
                    WebRequest requestObjGet = WebRequest.Create(strurltest);
                    requestObjGet.Method = "GET";
                    requestObjGet.ContentType = "application/json";
                    WebResponse responseObjGet = null;
                    responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();
                    string strresulttest = null;
                    StreamReader reader = new StreamReader(responseObjGet.GetResponseStream());
                    string jsons = reader.ReadToEnd();
                    //{"$id":"1","applicantId":0,"applicationNo":0,"name":null,"pmdcNo":null,"cnicNo":null,"amount":0,"dueDate":"0001-01-01T00:00:00","message":"Applicant Not Exists","status":false}
                    string[] packets = jsons.Split(':');
                    string iddd = "";
                    iddd = packets[4].Split(',')[0].Trim();
                    iddd = iddd.Split(',')[0];
                    iddd = iddd.Split('"')[1];
                    iddd = iddd.Split('\\')[0];
                    newRow[1] = iddd;

                    iddd = packets[6].Split(',')[0].Trim();
                    iddd = iddd.Split(',')[0];
                    iddd = iddd.Split('"')[1];
                    iddd = iddd.Split('\\')[0];
                    newRow[2] = iddd;

                    newRow[3] = dr[1];
                    newRow[4] = dr[2];
                    newRow[5] = dr[3];
                    table.Rows.Add(newRow);
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return Json(parentRow, JsonRequestBehavior.AllowGet);
            //return jsSerializer.Serialize(parentRow);
        }

        [HttpGet]
        public JsonResult GetTotalApplicantsWithVoucher127()
        {
            string query2 = "select av.applicantNo appNo, av.transactionIdBank transID, "
            + " FORMAT (av.dated, 'dd/MM/yyyy ') submitDate, av.amount amount from "
            + " tblApplicantVoucherBank av "
            + " where av.applicantId > 12700000 and av.applicantId < 12800000 order by av.dated";
            //query = query2;

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd2 = new SqlCommand(query2);
            DataTable table = new DataTable();
            //appNo,fullName,cnic,transID,submitDate,amount
            table.Columns.Add("appNo");
            table.Columns.Add("fullName");
            table.Columns.Add("cnic");
            table.Columns.Add("transID");
            table.Columns.Add("submitDate");
            table.Columns.Add("amount");
            DataTable table2 = new DataTable();
            var JSONString = new StringBuilder();
            var JSONString2 = new StringBuilder();
            JavaScriptSerializer jk = new JavaScriptSerializer();
            try
            {
                string Conn = ConfigurationManager.ConnectionStrings["DbPrpConn"].ConnectionString;
                con = new SqlConnection(Conn);
                con.Open();
                cmd2.Connection = con;
                SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
                sda2.Fill(table2);
                foreach (DataRow dr in table2.Rows)
                {
                    DataRow newRow = table.NewRow();
                    newRow[0] = dr[0];
                    string strurltest = String.Format("http://pnas2.phf.gop.pk/api/Public/GetApplicationPNAS/" + dr[0].ToString() + "");
                    WebRequest requestObjGet = WebRequest.Create(strurltest);
                    requestObjGet.Method = "GET";
                    requestObjGet.ContentType = "application/json";
                    WebResponse responseObjGet = null;
                    responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();
                    string strresulttest = null;
                    StreamReader reader = new StreamReader(responseObjGet.GetResponseStream());
                    string jsons = reader.ReadToEnd();
                    //{"$id":"1","applicantId":0,"applicationNo":0,"name":null,"pmdcNo":null,"cnicNo":null,"amount":0,"dueDate":"0001-01-01T00:00:00","message":"Applicant Not Exists","status":false}
                    string[] packets = jsons.Split(':');
                    string iddd = "";
                    iddd = packets[4].Split(',')[0].Trim();
                    iddd = iddd.Split(',')[0];
                    iddd = iddd.Split('"')[1];
                    iddd = iddd.Split('\\')[0];
                    newRow[1] = iddd;

                    iddd = packets[6].Split(',')[0].Trim();
                    iddd = iddd.Split(',')[0];
                    iddd = iddd.Split('"')[1];
                    iddd = iddd.Split('\\')[0];
                    newRow[2] = iddd;

                    newRow[3] = dr[1];
                    newRow[4] = dr[2];
                    newRow[5] = dr[3];
                    table.Rows.Add(newRow);
                }

            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                con.Close();
            }
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return Json(parentRow, JsonRequestBehavior.AllowGet);
            //return jsSerializer.Serialize(parentRow);
        }
        //appNo,fullName,cnic,transID,submitDate,amount
        [HttpGet]
        public JsonResult GetTotalApplicantsWithVoucher128()
        {
            string query = "select av.applicantNo appNo, a.name fullName, ai.cnicNo cnic, "
            +" av.transactionIdBank transID, "
            +" FORMAT (av.dated, 'dd/MM/yyyy ') submitDate, av.amount amount from"
            +" tblApplicantVoucherBank av"
            +" inner join tblApplicant a on av.applicantId = a.applicantId"
            + " inner join tblApplicantInfo ai on a.applicantId = ai.applicantId order by av.dated";

            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();

            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand(query);
            DataTable table = new DataTable();
            DataTable table2 = new DataTable();
            var JSONString = new StringBuilder();
            var JSONString2 = new StringBuilder();
            JavaScriptSerializer jk = new JavaScriptSerializer();
            try
            {
                string Conn = ConfigurationManager.ConnectionStrings["DbPrpConn"].ConnectionString;
                con = new SqlConnection(Conn);
                con.Open();
                cmd.Connection = con;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(table); 
                
            }
            catch (Exception ex)
            {

            }
            finally
            {
                con.Close();
            }
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return Json(parentRow, JsonRequestBehavior.AllowGet);
        }


        public ActionResult DsbJoiningHospital()
        {
            int instituteId = Request.QueryString["instituteId"].TooInt();

            ApplicantJoiningDsbModel model = new ApplicantJoiningDsbModel();

            if (instituteId > 0)
                model.listHospInst = new JoiningDAL().GetCountInstituteHospitalWise(instituteId);
            return View(model);
        }


    }
}