using asc_general.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace asc_general.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        private DbAscEntities db = new DbAscEntities();
        [HttpGet]
        public ActionResult Index(int? id, string searchString)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            int pageNumber = id ?? 1;
            ViewBag.searchStr = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                int count = db.blogs.Where(b => b.title.Contains(searchString)).Count();
                ViewBag.ResultCount = count;
                if (count != 0)
                {
                    ViewBag.Searching = db.blogs.Where(b => b.title.Contains(searchString)).OrderBy(o => o.id).ToPagedList(pageNumber, 12);

                }
                else
                {
                    ViewBag.NoResult = "Belə bir nəticə tapılmadı";
                    id = 1;
                }

            }
            else
            {
                ViewBag.EmptyString = "Axtarış daxil edilmədi";
                id = 1;
            }
            return View();
        }

        [HttpPost]
        public JsonResult LiveSearch(string searchString)
        {
            var blogTitles = db.blogs.Where(b => b.title.Contains(searchString)).Select(b => new { blogId = b.id, blogTitle = b.title.Substring(0, 20) }).ToList();
            return Json(blogTitles, JsonRequestBehavior.AllowGet);
        }
    }
}