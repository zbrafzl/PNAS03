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
    public class SpecialityDAL : PrpDBConnect
    {
        public Speciality GetById(int subjectId)
        {
            Speciality obj = new Speciality();
            try
            {
                tblSpeciality objt = db.tblSpecialities.FirstOrDefault(x => x.specialityId == subjectId);
                obj = MapSpeciality.ToEntity(objt);

            }
            catch (Exception)
            {
            }
            return obj;
        }

        public List<Speciality> GetAll()
        {
            List<Speciality> list = new List<Speciality>();
            try
            {
                List<tblSpeciality> listt = db.tblSpecialities.OrderBy(x => x.name).ToList();
                list = MapSpeciality.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<Speciality> GetWithJobByInduction(int inductionId)
        {
            List<Speciality> list = new List<Speciality>();
            try
            {
                var listt = db.spSpecialityJobByInduction(inductionId).ToList();
                list = MapSpeciality.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<EntityDDL> GetSpecialityDDL(DDLSpecialitys obj)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                var listt = db.spSpecialityForDDL(obj.typeId, obj.userId, obj.reffId, obj.reffIds, obj.condition).ToList();
                list = MapSpeciality.ToEntityList(listt);
            }
            catch (Exception)
            {
            }
            return list;
        }
        public Message AddUpdate(Speciality obj)
        {
            Message msg = new Message();
            try
            {
                tblSpeciality subject = new tblSpeciality();

                if (obj.specialityId == 0)
                {
                    subject = MapSpeciality.ToTable(obj);
                    db.tblSpecialities.Add(subject);
                }
                else
                {
                    subject = db.tblSpecialities.FirstOrDefault(x => x.specialityId == obj.specialityId);
                    subject.name = obj.name;
                    subject.isActive = obj.isActive;
                    subject.sortOrder = obj.sortOrder;
                    subject.dated = obj.dated;
                    subject.adminId = obj.adminId;
                }
                db.SaveChanges();

                msg.id = subject.specialityId;
            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.id = 0;
            }
            return msg;
        }




        public SpecialityJob GetSpecialityJobById(int specialityJobId)
        {
            SpecialityJob obj = new SpecialityJob();
            try
            {
                tblSpecialityJob objt = db.tblSpecialityJobs.FirstOrDefault(x => x.specialityJobId == specialityJobId);
                obj = MapSpeciality.ToEntity(objt);

            }
            catch (Exception)
            {
                obj = new SpecialityJob();
            }
            return obj;
        }
        public List<SpecialityJob> GetSpecialityJobByParameters(int inductionId, int specialityId)
        {
            List<SpecialityJob> list = new List<SpecialityJob>();
            try
            {
                var listt = db.spSpecialityJobByParameters(inductionId, specialityId).ToList();
                list = MapSpeciality.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<SpecialityJob> GetSpecialityJobByTypeId(int inductionId, int typeId)
        {
            List<SpecialityJob> list = new List<SpecialityJob>();
            try
            {
                var listt = db.spSpecialityJobByTypeId(inductionId, typeId).ToList();
                list = MapSpeciality.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public Message AddUpdate(SpecialityJob obj)
        {
            Message msg = new Message();
            try
            {
                tblSpecialityJob specialityJob = new tblSpecialityJob();

                if (obj.specialityJobId == 0)
                {
                    specialityJob = MapSpecialityJob.ToTable(obj);
                    db.tblSpecialityJobs.Add(specialityJob);
                }
                else
                {
                    specialityJob = db.tblSpecialityJobs.FirstOrDefault(x => x.specialityJobId == obj.specialityJobId);
                    specialityJob.inductionId = specialityJob.inductionId;
                    specialityJob.specialityId = obj.specialityId;
                    specialityJob.instituteId = obj.instituteId;
                    specialityJob.hospitalId = obj.hospitalId;
                    specialityJob.typeId = obj.typeId;
                    specialityJob.quotaId = obj.quotaId;
                    specialityJob.jobs = obj.jobs;
                    specialityJob.isActive = obj.isActive;
                    specialityJob.dated = obj.dated;
                    specialityJob.adminId = obj.adminId;
                }
                db.SaveChanges();

                #region Synch Speciality Job
                try
                {
                    db.spSynchSpecialityJob(specialityJob.specialityJobId);
                }
                catch (Exception)
                {
                }
                #endregion


                msg.id = specialityJob.specialityJobId;
            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.id = 0;
            }
            return msg;
        }


        public DataTable SpecialityJobPrefferenceSearch(SpecialityJobSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spSpecialityJobPrefferenceSearch]"
            };
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@statusTypeId", obj.statusTypeId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@quotaId", obj.quotaId);
            cmd.Parameters.AddWithValue("@specialityId", obj.specialityId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }
    }
}
