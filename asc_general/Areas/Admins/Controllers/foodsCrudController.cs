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
    public class foodsCrudController : Controller
    {
        private DbAscEntities db = new DbAscEntities();

        // GET: foodsCrud
        public ActionResult Index()
        {
            var foods = db.foods.Include(f => f.food_categories);
            return View(foods.ToList());
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,name,category_id,text,photo")] food food, HttpPostedFileBase photo)
        {   

            if (ModelState.IsValid)
            {
                if (photo.ContentType == "image/jpeg" || photo.ContentType == "image/png" || photo.ContentType == "image/gif")
                {
                    WebImage img = new WebImage(photo.InputStream);
              
                    DateTime now = DateTime.Now;
                    string fileName = now.ToString("yyyyMdHms") + Path.GetFileName(photo.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    photo.SaveAs(path);
                    if (img.Width > 1000)
                        img.Resize(500, 500);
                    img.Save(path);
                    food.photo = fileName;
                    db.foods.Add(food);
                    db.SaveChanges();
                } else
                {
                    ViewBag.category_id = new SelectList(db.food_categories, "id", "category_name");
                    ViewBag.Message = "You can only jpg,png or gif file upload";
                    return View();
                }
   

                }
            else
            {
                ViewBag.category_id = new SelectList(db.food_categories, "id", "category_name");
                ViewBag.Message = "Errorrr";
                return View();
            }
            return RedirectToAction("Index");
        }
      
           

        
        // GET: foodsCrud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            food food = db.foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // GET: foodsCrud/Create
        public ActionResult Create()
        {
            ViewBag.category_id = new SelectList(db.food_categories, "id", "category_name");
            return View();
        }

        // POST: foodsCrud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2 ([Bind(Include = "id,name,category_id,text,photo")] food food)
        {
            if (ModelState.IsValid)
            {  

                db.foods.Add(food);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.category_id = new SelectList(db.food_categories, "id", "category_name", food.category_id);
            return View(food);
        }

        // GET: foodsCrud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            food food = db.foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            ViewBag.category_id = new SelectList(db.food_categories, "id", "category_name", food.category_id);
            return View(food);
        }

        // POST: foodsCrud/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit2 ([Bind(Include = "id,name,category_id,text,photo")] food food)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.category_id = new SelectList(db.food_categories, "id", "category_name", food.category_id);
            return View(food);
        }


        [HttpPost]
        public ActionResult Edit(int id, [Bind(Include = "id,name,category_id,text,photo")] food food, string oldfile)
        {

            var gelensekil = HttpContext.Request.Files["photo"];
            if (gelensekil.FileName.Length > 0)
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
                    food.photo = fileName;

                }
                else
                {
                    ViewBag.category_id = new SelectList(db.food_categories, "id", "category_name");
                    ViewBag.Message = "You can only jpg,png or gif file upload";
                    return View();
                }
            }
            else
            {
                food.photo = oldfile;
            }
            if (ModelState.IsValid)
            {
                db.Entry(food).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(food);
        }

        // GET: foodsCrud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            food food = db.foods.Find(id);
            if (food == null)
            {
                return HttpNotFound();
            }
            return View(food);
        }

        // POST: foodsCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            food food = db.foods.Find(id);
            string fullPath = Request.MapPath("~/Uploads/" + food.photo);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);

            }
            db.foods.Remove(food);
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
