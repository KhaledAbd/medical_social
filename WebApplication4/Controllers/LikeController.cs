using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
namespace WebApplication4.Controllers
{
    public class LikeController : Controller
    {
        private Blog_MedicalEntities2 db = new Blog_MedicalEntities2();

        [HttpGet]
        public ActionResult  Create(int post_id)
        {

            like_post like = new like_post()
            {
                likeOrdislike = true,
                UserInfo = db.AspNetUsers.Single(u => u.UserName == User.Identity.Name).UserInfo
                    };
                    db.POSTs.Single(n => n.post_id == post_id).like_post.Add(like);
                    db.SaveChanges();
            return RedirectToAction("index", "Posts");
        }
        
    }
}