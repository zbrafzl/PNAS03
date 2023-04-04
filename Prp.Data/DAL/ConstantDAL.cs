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
    public class ConstantDAL : PrpDBConnect
    {
        public Constant GetById(int constantId)
        {
            Constant obj = new Constant();
            try
            {
                tblConstant objt = db.tblConstants.FirstOrDefault(x => x.constantId == constantId);
                obj = MapConstant.ToEntity(objt);

            }
            catch (Exception)
            {
            }
            return obj;
        }

        public List<Constant> GetAll()
        {
            List<Constant> list = new List<Constant>();
            try
            {
                List<tvwConstant> listt = db.tvwConstants.ToList();
                list = MapConstant.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<Constant> GetAll(int typeId)
        {
            List<Constant> list = new List<Constant>();
            int countRows = 0;
            string checkQuery = "";
            DataTable dt = new DataTable();
            checkQuery = "select [constantId],[id],[name],[code],[value],[nameDisplay],[shortDesc],[detail],[isActive],[isDeleted],[parentId],[typeId],[dated],[adminId] from tblConstant where typeId = " + typeId+" order by name";
            SqlCommand cmd = new SqlCommand(checkQuery);
            SqlConnection con = new SqlConnection(PrpDbConnectADO.Conn);
            con.Open();
            cmd.Connection = con;
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
                foreach (DataRow dr in dt.Rows)
                {
                    Constant c = new Constant();
                    c.constantId = dr[0].TooInt();
                    c.id = dr[1].TooInt();
                    c.name = dr[2].TooString();
                    c.code = dr[3].TooString();
                    c.value = dr[4].TooInt();
                    c.nameDisplay = dr[5].TooString();
                    c.shortDesc = dr[6].TooString();
                    c.detail = dr[7].TooString();
                    c.isActive = dr[8].TooBoolean();
                    c.isDeleted = dr[9].TooBoolean();
                    c.parentId = dr[10].TooInt();
                    c.typeId = dr[11].TooInt();
                    c.dated = Convert.ToDateTime(dr[12]);
                    c.adminId = dr[13].TooInt();
                    list.Add(c);
                    //c.typeName = dr[14].TooString();
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
            //try
            //{
            //    List<tvwConstant> listt = db.tvwConstants.Where(x => x.typeId == typeId).ToList();
            //    list = MapConstant.ToEntityList(listt);

            //}
            //catch (Exception ex)
            //{
            //}
            return list;
        }

        public List<Constant> GetTypes()
        {
            List<Constant> list = new List<Constant>();
            try
            {
                List<tblConstant> listt = db.tblConstants.Where(x => x.typeId == 0).ToList();
                list = MapConstant.ToEntityList(listt);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public List<EntityDDL> GetConstantDDL(DDLConstants obj)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spConstantForDDL]"
                };
                cmd.Parameters.AddWithValue("@typeId", obj.typeId);
                cmd.Parameters.AddWithValue("@parentId", obj.parentId);
                cmd.Parameters.AddWithValue("@reffId", obj.reffId);
                cmd.Parameters.AddWithValue("@userId", obj.userId);
                cmd.Parameters.AddWithValue("@reffIds", obj.reffIds);
                cmd.Parameters.AddWithValue("@condition", obj.condition);

                DataTable dt = PrpDbADO.FillDataTable(cmd);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {

                        EntityDDL obj1 = new EntityDDL();

                        obj1.id = dr["id"].TooInt();
                        obj1.value = dr["value"].TooString();
                        list.Add(obj1);
                    }
                }

                //var listt = db.spConstantForDDL(obj.typeId, obj.parentId, obj.reffId, obj.userId, obj.reffIds, obj.condition).ToList();
                //list = MapConstant.ToEntityList(listt);
            }
            catch (Exception)
            {
            }
            return list;
        }

        public Message AddUpdate(Constant obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spConstantAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@constantId", obj.constantId);
            cmd.Parameters.AddWithValue("@id", obj.id);
            cmd.Parameters.AddWithValue("@name", obj.name);
            cmd.Parameters.AddWithValue("@code", obj.code);
            cmd.Parameters.AddWithValue("@value", obj.value);
            cmd.Parameters.AddWithValue("@nameDisplay", obj.nameDisplay);
            cmd.Parameters.AddWithValue("@shortDesc", obj.shortDesc);
            cmd.Parameters.AddWithValue("@detail", obj.detail);
            cmd.Parameters.AddWithValue("@isActive", obj.isActive);
            cmd.Parameters.AddWithValue("@isDeleted", obj.isDeleted);
            cmd.Parameters.AddWithValue("@parentId", obj.parentId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);

            return PrpDbADO.FillDataTableMessage(cmd);
        }

    }
}
