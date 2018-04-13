using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using asc_general.Models;
using System.Web.Helpers;
using System.IO;

namespace asc_general.Areas.Admins.Controllers
{
    public class blogsCrudController : Controller
    {
        private DbAscEntities db = new DbAscEntities();

        // GET: blogsCrud
        public ActionResult Index()
        {
            var blogs = db.blogs.Include(b => b.blog_category);
            return View(blogs.ToList());
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,title,photo,date,category_id,dec,text")] blog blog, HttpPostedFileBase photo)
        {

            if (ModelState.IsValid)
            {
                if (photo.ContentType == "image/jpeg" || photo.ContentType == "image/png" || photo.ContentType == "image/gif")
                {
                    //WebImage img = new WebImage(photo.InputStream);

                    DateTime now = DateTime.Now;
                    string fileName = now.ToString("yyyyMdHms") + Path.GetFileName(photo.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    photo.SaveAs(path);
                    //if (img.Width > 1000)
                    //    img.Resize(500, 500);
                    //img.Save(path);
                    blog.photo = fileName;
                    blog.date = now;
                    db.blogs.Add(blog);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.category_id = new SelectList(db.blog_category, "id", "name");
                    ViewBag.Message = "You can only jpg,png or gif file upload";
                    return View();
                }


            }
            else
            {
                ViewBag.category_id = new SelectList(db.blog_category, "id", "name");
                ViewBag.Message = "Errorrr";
                return View();
            }
            return RedirectToAction("Index");
        }

        // GET: blogsCrud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            blog blog = db.blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // GET: blogsCrud/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.blog_category, "id", "name");
            return View();
        }

        // POST: blogsCrud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2([Bind(Include = "id,title,photo,date,category_id,dec,text")] blog blog)
        {
            if (ModelState.IsValid)
            {
                db.blogs.Add(blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.blog_category, "id", "name", blog.category_id);
            return View(blog);
        }



        // GET: blogsCrud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            blog blog = db.blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.blog_category, "id", "name", blog.category_id);
            return View(blog);
        }


        // POST: blogsCrud/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.


        [HttpPost]
        public ActionResult Edit(int id, [Bind(Include = "id,title,photo,date,category_id,dec,text")] blog blog, string oldfile)
        {

            var gelensekil = HttpContext.Request.Files["photo"];
            DateTime now = DateTime.Now;
            if (gelensekil.FileName.Length > 0)
            {

                if (gelensekil.ContentType == "image/jpeg" || gelensekil.ContentType == "image/png" || gelensekil.ContentType == "image/gif")
                {

                    string fullPath = Request.MapPath("~/Uploads/" + oldfile);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);

                    }
                    
                    string fileName = now.ToString("yyyyMdHms") + Path.GetFileName(gelensekil.FileName);
                    string newfile = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    gelensekil.SaveAs(newfile);
                    blog.photo = fileName;
                    blog.date = now;

                }
                else
                {
                    ViewBag.category_id = new SelectList(db.blog_category, "id", "name");
                    ViewBag.Message = "You can only jpg,png or gif file upload";
                    return View();
                }
            }
            else
            {
                blog.photo = oldfile;
            }
            blog.date = now;
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2([Bind(Include = "id,title,photo,date,category_id,dec,text")] blog blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.blog_category, "id", "name", blog.category_id);
            return View(blog);
        }

        // GET: blogsCrud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            blog blog = db.blogs.Find(id);
            if (blog == null)
            {
                return HttpNotFound();
            }
            return View(blog);
        }

        // POST: blogsCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            blog blog = db.blogs.Find(id);
            string fullPath = Request.MapPath("~/Uploads/" + blog.photo);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);

            }
            db.blogs.Remove(blog);
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
