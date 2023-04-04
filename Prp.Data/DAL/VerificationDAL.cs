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
