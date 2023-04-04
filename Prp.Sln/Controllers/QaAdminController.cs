using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Prp.Sln.Controllers
{
    //[RoutePrefix("qa")]
    public class QaAdminController : PnasAdminController
    {
        [HttpGet]
        [Route("admins/applicant/get")]
        public ApplicantInfoAdmin ApplicantGetInfoByEmail(string email, string phoneNumber)
        {
            ApplicantInfoAdmin obj = new CommonDAL().ApplicantGetInfoByEmail(email, phoneNumber);
            return obj;
        }

        [HttpGet]
        [Route("admins/applicant/delete")]
        public ApplicantInfoAPI DeleteApplicantByAdmin(string applicantID)
        {
            ApplicantInfoAPI obj = new CommonDAL().ApplicantGetInfo(applicantID, 2);
            return obj;
        }

        [HttpGet]
        [Route("admins/applicant/genderChange")]
        public ApplicantInfoAPI ApplicantChangeGenderByAdmin(string applicantID)
        {
            ApplicantInfoAPI obj = new CommonDAL().ApplicantGetInfo(applicantID, 2);
            return obj;
        }

        [HttpGet]
        [Route("admins/applicant/degreeChange")]
        public ApplicantInfoAPI ApplicantChangeDegreeByAdmin(string applicantID)
        {
            ApplicantInfoAPI obj = new CommonDAL().ApplicantGetInfo(applicantID, 2);
            return obj;
        }

        [HttpGet]
        [Route("admins/applicant/programChange")]
        public ApplicantInfoAPI ApplicantChangeProgramByAdmin(string applicantID)
        {
            ApplicantInfoAPI obj = new CommonDAL().ApplicantGetInfo(applicantID, 2);
            return obj;
        }

    }
}
