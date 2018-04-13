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
    public class gymblogController : Controller
    {
        private DbAscEntities db = new DbAscEntities();
        // GET: gym_blog
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dynamic mymodel = new ExpandoObject();
            gym_blog gym_id = db.gym_blog.Find(id);
            mymodel.gym_blog = gym_id;
            mymodel.other_sports = db.gym_blog.Where(s => s.id != gym_id.id).ToList();
            mymodel.next = db.blogs.FirstOrDefault(n => n.id > gym_id.id);
            mymodel.prev = db.blogs.OrderByDescending(x => x.id).FirstOrDefault(p => p.id < gym_id.id);
            return View(mymodel);
        }
    }
}