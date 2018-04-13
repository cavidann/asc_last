using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asc_general.Models;
using System.Dynamic;
using System.Net;
using PagedList;

namespace asc_general.Controllers
{
    public class ReceptsController : Controller
    {

        private DbAscEntities db = new DbAscEntities();
        // GET: Recepts
        public ActionResult Index(int? id,int? categoryID)
        {
            if (id == null || categoryID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dynamic mymodel = new ExpandoObject();
            food food_id = db.foods.Find(id);
            mymodel.foodId = food_id;
            mymodel.recepts = db.foods.ToList();
            mymodel.allrecepts = db.foods.Where(f => f.category_id == food_id.category_id).ToList();
            mymodel.prev = db.foods.OrderByDescending(x=> x.id).FirstOrDefault(p=> p.id > id && p.category_id == food_id.category_id);
            mymodel.next = db.foods.FirstOrDefault(n => n.id < id && n.category_id == food_id.category_id);
            mymodel.categories = db.food_categories.ToList();
            return View(mymodel);
        }

        public ActionResult Allrecepts(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            ViewBag.food_cat_id = db.food_categories.Find(id);
            ViewBag.othercategories = db.food_categories.Where(f => f.id != id).ToList();
            ViewBag.receptbycategories = db.foods.Where(f => f.category_id == id).ToList();

            int pageNumber = (page ?? 1);
            ViewBag.receptbycategories = db.foods.OrderBy(o => o.id).Where(c => c.category_id == id).ToPagedList(pageNumber, 5);
            return View();
        }
    }
}