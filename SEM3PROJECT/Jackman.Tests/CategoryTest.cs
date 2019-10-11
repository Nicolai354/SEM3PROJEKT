using System;
using System.Collections.Generic;
using System.Linq;
using Jackman.Data;
using Jackman.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jackman.Tests
{
    [TestClass]
    public class CategoryTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            Controller.CategoryController ctrl = new Controller.CategoryController(new CategoryMockup());

            List<Category> categories = ctrl.GetCategories().ToList();

            Assert.AreEqual(categories.Count, 3);

            Assert.AreEqual(categories[1].Name, "Software");
        }
    }

    class CategoryMockup : ICategoryData
    {
        public IEnumerable<Category> GetCategories(bool fullAssociation = false)
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Hardware",
                    Description = "Alt om hardware",
                    Subcategories = new List<Subcategory>()
                    {
                        new Subcategory(){ Id = 1, Name = "Mus", Description = "Sager med mus" },
                        new Subcategory(){ Id = 2, Name = "Tastatur", Description = "Sager om tastaturer" }
                    }
                },
                new Category()
                {
                    Id = 2,
                    Name = "Software",
                    Description = "Alt om software",
                    Subcategories = new List<Subcategory>()
                    {
                        new Subcategory() { Id = 3, Name = "Word", Description = "Sager om Word" },
                        new Subcategory() { Id = 4, Name = "Paint", Description = "Sager om paint" }
                    }
                },
                new Category()
                {
                    Id = 3,
                    Name = "Telefon",
                    Description = "Alt om telefoner",
                    Subcategories = new List<Subcategory>()
                    {
                        new Subcategory() { Id = 5, Name = "iPhone", Description = "Alt om iPhones" }
                    }
                }
            };
        }
    }
}
