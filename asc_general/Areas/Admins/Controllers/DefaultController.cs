using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace asc_general.Areas.Admins.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Admins/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}