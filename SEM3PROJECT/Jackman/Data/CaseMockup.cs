using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jackman.Models;

namespace Jackman.Data
{
    class CaseMockup : ICaseData
    {
        public int CaseCreate(Case c)
        {
            return -1;
        }

        public bool CaseEdit(Case c, Supporter s)
        {
            return c.Id > 0;
        }
    }
}
