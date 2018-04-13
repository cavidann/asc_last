using asc_general.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace asc_general.Controllers
{
    public class gymController : Controller
    {
        private DbAscEntities db = new DbAscEntities();
        // GET: gym
        public ActionResult Index()
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.gym = db.gym_blog.Take(6).ToList();
            mymodel.regions = db.regions.Take(4).ToList();
            mymodel.gymComplex = db.our_complex.Where(c => c.edu_or_gym == false).Take(8).ToList();
            return View(mymodel);

        }
    }
}