using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asc_general.Models;
using System.Dynamic;
using System.Net;
using PagedList;

namespace asc_general.Controllers.Controllers
{
    public class ComplexController : Controller
    {
        private DbAscEntities db = new DbAscEntities();

        // GET: Complex
        public ActionResult Index(int? id, int? regionID)
        {
            if (id == null || regionID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dynamic mymodel = new ExpandoObject();
            our_complex complex_id = db.our_complex.Find(id);
            mymodel.complexId = complex_id;
            mymodel.regions = db.regions.ToList();
            mymodel.allKindergarten = db.our_complex.Where(c => c.region_id == complex_id.region_id && c.edu_or_gym == complex_id.edu_or_gym).ToList();
          

            return View(mymodel);
        }

        public ActionResult allComplex(int? id, bool? complexType, int? page)
        {
            if (id == null || complexType == null || page == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.Region = db.regions.Find(id);
            ViewBag.otherRegions = db.regions.Where(r => r.id != id).ToList();
            ViewBag.kindergardenType = db.our_complex.Where(c => c.region_id == id && c.edu_or_gym == complexType).FirstOrDefault();

            int pageNumber = (page ?? 1);
            ViewBag.kindergardenByRegion = db.our_complex.OrderBy(o => o.id).Where(c => c.region_id == id && c.edu_or_gym == complexType).ToPagedList(pageNumber, 5);

            return View();
        }

    }
}