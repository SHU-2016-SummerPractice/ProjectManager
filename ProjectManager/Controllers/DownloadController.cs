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

			HSSFWorkbook book = new HSSFWorkbook();
			ISheet sheet = book.CreateSheet("Project");
			
			IRow headRow = sheet.CreateRow(0);
	        string[] headValues = { "ProjectID", "LO", "MISStatus", "IsLanuched", "CNPMId", "StartDate", "EndDate", "ReleaseDate", "DelayReleaseDate", "LanuchDate", "DelayLanuchDate"};
	        int[] widthValues = {2500,1500, 2500, 2600, 5000, 5000, 5000, 5000, 5000, 5000, 5000};
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

			MemoryStream ms = new MemoryStream();
			book.Write(ms);
			ms.Position = 0;//如果不设为0，将返回空
			return File(ms, "application/vnd.ms-excel", "Project.xls");
        }
		public ActionResult WorkHour()
		{
			return null;
		}
		public ActionResult WorkPercetage()
		{
			return null;
		}
		public ActionResult Launched()
		{
			return null;
		}
	}
}