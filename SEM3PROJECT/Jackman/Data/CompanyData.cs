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
    public class CompanyData : ICompanyData
    {
        public Company GetCompany(int id)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@id", id, DbType.Int32);
                DataTable dt = dal.ExecuteDataTable("SELECT * FROM [company] WHERE [id] = @id");
                dal.ClearParameters();

                if (dt.Rows.Count == 1)
                    return MapCompany(dt.Rows[0]);
                else
                    throw new DoesNotExistException(typeof(Company));
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        private Company MapCompany(DataRow row)
        {
            return new Company
            {
                Id = (int)row["id"],
                Name = row["name"].ToString(),
                Mail = row["mail"].ToString(),
                Phone = row["phone"].ToString()
            };
        }
    }
}
