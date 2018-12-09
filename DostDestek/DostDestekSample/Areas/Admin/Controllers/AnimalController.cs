using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DostDestek.Entity;
using DostDestek.Repository;
using DostDestekSample.Areas.Admin.Models.ResultModel;

namespace DostDestekSample.Areas.Admin.Controllers
{
    public class AnimalController : Controller
    {
        AnimalRepository ar = new AnimalRepository();
        InstanceResult<Animal> result = new InstanceResult<Animal>();
        // GET: Admin/Animal
        public ActionResult List(string m, int? id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("AdminLogin", "AdminLogin");
            }
            else
            {
                List<Animal> animal = ar.List().ProcessResult;
                result.resultList = ar.List();
                if (m != null)
                    ViewBag.Mesaj = string.Format("{0} nolu kaydin silme islemi {1}", id, m);
                else
                    ViewBag.Mesaj = "";
                return View(animal);

            }

        }
        public ActionResult AddAnimal()
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
        public ActionResult AddAnimal(Animal model)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("AdminLogin", "AdminLogin");
            }
            else
            {
                result.resultint = ar.Insert(model);
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
                return View(ar.GetObjById(id).ProcessResult);

            }

        }
        [HttpPost]
        public ActionResult Edit(Animal model)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("AdminLogin", "AdminLogin");
            }
            else
            {
                result.resultint = ar.Update(model);
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
                result.resultint = ar.Delete(id);
                return RedirectToAction("List", new { @m = result.resultint.UserMessage, @id = id });

            }
        }
    }
}