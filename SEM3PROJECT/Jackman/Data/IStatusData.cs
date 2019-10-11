using Jackman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackman.Data
{
    public interface IStatusData
    {
        Status GetStatus(int id);

        IEnumerable<Status> GetStatuses();
    }
}
