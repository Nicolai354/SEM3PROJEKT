using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;
using Jackman.Models;

namespace Jackman
{
    [ServiceContract]
    interface ISigvardtService
    {
        [OperationContract]
        int CaseCreate(Case c);

        [OperationContract]
        IEnumerable<Category> GetCategories();

        [OperationContract]
        IEnumerable<Subcategory> GetSubcategories(int categoryId);

        [OperationContract]
        IEnumerable<Case> GetCasesForCustomer();

        [OperationContract]
        void CreateComment(int caseId, string text);

        [OperationContract]
        IEnumerable<Comment> GetComments(int caseId);

        [OperationContract]
        Case GetCase(int id);
    }
}
