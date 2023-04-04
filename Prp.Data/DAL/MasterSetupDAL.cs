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
    public class MasterSetupDAL : PrpDBConnect
    {

        #region Region

        public List<EntityDDL> GetRegionDDL(DDLRegions obj)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                var listt = db.spRegionForDDL(obj.typeId, obj.parentId, obj.reffId, obj.userId, obj.reffIds, obj.condition).ToList();
                list = MapRegion.ToEntityList(listt);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public DataTable RegionSearch(RegionSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spRegionSearch]"
            };
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }

        #endregion

        #region Research Journal

        public List<EntityDDL> GetJournalForDDL(DDLJournal obj)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                var listt = db.spResearchJournalForDDL(obj.typeId, obj.parentId, obj.reffId, obj.userId, obj.reffIds, obj.condition).ToList();
                list = MapResearchJournal.ToEntityList(listt);
            }
            catch (Exception)
            {
            }
            return list;
        }

       
        public ResearchJournal JournalGetById(int researchJournalId)
        {
            ResearchJournal obj = new ResearchJournal();
            try
            {
                var objt = db.tblResearchJournals.FirstOrDefault(x => x.researchJournalId == researchJournalId);
                obj = MapResearchJournal.ToEntity(objt);

            }
            catch (Exception)
            {
                obj = new ResearchJournal();
            }
            return obj;
        }

        public Message JournalAddUpdate(ResearchJournal obj)
        {
            Message msg = new Message();
            try
            {
                db.spResearchJournalAddUpdate(obj.researchJournalId, obj.name, obj.code,obj.url, obj.typeId, obj.batchId
                    , obj.regionId, obj.displineId  , obj.isActive, obj.adminId);
            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.id = 0;
            }
            return msg;
        }

        public DataTable ResearchJournalSearch(ResearchJournalSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spResearchJournalSearch]"
            };
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@displineId", obj.displineId);
            cmd.Parameters.AddWithValue("@regionId", obj.regionId);
            cmd.Parameters.AddWithValue("@isActive", obj.isActive);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }

        public DataTable ApplicantResearchSearch(ResearchJournalSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spApplicantResearchPaperSearch]"
            };

            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@phaseId", obj.phaseId);
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@researchJournalId", obj.researchJournalId);
            cmd.Parameters.AddWithValue("@search", obj.search);
            return PrpDbADO.FillDataTable(cmd);
        }


        #endregion

        #region Check List

        //public List<EntityDDL> GetCheckListForDDL(DDLCheckList obj)
        //{
        //    List<EntityDDL> list = new List<EntityDDL>();
        //    try
        //    {
        //        var listt = db.spVerficationCheckListForDDL(obj.typeId, obj.reffIds, obj.condition).ToList();
        //        list = MapCheckList.ToEntityList(listt);
        //    }
        //    catch (Exception)
        //    {
        //    }
        //    return list;
        //}

        //public VerficationCheckList CheckListGetById(int researchJournalId)
        //{
        //    VerficationCheckList obj = new VerficationCheckList();
        //    try
        //    {
        //        var objt = db.tblResearchJournals.FirstOrDefault(x => x.researchJournalId == researchJournalId);
        //        obj = MapCheckList.ToEntity(objt);

        //    }
        //    catch (Exception)
        //    {
        //        obj = new VerficationCheckList();
        //    }
        //    return obj;
        //}

        //public Message CheckListAddUpdate(VerficationCheckList obj)
        //{
        //    Message msg = new Message();
        //    try
        //    {
        //        db.spResearchJournalAddUpdate(obj.researchJournalId, obj.name, obj.code, obj.url, obj.typeId, obj.batchId
        //            , obj.regionId, obj.displineId, obj.isActive, obj.adminId);
        //    }
        //    catch (Exception ex)
        //    {
        //        msg.msg = ex.Message;
        //        msg.id = 0;
        //    }
        //    return msg;
        //}

        #endregion

        #region Ticker

        public Ticker TickerGetById(int tickerId)
        {
            Ticker obj = new Ticker();
            try
            {
                var objt = db.tblTickers.FirstOrDefault(x => x.tickerId == tickerId);
                obj = MapTicker.ToEntity(objt);

            }
            catch (Exception)
            {
                obj = new Ticker();
            }
            return obj;
        }

        public List<Ticker> TickerGetByInduction(int inductionId, int typeId)
        {
            List<Ticker> list = new List<Ticker>();
            try
            {
                var listt = db.tvwTickers.Where(x=> x.inductionId== inductionId && x.typeId == typeId).ToList();
                list = MapTicker.ToEntityList(listt);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public List<Ticker> TickerGetByInduction(int inductionId)
        {
            List<Ticker> list = new List<Ticker>();
            try
            {
                var listt = db.tvwTickers.Where(x => x.inductionId == inductionId).ToList();
                list = MapTicker.ToEntityList(listt);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public Message TickerAddUpdate(Ticker obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spTickerAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@inductionId", obj.inductionId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@tickerId", obj.tickerId);
            cmd.Parameters.AddWithValue("@name", obj.name);
            cmd.Parameters.AddWithValue("@detail", obj.detail);
            cmd.Parameters.AddWithValue("@sortOrder", obj.sortOrder);
            cmd.Parameters.AddWithValue("@isActive", obj.isActive);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }
        public DataTable TickerSearch(int inductionId)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spTickerSearch]"
            };
            cmd.Parameters.AddWithValue("@inductionId", inductionId);
            return PrpDbADO.FillDataTable(cmd);
        }


        #endregion
    }
}
