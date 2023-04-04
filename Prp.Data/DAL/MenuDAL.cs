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
    public class MenuDAL : PrpDBConnect
    {

        public Menu GetById(int menuId)
        {
            Menu obj = new Menu();
            try
            {
                var objt = db.tvwMenus.FirstOrDefault(x => x.menuId == menuId);
                obj = MapMenu.ToEntity(objt);

            }
            catch (Exception ex)
            {
                obj = new Menu();
            }
            return obj;
        }

        public List<Menu> GetAll()
        {
            List<Menu> list = new List<Menu>();
            try
            {
                var listt = db.tvwMenus.ToList();
                list = MapMenu.ToEntityList(listt);

            }
            catch (Exception ex)
            {
                list = new List<Menu>();
            }
            return list;
        }

        public List<Menu> GetByType(int typeId)
        {
            List<Menu> list = new List<Menu>();
            try
            {
                var listt = db.tvwMenus.Where(x => x.typeId == typeId).ToList();
                list = MapMenu.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<Menu> GetByApp(int appId)
        {
            List<Menu> list = new List<Menu>();
            try
            {
                var listt = db.tvwMenus.Where(x => x.appId == appId).ToList();
                list = MapMenu.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<EntityDDL> GetMenuForDDL(DDLMenu obj)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                var listt = db.spMenuForDDL(obj.typeId, obj.parentId, obj.reffId, obj.userId, obj.reffIds, obj.condition).ToList();
                list = MapMenu.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<Menu> GetParent(int appId)
        {
            List<Menu> list = new List<Menu>();
            try
            {
                var listt = db.tvwMenus.Where(x => x.appId == appId && x.parentId == 0).OrderBy(x => x.sortOrder).ToList();
                list = MapMenu.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }
        public List<Menu> Search(int appId, int parentId)
        {
            List<Menu> list = new List<Menu>();
            try
            {
                var listt = db.spMenuSearch(appId, parentId).ToList();
                list = MapMenu.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<Menu> GetByUser(int userId, int appId)
        {
            List<Menu> list = new List<Menu>();
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand("spMenuGetByUserId");
            try
            {
                con = new SqlConnection(PrpDbConnectADO.Conn);
                con.Open();
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@userId", userId);
                cmd.Parameters.AddWithValue("@appId", 2);
                DataTable dt = new DataTable();
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    da.Fill(dt);
                }
                foreach (DataRow dr in dt.Rows)
                {
                    Menu li = new Menu();
                    li.menuId = dr[0].TooInt();
                    li.name = dr[1].TooString();
                    li.url = dr[2].TooString();
                    li.iconClass = dr[3].TooString();
                    li.sortOrder = dr[4].TooInt();
                    li.isMenu = dr[5].TooBoolean();
                    li.isActive = dr[6].TooBoolean();
                    li.parentId = dr[7].TooInt();
                    li.typeId = dr[8].TooInt();
                    list.Add(li);
                }
                //var listt = list;
                List<spMenuGetByUserId_Result> newList = new List<spMenuGetByUserId_Result>();
                foreach (var item in list)
                {
                    var il = new spMenuGetByUserId_Result();
                    il.menuId = item.menuId;
                    il.name = item.name;
                    il.url = item.url;
                    il.iconClass = item.iconClass;
                    il.sortOrder = item.sortOrder;
                    il.isMenu = item.isMenu;
                    il.isActive = item.isActive;
                    il.parentId = item.parentId;
                    il.typeId = item.typeId;
                    newList.Add(il);
                }
                list = MapMenu.ToEntityList(newList);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public bool CheckPageHasRight(int appId, int userId, string url)
        {
            if (userId == 3)
                userId = 1;
            bool result = false;
            spMenuGetRightByUrl_Result li = new spMenuGetRightByUrl_Result();
            DataTable dt = new DataTable();
            try
            {
                //var itemd = db.spMenuGetRightByUrl(url, userId, appId).ToList();
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand("spMenuGetRightByUrl");
                try
                {
                    con = new SqlConnection(PrpDbConnectADO.Conn);
                    con.Open();
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@url", url);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    cmd.Parameters.AddWithValue("@appId", 2);
                    
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        da.Fill(dt);
                    }
                    foreach (DataRow dr in dt.Rows)
                    {
                        
                        li.menuId = dr[0].TooInt();
                        li.userId = userId;
                        li.url = dr[1].TooString();
                    }
                }
                catch (Exception ex)
                {
                }
                if (li != null && dt.Rows.Count > 0)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }


        public Message AddUpdate(Menu obj)
        {
            Message msg = new Message();
            try
            {
                tblMenu menu = new tblMenu();

                if (obj.menuId == 0)
                {
                    menu = MapMenu.ToTable(obj);
                    db.tblMenus.Add(menu);
                }
                else
                {
                    menu = db.tblMenus.FirstOrDefault(x => x.menuId == obj.menuId);
                    menu.menuId = obj.menuId;
                    menu.name = obj.name;
                    menu.nameDisplay = obj.nameDisplay;
                    menu.url = obj.url;
                    menu.iconClass = obj.iconClass;
                    menu.sortOrder = obj.sortOrder;
                    menu.isMenu = obj.isMenu;
                    menu.typeId = obj.typeId;
                    menu.appId = obj.appId;
                    menu.isActive = obj.isActive;
                    menu.parentId = obj.parentId;
                    menu.dated = obj.dated;
                    menu.adminId = obj.adminId;
                }
                db.SaveChanges();

                msg.id = menu.menuId;
            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.id = 0;
            }
            return msg;
        }


        #region Access

        public Message AddUpdateUserAccess(int userId, string menuIds)
        {
            Message msg = new Message();

            try
            {
                db.spMenuAccessAddUpdate(userId, menuIds);
                msg.status = true;
                msg.msg = "Data saved";
            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.status = false;
            }

            return msg;
        }

        public Message AddUpdateUserTypeAccess(int typeId, string menuIds)
        {
            Message msg = new Message();

            try
            {
                db.spMenuUserTypeAddUpdate(typeId, menuIds);
                msg.status = true;
                msg.msg = "Data saved";
            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.status = false;
            }
            return msg;
        }


        public List<Menu> GetMenuListForUserId(int userId)
        {
            List<Menu> listMenu = new List<Menu>();
            try
            {
                var list = db.spMenuGetAllForUser(userId).ToList();
                listMenu = MapMenu.ToEntityList(list);
            }
            catch (Exception ex)
            {
            }
            return listMenu;
        }

        public List<Menu> GetMenuListForUserType(int userType)
        {
            List<Menu> listMenu = new List<Menu>();
            try
            {
                var list = db.spMenuGetAllForUserType(userType).ToList();
                listMenu = MapMenu.ToEntityList(list);
            }
            catch (Exception ex)
            {
            }
            return listMenu;
        }

        #endregion
    }
}
