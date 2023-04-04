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
    public class EmailDAL : PrpDBConnect
    {
        #region Email Template

        public EmailTemplate  EmailTemplateByTypeId(int typeId, int inductionId =0)
        {
            EmailTemplate obj = new EmailTemplate();
            try
            {
                var dbt = db.spEmailTemplateByTypeId(typeId, inductionId).FirstOrDefault();
                obj = MapEmail.ToEntity(dbt);
            }
            catch (Exception ex)
            {
                obj = new EmailTemplate();
            }
            return obj;
        }

        public List<EmailTemplate> EmailTemplateSearch(EmailTemplate template)
        {
            List<EmailTemplate> list = new List<EmailTemplate>();
            try
            {
                var dbt = db.spEmailTemplateSearch(template.inductionId, template.typeId, template.search).ToList();
                list = MapEmail.ToEntityList(dbt);
            }
            catch (Exception ex)
            {
                list = new List<EmailTemplate>();
            }
            return list;
        }

        public Message EmailTemplateAddUpdate(EmailTemplate template)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmailTemplateAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@emailTemplateId", template.emailTemplateId);
            cmd.Parameters.AddWithValue("@name", template.name);
            cmd.Parameters.AddWithValue("@title", template.title);
            cmd.Parameters.AddWithValue("@subject", template.subject);
            cmd.Parameters.AddWithValue("@body", template.body);
            cmd.Parameters.AddWithValue("@isActive", template.isActive);
            cmd.Parameters.AddWithValue("@adminId", template.adminId);

            cmd.Parameters.AddWithValue("@typeId", template.typeId);
            cmd.Parameters.AddWithValue("@inductionId", template.inductionId);

            return PrpDbADO.ExecuteNonQuery(cmd);
        }
       


        public Message EmailStatusAddUpdate(StatusEmail obj)
        {
            Message msg = new Message();
            try
            {
                var objt = db.spEmailStatusAddUpdate(obj.emailStatusId, obj.applicantId, obj.typeId, obj.statusId, obj.statusMessage
                    , obj.body, obj.adminId, obj.sechduleDate).FirstOrDefault();

                msg = MapStatusEmail.ToEntity(objt);

            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.id = 0;
            }
            return msg;
        }

        #endregion

        #region Email Process

        public EmailProcess EmailProcessGetByApplicantAndType(int applicantId, int typeId)
        {
            EmailProcess obj = new EmailProcess();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spEmailProcessGetByApplicantAndType]"
                };

                cmd.Parameters.AddWithValue("@applicantId", applicantId);
                cmd.Parameters.AddWithValue("@typeId", typeId);
                DataTable dt = PrpDbADO.FillDataTable(cmd);

                foreach (DataRow dr in dt.Rows)
                {
                    obj.emailProcessId = dr["emailProcessId"].TooInt();
                    obj.applicantId = dr["applicantId"].TooInt();
                    obj.title = dr["title"].TooString();
                    obj.subject = dr["subject"].TooString();
                    obj.body = dr["body"].TooString();
                }
               
            }
            catch (Exception ex)
            {
                obj = new EmailProcess();
            }
            return obj;
        }


        public List<EmailProcess> EmailProcessGetByType( int typeId)
        {
            List<EmailProcess> list = new List<EmailProcess>();
            try
            {

                var dbt = db.spEmailProcessGetByType(typeId).ToList();
                list = MapEmail.ToEntityList(dbt);
            }
            catch (Exception ex)
            {
                list = new List<EmailProcess>();
            }
            return list;
        }

        public List<EmailProcess> EmailProcessGetAllRemaninig()
        {
            List<EmailProcess> list = new List<EmailProcess>();
            try
            {

                var dbt = db.spEmailProcessGetAllRemaninig().ToList();
                list = MapEmail.ToEntityList(dbt);
            }
            catch (Exception ex)
            {
                list = new List<EmailProcess>();
            }
            return list;
        }

        public Message EmailProcessAdd(EmailProcess obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmailProcessAdd]"
            };

            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@keyword", obj.keyword);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);

            return PrpDbADO.ExecuteNonQuery(cmd);
        }

        public Message EmailStatusAddUpdate(EmailProcess obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmailProcessUpdateStatus]"
            };
            cmd.Parameters.AddWithValue("@emailProcessId", obj.emailProcessId);
            cmd.Parameters.AddWithValue("@isProcess", obj.isProcess);
            cmd.Parameters.AddWithValue("@isSent", obj.isSent);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.ExecuteNonQuery(cmd);
        }

        public Message EmailStatusUpdateByProcessIds(string emailProcessIds)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmailProcessUpdateStatusByProcessIds]"
            };
            cmd.Parameters.AddWithValue("@emailProcessIds", emailProcessIds);
          
            return PrpDbADO.ExecuteNonQuery(cmd);
        }


        #endregion
    }
}
