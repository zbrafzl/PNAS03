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
    public class JoiningDAL : PrpDBConnect
    {


        #region DashBoard


        public List<ApplicantJoiningDsb> GetCountInstituteHospitalWise(int instituteId = 0)
        {
            List<ApplicantJoiningDsb> list = new List<ApplicantJoiningDsb>();
            try
            {
                var objt = db.spJoiningCountInstituteHospitalWise(instituteId).ToList();
                list = MapApplicantJoining.ToEntityList(objt);

            }
            catch (Exception)
            {
                list = new List<ApplicantJoiningDsb>();
            }
            return list;
        }

        public List<ApplicantJoiningDsb> GetCountInstituteWise()
        {
            List<ApplicantJoiningDsb> list = new List<ApplicantJoiningDsb>();
            try
            {
                var objt = db.spJoiningCountInstituteWiseNew().ToList();
                list = MapApplicantJoining.ToEntityList(objt);

            }
            catch (Exception)
            {
                list = new List<ApplicantJoiningDsb>();
            }
            return list;
        }



        public List<ApplicantJoiningDsb> GetCountHospitalWise(int instituteId)
        {
            List<ApplicantJoiningDsb> list = new List<ApplicantJoiningDsb>();
            try
            {
                var objt = db.spJoiningCountByInstituteHospitalWise(instituteId).ToList();
                list = MapApplicantJoining.ToEntityList(objt);

            }
            catch (Exception)
            {
                list = new List<ApplicantJoiningDsb>();
            }
            return list;
        }

        #endregion

        #region Final Applicant

        public ApplicantSelected GetFinalApplicantById(int inductionId, int applicantId)
        {
            ApplicantSelected obj = new ApplicantSelected();
            try
            {
                var objt = db.tvwApplicantSelecteds.FirstOrDefault(x => x.inductionId == inductionId && x.applicantId == applicantId);
                if (objt != null && objt.applicantId > 0)
                    obj = MapApplicantFinal.ToEntity(objt);
                else obj = new ApplicantSelected();
            }
            catch (Exception)
            {
                obj = new ApplicantSelected();
            }
            return obj;
        }

        #endregion


        public ApplicantJoined GetByApplicantById(int applicantId)
        {
            ApplicantJoined obj = new ApplicantJoined();
            try
            {
                var objt = db.tblApplicantJoineds.FirstOrDefault(x => x.applicantId == applicantId);
                if (objt != null && objt.applicantId > 0)
                    obj = MapApplicantJoining.ToEntity(objt);
                else obj = new ApplicantJoined();
            }
            catch (Exception ex)
            {
                obj = new ApplicantJoined();
            }
            return obj;
        }

        public ApplicantJoined GetJoinedApplicantDetailById(int applicantId)
        {
            ApplicantJoined obj = new ApplicantJoined();
            try
            {
                var objt = db.tvwApplicantJoinings.FirstOrDefault(x => x.applicantId == applicantId);
                if (objt != null && objt.applicantId > 0)
                    obj = MapApplicantJoining.ToEntity(objt);
                else obj = new ApplicantJoined();
            }
            catch (Exception)
            {
                obj = new ApplicantJoined();
            }
            return obj;
        }


        public ApplicantSelected GetByApplicantDetailById(int applicantId)
        {
            ApplicantSelected obj = new ApplicantSelected();
            try
            {
                var objt = db.tvwApplicantSelecteds.FirstOrDefault(x => x.applicantId == applicantId);
                obj = MapApplicantJoining.ToEntity(objt);
            }
            catch (Exception)
            {
                obj = new ApplicantSelected();
            }
            return obj;
        }

        public ApplicantJoined GetByApplicant(int userId, int applicantId)
        {
            ApplicantJoined obj = new ApplicantJoined();
            try
            {
                var objt = db.spJoiningGetByHospital(userId, 0).ToList();

                List<ApplicantJoined> list = new List<ApplicantJoined>();
                list = MapApplicantJoining.ToEntityList(objt);
                if (list != null && list.Count > 0)
                {
                    obj = list.FirstOrDefault(x => x.applicantId == applicantId);
                }
            }
            catch (Exception)
            {
                obj = new ApplicantJoined();
            }
            return obj;
        }

        public ApplicantJoined GetApplicantByInstiteUser( int userId, int applicantId)
        {
            ApplicantJoined obj = new ApplicantJoined();
            try
            {
                var objt = db.spJoiningGetByInstitute(0,userId, 0, "").ToList();

                List<ApplicantJoined> list = new List<ApplicantJoined>();
                list = MapApplicantJoining.ToEntityList(objt);
                if (list != null && list.Count > 0)
                {
                    obj = list.FirstOrDefault(x => x.applicantId == applicantId);
                }
            }
            catch (Exception ex)
            {
                obj = new ApplicantJoined();
            }
            return obj;
        }


        public DataTable JoiningSearchGetByInstitute(JoiningApplicantSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spJoiningSearchGetByInstitute]"
            };

            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@userId", obj.userId);
            cmd.Parameters.AddWithValue("@instituteId", obj.instituteId);
            cmd.Parameters.AddWithValue("@search", obj.search);

            return PrpDbADO.FillDataTable(cmd);
        }

        public List<ApplicantJoined> GetAllByInstiteUser(int userId, int instituteId, string search)
        {
            List<ApplicantJoined> list = new List<ApplicantJoined>();
            try
            {
                var objt = db.spJoiningGetByInstitute(0,userId, instituteId, search).ToList();
                list = MapApplicantJoining.ToEntityList(objt);
            }
            catch (Exception)
            {
                list = new List<ApplicantJoined>();
            }
            return list;
        }

        public List<ApplicantJoined> GetAllByHospitalUser(int userId, int hospitalId = 0)
        {
            List<ApplicantJoined> list = new List<ApplicantJoined>();
            try
            {
                var objt = db.spJoiningGetByHospital(userId, hospitalId).ToList();
                list = MapApplicantJoining.ToEntityList(objt);

            }
            catch (Exception)
            {
                list = new List<ApplicantJoined>();
            }
            return list;
        }


        public List<ApplicantJoined> GetJoiningAll(int top, int pageNo, int joinStatus, string search)
        {
            List<ApplicantJoined> list = new List<ApplicantJoined>();
            try
            {
                var objt = db.spJoiningGetAll(top, pageNo, joinStatus, search).ToList();
                list = MapApplicantJoining.ToEntityList(objt);

            }
            catch (Exception)
            {
                list = new List<ApplicantJoined>();
            }
            return list;
        }


        public Message AddUpdate(ApplicantJoined obj)
        {
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spJoiningAddUpdate]"
                };
                cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
                cmd.Parameters.AddWithValue("@joiningDate", obj.joiningDate);
                cmd.Parameters.AddWithValue("@image", obj.image);
                cmd.Parameters.AddWithValue("@adminId", obj.jobs);
                DataTable dt = PrpDbADO.FillDataTable(cmd);

                msg = dt.ConvertToEnitityMessage();
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message AddUpdateMigration(ApplicantJoined obj)
        {
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spMigrationJoiningAddUpdate]"
                };
                cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
                cmd.Parameters.AddWithValue("@joiningDate", obj.joiningDate);
                cmd.Parameters.AddWithValue("@image", obj.image);
                cmd.Parameters.AddWithValue("@adminId", obj.jobs);
                DataTable dt = PrpDbADO.FillDataTable(cmd);

                msg = dt.ConvertToEnitityMessage();
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message AddUpdateReleaving(ApplicantJoined obj)
        {
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spReleavingAddUpdate]"
                };
                cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
                cmd.Parameters.AddWithValue("@joiningDate", obj.joiningDate);
                cmd.Parameters.AddWithValue("@image", obj.image);
                cmd.Parameters.AddWithValue("@adminId", obj.jobs);
                DataTable dt = PrpDbADO.FillDataTable(cmd);

                msg = dt.ConvertToEnitityMessage();
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public DataTable JoiningSearch(JoiningApplicantSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spJoiningSearch]"
            };

            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@search", obj.search);

            return PrpDbADO.FillDataTable(cmd);
        }


        public List<SpecialityJob> GetHardshipSeatsStatusByApplicant(int applicantId)
        {
            List<SpecialityJob> list = new List<SpecialityJob>();
            try
            {
                var objt = db.spHardshipSeatsStatusByApplicant(applicantId).ToList();
                list = MapApplicantJoining.ToEntityList(objt);
            }
            catch (Exception)
            {
                list = new List<SpecialityJob>();
            }
            return list;
        }

    }
}
