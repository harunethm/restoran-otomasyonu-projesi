using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestoranOtomasyonuProjesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly CategoryManager cm = new CategoryManager(new DalEfCategory());
        private readonly ProductManager pm = new ProductManager(new DalEfProduct());

        [HttpGet]
        public ActionResult Menu()
        {
            ViewBag.categories = cm.ListAll();
            ViewBag.products = pm.ListAll();
            return View();
        }
    }
}