using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Security;

namespace RestoranOtomasyonuProjesi.Controllers
{
    public class LogInController : Controller
    {
        private readonly UserManager um = new UserManager(new DalEfUser());

        [HttpGet]
        public ActionResult Entry()
        {
            CategoryManager categoryManager = new CategoryManager(new DalEfCategory());
            ViewBag.categories = categoryManager.ListAll();
            ProductManager productManager = new ProductManager(new DalEfProduct());
            ViewBag.products = productManager.ListAll();
            return View();
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        public ActionResult CheckUser(string phone, string password)
        {
            User user = um.GetByPhoneNumber(phone);

            // kullanıcı var ve şifresi doğru ise
            if (user != null && user.Password.Equals(password)) 
            {
                FormsAuthentication.SetAuthCookie(user.PhoneNumber, false);
                Session["userID"] = user;

                // admin ise admin controller'a değilse home controller'a
                string controller = user.Role == "admin" ? "Admin" : "Home";

                // admin ise statistics action'a değilse menu action'a
                string action = user.Role == "admin" ? "Statistics" : "Menu";

                // kullanıcının son giriş yapma tarihinin güncellenmesi
                user.LastLogin = DateTime.Now;
                um.UpdateUser(user);

                return Json(new { confirm = true, destination = Url.Action(action, controller) }, JsonRequestBehavior.AllowGet);
            }
            else // kullanıcı yok veya şifresi yanlış ise
            {
                return Json(new { confirm = false, destination = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Remove("userID");
            return RedirectToAction("LogIn", "LogIn");
        }
    }
}