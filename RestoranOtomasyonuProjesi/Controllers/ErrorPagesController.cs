using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RestoranOtomasyonuProjesi.Controllers
{
    public class ErrorPagesController : Controller
    {
        public ActionResult Error401()
        {
            Response.StatusCode = 401;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
        public ActionResult Error403()
        {
            Response.StatusCode = 403;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
        public ActionResult Error404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
        public ActionResult Error408()
        {
            Response.StatusCode = 408;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
        public ActionResult Error500()
        {
            Response.StatusCode = 500;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}