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
    public class RegionDAL : PrpDBConnect
    {
        public List<Region> RegionGetByCondition(int typeId, int parentId, string condition="")
        {
            DataTable dt = new DataTable();
            List<Region> list = new List<Region>();
            try
            {
                //var listt = db.spRegionGetByCondition(typeId, parentId, condition).ToList();
                
                int conditionID = 2;
                if (condition == "GetAllCountry")
                    conditionID = 1;
                else if (condition == "GetDistrictByParent")
                    conditionID = 4;
                else if (condition == "GetDistrictAll")
                    conditionID = 4;
                string query = "select [regionId],[name],[code],[sortOrder],[parentId],[isActive],[typeId],[dated],[adminId] from tblRegion where typeId = "+conditionID+" and parentId = "+parentId+" order by name";
                SqlConnection con = new SqlConnection();
                Message msg = new Message();
                SqlCommand cmd = new SqlCommand(query);
                try
                {
                    con = new SqlConnection(PrpDbConnectADO.Conn);
                    con.Open();
                    cmd.Connection = con;
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            Region oneItemlist = new Region();
                            oneItemlist.regionId = Convert.ToInt32(dr[0]);
                            oneItemlist.name = dr[1].ToString();
                            //oneItemlist.code = Convert.ToInt32(dr[2]).ToString();
                            oneItemlist.sortOrder = Convert.ToInt32(dr[3]);
                            oneItemlist.parentId = Convert.ToInt32(dr[4]);
                            oneItemlist.isActive = Convert.ToBoolean(dr[5]);
                            oneItemlist.typeId = Convert.ToInt32(dr[6]);
                            oneItemlist.dated = Convert.ToDateTime(dr[7]);
                            oneItemlist.adminId = Convert.ToInt32(dr[8]);
                            oneItemlist.parentName = dr[4].ToString();
                            oneItemlist.regionType = dr[0].ToString();
                            list.Add(oneItemlist);
                        }
                    }
                    else
                    {
                        msg.status = false;
                    }
                }
                catch (Exception ex)
                {
                    msg.status = false;
                    msg.msg = ex.Message;
                }
                finally
                {
                    con.Close();
                }
                //list.Add(regionList);
                //list = MapRegion.ToEntityList(listt);
            }
            catch (Exception ex)
            {
                list = new List<Region>();
            }
            return list;
        }
    }
}
