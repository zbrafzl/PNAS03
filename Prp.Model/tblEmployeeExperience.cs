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
    
    public partial class tblEmployeeExperience
    {
        public int id { get; set; }
        public int employeeId { get; set; }
        public int typeId { get; set; }
        public int hospitalId { get; set; }
        public string hospitalName { get; set; }
        public System.DateTime dateStart { get; set; }
        public System.DateTime dateEnd { get; set; }
        public bool isCurrent { get; set; }
        public int noOfDays { get; set; }
        public int noOfMonth { get; set; }
        public string experienceTitle { get; set; }
        public int adminId { get; set; }
        public System.DateTime dated { get; set; }
    }
}
