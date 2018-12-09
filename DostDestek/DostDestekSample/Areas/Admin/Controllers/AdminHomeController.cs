using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DostDestekSample.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: Admin/AdminHome
        public ActionResult AdminHome()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("AdminLogin", "AdminLogin");
            }
            return View("AdminHome");
        }
    }
}