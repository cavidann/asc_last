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
    public class blog_categoryCrudController : Controller
    {
        private DbAscEntities db = new DbAscEntities();

        // GET: blog_categoryCrud
        public ActionResult Index()
        {
            return View(db.blog_category.ToList());
        }

        // GET: blog_categoryCrud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            blog_category blog_category = db.blog_category.Find(id);
            if (blog_category == null)
            {
                return HttpNotFound();
            }
            return View(blog_category);
        }

        // GET: blog_categoryCrud/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: blog_categoryCrud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] blog_category blog_category)
        {
            if (ModelState.IsValid)
            {
                db.blog_category.Add(blog_category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(blog_category);
        }

        // GET: blog_categoryCrud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            blog_category blog_category = db.blog_category.Find(id);
            if (blog_category == null)
            {
                return HttpNotFound();
            }
            return View(blog_category);
        }

        // POST: blog_categoryCrud/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] blog_category blog_category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(blog_category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(blog_category);
        }

        // GET: blog_categoryCrud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            blog_category blog_category = db.blog_category.Find(id);
            if (blog_category == null)
            {
                return HttpNotFound();
            }
            return View(blog_category);
        }

        // POST: blog_categoryCrud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            blog_category blog_category = db.blog_category.Find(id);
            db.blog_category.Remove(blog_category);
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
