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

        public ActionResult _showPB()
        {
            return View();
        }
        public ActionResult _DetailProject()
        {
            List<DetailProject> detailProject = null;
            List<ProjectInfo> projectInfo = mdc.ProjectInfoes.ToList();
            List<PBInfo> pbInfo = mdc.PbInfoes.ToList();
            var detailList= from m in projectInfo join n in pbInfo on m.ProjectID equals n.ProjectID select new DetailProject { ProjectID = m.ProjectID, LO = m.LO, IsLanuched = m.IsLanuched, CNPMId = m.CNPMId, EndDate = m.EndDate, DelayReleaseDate = m.DelayReleaseDate, DelayLanuchDate = m.DelayLanuchDate, PBName = n.PBName };
            detailProject =  detailList.ToList<DetailProject>();

            return View(detailProject);
        }

        public string SaveProjectMessage(string ProjectID, string optionSelected, string lanuchDate)
        {
            string result="success";
            int id = Convert.ToInt32(ProjectID);
            var newProject = mdc.ProjectInfoes.Where(u => u.ProjectID == id ).FirstOrDefault();
            newProject.MISStatus = optionSelected;
            DateTime datetime=DateTime.ParseExact(lanuchDate, "yyyy/MM/dd HH:mm:ss", null);
            newProject.LanuchDate = datetime;
            mdc.SaveChanges();
            return result;
        }
    }
}