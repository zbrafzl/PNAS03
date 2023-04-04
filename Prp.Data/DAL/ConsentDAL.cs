using Prp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prp.Data
{
    public class ConsentDAL : PrpDBConnect
    {
        public Consent GetById(int consentId)
        {
            Consent obj = new Consent();
            try
            {
                var objt = db.tblConsents.FirstOrDefault(x => x.consentId == consentId);
                if(objt!=null)
                obj = MapConsent.ToEntity(objt);
                else obj = new Consent();

            }
            catch (Exception)
            {
                obj = new Consent();
            }
            return obj;
        }
        public Consent GetByApplicant(int applicantId)
        {
            Consent obj = new Consent();
            try
            {
                var objt = db.spConsentGetByApplicant(applicantId).FirstOrDefault();
                obj = MapConsent.ToEntity(objt);

            }
            catch (Exception)
            {
            }
            return obj;
        }

        public List<Consent> GetAllByApplicant(int applicantId)
        {
            List<Consent> list = new List<Consent>();
            try
            {
                var objt = db.spConsentGetByApplicant(applicantId).ToList();
                list = MapConsent.ToEntityList(objt);

            }
            catch (Exception)
            {
                list = new List<Consent>();
            }
            return list;
        }

        public List<EntityDDL> GetTypeApplicantHasMerit(int applicantId, int round)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                var listt = db.spGetTypeApplicantHasMerit(applicantId, round).ToList();
                list = MapConsent.ToEntityList(listt);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public Message AddUpdate(Consent obj)
        {
            Message msg = new Message();
            try
            {

                var objt = db.spConsentAddUpdate(obj.roundId, obj.applicantId, obj.typeId, obj.consentTypeId, 0).FirstOrDefault();
                msg = MapConsent.ToEntity(objt);
            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.id = 0;
            }

            if (msg == null)
            {
                msg = new Message();
            }

            return msg;
        }
    }
}
