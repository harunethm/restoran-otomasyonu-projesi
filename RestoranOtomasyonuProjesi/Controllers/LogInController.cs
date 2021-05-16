using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
//using System.Globalization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestoranOtomasyonuProjesi.Controllers
{
    public class LogInController : Controller
    {
        private readonly CategoryManager cm = new CategoryManager(new DalEfCategory());
        private readonly ProductManager pm = new ProductManager(new DalEfProduct());
        private readonly UserManager um = new UserManager(new DalEfUser());

        public ActionResult MainPage()
        {
            ViewBag.categories = cm.ListAll();
            ViewBag.products = pm.ListAll();
            return View();
        }

        [HttpGet]
        public ActionResult LogInPage()
        {
            ViewBag.users = um.ListAll();
            return View();
        }

        [HttpPost]
        public ActionResult LogInPage(User user)
        {
            UserValidator userValidator = new UserValidator();
            ValidationResult validationResult = userValidator.Validate(user);
            if (validationResult.IsValid)
            {
                return RedirectToAction("Menu", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}