using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ModelDbContext mdb = new ModelDbContext();
			IQueryable<Staff> staffs = mdb.Staffs;
            IEnumerable<int> staffsCount = from staff in staffs select staffs.Count();
			ViewBag.StaffCount = staffsCount.First();

            IQueryable<ProjectInfo> launcheds = mdb.ProjectInfoes;

            IQueryable<ProjectInfo> launchedsCount = mdb.ProjectInfoes;
            int projectInfoCount = (from projectInfo in launcheds
                                                 where projectInfo.IsLanuched == "Y"
                                                 select projectInfo).Count();
            ViewBag.ProjectInfoCount = projectInfoCount;
            
            return View();
		}
        public ActionResult ViviaTest()
        {
            if(!User.Identity.IsAuthenticated)
            {
                return Redirect("/Account/Login?returnUrl=/Home/ViviaTest");
            }
            ModelDbContext mdb = new ModelDbContext();
            IQueryable<ProjectInfo> projectInfos = mdb.ProjectInfoes;
            ProjectInfo tmp = projectInfos.First();
            ViewBag.pbInfos = tmp.IsLanuched;
            return View();
        }
        public ActionResult About()     //About界面
        {
			return View();
		}

	}
}