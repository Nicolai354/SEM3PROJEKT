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
    public class StatusData : IStatusData
    {
        public IEnumerable<Status> GetStatuses()
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                DataTable dt = dal.ExecuteDataTable("SELECT * FROM [status]");

                return dt.AsEnumerable().Select(r => MapStatus(r));
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }
        public Status GetStatus(int id)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@id", id, DbType.Int32);
                DataTable dt = dal.ExecuteDataTable("SELECT * FROM [status] WHERE [id] = @id");
                dal.ClearParameters();

                if (dt.Rows.Count == 1)
                    return MapStatus(dt.Rows[0]);
                else
                    throw new DoesNotExistException(typeof(Status));
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        private Status MapStatus(DataRow row)
        {
            return new Status
            {
                Id = (int)row["id"],
                Name = row["name"].ToString(),
                Description = row["description"].ToString()
            };
        }

    }
}
