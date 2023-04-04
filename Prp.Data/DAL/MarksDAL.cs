using Prp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prp.Data
{
    public class MarksDAL : PrpDBConnect
    {
        public List<Marks> GetMarksAccumulativeByApplicant( int applicantId)
        {
            List<Marks> list = new List<Marks>();
            try
            {
                var  listt = db.spMarksGetAccumulativeByApplicant(applicantId, 12).ToList();
                list = MapMark.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<Marks> GetMarksDetaikByApplicant(int inductionId, int applicantId)
        {
            List<Marks> list = new List<Marks>();
            try
            {
                var listt = db.spMarksDetailByApplicant(inductionId,applicantId).ToList();
                list = MapMark.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }
    }
}
