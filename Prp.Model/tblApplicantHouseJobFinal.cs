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
    
    public partial class tblApplicantHouseJobFinal
    {
        public int houseJodId { get; set; }
        public int inductionId { get; set; }
        public int applicantId { get; set; }
        public int provinceId { get; set; }
        public int typeId { get; set; }
        public int hospitalId { get; set; }
        public string hospital { get; set; }
        public int instituteId { get; set; }
        public bool isSame { get; set; }
        public string image { get; set; }
        public System.DateTime startDate { get; set; }
        public System.DateTime endDate { get; set; }
        public int noOfDays { get; set; }
        public int noOfMonth { get; set; }
        public bool isActive { get; set; }
        public int adminId { get; set; }
        public System.DateTime dated { get; set; }
    }
}
