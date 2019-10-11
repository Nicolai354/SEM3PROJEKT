using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jackman.Models;
using Jackman.Models.Exceptions;

namespace Jackman.Data
{
    public class CustomerData : ICustomerData
    {
        public Customer GetCustomer(int id)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@customerId", id, DbType.Int32);
                DataTable dt = dal.ExecuteDataTable("SELECT * FROM [vwCustomer] WHERE [id] = @customerId");
                dal.ClearParameters();

                if (dt.Rows.Count == 1)
                    return MapCustomer(dt.Rows[0]);
                else
                    throw new DoesNotExistException(typeof(Customer));
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        public Customer GetCustomer(string mail)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@mail", mail, DbType.String);
                DataTable dt = dal.ExecuteDataTable("SELECT * FROM [vwCustomer] WHERE [mail] = @mail");
                dal.ClearParameters();

                if (dt.Rows.Count == 1)
                    return MapCustomer(dt.Rows[0]);
                else
                    throw new DoesNotExistException(typeof(Customer));
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        //internal: Access is limited exclusively to classes defined within the current project assembly
        internal static Customer MapCustomer(DataRow row)
        {
            return new Customer
            {
                Id = (int)row["id"],
                Name = row["name"].ToString(),
                Company = new CompanyData().GetCompany((int)row["company_id"]),
                Mail = row["mail"].ToString(),
                Phone = row["phone"].ToString()
            };
        }

        public Credentials GetCredentials(string mail)
        {
            if (String.IsNullOrEmpty(mail))
                return null;

            DataAccessLayer dal = new DataAccessLayer();
            dal.AddParameter("@mail", mail, DbType.Int32);
            DataTable dt = dal.ExecuteDataTable("SELECT * FROM vwCredentials WHERE [mail] = @mail");
            dal.ClearParameters();

            return dt.Rows.Count == 1 ? new Credentials { Hash = (byte[])dt.Rows[0]["password"], Salt = (byte[])dt.Rows[0]["salt"] } : null;
        }
    }
}
