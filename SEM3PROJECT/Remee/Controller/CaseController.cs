using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Remee.JackmanService;
using Remee.Model.Exceptions;

namespace Remee.Controller
{
    public class CaseController
    {
        RemeeSupportClient client = new RemeeSupportClient();

        public Case CreateCase(string OS, int priority, string description, Category category, Subcategory subcategory)
        {
            Case c = new Case();
            if (OS != null && priority >= 1 && priority <= 5 && description != null)
            {
                c.OperatingSystem = OS;
                c.Priority = priority;
                c.Description = description;
                c.Customer = new Customer { Id = 5 };
                c.Subcategory = subcategory;
                c.Category = category;

                c.Id = client.CaseCreate(c);
            }
            else
            {
                throw new CaseCreateException("Case is not constructed correctly");
            }

            return c;
        }

        public Case GetCase(int id)
        {
            if (id < 1)
                throw new ArgumentException("Id can not be less than 1");
            
            return client.GetCase(id);
        }

        /// <summary>
        /// Edit a case, if editing supporter is the one assigned to the case
        /// </summary>
        /// <param name="c">Updated case</param>
        public void CaseEdit(Case c)
        {
            if (c == null)
                throw new CaseEditException("Case can't be null");
            
            if (c.Id <= 0 ||                                        //Id has to be set and greater than 0
                String.IsNullOrEmpty(c.OperatingSystem) ||          //OperatingSystem has to be set and not empty
                c.Priority < 1 || c.Priority > 5 ||                 //Priority has to be between 1 and 5
                c.Subcategory == null || c.Subcategory.Id <= 0 ||   //Subcategory has to be set and have an Id
                c.Customer == null || c.Customer.Id <= 0 ||         //Customer has to be set and have an Id
                String.IsNullOrEmpty(c.Description))                //Description has to be set and not empty
                throw new CaseEditException("Case is not constructed correctly");

            try
            {
                client.CaseEdit(c, SupporterController.LoggedInSupporter.Id);
            }
            catch (Exception e)
            {
                throw e;
                //throw new CaseEditException("There was an error with the service");
            }
        }

        public void CaseTake(Case c)
        {
            if (c == null)
                throw new ArgumentNullException("case");

            if (c.Id < 1)
                throw new ArgumentException("Case is not constructed correctly");

            client.CaseTake(c.Id, SupporterController.LoggedInSupporter.Id);
        }

        public void CaseChangeStatus(Case @case, int statusId, int supporterId)
        {
            if (@case == null)
                throw new ArgumentNullException("Case cant be null");
            if (@case.Id < 1)
                throw new ArgumentNullException("CaseId can not be less than 1");
            if (@case.Supporter?.Id != supporterId)
                throw new Exception("This Supporter cant edit this case");
            if (statusId < 1)
                throw new ArgumentNullException("StatusId can not be less than 1");
            if (supporterId < 1)
                throw new ArgumentNullException("SupporterId can not be less than 1");

            client.CaseChangeStatus(@case.Id, statusId, supporterId);
        }
    }
}