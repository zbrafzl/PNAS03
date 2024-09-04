using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Prp.Data
{
    public class VerificationDAL : PrpDBConnect
    {

        public ApplicantApprovalStatus GetApplicationApprovalStatusGetByTypeAndId(int inductionId, int phaseId, int statusTypeId, int applicantId)
        {
            ApplicantApprovalStatus obj = new ApplicantApprovalStatus();
            try
            {
                var objt = db.spApplicationApprovalStatusGetByTypeAndId(inductionId, phaseId, statusTypeId, applicantId).FirstOrDefault();
                if (objt != null && objt.applicationApprovalStatusId > 0)
                    obj = MapVerification.ToEntity(objt);
            }
            catch (Exception)
            {
                obj = new ApplicantApprovalStatus();
            }
            return obj;
        }

        public List<ApplicantApprovalStatus> GetApplicationApprovalStatusGetById(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantApprovalStatus> list = new List<ApplicantApprovalStatus>();
            try
            {
                var listt = db.spApplicationApprovalStatusGetById(inductionId, phaseId, applicantId).ToList();
                list = MapVerification.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantApprovalStatus>();
            }
            return list;
        }


        public ApplicationVerificationStatus GetApplicationAmendmentsStatus(int applicantId)
        {
            ApplicationVerificationStatus obj = new ApplicationVerificationStatus();
            try
            {
                var objt = db.tvwApplicationAmendments.FirstOrDefault(x => x.applicantId == applicantId);
                obj = MapVerification.ToEntity(objt);
            }
            catch (Exception)
            {
                obj = new ApplicationVerificationStatus();
            }
            return obj;
        }

        public Message GetApplicantIdBySearchVerification(string search, string condition)
        {
            Message msg = new Message();
            try
            {

                //var item = db.spApplicantGetBySearchVerification(search, condition).FirstOrDefault();
                //msg = MapVerification.ToEntity(item);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message AddUpdateVerficationStatus(VerificationEntity obj)
        {
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantApprovalStatusVerifyAddUpdate]"
                };
                if (obj.approvalStatusId == 2)
                    obj.approvalStatusId = 0;
                cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
                cmd.Parameters.AddWithValue("@approvalStatusId", obj.approvalStatusId);
                cmd.Parameters.AddWithValue("@comments", obj.comments);
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

        public Message AddUpdateScruitnyVerficationStatus(VerificationEntity obj)
        {
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantScruitnyApprovalStatusVerifyAddUpdate]"
                };
                cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
                cmd.Parameters.AddWithValue("@adminID", obj.adminId);
                cmd.Parameters.AddWithValue("@approvalStatusId", obj.approvalStatusId);
                cmd.Parameters.AddWithValue("@comments", obj.comments);
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

        public Message AddUpdateAmmendmentsStatus(AmmendmentsEntitymodel obj)
        {
            var date = DateTime.UtcNow;
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spNursingAmendmentByAdmin2]"
                };
                DateTime dts = obj.dOB.TooDate();
                DateTime cnicExpirtyDate = obj.cnicExpiryDate.TooDate();
                cmd.Parameters.AddWithValue("@amendmentApprovalStatus", obj.amendmentApprovalStatus);
                cmd.Parameters.AddWithValue("@amendmentApprovalRemarks", obj.amendmentApprovalRemarks);
                cmd.Parameters.AddWithValue("@dob", dts);
                cmd.Parameters.AddWithValue("@name", obj.name);
                cmd.Parameters.AddWithValue("@imgCnicFront", Convert.ToString(obj.imgCnicFront.TooString()));
                cmd.Parameters.AddWithValue("@imgCnicBack", Convert.ToString(obj.imgCnicBack.TooString()));
                cmd.Parameters.AddWithValue("@imgDomicile", Convert.ToString(obj.imgDomicile.TooString()));
                cmd.Parameters.AddWithValue("@address", Convert.ToString(obj.address.TooString()));
                cmd.Parameters.AddWithValue("@fatherName", Convert.ToString(obj.fatherName.TooString()));
                cmd.Parameters.AddWithValue("@cnicExpiryDate", cnicExpirtyDate);
                cmd.Parameters.AddWithValue("@domicileDistrict", Convert.ToInt32(obj.domicileDistrictId.TooInt()));
                cmd.Parameters.AddWithValue("@matricObtainedMarks", Convert.ToInt32(obj.matricMarksObtain.TooInt()));
                cmd.Parameters.AddWithValue("@matricTotalMarks", Convert.ToInt32(obj.matricTotalMarks.TooInt()));
                cmd.Parameters.AddWithValue("@matricBoard", Convert.ToString(obj.matricBoard.TooString()));
                cmd.Parameters.AddWithValue("@imgMatricDegree", Convert.ToString(obj.imgMatricDegree.TooString()));
                cmd.Parameters.AddWithValue("@fscObtainedMarks", Convert.ToInt32(obj.fscObtainedMarks.TooInt()));
                cmd.Parameters.AddWithValue("@fscTotalMarks", Convert.ToInt32(obj.fscTotalMarks.TooInt()));
                cmd.Parameters.AddWithValue("@fscBoard", Convert.ToString(obj.fscBoard.TooString()));
                //cmd.Parameters.AddWithValue("@imgCnicFront", obj.imgCnicFront);
                //cmd.Parameters.AddWithValue("@imgCnicFront", obj.imgCnicBack);
                cmd.Parameters.AddWithValue("@imgFscDegree", Convert.ToString(obj.imgFscDegree.TooString()));
                cmd.Parameters.AddWithValue("@imgVoucher", Convert.ToString(obj.imgVoucher.TooString()));
                cmd.Parameters.AddWithValue("@applicantId", obj.applicantId.TooInt());
                cmd.Parameters.AddWithValue("@adminId", obj.adminId.TooInt());
                cmd.Parameters.AddWithValue("@dated", Convert.ToDateTime(date));
                cmd.Parameters.AddWithValue("@cnicNo", Convert.ToString(obj.cnicNo));
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
        public Message AddUpdateVerficationStatusJoining(VerificationEntity obj)
        {
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantApprovalStatusVerifyAddUpdateJoining]"
                };
                if (obj.approvalStatusId == 2)
                    obj.approvalStatusId = 0;
                cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
                cmd.Parameters.AddWithValue("@approvalStatusId", obj.approvalStatusId);
                cmd.Parameters.AddWithValue("@comments", obj.comments);
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


        public DataTable ApplicantListVerifyView(VerificationEntity obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantListVerify]"
            };
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@condition", obj.condition);

            cmd.Parameters.AddWithValue("@condition", obj.condition);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable ApplicantListVerifyExport(VerificationEntity obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantListVerifyExport]"
            };

            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            return PrpDbADO.FillDataTable(cmd);
        }


        public DataTable GetApplicationHasAmedmentAndNotSentEmail()
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGetApplicationHasAmedmentAndNotSentEmail]"
            };

            return PrpDbADO.FillDataTable(cmd);
        }
    }
}
