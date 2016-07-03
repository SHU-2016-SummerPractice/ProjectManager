using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class DownloadController : Controller
    {

        public ActionResult Project()
        {
	        if (!User.Identity.IsAuthenticated)
				return Redirect("/Account/Login?returnUrl=/Download/Project");

			HSSFWorkbook book = new HSSFWorkbook();     //创建Excel文件
			ISheet sheet = book.CreateSheet("Project");     //sheet，Project：名字

            IRow headRow = sheet.CreateRow(0);      //表头，从0开始
	        string[] headValues = { "ProjectID", "LO", "MISStatus", "IsLanuched", "CNPMId", "StartDate", "EndDate", "ReleaseDate", "DelayReleaseDate", "LanuchDate", "DelayLanuchDate"};
	        int[] widthValues = {2500,1500, 2500, 2600, 5000, 5000, 5000, 5000, 5000, 5000, 5000};      //宽度，字/256
			for (int i = 0; i < headValues.Length; ++i)
	        {
		        headRow.CreateCell(i).SetCellValue(headValues[i]);
				sheet.SetColumnWidth(i, widthValues[i]);
			}
			int rowIndex = 0 + 1;
			DbSet<ProjectInfo> projectInfoes = new ModelDbContext().ProjectInfoes;
			foreach (var it in projectInfoes)
	        {
		        IRow newRow = sheet.CreateRow(rowIndex);
				for (int i = 0; i < headValues.Length; ++i)
					newRow.CreateCell(i).SetCellValue((typeof(ProjectInfo).GetProperty(headValues[i]).GetValue(it))?.ToString() ?? "");
		        ++rowIndex;
	        }
            //FileStream:写回磁盘
			MemoryStream ms = new MemoryStream();
			book.Write(ms);
			ms.Position = 0;//如果不设为0，将返回空
			return File(ms, "application/vnd.ms-excel", "Project.xls");
        }
		public ActionResult WorkHour()
		{
            if (!User.Identity.IsAuthenticated)
                return Redirect("/Account/Login?returnUrl=/Download/WorkHour");

            HSSFWorkbook book = new HSSFWorkbook();     //创建Excel文件
            ISheet sheet = book.CreateSheet("WorkHour");     //sheet，Project：名字

            IRow headRow = sheet.CreateRow(0);      //表头，从0开始
            string[] headValues = { "StaffId", "StartDate", "WorkType", "WorkContent"};
            int[] widthValues = { 2500, 5000, 2500, 2700*4};      //宽度，字/256
            for (int i = 0; i < headValues.Length; ++i)
            {
                headRow.CreateCell(i).SetCellValue(headValues[i]);
                sheet.SetColumnWidth(i, widthValues[i]);
            }
            int rowIndex = 0 + 1;
            DbSet<WorkHour> workHours = new ModelDbContext().WorkHours;
            foreach (var it in workHours)
            {
                IRow newRow = sheet.CreateRow(rowIndex);
                for (int i = 0; i < headValues.Length; ++i)
                    newRow.CreateCell(i).SetCellValue((typeof(WorkHour).GetProperty(headValues[i]).GetValue(it))?.ToString() ?? "");
                ++rowIndex;
            }
            //FileStream:写回磁盘
            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            ms.Position = 0;//如果不设为0，将返回空
            return File(ms, "application/vnd.ms-excel", "WorkHour.xls");
		}
        /// <summary>
        /// 下载WorkPercetage表
        /// </summary>
        /// <returns></returns>
		public ActionResult WorkPercetage()
		{
            if (!User.Identity.IsAuthenticated)
                return Redirect("/Account/Login?returnUrl=/Download/Project");

            HSSFWorkbook book = new HSSFWorkbook();     //创建Excel文件
            ISheet sheet = book.CreateSheet("Project");     //sheet，Project：名字

            IRow headRow = sheet.CreateRow(0);      //表头，从0开始

        string[] headValues = { "StaffId", "Percentage", "WorkLoadPercentage", "Project", "Memo"};
            int[] widthValues = { 2500, 1500*2, 2500*2, 4000, 7500 };      //宽度，字/256
            for (int i = 0; i < headValues.Length; ++i)
            {
                headRow.CreateCell(i).SetCellValue(headValues[i]);
                sheet.SetColumnWidth(i, widthValues[i]);
            }
            int rowIndex = 0 + 1;
            DbSet<Domain> domains = new ModelDbContext().Domains;
            foreach (var it in domains)
            {
                IRow newRow = sheet.CreateRow(rowIndex);
                for (int i = 0; i < headValues.Length; ++i)
                    newRow.CreateCell(i).SetCellValue((typeof(Domain).GetProperty(headValues[i]).GetValue(it))?.ToString() ?? "");
                ++rowIndex;
            }
            //FileStream:写回磁盘
            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            ms.Position = 0;//如果不设为0，将返回空
            return File(ms, "application/vnd.ms-excel", "WorkPercetage.xls");
        }
        /// <summary>
        /// 下载Launched表
        /// </summary>
        /// <returns></returns>
		public ActionResult Launched()
		{
            if (!User.Identity.IsAuthenticated)
                return Redirect("/Account/Login?returnUrl=/Download/Launched");

            HSSFWorkbook book = new HSSFWorkbook();     //创建Excel文件
            ISheet sheet = book.CreateSheet("Project");     //sheet，Project：名字

            IRow headRow = sheet.CreateRow(0);      //表头，从0开始
        
        string[] headValues = { "ProjectID", "LO", "MISStatus", "IsLanuched", "CNPMId", "StartDate", "EndDate", "ReleaseDate", "DelayReleaseDate", "LanuchDate", "DelayLanuchDate" };
            int[] widthValues = { 2500, 1500, 2500, 2600, 5000, 5000, 5000, 5000, 5000, 5000, 5000 };      //宽度，字/256
            for (int i = 0; i < headValues.Length; ++i)
            {
                headRow.CreateCell(i).SetCellValue(headValues[i]);
                sheet.SetColumnWidth(i, widthValues[i]);
            }
            int rowIndex = 0 + 1;
          
              DbSet<ProjectInfo> projectInfoes = new ModelDbContext().ProjectInfoes;
            IEnumerable<ProjectInfo> launchedsQuery = from projectInfo in projectInfoes
                                                      where projectInfo.IsLanuched == "Y"
                                                      select projectInfo;
            foreach (var it in launchedsQuery)
            {
                IRow newRow = sheet.CreateRow(rowIndex);
                for (int i = 0; i < headValues.Length; ++i)
                    newRow.CreateCell(i).SetCellValue((typeof(ProjectInfo).GetProperty(headValues[i]).GetValue(it))?.ToString() ?? "");
                ++rowIndex;
            }
            //FileStream:写回磁盘
            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            ms.Position = 0;//如果不设为0，将返回空
            return File(ms, "application/vnd.ms-excel", "Launched.xls");
        }
	}
}