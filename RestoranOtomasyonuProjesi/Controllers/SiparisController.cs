using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;

namespace RestoranOtomasyonuProjesi.Controllers
{
    public class SiparisController : Controller
    {
        TableManager tm = new TableManager(new DalEfTable());
        public ActionResult Index()
        {
            var tables = tm.ListAll();
            return View(tables);
        }
        
        [HttpGet]
        public ActionResult AddTable()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddTable(Table p)
        {
            tm.AddTable(p);
            return RedirectToAction("Index");
        }
    }
}