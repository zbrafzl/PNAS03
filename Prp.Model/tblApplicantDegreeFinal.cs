//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Prp.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class tblApplicantDegreeFinal
    {
        public int applicantDegreeDetailId { get; set; }
        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public int applicantId { get; set; }
        public int graduateTypeId { get; set; }
        public int degreeTypeId { get; set; }
        public int degreeYear { get; set; }
        public int provinceId { get; set; }
        public int instituteTypeId { get; set; }
        public int instituteId { get; set; }
        public string instituteName { get; set; }
        public int totalMarks { get; set; }
        public int obtainMarks { get; set; }
        public string imageDegree { get; set; }
        public string imageDegreeForeignFront { get; set; }
        public string imageDegreeForeignBack { get; set; }
        public int houseJobTypeId { get; set; }
        public string instituteHouseJob { get; set; }
        public int provinceIdHouseJob { get; set; }
        public string imageHouseJob { get; set; }
        public bool houseJobIsSameInstitute { get; set; }
        public string imageDegreeMatric { get; set; }
        public string imageCertificate { get; set; }
        public bool fcpsExemptionStatus { get; set; }
        public string fcpsExemptionCertificate { get; set; }
        public System.DateTime dated { get; set; }
    }
}
