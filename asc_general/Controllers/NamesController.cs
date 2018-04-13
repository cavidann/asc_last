using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asc_general.Models;
using System.Net;
using System.Dynamic;

namespace asc_general.Controllers
{
    public class NamesController : Controller
    {
        // GET: Names
        private DbAscEntities db = new DbAscEntities();
        public ActionResult Name(int? id, bool? gender)
        {
            if (id == null || gender == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            dynamic data = new ExpandoObject();
            data.Adlar = db.names.Where(name => name.lettrs_id == id && name.type == gender).ToList();
            return View(data);
        }
    }
}