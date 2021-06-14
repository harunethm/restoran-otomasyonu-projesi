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
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        #region manager sınıfları
        UserManager um = new UserManager(new DalEfUser());
        CategoryManager cm = new CategoryManager(new DalEfCategory());
        ProductManager pm = new ProductManager(new DalEfProduct());
        CashRegisterManager crm = new CashRegisterManager(new DalEfCashRegister());
        TableManager tm = new TableManager(new DalEfTable());
        OrderManager om = new OrderManager(new DalEfOrder());
        #endregion

        #region kullanıcı işlemleri
        [HttpGet]
        public ActionResult Users()
        {
            ViewBag.users = um.ListAll();
            return View();
        }

        public ActionResult AddUser(string name, string phone, string password)
        {
            if (name != "" && phone != "" && password != "")
            {
                if (um.GetByPhoneNumber(phone) == null) // kullanıcı yoktur kayıt yapılabilir
                {
                    User user = new User()
                    {
                        Name = name,
                        PhoneNumber = phone,
                        Password = password,
                        RegisterDate = DateTime.Now,
                        LastLogin = DateTime.Now,
                        LastPasswordChange = DateTime.Now,
                        Status = true,
                        Role = "staff",
                    };
                    um.AddUser(user);
                    return Json(new { errMessage = "", confirm = true }, JsonRequestBehavior.AllowGet);
                }
                else // kullanıcı zaten var
                {
                    return Json(new { errMessage = "Kullanıcı zaten var.", confirm = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { errMessage = "Alanlar boş bırakılmamalıdır.", confirm = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditUser(int id, string name, string phone, string password)
        {
            User user = um.GetByID(id); // orjinal kullanıcı
            User _ = um.GetByPhoneNumber(phone); // telefon numarasına kayıtlı kullanıcı
            if (user != null && (_ == null || _.UserID == user.UserID)) // kullanıcı vardır ve daha önce kayıtlı değildir
            {
                user.Name = name;
                user.PhoneNumber = phone;
                if (user.Password != password)
                {
                    user.Password = password;
                    user.LastPasswordChange = DateTime.Now;
                }
                um.UpdateUser(user);
                return Json(new { errMessage = "", confirm = true }, JsonRequestBehavior.AllowGet);
            }
            else // kullanıcı yok veya daha önce kayıtlı
            {
                return Json(new { errMessage = "Kullanıcı zaten var.", confirm = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult UserStatusChange(int id, bool status)
        {
            User user = um.GetByID(id);
            if (user != null)
            {
                user.Status = status;
                um.UpdateUser(user);
                return Json(new { errMessage = "", confirm = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { errMessage = "Kullanıcı bulunamadı.", confirm = false }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region kategori işlemleri
        [HttpGet]
        public ActionResult Categories()
        {
            ViewBag.categories = cm.ListAll();
            return View();
        }

        public ActionResult AddCategory(string categoryName)
        {
            if (cm.IsCategoryExist(categoryName) == 0 && categoryName != "")
            {
                cm.AddCategory(new Category()
                {
                    CategoryName = categoryName,
                    Status = true,
                });
                return Json(new { errMessage = "", confirm = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { errMessage = "Kategori zaten var.", confirm = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditCategory(int id, string categoryName)
        {
            Category category = cm.GetByID(id);
            if ((cm.IsCategoryExist(categoryName) == id || cm.IsCategoryExist(categoryName) == 0) && categoryName != "")
            {
                category.CategoryName = categoryName;
                cm.UpdateCategory(category);
                return Json(new { errMessage = "", confirm = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { errMessage = "Kategori zaten var.", confirm = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CategoryStatusChange(int id, bool status)
        {
            Category category = cm.GetByID(id);
            if (category != null)
            {
                category.Status = status;
                cm.UpdateCategory(category);
                return Json(new { errMessage = "", confirm = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { errMessage = "Kategori bulunamadı.", confirm = false }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ürün işlemleri
        [HttpGet]
        public ActionResult Products()
        {
            ViewBag.products = pm.ListAll();
            ViewBag.categories = cm.ListAll();
            return View();
        }

        public ActionResult AddProduct(string productName, string productPrice, string productDescription, int categoryID)
        {
            productPrice = productPrice.Contains('.') ? productPrice.Replace('.', ',') : productPrice;
            if (pm.IsProductExist(productName) == 0 && productName != "" && productDescription != "" && categoryID != 0)
            {
                pm.AddProduct(new Product()
                {
                    Name = productName,
                    Price = Convert.ToDouble(productPrice),
                    Description = productDescription,
                    CategoryID = categoryID,
                    Status = true,
                });
                return Json(new { errMessage = "", confirm = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { errMessage = "Ürün zaten var veya boş kutular var.", confirm = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditProduct(int id, string productName, string productPrice, string productDescription, int categoryID)
        {
            productPrice = productPrice.Contains('.') ? productPrice.Replace('.', ',') : productPrice;
            Product product = pm.GetByID(id);
            if ((pm.IsProductExist(productName) == id || pm.IsProductExist(productName) == 0) && productName != "")
            {
                product.Name = productName;
                product.Price = Convert.ToDouble(productPrice);
                product.Description = productDescription == null ? "" : productDescription;
                product.CategoryID = categoryID;
                pm.UpdateProduct(product);
                return Json(new { errMessage = "", confirm = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { errMessage = "Ürün adı zaten kayıtlı.", confirm = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductStatusChange(int id, bool status)
        {
            Product product = pm.GetByID(id);
            if (product != null)
            {
                product.Status = status;
                pm.UpdateProduct(product);
                return Json(new { errMessage = "", confirm = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { errMessage = "Ürün bulunamadı.", confirm = false }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region istatistik işlemleri
        public ActionResult Statistics()
        {
            List<Statistic> statistics = new List<Statistic>() {
                new Statistic() { StatisticHeader="En Çok Satan Ürün", StatisticValue= "Beyti"},
                new Statistic() { StatisticHeader="En Çok Satan Kategori", StatisticValue= "Kırmızı Et"},
                new Statistic() { StatisticHeader="Kâr", StatisticValue= "10.000 TL"},
                new Statistic() { StatisticHeader="Zarar", StatisticValue= "10 TL"},
                new Statistic() { StatisticHeader="asd", StatisticValue= "asdasdasd"},
                new Statistic() { StatisticHeader="dsa", StatisticValue= "adsa"},
                new Statistic() { StatisticHeader="asd", StatisticValue= "asdasdasdasdasdasdasdasdasdasdasdsdasdas"},
            };
            ViewBag.statistics = statistics;
            return View();
        }
        #endregion

        #region kasa işlemleri
        [HttpGet]
        public ActionResult CashRegister()
        {
            ViewBag.day = crm.IsDayStarted() != null ? true : false;
            ViewBag.cashRegisters = crm.ListAll();
            return View();
        }

        public ActionResult DayStatusChange()
        {
            // günü aç veya kapat
            //gün başlamış ise
            var day = crm.IsDayStarted();
            if (day != null)
            {
                if (tm.IsAllEmpty())
                {
                    day.DayEnd = DateTime.Now;
                    day.Status = false;
                    crm.Update(day);
                }
                else
                {
                    return Json(new { errMessage = "Lütfen tüm masaların boş olduğundan emin olun.", confirm = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else // gün başlamamış ise
            {
                CashRegister cashRegister = new CashRegister()
                {
                    AmountDiscount = 0,
                    AmountEarned = 0,
                    AmountServed = 0,
                    DayStart = DateTime.Now,
                    DayEnd = null,
                    Status = true,
                };
                crm.AddCashRegister(cashRegister);
            }
            ViewBag.day = day != null ? true : false;
            return Json(new { errMessage = "", confirm = true }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region masa işlemleri
        [HttpGet]
        public ActionResult Tables()
        {
            ViewBag.tables = tm.ListAll().OrderBy(x => x.Name).ToList();
            ViewBag.orders = om.ListAll();
            return View();
        }
        public ActionResult AddTable(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                tm.AddTable(new Table()
                {
                    Availability = 0,
                    OpeningDate = null,
                    ReceiptID = null,
                    Name = name,
                    Status = true,
                });
                return Json(new { errMessage = "", confirm = true }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { errMessage = "Lütfen bilgileri eksiksiz giriniz.", confirm = false }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditTable(int id, string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                Table table = tm.GetByID(id);
                table.Name = name;
                tm.Update(table);
                return Json(new { errMessage = "", confirm = true }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { errMessage = "Lütfen bilgileri eksiksiz giriniz.", confirm = false }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteTable(int id)
        {
            Table table = tm.GetByID(id);
            if (table != null)
            {
                table.Status = !table.Status;
                tm.Update(table);
                return Json(new { errMessage = "", confirm = true }, JsonRequestBehavior.AllowGet);
            }
            else
                return Json(new { errMessage = "Masa Bulunamadı.", confirm = false }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}