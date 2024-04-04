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
    public class MeritDAL : PrpDBConnect
    {

        public List<ApplicantSpecilityMerit> GetApplicantSpecialityWithMeritMarksFCPS(int applicantId)
        {
            List<ApplicantSpecilityMerit> list = new List<ApplicantSpecilityMerit>();
            try
            {
                var listt = db.spApplicantSpecialityWithMeritMarksFCPS(applicantId).ToList();
                list = MapMerit.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantSpecilityMerit>();
            }
            return list;
        }

        //public int typeId { get; set; }
        //public Nullable<int> applicantId { get; set; }
        //public int specialityJobId { get; set; }
        //public string specialityName { get; set; }
        //public string hospitalName { get; set; }
        //public Nullable<int> preferenceNo { get; set; }
        //public Nullable<decimal> marks { get; set; }
        //public Nullable<decimal> minMerit { get; set; }
        //public Nullable<decimal> differnceMarks { get; set; }
        //public int specialityJobIdMerit { get; set; }
        //public int preferenceNoMerit { get; set; }
        //public int isFilled { get; set; }



        public List<ApplicantSpecilityMerit> GetApplicantSpecialityWithMeritMarks(int inductionId, int applicantId, int roundNo)
        {
            List<ApplicantSpecilityMerit> list = new List<ApplicantSpecilityMerit>();
            try
            {

                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantSpecialityWithMeritMarks]"
                };

                cmd.Parameters.AddWithValue("@inductionId", inductionId);
                cmd.Parameters.AddWithValue("@applicantId", applicantId);
                cmd.Parameters.AddWithValue("@roundNo", roundNo);
                DataTable dt = PrpDbADO.FillDataTable(cmd);
                if (dt != null && dt.Rows.Count > 0)
                {

                    foreach (DataRow dr in dt.Rows)
                    {
                        ApplicantSpecilityMerit obj = new ApplicantSpecilityMerit();

                        obj.typeId = dr["typeId"].TooInt();
                        obj.applicantId = dr["applicantId"].TooInt();
                        obj.specialityJobId = dr["specialityJobId"].TooInt();
                        obj.specialityName = dr["specialityName"].TooString();
                        obj.hospitalName = dr["hospitalName"].TooString();
                        obj.preferenceNo = dr["preferenceNo"].TooInt();
                        obj.marks = dr["marks"].TooDecimal();
                        obj.minMerit = dr["minMerit"].TooDecimal();
                        obj.differnceMarks = dr["differnceMarks"].TooDecimal();
                        obj.specialityJobIdMerit = dr["specialityJobIdMerit"].TooInt();
                        obj.preferenceNoMerit = dr["preferenceNoMerit"].TooInt();
                        obj.isFilled = dr["isFilled"].TooInt();
                        list.Add(obj);
                    }
                }

                //var listt = db.spApplicantSpecialityWithMeritMarks(applicantId, roundNo).ToList();
                //list = MapMerit.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantSpecilityMerit>();
            }
            return list;
        }

        public DataTable GazatGetAllByTypeView(GazatMerit obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGazatGetAllByTypeView]"
            };

            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable GazetteGetAllByTypeView(GazatMerit obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGazatGetAllByTypeViewCopy]"
            };

            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable MeritGetAllByTypeView(GazatMerit obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spMeritGetAllByTypeView]"
            };

            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@quotaId", obj.quotaId);
            cmd.Parameters.AddWithValue("@roundNo", obj.roundNo);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }


        public DataTable GazatGetAllByTypeExport(GazatMerit obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGazatGetAllByTypeExport]"
            };
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            return PrpDbADO.FillDataTable(cmd);
        }

    }
}
