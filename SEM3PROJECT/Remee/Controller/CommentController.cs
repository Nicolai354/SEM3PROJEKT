using Remee.JackmanService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remee.Controller
{
    public class CommentController
    {
        RemeeSupportClient client = new RemeeSupportClient();


        public List<Comment> GetComments(int caseId)
        {

            if (caseId <= 0)
                throw new ArgumentException("Case ID could not be found");
            //client = new RemeeSupportClient();

            using (var client = new RemeeSupportClient())
            {
                return client.GetComments(caseId).ToList();
            }
        }

        public void CreateComment(int caseId, Person person, string text)
        {
            if (caseId <= 0)
                throw new ArgumentException("Case id missing");
            if (person.Id <= 0)
                throw new ArgumentException("Person id missing");
            if (text.Length <= 0)
                throw new ArgumentException("Text can not be empty");

            client.CreateComment(caseId, person.Id, text);
        }
    }
}
