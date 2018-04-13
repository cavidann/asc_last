using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using asc_general.Models;

namespace asc_general.Controllers
{
    public class HomeController : Controller
    {
       private DbAscEntities db = new DbAscEntities();

        public ActionResult Index()
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.names = db.names.Take(4).ToList();
            mymodel.cartoon = db.cartoons.Take(5).ToList();
            mymodel.blogs = db.blogs.Take(2).OrderByDescending(d=> d.date).Take(2).ToList();
            return View(mymodel);
        }

    }
}