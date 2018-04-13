using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Dynamic;
using asc_general.Models;
using System.Net;

namespace asc_general.Controllers
{   
    
    public   class FoodController : Controller
    {
       private DbAscEntities db = new DbAscEntities();
        // GET: Food
        public ActionResult Index()
        {
            dynamic model = new ExpandoObject();
            model.food_cat = db.food_categories.ToList();
            model.foods = db.foods.ToList();
            model.food_blog = db.blogs.Where(b => b.blog_category.name.Equals("qida")).OrderByDescending(b=> b.date).Take(3).ToList();
            return View(model);
        }

    }
}