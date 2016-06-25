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
			int num = staffs.Count();
			ViewBag.StaffCount = num;

            return View();
		}
        public ActionResult ViviaTest()
        {
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