using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sigvardt.JackmanService;

namespace Sigvardt.Tests.Controllers
{
    [TestClass]
    public class CaseControllerTest
    {
        [TestMethod]
        public void TestCaseCreate()
        {
            Sigvardt.Controllers.CaseController caseController = new Sigvardt.Controllers.CaseController(new SigvardtServiceMockup());

            caseController.Create();

            Assert.IsNotNull(caseController.ViewBag.Categories);
        }

        [TestMethod]
        public void TestCaseDetails()
        {
            Sigvardt.Controllers.CaseController caseController = new Sigvardt.Controllers.CaseController(new SigvardtServiceMockup());

            ViewResult view = (ViewResult)caseController.Details(4);

            Assert.IsNotNull(view.Model);

            Assert.AreEqual(view.Model.GetType(), typeof(Case));

            Assert.AreEqual(((Case)view.Model).Id, 4);
        }

        private class SigvardtServiceMockup : ISigvardtService
        {
            public int CaseCreate(Case c)
            {
                throw new NotImplementedException();
            }

            public Task<int> CaseCreateAsync(Case c)
            {
                throw new NotImplementedException();
            }

            public void CreateComment(int caseId, int personId, string text)
            {
                throw new NotImplementedException();
            }

            public void CreateComment(int caseId, string text)
            {
                throw new NotImplementedException();
            }

            public Task CreateCommentAsync(int caseId, int personId, string text)
            {
                throw new NotImplementedException();
            }

            public Task CreateCommentAsync(int caseId, string text)
            {
                throw new NotImplementedException();
            }

            public Case GetCase(int id)
            {
                return new Case
                {
                    Id = id
                };
            }

            public Task<Case> GetCaseAsync(int id)
            {
                throw new NotImplementedException();
            }

            public Case[] GetCasesForCustomer()
            {
                throw new NotImplementedException();
            }

            public Task<Case[]> GetCasesForCustomerAsync()
            {
                throw new NotImplementedException();
            }

            public Category[] GetCategories()
            {
                return new Category[]
                {
                    new Category{Id = 1 },
                    new Category{Id = 2 },
                    new Category{Id = 3 },
                    new Category{Id = 4 }
                };
            }

            public Task<Category[]> GetCategoriesAsync()
            {
                throw new NotImplementedException();
            }

            public Comment[] GetComments(int caseId)
            {
                throw new NotImplementedException();
            }

            public Task<Comment[]> GetCommentsAsync(int caseId)
            {
                throw new NotImplementedException();
            }

            public Subcategory[] GetSubcategories(int categoryId)
            {
                throw new NotImplementedException();
            }

            public Task<Subcategory[]> GetSubcategoriesAsync(int categoryId)
            {
                throw new NotImplementedException();
            }
        }
    }
}
