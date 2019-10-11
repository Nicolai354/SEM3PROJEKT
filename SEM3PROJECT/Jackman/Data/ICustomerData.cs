using Jackman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackman.Data
{
    public interface ICustomerData
    {
        Customer GetCustomer(int id);
        Customer GetCustomer(string mail);
        Credentials GetCredentials(string mail);
    }
}
