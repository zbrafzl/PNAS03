﻿using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace Prp.Sln.Areas.nadmin
{
    public class ClassesAdmin
    {
    }

    public static class AdminFunctions
    {

        public static ProofReadingAdminModel GenerateModelProofReading(int inductionId, int phaseId, int applicantId)
        {
            ProofReadingAdminModel model = new ProofReadingAdminModel();
            try
            {
                int inductionIdParam = inductionId;

                if (inductionId == 0)
                    inductionId = ProjConstant.inductionId;


                model.inductionId = inductionId;

                DDLConstants dDLConstant = new DDLConstants();
                dDLConstant.condition = "ByType";
                dDLConstant.typeId = ProjConstant.Constant.degreeType;
                //model.listType = new ConstantDAL().GetConstantDDL(dDLConstant).OrderBy(x => x.id).ToList();
                model.listType = new List<EntityDDL>();
                model.applicant = new ApplicantDAL().GetApplicant(inductionId, applicantId);
                try
                {

                    // Status w.r.t to inductionId in URL, if URL induction is 0 then current
                    ApplicationStatus objStatus = new ApplicantDAL().GetApplicationStatus(inductionId, ProjConstant.phaseId
                     , applicantId, ProjConstant.Constant.statusApplicantApplication).FirstOrDefault();

                    model.applicant.applicationStatusId = objStatus.statusId;
                    model.applicant.applicationStatus = objStatus.status;
                }
                catch (Exception)
                {
                }


                int applicationStatusId = 0;

                if (inductionId == ProjConstant.inductionId)
                {
                    applicationStatusId = model.applicant.applicationStatusId;
                    inductionIdParam = 0;
                }
                else
                {
                    applicationStatusId = model.applicant.applicationStatusId;
                    inductionIdParam = inductionId;
                }

                if (applicationStatusId > 1)
                    model.applicantInfo = new ApplicantDAL().GetApplicantInfoDetail(inductionIdParam, phaseId, applicantId);

                if (applicationStatusId > 2)
                {
                    model.degree = new ApplicantDAL().GetApplicantDegreeDetail(inductionIdParam, phaseId, applicantId);
                    model.listDegreeMark = new ApplicantDAL().GetApplicantDegreeMarkList(inductionIdParam, phaseId, applicantId);
                    model.listCertificate = new ApplicantDAL().GetApplicantCertificateListDetail(inductionIdParam, phaseId, applicantId);
                }

                if (applicationStatusId > 3)
                {
                    model.listJob = new ApplicantDAL().GetApplicantHouseJobList(inductionIdParam, phaseId, applicantId);
                    model.listExprince = new ApplicantDAL().GetApplicantExperienceListDetail(inductionIdParam, phaseId, applicantId);
                    model.listDistinction = new ApplicantDAL().GetApplicantDistinctionList(inductionIdParam, phaseId, applicantId);
                }

                if (applicationStatusId > 4)
                    model.listResearchPaper = new ApplicantDAL().GetApplicantResearchPaperListDetail(inductionIdParam, phaseId, applicantId);
                if (applicationStatusId > 5)
                    model.listSpecility = new ApplicantDAL().GetApplicantSpecilityList(inductionIdParam, phaseId, applicantId);

                if (applicationStatusId > 7)
                    model.voucher = new ApplicantDAL().GetApplicantVoucher(inductionIdParam, ProjConstant.phaseId, applicantId);

                model.listMarks = new MarksDAL().GetMarksDetaikByApplicant(inductionIdParam, applicantId);


            }
            catch (Exception)
            {
            }
            return model;
        }

        public static HardshipAdminModel GenerateModelHardship(int applicantId)
        {
            HardshipAdminModel model = new HardshipAdminModel();
            try
            {
                int inductionId = 0, phaseId = 0;
                model.applicant = new ApplicantDAL().GetApplicant(inductionId, applicantId);
                model.applicantInfo = new ApplicantDAL().GetApplicantInfoDetail(inductionId, phaseId, applicantId);
                model.degree = new ApplicantDAL().GetApplicantDegreeDetail(inductionId, phaseId, applicantId);
                model.joined = new JoiningDAL().GetJoinedApplicantDetailById(applicantId);
                model.listJob = new JoiningDAL().GetHardshipSeatsStatusByApplicant(applicantId);
            }
            catch (Exception)
            {
            }
            return model;
        }
    }

}