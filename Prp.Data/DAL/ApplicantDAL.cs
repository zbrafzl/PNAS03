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
    public class ApplicantDAL : PrpDBConnect
    {
        #region Applicant

        public Applicant Login(string userName, string password)
        {
            Applicant obj = new Applicant();
            try
            {

                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantLoginNursing]"
                };
                cmd.Parameters.AddWithValue("@emailId", userName);
                cmd.Parameters.AddWithValue("@password", password);

                DataTable dt = PrpDbADO.FillDataTable(cmd);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];

                    obj.inductionId = dr["inductionId"].TooInt();
                    obj.applicantId = dr["applicantId"].TooInt();
                    obj.network = dr["network"].TooInt();
                    obj.facultyId = dr["facultyId"].TooInt();
                    obj.statusId = dr["statusId"].TooInt();

                    obj.name = dr["name"].TooString();
                    obj.pmdcNo = dr["pmdcNo"].TooString();
                    obj.pncNo = dr["pncNo"].TooString();
                    obj.emailId = dr["emailId"].TooString();
                    obj.password = dr["password"].TooString();
                    obj.passwordDecrypt = dr["passwordDecrypt"].TooString();
                    obj.contactNumber = dr["contactNumber"].TooString();
                    obj.pic = dr["pic"].TooString();
                    obj.date = dr["date"].TooString();
                    obj.facultyName = dr["facultyName"].TooString();
                    obj.status = dr["status"].TooString();
                }

                //var dbt = db.spApplicantLogin(userName, password).FirstOrDefault();
                //obj = MapApplicant.ToEntity(dbt);
            }
            catch (Exception ex)
            {
                obj = new Applicant();
            }
            return obj;
        }

        public Message AddUpdateSeats(int inductionId, int totalSeats, int vacantSeats, int adminId)
        {
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spAddUpdateSeats]"
                };
                DateTime dts = DateTime.Now;

                cmd.Parameters.AddWithValue("@inductionId", inductionId);
                cmd.Parameters.AddWithValue("@totalSeats", totalSeats);
                cmd.Parameters.AddWithValue("@vacantSeats", vacantSeats);
                cmd.Parameters.AddWithValue("@adminId", adminId);

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

        public Message UpdateMigrationCandidate(MigrationCandidateData obj)
        {
            var date = DateTime.UtcNow;
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spUpdateMigrationCandidate]"
                };
                DateTime dts = obj.dOB.TooDate();
                DateTime cnicExpirtyDate = DateTime.Now.TooDate();

                cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
                cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
                cmd.Parameters.AddWithValue("@name", obj.name);
                cmd.Parameters.AddWithValue("@fatherName", Convert.ToString(obj.fatherName.TooString()));
                cmd.Parameters.AddWithValue("@genderId", obj.genderId);
                cmd.Parameters.AddWithValue("@dob", dts);
                cmd.Parameters.AddWithValue("@joiningDate", obj.joiningDate.TooDate());

                cmd.Parameters.AddWithValue("@contactNo", obj.contactNo);
                cmd.Parameters.AddWithValue("@cnicNo", obj.cnicNo);
                cmd.Parameters.AddWithValue("@domicileDistrictId", obj.domicileDistrictId);
                cmd.Parameters.AddWithValue("@religionId", obj.religionId);
                cmd.Parameters.AddWithValue("@emailId", obj.emailId);
                cmd.Parameters.AddWithValue("@districtId", obj.districtId);
                cmd.Parameters.AddWithValue("@address", obj.address);
                cmd.Parameters.AddWithValue("@matricBoard", obj.matricBoard);
                cmd.Parameters.AddWithValue("@matricMarksObtain", obj.matricMarksObtain);
                cmd.Parameters.AddWithValue("@matricTotalMarks", obj.matricTotalMarks);
                cmd.Parameters.AddWithValue("@dateOfPassingMatric", obj.dateOfPassingMatric.TooDate());
                cmd.Parameters.AddWithValue("@fscBoard", obj.fscBoard);
                cmd.Parameters.AddWithValue("@fscObtainedMarks", obj.fscObtainedMarks);
                cmd.Parameters.AddWithValue("@fscTotalMarks", obj.fscTotalMarks);
                cmd.Parameters.AddWithValue("@dateOfPassingInter", obj.dateOfPassingInter.TooDate());
                cmd.Parameters.AddWithValue("@img", obj.img);
                cmd.Parameters.AddWithValue("@cnicFront", obj.cnicFront);
                cmd.Parameters.AddWithValue("@cnicBack", obj.cnicBack);
                cmd.Parameters.AddWithValue("@imgDomicile", obj.imgDomicile);
                cmd.Parameters.AddWithValue("@domicilePicFront", obj.domicilePicFront);
                cmd.Parameters.AddWithValue("@imgMatricDegree", obj.imgMatricDegree);
                cmd.Parameters.AddWithValue("@imgFscDegree", obj.imgFscDegree);
                cmd.Parameters.AddWithValue("@adminId", obj.adminID);
                cmd.Parameters.AddWithValue("@dated", DateTime.Now);

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
        public Applicant LoginByPhfDeveloper(string userName, int typeId)
        {
            Applicant obj = new Applicant();
            try
            {
                //var dbt = db.spApplicantLoginByPhf(userName, typeId).FirstOrDefault();
                //obj = MapApplicant.ToEntity(dbt);
            }
            catch (Exception ex)
            {
                obj = new Applicant();
            }
            return obj;
        }

        public Message ApplicantIsExist(IsExists obj)
        {
            Message msg = new Message();
            try
            {
                var item = db.spApplicantIsExist(obj.id, obj.search, obj.condition).FirstOrDefault();
                if (item != null && item.applicantId > 0)
                {
                    msg.id = item.applicantId;
                }
                else
                {
                    msg.id = 0;
                }
            }
            catch (Exception ex)
            {
                msg.id = 0;
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message PNCApplicantIsExist(IsExists obj)
        {
            Message msg = new Message();
            try
            {
                var item = db.spApplicantIsExist(obj.id, obj.search, obj.condition).FirstOrDefault();
                if (item != null && item.applicantId > 0)
                {
                    msg.id = item.applicantId;
                }
                else
                {
                    msg.id = 0;
                }
            }
            catch (Exception ex)
            {
                msg.id = 0;
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message Registration(Applicant obj)
        {
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantRegister]"
                };
                cmd.Parameters.AddWithValue("@name", obj.name);
                cmd.Parameters.AddWithValue("@pmdcNo", obj.pncNo);
                cmd.Parameters.AddWithValue("@pncNo", obj.pncNo);
                cmd.Parameters.AddWithValue("@emailId", obj.emailId);
                cmd.Parameters.AddWithValue("@password", obj.password);
                cmd.Parameters.AddWithValue("@contactNumber", obj.contactNumber);
                cmd.Parameters.AddWithValue("@network", obj.network);
                cmd.Parameters.AddWithValue("@genderID", obj.genderID);
                cmd.Parameters.AddWithValue("@levelId", obj.levelId);
                cmd.Parameters.AddWithValue("@facultyId", obj.facultyId);
                cmd.Parameters.AddWithValue("@pic", obj.pic);
                cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
                cmd.Parameters.AddWithValue("@adminId", obj.adminId);
                DataTable dt = PrpDbADO.FillDataTable(cmd);

                msg = dt.ConvertToEnitityMessage();
                //var item = db.spApplicantRegister(obj.name, obj.pmdcNo, obj.emailId, obj.password, obj.contactNumber, obj.network
                //       , obj.levelId, obj.facultyId, obj.pic).FirstOrDefault();
                //msg = MapApplicant.ToEntity(item);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public DataTable OtpGetByMobileNo(string mobilenumber)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spOtpGetByMobileNo]"
            };
            cmd.Parameters.AddWithValue("@mobilenumber", mobilenumber);
            return PrpDbADO.FillDataTable(cmd);
        }

        public Message ApplicantUpdate(Applicant obj)
        {
            Message msg = ApplicantUpdateByAdmin(obj);
            return msg;
        }
        public Message ApplicantUpdateVerificationNursing(AmendmentsApplicantNursing obj)
        {
            Message msg = AmendmentsApplicantNursingByAdmin(obj);
            return msg;
        }

        public Message ApplicantDelete(Applicant obj)
        {
            Message msg = ApplicantDeleteByAdmin(obj);
            return msg;
        }

        public Message AmendmentsApplicantNursingByAdmin(AmendmentsApplicantNursing obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantUpdateVerificationNursingByAdmin]"
            };

            Message msg = new Message();

            cmd.Parameters.AddWithValue("@adminId", obj.adminId.TooInt());
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId.TooInt());
            cmd.Parameters.AddWithValue("@isAdmin", obj.isAdmin.TooInt());
            cmd.Parameters.AddWithValue("@isApplicant", obj.isApplicant.TooInt());
            cmd.Parameters.AddWithValue("@dated", obj.dated.TooDate());
            cmd.Parameters.AddWithValue("@txtNameValidty", obj.txtNameValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@txtFatherNameValidty", obj.txtFatherNameValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@txtDobValidty", obj.txtDobValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@ddlDistrictValidty", obj.ddlDistrictValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@ddlcnicSelectValidty", obj.ddlcnicSelectValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@txtCNICValidty", obj.txtCNICValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@txtCNICExpiryDateValidty", obj.txtCNICExpiryDateValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@ddlDomicileDistrictValidty", obj.ddlDomicileDistrictValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@txtAddressValidty", obj.txtAddressValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@imgPicValidty", obj.imgPicValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@imgCnicFrontValidty", obj.imgCnicFrontValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@imgCnicBackValidty", obj.imgCnicBackValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@imgDomicileFrontValidty", obj.imgDomicileFrontValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@ddlMatricBoardValidty", obj.ddlMatricBoardValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@txtRowDegree1MarksObtainValidty", obj.txtRowDegree1MarksObtainValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@txtRowDegree1MarksTotalValidty", obj.txtRowDegree1MarksTotalValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@txtDateOfPassingMatricValidty", obj.txtDateOfPassingMatricValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@imgRowDegree1Validty", obj.imgRowDegree1Validty.TooBoolean());
            cmd.Parameters.AddWithValue("@ddlFABoardValidty", obj.ddlFABoardValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@txtRowDegree2MarksObtainValidty", obj.txtRowDegree2MarksObtainValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@txtRowDegree2MarksTotalValidty", obj.txtRowDegree2MarksTotalValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@txtDateOfPassingInterValidty", obj.txtDateOfPassingInterValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@imgRowDegree2Validty", obj.imgRowDegree2Validty.TooBoolean());
            cmd.Parameters.AddWithValue("@txtBranchCodeValidty", obj.txtBranchCodeValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@txtSubmittedDateVoucherValidty", obj.txtSubmittedDateVoucherValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@imgVoucherValidty", obj.imgVoucherValidty.TooBoolean());
            cmd.Parameters.AddWithValue("@txtNameRemarks", obj.txtNameRemarks.TooString());
            cmd.Parameters.AddWithValue("@txtFatherNameRemarks", obj.txtFatherNameRemarks.TooString());
            cmd.Parameters.AddWithValue("@txtDobRemarks", obj.txtDobRemarks.TooString());
            cmd.Parameters.AddWithValue("@ddlDistrictRemarks", obj.ddlDistrictRemarks.TooString());
            cmd.Parameters.AddWithValue("@ddlcnicSelectRemarks", obj.ddlcnicSelectRemarks.TooString());
            cmd.Parameters.AddWithValue("@txtCNICRemarks", obj.txtCNICRemarks.TooString());
            cmd.Parameters.AddWithValue("@txtCNICExpiryDateRemarks", obj.txtCNICExpiryDateRemarks.TooString());
            cmd.Parameters.AddWithValue("@ddlDomicileDistrictRemarks", obj.ddlDomicileDistrictRemarks.TooString());
            cmd.Parameters.AddWithValue("@txtAddressRemarks", obj.txtAddressRemarks.TooString());
            cmd.Parameters.AddWithValue("@imgPicRemarks", obj.imgPicRemarks.TooString());
            cmd.Parameters.AddWithValue("@imgCnicFrontRemarks", obj.imgCnicFrontRemarks.TooString());
            cmd.Parameters.AddWithValue("@imgCnicBackRemarks", obj.imgCnicBackRemarks.TooString());
            cmd.Parameters.AddWithValue("@imgDomicileFrontRemarks", obj.imgDomicileFrontRemarks.TooString());
            cmd.Parameters.AddWithValue("@ddlMatricBoardRemarks", obj.ddlMatricBoardRemarks.TooString());
            cmd.Parameters.AddWithValue("@txtRowDegree1MarksObtainRemarks", obj.txtRowDegree1MarksObtainRemarks.TooString());
            cmd.Parameters.AddWithValue("@txtRowDegree1MarksTotalRemarks", obj.txtRowDegree1MarksTotalRemarks.TooString());
            cmd.Parameters.AddWithValue("@txtDateOfPassingMatricRemarks", obj.txtDateOfPassingMatricRemarks.TooString());
            cmd.Parameters.AddWithValue("@imgRowDegree1Remarks", obj.imgRowDegree1Remarks.TooString());
            cmd.Parameters.AddWithValue("@ddlFABoardRemarks", obj.ddlFABoardRemarks.TooString());
            cmd.Parameters.AddWithValue("@txtRowDegree2MarksObtainRemarks", obj.txtRowDegree2MarksObtainRemarks.TooString());
            cmd.Parameters.AddWithValue("@txtRowDegree2MarksTotalRemarks", obj.txtRowDegree2MarksTotalRemarks.TooString());
            cmd.Parameters.AddWithValue("@txtDateOfPassingInterRemarks", obj.txtDateOfPassingInterRemarks.TooString());
            cmd.Parameters.AddWithValue("@imgRowDegree2Remarks", obj.imgRowDegree2Remarks.TooString());
            cmd.Parameters.AddWithValue("@txtBranchCodeRemarks", obj.txtBranchCodeRemarks.TooString());
            cmd.Parameters.AddWithValue("@txtSubmittedDateVoucherRemarks", obj.txtSubmittedDateVoucherRemarks.TooString());
            cmd.Parameters.AddWithValue("@imgVoucherRemarks", obj.imgVoucherRemarks.TooString());
            cmd.Parameters.AddWithValue("@approvalStatus", obj.approvalStatus.TooInt());
            cmd.Parameters.AddWithValue("@approvalRemarks", obj.approvalRemarks.TooString());
            cmd.Parameters.AddWithValue("@approvedBy", obj.approvedBy.TooInt());
            cmd.Parameters.AddWithValue("@ddlMaritalStatusValidity", obj.ddlMaritalStatusValidity.TooBoolean());
            cmd.Parameters.AddWithValue("@ddlMaritalStatusRemarks", obj.ddlMaritalStatusRemarks.TooString());
            SqlConnection con = new SqlConnection(PrpDbConnectADO.Conn);
            cmd.Connection = con;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            if (i == 0)
            {
                msg.status = false;
            }
            else
            {
                msg.status = true;
            }
            return msg;
        }

        public Message ApplicantUpdateByAdmin(Applicant obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantUpdateAdmin]"
            };
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@name", obj.name);
            cmd.Parameters.AddWithValue("@pmdcNo", "");
            cmd.Parameters.AddWithValue("@emailId", obj.emailId);
            cmd.Parameters.AddWithValue("@contactNumber", obj.contactNumber);
            cmd.Parameters.AddWithValue("@facultyId", 13);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);

            return PrpDbADO.FillDataTableMessage(cmd);

        }

        public Message ApplicantDeleteByAdmin(Applicant obj)
        {
            Message msg = new Message();
            SqlConnection con = new SqlConnection(PrpDbConnectADO.Conn);
            try
            {
                string query = "delete from tblApplicant where applicantId = " + obj.applicantId + " "
                + " delete from tblApplicationStatus where applicantId = " + obj.applicantId + " "
                + " delete from tblNursingApplicantDegreeData where applicantID = " + obj.applicantId + " "
                + " delete from tblApplicantInfo where applicantId = " + obj.applicantId + " ";
                SqlCommand cmdUpdate = new SqlCommand(query);
                con.Open();
                cmdUpdate.Connection = con;
                cmdUpdate.ExecuteNonQuery();
                msg.status = true;
                msg.msg = "Applicant Delete Successfully";
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.message = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return msg;
        }

        public Message ChangePassword(ChangePassword obj)
        {
            Message msg = new Message();
            try
            {

                var item = db.spApplicantChangePassword(obj.id, obj.passwordOld, obj.passwordNew).FirstOrDefault();
                msg = MapApplicant.ToEntity(item);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Applicant GetApplicant(int inductionId, int applicantId)
        {
            Applicant obj = new Applicant();
            DataTable dt = new DataTable();
            try
            {
                string query = "select [applicantId],[name],[pmdcNo],[emailId],[password],[contactNumber],[network],[levelId],[facultyId],[pic],[statusId],[dated],[adminId],[pncNo],[genderID] ";
                query += "from tblApplicant where applicantId = " + applicantId + "";
                SqlConnection con = new SqlConnection();
                Message msg = new Message();
                SqlCommand cmd = new SqlCommand(query);
                try
                {
                    con = new SqlConnection(PrpDbConnectADO.Conn);
                    con.Open();
                    cmd.Connection = con;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        obj.accountStatus = dr["statusId"].ToString();
                        obj.accountStatusId = Convert.ToInt32(dr["statusId"]);
                        obj.adminId = Convert.ToInt32(dr["statusId"]);
                        obj.applicantId = Convert.ToInt32(dr["applicantId"]);
                        obj.applicationStatus = "active";
                        obj.applicationStatusId = Convert.ToInt32(dr["statusId"]);
                        obj.contactNumber = (dr["contactNumber"]).ToString();
                        obj.date = Convert.ToDateTime((dr["dated"])).ToString();
                        obj.dated = Convert.ToDateTime(dr["dated"]);
                        obj.emailId = dr["emailId"].ToString();
                        obj.facultyId = Convert.ToInt32(dr["facultyId"]);
                        obj.facultyName = Convert.ToInt32(dr["facultyId"]).ToString();
                        obj.inductionId = 12;
                        obj.isValid = 1;
                        obj.levelId = Convert.ToInt32(dr["levelId"]);
                        obj.levelName = Convert.ToInt32(dr["levelId"]).ToString();
                        obj.maritalStatus = "";
                        obj.name = dr["name"].ToString();
                        obj.network = Convert.ToInt32(dr["network"]);
                        obj.password = dr["password"].ToString();
                        obj.passwordDecrypt = dr["password"].ToString();
                        obj.pathProfilePic = dr["pic"].ToString();
                        obj.pmdcNo = "";
                        obj.pncNo = dr["pncNo"].ToString();
                        obj.status = dr["statusId"].ToString();
                        obj.statusId = Convert.ToInt32(dr["statusId"]);
                        obj.genderID = Convert.ToInt32(dr["genderID"]);
                        obj.pic = dr["pic"].ToString();
                    }
                    else
                    {
                        msg.status = false;
                    }
                }
                catch (Exception ex)
                {
                    msg.status = false;
                    msg.msg = ex.Message;
                }
                finally
                {
                    con.Close();
                }
                //var dbt = db.tblApplicants.FirstOrDefault(x => x.applicantId == applicantId);



            }
            catch (Exception ex)
            {
                obj = new Applicant();
            }
            return obj;
        }

        public DataTable GetApplicantByIdAdmin(int applicantId)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantGetByIdAdmin]"
            };
            cmd.Parameters.AddWithValue("@applicantId", applicantId);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable GetApplicantById(int applicantId)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantGetById]"
            };
            cmd.Parameters.AddWithValue("@applicantId", applicantId);
            return PrpDbADO.FillDataTable(cmd);
        }

        public Message GetApplicantIdBySearch(string search, string condition)
        {
            Message msg = new Message();
            try
            {
                string query = "";
                DataTable dt = new DataTable();
                if (condition.Trim() == "applicantId")
                {
                    query = "select top 1 applicantId from tblApplicant where applicantId = " + Convert.ToInt32(search) + " ";
                }
                else if (condition.Trim() == "emailId")
                {
                    query = "select top 1 applicantId from tblApplicant where emailId like '%" + search + "%' ";
                }
                else if (condition.Trim() == "contactNumber")
                {
                    query = "select top 1 applicantId from tblApplicant where contactNumber like '%" + search + "%' ";
                }
                else if (condition.Trim() == "pmdcNo")
                {
                    query = "select top 1 t1.applicantId from tblApplicant t1 inner join tblApplicantInfo t2 on t1.applicantId = t2.applicantId where t2.cnicNo like '%" + search + "%' ";
                }
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand(query);
                try
                {
                    con = new SqlConnection(PrpDbConnectADO.Conn);
                    con.Open();
                    cmd.Connection = con;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        msg.id = dr[0].TooInt();
                        msg.status = true;
                    }
                    else
                    {
                        msg.status = false;
                    }
                }
                catch (Exception ex)
                {
                    msg.status = false;
                    msg.msg = ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            //msg.id = search.TooInt();
            //msg.status = true;
            //msg.message = "Applicant Exists";
            return msg;
        }

        public Applicant GetApplicantByEmail(string emailId)
        {
            Applicant obj = new Applicant();
            //try
            //{
            //    var dbt = db.tvwApplicants.FirstOrDefault(x => x.emailId == emailId);
            //    obj = MapApplicant.ToEntity(dbt);
            //}
            //catch (Exception)
            //{
            //    obj = new Applicant();
            //}
            DataTable dt = new DataTable();
            string query = "select [applicantId],[name],[pmdcNo],[emailId],[password],[contactNumber],[network],[levelId],[facultyId],[pic],[statusId],[dated],[adminId],[pncNo],[genderID]";
            query += "from tblApplicant where emailId = '" + emailId + "'";
            SqlConnection con = new SqlConnection();
            Message msg = new Message();
            SqlCommand cmd = new SqlCommand(query);
            try
            {
                con = new SqlConnection(PrpDbConnectADO.Conn);
                con.Open();
                cmd.Connection = con;
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    obj.accountStatus = dr["statusId"].ToString();
                    obj.accountStatusId = Convert.ToInt32(dr["statusId"]);
                    obj.adminId = Convert.ToInt32(dr["statusId"]);
                    obj.applicantId = Convert.ToInt32(dr["applicantId"]);
                    obj.applicationStatus = "active";
                    obj.applicationStatusId = Convert.ToInt32(dr["statusId"]);
                    obj.contactNumber = (dr["contactNumber"]).ToString();
                    obj.date = Convert.ToDateTime((dr["dated"])).ToString();
                    obj.dated = Convert.ToDateTime(dr["dated"]);
                    obj.emailId = dr["emailId"].ToString();
                    obj.facultyId = Convert.ToInt32(dr["facultyId"]);
                    obj.facultyName = Convert.ToInt32(dr["facultyId"]).ToString();
                    obj.inductionId = 12;
                    obj.isValid = 1;
                    obj.levelId = Convert.ToInt32(dr["levelId"]);
                    obj.levelName = Convert.ToInt32(dr["levelId"]).ToString();
                    obj.maritalStatus = "";
                    obj.name = dr["name"].ToString();
                    obj.network = Convert.ToInt32(dr["network"]);
                    obj.password = dr["password"].ToString();
                    obj.passwordDecrypt = dr["password"].ToString();
                    obj.pathProfilePic = dr["pic"].ToString();
                    obj.pmdcNo = "";
                    obj.pncNo = dr["pncNo"].ToString();
                    obj.status = dr["statusId"].ToString();
                    obj.statusId = Convert.ToInt32(dr["statusId"]);
                    obj.genderID = Convert.ToInt32(dr["genderID"]);
                }
                else
                {
                    msg.status = false;
                }
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return obj;
        }

        //public ApplicantStatus GetApplicantStatus(int applicantId)
        //{
        //    ApplicantStatus obj = new ApplicantStatus();
        //    try
        //    {
        //        var dbt = db.spApplicantStatusGet(applicantId).FirstOrDefault();
        //        obj = MapApplicant.ToEntity(dbt);
        //    }
        //    catch (Exception)
        //    {
        //        obj = new ApplicantStatus();
        //    }
        //    return obj;
        //}

        public List<ApplicationStatus> GetApplicationStatus(int inductionId, int phaseId, int applicantId, int statusTypeId = 0)
        {
            List<ApplicationStatus> list = new List<ApplicationStatus>();
            try
            {

                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicationStatusGet]"
                };
                cmd.Parameters.AddWithValue("@inductionId", inductionId);
                cmd.Parameters.AddWithValue("@phaseId", phaseId);
                cmd.Parameters.AddWithValue("@applicantId", applicantId);
                cmd.Parameters.AddWithValue("@statusTypeId", statusTypeId);

                DataTable dt = PrpDbADO.FillDataTable(cmd);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        ApplicationStatus obj = new ApplicationStatus();

                        obj.inductionId = dr["inductionId"].TooInt();
                        obj.applicantId = dr["applicantId"].TooInt();
                        obj.statusTypeId = dr["statusTypeId"].TooInt();
                        obj.statusId = dr["statusId"].TooInt();

                        obj.statusType = dr["statusType"].TooString();
                        obj.status = dr["status"].TooString();
                        list.Add(obj);
                    }
                }


                //var dbt = db.spApplicationStatusGet(inductionId, phaseId, applicantId, statusTypeId);
                //obj = MapApplicant.ToEntity(dbt);
            }
            catch (Exception)
            {
                list = new List<ApplicationStatus>();
            }
            return list;
        }

        public ApplicationStatus GetApplicationStatus(int applicantId, int statusTypeId)
        {
            ApplicationStatus obj = new ApplicationStatus();
            try
            {

                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicationStatusGet]"
                };
                cmd.Parameters.AddWithValue("@applicantId", applicantId);
                cmd.Parameters.AddWithValue("@statusTypeId", statusTypeId);

                DataTable dt = PrpDbADO.FillDataTable(cmd);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        obj.inductionId = dr["inductionId"].TooInt();
                        obj.applicantId = dr["applicantId"].TooInt();
                        obj.statusTypeId = dr["statusTypeId"].TooInt();
                        obj.statusId = dr["statusId"].TooInt();
                        obj.statusType = dr["statusType"].TooString();
                        obj.status = dr["status"].TooString();
                    }
                }
               
            }
            catch (Exception)
            {
                obj = new ApplicationStatus();
            }
            return obj;
        }

        public Message ApplicantStatusUpdate(ApplicationStatus obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantStatusUpdate]"
            };
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@statusTypeId", obj.statusTypeId);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public Message ReigsterVerify(int applicantId, int statusId, int adminId)
        {
            Message msg = new Message();
            SqlConnection con = new SqlConnection(PrpDbConnectADO.Conn);
            try
            {
                string query = "";
                query = "IF NOT EXISTS (SELECT * FROM tblApplicationStatus "
                   + " WHERE inductionId = 15"
                   + " AND applicantId = " + applicantId + ""
                   + " AND statusTypeId = 53) "
                   //+ "AND statusId = 1) "
                   + " BEGIN "
                    + "INSERT INTO tblApplicationStatus(inductionId, applicantId, statusTypeId, statusId, dated) "
                    + " VALUES(15, " + applicantId + ", 53, 1, GETDATE()) "
                   + " END";
                SqlCommand cmdUpdate = new SqlCommand(query);
                con.Open();
                cmdUpdate.Connection = con;
                cmdUpdate.ExecuteNonQuery();
                msg.status = true;
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.message = ex.Message;
            }
            finally
            {
                con.Close();
            }
            return msg;
        }

        public Message AccountStatusUpdate(int applicantId, int statusId, int adminId)
        {
            //Insert Into tblApplicationStatus([inductionId], [applicantId], [statusTypeId], [statusId], [dated])
            //Values(12, @applicantId, 52, 1, GETDATE())
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spAccountStatusUpdate]"
                };

                cmd.Parameters.AddWithValue("@adminId", adminId);
                cmd.Parameters.AddWithValue("@applicantId", applicantId);
                cmd.Parameters.AddWithValue("@statusId", statusId);

                msg = PrpDbADO.FillDataTableMessage(cmd);

                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.message = ex.Message;
            }
            return msg;
        }

        public Message ReopenApplication(int applicantId, int status)
        {
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicationReopen]"
                };

                cmd.Parameters.AddWithValue("@applicantId", applicantId);
                cmd.Parameters.AddWithValue("@status", status);

                msg = PrpDbADO.FillDataTableMessage(cmd);

                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.message = ex.Message;
            }
            return msg;
        }

        public List<Applicant> GetApplicantList(int accountStatusId = 1)
        {
            List<Applicant> list = new List<Applicant>();
            try
            {
                //var dbt = db.tvwApplicants.Where(x => x.accountStatusId == accountStatusId).ToList();
                //list = MapApplicant.ToEntityList(dbt);
            }
            catch (Exception)
            {
                list = new List<Applicant>();
            }
            return list;
        }

        #endregion

        #region ApplicantInfo

        public Message ApplicantInfoAddUpdate(ApplicantInfo obj)
        {
            Message msg = new Message();
            try
            {
                //string query = "select count(*) from tblApplicant where pncNo like '" + obj + "'";
                //int count = 0;
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand("spApplicantAddUpdateInfo");
                try
                {
                    con = new SqlConnection(PrpDbConnectADO.Conn);
                    con.Open();
                    cmd.Connection = con;
                    obj.mbbsPassingDate = DateTime.Now;
                    obj.pmdcExpiryDate = DateTime.Now;
                    obj.midWiferyPassingDate = DateTime.Now;
                    cmd.CommandType = CommandType.StoredProcedure;
                    obj.generalNursingPassingDate = DateTime.Now;
                    obj.genericBSNPassingDate = DateTime.Now;
                    cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
                    cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
                    cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
                    cmd.Parameters.AddWithValue("@fatherName", obj.fatherName);
                    cmd.Parameters.AddWithValue("@genderId", obj.genderId);
                    cmd.Parameters.AddWithValue("@disableId", obj.disableId);
                    cmd.Parameters.AddWithValue("@dob", obj.dob);
                    cmd.Parameters.AddWithValue("@pmdcExpiryDate", obj.pmdcExpiryDate);
                    cmd.Parameters.AddWithValue("@mbbsPassingDate", obj.mbbsPassingDate);
                    cmd.Parameters.AddWithValue("@countryIdDegreePassing", 132);
                    cmd.Parameters.AddWithValue("@dualNationalityType", obj.dualNationalityType);
                    cmd.Parameters.AddWithValue("@countryId", obj.countryId);
                    cmd.Parameters.AddWithValue("@districtId", obj.districtId);
                    cmd.Parameters.AddWithValue("@districtName", obj.districtName);
                    cmd.Parameters.AddWithValue("@domicileProvinceId", obj.domicileProvinceId);
                    cmd.Parameters.AddWithValue("@domicileDistrictId", obj.domicileDistrictId);
                    cmd.Parameters.AddWithValue("@address", obj.address);
                    cmd.Parameters.AddWithValue("@cnicNo", obj.cnicNo);
                    cmd.Parameters.AddWithValue("@cnicExpiryDate", obj.cnicExpiryDate);
                    cmd.Parameters.AddWithValue("@cnicPicFront", obj.cnicPicFront);
                    cmd.Parameters.AddWithValue("@cnicPicBack", obj.cnicPicBack);
                    cmd.Parameters.AddWithValue("@domicilePicFront", obj.domicilePicFront);
                    cmd.Parameters.AddWithValue("@domicilePicBack", obj.domicilePicBack);
                    cmd.Parameters.AddWithValue("@pic", obj.pic);
                    cmd.Parameters.AddWithValue("@disableImage", obj.disableImage);
                    cmd.Parameters.AddWithValue("@religionId", obj.religionId);



                    if (obj.pncExpiryDate.Year == 1)
                    {
                        obj.pncExpiryDate = DateTime.Now;
                        obj.joiningDate = DateTime.Now;
                        obj.pncPicFront = "";
                        obj.pncPicBack = "";
                        obj.medicalFitnessCertificate = "";
                        obj.currentBPS = "";
                        obj.currentDesignation = "";
                        obj.currentPosting = "";
                        obj.nocCertificate = "";
                        obj.nicCertificate = "";
                        obj.recommendationLetter = "";
                        obj.recommendationLetter2 = "";
                        obj.regularizationOrder = "";
                    }
                    else
                    {
                        //cmd.Parameters.AddWithValue("@pncExpiryDate", obj.pncExpiryDate);
                    }
                    cmd.Parameters.AddWithValue("@medicalFitnessCertificate", obj.medicalFitnessCertificate);
                    cmd.Parameters.AddWithValue("@pncExpiryDate", obj.pncExpiryDate);
                    cmd.Parameters.AddWithValue("@joiningDate", obj.joiningDate);
                    cmd.Parameters.AddWithValue("@pncPicFront", obj.pncPicFront);
                    cmd.Parameters.AddWithValue("@pncPicBack", obj.pncPicBack);
                    cmd.Parameters.AddWithValue("@generalNursingPassingDate", obj.generalNursingPassingDate);
                    cmd.Parameters.AddWithValue("@midWiferyPassingDate", obj.midWiferyPassingDate);
                    cmd.Parameters.AddWithValue("@genericBSNPassingDate", obj.genericBSNPassingDate);
                    cmd.Parameters.AddWithValue("@meritalStatus", obj.meritalStatus);
                    cmd.Parameters.AddWithValue("@currentPosition", obj.currentDesignation);
                    cmd.Parameters.AddWithValue("@currentBPS", obj.currentBPS);
                    cmd.Parameters.AddWithValue("@currentPostingAddress", obj.currentPosting);
                    cmd.Parameters.AddWithValue("@nocCertificate", obj.nocCertificate);
                    cmd.Parameters.AddWithValue("@nicCertificate", obj.nicCertificate);
                    cmd.Parameters.AddWithValue("@recommendationLetter", "");
                    cmd.Parameters.AddWithValue("@recommendationLetter2", obj.recommendationLetter2);
                    cmd.Parameters.AddWithValue("@regularizationOrder", obj.regularizationOrder);
                    cmd.Parameters.AddWithValue("@levelId", obj.levelId);
                    cmd.Parameters.AddWithValue("@levelTypeId", obj.levelTypeId);
                    cmd.Parameters.AddWithValue("@instituteId", obj.instituteId);
                    //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    //count = Convert.ToInt32(cmd.ExecuteScalar());
                    int i = cmd.ExecuteNonQuery();
                    if (i == 0)
                    {
                        msg.status = false;
                    }
                    else
                    {
                        msg.status = true;
                    }

                }
                catch (Exception ex)
                {
                    msg.status = false;
                    msg.msg = ex.Message;
                }
                finally
                {
                    con.Close();
                }
                //int return = ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spApplicantAddUpdateInfo", obj.inductionId, obj.phaseId, obj.applicantId, obj.fatherName, obj.genderId, obj.disableId, obj.dob
                //    , obj.pmdcExpiryDate, obj.mbbsPassingDate, obj.countryIdDegreePassing, obj.dualNationalityType
                //   , obj.countryId, obj.districtId, obj.districtName, obj.domicileProvinceId, obj.domicileDistrictId
                //    , obj.address, obj.cnicNo, obj.cnicExpiryDate, obj.cnicPicFront, obj.cnicPicBack
                //    , obj.domicilePicFront, obj.domicilePicBack, obj.pic, obj.disableImage, obj.pncPicFront, obj.pncPicBack, obj.medicalFitnessCertificate
                //    , obj.pncExpiryDate, obj.generalNursingPassingDate, obj.midWiferyPassingDate, obj.genericBSNPassingDate, obj.meritalStatus);

                //db.spApplicantAddUpdateInfo(obj.inductionId, obj.phaseId, obj.applicantId, obj.fatherName, obj.genderId, obj.disableId, obj.dob
                //    , obj.pmdcExpiryDate, obj.mbbsPassingDate, obj.countryIdDegreePassing, obj.dualNationalityType
                //   , obj.countryId, obj.districtId, obj.districtName, obj.domicileProvinceId, obj.domicileDistrictId
                //    , obj.address, obj.cnicNo, obj.cnicExpiryDate, obj.cnicPicFront, obj.cnicPicBack
                //    , obj.domicilePicFront, obj.domicilePicBack, obj.pic, obj.disableImage, obj.pncPicFront, obj.pncPicBack, obj.medicalFitnessCertificate
                //    ,obj.pncExpiryDate, obj.generalNursingPassingDate, obj.midWiferyPassingDate, obj.genericBSNPassingDate,obj.meritalStatus);
                //msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message ApplicantInfoDualNationalAddUpdate(ApplicantInfoDualNational obj)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantAddUpdateInfoDualNational(0, 0, obj.applicantId, obj.countryId, obj.embassyCertificate, obj.languageCertificate
                    , obj.policeCertificate, obj.affidavitCertificate);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public ApplicantInfo GetApplicantInfo(int inductionId, int phaseId, int applicantId)
        {
            DataTable dt = new DataTable();
            ApplicantInfo obj = new ApplicantInfo();
            try
            {
                string query = "select a.*, b.*, r.name as disName from tblApplicant a left join tblApplicantInfo b on b.applicantId = a.applicantId left join tblRegion r on b.districtId = r.regionId  ";
                query += "where a.applicantId = " + applicantId + "";
                SqlConnection con = new SqlConnection();
                Message msg = new Message();
                SqlCommand cmd = new SqlCommand(query);
                try
                {
                    con = new SqlConnection(PrpDbConnectADO.Conn);
                    con.Open();
                    cmd.Connection = con;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        obj.applicantDetailId = Convert.ToInt32(dr[15]);
                        obj.inductionId = Convert.ToInt32(dr[16]);
                        obj.phaseId = Convert.ToInt32(dr[17]);
                        obj.applicantId = Convert.ToInt32(dr[18]);
                        obj.fatherName = dr[19].ToString();
                        obj.genderId = dr[20].TooInt();
                        obj.disableId = Convert.ToInt32(dr[21]);
                        obj.dob = Convert.ToDateTime(dr[22]);
                        obj.pmdcExpiryDate = Convert.ToDateTime(dr[23]);
                        obj.mbbsPassingDate = Convert.ToDateTime(dr[24]);
                        obj.countryIdDegreePassing = Convert.ToInt32(dr[25]);
                        obj.domicileProvinceId = Convert.ToInt32(dr[26]);
                        obj.domicileDistrictId = Convert.ToInt32(dr[27]);
                        obj.dualNationalityType = Convert.ToInt32(dr[28]);
                        obj.countryId = Convert.ToInt32(dr[29]);
                        obj.districtId = Convert.ToInt32(dr[30]);
                        obj.districtName = dr["disName"].ToString();
                        obj.address = dr[32].ToString();
                        obj.cnicNo = dr[33].ToString();
                        obj.cnicExpiryDate = Convert.ToDateTime(dr[34]);
                        obj.cnicPicFront = dr[35].ToString();
                        obj.cnicPicBack = dr[36].ToString();
                        obj.domicilePicFront = dr[37].ToString();
                        obj.domicilePicBack = dr[38].ToString();
                        obj.disableImage = dr[39].ToString();
                        obj.pncPicFront = dr[40].ToString();
                        obj.pncPicBack = dr[41].ToString();
                        obj.medicalFitnessCertificate = dr[42].ToString();
                        obj.pncExpiryDate = Convert.ToDateTime(dr[43]);
                        obj.joiningDate = Convert.ToDateTime(dr["joiningDate"]);
                        obj.generalNursingPassingDate = Convert.ToDateTime(dr[44]);
                        obj.midWiferyPassingDate = Convert.ToDateTime(dr[45]);
                        obj.genericBSNPassingDate = Convert.ToDateTime(dr[46]);
                        obj.meritalStatus = Convert.ToInt32(dr[47]);
                        obj.religionId = Convert.ToInt32(dr["religionId"]);
                        obj.nocCertificate = dr["nocCertificate"].TooString();
                        obj.nicCertificate = dr["nicCertificate"].TooString();
                        obj.recommendationLetter = dr["recommendationLetter"].TooString();
                        obj.recommendationLetter2 = dr["recommendationLetter2"].TooString();
                        obj.regularizationOrder = dr["regularizationOrder"].TooString();
                        obj.levelId = dr["postingInstituteLevelId"].TooInt();
                        obj.levelTypeId = dr["postingInstituteTypeId"].TooInt();
                        obj.inductionId = dr["postingInstituteId"].TooInt();
                        obj.instituteName = dr["currentPostingAddress"].TooString();
                    }
                    else
                    {
                        msg.status = false;
                    }
                }
                catch (Exception ex)
                {
                    msg.status = false;
                    msg.msg = ex.Message;
                }
                finally
                {
                    con.Close();
                }
                //var dbt = db.tblApplicants.FirstOrDefault(x => x.applicantId == applicantId);



            }
            catch (Exception ex)
            {
                obj = new ApplicantInfo();
            }
            return obj;
        }

        public ApplicantInfoDualNational GetApplicantInfoDualNational(int inductionId, int phaseId, int applicantId)
        {
            ApplicantInfoDualNational obj = new ApplicantInfoDualNational();
            try
            {
                var dbt = db.tvwApplicantInfoDualNationals.FirstOrDefault(x => x.applicantId == applicantId);
                if (dbt != null && dbt.dualInfoId > 0)
                    obj = MapApplicantInfo.ToEntity(dbt);
                else obj = new ApplicantInfoDualNational();
            }
            catch (Exception)
            {
                obj = new ApplicantInfoDualNational();
            }
            return obj;
        }

        public ApplicantInfo GetApplicantInfoDetail(int inductionId, int phaseId, int applicantId)
        {
            DataTable dt = new DataTable();
            ApplicantInfo obj = new ApplicantInfo();
            try
            {
                string query = "select a.*, b.*, r.name as disName, rdom.name as domDisName from tblApplicant a left join (select * from tblApplicantInfo union select * from tblApplicantInfoMigrants) b on b.applicantId = a.applicantId left join tblRegion r on b.districtId = r.regionId left join tblRegion rdom on b.domicileDistrictId = rdom.regionId  ";
                query += "where a.applicantId = " + applicantId + "";
                SqlConnection con = new SqlConnection();
                Message msg = new Message();
                SqlCommand cmd = new SqlCommand(query);
                try
                {
                    con = new SqlConnection(PrpDbConnectADO.Conn);
                    con.Open();
                    cmd.Connection = con;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];
                        obj.applicantDetailId = Convert.ToInt32(dr[15]);
                        obj.inductionId = Convert.ToInt32(dr[16]);
                        obj.phaseId = Convert.ToInt32(dr[17]);
                        obj.applicantId = Convert.ToInt32(dr[18]);
                        obj.fatherName = dr[19].ToString();
                        obj.genderId = dr[20].TooInt();
                        obj.disableId = Convert.ToInt32(dr[21]);
                        obj.dob = Convert.ToDateTime(dr[22]);
                        obj.pmdcExpiryDate = Convert.ToDateTime(dr[23]);
                        obj.mbbsPassingDate = Convert.ToDateTime(dr[24]);
                        obj.countryIdDegreePassing = Convert.ToInt32(dr[25]);
                        obj.domicileProvinceId = Convert.ToInt32(dr[26]);
                        obj.domicileDistrictId = Convert.ToInt32(dr[27]);
                        obj.dualNationalityType = Convert.ToInt32(dr[28]);
                        obj.countryId = Convert.ToInt32(dr[29]);
                        obj.districtId = Convert.ToInt32(dr[30]);
                        obj.districtName = dr["disName"].ToString();
                        obj.address = dr[32].ToString();
                        obj.cnicNo = dr[33].ToString();
                        obj.cnicExpiryDate = Convert.ToDateTime(dr[34]);
                        obj.cnicPicFront = dr[35].ToString();
                        obj.cnicPicBack = dr[36].ToString();
                        obj.domicilePicFront = dr[37].ToString();
                        obj.domicilePicBack = dr[38].ToString();
                        obj.disableImage = dr[39].ToString();
                        obj.pncPicFront = dr[40].ToString();
                        obj.pncPicBack = dr[41].ToString();
                        obj.medicalFitnessCertificate = dr[42].ToString();
                        obj.pncExpiryDate = Convert.ToDateTime(dr[43]);
                        obj.joiningDate = Convert.ToDateTime(dr["joiningDate"]);
                        obj.generalNursingPassingDate = Convert.ToDateTime(dr[44]);
                        obj.midWiferyPassingDate = Convert.ToDateTime(dr[45]);
                        obj.genericBSNPassingDate = Convert.ToDateTime(dr[46]);
                        obj.meritalStatus = Convert.ToInt32(dr[47]);
                        obj.nocCertificate = dr["nocCertificate"].TooString();
                        obj.nicCertificate = dr["nicCertificate"].TooString();
                        obj.recommendationLetter = dr["recommendationLetter"].TooString();
                        obj.recommendationLetter2 = dr["recommendationLetter2"].TooString();
                        obj.regularizationOrder = dr["regularizationOrder"].TooString();
                        obj.domicileDistrict = dr["domDisName"].TooString();
                    }
                    else
                    {
                        msg.status = false;
                    }
                }
                catch (Exception ex)
                {
                    msg.status = false;
                    msg.msg = ex.Message;
                }
                finally
                {
                    con.Close();
                }
                //var dbt = db.tblApplicants.FirstOrDefault(x => x.applicantId == applicantId);



            }
            catch (Exception ex)
            {
                obj = new ApplicantInfo();
            }
            return obj;
        }

        #endregion

        #region Education


        public Message ApplicantDegreeMarksMakeAccurate(int inductionId, int phaseId, int applicantId)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantDegreeMarksMakeAccurate(applicantId);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }


        public Message ApplicantDegreeAddUpdate(ApplicantDegree obj)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantDegreeAddUpdate(obj.applicantDegreeDetailId, obj.inductionId, obj.phaseId, obj.applicantId, obj.graduateTypeId
                    , obj.degreeTypeId, obj.degreeYear, obj.provinceId, obj.instituteTypeId, obj.instituteId, obj.instituteName
                    , obj.totalMarks, obj.obtainMarks, obj.imageDegree, obj.imageDegreeForeignFront, obj.imageDegreeForeignBack
                    , obj.imageDegreeMatric, obj.imageCertificate, obj.typeIds, obj.fcpsExemptionStatus, obj.fcpsExemptionCertificate);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message ApplicantDegreeMarksAddUpdate(int inductionId, int phaseId, List<ApplicantDegreeMark> listMarks)
        {
            int applicantId = 0;
            Message msg = new Message();
            try
            {
                foreach (ApplicantDegreeMark objMark in listMarks)
                {
                    try
                    {
                        applicantId = objMark.applicantId;
                        db.spApplicantDegreeMarksAddUpdate(objMark.degreeMarksId, inductionId, phaseId, objMark.applicantId, objMark.graduateTypeId
                            , objMark.year
                            , objMark.totalMarks, objMark.obtainMarks, objMark.attempt, objMark.imageDMC);
                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.msg = ex.Message;
                    }
                }
                msg.status = true;


            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public ApplicantDegree GetApplicantDegree(int inductionId, int phaseId, int applicantId)
        {
            ApplicantDegree obj = new ApplicantDegree();
            try
            {
                var dbt = db.tvwApplicantDegrees.FirstOrDefault(x => x.applicantId == applicantId);
                obj = MapApplicantDegree.ToEntity(dbt);
            }
            catch (Exception)
            {
                obj = new ApplicantDegree();
            }
            return obj;
        }

        public ApplicantDegree GetApplicantDegreeDetail(int inductionId, int phaseId, int applicantId)
        {


            ApplicantDegree obj = new ApplicantDegree();
            try
            {
                if (inductionId == 0)
                {
                    try
                    {
                        ApplicantDegreeMarksMakeAccurate(inductionId, phaseId, applicantId);
                    }
                    catch (Exception)
                    {
                    }
                    var dbt = db.tvwApplicantDegrees.FirstOrDefault(x => x.applicantId == applicantId);
                    obj = MapApplicantDegree.ToEntity(dbt);
                }
                else
                {
                    var dbt = db.tvwfApplicantDegrees.FirstOrDefault(x => x.applicantId == applicantId && x.inductionId == inductionId);
                    obj = MapApplicantDegree.ToEntity(dbt);
                }
            }
            catch (Exception)
            {
                obj = new ApplicantDegree();
            }
            return obj;
        }

        public List<ApplicantDegreeMark> GetApplicantDegreeMarkList(int inductionId, int phaseId, int applicantId)
        {
            inductionId = 12;
            List<ApplicantDegreeMark> list = new List<ApplicantDegreeMark>();
            try
            {
                if (inductionId == 0)
                {
                    var listt = db.tblApplicantDegreeMarks.Where(x => x.applicantId == applicantId).ToList();
                    list = MapApplicantDegreeMark.ToEntityList(listt);
                }
                else
                {
                    DataTable dt = new DataTable();
                    string query = "select nadd.*, nadt.degreeTypeName " +
                        "from tblNursingApplicantDegreeData nadd " +
                        "inner join tblNursingApplicantDegreeType nadt on nadd.degreeTypeID = nadt.degreeTypeID " +
                        " where nadd.applicantId = " + applicantId + " ";
                    SqlConnection con = new SqlConnection();
                    Message msg = new Message();
                    SqlCommand cmd = new SqlCommand(query);
                    try
                    {
                        con = new SqlConnection(PrpDbConnectADO.Conn);
                        con.Open();
                        cmd.Connection = con;
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                ApplicantDegreeMark listt = new ApplicantDegreeMark();
                                listt.degreeInstitute = dr["degreeInstitute"].ToString();
                                listt.degreeName = dr["degreeTypeName"].ToString();
                                listt.totalMarks = Convert.ToDouble(dr["totalMarks"]);
                                listt.obtainMarks = Convert.ToDouble(dr["obtainedMarks"]);
                                listt.imageDMC = dr["degreePicFront"].ToString();
                                list.Add(listt);
                                msg.status = true;
                            }
                        }
                        else
                        {
                            msg.status = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.msg = ex.Message;
                    }
                    finally
                    {
                        con.Close();
                    }


                }

            }
            catch (Exception)
            {
                list = new List<ApplicantDegreeMark>();
            }
            return list;
        }

        public Message ApplicantCertificateAddUpdate(List<ApplicantCertificate> listCertificate)
        {
            Message msg = new Message();
            try
            {
                foreach (ApplicantCertificate obj in listCertificate)
                {
                    try
                    {
                        db.spApplicantCertificateAddUpdate(obj.applicantCertificateTypeId, obj.inductionId, obj.phaseId
                            , obj.applicantId, obj.typeId
                           , obj.disciplineId, obj.obtainMarks, obj.totalMarks, obj.passingDate, obj.imageCertificate);
                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.msg = ex.Message;
                    }
                }
                msg.status = true;


            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public List<ApplicantCertificate> GetApplicantCertificateList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantCertificate> list = new List<ApplicantCertificate>();
            try
            {
                var listt = db.tblApplicantCertificates.Where(x => x.applicantId == applicantId).ToList();
                list = MapApplicantCertificate.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantCertificate>();
            }
            return list;
        }


        public List<ApplicantCertificate> GetApplicantCertificateListDetail(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantCertificate> list = new List<ApplicantCertificate>();
            try
            {
                if (inductionId == 0)
                {
                    var listt = db.tvwApplicantCertificates.Where(x => x.applicantId == applicantId).ToList();
                    list = MapApplicantCertificate.ToEntityList(listt);
                }
                else
                {
                    var listt = db.tvwfApplicantCertificates.Where(x => x.applicantId == applicantId && x.inductionId == inductionId).ToList();
                    list = MapApplicantCertificate.ToEntityList(listt);

                }
            }
            catch (Exception)
            {
                list = new List<ApplicantCertificate>();
            }
            return list;
        }
        #endregion

        #region HouseJob

        public List<ApplicantHouseJob> GetApplicantHouseJobList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantHouseJob> list = new List<ApplicantHouseJob>();
            try
            {
                var listt = db.tvwApplicantHouseJobs.Where(x => x.applicantId == applicantId).ToList();
                list = MapApplicantHouseJob.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantHouseJob>();
            }
            return list;
        }

        public List<ApplicantHouseJob> GetApplicantHouseJobListDetail(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantHouseJob> list = new List<ApplicantHouseJob>();
            try
            {
                var listt = db.tvwApplicantHouseJobs.Where(x => x.applicantId == applicantId).ToList();
                list = MapApplicantHouseJob.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantHouseJob>();
            }
            return list;
        }



        public Message ApplicantHouseJobDeleteSingle(int houseJodId)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantHouseJobDeleteByApplicant(houseJodId);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }


        public Message ApplicantHouseJobAddUpdate(ApplicantHouseJob obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantHouseJobAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@houseJodId", obj.houseJodId);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@provinceId", obj.provinceId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@isSame", obj.isSame);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@hospital", obj.hospital);
            cmd.Parameters.AddWithValue("@startDate", obj.startDate);
            cmd.Parameters.AddWithValue("@endDate", obj.endDate);
            cmd.Parameters.AddWithValue("@image", obj.image);

            return PrpDbADO.FillDataTableMessage(cmd);
        }

        #endregion

        #region Exprience

        public List<ApplicantExperience> GetApplicantExperienceList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantExperience> list = new List<ApplicantExperience>();
            try
            {
                var listt = db.tblApplicantExperiences.Where(x => x.applicantId == applicantId).ToList();
                list = MapApplicantExperience.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantExperience>();
            }
            return list;
        }

        public List<ApplicantExperience> GetApplicantExperienceListDetail(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantExperience> list = new List<ApplicantExperience>();
            try
            {
                inductionId = 12;
                DataTable dt = new DataTable();
                if (inductionId == 0)
                {
                    var listt = db.tvwApplicantExperiences.Where(x => x.applicantId == applicantId).ToList();
                    list = MapApplicantExperience.ToEntityList(listt);
                }
                else
                {
                    string query = "select (select name from tblRegion where regionId = ae.provinceId) as pName, " +
                    "(select name from tblConstant where constantId = ae.levelId) as levelName, h.name, " +
                    "ae.startDated, ae.endDate, ae.noOfMonth, ae.noOfDays, ae.imageExperience from tblApplicantExperience ae " +
                    "inner join tblHospital h on ae.instituteId = h.hospitalId " +
                    "inner join tblRegion r on h.regionId = r.regionId " +
                    "where ae.applicantId = " + applicantId + " ";
                    SqlConnection con = new SqlConnection();
                    Message msg = new Message();
                    SqlCommand cmd = new SqlCommand(query);
                    try
                    {
                        con = new SqlConnection(PrpDbConnectADO.Conn);
                        con.Open();
                        cmd.Connection = con;
                        SqlDataAdapter sda = new SqlDataAdapter(cmd);
                        sda.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt.Rows)
                            {
                                ApplicantExperience listtt = new ApplicantExperience();
                                listtt.provinceName = dr["pName"].ToString();
                                listtt.typeName = dr["levelName"].ToString();
                                listtt.instituteName = dr["name"].ToString();
                                listtt.startDated = Convert.ToDateTime(dr["startDated"]);
                                listtt.endDate = Convert.ToDateTime(dr["endDate"]);
                                listtt.noOfMonth = Convert.ToInt32(dr["noOfMonth"]);
                                listtt.noOfDays = Convert.ToInt32(dr["noOfDays"]);
                                listtt.imageExperience = dr["imageExperience"].ToString();
                                list.Add(listtt);
                                msg.status = true;
                            }
                        }
                        else
                        {
                            msg.status = false;
                        }
                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.msg = ex.Message;
                    }
                    finally
                    {
                        con.Close();
                    }
                    //var listt = db.tvwfApplicantExperiences.Where(x => x.applicantId == applicantId && x.inductionId == inductionId).ToList();
                    //list = MapApplicantExperience.ToEntityList(listt);
                }
            }
            catch (Exception)
            {
                list = new List<ApplicantExperience>();
            }
            return list;
        }

        public List<ApplicantExperience> GetApplicantExperienceListByApplicant(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantExperience> list = new List<ApplicantExperience>();
            try
            {
                var listt = db.spApplicantExperienceByApplicant(inductionId, phaseId, applicantId).ToList();
                list = MapApplicantExperience.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantExperience>();
            }
            return list;
        }

        public Message ApplicantExperienceDeleteSingle(int inductionId, int phaseId, int applicantExperienceId)
        {
            Message msg = new Message();
            try
            {
                //string query = "select count(*) from tblApplicant where pncNo like '" + obj + "'";
                //int count = 0;
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand("spApplicantExperienceDeleteByApplicant");
                try
                {
                    con = new SqlConnection(PrpDbConnectADO.Conn);
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@applicantExperienceId", applicantExperienceId);
                    cmd.ExecuteNonQuery();
                    msg.status = true;
                }
                catch (Exception ex)
                {
                    msg.status = false;
                    msg.msg = ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
            //catch (Exception ex) { }
            //try
            //{
            //    var objt = db.spApplicantExperienceAddUpdate(obj.applicantExperienceId, obj.inductionId, obj.phaseId, obj.applicantId, obj.levelId
            //          , obj.levelTypeId, obj.instituteName, obj.instituteId, obj.provinceId, obj.typeId
            //          , obj.startDated, obj.endDate
            //          , obj.isCurrent, obj.imageExperience).FirstOrDefault();
            //    msg = MapApplicantExperience.ToEntity(objt);
            //}
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            //try
            //{
            //    db.spApplicantExperienceDeleteByApplicant(applicantExperienceId);
            //    msg.status = true;
            //}
            //catch (Exception ex)
            //{
            //    msg.status = false;
            //    msg.msg = ex.Message;
            //}
            return msg;
        }


        public Message ApplicantExperienceAddUpdate(ApplicantExperience obj)
        {
            Message msg = new Message();
            try
            {
                //string query = "select count(*) from tblApplicant where pncNo like '" + obj + "'";
                //int count = 0;
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand("spApplicantExperienceAddUpdate");
                try
                {
                    con = new SqlConnection(PrpDbConnectADO.Conn);
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@applicantExperienceId", obj.applicantExperienceId);
                    cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
                    cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
                    cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
                    cmd.Parameters.AddWithValue("@levelId", obj.levelId);
                    cmd.Parameters.AddWithValue("@levelTypeId", obj.levelTypeId);
                    cmd.Parameters.AddWithValue("@instituteName", obj.instituteName);
                    cmd.Parameters.AddWithValue("@instituteId", obj.instituteId);
                    cmd.Parameters.AddWithValue("@provinceId", obj.provinceId);
                    cmd.Parameters.AddWithValue("@typeId", obj.typeId);
                    cmd.Parameters.AddWithValue("@startDated", obj.startDated);
                    cmd.Parameters.AddWithValue("@endDate", obj.endDate);
                    cmd.Parameters.AddWithValue("@isCurrent", obj.isCurrent);
                    cmd.Parameters.AddWithValue("@imageExperience", obj.imageExperience);
                    DataTable table = new DataTable();
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        da.Fill(table);
                        msg.msg = table.Rows[0][2].ToString();
                        msg.status = Convert.ToBoolean(table.Rows[0][1]);
                        msg.id = Convert.ToInt32(table.Rows[0][0]);
                    }
                    //cmd.ExecuteNonQuery();
                    //msg.status = true;
                }
                catch (Exception ex)
                {
                    msg.status = false;
                    msg.msg = ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
            //catch (Exception ex) { }
            //try
            //{
            //    var objt = db.spApplicantExperienceAddUpdate(obj.applicantExperienceId, obj.inductionId, obj.phaseId, obj.applicantId, obj.levelId
            //          , obj.levelTypeId, obj.instituteName, obj.instituteId, obj.provinceId, obj.typeId
            //          , obj.startDated, obj.endDate
            //          , obj.isCurrent, obj.imageExperience).FirstOrDefault();
            //    msg = MapApplicantExperience.ToEntity(objt);
            //}
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        #endregion

        #region Distinction

        public List<ApplicantDistinction> GetApplicantDistinctionList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantDistinction> list = new List<ApplicantDistinction>();
            try
            {
                if (inductionId == 0)
                {
                    var listt = db.tblApplicantDistinctions.Where(x => x.applicantId == applicantId).ToList();
                    list = MapApplicantDistinction.ToEntityList(listt);
                }
                else
                {
                    var listt = db.tblApplicantDistinctionFinals.Where(x => x.applicantId == applicantId && x.inductionId == inductionId).ToList();
                    list = MapApplicantDistinction.ToEntityList(listt);
                }
            }
            catch (Exception)
            {
                list = new List<ApplicantDistinction>();
            }
            return list;
        }

        public Message ApplicantDistinctionDeleteSingle(int inductionId, int phaseId, int applicantDistinctionId)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantDistinctionDeleteByApplicant(applicantDistinctionId, inductionId, phaseId);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message ApplicantDistinctionAddUpdate(ApplicantDistinction obj)
        {


            Message msg = new Message();
            try
            {

                var objt = db.spApplicantDistinctionAddUpdate(obj.applicantDistinctionId, obj.inductionId, obj.phaseId
                    , obj.applicantId, obj.subject, obj.year, obj.imageDistinction).FirstOrDefault();
                msg = MapApplicantDistinction.ToEntity(objt);
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message GetApplicantVerifyDistictionCountByApplicant(int inductionId, int phaseId, int applicantId)
        {
            Message msg = new Message();
            try
            {
                //var objt = db.spApplicantVerifyDistictionCountByApplicant(applicantId).FirstOrDefault();
                //if (objt == null)
                //{
                //    msg.id = 0;
                //    msg.status = false;
                //}
                //else if (objt != null && objt.id > 0)
                //{
                //    msg.id = objt.id.TooInt();
                //    msg.status = true;
                //}
                //else
                //{
                //    msg.id = 0;
                //    msg.status = false;
                //}
            }
            catch (Exception ex)
            {
                msg.id = 0;
                msg.status = false;
            }
            return msg;
        }

        #endregion

        #region Research Paper

        public List<ApplicantResearchPaper> GetApplicantResearchPaperList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantResearchPaper> list = new List<ApplicantResearchPaper>();
            try
            {
                var listt = db.spApplicantResearchPaperByApplicant(inductionId, phaseId, applicantId).ToList();
                list = MapApplicantResearchPaper.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantResearchPaper>();
            }
            return list;
        }

        public List<ApplicantResearchPaper> GetApplicantResearchPaperListDetail(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantResearchPaper> list = new List<ApplicantResearchPaper>();
            try
            {
                if (inductionId == 0)
                {
                    var listt = db.spApplicantResearchPaperByApplicant(inductionId, phaseId, applicantId).ToList();
                    list = MapApplicantResearchPaper.ToEntityList(listt);
                }
                else
                {
                    var listt = db.tblApplicantResearchPaperFinals.Where(x => x.applicantId == applicantId && x.inductionId == inductionId).ToList();
                    list = MapApplicantResearchPaper.ToEntityList(listt);
                }
            }
            catch (Exception)
            {
                list = new List<ApplicantResearchPaper>();
            }
            return list;
        }

        public Message ApplicantResearchPaperDeleteSingle(int inductionId, int phaseId, int applicantResearchId)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantResearchPaperDeleteByApplicant(applicantResearchId);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message ApplicantResearchPaperAddUpdate(ApplicantResearchPaper obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantResearchPaperAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@applicantResearchId", obj.applicantResearchId);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@name", obj.name);
            cmd.Parameters.AddWithValue("@authorId", obj.authorId);
            cmd.Parameters.AddWithValue("@publishStatusId", obj.publishStatusId);
            cmd.Parameters.AddWithValue("@link", obj.link);
            cmd.Parameters.AddWithValue("@webLink", obj.webLink);
            cmd.Parameters.AddWithValue("@imageLetter", obj.imageLetter);
            cmd.Parameters.AddWithValue("@researchJournalId", obj.researchJournalId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }


        #endregion

        #region Applicant Specility

        public DataSet ApplicantSpecilityGet(ApplicantSpecility obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantSpecilityByApplicant]"
            };
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);

            return PrpDbADO.FillDataSet(cmd);
        }

        public Message ApplicantSpecilityDeleteByType(ApplicantSpecility obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantSpecilityDeleteByType]"
            };
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);

            return PrpDbADO.FillDataTableMessage(cmd);
        }

        //spApplicantSpecilityDeleteByType

        public List<ApplicantSpecility> GetApplicantSpecilityList(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantSpecility> list = new List<ApplicantSpecility>();
            try
            {

                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantSpecilityByApplicant]"
                };
                cmd.Parameters.AddWithValue("@inductionId", inductionId);
                cmd.Parameters.AddWithValue("@phaseId", phaseId);
                cmd.Parameters.AddWithValue("@applicantId", applicantId);

                DataTable dt = PrpDbADO.FillDataTable(cmd);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        ApplicantSpecility obj = new ApplicantSpecility();
                        obj.applicantSpecilityId = dr["applicantSpecilityId"].TooInt();
                        obj.inductionId = dr["inductionId"].TooInt();
                        obj.phaseId = dr["phaseId"].TooInt();
                        obj.applicantId = dr["applicantId"].TooInt();
                        obj.preferenceNo = dr["preferenceNo"].TooInt();
                        obj.typeId = dr["typeId"].TooInt();
                        obj.specialityId = dr["specialityId"].TooInt();
                        obj.hospitalId = dr["hospitalId"].TooInt();
                        obj.specialityJobId = dr["specialityJobId"].TooInt();
                        obj.dated = Convert.ToDateTime(dr["dated"]);
                        obj.typeName = dr["typeName"].TooString();
                        obj.specialityName = dr["specialityName"].TooString();
                        obj.hospitalName = dr["hospitalName"].TooString();
                        list.Add(obj);
                    }
                }


                //var dbt = db.spApplicationStatusGet(inductionId, phaseId, applicantId, statusTypeId);
                //obj = MapApplicant.ToEntity(dbt);
            }
            catch (Exception)
            {
                //list = new List<ApplicationStatus>();
            }
            return list;
            //try
            //{
            //    var listt = db.spApplicantSpecilityByApplicant(inductionId, phaseId, applicantId).ToList();
            //    list = MapApplicantSpecility.ToEntityList(listt);
            //}
            //catch (Exception)
            //{
            //    list = new List<ApplicantSpecility>();
            //}
            //return list;
        }


        public List<ApplicantSpecility> GetApplicantSpecilityListWithMarks(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantSpecility> list = new List<ApplicantSpecility>();
            try
            {
                inductionId = 12;
                if (inductionId == 0)
                {
                    var listt = db.spApplicantSpecilityWithMarksByApplicant(applicantId, inductionId, phaseId).ToList();
                    list = MapApplicantSpecility.ToEntityList(listt);
                }
                else
                {
                    var listt = db.spApplicantSpecilityWithMarksByApplicantFinal(applicantId, inductionId, phaseId).ToList();
                    list = MapApplicantSpecility.ToEntityList(listt);
                }
            }
            catch (Exception ex)
            {
                list = new List<ApplicantSpecility>();
            }
            return list;
        }

        public List<ApplicantSpecility> GetApplicantSpecilityListDetail(int inductionId, int phaseId, int applicantId)
        {
            List<ApplicantSpecility> list = new List<ApplicantSpecility>();
            try
            {
                var listt = db.tvwApplicantSpecilities.Where(x => x.applicantId == applicantId).ToList();
                list = MapApplicantSpecility.ToEntityList(listt);
            }
            catch (Exception)
            {
                list = new List<ApplicantSpecility>();
            }
            return list;
        }


        public Message ApplicantSpecilityDeleteSingle(int inductionId, int phaseId, int applicantSpecilityId)
        {
            Message msg = new Message();
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand("spApplicantSpecilityDeleteByApplicant");
                try
                {
                    con = new SqlConnection(PrpDbConnectADO.Conn);
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@applicantSpecilityId", applicantSpecilityId);
                    cmd.ExecuteNonQuery();
                    msg.status = true;
                }
                catch (Exception ex)
                {
                    msg.status = false;
                    msg.msg = ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message ApplicantSpecilityCheckPreferenceNo(ApplicantSpecility obj)
        {
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantSpecilityCheckPreferenceNo]"
                };
                cmd.Parameters.AddWithValue("@applicantSpecilityId", obj.applicantSpecilityId);
                cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
                cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
                cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
                cmd.Parameters.AddWithValue("@preferenceNo", obj.preferenceNo);
                cmd.Parameters.AddWithValue("@typeId", obj.typeId);
                cmd.Parameters.AddWithValue("@specialityId", obj.specialityId);
                DataTable dt = PrpDbADO.FillDataTable(cmd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    msg.message = dr["message"].TooString();
                    msg.status = dr["status"].TooBoolean();
                }
            }
            catch (Exception)
            {
                msg.message = "System Error";
                msg.status = false;

            }

            //try
            //{
            //    var item = db.spApplicantSpecilityCheckPreferenceNo(obj.applicantSpecilityId, obj.inductionId, obj.phaseId, obj.applicantId, obj.preferenceNo
            //         , obj.typeId, obj.specialityId).FirstOrDefault();

            //    msg = MapApplicant.ToEntity(item);
            //}
            //catch (Exception ex)
            //{
            //    msg.status = false;
            //    msg.msg = ex.Message;
            //}
            return msg;
        }

        public Message ApplicantSpecilityAddUpdate(ApplicantSpecility obj)
        {
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantSpecilityAddUpdate]"
                };
                cmd.Parameters.AddWithValue("@applicantSpecilityId", obj.applicantSpecilityId);
                cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
                cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
                cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
                cmd.Parameters.AddWithValue("@preferenceNo", obj.preferenceNo);
                cmd.Parameters.AddWithValue("@typeId", obj.typeId);
                cmd.Parameters.AddWithValue("@specialityId", obj.specialityId);
                cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
                DataTable dt = PrpDbADO.FillDataTable(cmd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    msg.message = dr["message"].TooString();
                    msg.status = dr["status"].TooBoolean();
                }
            }
            catch (Exception)
            {
                msg.message = "System Error";
                msg.status = false;

            }
            //try
            //{
            //    var item = db.spApplicantSpecilityAddUpdate(obj.applicantSpecilityId, obj.inductionId, obj.phaseId, obj.applicantId, obj.preferenceNo
            //         , obj.typeId, obj.specialityId, obj.hospitalId).FirstOrDefault();

            //    msg = MapApplicant.ToEntity(item);
            //}
            //catch (Exception ex)
            //{
            //    msg.status = false;
            //    msg.msg = ex.Message;
            //}
            return msg;
        }

        public Message ApplicantSpecialityDuplicateByType(ApplicantSpecility obj)
        {
            Message msg = new Message();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantSpecialityDuplicateByType]"
                };
                cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
                cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
                cmd.Parameters.AddWithValue("@typeId", obj.typeId);

                DataTable dt = PrpDbADO.FillDataTable(cmd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    msg.message = dr["message"].TooString();
                    msg.status = dr["status"].TooBoolean();
                }
            }
            catch (Exception)
            {
                msg.message = "System Error";
                msg.status = false;
            }
            return msg;
        }
        #endregion

        #region Applicant Voucher

        public DataSet ApplicantVoucherInfoGetAll(ApplicantVoucher obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantVoucherInfoGetAll]"
            };
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);

            return PrpDbADO.FillDataSet(cmd);
        }

        public ApplicantVoucher GetApplicantVoucher(int inductionId, int phaseId, int applicantId)
        {
            inductionId = 15;
            ApplicantVoucher obj = new ApplicantVoucher();
            SqlConnection con = new SqlConnection();
            try
            {
                //if (inductionId == 0)
                //{
                //    var dbt = db.tblApplicantVouchers.FirstOrDefault(x => x.applicantId == applicantId);
                //    if (dbt != null && dbt.applicantVoucherId > 0)
                //        obj = MapVoucher.ToEntity(dbt);
                //    else obj = new ApplicantVoucher();
                //}
                //else
                //{
                //    var dbt = db.tblApplicantVoucherFinals.FirstOrDefault(x => x.inductionId == inductionId && x.applicantId == applicantId);
                //    if (dbt != null && dbt.applicantVoucherId > 0)
                //        obj = MapVoucher.ToEntity(dbt);
                //    else obj = new ApplicantVoucher();

                //}

                con = new SqlConnection(PrpDbConnectADO.Conn);
                //string query = "select a.[applicantVoucherId],a.[inductionId],a.[phaseId],a.[applicantId],a.[amount], " +
                //    " a.[branchCode],a.[voucherImage],a.[ibn],a.[accountNo],a.[accountTitle],a.[submittedDate],a.[dated],a.[testingCenterID], h.name" +
                //    " from tblApplicantVoucher a inner join (select name,hospitalId from tblHospital where levelId = 25) h on a.testingCenterID = h.hospitalId "+
                //    " where a.inductionId = " + inductionId + " and a.applicantId = " + applicantId + "";
                string query = "select a.[applicantVoucherId],a.[inductionId],a.[phaseId],a.[applicantId],a.[amount],a.[branchCode],a.[voucherImage],a.[ibn],a.[accountNo],a.[accountTitle],a.[submittedDate],a.[dated],a.[testingCenterID] from tblApplicantVoucher a  where a.inductionId = " + inductionId + " and a.applicantId = " + applicantId + " ";
                SqlCommand cmd = new SqlCommand(query, con);
                DataTable dt = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                con.Open();
                sda.Fill(dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    obj.applicantVoucherId = dr[0].TooInt();
                    obj.inductionId = dr[1].TooInt();
                    obj.phaseId = dr[2].TooInt();
                    obj.applicantId = dr[3].TooInt();
                    obj.amount = dr[4].TooInt();
                    obj.branchCode = dr[5].TooString();
                    obj.voucherImage = dr[6].TooString();
                    obj.ibn = dr[7].TooString();
                    obj.accountNo = dr[8].TooString();
                    obj.accountTitle = dr[9].TooString();
                    obj.submittedDate = Convert.ToDateTime(dr[10]);
                    obj.dated = Convert.ToDateTime(dr[11]);
                    obj.testingCenter = dr[12].TooInt();
                    //obj.testingCenterName = dr["name"].TooString();
                }
            }
            catch (Exception ex)
            {
                obj = new ApplicantVoucher();
            }
            finally
            {
                con.Close();
            }
            return obj;
        }


        public Message ApplicantVoucherAddUpdate(ApplicantVoucher obj)
        {
            //Message msg = new Message();
            Message msg = new Message();
            try
            {
                //string query = "select count(*) from tblApplicant where pncNo like '" + obj + "'";
                //int count = 0;
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand("spApplicantVoucherAddUpdate");
                try
                {
                    con = new SqlConnection(PrpDbConnectADO.Conn);
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
                    cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
                    cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
                    cmd.Parameters.AddWithValue("@amount", obj.amount);
                    cmd.Parameters.AddWithValue("@branchCode", obj.branchCode);
                    cmd.Parameters.AddWithValue("@voucherImage", obj.voucherImage);
                    cmd.Parameters.AddWithValue("@ibn", obj.ibn);
                    cmd.Parameters.AddWithValue("@accountNo", obj.accountNo);
                    cmd.Parameters.AddWithValue("@accountTitle", obj.accountTitle);
                    cmd.Parameters.AddWithValue("@submittedDate", obj.submittedDate);
                    cmd.Parameters.AddWithValue("@testingCenter", obj.testingCenter);
                    cmd.ExecuteNonQuery();
                    msg.status = true;
                }
                catch (Exception ex)
                {
                    msg.status = false;
                    msg.msg = ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public MessageAPI ApplicantVoucherBankAddUpdate(ApplicantVoucherBank obj)
        {
            MessageAPI msg = new MessageAPI();
            SqlConnection con = new SqlConnection();
            try
            {
                con = new SqlConnection(PrpDbConnectADO.Conn);
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantVoucherAddUpdateBank]"
                };
                con.Open();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@applicantNo", obj.applicantNo);
                cmd.Parameters.AddWithValue("@amount", obj.amount);
                cmd.Parameters.AddWithValue("@transactionIdBank", obj.transactionIdBank);
                cmd.Parameters.AddWithValue("@statusBank", obj.statusBank);
                cmd.Parameters.AddWithValue("@transactionType", obj.transactionType);
                DataTable dt = PrpDbADO.FillDataTable(cmd);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    msg.message = dr["message"].TooString();
                    msg.status = dr["status"].TooBoolean();
                }
            }
            catch (Exception)
            {
                msg.message = "System Error";
                msg.status = false;

            }
            finally
            {
                con.Close();
            }
            return msg;
        }

        //public Message ApplicantVoucherBankAddUpdate(ApplicantVoucherBank obj)
        //{
        //    Message msg = new Message();
        //    try
        //    {
        //        db.spApplicantVoucherAddUpdateBank(obj.applicantNo, obj.amount, obj.transactionIdBank, obj.statusBank, obj.transactionType);

        //        msg.status = true;
        //        msg.message = "Transaction saved successfully";
        //    }
        //    catch (Exception ex)
        //    {
        //        msg.status = false;
        //        msg.msg = "System error.";
        //    }
        //    return msg;
        //}


        public ApplicantVoucherBank ApplicantVoucherGetByApplicantNo(string applicantNo, int transactionType)
        {
            ApplicantVoucherBank obj = new ApplicantVoucherBank();


            try
            {
                var ss = db.spApplicantVoucherGetByApplicantNo(applicantNo, transactionType).FirstOrDefault();

                obj = MapVoucher.ToEntity(ss);
            }
            catch (Exception ex)
            {
                obj = new ApplicantVoucherBank();
                obj.status = "System Error.";
            }

            return obj;
        }
        #endregion


        #region Admin

        #region Applicant Status

        public List<Applicant> GetApplicantList(ApplicantEntity obj)
        {
            List<Applicant> list = new List<Applicant>();
            try
            {
                var listt = db.spApplicantGetList(obj.accountStatusId, obj.applicationStatusId, obj.facultyId, obj.condition).ToList();
                list = MapApplicant.ToEntityList(listt);
            }
            catch (Exception ex)
            {
                list = new List<Applicant>();
            }
            return list;
        }

        #endregion

        #endregion




    }
}
