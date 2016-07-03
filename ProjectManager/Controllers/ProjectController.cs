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
            ViewBag.lanuchCount = (from projectInfo in mdc.ProjectInfoes
                                   where projectInfo.MISStatus.Equals("Lanuch")
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

            List<PBInfo> pbInfo = mdc.PbInfoes.Where(u => u.ShowPB == 0).ToList();

            return View(pbInfo);
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

        public string SaveProjectMessage(string ProjectID, string optionSelected, string lanuchDate, string LO, string CNPMId, string status, string IsLanuched, string startDate, string releaseDate, string DelayLanuchDate, string DelayReleaseDate)
        {
            string result="更新成功！";
            int id = Convert.ToInt32(ProjectID);
            int lo=Convert.ToInt32(LO);
            var newProject = mdc.ProjectInfoes.Where(u => u.ProjectID == id ).FirstOrDefault();
            newProject.MISStatus = optionSelected;
            DateTime datetime=DateTime.ParseExact(lanuchDate, "yyyy/M/d H:mm:ss", null);
            DateTime startDateTime = DateTime.ParseExact(startDate, "yyyy/M/d H:mm:ss", null);
            DateTime releaseDateTime = DateTime.ParseExact(releaseDate, "yyyy/M/d H:mm:ss", null);
            DateTime delayLanuchDateTime = DateTime.ParseExact(DelayLanuchDate, "yyyy/M/d H:mm:ss", null);
            DateTime delayReleaseDateTime = DateTime.ParseExact(DelayReleaseDate, "yyyy/M/d H:mm:ss", null);
            if(CNPMId.Length != 0)
            {
                int cnpId=Convert.ToInt32(CNPMId);
                newProject.LanuchDate = datetime;
                newProject.LO = lo;
                newProject.CNPMId = cnpId;
                newProject.StartDate = startDateTime;
                newProject.ReleaseDate = releaseDateTime;
                newProject.DelayLanuchDate = delayLanuchDateTime;
                newProject.DelayReleaseDate = delayReleaseDateTime;
                newProject.IsLanuched = IsLanuched;
                mdc.SaveChanges();
            }
            else
            {
                newProject.LanuchDate = datetime;
                newProject.LO = lo;
                newProject.StartDate = startDateTime;
                newProject.ReleaseDate = releaseDateTime;
                newProject.DelayLanuchDate = delayLanuchDateTime;
                newProject.DelayReleaseDate = delayReleaseDateTime;
                newProject.IsLanuched = IsLanuched;
                mdc.SaveChanges();
            }
            return result;
        }

        public string AddProject(string ProjectID,string LO,string CNPMId,string status,string IsLanuched, string startDate, string releaseDate, string lanuchDate, string DelayLanuchDate,string DelayReleaseDate,string[] pbNO)
        {
            string result="添加成功！";
            int projectID=Convert.ToInt32(ProjectID.Trim());
            int lo=Convert.ToInt32(LO.Trim());
            int cnpId=Convert.ToInt32(CNPMId.Trim());
            DateTime startDateTime = DateTime.ParseExact(startDate, "yyyy/M/d H:mm:ss", null);
            DateTime releaseDateTime = DateTime.ParseExact(releaseDate, "yyyy/M/d H:mm:ss", null);
            DateTime lanuchDateTime = DateTime.ParseExact(lanuchDate, "yyyy/M/d H:mm:ss", null);
            DateTime delayLanuchDateTime = DateTime.ParseExact(DelayLanuchDate, "yyyy/M/d H:mm:ss", null);
            DateTime delayReleaseDateTime = DateTime.ParseExact(DelayReleaseDate, "yyyy/M/d H:mm:ss", null);
            //向PB里面插入数据
            foreach(string pb in pbNO)
            {
                var project = mdc.PbInfoes.Where(u => u.PBNo == pb
                    ).FirstOrDefault();
                project.ProjectID = projectID;
                project.ShowPB = 1;
                mdc.SaveChanges();
            }
            //向project里面插入数据
            ProjectInfo projectInfo = new ProjectInfo() { ProjectID = projectID, LO = lo, MISStatus = status, IsLanuched = IsLanuched, CNPMId = cnpId, StartDate = startDateTime, ReleaseDate = releaseDateTime, DelayReleaseDate = delayReleaseDateTime, LanuchDate = lanuchDateTime, DelayLanuchDate = delayLanuchDateTime, EndDate = null };
            mdc.ProjectInfoes.Add(projectInfo);
            mdc.SaveChanges();
            return result;
        }
    }
}