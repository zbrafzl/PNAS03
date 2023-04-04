using Prp.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prp.Data
{
    public class GrievanceDAL : PrpDBConnect
    {

        public Grievance GetById(int grievanceId)
        {
            Grievance obj = new Grievance();
            try
            {
                var objt = db.tblGrievances.FirstOrDefault(x => x.grievanceId == grievanceId);
                obj = MapGrievance.ToEntity(objt);
            }
            catch (Exception)
            {
                obj = new Grievance();
            }
            if (obj == null)
                obj = new Grievance();
            return obj;
        }

        public GrievanceAction ActionGetById(int grievanceId)
        {
            GrievanceAction obj = new GrievanceAction();
            try
            {
                var objt = db.tblGrievanceActions.FirstOrDefault(x => x.grievanceId == grievanceId);
                obj = MapGrievanceAction.ToEntity(objt);
            }
            catch (Exception)
            {
                obj = new GrievanceAction();
            }
            if (obj == null)
                obj = new GrievanceAction();
            return obj;
        }

        public GrievanceRemark RemarksGetById(int grievanceId)
        {
            GrievanceRemark obj = new GrievanceRemark();
            try
            {
                var objt = db.tblGrievanceRemarks.FirstOrDefault(x => x.grievanceId == grievanceId);
                obj = MapGrievanceRemark.ToEntity(objt);
            }
            catch (Exception)
            {
                obj = new GrievanceRemark();
            }
            if (obj == null)
                obj = new GrievanceRemark();
            return obj;
        }

        public Grievance GetByTypeAndApplicant(int typeId ,int applicantId )
        {
            Grievance obj = new Grievance();
            try
            {
                var objt = db.tblGrievances.FirstOrDefault(x => x.typeId == typeId && x.applicantId== applicantId);
                obj = MapGrievance.ToEntity(objt);
            }
            catch (Exception)
            {
                obj = new Grievance();
            }
            if (obj == null)
                obj = new Grievance();
            return obj;
        }

        //public Grievance GetByApplicantAndType(int applicantId, int grievanceTypeId)
        //{
        //    Grievance obj = new Grievance();
        //    try
        //    {
        //        var listt = db.tvwGrievances.Where(x => x.applicantId == applicantId && x.grievanceTypeId == grievanceTypeId);
        //        if (listt != null && listt.Count() > 0)
        //        {
        //            var objt = listt.FirstOrDefault(x => x.adminIdStatus > 0);
        //            if (objt != null)
        //            {
        //                obj = MapGrievance.ToEntity(objt);
        //            }
        //            else
        //            {
        //                objt = listt.FirstOrDefault();
        //                obj = MapGrievance.ToEntity(objt);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        obj = new Grievance();
        //    }
        //    if(obj == null)
        //        obj = new Grievance();
        //    return obj;
        //}


        //public List<Grievance> GetByGrievanceId(int grievanceId)
        //{
        //    List<Grievance> list = new List<Grievance>();
        //    try
        //    {
        //        var listt = db.tvwGrievances.Where(x => x.grievanceId == grievanceId).ToList();
        //        list = MapGrievance.ToEntityList(listt);

        //    }
        //    catch (Exception)
        //    {
        //        list = new List<Grievance>();
        //    }
        //    return list;
        //}

        //public Grievance GetByAppearanceId(int appearanceId)
        //{
        //    Grievance obj = new Grievance();
        //    try
        //    {
        //        var objt = db.tvwGrievances.FirstOrDefault(x => x.appearanceId == appearanceId);
        //        obj = MapGrievance.ToEntity(objt);

        //    }
        //    catch (Exception)
        //    {
        //        obj = new Grievance();
        //    }
        //    if(obj == null)
        //        obj = new Grievance();
        //    return obj;
        //}


       

        public Message AddUpdate(Grievance obj)
        {
            Message msg = new Message();
            try
            {
                db.spGrievanceAddUpdate(obj.grievanceId, obj.inductionId, obj.phaseId, obj.applicantId
                    , obj.typeId, obj.verficationTypeId, obj.checkListIds, obj.title, obj.detail);

                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
                msg.id = 0;
            }
            return msg;
        }

        public Message ActionAddUpdate(GrievanceAction obj)
        {
            Message msg = new Message();
            try
            {
                db.spGrievanceAttendenceAddUpdate(obj.grievanceActionId, obj.grievanceId, obj.isSelf, obj.relationId, obj.attendenceNo, obj.adminIdAttendece);

                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
                msg.id = 0;
            }
            return msg;
        }

        public Message RemarksAddUpdate(GrievanceRemark obj)
        {
            Message msg = new Message();
            try
            {
                db.spGrievanceRemarksAddUpdate(obj.grievanceRemarksId, obj.grievanceId,obj.title, obj.typeId, obj.remarks,  obj.adminId);

                msg.status = true;
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
                msg.id = 0;
            }
            return msg;
        }

        public DataTable GrievanceSearch(Grievance obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGrievanceSearch]"
            };
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@verficationTypeId", obj.verficationTypeId);
            cmd.Parameters.AddWithValue("@checkListIds", obj.checkListIds);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable GrievanceSearchAttendence(Grievance obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGrievanceSearchAttendece]"
            };
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@verficationTypeId", obj.verficationTypeId);
            cmd.Parameters.AddWithValue("@checkListIds", obj.checkListIds);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable GrievancePrint(Grievance obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spGrievancePrint]"
            };
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@dated", obj.dated);
            return PrpDbADO.FillDataTable(cmd);
        }

    }
}
