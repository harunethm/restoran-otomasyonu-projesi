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
        #region manager classes
        CashRegisterManager crm = new CashRegisterManager(new DalEfCashRegister());
        TakeAwayManager tam = new TakeAwayManager(new DalEfTakeAway());
        CategoryManager cm = new CategoryManager(new DalEfCategory());
        ProductManager pm = new ProductManager(new DalEfProduct());
        TableManager tm = new TableManager(new DalEfTable());
        OrderManager om = new OrderManager(new DalEfOrder());
        UserManager um = new UserManager(new DalEfUser());
        ReceiptManager rm = new ReceiptManager(new DalEfReceipt());
        #endregion

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
                    if (table.ReceiptID == null)
                    {
                        Receipt receipt = new Receipt()
                        {
                            Discount = 0,
                            PaymentMethod = 0,
                            ReceiptDate = DateTime.Now,
                            Status = true,
                            Total = 0,
                        };
                        rm.AddReceipt(receipt);
                        receipt = rm.GetLast();
                        table.ReceiptID = receipt.ReceiptID;
                        tm.Update(table);
                    }

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

        public ActionResult TableInfo(int id, int durum)
        {
            List<Order> ordersInCart = new List<Order>();
            if (durum == 0)
                ordersInCart = om.GetInCartByReceiptID(id);
            else
                ordersInCart = om.GetForOrdersPage(id);
            string[,] orders = new string[ordersInCart.Count, 3 + durum];
            int i = 0;
            foreach (var item in ordersInCart)
            {
                orders[i, 0] = item.Product.Name;
                orders[i, 1] = item.Amount + "";
                if (durum != 0)
                    orders[i, 2] = item.Status + "";
                orders[i, 2 + durum] = item.Product.Price + "";
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
            List<Order> orders = om.GetForOrdersPage();
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

        public ActionResult GetInCart(int receiptID)
        {
            List<Order> ordersInCart = om.GetInCartByReceiptID(receiptID);
            string[,] orders = new string[ordersInCart.Count, 2];
            int i = 0;
            foreach (var item in ordersInCart)
            {
                orders[i, 0] = item.ProductID + "";
                orders[i, 1] = item.Amount + "";
                i++;
            }
            return Json(new { orders }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmCancelOrder(int tableID, int status)
        {
            Table table = tm.GetByID(tableID);
            Receipt receipt = rm.GetByID((int)table.ReceiptID);
            List<Order> orders = om.GetInCartByReceiptID(receipt.ReceiptID);
            bool flag = false;
            double total = 0;
            foreach (var item in orders)
            {
                item.Status = status;
                om.Update(item);

                total += item.Product.Price * (item.Amount - item.AmountPaid);

                if (!flag)
                    flag = true;
            }
            if (status == 1 && table.Availability != 1 && flag) // sipariş onay
            {
                table.Availability = 1;
                table.OpeningDate = DateTime.Now;
                tm.Update(table);
                receipt.Total = total;
                rm.Update(receipt);
            }
            return Json(new { errMessage = "", confirm = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OrderPlusMinus(int productID, int receiptID, int amount, bool amountInput, bool plusMinus)
        {
            Receipt receipt = rm.GetByID(receiptID);
            List<Order> ordersInCart = om.GetInCartByReceiptID(receipt.ReceiptID);
            bool flag = true;
            foreach (var item in ordersInCart)
            {
                if (item.ProductID == productID) // eğer ürün zaten sepette varsa miktarını arttır veya azalt
                {
                    if (amountInput)
                        item.Amount = amount;
                    else
                    {
                        if (plusMinus)
                            item.Amount++;
                        else if (!plusMinus)
                        {
                            if (item.Amount <= 1)
                                item.Status = 4;
                            item.Amount--;
                        }
                    }
                    om.Update(item);
                    flag = false; // ürün zaten sepette var yeni ürün eklenmesine gerek yok
                    return Json(new { errMessage = "", confirm = true, newAmount = item.Amount }, JsonRequestBehavior.AllowGet);
                }
            }
            if (flag) // ürün sepette yok yeni ürün eklenmeli
            {
                User user = Session["user"] as User;
                Order order = new Order()
                {
                    Amount = amountInput ? amount : plusMinus ? 1 : 0,
                    AmountPaid = 0,
                    Description = "",
                    OrderDate = DateTime.Now,
                    ProductID = productID,
                    ReceiptID = receipt.ReceiptID,
                    Status = 0,
                    UserID = user.UserID,
                };
                if (order.Amount > 0)
                {
                    om.AddOrder(order);
                    return Json(new { errMessage = "", confirm = true, newAmount = order.Amount }, JsonRequestBehavior.AllowGet);
                }
                return Json(new { errMessage = "Bir şeyler ters gitti.", confirm = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { errMessage = "Bir şeyler ters gitti.", confirm = false }, JsonRequestBehavior.AllowGet);
        }

        /*public ActionResult OrderInput(int productID, int tableID, int amount)
        {
            Table table = tm.GetByID(tableID);
            Receipt receipt = rm.GetByID((int)table.ReceiptID);
            List<Order> ordersInCart = om.GetInCartByReceiptID(receipt.ReceiptID);
            bool flag = true;
            foreach (var item in ordersInCart)
            {
                if (item.ProductID == productID) // eğer ürün zaten sepette varsa miktarını arttır veya azalt
                {
                    item.Amount = amount;
                    om.Update(item);
                    flag = false; // ürün zaten sepette var yeni ürün eklenmesine gerek yok
                    return Json(new { errMessage = "", confirm = true, destination = "" }, JsonRequestBehavior.AllowGet);
                }
            }
            if (flag) // ürün sepette yok yeni ürün eklenmeli
            {
                User user = Session["user"] as User;
                Order order = new Order()
                {
                    Amount = amount,
                    AmountPaid = 0,
                    Description = "",
                    OrderDate = DateTime.Now,
                    ProductID = productID,
                    ReceiptID = receipt.ReceiptID,
                    Status = 0,
                    UserID = user.UserID,
                };
                om.AddOrder(order);
                return Json(new { errMessage = "", confirm = true, destination = "" }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { errMessage = "Bir şeyler ters gitti.", confirm = false, destination = "" }, JsonRequestBehavior.AllowGet);
        }*/
        #endregion
    }
}