using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Sigvardt.JackmanService;

namespace Sigvardt.Controllers
{
    public class CaseController : Controller
    {
        ISigvardtService client;

        public CaseController()
            : this(null) { }
        public CaseController(ISigvardtService client)
        {
            this.client = client;
        }

        // GET: Case
        //public ActionResult Index()
        //{
        //    JackmanService.SigvardtServiceClient client = new SigvardtServiceClient();
        //    Case[] cases = client.GetCasesForCustomer(new Customer { Id = 1 });

        //    return View(cases);
        //}

        public ActionResult Index(string sortOrder, string filter)
        {
            IEnumerable<Case> cases = new ServiceController().GetClient(client).GetCasesForCustomer().ToList();

            if (!string.IsNullOrEmpty(filter))
                cases = cases.Where(c =>
                c.Customer.Name.Contains(filter)
                || c.OperatingSystem.Contains(filter)
                || c.Status.Name.Contains(filter)
                || c.Description.Contains(filter)
                || c.Category.Name.Contains(filter)
                || c.Subcategory.Name.Contains(filter)
                || c.Id.ToString().Contains(filter)
                || c.Priority.ToString().Contains(filter));

            sortOrder = sortOrder == null ? "" : sortOrder.ToLower();

            ViewBag.IdSort = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            //                      IF                          THEN        ELSE
            ViewBag.CreatedDateSort = sortOrder == "date" ? "date_desc" : "date";
            ViewBag.DescriptionSort = sortOrder == "description" ? "description_desc" : "description";
            ViewBag.OperatingSystemSort = sortOrder == "operatingsystem" ? "operatingsystem_desc" : "operatingsystem";
            ViewBag.PrioritySort = sortOrder == "priority" ? "priority_desc" : "priority";
            ViewBag.SupporterSort = sortOrder == "supporter" ? "supporter_desc" : "supporter";
            ViewBag.StatusSort = sortOrder == "status" ? "status_desc" : "status";
            ViewBag.CategorySort = sortOrder == "category" ? "category_desc" : "category";
            ViewBag.SubcategorySort = sortOrder == "subcategory" ? "subcategory_desc" : "subcategory";

            Func<Case, object> sorting = c => c.Id;

            switch (sortOrder.Split('_')[0])
            {
                case "priority":
                    sorting = c => c.Priority;
                    break;
                case "description":
                    sorting = c => c.Description;
                    break;
                case "category":
                    sorting = c => c.Category.Name;
                    break;
                case "subcategory":
                    sorting = c => c.Subcategory.Name;
                    break;
                case "status":
                    sorting = c => c.Status.Name;
                    break;
                case "operatingsystem":
                    sorting = c => c.OperatingSystem;
                    break;
                case "createddate":
                    sorting = c => c.CreatedDate;
                    break;
            }

            var sortedCases = sortOrder.EndsWith("_desc") ? cases.OrderByDescending(sorting) : cases.OrderBy(sorting);

            return View(sortedCases);

        }

        // GET: Case/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Case c = new ServiceController().GetClient(client).GetCase(id);

                return View(c);
            }
            catch (Exception)
            {
                return RedirectToAction("Index");
            }
        }

        // GET: Case/Create
        public ActionResult Create()
        {
            ViewBag.Categories = new ServiceController().GetClient(client).GetCategories().ToList();

            return View(new Case());
        }

        [HttpPost]
        public JsonResult GetSubcategories(int id)
        {
            //GetSubcategories

            return Json(new ServiceController().GetClient(client).GetSubcategories(id), JsonRequestBehavior.AllowGet);
        }

        // POST: Case/Create
        [HttpPost]
        public ActionResult Create(Case @case, FormCollection collection)
        {
            if (!Int32.TryParse(collection["Subcategory"], out int subcategoryId) || subcategoryId <= 0)
            {
                ViewBag.ErrorMessage = "Underkategori er ikke udfyldt!";
                ViewBag.Categories = new ServiceController().GetClient(client).GetCategories().ToList();
                return View(@case);
            }
            else
                @case.Subcategory = new Subcategory { Id = subcategoryId };

            if (@case.Priority < 1 || @case.Priority > 5)
            {
                ViewBag.ErrorMessage = "Prioritet skal være mellem 1 og 5";
                ViewBag.Categories = new ServiceController().GetClient(client).GetCategories().ToList();
                return View(@case);
            }

            if (String.IsNullOrEmpty(@case.OperatingSystem))
            {
                ViewBag.ErrorMessage = "Operativ-system er ikke udfyldt!";
                ViewBag.Categories = new ServiceController().GetClient(client).GetCategories().ToList();
                return View(@case);
            }

            if (String.IsNullOrEmpty(@case.Description))
            {
                ViewBag.ErrorMessage = "Beskrivelse er ikke udfyldt!";
                ViewBag.Categories = new ServiceController().GetClient(client).GetCategories().ToList();
                return View(@case);
            }

            try
            {
                int caseId = new ServiceController().GetClient().CaseCreate(@case);

                if (caseId > 0)
                    return RedirectToAction($"{caseId}");
                else
                {
                    ViewBag.ErrorMessage = "Sagen blev ikke oprettet, prøv igen.";
                    ViewBag.Categories = new ServiceController().GetClient(client).GetCategories().ToList();
                    return View(@case);
                }
            }
            catch (Exception e)
            {
                ViewBag.ErrorMessage = e.Message;
                ViewBag.Categories = new ServiceController().GetClient(client).GetCategories().ToList();
                return View();
            }
        }

        //// GET: Case/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Case/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction($"{id}");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Case/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Case/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //public ActionResult Comment(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here
        //        int caseId = new ServiceController().GetClient(client).CaseCreate(new Case
        //        {
        //            Subcategory = new Subcategory { Id = int.Parse(collection["Subcategory"]) },
        //            Description = collection["Description"],
        //            OperatingSystem = collection["OperatingSystem"],
        //            Priority = int.Parse(collection["Priority"]),
        //            Customer = new Customer { Id = 1 }
        //        });

        //        if (caseId > 0)
        //            return RedirectToAction($"{caseId}");
        //        else
        //            return new HttpStatusCodeResult(501);
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
