using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Prp.Data
{
    public class BasedEntityAPI
    {
    }

    public class MessageAPI
    {

        public string message { get; set; }
        public bool status { get; set; }
        public MessageAPI()
        {
            status = false;
        }
    }

    public class ApplicantInfoAPI : MessageAPI
    {
        public int applicantId { get; set; }

        public string applicantNo { get; set; }
        public string name { get; set; }
        public string pmdcNo { get; set; }
        public string cnicNo { get; set; }
        public int amount { get; set; }
        public DateTime dueDate { get; set; }

    }

    public class ApplicantInfoAdmin : MessageAPI
    {
        public int applicantId { get; set; }
        public string name { get; set; }
        public string contactNumber { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string message { get; set; }

    }
    public class ApplicantVoucherAPIInPut
    {

        public string applicantNo { get; set; }
        public int amount { get; set; }
        public string transactionIdBank { get; set; }

    }

    public class ApplicantVoucherAPIOutPut
    {
        public int applicantId { get; set; }
        public string applicantNo { get; set; }
        public string name { get; set; }
        public string pmdcNo { get; set; }
        public string cnicNo { get; set; }
        public string transactionStatus { get; set; }
        public string transactionIdBank { get; set; }

    }
}
