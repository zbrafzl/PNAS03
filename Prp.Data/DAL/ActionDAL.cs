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
    public class ActionDAL : PrpDBConnect
    {
        public ApplicantAction GetById(int applicantId, int typeId)
        {
            ApplicantAction obj = new ApplicantAction();
            try
            {
                var objt = db.spApplicantActionGetByApplicantId(applicantId, typeId).FirstOrDefault();
                obj = MapResignation.ToEntity(objt);
            }
            catch (Exception)
            {
                obj = new ApplicantAction();
            }
            return obj;
        }

        public Message AddUpdate(ApplicantAction obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spActionAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@actionId", obj.actionId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@specialityJobId", obj.specialityJobId);
            cmd.Parameters.AddWithValue("@image", obj.image);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@categoryId", obj.categoryId);
            cmd.Parameters.AddWithValue("@startDate", obj.startDate);
            cmd.Parameters.AddWithValue("@endDate", obj.endDate);
            cmd.Parameters.AddWithValue("@isDocsCollected", obj.isDocsCollected);
            cmd.Parameters.AddWithValue("@remarks", obj.remarks);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public Message AddUpdateStatus(ApplicantAction obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spActionStatusAdd]"
            };
            cmd.Parameters.AddWithValue("@actionId", obj.actionId);
            cmd.Parameters.AddWithValue("@image", obj.image);
            cmd.Parameters.AddWithValue("@remarks", obj.remarks);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }
    }


}
