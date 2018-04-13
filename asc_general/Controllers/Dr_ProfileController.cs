using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asc_general.Models;
using System.Dynamic;
using System.Net;

namespace asc_general.Controllers
{

    public class Dr_ProfileController : Controller
    {
        private DbAscEntities db = new DbAscEntities();
        // GET: Dr_Profile
        public ActionResult Index(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dynamic mymodel = new ExpandoObject();
            health_staff staff_id = db.health_staff.Find(id);
            mymodel.staffId = staff_id;
            mymodel.other_doctors = db.health_staff.Where(h => h.id != id).Take(4).ToList();
            return View(mymodel);
        }
    }
}