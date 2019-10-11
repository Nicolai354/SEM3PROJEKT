using Jackman.Models;
using Jackman.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackman.Data
{
    public class SupporterData : ISupporterData
    {
        public Supporter GetSupporter(int id)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@id", id, DbType.Int32);
                DataTable dt = dal.ExecuteDataTable("SELECT * FROM [vwSupporter] WHERE [id] = @id");
                dal.ClearParameters();

                if (dt.Rows.Count == 1)
                    return MapSupporter(dt.Rows[0]);
                else
                    throw new DoesNotExistException(typeof(Supporter));
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        public IEnumerable<Supporter> GetSupporters()
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                DataTable dt = dal.ExecuteDataTable("SELECT * FROM [vwSupporter]");
                
                return dt.AsEnumerable().Select(r => MapSupporter(r));
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        //internal: Access is limited exclusively to classes defined within the current project assembly
        internal static Supporter MapSupporter(DataRow row)
        {
            return new Supporter
            {
                Id = (int)row["id"],
                Name = row["name"].ToString(),
                Mail = row["mail"].ToString(),
                Phone = row["phone"].ToString()
            };
        }
    }
}
