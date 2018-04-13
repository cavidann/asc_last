using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Dynamic;
using asc_general.Models;

namespace asc_general.Controllers
{
    public class SIngleBlogController : Controller
    {
        private DbAscEntities db = new DbAscEntities();
        // GET: SIngleBlog
        public ActionResult Index(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            dynamic mymodel = new ExpandoObject();
            blog blg = db.blogs.Find(id);
            mymodel.myblog = db.blogs.ToList();
            mymodel.likes = db.likes.ToList();
            mymodel.Blog = blg;
            mymodel.otherblogs = db.blogs.Where( o=> o.id != id && o.category_id == blg.category_id ).ToList();
            mymodel.next = db.blogs.FirstOrDefault(n => n.id > blg.id && n.category_id == blg.category_id);
            mymodel.prev = db.blogs.OrderByDescending(x => x.id).FirstOrDefault(p => p.id < blg.id && p.category_id == blg.category_id);
            return View(mymodel);
        }

        public string LikeThis(int id)
        {
            dynamic mymodel = new ExpandoObject();
            blog blg = db.blogs.FirstOrDefault(x => x.id == id);
            if (User.Identity.IsAuthenticated || Session["user"] != null)
            {
                user thisUser = (user)(Session["user"]);
                user e = db.users.FirstOrDefault(x => x.email == thisUser.email);
                blg.mylikes++;
                like like = new like();
                like.blog_id = id;
                like.user_id = e.id;
                like.liked = true;
                db.likes.Add(like);
                db.SaveChanges();

            }

            return blg.mylikes.ToString();

        }
        public string UnlikeThis(int id)
        {
            blog blg = db.blogs.FirstOrDefault(x => x.id == id);
            if (User.Identity.IsAuthenticated || Session["user"] != null)
            {
                user thisUser = (user)(Session["user"]);
                user d = db.users.FirstOrDefault(x => x.email == thisUser.email);
                like l = db.likes.FirstOrDefault(x => x.blog_id == id && x.user_id == d.id);
          
                blg.mylikes--;
                db.likes.Remove(l);
                db.SaveChanges();

            }
            return blg.mylikes.ToString();
        }
    }
}