using System;
using System.Collections.Generic;
using System.Linq;
using Jackman.Models;
using Jackman.Models.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jackman.Tests
{
    [TestClass]
    public class CaseTest
    {
        [TestMethod]
        public void TestCaseCreate()
        {
            Controller.CaseController caseCtrl = new Controller.CaseController(new CaseMockup());

            //Test that case can't be null
            Assert.ThrowsException<ArgumentNullException>(() => caseCtrl.CaseCreate(null), "Failed at testing Case as null");

            //Test that Description must be set
            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseCreate(new Case
            {
                Description = null,
                Customer = new Customer { Id = 1 },
                OperatingSystem = "OS",
                Priority = 1,
                Subcategory = new Subcategory { Id = 1 }
            }), "Failed at testing Description");

            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseCreate(new Case
            {
                Description = "",
                Customer = new Customer { Id = 1 },
                OperatingSystem = "OS",
                Priority = 1,
                Subcategory = new Subcategory { Id = 1 }
            }), "Failed at testing Description");

            //Test that Customer must be set
            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseCreate(new Case
            {
                Description = "Description",
                Customer = null,
                OperatingSystem = "OS",
                Priority = 1,
                Subcategory = new Subcategory { Id = 1 }
            }), "Failed at testing Customer");

            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseCreate(new Case
            {
                Description = "Description",
                Customer = new Customer { Id = 0 },
                OperatingSystem = "OS",
                Priority = 1,
                Subcategory = new Subcategory { Id = 1 }
            }), "Failed at testing Customer");

            //Test that OperatingSystem must be set
            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseCreate(new Case
            {
                Description = "Description",
                Customer = new Customer { Id = 1 },
                OperatingSystem = null,
                Priority = 1,
                Subcategory = new Subcategory { Id = 1 }
            }), "Failed at testing OperatingSystem");

            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseCreate(new Case
            {
                Description = "Description",
                Customer = new Customer { Id = 1 },
                OperatingSystem = "",
                Priority = 1,
                Subcategory = new Subcategory { Id = 1 }
            }), "Failed at testing OperatingSystem");

            //Test that Priority must be set correectly
            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseCreate(new Case
            {
                Description = "Description",
                Customer = new Customer { Id = 1 },
                OperatingSystem = "OS",
                Priority = 0,
                Subcategory = new Subcategory { Id = 1 }
            }), "Failed at testing Priority");

            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseCreate(new Case
            {
                Description = "Description",
                Customer = new Customer { Id = 1 },
                OperatingSystem = "OS",
                Priority = 6,
                Subcategory = new Subcategory { Id = 1 }
            }), "Failed at testing Priority");

            //Test that Subcategory must be set
            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseCreate(new Case
            {
                Description = "Description",
                Customer = new Customer { Id = 1 },
                OperatingSystem = "OS",
                Priority = 1,
                Subcategory = null
            }), "Failed at testing Subcategory");

            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseCreate(new Case
            {
                Description = "Description",
                Customer = new Customer { Id = 1 },
                OperatingSystem = "OS",
                Priority = 1,
                Subcategory = new Subcategory { Id = 0 }
            }), "Failed at testing Subcategory");

            //Test that case gets created when all data is set correctly
            Assert.AreEqual(caseCtrl.CaseCreate(new Case
            {
                Description = "Description",
                Customer = new Customer { Id = 1 },
                OperatingSystem = "OS",
                Priority = 1,
                Subcategory = new Subcategory { Id = 1 }
            }), 1, "Failed at testing correct creation");
        }

        [TestMethod]
        public void TestGetCase()
        {
            Controller.CaseController ctrl = new Controller.CaseController(new CaseMockup());

            //Test that id must be > 0
            Assert.ThrowsException<DoesNotExistException>(() => ctrl.GetCase(0));

            //Test that a case gets returned with correct argument
            Assert.AreEqual(ctrl.GetCase(1).Id, 1);
        }

        [TestMethod]
        public void TestCaseEdit()
        {
            //Missing method to get a case, to check if it works
            Controller.CaseController caseCtrl = new Controller.CaseController(new CaseMockup());

            //Test that case can't be null
            Assert.ThrowsException<ArgumentNullException>(() => caseCtrl.CaseEdit(null, 1));

            //Test that case.Id must be set
            Assert.ThrowsException<DoesNotExistException>(() => caseCtrl.CaseEdit(
                new Case
                {
                    Id = 0,
                    Description = "Description",
                    Customer = new Customer { Id = 1 },
                    OperatingSystem = "OS",
                    Priority = 1,
                    Subcategory = new Subcategory { Id = 1 },
                    Supporter = new Supporter { Id = 1 }
                }, 1), "Failed at testing Case.Id");

            //Test that Description must be set
            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseEdit(
                new Case
                {
                    Description = null,
                    Customer = new Customer { Id = 1 },
                    OperatingSystem = "OS",
                    Priority = 1,
                    Subcategory = new Subcategory { Id = 1 },
                    Supporter = new Supporter { Id = 1 }
                }, 1), "Failed at testing Description");

            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseEdit(new Case
            {
                Description = "",
                Customer = new Customer { Id = 1 },
                OperatingSystem = "OS",
                Priority = 1,
                Subcategory = new Subcategory { Id = 1 },
                Supporter = new Supporter { Id = 1 }
            }, 1), "Failed at testing Description");

            //Test that Customer must be set
            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseEdit(new Case
            {
                Description = "Description",
                Customer = null,
                OperatingSystem = "OS",
                Priority = 1,
                Subcategory = new Subcategory { Id = 1 },
                Supporter = new Supporter { Id = 1 }
            }, 1), "Failed at testing Customer");

            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseEdit(new Case
            {
                Description = "Description",
                Customer = new Customer { Id = 0 },
                OperatingSystem = "OS",
                Priority = 1,
                Subcategory = new Subcategory { Id = 1 },
                Supporter = new Supporter { Id = 1 }
            }, 1), "Failed at testing Customer");

            //Test that OperatingSystem must be set
            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseEdit(new Case
            {
                Description = "Description",
                Customer = new Customer { Id = 1 },
                OperatingSystem = null,
                Priority = 1,
                Subcategory = new Subcategory { Id = 1 },
                Supporter = new Supporter { Id = 1 }
            }, 1), "Failed at testing OperatingSystem");

            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseEdit(new Case
            {
                Description = "Description",
                Customer = new Customer { Id = 1 },
                OperatingSystem = "",
                Priority = 1,
                Subcategory = new Subcategory { Id = 1 },
                Supporter = new Supporter { Id = 1 }
            }, 1), "Failed at testing OperatingSystem");

            //Test that Priority must be set correectly
            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseEdit(new Case
            {
                Description = "Description",
                Customer = new Customer { Id = 1 },
                OperatingSystem = "OS",
                Priority = 0,
                Subcategory = new Subcategory { Id = 1 },
                Supporter = new Supporter { Id = 1 }
            }, 1), "Failed at testing Priority");

            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseEdit(new Case
            {
                Description = "Description",
                Customer = new Customer { Id = 1 },
                OperatingSystem = "OS",
                Priority = 6,
                Subcategory = new Subcategory { Id = 1 },
                Supporter = new Supporter { Id = 1 }
            }, 1), "Failed at testing Priority");

            //Test that Subcategory must be set
            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseEdit(new Case
            {
                Description = "Description",
                Customer = new Customer { Id = 1 },
                OperatingSystem = "OS",
                Priority = 1,
                Subcategory = null,
                Supporter = new Supporter { Id = 1 }
            }, 1), "Failed at testing Subcategory");

            Assert.ThrowsException<ArgumentException>(() => caseCtrl.CaseEdit(new Case
            {
                Description = "Description",
                Customer = new Customer { Id = 1 },
                OperatingSystem = "OS",
                Priority = 1,
                Subcategory = new Subcategory { Id = 0 },
                Supporter = new Supporter { Id = 1 }
            }, 1), "Failed at testing Subcategory");

            //Test that editingSupporterId must be set
            Assert.ThrowsException<DoesNotExistException>(() => caseCtrl.CaseEdit(
                new Case
                {
                    Id = 1,
                    Description = "Description",
                    Customer = new Customer { Id = 1 },
                    OperatingSystem = "OS",
                    Priority = 1,
                    Subcategory = new Subcategory { Id = 1 },
                    Supporter = new Supporter { Id = 1 }
                }, 0), "Failed at testing editingSupporterId");

            //Test that only the correct supporter can edit a case
            Assert.ThrowsException<NotAuthorizedException>(() => caseCtrl.CaseEdit(
                new Case
                {
                    Id = 1,
                    Description = "Description",
                    Customer = new Customer { Id = 1 },
                    OperatingSystem = "OS",
                    Priority = 1,
                    Subcategory = new Subcategory { Id = 1 },
                    Supporter = new Supporter { Id = 1 }
                }, 2), "Failed at testing wrong editingSupporterId");

            //Check that a case can be edited
            try
            {
                caseCtrl.CaseEdit(
                new Case
                {
                    Id = 1,
                    Description = "Description",
                    Customer = new Customer { Id = 1 },
                    OperatingSystem = "OS",
                    Priority = 1,
                    Subcategory = new Subcategory { Id = 1 },
                    Supporter = new Supporter { Id = 1 }
                }, 1);
            }
            catch (Exception)
            {
                Assert.Fail("Failed to edit case");
            }
        }

        [TestMethod]
        public void TestCaseTake()
        {
            Controller.CaseController ctrl = new Controller.CaseController(new CaseMockup());
            
            //Test that controller throws exception on missing id's
            Assert.ThrowsException<DoesNotExistException>(() => ctrl.CaseTake(0, 0));
            Assert.ThrowsException<DoesNotExistException>(() => ctrl.CaseTake(0, 1));
            Assert.ThrowsException<DoesNotExistException>(() => ctrl.CaseTake(1, 0));

            //Test that controller accepts good parameters
            try
            {
                ctrl.CaseTake(1, 1);
            }
            catch (Exception)
            {
                Assert.Fail("Could not take a case");
            }
        }

        [TestMethod]
        public void TestGetCases()
        {
            Controller.CaseController ctrl = new Controller.CaseController(new CaseMockup());

            Assert.AreEqual(ctrl.GetCases().Count(), 2);

            List<Case> customerCases = ctrl.GetCasesForCustomer("mail").ToList();
            Assert.AreEqual(customerCases.Count, 3);

            List<Case> supporterCases = ctrl.GetCasesForSupporter(3).ToList();
            Assert.AreEqual(supporterCases.Count, 4);
        }

        private class CaseMockup : Data.ICaseData
        {
            public void CaseChangeStatus(int caseId, int statusId, int supporterId)
            {
                
            }

            public int CaseCreate(Case c)
            {
                return 1;
            }

            public void CaseEdit(Case c, int s)
            {

            }
            
            public void CaseTake(int c, int s)
            {

            }

            public Case GetCase(int id)
            {
                return new Case { Id = id, Supporter = new Supporter { Id = 1 } };
            }

            public IEnumerable<Case> GetCases()
            {
                return new List<Case>
                {
                    new Case{Id = 1 },
                    new Case{Id = 2 }
                };
            }

            public IEnumerable<Case> GetCasesForCustomer(string mail)
            {
                return new List<Case>
                {
                    new Case { Id = 3 },
                    new Case { Id = 4 },
                    new Case { Id = 5 }
                };
            }

            public IEnumerable<Case> GetCasesForSupporter(int s)
            {
                return new List<Case>
                {
                    new Case { Id = 6 },
                    new Case { Id = 7 },
                    new Case { Id = 8 },
                    new Case { Id = 9 }
                };
            }

            public Case GetCustomersCase(int caseId, string mail)
            {
                throw new NotImplementedException();
            }
        }
    }
}
