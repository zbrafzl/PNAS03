using Prp.Data;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Prp.Sln
{
    public class AutoJobs
    {
    }

    public class JobServiceSMS : IJob
    {
        public static readonly string JobServiceStatusSMS = ConfigurationManager.AppSettings["JobServiceStatusSMS"];
        public Task Execute(IJobExecutionContext context)
        {
            var task = Task.Run(() =>
            {
                if (JobServiceStatusSMS.Equals("ON"))
                {
                    try
                    {
                        try
                        {
                            Message msg = new Message();

                            try
                            {
                                string number = "03353131989";
                                string message = "Dear Applicant, the testing message Auto message. JobServiceSMS";
                                //msg = FunctionUI.SendSms(number, message);
                            }
                            catch (Exception ex)
                            {
                            }

                            //List<SmsProcess> listProcess = new SMSDAL().SMSProcessGetRemaning();
                            //if (listProcess != null && listProcess.Count > 0)
                            //{
                            //    foreach (var sms in listProcess)
                            //    {
                            //        Message msgSms = FunctionUI.SendSms(sms.contactNumber, sms.detail);
                            //        try
                            //        {
                            //            SmsProcess objProcess = msgSms.status.SmsProcessMakeDefaultObject(sms.applicantId, sms.smsId);
                            //            objProcess.smsProcessId = sms.smsProcessId;
                            //            new SMSDAL().AddUpdateSmsProcess(objProcess);
                            //        }
                            //        catch (Exception)
                            //        {


                            //        }
                            //    }
                            //}

                        }
                        catch (Exception)
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            });
            return task;
        }
    }

    public class JobServiceEmail : IJob
    {
        public static readonly string JobServiceStatusEmail = ConfigurationManager.AppSettings["JobServiceStatusEmail"];
        public Task Execute(IJobExecutionContext context)
        {
            var task = Task.Run(() =>
            {
                if (JobServiceStatusEmail.Equals("ON"))
                {
                    try
                    {
                        int emailTypeId = ConfigurationManager.AppSettings["EmailTypeId"].TooInt(); 

                        string emailProcessIds = "";
                       
                        List<EmailProcess> listProcess = new EmailDAL().EmailProcessGetByType(emailTypeId).ToList();
                        //List<EmailProcess> listProcess = new EmailDAL().EmailProcessGetAllRemaninig().ToList();
                        if (listProcess != null && listProcess.Count > 0)
                        {
                            foreach (var email in listProcess)
                            {
                                try
                                {
                                    Message msgSms = email.SendEmail();
                                    if (msgSms.status)
                                    {
                                        emailProcessIds = emailProcessIds + email.emailProcessId + ",";
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }

                            emailProcessIds = emailProcessIds.TrimEnd(',');

                            new EmailDAL().EmailStatusUpdateByProcessIds(emailProcessIds);
                        }
                    }
                    catch (Exception)
                    {


                    }
                }
            });
            return task;
        }
    }



    public class ExecuteTaskServiceCallScheduler
    {
        //private static readonly string ScheduleCronExpression = ConfigurationManager.AppSettings["ExecuteTaskScheduleCronExpression"];
        public static async System.Threading.Tasks.Task StartAsync()
        {
            try
            {

                var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                if (!scheduler.IsStarted)
                {
                    await scheduler.Start();
                }


                var jobSMS = JobBuilder.Create<JobServiceSMS>()
                    .WithIdentity("JobSMS", "GroupJobSMS")
                    .Build();

                var triggerSMS = TriggerBuilder.Create()
                    .WithIdentity("TriggerSMS", "GroupTriggerSMS")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(30)
                    .RepeatForever())
                    .Build();

                await scheduler.ScheduleJob(jobSMS, triggerSMS);



                var jobEmail = JobBuilder.Create<JobServiceEmail>()
                   .WithIdentity("JobEmail", "GroupJobEmail")
                   .Build();

                var triggerEmail = TriggerBuilder.Create()
                    .WithIdentity("TriggerEmail", "GroupTriggerEmail")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(ProjConstant.emailInterval)
                    .RepeatForever())
                    .Build();

                await scheduler.ScheduleJob(jobEmail, triggerEmail);

            }
            catch (Exception ex)
            {

            }
        }
    }
}