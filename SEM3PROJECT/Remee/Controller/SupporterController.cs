using Remee.JackmanService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remee.Controller
{
    public class SupporterController
    {
        public static Supporter LoggedInSupporter { get; set; }

        public List<Supporter> GetSupporters()
        {
            using (var client = new RemeeSupportClient())
            {
                return client.GetSupporters().ToList();
            }
        }
    }
}
