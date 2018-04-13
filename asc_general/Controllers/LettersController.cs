using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Dynamic;
using asc_general.Models;

namespace asc_general.Controllers
{
    public class LettersController : Controller
    {
        private DbAscEntities db = new DbAscEntities();
        // GET: Namen=>
        public ActionResult Index()
        {
            dynamic datalar = new ExpandoObject();
            datalar.herfler = db.lettrs.ToList();
            return View(datalar);
        }
    }
}