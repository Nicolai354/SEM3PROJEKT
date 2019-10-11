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
    class CategoryData : ICategoryData
    {
        public IEnumerable<Category> GetCategories(bool fullAssociation = false)
        {
            try
            {
                DataTable dt = new DataAccessLayer().ExecuteDataTable("SELECT * FROM category");
                return dt.AsEnumerable().Select(r => MapCategory(r, fullAssociation));
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        public Category GetCategoryForSubcategory(int subcategoryId, bool fullAssociation = false)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@subcategoryId", subcategoryId, DbType.Int32);
                DataTable dt = dal.ExecuteDataTable("SELECT * FROM [category] WHERE [id] = (SELECT [category_id] FROM [subcategory] WHERE [subcategory].id = @subcategoryId)");
                dal.ClearParameters();

                return dt.Rows.Count == 1 ? MapCategory(dt.Rows[0], fullAssociation) : null;
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        private Category MapCategory(DataRow row, bool fullAssociation = false)
        {
            return new Category()
            {
                Id = (int)row["id"],
                Name = row["name"].ToString(),
                Description = row["description"].ToString(),
                Subcategories = fullAssociation ? new SubcategoryData().GetSubcategories((int)row["id"]) : null
            };
        }
    }
}