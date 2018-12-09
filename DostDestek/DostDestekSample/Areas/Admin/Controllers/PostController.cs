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
    public class PostController : Controller
    {
        // GET: Admin/Post
        PostRepository pr = new PostRepository();
        InstanceResult<Post> result = new InstanceResult<Post>();

        public ActionResult List(string m, int? id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("AdminLogin", "AdminLogin");
            }
            else
            {
                List<Post> post = pr.List().ProcessResult;
                result.resultList = pr.List();
                if (m != null)
                    ViewBag.Mesaj = string.Format("{0} nolu kaydin silme islemi {1}", id, m);
                else
                    ViewBag.Mesaj = "";
                return View(post);

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
                result.resultint = pr.Delete(id);
                return RedirectToAction("List", new { @m = result.resultint.UserMessage, @id = id });

            }
        }
    }
}