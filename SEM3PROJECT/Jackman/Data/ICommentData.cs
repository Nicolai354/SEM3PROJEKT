using Jackman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackman.Data
{
    public interface ICommentData
    {
        IEnumerable<Comment> GetComments(int caseId);
        void CreateComment(int caseId, int personId, string text);
    }
}
