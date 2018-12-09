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
    public class TopicController : Controller
    {
        TopicRepository tr = new TopicRepository();
        InstanceResult<Topic> result = new InstanceResult<Topic>();
        // GET: Admin/Topic
        public ActionResult List(string m, int? id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("AdminLogin", "AdminLogin");
            }
            else
            {
                List<Topic> topic = tr.List().ProcessResult;
                result.resultList = tr.List();
                if (m != null)
                    ViewBag.Mesaj = string.Format("{0} nolu kaydin silme islemi {1}", id, m);
                else
                    ViewBag.Mesaj = "";
                return View(topic);

            }
        }
        public ActionResult AddTopic()
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
        public ActionResult AddTopic(Topic model)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("AdminLogin", "AdminLogin");
            }
            else
            {
                result.resultint = tr.Insert(model);
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
                return View(tr.GetObjById(id).ProcessResult);

            }
        }
        [HttpPost]
        public ActionResult Edit(Topic model)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("AdminLogin", "AdminLogin");
            }
            else
            {
                result.resultint = tr.Update(model);
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
                result.resultint = tr.Delete(id);
                return RedirectToAction("List", new { @m = result.resultint.UserMessage, @id = id });

            }
        }
    }
}