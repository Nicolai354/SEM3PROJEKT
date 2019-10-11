using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sigvardt.JackmanService;

namespace Sigvardt.Controllers
{
    public class CommentController : Controller
    {
        ISigvardtService client;

        public CommentController()
            : this(null) { }
        public CommentController(ISigvardtService client)
        {
            this.client = client;
        }
        // GET: Comment
        public ActionResult Index(int caseId)
        {
            var comments = new ServiceController().GetClient(client).GetComments(caseId);
            return PartialView(comments);
        }

        [HttpPost]
        public ActionResult Index(int caseId, string text)
        {
            client = new ServiceController().GetClient(client);

            try
            {
                // TODO: Add insert logic here
                client.CreateComment(caseId, text);
                
                var comments = client.GetComments(caseId);
                return PartialView(comments);
            }
            catch (Exception)
            {
                return PartialView();
            }
        }

        // GET: Comment/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Comment/Create
        //public ActionResult Create(int caseId)
        //{
        //    return PartialView();
        //}

        //// POST: Comment/Create
        //[HttpPost]
        //public ActionResult Create(int caseId, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here
        //        SigvardtServiceClient client = new SigvardtServiceClient();
        //        client.CreateComment(caseId, 5, collection["Text"]);

        //        return PartialView();
        //    }
        //    catch (Exception)
        //    {
        //        return PartialView();
        //    }
        //}

        // GET: Comment/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: Comment/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        // GET: Comment/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        // POST: Comment/Delete/5
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
    }
}
