using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using asc_general.Models;
using System.IO;
using System.Web.Helpers;

namespace asc_general.Areas.Admins.Controllers
{
    public class gym_blogController : Controller
    {
       private DbAscEntities db = new DbAscEntities();

        // GET: gym_blog
        public ActionResult Index()
        {
            return View(db.gym_blog.ToList());
        }

        // GET: gym_blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gym_blog gym_blog = db.gym_blog.Find(id);
            if (gym_blog == null)
            {
                return HttpNotFound();
            }
            return View(gym_blog);
        }

        // GET: gym_blog/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,title,description,text,video_url")] gym_blog gym_blog, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid) {
            if (photo.ContentType == "image/jpeg" || photo.ContentType == "image/png" || photo.ContentType == "image/gif")
            {
                 //   WebImage img = new WebImage(photo.InputStream);
                    DateTime now = DateTime.Now;

                    string fileName = now.ToString("yyyyMdHms") + Path.GetFileName(photo.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    photo.SaveAs(path);
                    //if (img.Width > 1000)
                    //img.Resize(500, 500);
                    //img.Save(path);
                    gym_blog.photo = fileName;
                    db.gym_blog.Add(gym_blog);
                    db.SaveChanges();
            }
            else
            {
                ViewBag.Message = "You can only jpg,png or gif file upload";
                return View();
            }
            } else
            {
                ViewBag.Message = "Error";
                return View();
            }
            return RedirectToAction("Index");

        }


        // POST: gym_blog/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2 ([Bind(Include = "id,title,photo,description,text,video_url")] gym_blog gym_blog)
        {

            if (ModelState.IsValid)
            {
                db.gym_blog.Add(gym_blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(gym_blog);
        }

        



        // GET: gym_blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gym_blog gym_blog = db.gym_blog.Find(id);
            if (gym_blog == null)
            {
                return HttpNotFound();
            }
            return View(gym_blog);
        }

        // POST: gym_blog/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,title,photo,description,text,video_url")] gym_blog gym_blog, string oldfile)
        {

            var gelensekil = HttpContext.Request.Files["photo"];
            if (gelensekil.FileName.Length> 0)
            {


                if (gelensekil.ContentType == "image/jpeg" || gelensekil.ContentType == "image/png" || gelensekil.ContentType == "image/gif")
                {

                    DateTime now = DateTime.Now;

                    string fileName = now.ToString("yyyyMdHms") + Path.GetFileName(gelensekil.FileName);
                    string newfile = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    gelensekil.SaveAs(newfile);
                    gym_blog.photo = fileName;

                    string fullPath = Request.MapPath("~/Uploads/" + oldfile);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }

                }
                else
                {
                    ViewBag.Message = "You can only jpg,png or gif file upload";
                    return View();
                }
            }
            else
            {
                gym_blog.photo = oldfile;
            }
            if (ModelState.IsValid)
            {
                db.Entry(gym_blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gym_blog);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            gym_blog gym_blog = db.gym_blog.Find(id);
            if (gym_blog == null)
            {
                return HttpNotFound();
            }
            return View(gym_blog);
        }

        // POST: gym_blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            gym_blog gym_blog = db.gym_blog.Find(id);
            string fullPath = Request.MapPath("~/Uploads/" + gym_blog.photo);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
            db.gym_blog.Remove(gym_blog);
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

