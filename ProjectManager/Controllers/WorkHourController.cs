using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class WorkHourController : Controller
    {
        // GET: WorkHour
        public ActionResult Index()
        {
            ModelDbContext mdb = new ModelDbContext();
            IQueryable<WorkHour> workHours = mdb.WorkHours;
            ViewBag.workHours = workHours;
            return View();
        }
    }
}