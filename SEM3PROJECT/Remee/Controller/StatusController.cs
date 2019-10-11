using Remee.JackmanService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remee.Controller
{
    public class StatusController
    {
        RemeeSupportClient client = new RemeeSupportClient();

        public List<Status> GetStatuses()
        {
            return client.GetStatuses().ToList();
        }
    }
}
