using System;
using System.Collections.Generic;
using System.Linq;
using Jackman.Data;
using Jackman.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jackman.Tests
{
    [TestClass]
    public class SubcategoryTest
    {
        [TestMethod]
        public void TestGetSubcategories()
        {
            Controller.SubcategoryController ctrl = new Controller.SubcategoryController(new SubcategoryMockup());

            List<Subcategory> subcategories = ctrl.GetSubcategories(2).ToList();

            Assert.AreEqual(subcategories[1].Id, 2);
            Assert.AreEqual(subcategories[0].Name, "sub1 til cat2");
        }
    }

    class SubcategoryMockup : ISubcategoryData
    {
        public IEnumerable<Subcategory> GetSubcategories(int categoryId)
        {
            List<Subcategory> subcategories = new List<Subcategory>();
            for (int i = 1; i <= 5; i++)
            {
                subcategories.Add(new Subcategory()
                {
                    Id = i,
                    Name = $"sub{i} til cat{categoryId}",
                    Description = "new description"
                });
            }
            return subcategories;
        }

        public Subcategory GetSubcategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}