using Jackman.Data;
using Jackman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackman.Controller
{
    public class CategoryController
    {
        ICategoryData categoryData;

        public CategoryController(ICategoryData categoryData)
        {
            this.categoryData = categoryData;
        }
        public IEnumerable<Category> GetCategories()
        {
            return categoryData.GetCategories(false);
        }
    }
}
