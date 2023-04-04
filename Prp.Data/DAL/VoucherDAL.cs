using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prp.Data.DAL
{
    public class VoucherDAL : PrpDBConnect
    {
        public List<VoucherInfo> GetVoucherList(int countryId = 1)
        {
            List<VoucherInfo> list = new List<VoucherInfo>();
            try
            {
                if (countryId == 1)
                {
                    var listt = db.tvwVoucherInfoes.Where(x => x.countryId == 132).ToList();
                    list = MapVoucher.ToEntityList(listt);
                }
            }
            catch (Exception)
            {
                list = new List<VoucherInfo>();
            }
            return list;
        }

        public List<VoucherInfo> GetVoucherByCondtion(int applicationStatusId, int countryType, string condition)
        {
            List<VoucherInfo> list = new List<VoucherInfo>();
            try
            {
                var listt = db.spVoucherGetList(applicationStatusId, countryType, condition).ToList();
                list = MapVoucher.ToEntityList(listt);

            }
            catch (Exception)
            {
                list = new List<VoucherInfo>();
            }
            return list;
        }


        public DataTable VoucherSearch(VoucherSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spVoucherSearch]"
            };
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@countryTypeId", obj.countryTypeId);
            cmd.Parameters.AddWithValue("@applicantNo", obj.applicantNo);
            cmd.Parameters.AddWithValue("@cnicNo", obj.cnicNo);
            cmd.Parameters.AddWithValue("@startDate", obj.startDate);
            cmd.Parameters.AddWithValue("@endDate", obj.endDate);
            cmd.Parameters.AddWithValue("@applicationStatusId", obj.applicationStatusId);
            cmd.Parameters.AddWithValue("@voucherStatusId", obj.voucherStatusId);
            cmd.Parameters.AddWithValue("@condition", obj.condition);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable VoucherSearchWithBank(VoucherSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spVoucherSearchWithBank]"
            };
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@statusIdBank", obj.statusIdBank);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable VoucherSearchBank(VoucherSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spVoucherSearchBank]"
            };
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@startDate", obj.startDate);
            cmd.Parameters.AddWithValue("@endDate", obj.endDate);
            cmd.Parameters.AddWithValue("@condition", obj.condition);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable VoucherExport(VoucherSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spVoucherExport]"
            };

            cmd.Parameters.AddWithValue("@countryTypeId", obj.countryTypeId);
            cmd.Parameters.AddWithValue("@applicantNo", obj.applicantNo);
            cmd.Parameters.AddWithValue("@cnicNo", obj.cnicNo);
            cmd.Parameters.AddWithValue("@startDate", obj.startDate);
            cmd.Parameters.AddWithValue("@endDate", obj.endDate);
            cmd.Parameters.AddWithValue("@applicationStatusId", obj.applicationStatusId);
            cmd.Parameters.AddWithValue("@voucherStatusId", obj.voucherStatusId);
            cmd.Parameters.AddWithValue("@condition", obj.condition);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable VoucherExportBank(VoucherSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spVoucherExportBank]"
            };
            cmd.Parameters.AddWithValue("@applicantNo", obj.applicantNo);
            cmd.Parameters.AddWithValue("@startDate", obj.startDate);
            cmd.Parameters.AddWithValue("@endDate", obj.endDate);
            cmd.Parameters.AddWithValue("@condition", obj.condition);
            return PrpDbADO.FillDataTable(cmd);
        }


        public DataTable GetApplicantThoseWasInServiceHospitalSepeciality()
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGetApplicantThoseWasInServiceHospitalSepeciality]"
            };

            return PrpDbADO.FillDataTable(cmd);
        }
    }
}
