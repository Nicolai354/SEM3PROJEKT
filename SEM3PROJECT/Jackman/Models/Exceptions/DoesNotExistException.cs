using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackman.Models.Exceptions
{
    public class DoesNotExistException : Exception
    {
        public DoesNotExistException(Type type)
            : this($"{type} does not exist.") { }

        public DoesNotExistException(string message = "Object does not exist.")
            : base(message) { }
    }
}
