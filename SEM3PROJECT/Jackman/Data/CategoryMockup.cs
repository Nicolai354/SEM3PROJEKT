using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jackman.Models;

namespace Jackman.Data
{
    class CategoryMockup : ICategoryData
    {
        public List<Category> GetCategories(bool fullAssociation = false)
        {
            return new List<Category>()
            {
                new Category()
                {
                    Id = 1,
                    Name = "Hardware",
                    Description = "Alt om hardware",
                    Subcategories = new List<Models.Subcategory>()
                    {
                        new Models.Subcategory(){ Id = 1, Name = "Mus", Description = "Sager med mus" },
                        new Models.Subcategory(){ Id = 2, Name = "Tastatur", Description = "Sager om tastaturer" }
                    }
                },
                new Category()
                {
                    Id = 2,
                    Name = "Software",
                    Description = "Alt om software",
                    Subcategories = new List<Models.Subcategory>()
                    {
                        new Models.Subcategory() { Id = 3, Name = "Word", Description = "Sager om Word" },
                        new Models.Subcategory() { Id = 4, Name = "Paint", Description = "Sager om paint" }
                    }
                },
                new Category()
                {
                    Id = 3,
                    Name = "Telefon",
                    Description = "Alt om telefoner",
                    Subcategories = new List<Models.Subcategory>()
                    {
                        new Models.Subcategory() { Id = 5, Name = "iPhone", Description = "Alt om iPhones" }
                    }
                }
            };
        }
    }
}
