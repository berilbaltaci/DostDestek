using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DostDestekSample.Areas.Admin.Models.ResultModel;
using DostDestek.Entity;
using DostDestek.Repository;
using System.IO;
using DostDestek.Common;
using PagedList;
using PagedList.Mvc;

namespace DostDestekSample.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        PostRepository pr = new PostRepository();
        HayvanDestekEntities db = Tools.GetConnection();
        CommentRepository cr = new CommentRepository();
        public ActionResult Index(int page = 1)
        {
            var post = db.Post.OrderByDescending(p => p.PostId).ToPagedList(page, 4);
            return View(post);
        }

        InstanceResult<Post> resultPost = new InstanceResult<Post>();
        public ActionResult Detail(int id)
        {
            Post pdet = pr.GetObjById(id).ProcessResult;
            Session["Post"] = pdet;
            ((Post)Session["Post"]).View += 1;
            db.SaveChanges();
            return View(pdet);
        }
        [HttpPost]
        public ActionResult Detail(string comment)
        {
            if (Session["User"] != null)
            {
                Comment newComment = new Comment();
                newComment.Comment1 = comment;
                newComment.MemberId = ((Member)Session["User"]).UserId;
                newComment.PostId = ((Post)Session["Post"]).PostId;
                newComment.CommentDate = DateTime.Now;
                newComment.Dislike = 0;
                newComment.Like = 0;
                ((Post)Session["Post"]).CommentNum += 1;
                ((Post)Session["Post"]).View -= 1;
                result.resultint = cr.Insert(newComment);
                return RedirectToAction("Detail", "Home");
            }
            else
            {
                return View("SignIn");
            }

        }
        public ActionResult DeletePost(int id)
        {
            var post = db.Post.Where(m => m.PostId == id).SingleOrDefault();
            db.Post.Remove(post);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteComment(int id)
        {
            var mem = ((Member)Session["User"]);
            var comment = db.Comment.Where(c => c.CommentId == id).SingleOrDefault();
            var post = db.Post.Where(p => p.PostId == comment.PostId).SingleOrDefault();
            if (comment.MemberId == mem.UserId)
            {
                ((Post)Session["Post"]).View -= 1;
                ((Post)Session["Post"]).CommentNum -= 1;
                db.Comment.Remove(comment);
                db.SaveChanges();
                return RedirectToAction("Detail", "Home", new { id = post.PostId });
            }
            else
            {
                return HttpNotFound();
            }
        }

        public ActionResult LikeValue(int id)
        {
            if (Session["User"] != null)
            {
                var post = db.Post.Where(p => p.PostId == id).SingleOrDefault();
                post.Like += 1;
                db.SaveChanges();
                return RedirectToAction("Index", "Home", new { id = post.PostId });
            }
            else
            {
                return View("SignIn");
            }

        }

        public ActionResult DislikeValue(int id)
        {
            if (Session["User"] != null)
            {
                var post = db.Post.Where(p => p.PostId == id).SingleOrDefault();
                post.Dislike += 1;
                db.SaveChanges();
                return RedirectToAction("Index", "Home", new { id = post.PostId });
            }
            else
            {
                return View("SignIn");
            }

        }

        public ActionResult ViewValue(int id)
        {
            if (Session["User"] != null)
            {
                var post = db.Post.Where(p => p.PostId == id).SingleOrDefault();
                post.View += 1;
                db.SaveChanges();
                return View();
            }
            else
            {
                return View("SignIn");
            }

        }
        public ActionResult NewPost()
        {
            if (Session["User"] != null)
            {
                return View();
            }
            else
            {
                return View("SignIn");
            }
        }
        [HttpPost]
        public ActionResult NewPost(Post model)
        {
            if (Session["User"] != null)
            {
                model.Like = 0;
                model.Dislike = 0;
                model.View = 0;
                model.CommentNum = 0;
                model.MemberId = ((Member)Session["User"]).UserId;
                model.PostDate = DateTime.Now;
                resultPost.resultint = pr.Insert(model);
                if (resultPost.resultint.IsSuccessed)
                {
                    return RedirectToAction("Index", "Home");
                }
                return View(model);
            }
            else
            {
                return View("SignIn");
            }
        }
        public ActionResult List(int? id)
        {
            List<Post> pList = pr.List().ProcessResult.Where(t => t.TopicId == id).ToList();
            return View(pList);
        }
        public ActionResult ListAllPost()
        {
            return View(pr.List().ProcessResult);
        }
        MemberRepository mr = new MemberRepository();
        InstanceResult<Member> result = new InstanceResult<Member>();
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Member member)
        {
            member.RoleId = 2;
            result.resultint = mr.Insert(member);
            if (result.resultint.IsSuccessed)
            {
                Session["User"] = member;
                return RedirectToAction("Index", "Home");
            }
            else
                return View(member);
        }
        public ActionResult SignIn()
        {
            if (Session["User"] != null)
            {
                return RedirectToAction("Index", "Admin/Admin");
            }
            return View();
        }
        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult SignIn(Member member)
        {
            using (HayvanDestekEntities db = new HayvanDestekEntities())
            {
                var userDetails = db.Member.FirstOrDefault(t => t.UserName == member.UserName && t.Password == member.Password);
                if (userDetails == null)
                {
                    return View("SignIn", member);
                }
                else
                {
                    Session["User"] = userDetails;
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public ActionResult LogOut()
        {
            Session["User"] = null;
            return RedirectToAction("SignIn", "Home");
        }
    }
}