using Jackman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackman.Data
{
    public interface ICategoryData
    {
        IEnumerable<Category> GetCategories(bool fullAssociation = false);
    }
}
