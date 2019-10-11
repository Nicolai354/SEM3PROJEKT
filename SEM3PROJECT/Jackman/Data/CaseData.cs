using Jackman.Models;
using Jackman.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackman.Data
{
    public class CaseData : ICaseData
    {
        public int CaseCreate(Case c)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@description", c.Description, DbType.String);
                dal.AddParameter("@priority", c.Priority, DbType.String);
                dal.AddParameter("@subcategoryId", c.Subcategory.Id, DbType.Int32);
                dal.AddParameter("@operatingSystem", c.OperatingSystem, DbType.String);
                dal.AddParameter("@customerId", c.Customer.Id, DbType.Int32);
                dal.AddParameter("@newId", null, DbType.Int32, ParameterDirection.Output);
                dal.ExecuteStoredProcedure("CaseCreate");
                int newId = Int32.Parse(dal.GetParameter("@newId").Value.ToString());
                dal.ClearParameters();

                return newId;
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        public void CaseEdit(Case c, int editingSupporterId)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@caseId", c.Id, DbType.Int32);
                dal.AddParameter("@description", c.Description, DbType.String);
                dal.AddParameter("@priority", c.Priority, DbType.Int32);
                dal.AddParameter("@subcategoryId", c.Subcategory.Id, DbType.Int32);
                dal.AddParameter("@operatingSystem", c.OperatingSystem, DbType.String);
                //The editing supporter. Not the supporter assigned to the case.
                dal.AddParameter("@editingSupporterId", editingSupporterId, DbType.Int32);
                dal.ExecuteStoredProcedure("CaseEdit");
                dal.ClearParameters();
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        public void CaseTake(int caseId, int supporterId)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@caseId", caseId, DbType.Int32);
                dal.AddParameter("@supporterId", supporterId, DbType.Int32);
                dal.ExecuteStoredProcedure("CaseTake");
                dal.ClearParameters();
                
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        public void CaseChangeStatus(int caseId, int statusId, int supporterId)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                dal.AddParameter("@caseId", caseId, DbType.Int32);
                dal.AddParameter("@statusId", statusId, DbType.Int32);
                dal.AddParameter("@editingSupporterId", supporterId, DbType.Int32);
                dal.ExecuteStoredProcedure("CaseChangeStatus");
                dal.ClearParameters();
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        public Case GetCustomersCase(int id, string mail)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@id", id, DbType.Int32);
                dal.AddParameter("@mail", mail, DbType.String);
                DataTable dt = dal.ExecuteDataTable("SELECT * FROM [case] WHERE [id] = @id AND [customer_id] = (SELECT [id] FROM [vwCustomer] WHERE [mail] = @mail)");
                dal.ClearParameters();

                if (dt.Rows.Count != 1)
                    throw new DoesNotExistException(typeof(Case));
                else
                    return MapCase(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        public Case GetCase(int id)
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@id", id, DbType.Int32);
                DataTable dt = dal.ExecuteDataTable("SELECT * FROM [case] WHERE [id] = @id");
                dal.ClearParameters();

                if (dt.Rows.Count != 1)
                    throw new DoesNotExistException(typeof(Case));
                else
                    return MapCase(dt.Rows[0]);
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        public IEnumerable<Case> GetCases()
        {
            try
            {
                DataAccessLayer dal = new DataAccessLayer();
                DataTable dt = dal.ExecuteDataTable("SELECT * FROM [case]");

                return dt.AsEnumerable().Select(r => MapCase(r));
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        public IEnumerable<Case> GetCasesForCustomer(string mail)
        {
            //No reason to query DB if we know that no records exist
            if (String.IsNullOrEmpty(mail))
                return new Case[0];

            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@mail", mail, DbType.String);
                DataTable dt = dal.ExecuteDataTable("SELECT * FROM [case] WHERE [customer_id] = (SELECT [Id] FROM [vwCustomer] WHERE [mail] = @mail)");
                dal.ClearParameters();

                return dt.AsEnumerable().Select(r => MapCase(r));
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        public IEnumerable<Case> GetCasesForSupporter(int supporterId)
        {
            //No reason to query DB if we know that no records exist
            if (supporterId <= 0)
                return new Case[0];

            try
            {
                DataAccessLayer dal = new DataAccessLayer();

                dal.AddParameter("@supporterId", supporterId, DbType.Int32);
                DataTable dt = dal.ExecuteDataTable("SELECT * FROM [case] WHERE [supporter_id] = @supporterId");
                dal.ClearParameters();

                return dt.AsEnumerable().Select(r => MapCase(r));
            }
            catch (Exception ex)
            {
                throw DataExceptionCheck.CheckException(ex);
            }
        }

        private Case MapCase(DataRow row)
        {
            return new Case
            {
                Id = (int)row["id"],
                CreatedDate = (DateTime)row["createdDate"],
                Customer = new CustomerData().GetCustomer((int)row["customer_id"]),
                Description = row["description"].ToString(),
                OperatingSystem = row["operatingSystem"].ToString(),
                Priority = (int)row["priority"],
                Status = new StatusData().GetStatus((int)row["status_id"]),
                Subcategory = new SubcategoryData().GetSubcategory((int)row["subcategory_id"]),
                Category = new CategoryData().GetCategoryForSubcategory((int)row["subcategory_id"]),
                Supporter = row["supporter_id"] == DBNull.Value ? null : new SupporterData().GetSupporter((int)row["supporter_id"])
            };
        }
    }
}