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

        public AmendmentsApplicantNursing getAmendmentsApplicantNursingData(int applicantId)
        {
            AmendmentsApplicantNursing data = new AmendmentsApplicantNursing();
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGetAmendmentsApplicantNursingData]"
            };
            SqlConnection con = new SqlConnection();
            con = new SqlConnection(PrpDbConnectADO.Conn);
            cmd.Parameters.AddWithValue("@applicantId", applicantId);
            DataTable dt = new DataTable();
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            try
            {
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    data.amendmentId = Convert.ToInt32(dr[0]);
                    int i = 0;
                    data.adminId = Convert.ToInt32(dr[++i]);
                    data.applicantId = Convert.ToInt32(dr[++i]);
                    data.isAdmin = Convert.ToInt32(dr[++i]);
                    data.isApplicant = Convert.ToInt32(dr[++i]);
                    data.dated = Convert.ToDateTime(dr[++i]);
                    data.txtNameValidty = dr[++i].TooBoolean();
                    data.txtFatherNameValidty = dr[++i].TooBoolean();
                    data.txtDobValidty = dr[++i].TooBoolean();
                    data.ddlDistrictValidty = dr[++i].TooBoolean();
                    data.ddlcnicSelectValidty = dr[++i].TooBoolean();
                    data.txtCNICValidty = dr[++i].TooBoolean();
                    data.txtCNICExpiryDateValidty = dr[++i].TooBoolean();
                    data.ddlDomicileDistrictValidty = dr[++i].TooBoolean();
                    data.txtAddressValidty = dr[++i].TooBoolean();
                    data.imgPicValidty = dr[++i].TooBoolean();
                    data.imgCnicFrontValidty = dr[++i].TooBoolean();
                    data.imgCnicBackValidty = dr[++i].TooBoolean();
                    data.imgDomicileFrontValidty = dr[++i].TooBoolean();
                    data.ddlMatricBoardValidty = dr[++i].TooBoolean();
                    data.txtRowDegree1MarksObtainValidty = dr[++i].TooBoolean();
                    data.txtRowDegree1MarksTotalValidty = dr[++i].TooBoolean();
                    data.txtDateOfPassingMatricValidty = dr[++i].TooBoolean();
                    data.imgRowDegree1Validty = dr[++i].TooBoolean();
                    data.ddlFABoardValidty = dr[++i].TooBoolean();
                    data.txtRowDegree2MarksObtainValidty = dr[++i].TooBoolean();
                    data.txtRowDegree2MarksTotalValidty = dr[++i].TooBoolean();
                    data.txtDateOfPassingInterValidty = dr[++i].TooBoolean();
                    data.imgRowDegree2Validty = dr[++i].TooBoolean();
                    data.txtBranchCodeValidty = dr[++i].TooBoolean();
                    data.txtSubmittedDateVoucherValidty = dr[++i].TooBoolean();
                    data.imgVoucherValidty = dr[++i].TooBoolean();

                    data.txtNameRemarks = dr[++i].TooString();
                    data.txtFatherNameRemarks = dr[++i].TooString();
                    data.txtDobRemarks = dr[++i].TooString();
                    data.ddlDistrictRemarks = dr[++i].TooString();
                    data.ddlcnicSelectRemarks = dr[++i].TooString();
                    data.txtCNICRemarks = dr[++i].TooString();
                    data.txtCNICExpiryDateRemarks = dr[++i].TooString();
                    data.ddlDomicileDistrictRemarks = dr[++i].TooString();
                    data.txtAddressRemarks = dr[++i].TooString();
                    data.imgPicRemarks = dr[++i].TooString();
                    data.imgCnicFrontRemarks = dr[++i].TooString();
                    data.imgCnicBackRemarks = dr[++i].TooString();
                    data.imgDomicileFrontRemarks = dr[++i].TooString();
                    data.ddlMatricBoardRemarks = dr[++i].TooString();
                    data.txtRowDegree1MarksObtainRemarks = dr[++i].TooString();
                    data.txtRowDegree1MarksTotalRemarks = dr[++i].TooString();
                    data.txtDateOfPassingMatricRemarks = dr[++i].TooString();
                    data.imgRowDegree1Remarks = dr[++i].TooString();
                    data.ddlFABoardRemarks = dr[++i].TooString();
                    data.txtRowDegree2MarksObtainRemarks = dr[++i].TooString();
                    data.txtRowDegree2MarksTotalRemarks = dr[++i].TooString();
                    data.txtDateOfPassingInterRemarks = dr[++i].TooString();
                    data.imgRowDegree2Remarks = dr[++i].TooString();
                    data.txtBranchCodeRemarks = dr[++i].TooString();
                    data.txtSubmittedDateVoucherRemarks = dr[++i].TooString();
                    data.imgVoucherRemarks = dr[++i].TooString();
                    data.approvalStatus = dr[++i].TooInt();
                    data.approvalRemarks = dr[++i].TooString();
                    data.approvedBy = dr[++i].TooInt();
                    data.ddlMaritalStatusValidity = dr[++i].TooBoolean();
                    data.ddlMaritalStatusRemarks = dr[++i].TooString();
                    data.amendmentStatus = dr[i++].TooInt();
                }
            }
            catch (Exception ex)
            {

            }

            return data;
        }
    }


}
