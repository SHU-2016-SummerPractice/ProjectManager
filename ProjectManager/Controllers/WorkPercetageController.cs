using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class WorkPercetageController : Controller
    {
        // GET: WorkPercetage
        public ActionResult Index()
        {
            ModelDbContext mdb = new ModelDbContext();
            IQueryable<Domain> workPercetages = mdb.Domains;
            ViewBag.workPercetages = workPercetages;
            return View();
        }
    }
}