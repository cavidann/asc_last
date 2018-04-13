using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using asc_general.Models;
using System.Data.Entity;
using System.Dynamic;

namespace asc_general.Controllers.Controllers
{
    public class LikedController : Controller
    {
        // GET: Liked
        private DbAscEntities db = new DbAscEntities();
        public ActionResult Index()
        {
            if (Session["user"] != null)
            {
                user thisUser = (user)(Session["user"]);
                dynamic mymodel = new ExpandoObject();
                mymodel.mylikes = db.likes.Where(x => x.user_id == thisUser.id);
               return View(mymodel);

            }
            else
            {
                return RedirectToAction("login","user");
            }
        }
    }
}