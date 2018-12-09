using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DostDestek.Repository;
using DostDestek.Entity;
using DostDestekSample.Areas.Admin.Models.ResultModel;

namespace DostDestekSample.Areas.Admin.Controllers
{
    public class VeterinerController : Controller
    {
        VetRepository vr = new VetRepository();
        InstanceResult<Veteriner> result = new InstanceResult<Veteriner>();
        // GET: Admin/Veteriner
        public ActionResult List(string m, int? id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("AdminLogin", "AdminLogin");
            }
            else
            {
                List<Veteriner> animal = vr.List().ProcessResult;
                result.resultList = vr.List();
                if (m != null)
                    ViewBag.Mesaj = string.Format("{0} nolu kaydin silme islemi {1}", id, m);
                else
                    ViewBag.Mesaj = "";
                return View(animal);

            }
        }
        public ActionResult AddVet()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("AdminLogin", "AdminLogin");
            }
            else
            {
                return View();

            }
        }
        [HttpPost]
        public ActionResult AddVet(Veteriner model)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("AdminLogin", "AdminLogin");
            }
            else
            {
                result.resultint = vr.Insert(model);
                if (result.resultint.IsSuccessed)
                    return RedirectToAction("List");
                else
                    return View(model);

            }
        }
        public ActionResult Edit(int id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("AdminLogin", "AdminLogin");
            }
            else
            {
                return View(vr.GetObjById(id).ProcessResult);

            }
        }
        [HttpPost]
        public ActionResult Edit(Veteriner model)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("AdminLogin", "AdminLogin");
            }
            else
            {
                result.resultint = vr.Update(model);
                if (result.resultint.IsSuccessed)
                    return RedirectToAction("List");
                else
                    return View(model);

            }
        }
        public ActionResult Delete(int id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("AdminLogin", "AdminLogin");

            }
            else
            {
                result.resultint = vr.Delete(id);
                return RedirectToAction("List", new { @m = result.resultint.UserMessage, @id = id });
            }
        }
    }
}