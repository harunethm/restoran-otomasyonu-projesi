using EntityLayer.Concrete;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestoranOtomasyonuProjesi.Controllers
{
    public class PaymentController : Controller
    {
        OrderManager om = new OrderManager(new DalEfOrder());
        ReceiptManager rm = new ReceiptManager(new DalEfReceipt());

        // GET: Payment
        public ActionResult Index(int receiptID = 3)
        {
            List<Order> orders = om.GetByReceipID(receiptID);
            double totalPrice = rm.GetByID(receiptID).Total;

            ViewBag.totalPrice = totalPrice;
            ViewBag.orders = orders;
            return View();
        }
    }
}