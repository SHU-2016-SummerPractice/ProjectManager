using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class WorkPercetageController : Controller
    {
        // GET: WorkPercetage
        public ActionResult Index()
        {
			if (!User.Identity.IsAuthenticated)
				return Redirect("/Account/Login?returnUrl=/WorkPercetage/Index");
			ModelDbContext mdb = new ModelDbContext();
            IQueryable<Domain> workPercetages = mdb.Domains;
            ViewBag.workPercetages = workPercetages;
            return View();
        }

	    public ActionResult Update(string staffid, string percentage, string project, string memo)
	    {
		    if (!User.Identity.IsAuthenticated)
			    return null;
			if (staffid == null || staffid.IsEmpty()
				|| percentage == null || percentage.IsEmpty()
				|| project == null || project.IsEmpty()
				|| memo == null || memo.IsEmpty()
				)
				return null;
			ModelDbContext mdb = new ModelDbContext();
			IQueryable<Domain> workPercetages = mdb.Domains;
		    int staff = int.Parse(staffid);
			Domain aim = (from workPercetage in workPercetages
			    where workPercetage.StaffId == staff
						  select workPercetage).First();
		    aim.Percentage = 100 > int.Parse(percentage) ? int.Parse(percentage) : 100;
			aim.Project = project;
			aim.Memo = memo;
		    mdb.SaveChanges();
			return null;
		}

	}
}