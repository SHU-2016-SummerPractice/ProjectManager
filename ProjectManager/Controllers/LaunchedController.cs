using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManager.Controllers
{
    public class LaunchedController : Controller
    {
        // GET: Launched
        public ActionResult Index()
        {
            ModelDbContext mdb = new ModelDbContext();
            IQueryable<ProjectInfo>  launcheds = mdb.ProjectInfoes;
            IEnumerable<ProjectInfo> launchedsQuery =from projectInfo in launcheds
                                                     where projectInfo.IsLanuched=="Y" select projectInfo;
            ViewBag.launcheds = launchedsQuery;
            return View();
        }
    }
}