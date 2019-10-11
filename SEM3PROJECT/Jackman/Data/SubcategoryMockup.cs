using Jackman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackman.Data
{
    class SubcategoryMockup : ISubcategoryData
    {
        public List<Subcategory> GetSubcategories(int categoryId)
        {
            List<Subcategory> subcategories = new List<Subcategory>();
            for (int i = 1; i <= 5; i++)
            {
                subcategories.Add(new Subcategory()
                {
                    Id = i,
                    Name = $"sub{i} til cat{categoryId}",
                    Description = "new description"
                });
            }
            return subcategories;
        }
    }
}
