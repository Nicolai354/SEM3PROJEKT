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
    class SubcategoryData : ISubcategoryData
    {
        public IEnumerable<Subcategory> GetSubcategories(int categoryId)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@category_id", categoryId, DbType.Int32);
                DataTable dt = dal.ExecuteDataTable("SELECT * FROM subcategory WHERE [category_id] = @category_id");
                dal.ClearParameters();

                return dt.AsEnumerable().Select(r => MapSubcategory(r));
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        public Subcategory GetSubcategory(int id)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@id", id, DbType.Int32);
                DataTable dt = dal.ExecuteDataTable("SELECT * FROM [subcategory] WHERE [id] = @id");
                dal.ClearParameters();

                if (dt.Rows.Count == 1)
                    return MapSubcategory(dt.Rows[0]);
                else
                    throw new DoesNotExistException(typeof(Subcategory));
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        private Subcategory MapSubcategory(DataRow row)
        {
            return new Subcategory
            {
                Id = (int)row["id"],
                Name = row["name"].ToString(),
                Description = row["description"].ToString()
            };
        }
    }
}
