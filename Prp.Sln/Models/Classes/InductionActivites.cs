using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Prp.Sln.Models.Classes
{
    public class InductionActivites
    {
        [Key]
        public int id { get; set; }
        public int inductionId { get; set; }
        public DateTime inductionStartTime { get; set; }
        public DateTime inductionEndTime { get; set; }
        public DateTime profileStartTime { get; set; }
        public DateTime profileEndTime { get; set; }

        public DateTime verificationStartTime { get; set; }
        public DateTime verificationEndTime { get; set; }
        public DateTime amendmentStartTime { get; set; }
        public DateTime amendmentEndTime { get; set; }
        public DateTime consentStartTime { get; set; }
        public DateTime consentEndTime { get; set; }
        public DateTime joiningStartTime { get; set; }
        public DateTime joiningEndTime { get; set; }
        public DateTime phase1StartTime { get; set; }
        public DateTime phase1EndTime { get; set; }
        public DateTime phase2StartTime { get; set; }
        public DateTime phase2EndTime { get; set; }
        public DateTime phase3StartTime { get; set; }
        public DateTime phase3EndTime { get; set; }


    }
}