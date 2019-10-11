using Sigvardt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigvardt.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            ServiceController serviceController = new ServiceController();

            try
            {
                serviceController.Authenticate(username, password);
                ViewBag.ErrorMessage = null;

                return RedirectToAction("Index");
            }
            catch (WrongCredentialsException)
            {
                ViewBag.ErrorMessage = "Forkert brugernavn eller adgangskode!";
            }
            catch (Exception)
            {
                ViewBag.ErrorMessage = "Der skete en fejl!";
            }

            return View();
        }

        public ActionResult Logout()
        {
            new ServiceController().Logout();

            return RedirectToAction("Login");
        }
    }
}