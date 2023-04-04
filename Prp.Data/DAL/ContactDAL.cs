using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prp.Data
{
    public class ContactDAL : PrpDBConnect
    {

        #region Dashboard

        public DataTable GetContactStatus(int inductionId, int typeId)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spContactStatus]"
            };
            cmd.Parameters.AddWithValue("@inductionId", inductionId);
            cmd.Parameters.AddWithValue("@typeId", typeId);
            return PrpDbADO.FillDataTable(cmd);
        }

        #endregion

        #region Questions


        public Contact GetById(int contactId)
        {
            Contact obj = new Contact();
            try
            {
                var objt = db.tvwContacts.FirstOrDefault(x => x.contactId == contactId);
                obj = MapContact.ToEntity(objt);
            }
            catch (Exception)
            {
                obj = new Contact();
            }

            return obj;
        }

        public Contact GetByApplicantId(int inductionId, int contactId, int applicantId)
        {
            Contact obj = new Contact();
            try
            {
                var objt = db.tvwContacts.Where(x => x.inductionId == inductionId
                    && x.contactId == contactId && x.applicantId == applicantId).FirstOrDefault();

                if (objt != null && objt.contactId > 0)
                    obj = MapContact.ToEntity(objt);
            }
            catch (Exception)
            {
                obj = new Contact();
            }

            return obj;
        }

        public Contact GetById(int contactId, int adminId)
        {
            Contact obj = new Contact();
            try
            {
                var objt = db.spContactQuestionGetById(contactId, adminId).FirstOrDefault();
                if (objt != null && objt.contactId > 0)
                    obj = MapContact.ToEntity(objt);
            }
            catch (Exception)
            {
                obj = new Contact();
            }

            return obj;
        }

        public List<Contact> GetByTypeId(int typeId, int inductionId)
        {
            List<Contact> list = new List<Contact>();
            try
            {
                var listt = db.tvwContacts.Where(x => x.typeId == typeId && x.inductionId == inductionId).ToList();
                list = MapContact.ToEntityList(listt);

            }
            catch (Exception)
            {
                list = new List<Contact>();
            }
            return list;
        }

        public List<Contact> GetQuestionByApplicant(int typeId, int inductionId, int applicantId)
        {
            List<Contact> list = new List<Contact>();
            try
            {
                var listt = db.tvwContacts.Where(x => x.typeId == typeId && x.inductionId == inductionId && x.applicantId == applicantId).ToList();
                list = MapContact.ToEntityList(listt);

            }
            catch (Exception)
            {
                list = new List<Contact>();
            }
            return list;
        }

        public List<ContactDoc> GetAllDocsByTypeAndContact(int typeId, int contactId)
        {
            List<ContactDoc> list = new List<ContactDoc>();
            try
            {
                var listt = db.tblContactDocs.Where(x => x.typeId == typeId && x.contactId == contactId).ToList();
                list = MapContact.ToEntityList(listt);

            }
            catch (Exception)
            {
                list = new List<ContactDoc>();
            }
            return list;
        }



        public DataTable ContactQuestionSearch(Contact obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spContactQuestionSearch]"
            };
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }

        public int ContactQuestionGetNoReplied(int adminId)
        {
            int totalCount = 0;
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spContactQuestionGetNoReplied]"
                };
                cmd.Parameters.AddWithValue("@adminId", adminId);
                DataTable dt = PrpDbADO.FillDataTable(cmd);
                if (dt != null)
                    totalCount = dt.Rows.Count;

            }
            catch (Exception)
            {
                totalCount = 0;
            }
            return totalCount;
        }

        public Message AddUpdate(Contact obj)
        {
            Message msg = new Message();
            try
            {
                var objt = db.spContactQuestion(obj.typeId, obj.name, obj.applicantId, obj.pmdcNo, obj.emailId, obj.title, obj.question).FirstOrDefault();
                msg = MapContact.ToEntity(objt);
            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.id = 0;
            }
            return msg;
        }

        public Message AddUpdateDocs(int contactId, string images, int typeId)
        {
            Message msg = new Message();
            try
            {
                var objt = db.spContactDocsAdd(typeId, contactId, images).FirstOrDefault();
                msg = MapContact.ToEntity(objt);

            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.id = 0;
            }
            return msg;
        }

        #endregion


        #region Answer

        public List<ContactReply> GetAnswerListByApplicant( int contactId, int applicantId)
        {
            List<ContactReply> list = new List<ContactReply>();
            try
            {
                var listt = db.tblContactReplies.Where(x => x.contactId == contactId).ToList();
                list = MapContact.ToEntityList(listt);

            }
            catch (Exception)
            {
                list = new List<ContactReply>();
            }
            return list;
        }

        public Message ContactReplyAnswer(ContactReply obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spContactAnswer]"
            };
            cmd.Parameters.AddWithValue("@contactId", obj.contactId);
            cmd.Parameters.AddWithValue("@answer", obj.answer);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public List<ContactReply> GetAnswerListByContactId(int contactId, int adminId)
        {
            List<ContactReply> list = new List<ContactReply>();
            try
            {
                var listt = db.spContactAnswerListGetById(contactId, adminId).ToList();
                list = MapContact.ToEntityList(listt);

            }
            catch (Exception)
            {
                list = new List<ContactReply>();
            }
            return list;
        }

        #endregion


        #region Attendence


        public Message AttendenceAddUpdate(ContactAttendence obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spContactAttendence]"
            };
            cmd.Parameters.AddWithValue("@contactIdAttendenceId", obj.contactIdAttendenceId);
            cmd.Parameters.AddWithValue("@contactId", obj.contactId);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@isSelf", obj.isSelf);
            cmd.Parameters.AddWithValue("@relationId", obj.relationId);
            cmd.Parameters.AddWithValue("@attendenceNo", obj.attendenceNo);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);

            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public DataTable ContactAttendenceSearch(ContactSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spContactAttendenceSearch]"
            };
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable ContactAttendenceSearchCurrent(ContactSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spContactAttendenceCurrentForAttendnce]"
            };
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable ContactAttendencePrint(ContactSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spContactAttendencePrint]"
            };
            cmd.Parameters.AddWithValue("@start", obj.start);
            cmd.Parameters.AddWithValue("@end", obj.end);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            return PrpDbADO.FillDataTable(cmd);
        }

        #endregion

        #region Contact Status Comments

        public DataTable ContactAttendenceStatusSearchCurrent(ContactSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spContactAttendenceCurrentForStatus]"
            };
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }

        public ContactStatus GetContactStatusById(int contactId)
        {
            ContactStatus obj = new ContactStatus();
            try
            {
                var objt = db.tblContactStatus.Where(x => x.contactId == contactId).OrderByDescending(x=> x.id).FirstOrDefault();
                obj = MapContact.ToEntity(objt);
            }
            catch (Exception)
            {
                obj = new ContactStatus();
            }
            return obj;
        }

        public Message AddUpdateStatus(ContactStatus obj)
        {

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spContactStatusAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@id", obj.id);
            cmd.Parameters.AddWithValue("@contactId", obj.contactId);
            cmd.Parameters.AddWithValue("@comments", obj.comments);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);

           
        }

        public DataTable ContactAttendenceStatusPrint(ContactSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spContactAttendenceStatusPrint]"
            };
            cmd.Parameters.AddWithValue("@start", obj.start);
            cmd.Parameters.AddWithValue("@end", obj.end);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@dayNo", obj.dayNo);
            return PrpDbADO.FillDataTable(cmd);
        }

        #endregion


    }
}
