using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remee.Model.Exceptions
{
    class CaseEditException : Exception
    {
        public CaseEditException(string message)
            : base(message)
        { }
    }
}
