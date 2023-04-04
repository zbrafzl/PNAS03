using Prp.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hims.API.Controllers.QA
{
    [RoutePrefix("qa")]
    public class QaAdminController : AdminBasedController
    {
        [HttpGet]
        [Route("admin/applicant/get")]
        public ApplicantInfoAdmin ApplicantGetInfoByEmail(string email, string phoneNumber)
        {
            ApplicantInfoAdmin obj = new CommonDAL().ApplicantGetInfoByEmail(email, phoneNumber);
            return obj;
        }

        [HttpGet]
        [Route("admin/applicant/delete")]
        public ApplicantInfoAPI DeleteApplicantByAdmin(string applicantID)
        {
            ApplicantInfoAPI obj = new CommonDAL().ApplicantGetInfo(applicantID, 2);
            return obj;
        }

        [HttpGet]
        [Route("admin/applicant/genderChange")]
        public ApplicantInfoAPI ApplicantChangeGenderByAdmin(string applicantID)
        {
            ApplicantInfoAPI obj = new CommonDAL().ApplicantGetInfo(applicantID, 2);
            return obj;
        }

        [HttpGet]
        [Route("admin/applicant/degreeChange")]
        public ApplicantInfoAPI ApplicantChangeDegreeByAdmin(string applicantID)
        {
            ApplicantInfoAPI obj = new CommonDAL().ApplicantGetInfo(applicantID, 2);
            return obj;
        }

        [HttpGet]
        [Route("admin/applicant/programChange")]
        public ApplicantInfoAPI ApplicantChangeProgramByAdmin(string applicantID)
        {
            ApplicantInfoAPI obj = new CommonDAL().ApplicantGetInfo(applicantID, 2);
            return obj;
        }

    }
}
