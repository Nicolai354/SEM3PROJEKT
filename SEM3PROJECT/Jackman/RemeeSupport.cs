using Jackman.Controller;
using Jackman.Data;
using Jackman.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel.Web;
using Jackman.Models.Exceptions;

namespace Jackman
{
    class RemeeSupport : IRemeeSupport
    {
        public void CaseEdit(Case c, int editingSupporterId)
        {
            RunCode(() => new CaseController(new CaseData()).CaseEdit(c, editingSupporterId));
        }

        public void CaseTake(int caseId, int supporterId)
        {
            RunCode(() => new CaseController(new CaseData()).CaseTake(caseId, supporterId));
        }

        public int CaseCreate(Case c)
        {
            return RunCode(() => new CaseController(new CaseData()).CaseCreate(c));
        }

        public IEnumerable<Case> GetCases()
        {
            return RunCode(() => new CaseController(new CaseData()).GetCases());
        }

        public IEnumerable<Case> GetCasesForSupporter(int supporterId)
        {
            return RunCode(() => new CaseController(new CaseData()).GetCasesForSupporter(supporterId));
        }

        public IEnumerable<Category> GetCategories()
        {
            return RunCode(() => new CategoryController(new CategoryData()).GetCategories());
        }

        public IEnumerable<Subcategory> GetSubcategories(int categoryId)
        {
            return RunCode(() => new SubcategoryController(new SubcategoryData()).GetSubcategories(categoryId));
        }

        public Case GetCase(int id)
        {
            return RunCode(() => new CaseController(new CaseData()).GetCase(id));
        }

        public IEnumerable<Comment> GetComments(int caseId)
        {
            return RunCode(() => new CommentController(new CommentData()).GetComments(caseId));
        }

        public void CreateComment(int caseId, int personId, string text)
        {
            RunCode(() => new CommentController(new CommentData()).CreateComment(caseId, personId, text));
        }

        public IEnumerable<Status> GetStatuses()
        {
            return RunCode(() => new StatusController(new StatusData()).GetStatuses());
        }

        public void CaseChangeStatus(int caseId, int statusId, int supporterId)
        {
            RunCode(() => new CaseController(new CaseData()).CaseChangeStatus(caseId, statusId, supporterId));
        }

        public IEnumerable<Supporter> GetSupporters()
        {
            return RunCode(() => new SupporterController(new SupporterData()).GetSupporters());
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