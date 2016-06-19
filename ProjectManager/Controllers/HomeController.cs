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

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

	}
}