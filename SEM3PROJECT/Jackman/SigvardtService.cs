using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using Jackman.Models;
using Jackman.Data;
using Jackman.Controller;
using System.ServiceModel.Web;
using Jackman.Models.Exceptions;

namespace Jackman
{
    class SigvardtService : ISigvardtService
    {
        readonly string mail;

        public SigvardtService()
        {
            mail = OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name;
        }
        public int CaseCreate(Case c)
        {
            c.Customer = new CustomerController().GetCustomer(mail);
            return RunCode(() => new CaseController(new CaseData()).CaseCreate(c));
        }

        public IEnumerable<Case> GetCasesForCustomer()
        {
            return RunCode(() => new CaseController(new CaseData()).GetCasesForCustomer(mail));
        }

        public IEnumerable<Category> GetCategories()
        {
            return RunCode(() => new CategoryController(new CategoryData()).GetCategories());
        }

        public IEnumerable<Subcategory> GetSubcategories(int categoryId)
        {
            return RunCode(() => new SubcategoryController(new SubcategoryData()).GetSubcategories(categoryId));          
        }

        public void CreateComment(int caseId, string text)
        {
            Customer customer = new CustomerController().GetCustomer(mail);
            RunCode(() => new CommentController(new CommentData()).CreateComment(caseId, customer.Id, text));
        }

        public IEnumerable<Comment> GetComments(int caseId)
        {
            return RunCode(() => new CommentController(new CommentData()).GetComments(caseId));
        }

        public Case GetCase(int caseId)
        {
            return RunCode(() => new CaseController(new CaseData()).GetCustomersCase(caseId, mail));
        }

        private void RunCode(Action func)
        {
            try
            {
                func();
            }
            catch (Exception ex)
            {
                throw GetException(ex);
            }
        }

        private T RunCode<T>(Func<T> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                throw GetException(ex);
            }
        }

        private Exception GetException(Exception ex)
        {
            if (ex is ArgumentException)
                return new WebFaultException<string>(ex.Message, System.Net.HttpStatusCode.BadRequest);
            else if (ex is NotFoundException)
                return new WebFaultException<string>(ex.Message, System.Net.HttpStatusCode.NotFound);
            else if (ex is NotAuthorizedException)
                return new WebFaultException<string>(ex.Message, System.Net.HttpStatusCode.Forbidden);
            else
                return new WebFaultException(System.Net.HttpStatusCode.InternalServerError);
        }
    }
}