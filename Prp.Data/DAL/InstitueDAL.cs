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
    public class InstitueDAL : PrpDBConnect
    {
        public Institute GetById(int instituteId)
        {
            Institute obj = new Institute();
            try
            {
                tblInstitute objt = db.tblInstitutes.FirstOrDefault(x => x.instituteId == instituteId);
                obj = MapInstitue.ToEntity(objt);

            }
            catch (Exception)
            {
            }
            return obj;
        }

        public Institute GetByUserId(int userId)
        {
            Institute obj = new Institute();
            try
            {

                tblInstituteUser objIU = db.tblInstituteUsers.FirstOrDefault(x => x.userId == userId);

                tblInstitute objt = db.tblInstitutes.FirstOrDefault(x => x.instituteId == objIU.instituteId);
                obj = MapInstitue.ToEntity(objt);

            }
            catch (Exception)
            {
                obj = new Institute();
            }
            return obj;
        }

        public List<Institute> GetAll()
        {
            List<Institute> list = new List<Institute>();
            try
            {
                List<tvwInstitute> listt = db.tvwInstitutes.ToList();
                list = MapInstitue.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }
        public List<Institute> GetAll(int instituteTypeId)
        {
            List<Institute> list = new List<Institute>();
            try
            {
                string query = "";
                DataTable dt = new DataTable();
                query += "select instituteId, name from tblInstitute where instituteId > 216";
                SqlCommand cmd = new SqlCommand(query);
                SqlConnection con = new SqlConnection(PrpDbConnectADO.Conn);
                con.Open();
                cmd.Connection = con;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                try
                {
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        
                        foreach (DataRow dr in dt.Rows)
                        {
                            Institute inst = new Institute();
                            inst.instituteId = Convert.ToInt32(dr[0]).TooInt();
                            inst.name = dr[1].TooString();
                            list.Add(inst);
                        }
                    }
                }
                catch (Exception ex)
                { 
                
                }
            }
            catch (Exception)
            {
            }
            return list;
        }

        public List<Institute> GetAll(int instituteTypeId, int provinceId)
        {
            List<Institute> list = new List<Institute>();
            try
            {
                List<tvwInstitute> listt = db.tvwInstitutes.Where(x => x.instituteTypeId == instituteTypeId && x.provinceId == provinceId).ToList();
                list = MapInstitue.ToEntityList(listt);

            }
            catch (Exception)
            {
            }
            return list;
        }

        public List<EntityDDL> GetInstituteDDL(DDLInstitutes obj)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                var listt = db.spInstituteForDDL(obj.hospitalId, obj.regionId, obj.typeId, obj.userId, obj.reffIds, obj.condition).ToList();
                list = MapInstitue.ToEntityList(listt);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public Message AddUpdate(Institute obj)
        {
            Message msg = new Message();
            try
            {
                tblInstitute institute = new tblInstitute();

                if (obj.instituteId == 0)
                {
                    institute = MapInstitue.ToTable(obj);
                    db.tblInstitutes.Add(institute);
                }
                else
                {
                    institute = db.tblInstitutes.FirstOrDefault(x => x.instituteId == obj.instituteId);
                    institute.name = obj.name;
                    institute.code = obj.code;
                    institute.instituteTypeId = obj.instituteTypeId;
                    institute.provinceId = obj.provinceId;
                    institute.districtId = obj.districtId;
                    institute.isActive = obj.isActive;
                    institute.sortOrder = obj.sortOrder;
                    institute.dated = obj.dated;
                    institute.adminId = obj.adminId;
                }
                db.SaveChanges();

                msg.id = institute.instituteId;

                try
                {
                    db.spHospitalInstituteAddUpdate(institute.instituteId, obj.adminId, obj.hospitalIds);
                }
                catch (Exception)
                {
                }
            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.id = 0;
            }
            return msg;
        }
    }
}
