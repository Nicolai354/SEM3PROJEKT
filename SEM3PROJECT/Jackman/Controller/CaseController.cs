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
    public class CaseController
    {
        ICaseData caseData;

        public CaseController(ICaseData caseData)
        {
            this.caseData = caseData;
        }

        public void CaseEdit(Case c, int editingSupporterId)
        {
            CheckCaseDetails(c);

            if (c.Id < 1)
                throw new DoesNotExistException(typeof(Case));

            if (editingSupporterId <= 0)
                throw new DoesNotExistException(typeof(Supporter));

            if (caseData.GetCase(c.Id).Supporter?.Id != editingSupporterId)
                throw new NotAuthorizedException("The editing supporter can not edit this case.");

            caseData.CaseEdit(c, editingSupporterId);
        }

        public int CaseCreate(Case c)
        {
            CheckCaseDetails(c);

            if (c.Status != null &&                                 //Status should be null, since it is set in DB when created
                c.Supporter != null)                                //Supporter should be null
                throw new ArgumentException("Case is not constructed correctly.");

            return caseData.CaseCreate(c);
        }

        private void CheckCaseDetails(Case c)
        {
            if (c == null)
                throw new ArgumentNullException("Case can not be null.");

            if (String.IsNullOrEmpty(c.OperatingSystem) ||          //OperatingSystem has to be set and not empty
                c.Priority < 1 || c.Priority > 5 ||                 //Priority has to be between 1 and 5
                c.Subcategory == null || c.Subcategory.Id <= 0 ||   //Subcategory has to be set and have an Id
                c.Customer == null || c.Customer.Id <= 0 ||         //Customer has to be set and have an Id
                String.IsNullOrEmpty(c.Description))                //Description has to be set and not empty
                throw new ArgumentException("Case is not constructed correctly."); //Throw exception if any of the above comments are not met
        }

        public IEnumerable<Case> GetCases()
        {
            return caseData.GetCases();
        }
        public IEnumerable<Case> GetCasesForCustomer(string mail)
        {
            return caseData.GetCasesForCustomer(mail);
        }
        public IEnumerable<Case> GetCasesForSupporter(int supporterId)
        {
            return caseData.GetCasesForSupporter(supporterId);
        }
        public void CaseTake(int caseId, int supporterId)
        {
            if (caseId <= 0)
                throw new DoesNotExistException(typeof(Case));

            if (supporterId <= 0)
                throw new DoesNotExistException(typeof(Supporter));

            caseData.CaseTake(caseId, supporterId);
        }

        public Case GetCustomersCase(int id, string mail)
        {
            if (id <= 0)
                throw new DoesNotExistException(typeof(Case));

            if (String.IsNullOrEmpty(mail))
                throw new DoesNotExistException(typeof(Case));

            return caseData.GetCustomersCase(id, mail);
        }

        public Case GetCase(int id)
        {
            if (id <= 0)
                throw new DoesNotExistException(typeof(Case));

            return caseData.GetCase(id);
        }
        public void CaseChangeStatus(int caseId, int statusId, int supporterId)
        {
            if (caseId <= 0)
                throw new DoesNotExistException(typeof(Case));
            if (statusId <= 0)
                throw new DoesNotExistException(typeof(Status));
            if (supporterId <= 0)
                throw new DoesNotExistException(typeof(Supporter));

            caseData.CaseChangeStatus(caseId, statusId, supporterId);
        }
    }
}