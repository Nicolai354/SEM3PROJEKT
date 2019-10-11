using Jackman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackman.Data
{
    public interface ISubcategoryData
    {
        IEnumerable<Subcategory> GetSubcategories(int categoryId);
        Subcategory GetSubcategory(int id);
    }
}
