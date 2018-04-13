using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using asc_general.Models;

namespace asc_general.Areas.Admins.Controllers
{
    public class food_categoriesCrudController : Controller
    {
        private DbAscEntities db = new DbAscEntities();

        // GET: food_categoriesCrud
        public ActionResult Index()
        {
            return View(db.food_categories.ToList());
        }

        // GET: food_categoriesCrud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            food_categories food_categories = db.food_categories.Find(id);
            if (food_categories == null)
            {
                return HttpNotFound();
            }
            return View(food_categories);
        }

        // GET: food_categoriesCrud/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: food_categoriesCrud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,sira,category_name")] food_categories food_categories)
        {
            if (ModelState.IsValid)
            {
                db.food_categories.Add(food_categories);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(food_categories);
        }

        // GET: food_categoriesCrud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            food_categories food_categories = db.food_categories.Find(id);
            if (food_categories == null)
            {
                return HttpNotFound();
            }
            return View(food_categories);
        }

        // POST: food_categoriesCrud/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,sira,category_name")] food_categories food_categories)
        {
            if (ModelState.IsValid)
            {
                db.Entry(food_categories).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(food_categories);
        }

        // GET: food_categoriesCrud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            food_categories food_categories = db.food_categories.Find(id);
            if (food_categories == null)
            {
                return HttpNotFound();
            }
            return View(food_categories);
        }

        // POST: food_categoriesCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            food_categories food_categories = db.food_categories.Find(id);
            db.food_categories.Remove(food_categories);
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
