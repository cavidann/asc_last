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
    public class CrudLettrsController : Controller
    {
       private DbAscEntities db = new DbAscEntities();

        // GET: CrudLettrs
        public ActionResult Index()
        {
            return View(db.lettrs.ToList());
        }

        // GET: CrudLettrs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lettr lettr = db.lettrs.Find(id);
            if (lettr == null)
            {
                return HttpNotFound();
            }
            return View(lettr);
        }

        // GET: CrudLettrs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CrudLettrs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name")] lettr lettr)
        {
            if (ModelState.IsValid)
            {
                db.lettrs.Add(lettr);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(lettr);
        }

        // GET: CrudLettrs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lettr lettr = db.lettrs.Find(id);
            if (lettr == null)
            {
                return HttpNotFound();
            }
            return View(lettr);
        }

        // POST: CrudLettrs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name")] lettr lettr)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lettr).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lettr);
        }

        // GET: CrudLettrs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            lettr lettr = db.lettrs.Find(id);
            if (lettr == null)
            {
                return HttpNotFound();
            }
            return View(lettr);
        }

        // POST: CrudLettrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            lettr lettr = db.lettrs.Find(id);
            db.lettrs.Remove(lettr);
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
