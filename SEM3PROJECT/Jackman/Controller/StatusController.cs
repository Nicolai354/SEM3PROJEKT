using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jackman.Data;
using Jackman.Models;

namespace Jackman.Controller
{
    public class StatusController
    {
        IStatusData statusData;

        public StatusController(IStatusData statusData)
        {
            this.statusData = statusData;
        }

        public IEnumerable<Status> GetStatuses()
        {
            return statusData.GetStatuses();
        }
    }
}
