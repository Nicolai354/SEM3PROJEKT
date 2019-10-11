using Remee.JackmanService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remee.Controller
{
    public class CategoryController
    {
        RemeeSupportClient client = new RemeeSupportClient();

        public List<Category> GetCategories()
        {
            return client.GetCategories().ToList();
        }
    }
}
