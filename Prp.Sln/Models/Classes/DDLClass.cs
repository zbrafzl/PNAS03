using Prp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Prp.Sln
{
    public class DDLClass
    {
    }

    public static class DDLConstant
    {
        public static List<EntityDDL> GetAll(int typeId = 0)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.constant;
                ddl.typeId = typeId;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.constant;
                ddl.search = search;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search, string reffIds)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.constant;
                ddl.search = search;
                ddl.reffIds = reffIds;

                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.constant;
                ddl.typeId = typeId;
                ddl.search = search;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.constant;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.constant;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.constant;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(DDLSearch ddl)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                ddl.section = ProjConstant.DDL.constant;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
    }

    public static class DDLInduction
    {
        public static List<EntityDDL> GetAll(int typeId = 0)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.induction;
                ddl.typeId = typeId;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.induction;
                ddl.search = search;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search, string reffIds)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.induction;
                ddl.search = search;
                ddl.reffIds = reffIds;

                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.induction;
                ddl.typeId = typeId;
                ddl.search = search;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.induction;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.induction;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.induction;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(DDLSearch ddl)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                ddl.section = ProjConstant.DDL.induction;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
    }

    public static class DDLRegion
    {
        public static List<EntityDDL> GetAll(int typeId = 0)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.region;
                ddl.typeId = typeId;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.region;
                ddl.search = search;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search, string reffIds)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.region;
                ddl.search = search;
                ddl.reffIds = reffIds;

                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.region;
                ddl.typeId = typeId;
                ddl.search = search;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.region;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.region;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.region;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(DDLSearch ddl)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                ddl.section = ProjConstant.DDL.region;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
    }


    public static class DDLDiscipline
    {
        public static List<EntityDDL> GetAll(int typeId = 0)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.discipline;
                ddl.typeId = typeId;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.discipline;
                ddl.search = search;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search, string reffIds)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.discipline;
                ddl.search = search;
                ddl.reffIds = reffIds;

                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.discipline;
                ddl.typeId = typeId;
                ddl.search = search;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.discipline;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.discipline;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.discipline;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(DDLSearch ddl)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                ddl.section = ProjConstant.DDL.discipline;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
    }

    public static class DDLSpeciality
    {
        public static List<EntityDDL> GetAll(int typeId = 0)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.speciality;
                ddl.typeId = typeId;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.speciality;
                ddl.search = search;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search, string reffIds)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.speciality;
                ddl.search = search;
                ddl.reffIds = reffIds;

                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.speciality;
                ddl.typeId = typeId;
                ddl.search = search;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.speciality;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.speciality;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.speciality;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(DDLSearch ddl)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                ddl.section = ProjConstant.DDL.speciality;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
    }
    public static class DDLInstitute
    {
        public static List<EntityDDL> GetAll(int typeId = 0)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.institute;
                ddl.typeId = typeId;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.institute;
                ddl.search = search;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.institute;
                ddl.typeId = typeId;
                ddl.search = search;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.institute;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.institute;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.institute;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(DDLSearch ddl)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                ddl.section = ProjConstant.DDL.institute;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
    }

    public static class DDLHospital
    {
        public static List<EntityDDL> GetAll(int typeId = 0)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.hospital;
                ddl.typeId = typeId;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.hospital;
                ddl.search = search;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search, string reffIds)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.hospital;
                ddl.search = search;
                ddl.reffIds = reffIds;

                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.hospital;
                ddl.typeId = typeId;
                ddl.search = search;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.hospital;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.hospital;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.hospital;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(DDLSearch ddl)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                ddl.section = ProjConstant.DDL.hospital;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
    }

    public static class DDLDepartment
    {
        public static List<EntityDDL> GetAll(int typeId = 0)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.department;
                ddl.typeId = typeId;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.department;
                ddl.search = search;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search, string reffIds)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.department;
                ddl.search = search;
                ddl.reffIds = reffIds;

                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.department;
                ddl.typeId = typeId;
                ddl.search = search;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.department;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.department;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.department;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(DDLSearch ddl)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                ddl.section = ProjConstant.DDL.department;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
    }

    public static class DDLUnit
    {
        public static List<EntityDDL> GetAll(int typeId = 0)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.unit;
                ddl.typeId = typeId;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.unit;
                ddl.search = search;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search, string reffIds)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.unit;
                ddl.search = search;
                ddl.reffIds = reffIds;

                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.unit;
                ddl.typeId = typeId;
                ddl.search = search;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.unit;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.unit;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.unit;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(DDLSearch ddl)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                ddl.section = ProjConstant.DDL.unit;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
    }

    public static class DDLEmployee
    {
        public static List<EntityDDL> GetAll(int typeId = 0)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.employee;
                ddl.typeId = typeId;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.employee;
                ddl.search = search;


                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }

        public static List<EntityDDL> GetAll(string search, string reffIds)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try

            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.employee;
                ddl.search = search;
                ddl.reffIds = reffIds;

                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.employee;
                ddl.typeId = typeId;
                ddl.search = search;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.employee;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.employee;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(int typeId, int reffId, string reffIds, string search = "")
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                DDLSearch ddl = new DDLSearch();
                ddl.section = ProjConstant.DDL.employee;
                ddl.typeId = typeId;
                ddl.search = search;
                ddl.reffId = reffId;
                ddl.reffIds = reffIds;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
        public static List<EntityDDL> GetAll(DDLSearch ddl)
        {
            List<EntityDDL> list = new List<EntityDDL>();
            try
            {
                ddl.section = ProjConstant.DDL.employee;
                list = new CommonDAL().SearchDDL(ddl).OrderBy(x => x.value).ToList();
            }
            catch (Exception)
            {
                list = new List<EntityDDL>();
            }
            return list;
        }
    }
}