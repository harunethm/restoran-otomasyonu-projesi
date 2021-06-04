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
    public class OrderController : Controller
    {
        TableManager tm = new TableManager(new DalEfTable());
        TakeAwayManager tam = new TakeAwayManager(new DalEfTakeAway());
        CategoryManager cm = new CategoryManager(new DalEfCategory());
        ProductManager pm = new ProductManager(new DalEfProduct());
        OrderManager om = new OrderManager(new DalEfOrder());

        [HttpGet]
        public ActionResult Tables()
        {
            List<Table> tables = tm.ListAll();
            ViewBag.tables = tables;
            return View();
        }

        [HttpPost]
        public ActionResult Tables(string id)
        {
            return RedirectToAction("TableOrder", "Order", id);
        }

        [HttpGet]
        public ActionResult Orders()
        {
            ViewBag.orders = om.ListAll();
            ViewBag.tables= tm.ListAll();
            return View();
        }

        [HttpPost]
        public ActionResult Orders(FormCollection fc)
        {

            return View();
        }


        [HttpGet]
        public ActionResult TakeAway()
        {
            List<TakeAway> takeAways = tam.ListAll();
            ViewBag.takeAways = takeAways;
            ViewBag.categories = cm.ListAll();
            ViewBag.products = pm.ListAll();
            return View();
        }


        [HttpPost]
        public ActionResult TakeAway(TakeAway takeAway)
        {
            return View();
        }


        [HttpGet]
        public ActionResult TableOrder(string id)
        {
            ViewBag.categories = cm.ListAll();
            ViewBag.products = pm.ListAll();
            //int _id = Convert.ToInt32(id);
            //Table t = tm.GetByID(_id);
            //ViewBag.table = t;
            return View();
        }

        [HttpPost]
        public ActionResult TableOrder(Table table, FormCollection fc, string siparisAzalt, string _button)
        {
            ViewBag.categories = cm.ListAll();
            ViewBag.products = pm.ListAll();
            var id = fc["siparisAzalt"];
            if (_button != null && _button.Equals("odemeAl"))
                return RedirectToAction("Index", "Payment");
            return RedirectToAction("TableOrder", "Order");
        }
    }
}