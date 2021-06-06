using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
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
        public ActionResult LogIn(string phoneNumber, string password)
        {
            User user = um.GetByPhoneNumber(phoneNumber);
            try
            {
                if (user != null && password != null && user.Password.Equals(password))
                    return user.AuthorityLevel == 2 ? RedirectToAction("Statistics", "Admin") : RedirectToAction("Menu", "Home");
                else
                    return RedirectToAction("LogIn");
            }
            catch (Exception e)
            {
                ViewBag.exception = e.Message;
                return RedirectToAction("LogIn");
            }
        }


        public ActionResult LogOut()
        {
            return RedirectToAction("Entry", "LogIn");
        }
    }
}