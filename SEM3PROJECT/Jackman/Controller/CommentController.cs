using Jackman.Data;
using Jackman.Models;
using Jackman.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackman.Controller
{
    public class CommentController
    {
        ICommentData commentData;

        public CommentController(ICommentData commentData)
        {
            this.commentData = commentData;
        }
        public IEnumerable<Comment> GetComments(int caseId)
        {
            if (caseId <= 0)
                throw new DoesNotExistException(typeof(Case));

            return commentData.GetComments(caseId);
        }

        public void CreateComment(int caseId, int personId, string text)
        {
            if (caseId <= 0)
                throw new DoesNotExistException(typeof(Case));

            if (personId <= 0)
                throw new DoesNotExistException(typeof(Person));

            if (String.IsNullOrEmpty(text))
                throw new ArgumentException("Text can not be empty.");

            commentData.CreateComment(caseId, personId, text);
        }
    }
}
