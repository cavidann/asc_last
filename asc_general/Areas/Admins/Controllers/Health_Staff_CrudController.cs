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

namespace asc_general.Areas.Admins.Controllers
{
    public class Health_Staff_CrudController : Controller
    {
        private DbAscEntities db = new DbAscEntities();

        // GET: Health_Staff_Crud
        public ActionResult Index()
        {
            return View(db.health_staff.ToList());
        }

        // GET: Health_Staff_Crud/Details/5
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

        // GET: Health_Staff_Crud/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,name,profession,photo,text_about,facebook,intagram,phone,email")] health_staff health_staff, HttpPostedFileBase photo)
        {
            if(ModelState.IsValid)
            {
                if (photo.ContentType == "image/jpeg" || photo.ContentType == "image/png" || photo.ContentType == "image/gif")
                {

                    DateTime now = DateTime.Now;

                    string fileName = now.ToString("yyyyMdHms") + Path.GetFileName(photo.FileName);
                    string path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    photo.SaveAs(path);


                    health_staff.photo = fileName;
                    db.health_staff.Add(health_staff);
                    db.SaveChanges();


                }
                else
                {
                    ViewBag.Message = "You can only jpg,png or gif file upload";
                    return View();

                }
            }else
            {
                ViewBag.Message = "Error";
                return View();
            }
        
            return RedirectToAction("Index");

        }

        // POST: Health_Staff_Crud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create2([Bind(Include = "id,name,profession,photo,text_about,instagram,facebook,phone,email")] health_staff health_staff)
        {
            if (ModelState.IsValid)
            {
                db.health_staff.Add(health_staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(health_staff);
        }

        // GET: Health_Staff_Crud/Edit/5
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

        // POST: Health_Staff_Crud/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Edit([Bind(Include = "id,name,profession,photo,text_about,instagram,facebook,phone,email")] health_staff health_staff, string oldfile)
        {

            var gelensekil = HttpContext.Request.Files["photo"];
            if (gelensekil.FileName.Length > 0)
            {


                if (gelensekil.ContentType == "image/jpeg" || gelensekil.ContentType == "image/png" || gelensekil.ContentType == "image/gif")
                {

                    DateTime now = DateTime.Now;

                    string fileName = now.ToString("yyyyMdHms") + Path.GetFileName(gelensekil.FileName);
                    string newfile = Path.Combine(Server.MapPath("~/Uploads"), fileName);
                    gelensekil.SaveAs(newfile);
                    health_staff.photo = fileName;

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
                health_staff.photo = oldfile;
            }
            if (ModelState.IsValid)
            {
                db.Entry(health_staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(health_staff);
        }



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

        // POST: Health_Staff_Crud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            health_staff health_staff = db.health_staff.Find(id);
            string fullPath = Request.MapPath("~/Uploads/" + health_staff.photo);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
            }
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
