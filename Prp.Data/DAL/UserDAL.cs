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
    public class UserDAL : PrpDBConnect
    {
        public User Login(string userName, string password)
        {
            User obj = new User();
            //try
            //{
            //    var dbt = db.spAdminLogin(userName, password).FirstOrDefault();
            //    obj = MapUser.ToEntity(dbt);
            //}
            try
            {

                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spAdminLogin]"
                };
                cmd.Parameters.AddWithValue("@emailId", userName);
                cmd.Parameters.AddWithValue("@password", password);

                DataTable dt = PrpDbADO.FillDataTable(cmd);

                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    obj.emailId = dr["emailId"].TooString();
                    obj.password = dr["password"].TooString();
                    obj.userId = dr["userId"].TooInt();
                    obj.typeId = dr["typeId"].TooInt();
                    obj.displayName = dr["firstName"].TooString();
                    obj.firstName = dr["firstName"].TooString();
                }

                //var dbt = db.spApplicantLogin(userName, password).FirstOrDefault();
                //obj = MapApplicant.ToEntity(dbt);
            }
            catch (Exception)
            {
                obj = new User();
            }
            return obj;
        }

        public User GetById(int userId)
        {
            User obj = new User();
            try
            {
                var objt = db.tvwUsers.FirstOrDefault(x => x.userId == userId);
                obj = MapUser.ToEntity(objt);

            }
            catch (Exception ex)
            {
                obj = new User();
            }
            return obj;
        }

        public List<User> GetAll()
        {
            List<User> list = new List<User>();
            try
            {
                List<tvwUser> listt = db.tvwUsers.ToList();
                list = MapUser.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<User> GetByTypeId(int typeId)
        {
            List<User> list = new List<User>();
            try
            {
                var listt = db.spUserGetByType(typeId).ToList();
                list = MapUser.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<User> GetByType(int typeId)
        {
            List<User> list = new List<User>();
            try
            {
                List<tvwUser> listt = db.tvwUsers.Where(x => x.typeId == typeId).ToList();
                list = MapUser.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public List<User> GetByParentId(int parentId)
        {
            List<User> list = new List<User>();
            try
            {
                List<tvwUser> listt = db.tvwUsers.Where(x => x.parentId == parentId).ToList();
                list = MapUser.ToEntityList(listt);

            }
            catch (Exception ex)
            {
            }
            return list;
        }

        public Message AddUpdate(User obj)
        {
            Message msg = new Message();
            try
            {
                var objt = db.spUserAddUpdate(obj.userId, obj.firstName, obj.lastName, obj.emailId, obj.password
                    , obj.typeId, obj.parentId, obj.departmentId, obj.designationId, obj.isActive, obj.adminId).FirstOrDefault();

                msg = MapUser.ToEntity(objt);

            }
            catch (Exception ex)
            {
                msg.msg = ex.Message;
                msg.id = 0;
            }
            return msg;
        }


        public Message UpdatePassword(User obj)
        {

            Message msg = new Message();

            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "[dbo].[spUserChangePassword]"
                };
                cmd.Parameters.AddWithValue("@userId", obj.userId);
                cmd.Parameters.AddWithValue("@password", obj.password);
                cmd.Parameters.AddWithValue("@passwordNew", obj.passwordNew);
                DataTable dt = PrpDbADO.FillDataTable(cmd);

                msg = dt.ConvertToEnitityMessage();
            }
            catch (Exception ex)
            {
                msg.status = false;
                msg.msg = ex.Message;
            }

            return msg;
        }

    }
}
