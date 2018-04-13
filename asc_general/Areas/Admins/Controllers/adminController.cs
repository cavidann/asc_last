using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asc_general.Models;
using System.Dynamic;
using System.Net;

namespace asc_general.Areas.Admins.Controllers
{
    public class adminController : Controller
    {
        private DbAscEntities db = new DbAscEntities();
        // GET: admin
        public ActionResult Index(int? id)
        {

            if (Session["admin"] != null)
            {
                //if (id == null)
                //{
                //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                //}
                dynamic mymodel = new ExpandoObject();
                admin admin_id = db.admins.Find(id);
                mymodel.adminID = admin_id;
                mymodel.admin = db.admins.ToList();
                return View(mymodel);

            }
            else
            {
                return RedirectToAction("login");
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(admin adm)
        {
            if (adm.adminname != null && adm.password != null)
            {
                admin admin = db.admins.FirstOrDefault(a => a.adminname == adm.adminname && a.password == adm.password);
                if (admin != null)
                {
                    Session["admin"] = true;
                    Session["admin"] = new admin() { adminname = adm.adminname, image = admin.image };
                    return RedirectToAction("index", "admin");
                }
                else
                {
                    Session["error_input"] = "Username ve ya password yalnisdir";
                    return RedirectToAction("login");
                }
            }
            else
            {
                Session["error_message"] = "Bosluq buraxma";
                return RedirectToAction("login");
            }

        }
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("index", "admin");
        }

    }
}