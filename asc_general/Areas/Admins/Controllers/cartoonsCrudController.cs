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
    public class cartoonsCrudController : Controller
    {
        private DbAscEntities db = new DbAscEntities();

        // GET: cartoonsCrud
        public ActionResult Index()
        {
            return View(db.cartoons.ToList());
        }

        [HttpPost]
        public ActionResult Create ([Bind(Include = "id,title,text,video_url")] cartoon cartoon,HttpPostedFileBase photo)
        {
            if (photo.ContentLength >0)
            {
                if (photo.ContentType == "image/jpeg" || photo.ContentType == "image/png" || photo.ContentType == "image/gif")
                {
          
                    DateTime now = DateTime.Now;
                    string fileName = now.ToString("yyyyMdHms") + Path.GetFileName(photo.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    photo.SaveAs(path);
                    cartoon.photo = fileName;
                    db.cartoons.Add(cartoon);
                    db.SaveChanges();


                }
                else
                {  

                    ViewBag.Message = "You can only jpg,png or gif file upload";
                    return View();

                }

            }
            else
            {
                ViewBag.Message = "Errorrr";
                return View();
            }
        
      
            return RedirectToAction("Index");

        }
        // GET: cartoonsCrud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cartoon cartoon = db.cartoons.Find(id);
            if (cartoon == null)
            {
                return HttpNotFound();
            }
            return View(cartoon);
        }

        // GET: cartoonsCrud/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: cartoonsCrud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2([Bind(Include = "id,title,photo,text,video_url")] cartoon cartoon)
        {
            if (ModelState.IsValid)
            {
                db.cartoons.Add(cartoon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cartoon);
        }

        // GET: cartoonsCrud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cartoon cartoon = db.cartoons.Find(id);
            if (cartoon == null)
            {
                return HttpNotFound();
            }
            return View(cartoon);
        }

        // POST: cartoonsCrud/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2([Bind(Include = "id,title,photo,text,video_url")] cartoon cartoon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cartoon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cartoon);
        }


        [HttpPost]
        public ActionResult Edit(int id,[Bind(Include = "id,title,photo,text,video_url")] cartoon cartoon, string oldfile)
        {
           
            var gelensekil = HttpContext.Request.Files["photo"];
            if (gelensekil.FileName.Length> 0)
            {

                if (gelensekil.ContentType == "image/jpeg" || gelensekil.ContentType == "image/png" || gelensekil.ContentType == "image/gif")
                {
                    string fullPath = Request.MapPath("~/Uploads/" + oldfile);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);

                    }
                    DateTime now = DateTime.Now;
                    string fileName = now.ToString("yyyyMdHms") + Path.GetFileName(gelensekil.FileName);
                    string newfile = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    gelensekil.SaveAs(newfile);
                    cartoon.photo = fileName;

                } else
                {
                    ViewBag.Message = "You can only jpg,png or gif file upload";
                    return View();
                }
            }
            else
            {
                cartoon.photo = oldfile;
            }
            if (ModelState.IsValid)
            {
                db.Entry(cartoon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cartoon);
        }

        // GET: cartoonsCrud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            cartoon cartoon = db.cartoons.Find(id);
            if (cartoon == null)
            {
                return HttpNotFound();
            }
            return View(cartoon);
        }

        // POST: cartoonsCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            cartoon cartoon = db.cartoons.Find(id);
            string fullPath = Request.MapPath("~/Uploads/"+ cartoon.photo);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);

            }
            
            db.cartoons.Remove(cartoon);
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
