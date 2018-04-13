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
    public class cartoon_blogController : Controller
    {
        private DbAscEntities db = new DbAscEntities();
        // GET: cartoon_blog
        public ActionResult Index(int? id)
        {  
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dynamic mymodel = new ExpandoObject();
            cartoon cartoon_id = db.cartoons.Find(id);
            mymodel.cartoon_blog = cartoon_id;
            mymodel.othercartoons = db.cartoons.Where(o => o.id != cartoon_id.id).ToList();
            mymodel.next = db.blogs.FirstOrDefault(n => n.id > cartoon_id.id);
            mymodel.prev = db.blogs.OrderByDescending(x => x.id).FirstOrDefault(p => p.id < cartoon_id.id);
            return View(mymodel);
        }

 
    }
}