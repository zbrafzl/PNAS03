
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
    public class QaApplicantAPIController : PrpQaBasedController
    {

        [HttpGet]
        [Route("applicant/get")]
        public ApplicantInfoAPI ApplicantGetInfo(string applicantNo)
        {
            ApplicantInfoAPI obj = new CommonDAL().ApplicantGetInfo(applicantNo, 2);

            //if (obj != null && obj.applicantId > 0)
            //{
            //    obj.message = "Applicant Exists";
            //    obj.status = true;
            //}
            //else
            //{
            //    obj.message = "Applicant Not Exists";
            //    obj.status = false;

            //}
            return obj;
        }

        [HttpPost]
        [Route("applicant/voucher-add")]
        public MessageAPI ApplicantVoucherInfoAdd(ApplicantVoucherAPIInPut obj)
        {
            MessageAPI msg = new MessageAPI();
            try
            {
                ApplicantVoucherBank item = new ApplicantVoucherBank();
                item.applicantNo = obj.applicantNo;
                item.applicantId = obj.applicantNo.TooInt();
                item.amount = obj.amount.TooInt();
                if (item.amount == 0)
                {
                    if ((item.applicantId.ToString().Substring(2, 1)) == "7")
                    {
                        item.amount = 2000;
                    }
                    else
                    {
                        item.amount = 500;
                    }
                }
                item.transactionIdBank = obj.transactionIdBank;
                item.statusBank = 1;
                item.transactionType = 2;

                msg = new ApplicantDAL().ApplicantVoucherBankAddUpdate(item);

                //if (msgg.status)
                //{
                //    msg.status = msgg.status;
                //    msg.message = "Transaction saved successfully";
                //}
                //else
                //{
                //    msg.status = false;
                //    msg.message = msg.message;

                //}

            }
            catch (Exception)
            {
                msg.message = "System Error";
                msg.status = false;
            }

            return msg;

        }


        [HttpPost]
        [Route("applicant/voucher-cancel")]
        public MessageAPI ApplicantVoucherInfoCancel(ApplicantVoucherAPIInPut obj)
        {
            MessageAPI msg = new MessageAPI();
            try
            {
                ApplicantVoucherBank item = new ApplicantVoucherBank();
                item.applicantNo = obj.applicantNo;
                item.applicantId = obj.applicantNo.TooInt();
                item.amount = obj.amount.TooInt();
                if (item.amount == 0)
                {
                    if ((item.applicantId.ToString().Substring(2, 1)) == "7")
                    {
                        item.amount = 2000;
                    }
                    else
                    {
                        item.amount = 500;
                    }
                }
                item.transactionIdBank = obj.transactionIdBank;
                item.statusBank = 2;
                item.transactionType = 2;


                msg = new ApplicantDAL().ApplicantVoucherBankAddUpdate(item);

                //if (msgg.status)
                //{
                //    msg.status = msgg.status;
                //    msg.message = "Transaction saved successfully";
                //}
                //else
                //{
                //    msg.status = false;
                //    msg.message = msg.message;

                //}


            }
            catch (Exception)
            {
                msg.message = "System Error";
                msg.status = false;
            }


            return msg;




        }

        [HttpGet]
        [Route("voucher/info-get")]
        public ApplicantVoucherAPIOutPut VoucherInfoGetByApplicantNo(string applicantNo)
        {
            ApplicantVoucherAPIOutPut objRturn = new ApplicantVoucherAPIOutPut();

            if (applicantNo.Length == 8 && applicantNo.Substring(2, 1) == "8")
            {
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spApplicantVoucherGetByApplicantNo]"
                };
                try
                {
                    con = new SqlConnection(PrpDbConnectADO.Conn);
                    con.Open();
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@applicantNo", applicantNo);
                    cmd.Parameters.AddWithValue("@transactionType", 2);
                    DataTable dt = PrpDbADO.FillDataTable(cmd);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        DataRow dr = dt.Rows[0];

                        objRturn.applicantId = dr["applicantId"].TooInt();
                        objRturn.applicantNo = dr["applicantNo"].TooString();
                        objRturn.name = dr["name"].TooString();
                        objRturn.pmdcNo = dr["pncNo"].TooString();
                        objRturn.cnicNo = dr["cnicNo"].TooString();
                        objRturn.transactionStatus = dr["[status"].TooString();
                        objRturn.transactionIdBank = dr["transactionIdBank"].TooString();
                    }
                }
                catch (Exception)
                {

                    objRturn = new ApplicantVoucherAPIOutPut();
                    objRturn.transactionStatus = "System Error.";
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {

                try
                {
                    ApplicantVoucherBank obj = new ApplicantDAL().ApplicantVoucherGetByApplicantNo(applicantNo, 2);

                    //ApplicantVoucherAPIOutPut
                    if (obj != null && obj.applicantId > 0)
                    {
                        objRturn.applicantId = obj.applicantId;
                        objRturn.applicantNo = obj.applicantNo;
                        objRturn.name = obj.name;
                        objRturn.pmdcNo = obj.pmdcNo;
                        objRturn.cnicNo = obj.cnicNo;
                        objRturn.transactionStatus = obj.status;
                        objRturn.transactionIdBank = obj.transactionIdBank;
                    }
                    else
                    {
                        objRturn = new ApplicantVoucherAPIOutPut();
                        objRturn.transactionStatus = "Applicant Not Exists";
                    }
                }
                catch (Exception)
                {
                    objRturn = new ApplicantVoucherAPIOutPut();
                    objRturn.transactionStatus = "System Error.";
                }
            }

            return objRturn;
        }



    }
}
