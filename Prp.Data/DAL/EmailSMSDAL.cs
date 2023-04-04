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
    public class EmailSMSDAL : PrpDBConnect
    {
        public DataTable GetEmailStatusRemaningRecords(string condition)
        {
            SqlCommand cmd = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "[dbo].[spEmailStatusRemaningRecords]"
            };
            cmd.Parameters.AddWithValue("@condition", condition);

            return PrpDbADO.FillDataTable(cmd);
        }
    }
}
