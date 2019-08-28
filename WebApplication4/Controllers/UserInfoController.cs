using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Authorize]
    public class UserInfoController : Controller
    {
        Blog_MedicalEntities2 db = new Blog_MedicalEntities2();
        // GET: UserInfo
        [HttpGet]
        public ActionResult Index() =>View(db.AspNetUsers.Single(a => a.UserName == User.Identity.Name));


        [HttpGet]
        [AllowAnonymous]
        public ActionResult Create() {

            if (db.AspNetUsers.Single(a => a.UserName == User.Identity.Name).UserInfo == null)
                return View();
            return RedirectToAction("index", "home");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserInfo userinfo)
        {
            if (ModelState.IsValid)
            {
                userinfo.ID = db.AspNetUsers.Single(a => a.UserName == User.Identity.Name).Id;
                db.UserInfoes.Add(userinfo);
                db.SaveChanges();
                return RedirectToAction("Index", "Home", null);

            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(String id)
        {
            UserInfo userInfo = db.UserInfoes.First(m => m.ID == id);

            return View(userInfo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(UserInfo usernew)
        {
            if (ModelState.IsValid)
            {
                UserInfo userold = db.UserInfoes.Single(m => m.ID == usernew.ID);
                userold.Fname = usernew.Fname;
                userold.Lname = usernew.Lname;
                userold.gender = usernew.gender;
                userold.isDoctor = usernew.isDoctor;
                db.SaveChanges();
                return RedirectToAction("index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}