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
    public class health_staffController : Controller
    {
       private DbAscEntities db = new DbAscEntities();

        // GET: health_staff
        public ActionResult Index()
        {
            return View(db.health_staff.ToList());
        }

        // GET: health_staff/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            health_staff health_staff = db.health_staff.Find(id);
            if (health_staff == null)
            {
                return HttpNotFound();
            }
            return View(health_staff);
        }

        // GET: health_staff/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: health_staff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,profession,photo,text_about,instagram,facebook")] health_staff health_staff)
        {
            if (ModelState.IsValid)
            {
                db.health_staff.Add(health_staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(health_staff);
        }

        // GET: health_staff/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            health_staff health_staff = db.health_staff.Find(id);
            if (health_staff == null)
            {
                return HttpNotFound();
            }
            return View(health_staff);
        }

        // POST: health_staff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,profession,photo,text_about,instagram,facebook")] health_staff health_staff)
        {
            if (ModelState.IsValid)
            {
                db.Entry(health_staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(health_staff);
        }

        // GET: health_staff/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            health_staff health_staff = db.health_staff.Find(id);
            if (health_staff == null)
            {
                return HttpNotFound();
            }
            return View(health_staff);
        }

        // POST: health_staff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            health_staff health_staff = db.health_staff.Find(id);
            db.health_staff.Remove(health_staff);
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
