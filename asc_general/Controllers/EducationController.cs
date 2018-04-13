using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asc_general.Models;
using System.Dynamic;

namespace asc_general.Controllers
{
   public class EducationController : Controller
    {
        // GET: Education
         private DbAscEntities db = new DbAscEntities();
        public ActionResult Index()
        {
            dynamic datalar = new ExpandoObject();
            datalar.regions = db.regions.ToList();
            datalar.complex = db.our_complex.Where(b => b.edu_or_gym == true).Take(8).ToList();
            datalar.edu_blog = db.blogs.Where(e => e.blog_category.name.Equals("təhsil")).OrderByDescending(e => e.date).Take(3).ToList();
            return View(datalar);
        }
    }
}