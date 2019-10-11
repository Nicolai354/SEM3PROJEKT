using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackman.Models
{
    public class Credentials
    {

        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
    }
}
