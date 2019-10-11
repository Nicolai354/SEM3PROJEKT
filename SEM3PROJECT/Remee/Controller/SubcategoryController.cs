using Remee.JackmanService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remee.Controller
{
    public class SubcategoryController
    {
        RemeeSupportClient client = new RemeeSupportClient();

        public List<Subcategory> GetSubcategories(Category category)
        {
            return client.GetSubcategories(category.Id).ToList();
        }
    }
}
