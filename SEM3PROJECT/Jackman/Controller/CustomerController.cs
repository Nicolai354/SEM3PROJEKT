using Jackman.Data;
using Jackman.Models;
using Jackman.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Selectors;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Jackman.Controller
{
    public class CustomerController : UserNamePasswordValidator
    {
        private ICustomerData customerData;

        public CustomerController()
            : this(null) { }

        public CustomerController(ICustomerData customerData)
        {
            this.customerData = customerData ?? new CustomerData();
        }

        public Customer GetCustomer(int id)
        {
            if (id <= 0)
                throw new DoesNotExistException(typeof(Customer));

            return customerData.GetCustomer(id);
        }

        public Customer GetCustomer(string mail)
        {
            if (String.IsNullOrEmpty(mail))
                throw new DoesNotExistException(typeof(Customer));

            return customerData.GetCustomer(mail);
        }

        public override void Validate(string mail, string password)
        {
            //Get credentials from datasource
            Credentials credentials = customerData.GetCredentials(mail);

            //If credentials are null, customer is not found, thus wrong credentials
            //If PasswordHash.Verify fails, the password is wrong, thus wrong credentials
            if (credentials == null || !new PasswordHash(credentials.Salt, credentials.Hash).Verify(password))
                throw new WebFaultException<string>("Invalid username or password!", System.Net.HttpStatusCode.Forbidden);
        }

        //https://stackoverflow.com/questions/2659214/why-do-i-need-to-use-the-rfc2898derivebytes-class-in-net-instead-of-directly
        private sealed class PasswordHash
        {
            const int SaltSize = 16, HashSize = 20, HashIter = 10000;
            readonly byte[] _salt, _hash;
            public PasswordHash(string password)
            {
                new RNGCryptoServiceProvider().GetBytes(_salt = new byte[SaltSize]);
                _hash = new Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize);
            }
            public PasswordHash(byte[] hashBytes)
            {
                Array.Copy(hashBytes, 0, _salt = new byte[SaltSize], 0, SaltSize);
                Array.Copy(hashBytes, SaltSize, _hash = new byte[HashSize], 0, HashSize);
            }
            public PasswordHash(byte[] salt, byte[] hash)
            {
                Array.Copy(salt, 0, _salt = new byte[SaltSize], 0, SaltSize);
                Array.Copy(hash, 0, _hash = new byte[HashSize], 0, HashSize);
            }
            public byte[] ToArray()
            {
                byte[] hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(_salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(_hash, 0, hashBytes, SaltSize, HashSize);
                return hashBytes;
            }
            public byte[] Salt { get { return (byte[])_salt.Clone(); } }
            public byte[] Hash { get { return (byte[])_hash.Clone(); } }
            public bool Verify(string password)
            {
                byte[] test = new Rfc2898DeriveBytes(password, _salt, HashIter).GetBytes(HashSize);
                for (int i = 0; i < HashSize; i++)
                    if (test[i] != _hash[i])
                        return false;
                return true;
            }
        }
    }
}
