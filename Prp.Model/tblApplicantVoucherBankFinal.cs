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
    
    public partial class tblApplicantVoucherBankFinal
    {
        public int voucherBankId { get; set; }
        public int inductionId { get; set; }
        public int phaseId { get; set; }
        public int applicantId { get; set; }
        public string applicantNo { get; set; }
        public int amount { get; set; }
        public string transactionIdBank { get; set; }
        public int statusBank { get; set; }
        public System.DateTime dated { get; set; }
        public int transactionType { get; set; }
    }
}
