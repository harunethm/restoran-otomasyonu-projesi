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
    [Authorize]
    public class OrderController : Controller
    {
        CashRegisterManager crm = new CashRegisterManager(new DalEfCashRegister());
        TakeAwayManager tam = new TakeAwayManager(new DalEfTakeAway());
        CategoryManager cm = new CategoryManager(new DalEfCategory());
        ProductManager pm = new ProductManager(new DalEfProduct());
        TableManager tm = new TableManager(new DalEfTable());
        OrderManager om = new OrderManager(new DalEfOrder());
        UserManager um = new UserManager(new DalEfUser());

        #region masa işlemleri
        [HttpGet]
        public ActionResult Tables()
        {
            ViewBag.tables = tm.ListAll().OrderBy(x => x.Name).ToList();
            return View();
        }

        public ActionResult TableClick(string type, int id)
        {
            if (!string.IsNullOrEmpty(type))
            {
                if (crm.IsDayStarted() != null)
                {
                    Table table = tm.GetByID(id);
                    //TODO receipt oluştur ve id sini table a ata
                    switch (type)
                    {
                        case "rezervation":
                            table.Availability = 2;
                            tm.Update(table);
                            return Json(new { errMessage = "", confirm = true, destination = Url.Action("Tables", "Order") }, JsonRequestBehavior.AllowGet);
                        case "order":
                            return Json(new { errMessage = "", confirm = true, destination = Url.Action("TableOrder", "Order", new { tableID = id }) }, JsonRequestBehavior.AllowGet);
                        case "payment":
                            return Json(new { errMessage = "", confirm = true, destination = Url.Action("Index", "Payment", new { receiptID = table.ReceiptID }) }, JsonRequestBehavior.AllowGet);
                        default:
                            break;
                    }
                }
                else
                {
                    return Json(new { errMessage = "Admin tarafından gün başı yapılması bekleniyor.", confirm = false });
                }
            }

            return Json(new { errMessage = "An error has occured.", confirm = false });
        }

        public ActionResult TableInfo(int id)
        {
            Table t = tm.GetByID(id);
            List<Order> ordersInCart = om.GetInCartByReceiptID((int)t.ReceiptID);
            string[,] orders = new string[ordersInCart.Count, 3];
            int i = 0;
            foreach (var item in ordersInCart)
            {
                orders[i, 0] = item.Product.Name;
                orders[i, 1] = item.Amount + "";
                orders[i, 2] = item.Product.Price + "";
                i++;
            }
            return Json(new { orders }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region tüm siparişler
        [HttpGet]
        public ActionResult Orders()
        {
            // TODO paket siparişleri de bu ekrana dahil et
            List<Order> orders = om.ListAll();
            List<Table> tables = tm.GetTablesForOrders();
            ViewBag.orders = orders;
            ViewBag.tables = tables;
            return View();
        }

        public ActionResult EditOrder(int id, int amount)
        {
            if (amount >= 0)
            {
                Order order = om.GetByID(id);
                order.Amount = amount;
                om.Update(order);
                return Json(new { confirm = true, errMessage = "" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { confirm = false, errMessage = "Sipariş miktarı eksi olamaz." }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult OrderStatusChange(int id, int status)
        {
            Order order = om.GetByID(id);
            order.Status = status;
            om.Update(order);
            return Json(new { confirm = true, errMessage = "" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region paket sipariş işlemleri
        [HttpGet]
        public ActionResult TakeAway()
        {
            ViewBag.takeAways = tam.ListAll();
            ViewBag.categories = cm.ListAll();
            ViewBag.products = pm.ListAll();
            return View();
        }

        [HttpPost]
        public ActionResult TakeAway(TakeAway takeAway)
        {
            // TODO adres bilgisini, siparişleri al ve bir fiş oluşturup bütün hepsini kaydet
            return View();
        }
        #endregion

        #region masa siparişi işlemleri
        [HttpGet]
        public ActionResult TableOrder(int tableID)
        {
            ViewBag.categories = cm.GetForMenu();
            ViewBag.products = pm.ListAll();
            int receiptID = (int)tm.GetByID(tableID).ReceiptID;
            ViewBag.orders = om.GetInCartByReceiptID(receiptID);
            ViewBag.table = tm.GetByID(Convert.ToInt32(tableID));

            return View();
        }

        public ActionResult ConfirmOrder(int tableID)
        {
            return Json(new { errMessage = "", confirm = false, destination = ""}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CancelOrder(int tableID)
        {
            return Json(new { errMessage = "", confirm = false, destination = "" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddOrder(int productID, int amount)
        {
            return Json(new { errMessage = "", confirm = false, destination = "" }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}