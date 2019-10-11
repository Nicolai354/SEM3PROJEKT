using Jackman.Data;
using Jackman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackman.Controller
{
    public class SupporterController
    {
        ISupporterData supporterData;

        public SupporterController(ISupporterData supporterData)
        {
            this.supporterData = supporterData;
        }

        public IEnumerable<Supporter> GetSupporters()
        {
            return supporterData.GetSupporters();
        }
    }
}
