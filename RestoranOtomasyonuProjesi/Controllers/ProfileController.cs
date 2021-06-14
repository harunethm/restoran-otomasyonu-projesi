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
    public class ProfileController : Controller
    {
        UserManager um = new UserManager(new DalEfUser());

        public ActionResult Settings()
        {
            User user = Session["userID"] as User;
            int id = user.UserID;
            ViewBag.user = um.GetByID(id);
            return View();
        }

        public ActionResult ApplyChanges(int id, string name, string phone, string password)
        {
            User user = um.GetByID(id);
            if(user != null && !string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(phone) && !string.IsNullOrEmpty(password) && phone.Length == 10)
            {
                if(um.GetByPhoneNumber(phone) == null || um.GetByPhoneNumber(phone).UserID == id)
                {
                    user.Name = name;
                    user.PhoneNumber = phone;
                    user.Password = password;
                    um.UpdateUser(user);
                    return Json(new { errMessage = "", confirm = true}, JsonRequestBehavior.AllowGet);
                }
                else
                    return Json(new { errMessage = "Telefon numarası başka bir kullanıcıya ait.", confirm = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { errMessage = "Lütfen bilgileri doğru ve eksiksiz doldurun.", confirm = false}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult IsUserAdmin()
        {
            User user = Session["userID"] as User;
            if (um.GetByID(user.UserID) != null)
                return um.GetByID(user.UserID).Role == "admin" ? Json(new { confirm = true }, JsonRequestBehavior.AllowGet) : Json(new { confirm = false }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { confirm = false }, JsonRequestBehavior.AllowGet);
        }

    }
}