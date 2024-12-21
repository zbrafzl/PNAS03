using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using prp.fn;
using Prp.Data;
using Prp.Model;

namespace Prp.Sln.Controllers
{
    public class CommonController : Controller
    {

        [HttpGet]
        public JsonResult GetCurrentDateTime()
        {
            DateTime date = DateTime.UtcNow;
            //https://stackoverflow.com/questions/46120116/json-date-and-datetime-serialisation-in-c-sharp-newtonsoft
            string sdsd = JsonConvert.SerializeObject(date);
            return Json(date, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetSmsInfoByCellNo(string contactNumber)
        {
            DataTable dataTable = new ApplicantDAL().OtpGetByMobileNo(contactNumber,"");
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }

        [HttpPost]
        public ActionResult SMSProcessGetInfo(SmsProcess obj)
        {
            DataTable dataTable = new SMSDAL().SMSProcessGetInfo(obj);
            string json = JsonConvert.SerializeObject(dataTable, Formatting.Indented);
            return Content(json, "application/json");
        }


        public FileResult GetCaptchaImage()
        {
            Message msg = CaptchaGenerate();
            string text = msg.msg;

            //first, create a dummy bitmap just to get a graphics object
            System.Drawing.Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            Font font = new Font("Arial", 15);
            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width + 40, (int)textSize.Height + 20);
            drawing = Graphics.FromImage(img);

            Color backColor = Color.SeaShell;
            Color textColor = Color.Red;
            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 20, 10);

            drawing.Save();

            font.Dispose();
            textBrush.Dispose();
            drawing.Dispose();

            MemoryStream ms = new MemoryStream();
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            img.Dispose();

            return File(ms.ToArray(), "image/png");
        }


        public JsonResult GenerateCaptcha()
        {
            Message msg = CaptchaGenerate();
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        public Message CaptchaGenerate()
        {
            Message msg = new Message();

            try
            {
                msg.msg = ProjFunctions.GetCaptchaText();
                Session["CAPTCHA"] = msg.msg;

            }
            catch (Exception ex)
            {
                msg.id = 0;
                msg.status = false;
                msg.msg = ex.Message;
            }

            return msg;
        }

        public JsonResult CompareCaptcha(string captcha)
        {
            Message msg = new Message();
            try
            {

                string serverCaptcha = Session["CAPTCHA"].ToString();

                if (!captcha.Equals(serverCaptcha))
                {
                    msg.status = false;
                }
                else
                {
                    msg.status = true;
                }
            }
            catch (Exception ex)
            {
                msg.status = false;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }



        #region Commons

        [HttpGet]
        public JsonResult RegionGetByCondition(int typeId, int parentId, string condition)
        {
            List<Prp.Data.Region > list = new RegionDAL().RegionGetByCondition(typeId, parentId, condition);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult InstitueGetByType(int typeId)
        {
            List<Institute> list = new InstitueDAL().GetAll(typeId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ConstantGetByType(int typeId)
        {
            List<Constant> list = new ConstantDAL().GetAll(typeId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ConstantGetForDDL(DDLConstants obj)
        {
            List<EntityDDL> list = new ConstantDAL().GetConstantDDL(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SpecialityGetForDDL(DDLSpecialitys obj)
        {
            List<EntityDDL> list = new SpecialityDAL().GetSpecialityDDL(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SpecialityGetForDDLSpeciality(DDLSpecialitys obj)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            string checkQuery = "";
            DataTable dt = new DataTable();
            checkQuery = "Select distinct sj.specialityId, s.name From tblSpecialityJob sj inner join tblSpeciality s on sj.specialityId = s.specialityId where sj.isActive = 1 And inductionId =12 and jobs > 0";
            SqlCommand cmd = new SqlCommand(checkQuery);
            SqlConnection con = new SqlConnection(PrpDbConnectADO.Conn);
            con.Open();
            cmd.Connection = con;
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    EntityDDL c = new EntityDDL();
                    c.id = dr[0].TooInt();
                    c.name = dr[0].TooString();
                    c.key = dr[1].TooString();
                    c.value = dr[1].TooString();
                    c.typeId = 6;
                    list.Add(c);
                    //c.typeName = dr[14].TooString();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult JournalGetForDDL(DDLJournal obj)
        {
            List<EntityDDL> list = new MasterSetupDAL().GetJournalForDDL(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        //

        [HttpPost]
        public JsonResult DDLGetInstitute(DDLSearch obj)
        {
            List<EntityDDL> list = DDLInstitute.GetAll(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DDLGetInstituteBSN(DDLSearch obj)
        {
            Message msg = new Message();
            List<EntityDDL> list = new List<EntityDDL>();
            DbPrpEntities db = new DbPrpEntities();
            DataTable dt = new DataTable();
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand("select instituteId, name, instituteTypeId from tblInstitute order by provinceId");
                try
                {
                    con = new SqlConnection(PrpDbConnectADO.Conn);
                    con.Open();
                    cmd.Connection = con;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    msg.status = true;
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityDDL itemList = new EntityDDL();
                        itemList.id = Convert.ToInt32(dr[0]);
                        itemList.name = (dr[1]).ToString();
                        itemList.key = (dr[1]).ToString();
                        itemList.typeId = Convert.ToInt32(dr[2]);
                        itemList.value = Convert.ToInt32(dr[0]).ToString();
                        list.Add(itemList);
                    }
                }
                catch (Exception ex)
                {
                    msg.status = false;
                    msg.msg = ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            { 
            
            }           
            //= DDLInstitute.GetAll(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InstituteGetForDDL(DDLInstitutes obj)
        {
            List<EntityDDL> list =  new InstitueDAL().GetInstituteDDL(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DDLGetRegion(DDLSearch obj)
        {
            List<EntityDDL> list = DDLRegion.GetAll(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DDLSpecialityGet(DDLSearch obj)
        {
            List<EntityDDL> list = DDLSpeciality.GetAll(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult HospitalGetForDDL(DDLHospitals obj)
        {

            obj.typeId = obj.typeId.TooInt();
            obj.typeId = obj.typeId.TooInt();
            obj.userId = obj.userId.TooInt();
            obj.reffIds = obj.reffIds.TooString();

            List<EntityDDL> list = new HospitalDAL().GetHospitalDDL(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult HospitalGetForDDLspeciality(DDLHospitals obj)
        {

            obj.typeId = obj.typeId.TooInt();
            obj.typeId = obj.typeId.TooInt();
            obj.userId = obj.userId.TooInt();
            obj.reffIds = obj.reffIds.TooString();

            List<EntityDDL> list = new List<EntityDDL>();
            string checkQuery = "";
            DataTable dt = new DataTable();
            checkQuery = "select h.userId instituteId, h.firstName name from tblUser h where typeId = 69 and userId < 65";
            
            SqlCommand cmd = new SqlCommand(checkQuery);
            SqlConnection con = new SqlConnection(PrpDbConnectADO.Conn);
            con.Open();
            cmd.Connection = con;
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    EntityDDL c = new EntityDDL();
                    c.id = dr[0].TooInt();
                    c.name = dr[1].TooString();
                    c.value = dr[1].TooString();
                    list.Add(c);
                    //c.typeName = dr[14].TooString();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult HospitalGetForDDLspecialityEvening(DDLHospitals obj)
        {

            obj.typeId = obj.typeId.TooInt();
            obj.typeId = obj.typeId.TooInt();
            obj.userId = obj.userId.TooInt();
            obj.reffIds = obj.reffIds.TooString();

            List<EntityDDL> list = new List<EntityDDL>();
            string checkQuery = "";
            DataTable dt = new DataTable();
            checkQuery = "select u.userId instituteId, u.firstName name from tblSpecialityJob t1 inner join tblUser u on t1.hospitalId = u.userId where t1.inductionId = 16 and t1.typeId = 10 and t1.quotaId = 1 and t1.jobs > 0";

            SqlCommand cmd = new SqlCommand(checkQuery);
            SqlConnection con = new SqlConnection(PrpDbConnectADO.Conn);
            con.Open();
            cmd.Connection = con;
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    EntityDDL c = new EntityDDL();
                    c.id = dr[0].TooInt();
                    c.name = dr[1].TooString();
                    c.value = dr[1].TooString();
                    list.Add(c);
                    //c.typeName = dr[14].TooString();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }

            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DDLGetDepartment(DDLSearch obj)
        {
            List<EntityDDL> list = DDLDepartment.GetAll(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DDLGetUnit(DDLSearch obj)
        {
            List<EntityDDL> list = DDLUnit.GetAll(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DDLGetDiscipline(DDLSearch obj)
        {
            List<EntityDDL> list = DDLDiscipline.GetAll(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DDLGetSpeciality(DDLSearch obj)
        {
            List<EntityDDL> list = DDLSpeciality.GetAll(obj);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region file

        [HttpPost]
        public ActionResult UploadApplicantFile(HttpPostedFileBase file, string applicantId)
        {
            if (file != null && file.ContentLength > 0 && !string.IsNullOrEmpty(applicantId))
            {
                // Define the path where the file will be saved
                var folderPath = Server.MapPath($"~/Images/Applicant/{applicantId}/");

                // Ensure the directory exists
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Create the full file path
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                // Save the file
                file.SaveAs(filePath);

                // Return the saved file name
                return Json(new { success = true, fileName = fileName }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { success = false, message = "Invalid file or applicant ID." }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Image

        [HttpPost]
        public ActionResult UploadImage()
        {
            Message msg = new Message();
            string imageName = "";
            string folderPath = "/Images/Applicant/";

            try
            {

                string imageType = Request.Form["imageType"].TooString();
                int applicantId = Request.Form["applicantId"].TooInt();
                int inductionId = ProjConstant.inductionId;

                folderPath = folderPath + "/" + applicantId;

                CreateDirectory(folderPath);

                // Checking no of files injected in Request object  
                if (Request.Files.Count > 0)
                {
                    try
                    {
                        //  Get all files from Request object  
                        HttpFileCollectionBase files = Request.Files;
                        for (int i = 0; i < files.Count; i++)
                        {
                            //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                            //string filename = Path.GetFileName(Request.Files[i].FileName);  

                            HttpPostedFileBase file = files[i];
                            string fname;

                            // Checking for Internet Explorer  
                            if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                            {
                                string[] testfiles = file.FileName.Split(new char[] { '\\' });
                                fname = testfiles[testfiles.Length - 1];
                            }
                            else
                            {
                                fname = file.FileName;
                            }

                            imageName = imageType + "_" + inductionId + Path.GetExtension(fname);


                            //imageName = fname;

                            //int number = 1;
                            //imageName = MakeUniqueFileName(folderPath, imageName, number);
                            //if (String.IsNullOrWhiteSpace(imageName))
                            //    imageName = fname;

                            string imagePath = Path.Combine(Server.MapPath(folderPath), imageName);
                            if (System.IO.File.Exists(imagePath))
                            {
                                imageName = imageType + "_" + inductionId + "_1" + Path.GetExtension(fname);
                                imagePath = Path.Combine(Server.MapPath(folderPath), imageName);

                                if (System.IO.File.Exists(imagePath))
                                {
                                    imageName = imageType + "_" + inductionId + "_2" + Path.GetExtension(fname);
                                    imagePath = Path.Combine(Server.MapPath(folderPath), imageName);

                                    if (System.IO.File.Exists(imagePath))
                                    {
                                        imageName = imageType + "_" + inductionId + "_3" + Path.GetExtension(fname);
                                        imagePath = Path.Combine(Server.MapPath(folderPath), imageName);

                                        if (System.IO.File.Exists(imagePath))
                                        {
                                            imageName = imageType + "_" + inductionId + "_4" + Path.GetExtension(fname);
                                            imagePath = Path.Combine(Server.MapPath(folderPath), imageName);

                                            if (System.IO.File.Exists(imagePath))
                                            {
                                                imageName = imageType + "_" + inductionId + "_5" + Path.GetExtension(fname);
                                                imagePath = Path.Combine(Server.MapPath(folderPath), imageName);
                                            }
                                        }
                                    }
                                }
                            }

                            // Get the complete folder path and store the file inside it.  
                            //imagePath = Path.Combine(Server.MapPath(folderPath), imageName);

                            file.SaveAs(imagePath);
                        }

                        msg.id = 1;
                        msg.msg = imageName;
                    }
                    catch (Exception ex)
                    {
                        msg.id = 0;
                        msg.msg = ex.Message;
                    }
                }
                else
                {
                    msg.id = 0;
                    msg.msg = "No image selected";
                }
            }
            catch (Exception ex)
            {

                msg.id = 0;
                msg.msg = ex.Message;
            }
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

        private string MakeUniqueFileName(string folderPath, string imageName, int number)
        {
            string imageFileName = imageName;
            try
            {
                string imagePath = Path.Combine(Server.MapPath(folderPath), imageName);
                if (System.IO.File.Exists(imagePath))
                {
                    imageName = Path.GetFileNameWithoutExtension(imageName) + number + Path.GetExtension(imageName);
                    imageFileName = MakeUniqueFileName(folderPath, imageName, number);
                }
            }
            catch (Exception)
            {
            }
            return imageFileName;
        }

        public void CreateDirectory(string subPath)
        {
            bool exists = System.IO.Directory.Exists(Server.MapPath(subPath));

            if (!exists)
                System.IO.Directory.CreateDirectory(Server.MapPath(subPath));

        }


        #endregion
    }
}