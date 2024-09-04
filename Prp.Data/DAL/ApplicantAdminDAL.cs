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
    public class ApplicantAdminDAL : PrpDBConnect
    {
        public DataTable ApplicantSearchSimpleDownload(ApplicantSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantSearchSimpleDownload]"
            };

            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@statusTypeId", obj.statusTypeId);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@search", obj.search.TooString());
            return PrpDbADO.FillDataTable(cmd);
        }
        public DataTable ApplicantSearchSimple(ApplicantSearch obj)
        {
            if (obj.statusTypeId == 73 && obj.statusId == 3)
            {
                obj.statusTypeId = 53;
                obj.statusId = 8;
            }
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantSearchSimple]"
            };

            if (Convert.ToInt32(90) == 3 && obj.inductionId == 12)
            {
                obj.inductionId = 112;
            }
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@statusTypeId", obj.statusTypeId);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@search", obj.search.TooString());
            return PrpDbADO.FillDataTable(cmd);
        }
        public DataTable GetApplicantsCollegeWise(ApplicantSearch obj)
        {
            if (obj.statusTypeId == 73 && obj.statusId == 3)
            {
                obj.statusTypeId = 53;
                obj.statusId = 8;
            }
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGetCollegeWiseList]"
            };

            if (Convert.ToInt32(90) == 3 && obj.inductionId == 12)
            {
                obj.inductionId = 112;
            }
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@statusTypeId", obj.statusTypeId);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@search", obj.search.TooString());
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable SearchPhoneNumberFromCNIC(string searchNumber)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spSearchPhoneNumberFromCNIC]"
            };
            cmd.Parameters.AddWithValue("@cnicNo", searchNumber);
            DataTable dt = PrpDbADO.FillDataTable(cmd);
            return dt;
        }

        public bool UpdatePhoneNumber(int applicantId, string newPhoneNumber)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spUpdatePhoneNumberFromCNIC]"
            };
            cmd.Parameters.AddWithValue("@applicantId", applicantId);
            cmd.Parameters.AddWithValue("@newPhoneNumber", newPhoneNumber);
            return PrpDbADO.ExecuteNonQuery(cmd).status;
        }
        public DataTable getCollegesForInduction(string induction)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGetCollegeForInduction]"
            };

            cmd.Parameters.AddWithValue("@induction", induction);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable getActivitesForInduction(string induction)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGetActivitesForInduction]"
            };

            cmd.Parameters.AddWithValue("@induction", induction);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable getInductionForCollege(int adminId)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spInductionForCollege]"
            };

            cmd.Parameters.AddWithValue("@adminId", adminId);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable ApplicantSearchSimpleToVerify(ApplicantSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantSearchSimpleToVerify]"
            };
            
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@inductionId", 13);
            cmd.Parameters.AddWithValue("@phaseId", 1);
            cmd.Parameters.AddWithValue("@statusTypeId", obj.statusTypeId);
            cmd.Parameters.AddWithValue("@userId", obj.userId);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@search", obj.search.TooString());
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable ApplicantSearchSimpleToAmmendment(ApplicantSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantSearchSimpleToAmmendment]"
            };

            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@inductionId", 13);
            cmd.Parameters.AddWithValue("@phaseId", 1);
            cmd.Parameters.AddWithValue("@statusTypeId", obj.statusTypeId);
            cmd.Parameters.AddWithValue("@userId", obj.userId);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@search", obj.search.TooString());
            return PrpDbADO.FillDataTable(cmd);
        }


        public DataTable ApplicantSearchSimpleVerify(ApplicantSearch obj)
        {
            if (obj.statusTypeId == 73 && obj.statusId == 3)
            {
                obj.statusTypeId = 53;
                obj.statusId = 8;
            }
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantSearchSimpleVerify]"
            };

            if (Convert.ToInt32(90) == 3 && obj.inductionId == 12)
            {
                obj.inductionId = 112;
            }
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@statusTypeId", obj.statusTypeId);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@search", obj.search.TooString());
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable ApplicantSearchSimpleJoin(ApplicantSearch obj)
        {
            if (obj.statusTypeId == 73 && obj.statusId == 3)
            {
                obj.statusTypeId = 53;
                obj.statusId = 8;
            }
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantSearchSimpleJoin]"
            };

            if (Convert.ToInt32(90) == 3 && obj.inductionId == 12)
            {
                obj.inductionId = 112;
            }
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@statusTypeId", obj.statusTypeId);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@search", obj.search.TooString());
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable ApplicantSearchByAdmin(ApplicantSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantSearchByAdmin]"
            };
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@userId", obj.userId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable ApplicantStatusHistory(int applicantId)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGetApplicantStatusByApplicantId]"
            };
            cmd.Parameters.AddWithValue("@applicantId", applicantId);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable ApplicantSearch(ApplicantSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantSearch]"
            };
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@statusTypeId", obj.statusTypeId);
            cmd.Parameters.AddWithValue("@statusId", obj.statusId);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }

        public Message ApplicantInfoAddUpdate(ApplicantInfo obj)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantAddUpdateInfoAdmin(obj.inductionId, obj.phaseId, obj.applicantId, obj.fatherName, obj.genderId, obj.disableId, obj.dob
                    , obj.pmdcExpiryDate, obj.mbbsPassingDate, obj.countryIdDegreePassing
                   , obj.countryId, obj.districtId, obj.districtName, obj.domicileProvinceId, obj.domicileDistrictId
                    , obj.address, obj.cnicNo, obj.cnicExpiryDate, obj.cnicPicFront, obj.cnicPicBack
                    , obj.domicilePicFront, obj.domicilePicBack, obj.pic, obj.disableImage, obj.adminId);
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
                db.spApplicantDegreeAddUpdateAdmin(obj.applicantDegreeDetailId, obj.inductionId, obj.phaseId, obj.applicantId
                    , obj.graduateTypeId, obj.degreeTypeId
                    , obj.degreeYear, obj.provinceId, obj.instituteTypeId, obj.instituteId, obj.instituteName, obj.totalMarks, obj.obtainMarks
                    , obj.imageDegree, obj.imageDegreeForeignFront, obj.imageDegreeForeignBack
                    , obj.imageDegreeMatric, obj.imageCertificate, obj.fcpsExemptionStatus, obj.fcpsExemptionCertificate, obj.adminId);
                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message ApplicantDegreeMarksAddUpdate(List<ApplicantDegreeMark> listMarks, int inductionId, int phaseId, int adminId)
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
                        db.spApplicantDegreeMarksAddUpdateAdmin(objMark.degreeMarksId, inductionId, phaseId, objMark.applicantId, objMark.graduateTypeId
                            , objMark.year
                            , objMark.totalMarks, objMark.obtainMarks, objMark.attempt, objMark.imageDMC, adminId);
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

        public Message ApplicantCertificateAddUpdate(List<ApplicantCertificate> listCertificate)
        {
            Message msg = new Message();
            try
            {
                foreach (ApplicantCertificate obj in listCertificate)
                {
                    try
                    {
                        db.spApplicantCertificateAddUpdateAdmin(obj.applicantCertificateTypeId, obj.applicantId, obj.typeId
                            , 0
                           , obj.disciplineId, obj.obtainMarks, obj.totalMarks, obj.passingDate, obj.imageCertificate, obj.adminId);
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

        public Message ApplicantExperienceAddUpdate(ApplicantExperience obj)
        {
            Message msg = new Message();
            try
            {
                var objt = db.spApplicantExperienceAddUpdateAdmin(obj.applicantExperienceId, obj.inductionId, obj.phaseId, obj.applicantId, obj.levelId
                      , obj.levelTypeId, obj.instituteName, obj.instituteId, obj.provinceId, obj.typeId
                      , obj.startDated, obj.endDate
                      , obj.isCurrent, obj.imageExperience, obj.adminId).FirstOrDefault();
                msg = MapApplicantExperience.ToEntity(objt);
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
                db.spApplicantDistinctionAddUpdateAdmin(obj.applicantDistinctionId, obj.inductionId, obj.phaseId, obj.applicantId
                    , obj.subject, obj.year, obj.imageDistinction, obj.adminId).FirstOrDefault();

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
                CommandText = "[dbo].[spApplicantHouseJobAddUpdateAdmin]"
            };
            cmd.Parameters.AddWithValue("@houseJodId", obj.houseJodId);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@applicantId", obj.applicantId);
            cmd.Parameters.AddWithValue("@provinceId", obj.provinceId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@hospital", obj.hospital);
            cmd.Parameters.AddWithValue("@startDate", obj.startDate);
            cmd.Parameters.AddWithValue("@endDate", obj.endDate);
            cmd.Parameters.AddWithValue("@image", obj.image);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);

        }

        public Message ApplicantResearchPaperAddUpdate(ApplicantResearchPaper obj)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantResearchPaperAddUpdateAdmin(obj.applicantResearchId, obj.inductionId, obj.phaseId, obj.applicantId, obj.name
                    , obj.authorId, obj.publishStatusId, obj.link, obj.webLink, obj.imageLetter, obj.researchJournalId, obj.isActive, obj.adminId);


                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }

        public Message ApplicantSpecilityAddUpdate(ApplicantSpecility obj)
        {
            Message msg = new Message();
            try
            {
                //var item = db.spApplicantSpecilityAddUpdateAdmin(obj.applicantSpecilityId, obj.inductionId, obj.phaseId
                //    , obj.applicantId, obj.preferenceNo
                //     , obj.typeId, obj.specialityId, obj.hospitalId, obj.adminId).FirstOrDefault();

                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }
            return msg;
        }


        public Message ApplicantVoucherAddUpdate(ApplicantVoucher obj)
        {
            Message msg = new Message();
            try
            {
                db.spApplicantVoucherAddUpdateAdmin(obj.applicantId, obj.amount, obj.branchCode, obj.voucherImage
                    , obj.ibn, obj.accountNo, obj.accountTitle, obj.submittedDate, obj.adminId);

                msg.status = true;
                msg.message = "Transaction saved successfully";
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = "System error.";
            }
            return msg;
        }



    }
}
