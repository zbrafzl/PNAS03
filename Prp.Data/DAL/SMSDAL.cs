using Prp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Prp.Data
{
    public class SMSDAL : PrpDBConnect
    {
        public DataTable SearchSMSByApplicant(SmsEntity obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spSMSProcessSearch]"
            };
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            return PrpDbADO.FillDataTable(cmd);
        }

        public SMS GetById(int smsId)
        {
            SMS obj = new SMS();
            try
            {
                var objt = db.tblSMS.FirstOrDefault(x => x.smsId == smsId);
                obj = MapSMS.ToEntity(objt);

            }
            catch (Exception)
            {
            }
            return obj;
        }

        public List<SMS> GetAll()
        {
            List<SMS> list = new List<SMS>();
            try
            {
               var listt = db.tvwSMS.OrderBy(x => x.name).ToList();
                list = MapSMS.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<SMS> GetAll(int inductionId)
        {
            List<SMS> list = new List<SMS>();
            try
            {
                var listt = db.tvwSMS.Where(x=> x.inductionId == inductionId).OrderBy(x => x.name).ToList();
                list = MapSMS.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<SMS> GetAllByType(int inductionId, int typeId )
        {
            List<SMS> list = new List<SMS>();
            try
            {
                var listt = db.tvwSMS.Where(x => x.inductionId == inductionId && x.typeId == typeId).OrderBy(x => x.name).ToList();
                list = MapSMS.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }


        public SMS GetByTypeForApplicant(int applicantId,  int typeId)
        {
            SMS obj = new SMS();
            try
            {
                var objt = db.spSMSGetByTypeForApplicant(applicantId, typeId).FirstOrDefault(); ;
                obj = MapSMS.ToEntity(objt);

            }
            catch (Exception)
            {
            }
            return obj;
        }

        


        public Message AddUpdate(SMS obj)
        {
            Message msg = new Message();
            try
            {
                tblSM sms = new tblSM();

                if (obj.smsId == 0)
                {
                    sms = MapSMS.ToTable(obj);
                    db.tblSMS.Add(sms);
                }
                else
                {
                    sms = db.tblSMS.FirstOrDefault(x => x.smsId == obj.smsId);
                    sms.inductionId = obj.inductionId;
                    sms.phaseId = obj.phaseId;
                    sms.name = obj.name;
                    sms.detail = obj.detail;
                    sms.preDetail = obj.preDetail;
                    sms.postDetail = obj.postDetail;
                    sms.isActive = obj.isActive;
                    sms.typeId = obj.typeId;
                    sms.dated = obj.dated;
                    sms.adminId = obj.adminId;
                }
                db.SaveChanges();

                msg.id = sms.smsId;
            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.id = 0;
            }
            return msg;
        }


        #region SMS Process

        public Message AddUpdateSmsProcess(SmsProcess obj)
        {
            Message msg = new Message();
            try
            {
                db.spSMSProcessAddUpdate(obj.smsProcessId, obj.smsId, obj.applicantId, obj.isProcess, obj.isSent);
            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.id = 0;
            }

            return msg;
        }

        public List<SmsProcess> SMSProcessGetByType(int typeId, int isProcess)
        {
            List<SmsProcess> list = new List<SmsProcess>();
            try
            {
                var listt = db.spSMSProcessGetByType(typeId, isProcess).ToList();
                list = MapSMS.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<SmsProcess> SMSProcessGetBySmsId(int smsId, int isProcess=0)
        {
            List<SmsProcess> list = new List<SmsProcess>();
            try
            {
                var listt = db.spSMSProcessGetBySmsId(smsId, isProcess).ToList();
                list = MapSMS.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<SmsProcess> SMSProcessGetRemaning()
        {
            List<SmsProcess> list = new List<SmsProcess>();
            try
            {
                var listt = db.spSMSProcessGetRemaning().ToList();
                list = MapSMS.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public Message SMSProcessCreateListBySmsId( int typeId,  int smsId)
        {
            Message msg = new Message();

            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spSMSProcessCreateListBySmsId]"
            };
            cmd.Parameters.AddWithValue("@typeId", typeId);
            cmd.Parameters.AddWithValue("@smsId", smsId);
            return PrpDbADO.FillDataTableMessage(cmd);

        }

        #endregion

    }
}
