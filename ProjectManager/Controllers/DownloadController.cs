using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class DownloadController : Controller
    {
        // GET: Download
        public ActionResult Project()
        {

	        if (!User.Identity.IsAuthenticated)
	        {
		        return RedirectToAction("", "");
	        }
			HSSFWorkbook book = new HSSFWorkbook();
			ISheet sheet = book.CreateSheet("Project");
			

			IRow headRow = sheet.CreateRow(0);
	        string[] headValues = { "ProjectID", "LO", "MISStatus", "IsLanuched", "CNPMId", "StartDate", "EndDate", "ReleaseDate", "DelayReleaseDate", "LanuchDate", "DelayLanuchDate"};
	        int[] widthValues = {10000,10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000, 10000};
			for (int i = 0; i < headValues.Length; ++i)
	        {
		        headRow.CreateCell(i).SetCellValue(headValues[i]);
				sheet.SetColumnWidth(i, widthValues[i]);
			}

			DbSet<ProjectInfo> projectInfoes = new ModelDbContext().ProjectInfoes;
		

			int rowIndex = 0 + 1;

	        foreach (var it in projectInfoes)
	        {
		        IRow newRow = sheet.CreateRow(rowIndex);

				for (int i = 0; i < headValues.Length; ++i)
		        {
					ICell newCell = newRow.CreateCell(i);
			        Object value = (typeof(ProjectInfo).GetProperty(headValues[i]).GetValue(it));
					
					newCell.SetCellValue(value?.ToString() ?? "");
		        }
		        ++rowIndex;
	        }

			MemoryStream ms = new MemoryStream();

			book.Write(ms);

			ms.Position = 0;//如果不设为0，将返回空

			return File(ms, "application/vnd.ms-excel", "Project.xls");
        }
		public FileStreamResult WorkHour()
		{
			HSSFWorkbook book = new HSSFWorkbook();
			ISheet sheet = book.CreateSheet("test_01");

			IRow row = sheet.CreateRow(0);
			row.CreateCell(0).SetCellValue("第一列第一行");

			IRow row2 = sheet.CreateRow(1);
			row2.CreateCell(0).SetCellValue("第二列第一行");

			MemoryStream ms = new MemoryStream();

			book.Write(ms);

			ms.Position = 0;//如果不设为0，将返回空

			return File(ms, "application/vnd.ms-excel", "test.xls");
		}
		public FileStreamResult WorkPercetage()
		{
			HSSFWorkbook book = new HSSFWorkbook();
			ISheet sheet = book.CreateSheet("test_01");

			IRow row = sheet.CreateRow(0);
			row.CreateCell(0).SetCellValue("第一列第一行");

			IRow row2 = sheet.CreateRow(1);
			row2.CreateCell(0).SetCellValue("第二列第一行");

			MemoryStream ms = new MemoryStream();

			book.Write(ms);

			ms.Position = 0;//如果不设为0，将返回空

			return File(ms, "application/vnd.ms-excel", "test.xls");
		}
		public FileStreamResult Launched()
		{
			HSSFWorkbook book = new HSSFWorkbook();
			ISheet sheet = book.CreateSheet("test_01");

			IRow row = sheet.CreateRow(0);
			row.CreateCell(0).SetCellValue("第一列第一行");

			IRow row2 = sheet.CreateRow(1);
			row2.CreateCell(0).SetCellValue("第二列第一行");

			MemoryStream ms = new MemoryStream();

			book.Write(ms);

			ms.Position = 0;//如果不设为0，将返回空

			return File(ms, "application/vnd.ms-excel", "test.xls");
		}
	}
}