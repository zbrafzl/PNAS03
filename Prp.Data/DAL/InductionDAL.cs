using Prp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prp.Data
{
    public class InductionDAL : PrpDBConnect
    {

        public List<EntityDDL> GetInductionDDL(DDLInduction obj)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                var listt = db.spInductionForDDL(obj.userId, obj.reffIds, obj.condition).OrderByDescending(x=> x.id).ToList();
                list = MapInduction.ToEntityList(listt);
            }
            catch (Exception)
            {
            }
            return list;
        }
    }
}
