using System;
using System.Collections.Generic;
using Jackman.Models;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Jackman.Models.Exceptions;

namespace Jackman.Tests
{
    [TestClass]
    public class CommentTest
    {
        [TestMethod]
        public void TestGetComments()
        {
            Controller.CommentController ctrl = new Controller.CommentController(new CommentMockup());
            Assert.ThrowsException<DoesNotExistException>(() => ctrl.GetComments(0));

            Assert.AreEqual(1, ctrl.GetComments(1).ToList().Count);
        }

        [TestMethod]
        public void TestCreateComment()
        {
            Controller.CommentController ctrl = new Controller.CommentController(new CommentMockup());

            Assert.ThrowsException<DoesNotExistException>(() => ctrl.CreateComment(0, 0, "test"));
            Assert.ThrowsException<DoesNotExistException>(() => ctrl.CreateComment(0, 2, "test"));
            Assert.ThrowsException<DoesNotExistException>(() => ctrl.CreateComment(2, 0, "test"));
            Assert.ThrowsException<ArgumentException>(() => ctrl.CreateComment(2, 2, ""));

            try
            {
                ctrl.CreateComment(1, 1, "test");
            }
            catch (Exception)
            {
                Assert.Fail("Could not create comment");
            }
        }

        private class CommentMockup : Data.ICommentData
        {
            public IEnumerable<Comment> GetComments(int caseId)
            {
                return new List<Comment>()
                {
                    new Comment()
                };
            }

            public void CreateComment(int caseId, int personId, string text)
            {

            }
        }
    }
}
