using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;

namespace WebApplication4.Controllers
{
    [Authorize]
    public class POSTsController : Controller
    {
        private Blog_MedicalEntities2 db = new Blog_MedicalEntities2();
        
        // GET: POSTs
        public ActionResult Index()
        {
            return View(db.POSTs.ToList());
        }

        // GET: POSTs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POST pOST = db.POSTs.Find(id);
            if (pOST == null)
            {
                return HttpNotFound();
            }
            return View(pOST);
        }

        // GET: POSTs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: POSTs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "text")] POST pOST, HttpPostedFileBase file)
        {
            if(file != null)
            {
                string pic = System.IO.Path.GetFileName(file.FileName);
                string name = pic.Substring(0, pic.IndexOf('.'));
                string ext = pic.Substring(pic.IndexOf('.'));
                int result = (int)DateTime.Now.Subtract(DateTime.MinValue).TotalSeconds;
                pic = name + result.ToString() + ext; 
                
                string path = System.IO.Path.Combine(Server.MapPath("~/App_Data/image/posts"), pic);

                file.SaveAs(path);
                pOST.picture = pic;
                using(MemoryStream ms = new MemoryStream())
                {
                    file.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();
                }
                
            }
            if (ModelState.IsValid && pOST != null)
            {
                pOST.UserInfo = db.AspNetUsers.First(u => u.UserName == User.Identity.Name).UserInfo;
                pOST.created_at = DateTime.Now;
                db.POSTs.Add(pOST);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pOST);
        }

        // GET: POSTs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POST pOST = db.POSTs.Find(id);
            if (pOST == null)
            {
                return HttpNotFound();
            }
            return View(pOST);
        }

        // POST: POSTs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "text,picture")] POST pOST)
        {
            if (ModelState.IsValid)
            {
                pOST.created_at = DateTime.Now;
                db.Entry(pOST).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pOST);
        }

        // GET: POSTs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POST pOST = db.POSTs.Find(id);
            if (pOST == null)
            {
                return HttpNotFound();
            }
            return View(pOST);
        }

        // POST: POSTs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            POST pOST = db.POSTs.Find(id);
            db.POSTs.Remove(pOST);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
