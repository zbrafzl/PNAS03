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
    
    public partial class tblApplicant
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public tblApplicant()
        {
            this.tblApplicantInfoes = new HashSet<tblApplicantInfo>();
        }
    
        public int applicantId { get; set; }
        public string name { get; set; }
        public string pmdcNo { get; set; }
        public string emailId { get; set; }
        public string password { get; set; }
        public string contactNumber { get; set; }
        public int network { get; set; }
        public int levelId { get; set; }
        public int facultyId { get; set; }
        public string pic { get; set; }
        public int statusId { get; set; }
        public System.DateTime dated { get; set; }
        public int adminId { get; set; }
        public string pncNo { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<tblApplicantInfo> tblApplicantInfoes { get; set; }
    }
}
