using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RestoranOtomasyonuProjesi.Controllers
{
    public class LogInController : Controller
    {
        private readonly CategoryManager cm = new CategoryManager(new DalEfCategory());
        private readonly ProductManager pm = new ProductManager(new DalEfProduct());
        private readonly UserManager um = new UserManager(new DalEfUser());

        [HttpGet]
        public ActionResult Entry()
        {
            ViewBag.categories = cm.ListAll();
            ViewBag.products = pm.ListAll();
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            List<User> users = um.ListAll();
            ViewBag.users = users;
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(User u)
        {
            u.Password = u.Password == null ? "" : u.Password;
            User user = um.GetByID(u.UserID);
            List<User> users = um.ListAll();
            ViewBag.users = users;
            try
            {
                if (user != null && u.Password.Equals(user.Password))
                {
                    return user.AuthorityLevel == 2 ? RedirectToAction("Istatistik", "Admin") : RedirectToAction("Menu", "Home");
                }
                else
                {
                    ViewBag.u = u;
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }


        public ActionResult LogOut()
        {
            return RedirectToAction("Entry", "LogIn");
        }
    }
}