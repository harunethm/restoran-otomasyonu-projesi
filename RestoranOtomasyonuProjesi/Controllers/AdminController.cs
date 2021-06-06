using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestoranOtomasyonuProjesi.Controllers
{
    public class AdminController : Controller
    {
        UserManager um = new UserManager(new DalEfUser());
        CategoryManager cm = new CategoryManager(new DalEfCategory());
        ProductManager pm = new ProductManager(new DalEfProduct());
        CashRegisterManager crm = new CashRegisterManager(new DalEfCashRegister());

        [HttpGet]
        public ActionResult Users()
        {
            List<User> users = um.ListAll();
            ViewBag.users = users;
            return View();
        }

        [HttpPost]
        public ActionResult Users(FormCollection fc)
        {

                //User p = um.GetByID(Convert.ToInt32(fc["btnDelete"]));
                //um.DeleteUser(p);

            return RedirectToAction("Users");
        }

        [HttpGet]
        public ActionResult Categories()
        {
            ViewBag.categories = cm.ListAll();
            return View();
        }

        [HttpPost]
        public ActionResult Categories(FormCollection fc)
        {
            return RedirectToAction("Categories");
        }

        [HttpGet]
        public ActionResult Products()
        {
            List<Product> products = pm.ListAll();
            List<Category> categories = cm.ListAll();
            ViewBag.products = products;
            ViewBag.categories = categories;
            return View();
        }

        [HttpPost]
        public ActionResult Products(Product p)
        {
            return RedirectToAction("Products");
        }

        public ActionResult Statistics()
        {
            List<Statistic> statistics = new List<Statistic>() { 
                new Statistic() { StatisticHeader="En Çok Satan Ürün", StatisticValue= "Beyti"},
                new Statistic() { StatisticHeader="En Çok Satan Kategori", StatisticValue= "Kırmızı Et"},
                new Statistic() { StatisticHeader="Kâr", StatisticValue= "10.000 TL"},
                new Statistic() { StatisticHeader="Zarar", StatisticValue= "10 TL"},
                new Statistic() { StatisticHeader="asd", StatisticValue= "asdasdasd"},
                new Statistic() { StatisticHeader="dsa", StatisticValue= "adsa"},
                new Statistic() { StatisticHeader="asd", StatisticValue= "asdasdasdasdasdasdasdasdasdasdasdsdasdas"},
            };
            ViewBag.statistics = statistics;
            return View();
        }

        [HttpGet]
        public ActionResult CashRegister()
        {
            List<CashRegister> CashRegisters = crm.ListAll();
            ViewBag.cashRegisters = CashRegisters;
            return View();
        }

        [HttpPost]
        public ActionResult CashRegister(CashRegister p)
        {
            return RedirectToAction("CashRegister");
        }

        public ActionResult SignOut()
        {
            return View();
        }
    }
}