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
    public class our_complex_CrudController : Controller
    {
        private DbAscEntities db = new DbAscEntities();

        // GET: Admins/our_complex_Crud
        public ActionResult Index()
        {
            var our_complex = db.our_complex.Include(o => o.region);
            return View(our_complex.ToList());
        }

        // GET: Admins/our_complex_Crud/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            our_complex our_complex = db.our_complex.Find(id);
            if (our_complex == null)
            {
                return HttpNotFound();
            }
            return View(our_complex);
        }

        // GET: Admins/our_complex_Crud/Create
        public ActionResult Create()
        {
            ViewBag.region_id = new SelectList(db.regions, "id", "region1");
            return View();
        }

        // POST: Admins/our_complex_Crud/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "id,region_id,complex_name,address,phone,facebook,instagram,edu_or_gym,photo,text,map_url")] our_complex our_complex,HttpPostedFileBase photo)
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
                    our_complex.photo = fileName;
                    db.our_complex.Add(our_complex);
                    db.SaveChanges();
                }
                else
                {
                    ViewBag.region_id = new SelectList(db.regions, "id", "region1", our_complex.region_id);
                    ViewBag.Message = "You can only jpg,png or gif file upload";
                    return View();
                }


            }
            else
            {
                ViewBag.region_id = new SelectList(db.regions, "id", "region1", our_complex.region_id);
                ViewBag.Message = "Errorrr";
                return View();
            }
            return RedirectToAction("Index");
        }


        // GET: Admins/our_complex_Crud/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            our_complex our_complex = db.our_complex.Find(id);
            if (our_complex == null)
            {
                return HttpNotFound();
            }
            ViewBag.region_id = new SelectList(db.regions, "id", "region1", our_complex.region_id);
            return View(our_complex);
        }

        // POST: Admins/our_complex_Crud/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    
        [HttpPost]
        public ActionResult Edit(int id, [Bind(Include = "id,region_id,complex_name,address,phone,facebook,instagram,edu_or_gym,photo,text,map_url")] our_complex our_complex, string oldfile)
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
                    our_complex.photo = fileName;

                }
                else
                {
                    ViewBag.region_id = new SelectList(db.regions, "id", "region1", our_complex.region_id);
                    ViewBag.Message = "You can only jpg,png or gif file upload";
                    return View();
                }
            }
            else
            {
                our_complex.photo = oldfile;
            }
            if (ModelState.IsValid)
            {
                db.Entry(our_complex).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(our_complex);
        }


        // GET: Admins/our_complex_Crud/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            our_complex our_complex = db.our_complex.Find(id);
            if (our_complex == null)
            {
                return HttpNotFound();
            }
            return View(our_complex);
        }

        // POST: Admins/our_complex_Crud/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            our_complex our_complex = db.our_complex.Find(id);
            string fullPath = Request.MapPath("~/Uploads/" + our_complex.photo);

            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);

            }
            db.our_complex.Remove(our_complex);
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
