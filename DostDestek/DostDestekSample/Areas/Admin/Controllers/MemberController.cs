using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DostDestek.Entity;
using DostDestek.Repository;
using DostDestekSample.Areas.Admin.Models.ResultModel;

namespace DostDestekSample.Areas.Admin.Controllers
{
    public class MemberController : Controller
    {
        // GET: Admin/Member
        MemberRepository mr = new MemberRepository();
        InstanceResult<Member> result = new InstanceResult<Member>();
        public ActionResult List(string m, int? id)
        {
            if (Session["User"] == null)
            {
               return RedirectToAction("AdminLogin", "AdminLogin");
            }
            else
            { List<Member> mm = mr.List().ProcessResult.Where(t => t.RoleId == 1).ToList();
                result.resultList = mr.List();
                if (m != null)
                    ViewBag.Mesaj = string.Format("{0} nolu kaydin silme islemi {1}", id, m);
                else
                    ViewBag.Mesaj = "";
                return View(mm);
                
            }
        }
        public ActionResult AddAdmin()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("AdminLogin", "AdminLogin");
            }
            else
            {return View();
                
            }
        }
        [HttpPost]
        public ActionResult AddAdmin(Member model)
        {
            if (Session["User"] == null)
            {
               return RedirectToAction("AdminLogin", "AdminLogin"); 
            }
            else
            {model.RoleId = 1;
                result.resultint = mr.Insert(model);
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
            {return View(mr.GetObjById(id).ProcessResult);
                
            }
        }
        [HttpPost]
        public ActionResult Edit(Member model)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("AdminLogin", "AdminLogin");
            }
            else
            {result.resultint = mr.Update(model);
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
            {result.resultint = mr.Delete(id);
                return RedirectToAction("List", new { @m = result.resultint.UserMessage, @id = id });
               
            }
        }
    }
}