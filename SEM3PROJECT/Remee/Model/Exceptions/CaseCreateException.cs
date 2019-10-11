using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remee.Model.Exceptions
{
    public class CaseCreateException : Exception
    {
        public CaseCreateException(string message)
            : base(message)
        { }
    }
}
