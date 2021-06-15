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
    [Authorize]
    public class PaymentController : Controller
    {
        OrderManager om = new OrderManager(new DalEfOrder());
        ReceiptManager rm = new ReceiptManager(new DalEfReceipt());

        // GET: Payment
        public ActionResult Index(int receiptID)
        {
            // TODO ödeme al

            List<Order> orders = om.GetByReceiptID(receiptID);
            double totalPrice = rm.GetByID(receiptID).Total;

            ViewBag.totalPrice = totalPrice;
            ViewBag.orders = orders;
            return View();
        }

        public ActionResult Pay()
        {
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DiziAl(int[] dizi)
        {
            var x = dizi;
            return Json(new { }, JsonRequestBehavior.AllowGet);
        }
    }
}