using Jackman.Data;
using Jackman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackman.Controller
{
    public class SubcategoryController
    {
        ISubcategoryData data;

        public SubcategoryController(ISubcategoryData data)
        {
            this.data = data;
        }

        public IEnumerable<Subcategory> GetSubcategories(int categoryId)
        {
            return data.GetSubcategories(categoryId);
        }
    }
}