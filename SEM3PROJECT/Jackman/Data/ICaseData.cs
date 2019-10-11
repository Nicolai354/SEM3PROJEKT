using Jackman.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jackman.Data
{
    public interface ICaseData
    {
        int CaseCreate(Case c);
        void CaseEdit(Case c, int editingSupporterId);
        IEnumerable<Case> GetCases();
        IEnumerable<Case> GetCasesForCustomer(string mail);
        IEnumerable<Case> GetCasesForSupporter(int supporterId);
        void CaseTake(int caseId, int supporterId);
        Case GetCase(int caseId);
        Case GetCustomersCase(int caseId, string mail);
        void CaseChangeStatus(int caseId, int statusId, int supporterId);
    }
}
