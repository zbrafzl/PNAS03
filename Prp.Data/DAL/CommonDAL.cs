using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Prp.Data
{
    public class CommonDAL : PrpDBConnect
    {
        public List<EntityDDL> SearchDDL(DDLSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spDDL]"
            };

            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@parentId", obj.parentId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@reffId", obj.reffId);
            cmd.Parameters.AddWithValue("@reffIds", obj.reffIds);
            cmd.Parameters.AddWithValue("@search", obj.search);
            cmd.Parameters.AddWithValue("@section", obj.section);
            return PrpDbADO.FillDataTableEntityDDL(cmd);

        }

        public List<EntityCount> GetDashboardCount(int inductionId, int phaseId)
        {
            List<EntityCount> list = new List<EntityCount>();
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand("spApplicantStatusGetAll");
                try
                {
                    con = new SqlConnection(PrpDbConnectADO.Conn);
                    con.Open();
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@inductionId", 13);
                    cmd.Parameters.AddWithValue("@phaseId", 1);
                    DataTable dt = new DataTable();
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        da.Fill(dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityCount li = new EntityCount();
                        li.statusTypeId = dr[0].TooInt();
                        li.statusType = dr[1].TooString();
                        li.statusId = dr[2].TooInt();
                        li.status = dr[3].TooString();
                        li.totalCount = dr[4].TooInt();
                        if (li.statusId == 4 || li.statusId == 6)
                        { 
                        
                        }
                        else
                        { 
                            list.Add(li);
                        }
                    }
                }
                catch (Exception ex)
                { 
                    
                }
                //var listt = db.spApplicantStatusGetAll(inductionId, phaseId).ToList();
                //list = MapCommon.ToEntityList(listt);
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<EntityCount> GetDashboardCountNursingVerification(int userId, int inductionId, int phaseId)
        {
            List<EntityCount> list = new List<EntityCount>();
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand("spApplicantStatusGetAllNursingVerification");
                try
                {
                    con = new SqlConnection(PrpDbConnectADO.Conn);
                    con.Open();
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@inductionId", 13);
                    cmd.Parameters.AddWithValue("@phaseId", 1);
                    DataTable dt = new DataTable();
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        da.Fill(dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityCount li = new EntityCount();
                        li.statusTypeId = dr[0].TooInt();
                        li.statusType = dr[1].TooString();
                        li.statusId = dr[2].TooInt();
                        li.status = dr[3].TooString();
                        li.totalCount = dr[4].TooInt();
                        if (li.statusId == 4 || li.statusId == 6)
                        {

                        }
                        else
                        {
                            list.Add(li);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
                //var listt = db.spApplicantStatusGetAll(inductionId, phaseId).ToList();
                //list = MapCommon.ToEntityList(listt);
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<EntityCount> GetDashboardCountInstituteHospital(int instituteId, int hospitalId, string condition)
        {
            List<EntityCount> list = new List<EntityCount>();
            try
            {
                var listt = db.spInstituteHospitalGetDashBoardData(instituteId, hospitalId, condition).ToList();
                list = MapCommon.ToEntityList(listt);
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<EntityCount> GetCountJobs(CountJobsEntity obj)
        {
            List<EntityCount> list = new List<EntityCount>();
            try
            {
                var listt = db.spJobGetCount(obj.typeId, obj.hospitalId, obj.specialityId, obj.condition).ToList();
                list = MapCommon.ToEntityList(listt);
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<EntityCount> GetCountApplicantApprovalStatus(CountApplicantStatusEntity obj)
        {
            List<EntityCount> list = new List<EntityCount>();
            try
            {
                //var listt = db.spApplicantApprovalStatusCount(obj.reffId, obj.condition).ToList();
                //list = MapCommon.ToEntityList(listt);
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        #region DisciplineDAL


        public List<Discipline> DisciplineGetAll()
        {
            List<Discipline> list = new List<Discipline>();
            try
            {
                var listt = db.tblDisciplines.ToList();
                list = MapDiscipline.ToEntityList(listt);

            }
            catch (Exception ex)
            {
                list = new List<Discipline>();
            }
            return list;
        }

        #endregion

        #region Department

        public Department DepartmentGetById(int departmentId)
        {
            Department obj = new Department();
            try
            {
                tblDepartment objt = db.tblDepartments.FirstOrDefault(x => x.departmentId == departmentId);
                obj = MapDepartment.ToEntity(objt);

            }
            catch (Exception)
            {
            }
            return obj;
        }

        public List<Department> DepartmentGetAll(int typeId)
        {
            List<Department> list = new List<Department>();
            try
            {
                List<tvwDepartment> listt = db.tvwDepartments.Where(x => x.typeId == typeId).ToList();
                list = MapDepartment.ToEntityList(listt);

            }
            catch (Exception)
            {
            }
            return list;
        }

        public Message DepartmentAddUpdate(Department obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spDepartmentAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@departmentId", obj.departmentId);
            cmd.Parameters.AddWithValue("@name", obj.name);
            cmd.Parameters.AddWithValue("@code", obj.code);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@isActive", obj.isActive);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public DataTable GetDepartmentForHospital(int hospitalId)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spDepartmentForHospital]"
            };
            cmd.Parameters.AddWithValue("@hospitalId", hospitalId);
            return PrpDbADO.FillDataTable(cmd);
        }

        public Message DepartmentHospitalAddUpdate(int hospitalId, string ids, int adminId)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spDepartmentHospitalAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@hospitalId", hospitalId);
            cmd.Parameters.AddWithValue("@ids", ids);
            cmd.Parameters.AddWithValue("@adminId", adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }



        #endregion

        #region Unit

        public Message UnitAddUpdate(Unit obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spUnitAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@unitId", obj.unitId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@departmentId", obj.departmentId);
            cmd.Parameters.AddWithValue("@name", obj.name);
            cmd.Parameters.AddWithValue("@code", obj.code);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@isActive", obj.isActive);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }
        #endregion

        #region Beds

        public Message BedAddUpdate(Bed obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spBedsAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@bedId", obj.bedId);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@departmentId", obj.departmentId);
            cmd.Parameters.AddWithValue("@unitId", obj.unitId);
            cmd.Parameters.AddWithValue("@disciplineId", obj.disciplineId);
            cmd.Parameters.AddWithValue("@specialityId", obj.specialityId);
            cmd.Parameters.AddWithValue("@bedsER", obj.bedsER);
            cmd.Parameters.AddWithValue("@bedsICU", obj.bedsICU);
            cmd.Parameters.AddWithValue("@bedsWards", obj.bedsWards);
            cmd.Parameters.AddWithValue("@bedsOther", obj.bedsOther);
            cmd.Parameters.AddWithValue("@remarksN", obj.remarksN);
            cmd.Parameters.AddWithValue("@imageN", obj.imageN);
            cmd.Parameters.AddWithValue("@bedsDep", obj.bedsDep);
            cmd.Parameters.AddWithValue("@remarksDep", obj.remarksDep);
            cmd.Parameters.AddWithValue("@imageDep", obj.imageDep);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public Message BedDelete(Bed obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spBedsDelete]"
            };
            cmd.Parameters.AddWithValue("@bedId", obj.bedId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public DataTable BedsSearch(Bed obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[BedsSearch]"
            };
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@departmentId", obj.departmentId);
            cmd.Parameters.AddWithValue("@unitId", obj.unitId);
            cmd.Parameters.AddWithValue("@disciplineId", obj.disciplineId);

            return PrpDbADO.FillDataTable(cmd);
        }

        #endregion


        public class AppFromHisdu
        {
            public string Id { get; set; }
            public int applicantId { get; set; }
            public int applicationNo { get; set; }
            public object name { get; set; }
            public object pmdcNo { get; set; }
            public object cnicNo { get; set; }
            public int amount { get; set; }
            public DateTime dueDate { get; set; }
            public string message { get; set; }
            public bool status { get; set; }
        }

        #region API DAL

        public ApplicantInfoAPI ApplicantGetInfo(string applicantNo, int transactionType)
        {
            ApplicantInfoAPI obj = new ApplicantInfoAPI();
            try
            {
                if(DateTime.Now < new DateTime(2022, 06, 25, 00, 00, 00) && (applicantNo.Substring(0,3) == "127" || applicantNo.Substring(0, 3) == "126" || applicantNo.Substring(0, 3) == "125"))
                {
                    try
                    {
                        string strurltest = String.Format("http://pnas2.phf.gop.pk/api/Public/GetApplicationPNAS/" + applicantNo + "");
                        WebRequest requestObjGet = WebRequest.Create(strurltest);
                        requestObjGet.Method = "GET";
                        WebResponse responseObjGet = null;
                        responseObjGet = (HttpWebResponse)requestObjGet.GetResponse();
                        string strresulttest = null;
                        StreamReader reader = new StreamReader(responseObjGet.GetResponseStream());
                        string jsons = reader.ReadToEnd();
                        //{"$id":"1","applicantId":0,"applicationNo":0,"name":null,"pmdcNo":null,"cnicNo":null,"amount":0,"dueDate":"0001-01-01T00:00:00","message":"Applicant Not Exists","status":false}
                        string[] packets = jsons.Split(':');
                        string iddd = packets[2].Split(',')[0].Trim();
                        int nnnn = 0;
                        string ssss = null;
                        bool success = int.TryParse(new string(iddd
                             .SkipWhile(x => !char.IsDigit(x))
                             .TakeWhile(x => char.IsDigit(x))
                             .ToArray()), out nnnn);
                        if (success)
                        {
                            obj.applicantId = Convert.ToInt32(nnnn);
                        }
                        
                        ssss = null;
                        nnnn = 0;
                        ssss = packets[3].Split(',')[0].Trim();
                        if (success)
                        {
                            obj.applicantNo = ssss;
                        }


                        ssss = null;
                        nnnn = 0;
                        iddd = packets[4].Split(',')[0].Trim();
                        iddd = iddd.Split(',')[0];
                        iddd = iddd.Split('"')[1];
                        iddd = iddd.Split('\\')[0];
                        if (success)
                        {
                            obj.name = iddd;
                        }

                        iddd = packets[5].Split(',')[0].Trim();
                        if (success)
                        {
                            try
                            {
                                iddd = iddd.Split(',')[0];
                                iddd = iddd.Split('"')[1];
                                iddd = iddd.Split('\\')[0];
                                obj.pmdcNo = iddd;
                            }
                            catch (Exception ex)
                            {
                                obj.pmdcNo = iddd;
                            }
                        }

                        iddd = packets[6].Split(',')[0].Trim();
                        iddd = iddd.Split(',')[0];
                        iddd = iddd.Split('"')[1];
                        iddd = iddd.Split('\\')[0];
                        if (success)
                        {
                            obj.cnicNo = iddd;
                        }

                        ssss = null;
                        nnnn = 0;
                        iddd = packets[7].Split(',')[0].Trim();
                        success = int.TryParse(new string(iddd
                             .SkipWhile(x => !char.IsDigit(x))
                             .TakeWhile(x => char.IsDigit(x))
                             .ToArray()), out nnnn);
                        if (success)
                        {
                            obj.amount = Convert.ToInt32(nnnn);
                        }

                        iddd = packets[8].Split(',')[0].Trim();
                        iddd += ":";
                        iddd += packets[9].Trim();
                        iddd += ":";
                        iddd += packets[10].Trim();
                        iddd = iddd.Split(',')[0];
                        iddd = iddd.Split('"')[1];
                        iddd = iddd.Split('\\')[0];
                        if (success)
                        {
                            try
                            {
                                obj.dueDate = DateTime.Parse(iddd.Split(',')[0]);
                            }
                            catch (Exception ex)
                            { 
                            
                            }
                        }

                        iddd = packets[11].Split(',')[0].Trim();
                        iddd = iddd.Split(',')[0];
                        iddd = iddd.Split('"')[1];
                        iddd = iddd.Split('\\')[0];
                        if (success)
                        {
                            obj.message = iddd;
                        }

                        iddd = packets[12].Split(',')[0].Trim();
                        if (success)
                        {
                            string nw = iddd.Split('}')[0];
                            //nw = nw.Split('"')[1];
                            obj.status = Convert.ToBoolean(nw);
                        }


                    }
                    catch (Exception ex)
                    {

                    }
                }
                else
                {
                    SqlCommand cmd = new SqlCommand
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandText = "[dbo].[spApplicantGetByApplicantNo]"
                    };
                    cmd.Parameters.AddWithValue("@applicantNo", applicantNo);
                    cmd.Parameters.AddWithValue("@transactionType", transactionType);
                    DataTable dt = PrpDbADO.FillDataTable(cmd);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        obj.applicantId = dr["applicantId"].TooInt();
                        obj.applicantNo = dr["applicantNo"].TooString();
                        obj.name = dr["name"].TooString();
                        obj.pmdcNo = dr["pmdcNo"].TooString();
                        obj.cnicNo = dr["cnicNo"].TooString();
                        obj.amount = dr["amount"].TooInt();
                        obj.message = dr["message"].TooString();
                        obj.status = dr["status"].TooBoolean();
                        obj.dueDate = Convert.ToDateTime(dr["dueDate"]);
                    }
                }
                
            }
            catch (Exception ex)
            {

                obj = new ApplicantInfoAPI();
            }

            return obj;
        }


        //public ApplicantInfoAPI APiApplicantInfo(string applicantNo)
        //{
        //    ApplicantInfoAPI obj = new ApplicantInfoAPI();
        //    try
        //    {
        //        var objt = db.spApplicantGetByApplicantNo(applicantNo).FirstOrDefault();
        //        if (objt != null && objt.applicantId > 0)
        //        {
        //            obj = MapCommon.ToEntity(objt);
        //        }
        //        else
        //        {
        //            obj = new ApplicantInfoAPI();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        obj = new ApplicantInfoAPI();
        //    }
        //    return obj;
        //}

        public ApplicantInfoAdmin ApplicantGetInfoByEmail(string applicantNo, string phoneNumber)
        {
            ApplicantInfoAdmin obj = new ApplicantInfoAdmin();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantGetByEmailAndPhone]"
                };
                cmd.Parameters.AddWithValue("@email", applicantNo);
                cmd.Parameters.AddWithValue("@phoneNo", phoneNumber);
                DataTable dt = PrpDbADO.FillDataTable(cmd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    obj.applicantId = dr["applicantId"].TooInt();
                    obj.name = dr["name"].TooString();
                    obj.contactNumber = dr["contactNumber"].TooString();
                    obj.email = dr["email"].TooString();
                    obj.password = dr["password"].TooString();
                    obj.message = dr["message"].TooString();
                }
            }
            catch (Exception)
            {

                obj = new ApplicantInfoAdmin();
            }

            return obj;
        }


        #endregion

    }
}
