using System;
using System.Collections.Generic;
using System.IO;
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
        public ActionResult Create(UserInfo userinfo, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string name = pic.Substring(0, pic.IndexOf('.'));
                    string ext = pic.Substring(pic.IndexOf('.'));
                    int result = (int)DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
                    pic = name + result.ToString() + ext;

                    string path = System.IO.Path.Combine(Server.MapPath("~/image/profile/"), pic);

                    file.SaveAs(path);
                    userinfo.photo = pic;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }

                }
                else
                {
                    userinfo.photo = "profile.jpg";
                }
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
        public ActionResult Edit(UserInfo usernew, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                UserInfo userold = db.UserInfoes.Single(m => m.ID == usernew.ID);

                if (file != null)
                {
                    string pic = System.IO.Path.GetFileName(file.FileName);
                    string name = pic.Substring(0, pic.IndexOf('.'));
                    string ext = pic.Substring(pic.IndexOf('.'));
                    int result = (int)DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
                    pic = name + result.ToString() + ext;

                    string path = System.IO.Path.Combine(Server.MapPath("~/image/profile/"), pic);

                    file.SaveAs(path);
                    userold.photo = pic;
                    using (MemoryStream ms = new MemoryStream())
                    {
                        file.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }

                }
                else
                {
                    userold.photo = "profile.jpg";
                }
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
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Profile(string email)
        {
            if(email != null)
            {
                return View(db.AspNetUsers.SingleOrDefault(m => m.UserName == email));
            }
            return View(db.AspNetUsers.SingleOrDefault(m => m.UserName == User.Identity.Name));
        }        
        public ActionResult Doctors()
        {
            return View("Doctors",db.UserInfoes.Where(n => n.isDoctor == true).ToList());
        }
    }
}