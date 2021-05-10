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
    public class MainMenuController : Controller
    {
        CategoryManager cm = new CategoryManager(new DalEfCategory());
        ProductManager pm = new ProductManager(new DalEfProduct());
        public ActionResult Index()
        {
            ViewBag.categories = cm.ListAll();
            ViewBag.products = pm.ListAll();
            return View();
        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product p)
        {
            pm.AddProduct(p);
            return RedirectToAction("Index");
        }
    }
}