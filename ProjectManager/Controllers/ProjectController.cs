using ProjectManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManager.Controllers
{
    public class ProjectController : Controller
    {
        ModelDbContext mdc = new ModelDbContext();

        // GET: Project
        public ActionResult Index()
        {
            ViewBag.releaseCount = (from projectInfo in mdc.ProjectInfoes
                                    where projectInfo.MISStatus.Equals("Release")
                                    select projectInfo.MISStatus).Count();
            ViewBag.loadCount = (from projectInfo in mdc.ProjectInfoes
                                 where projectInfo.MISStatus.Equals("Load")
                                 select projectInfo.MISStatus).Count();
            ViewBag.codingCount = (from projectInfo in mdc.ProjectInfoes
                                   where projectInfo.MISStatus.Equals("Coding")
                                   select projectInfo.MISStatus).Count();

            return View();
        }

        public ActionResult _showProject()
        {
            List<ProjectInfo> projectInfo = mdc.ProjectInfoes.ToList();

            return View(projectInfo);
        }
    }
}