using Jackman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Jackman
{
    [ServiceContract]
    interface IRemeeSupport
    {
        [OperationContract]
        void CaseEdit(Case c, int editingSupporterId);

        [OperationContract]
        IEnumerable<Case> GetCases();

        [OperationContract]
        IEnumerable<Case> GetCasesForSupporter(int supporterId);

        [OperationContract]
        int CaseCreate(Case c);

        [OperationContract]
        Case GetCase(int id);

        [OperationContract]
        void CaseTake(int caseId, int supporterId);

        [OperationContract]
        IEnumerable<Category> GetCategories();

        [OperationContract]
        IEnumerable<Subcategory> GetSubcategories(int categoryId);

        [OperationContract]
        IEnumerable<Comment> GetComments(int caseId);

        [OperationContract]
        void CreateComment(int caseId, int personId, string text);

        [OperationContract]
        IEnumerable<Status> GetStatuses();

        [OperationContract]
        void CaseChangeStatus(int caseId, int StatusId, int SupporterId);
        [OperationContract]

        IEnumerable<Supporter> GetSupporters();
    }
}
