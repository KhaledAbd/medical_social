using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
namespace WebApplication4.Models
{
    [Authorize]
    public class CommentController : Controller
    {
        private Blog_MedicalEntities2 db = new Blog_MedicalEntities2();
        // GET: Comment
        public ActionResult Index()
        {
            return View(db.comments.ToList());
        }
        /*
        [HttpGet]
        
        public ActionResult Create(int post_id)
        {
            comment cm = new comment();
            cm.post_id = post_id;
            return View(cm);
        }
        [ValidateAntiForgeryToken]
        public ActionResult Create(comment Comment)
        {
            Comment.UserInfo = db.AspNetUsers.Single(a => a.UserName == User.Identity.Name).UserInfo;
            Comment.created_at = DateTime.Now;
            db.comments.Add(Comment);
            db.SaveChanges();
            return RedirectToAction("index");
        }*/
        public ActionResult Show(int post_id)
        {
            return View(db.comments.Where(c => c.post_id == post_id).ToList());
        }
    }
}