using Prp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prp.Data
{
    public class FeedBackDAL : PrpDBConnect
    {
        public FeedBack GetById(int feedbackId)
        {
            FeedBack obj = new FeedBack();
            try
            {
                var objt = db.tblFeedBacks.FirstOrDefault(x => x.feedbackId == feedbackId);
                obj = MapFeedBack.ToEntity(objt);
            }
            catch (Exception)
            {
                obj = new FeedBack();
            }

            return obj;
        }

        public List<FeedBack> GetByApplicant(int applicantId)
        {
            List<FeedBack> list = new List<FeedBack>();
            try
            {
                var listt = db.tvwFeedbacks.Where(x => x.feedBackById == applicantId).ToList();
                list = MapFeedBack.ToEntityList(listt);

            }
            catch (Exception)
            {
                list = new List<FeedBack>();
            }
            return list;
        }
        public List<FeedBack> GetBySearch(FeedbackSearch obj)
        {
            List<FeedBack> list = new List<FeedBack>();
            try
            {
                var listt = db.spFeedbackSearch(obj.top, obj.pageNum, obj.search, obj.startDate, obj.endDate).ToList();
                list = MapFeedBack.ToEntityList(listt);

            }
            catch (Exception)
            {
            }
            return list;
        }

        public Message AddUpdate(FeedBack obj)
        {
            Message msg = new Message();
            try
            {
                tblFeedBack feed = new tblFeedBack();

                if (obj.feedbackId == 0)
                {
                    feed = MapFeedBack.ToTable(obj);
                    db.tblFeedBacks.Add(feed);
                }
                else
                {
                    feed = db.tblFeedBacks.FirstOrDefault(x => x.feedbackId == obj.feedbackId);
                    feed.applicantIdBy = obj.applicantIdBy;
                    feed.pmdcNo = obj.pmdcNo;
                    feed.comments = obj.comments;
                    feed.dated = obj.dated;

                }
                db.SaveChanges();

                msg.id = feed.feedbackId;
            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.id = 0;
            }
            return msg;
        }


        public Message UpdateFeedback(FeedBack obj)
        {
            Message msg = new Message();
            try
            {
                tblFeedBack feed = new tblFeedBack();
                feed = db.tblFeedBacks.FirstOrDefault(x => x.feedbackId == obj.feedbackId);
                feed.adminId = obj.adminId;
                feed.reply = obj.reply;
                feed.datedReply = obj.datedReply;
                db.SaveChanges();

                msg.id = feed.feedbackId;
            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.id = 0;
            }
            return msg;
        }
    }
}
