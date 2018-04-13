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
    public class CrudNamesController : Controller
    {
       private DbAscEntities db = new DbAscEntities();

        // GET: CrudNames
        public ActionResult Index()
        {
            var names = db.names.Include(n => n.lettr);
            return View(names.ToList());
        }

        // GET: CrudNames/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            name name = db.names.Find(id);
            if (name == null)
            {
                return HttpNotFound();
            }
            return View(name);
        }

        // GET: CrudNames/Create
        public ActionResult Create()
        {
            ViewBag.lettrs_id = new SelectList(db.lettrs, "id", "name");
            return View();
        }

        // POST: CrudNames/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name1,lettrs_id,decription,type")] name name)
        {
            if (ModelState.IsValid)
            {
                db.names.Add(name);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.lettrs_id = new SelectList(db.lettrs, "id", "name", name.lettrs_id);
            return View(name);
        }

        // GET: CrudNames/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            name name = db.names.Find(id);
            if (name == null)
            {
                return HttpNotFound();
            }
            ViewBag.lettrs_id = new SelectList(db.lettrs, "id", "name", name.lettrs_id);
            return View(name);
        }

        // POST: CrudNames/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name1,lettrs_id,decription,type")] name name)
        {
            if (ModelState.IsValid)
            {
                db.Entry(name).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.lettrs_id = new SelectList(db.lettrs, "id", "name", name.lettrs_id);
            return View(name);
        }

        // GET: CrudNames/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            name name = db.names.Find(id);
            if (name == null)
            {
                return HttpNotFound();
            }
            return View(name);
        }

        // POST: CrudNames/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            name name = db.names.Find(id);
            db.names.Remove(name);
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
