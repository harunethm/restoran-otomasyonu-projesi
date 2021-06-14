using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestoranOtomasyonuProjesi.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly CategoryManager cm = new CategoryManager(new DalEfCategory());
        private readonly ProductManager pm = new ProductManager(new DalEfProduct());

        [HttpGet]
        public ActionResult Menu()
        {
            ViewBag.categories = cm.GetForMenu();
            ViewBag.products = pm.ListAll();
            return View();
        }
    }
}