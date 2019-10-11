using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jackman.Models;
using Jackman.Models.Exceptions;

namespace Jackman.Data
{
    public class PersonData : IPersonData
    {
        public Person GetPerson(int id)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@id", id, DbType.Int32);
                DataTable dt = dal.ExecuteDataTable("SELECT * FROM [vwPerson] WHERE [id] = @id");
                dal.ClearParameters();

                if (dt.Rows.Count == 1)
                    return MapPerson(dt.Rows[0]);
                else
                    throw new DoesNotExistException(typeof(Person));
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        private Person MapPerson(DataRow row)
        {
            Person p = null;

            if (row["customerId"] != DBNull.Value)
                p = CustomerData.MapCustomer(row);
            else if (row["supporterId"] != DBNull.Value)
                p = SupporterData.MapSupporter(row);

            return p;
        }
    }
}
