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
        PaymentManager pm = new PaymentManager(new DalEfPayment());
        TableManager tm = new TableManager(new DalEfTable());
        CashRegisterManager crm = new CashRegisterManager(new DalEfCashRegister());

        [HttpGet]
        public ActionResult Index(int receiptID)
        {
            List<Order> orders = om.GetByReceiptID(receiptID).OrderBy(x => x.Product.CategoryID).ToList();
            double totalPrice = rm.GetByID(receiptID).Total;

            ViewBag.totalPrice = totalPrice;
            ViewBag.orders = orders;
            return View();
        }

        public ActionResult Pay(int[] orders, float discount, int paymentMethod)
        {
            List<Order> paidOrders = new List<Order>();
            double total = 0;
            if (orders != null)
            {
                for (int i = 0; i < orders.Length; i++)
                {
                    if (paidOrders.Count(x => x.OrderID == orders[i]) == 0)
                    {
                        Order ord = om.GetByID(orders[i]);
                        ord.AmountPaid++;
                        paidOrders.Add(ord);
                        total += ord.Product.Price;
                        if (ord.Amount == ord.AmountPaid)
                            ord.Status = 3;
                    }
                    else
                    {
                        paidOrders.Find(x => x.OrderID == orders[i]).AmountPaid += 1;
                        total += paidOrders.Find(x => x.OrderID == orders[i]).Product.Price;
                        if (paidOrders.Find(x => x.OrderID == orders[i]).Amount == paidOrders.Find(x => x.OrderID == orders[i]).AmountPaid)
                            paidOrders.Find(x => x.OrderID == orders[i]).Status = 3;
                    }
                }

                foreach (var item in paidOrders)
                {
                    Payment payment = new Payment()
                    {
                        Amount = item.AmountPaid,
                        PaymentDateTime = DateTime.Now,
                        OrderID = item.OrderID,
                        PaymentMethod = paymentMethod,
                        Total = total,
                    };
                    
                    pm.Add(payment);
                    om.Update(item);
                }
                CashRegister cashRegister = crm.IsDayStarted();
                if (paymentMethod == 2)
                {
                    cashRegister.AmountServed += total;
                }
                else
                {
                    cashRegister.AmountEarned += total - discount;
                    cashRegister.AmountDiscount += discount;
                }
                crm.Update(cashRegister);


                Receipt receipt = rm.GetByID(om.GetByID(orders[0]).ReceiptID);
                receipt.Discount += discount;
                receipt.Paid += total;
                if (receipt.Paid >= receipt.Total - receipt.Discount)
                {
                    receipt.Status = false;
                }
                rm.Update(receipt);


                if (om.GetByReceiptID(receipt.ReceiptID).Count(x => x.Status > 2) == om.GetByReceiptID(receipt.ReceiptID).Count()) // verilen tüm siparişler hazır, iptal edilmiş veya geçmiş sipariş ise
                {
                    Table t = tm.GetByReceiptID(receipt.ReceiptID);
                    t.Availability = 0;
                    t.OpeningDate = null;
                    t.ReceiptID = null;
                    tm.Update(t);
                    return Json(new { confirm = true, destination = Url.Action("Tables", "Order") }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { confirm = false }, JsonRequestBehavior.AllowGet);

            }

            return Json(new { confirm = false }, JsonRequestBehavior.AllowGet);
        }

    }
}