using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prp.Data
{
    public class HospitalDAL : PrpDBConnect
    {
        public Hospital GetById(int hospitalId)
        {
            Hospital obj = new Hospital();
            try
            {
                tblHospital objt = db.tblHospitals.FirstOrDefault(x => x.hospitalId == hospitalId);
                obj = MapHospital.ToEntity(objt);
            }
            catch (Exception)
            {
            }
            return obj;
        }

        public Hospital GetByUserId(int userId)
        {
            Hospital obj = new Hospital();
            try
            {
                var objt = db.tvwHospitalUsers.FirstOrDefault(x => x.userId == userId);
                obj = MapHospital.ToEntity(objt);
            }
            catch (Exception)
            {
            }
            return obj;
        }

        public List<Hospital> GetAll()
        {
            List<Hospital> list = new List<Hospital>();
            try
            {
                var listt = db.tvwHospitals.ToList();
                list = MapHospital.ToEntityList(listt);
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<Hospital> GetAll(int levelId , int typeId )
        {
            List<Hospital> list = new List<Hospital>();
            try
            {
                var listt = db.tvwHospitals.Where(x=> x.levelId == levelId && x.typeId== typeId).ToList();
                list = MapHospital.ToEntityList(listt);
            }
            catch (Exception ex)
            {
            }
            return list;
        }



        public List<EntityDDL> GetHospitalDDL(DDLHospitals obj)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DbPrpEntities db = new DbPrpEntities();
                DataTable dt = new DataTable();
                SqlConnection con = new SqlConnection();
                string query = "";
                if (obj.condition == "ByLevelAndType" && obj.instituteId == 25)
                    query += "Select hospitalId, name From tblHospital Where isActive = 1 And levelId = "+obj.instituteId+" And typeId = "+obj.typeId+" and hospitalId NOT IN  (38 )";
                if (obj.condition == "ByLevelAndType" && obj.instituteId != 25)
                    query += "Select hospitalId, name From tblHospital Where isActive = 1 And levelId = " + obj.instituteId + " And typeId = " + obj.typeId + " and hospitalId NOT IN  (38 )";
                else
                    query += "Select hospitalId, name From tblHospital Where isActive = 1 And levelId = " + obj.instituteId + " And typeId = " + obj.typeId + " ";
                try
                {
                    SqlCommand cmd = new SqlCommand(query);
                    con = new SqlConnection(PrpDbConnectADO.Conn);
                    con.Open();
                    cmd.Connection = con;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        EntityDDL itemList = new EntityDDL();
                        itemList.id = Convert.ToInt32(dr[0]);
                        itemList.name = (dr[1]).ToString();
                        itemList.key = (dr[1]).ToString();
                        itemList.typeId = Convert.ToInt32(dr[0]);
                        itemList.value = (dr[1]).ToString();
                        list.Add(itemList);
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    con.Close();
                }
                //if (obj.condition == "ByLevelAndType" && obj.instituteId == 25)
                //{
                //    var listt = db.tblHospitals.Where(h => h.levelId == obj.instituteId && h.typeId == obj.typeId && h.hospitalId != 38 ).Select(h=> new { h.hospitalId, h.name}).ToList();
                //    foreach (var item in listt)
                //    {
                //        EntityDDL e = new EntityDDL();
                //        e.id = item.hospitalId;
                //        e.name = item.name;
                //        e.key = item.name;
                //        e.typeId = obj.typeId;
                //        e.value = item.name;
                //        list.Add(e);
                //    }
                //}

                //else if (obj.condition == "ByLevelAndType" && obj.instituteId != 25)
                //{
                //    var listt = db.tblHospitals.Where(h => h.levelId == obj.instituteId && h.typeId == obj.typeId && h.hospitalId != 38).Select(h => new { h.hospitalId, h.name }).ToList();
                //    foreach (var item in listt)
                //    {
                //        EntityDDL e = new EntityDDL();
                //        e.id = item.hospitalId;
                //        e.name = item.name;
                //        e.key = item.name;
                //        e.typeId = obj.typeId;
                //        e.value = item.name;
                //        list.Add(e);
                //    }
                //}

                //var listt = db.spHospitalForDDL(obj.instituteId,obj.typeId,  obj.userId, obj.reffId, obj.reffIds, obj.condition).ToList();
                //list = MapHospital.ToEntityList(listt);
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<Hospital> GetHospitalForInstitute(int instituteId)
        {
            List<Hospital> list = new List<Hospital>();
            try
            {
                var listt = db.spHospitalForInstitute(instituteId).ToList();
                list = MapHospital.ToEntityList(listt);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public Message AddUpdate(Hospital obj)
        {
            Message msg = new Message();
            try
            {
                tblHospital hospital = new tblHospital();

                if (obj.hospitalId == 0)
                {
                    hospital = MapHospital.ToTable(obj);
                    db.tblHospitals.Add(hospital);
                }
                else
                {
                    hospital = db.tblHospitals.FirstOrDefault(x => x.hospitalId == obj.hospitalId);

                    hospital.name = obj.name;
                    hospital.code = obj.code;
                    hospital.abbr = obj.abbr;
                    hospital.levelId = obj.levelId;
                    hospital.typeId = obj.typeId;
                    hospital.regionId = obj.regionId;
                    hospital.nameDisplay = obj.nameDisplay;
                    hospital.isActive = obj.isActive;
                    hospital.address = obj.address;
                    hospital.dated = obj.dated;
                    hospital.adminId = obj.adminId;
                }
                db.SaveChanges();

                msg.id = hospital.hospitalId;
            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.id = 0;
            }
            return msg;
        }


        public DataTable HospitalSearch(HospitalSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spHospitalSearch]"
            };
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@levelId", obj.levelId);
            cmd.Parameters.AddWithValue("@regionId", obj.regionId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }

        #region Discipline

        public HospitalDiscipline HospitalDisciplineGetById(int id)
        {
            HospitalDiscipline obj = new HospitalDiscipline();
            try
            {
                tblHospitalDiscipline objt = db.tblHospitalDisciplines.FirstOrDefault(x => x.id == id);
                if (objt != null && objt.id > 0)
                    obj = MapHospital.ToEntity(objt);
                else obj = new HospitalDiscipline();

            }
            catch (Exception)
            {
                obj = new HospitalDiscipline();
            }
            return obj;
        }

        public Message HospitalDisciplineAddUpdate(HospitalDiscipline obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spHospitalDisciplineAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@id", obj.id);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@isApproved", obj.isApproved);
            cmd.Parameters.AddWithValue("@disciplineId", obj.disciplineId);
            cmd.Parameters.AddWithValue("@specialityId", obj.specialityId);
            cmd.Parameters.AddWithValue("@remarks", obj.remarks);
            cmd.Parameters.AddWithValue("@dateStart", obj.dateStart);
            cmd.Parameters.AddWithValue("@dateEnd", obj.dateEnd);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public Message HospitalDisciplineDelete(HospitalDiscipline obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spHospitalDisciplineDelete]"
            };
            cmd.Parameters.AddWithValue("@id", obj.id);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public List<HospitalDiscipline> HospitalDisciplineSearch(int hospitalId)
        {
            List<HospitalDiscipline> list = new List<HospitalDiscipline>();
            try
            {
                var listt = db.spHospitalDisciplineSearch(hospitalId).ToList();
                list = MapHospital.ToEntityList(listt);
            }
            catch (Exception ex)
            {
            }
            return list;
        }
        #endregion
    }
}
