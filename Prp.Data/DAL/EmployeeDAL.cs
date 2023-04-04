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
    public class EmployeeDAL : PrpDBConnect
    {
        public Employee GetById(int employeeId)
        {
            Employee obj = new Employee();
            try
            {
                var objt = db.tblEmployees.FirstOrDefault(x => x.employeeId == employeeId);
                if (objt != null && objt.employeeId > 0)
                    obj = MapEmployee.ToEntity(objt);
                else
                {
                    obj = new Employee();
                }

            }
            catch (Exception)
            {
                obj = new Employee();
            }
            return obj;
        }

        public Employee GetDetailById(int employeeId)
        {
            Employee obj = new Employee();
            try
            {
                var objt = db.tvwEmployees.FirstOrDefault(x => x.employeeId == employeeId);
                if (objt != null && objt.employeeId > 0)
                    obj = MapEmployee.ToEntity(objt);
                else
                {
                    obj = new Employee();
                }

            }
            catch (Exception ex)
            {
                obj = new Employee();
            }
            return obj;
        }

        public List<spEmployeeSearch_Result> EmployeeSearch(EmployeeSearch obj)
        {
            List<spEmployeeSearch_Result> list = new List<spEmployeeSearch_Result>();
            try
            {
                list = db.spEmployeeSearch(0,0,obj.hospitalId, obj.adminId, obj.search).ToList();

            }
            catch (Exception ex)
            {
                list = new List<spEmployeeSearch_Result>();
            }
            return list;
        }

        public List<Employee> Search(EmployeeSearch obj)
        {
            List<Employee> list = new List<Employee>();
            try
            {
                var listt = db.spEmployeeSearch(0,0,obj.hospitalId, obj.adminId, obj.search).ToList();
                list = MapEmployee.ToEntityList(listt);
            }
            catch (Exception ex)
            {
            }
            return list;
        }


        public Message EmployeeIsExistsCellNo(Employee obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmployeeIsExistsCellNo]"
            };
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@employeeId", obj.employeeId);
            cmd.Parameters.AddWithValue("@cellNo", obj.cellNo);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public Message EmployeeIsExistsCNIC(Employee obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmployeeIsExistsCNIC]"
            };
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@employeeId", obj.employeeId);
            cmd.Parameters.AddWithValue("@cnic", obj.cnic);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public Message AddUpdate(Employee obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmployeeAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@employeeId", obj.employeeId);
            cmd.Parameters.AddWithValue("@name", obj.name);
            cmd.Parameters.AddWithValue("@relationId", obj.relationId);
            cmd.Parameters.AddWithValue("@relationName", obj.relationName);
            cmd.Parameters.AddWithValue("@genderId", obj.genderId);
            cmd.Parameters.AddWithValue("@martialStatusId", obj.martialStatusId);
            cmd.Parameters.AddWithValue("@cellNo", obj.cellNo);
            cmd.Parameters.AddWithValue("@cnic", obj.cnic);
            cmd.Parameters.AddWithValue("@districtId", obj.districtId);
            cmd.Parameters.AddWithValue("@address", obj.address);
            cmd.Parameters.AddWithValue("@designationId", obj.designationId);
            cmd.Parameters.AddWithValue("@degreeId", obj.degreeId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@joiningDate", obj.joiningDate);
            cmd.Parameters.AddWithValue("@image", obj.image);
            cmd.Parameters.AddWithValue("@isActive", obj.isActive);
            cmd.Parameters.AddWithValue("@programIds", obj.programIds);
            cmd.Parameters.AddWithValue("@yearExerience", obj.yearExerience);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);

            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public Message EmployeeDelete(Employee obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmployeeDelete]"
            };
            cmd.Parameters.AddWithValue("@employeeId", obj.employeeId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }

        public Message AddUpdateExperience(EmployeeExperience obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmployeeExperienceAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@id", obj.id);
            cmd.Parameters.AddWithValue("@employeeId", obj.employeeId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@hospitalName", obj.hospitalName);
            cmd.Parameters.AddWithValue("@dateStart", obj.dateStart);
            cmd.Parameters.AddWithValue("@dateEnd", obj.dateEnd);
            cmd.Parameters.AddWithValue("@isCurrent", obj.isCurrent);
            cmd.Parameters.AddWithValue("@executionType", obj.executionType);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);

            return PrpDbADO.FillDataTableMessage(cmd);
        }


        public EmployeeExperience EmployeeExperienceGetById(int id)
        {
            EmployeeExperience list = new EmployeeExperience();
            try
            {
                var listt = db.tblEmployeeExperiences.FirstOrDefault(x => x.id == id);
                if (listt != null && listt.id > 0)
                    list = MapEmployee.ToEntity(listt);
                else
                {
                    list = new EmployeeExperience();
                }
            }
            catch (Exception ex)
            {
                list = new EmployeeExperience();
            }
            return list;


        }
        public List<EmployeeExperience> EmployeeExperienceGet(EmployeeExperience obj)
        {
            List<EmployeeExperience> list = new List<EmployeeExperience>();
            try
            {
                var listt = db.spEmployeeExperienceGet(obj.employeeId, obj.hospitalId, obj.adminId).ToList();
                list = MapEmployee.ToEntityList(listt);
            }
            catch (Exception ex)
            {
            }
            return list;

            //SqlCommand cmd = new SqlCommand
            //{
            //    CommandType = CommandType.StoredProcedure,
            //    CommandText = "[dbo].[spEmployeeExperienceGet]"
            //};
            //cmd.Parameters.AddWithValue("@employeeId", obj.employeeId);
            //cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            //cmd.Parameters.AddWithValue("@adminId", obj.adminId);

            //return PrpDbADO.FillDataTable(cmd);
        }

        public EmployeeSpeciality GetEmployeeSpeciality(int id)
        {
            EmployeeSpeciality obj = new EmployeeSpeciality();
            try
            {
                var objt = db.tblEmployeeSpecialities.FirstOrDefault(x => x.id == id);
                if (objt != null && objt.id > 0)
                    obj = MapEmployee.ToEntity(objt);
                else
                {
                    obj = new EmployeeSpeciality();
                }

            }
            catch (Exception)
            {
                obj = new EmployeeSpeciality();
            }
            return obj;
        }

        public EmployeeSpeciality GetEmployeeSpecialitySingleByEmployee(int employeeId)
        {
            EmployeeSpeciality obj = new EmployeeSpeciality();
            try
            {
                var objt = db.tblEmployeeSpecialities.FirstOrDefault(x => x.employeeId == employeeId);
                if (objt != null && objt.id > 0)
                    obj = MapEmployee.ToEntity(objt);
                else
                {
                    obj = new EmployeeSpeciality();
                }

            }
            catch (Exception)
            {
                obj = new EmployeeSpeciality();
            }
            return obj;
        }

        public List<EmployeeSpeciality> GetEmployeeSpecialities(int employeeId)
        {
            List<EmployeeSpeciality> list = new List<EmployeeSpeciality>();
            try
            {
                var objt = db.tvwEmployeeSpecialities.Where(x => x.employeeId == employeeId).ToList();
                list = MapEmployee.ToEntityList(objt);
            }
            catch (Exception)
            {
                list = new List<EmployeeSpeciality>();
            }
            return list;
        }

        public Message AddUpdateEmployeeSpeciality(EmployeeSpeciality obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmployeeSpecialityAddUpdate]"
            };
            cmd.Parameters.AddWithValue("@id", obj.id);
            cmd.Parameters.AddWithValue("@employeeId", obj.employeeId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@typeId", obj.typeId);
            cmd.Parameters.AddWithValue("@disciplineId", obj.disciplineId);
            cmd.Parameters.AddWithValue("@specialityId", obj.specialityId);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            return PrpDbADO.FillDataTableMessage(cmd);
        }


        #region Reports


        public DataTable EmployeeSearchReport(EmployeeSearch obj)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmployeeSearchReport]"
            };
            cmd.Parameters.AddWithValue("@top", obj.top);
            cmd.Parameters.AddWithValue("@pageNum", obj.pageNum);
            cmd.Parameters.AddWithValue("@adminId", obj.adminId);
            cmd.Parameters.AddWithValue("@specialityId", obj.specialityId);
            cmd.Parameters.AddWithValue("@hospitalId", obj.hospitalId);
            cmd.Parameters.AddWithValue("@genderId", obj.genderId);
            cmd.Parameters.AddWithValue("@degreeId", obj.degreeId);
            cmd.Parameters.AddWithValue("@designationId", obj.designationId);
            cmd.Parameters.AddWithValue("@reportTypeId", obj.reportTypeId);
            cmd.Parameters.AddWithValue("@search", obj.search);

            return PrpDbADO.FillDataTable(cmd);
        }

        #endregion
    }
}
