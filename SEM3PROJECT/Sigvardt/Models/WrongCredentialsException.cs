using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sigvardt.Models
{
    public class WrongCredentialsException : Exception
    {
        public WrongCredentialsException()
            : base("Wrong username or password!") { }
    }
}