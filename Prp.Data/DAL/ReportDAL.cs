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
    public class ReportDAL : PrpDBConnect
    {
        public DataTable RptSeatsStatus(SpecialityJobSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spRptSeatsStatus]"
            };
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@search", obj.search);
            cmd.Parameters.AddWithValue("@isExport", obj.isExport);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable JoinedApplicantSearch(SearchReport obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spJoinedApplicantSearch]"
            };
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@userId", obj.userId);
            cmd.Parameters.AddWithValue("@instituteId", obj.instituteId);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@search", obj.search);

            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable ActionSearch(SearchReport obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spActionSearch]"
            };
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@instituteId", obj.instituteId);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable JoiningApplicantByHospitalSearch(SpecialityJobSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spJoiningApplicantByHospitalSearch]"
            };
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@userId", obj.userId);
            cmd.Parameters.AddWithValue("@attachStatusId", obj.attachStatusId);
            cmd.Parameters.AddWithValue("@specialityId", obj.specialityId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@search", obj.search);

            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable JoinedApplicantByHospitalSearch(SpecialityJobSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spJoinedApplicantByHospitalSearch]"
            };
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@userId", obj.userId);
            cmd.Parameters.AddWithValue("@attachStatusId", obj.attachStatusId);
            cmd.Parameters.AddWithValue("@specialityId", obj.specialityId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@search", obj.search);

            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable HospitalTraineeCountReport(SpecialityJobSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spHospitalTraineeCountReport]"
            };
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@statusId", obj.attachStatusId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@search", obj.search);

            return PrpDbADO.FillDataTable(cmd);
        }
    }
}
