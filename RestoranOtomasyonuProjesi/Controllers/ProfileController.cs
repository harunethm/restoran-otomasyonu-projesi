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
    public class ProfileController : Controller
    {
        UserManager um = new UserManager(new DalEfUser());
        public ActionResult Settings(int id)
        {
            User u = um.GetByID(id);
            ViewBag.user = u;
            return View();
        }
    }
}