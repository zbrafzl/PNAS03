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
    public class TraineeDAL : PrpDBConnect
    {
        public Message TraineeAddUpdate(TraineeInfo obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spTraineeInfoAddUpdate]"
            };

            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@specialityId", obj.specialityId);
            cmd.Parameters.AddWithValue("@marks", obj.marks);
            cmd.Parameters.AddWithValue("@joiningDate", obj.joiningDate);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);

            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public Message TraineeInfoStatusUpdate(TraineeInfo obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spTraineeInfoStatusUpdate]"
            };
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@hospitalIdTo", obj.hospitalIdTo);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);

            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public DataTable TraineeGetById(int applicantId)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spTraineeInfoGetById]"
            };
            cmd.Parameters.AddWithValue("@applicantId", applicantId);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable ApplicantForTraineeYear(TraineeSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantForTraineeYear]"
            };
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@employeeId", obj.employeeId);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }

        public Message EmployeeTraineeAddUpdate(EmployeeTrainee obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmployeeTraineeAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@employeeId", obj.employeeId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@isActive", obj.isActive);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);

            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public DataTable TraineeAttachedGetByEmployee(TraineeSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spTraineeAttachedGetByEmployee]"
            };
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@employeeId", obj.employeeId);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }
    }
}
