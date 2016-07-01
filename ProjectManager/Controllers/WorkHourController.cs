using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using ProjectManager.Models;

namespace ProjectManager.Controllers
{
    public class WorkHourController : Controller
    {
        // GET: WorkHour
        public ActionResult Index()
        {
            return View();
        }

		public JsonResult JsonForHandson()
		{
			List<StaffWorkHoursInfo> list = new List<StaffWorkHoursInfo>();
			for (int i = -4; i < 4; i++)
			{
				list.AddRange(DomainAdapter.getWorkHoursForAll(DateTime.Now.AddDays(i * -7)));
			}
			string[][] res = listFormater(list);
			return Json(res, JsonRequestBehavior.AllowGet);
		}

		public static string[][] listFormater(List<StaffWorkHoursInfo> list)
		{
			list = list.OrderBy(l => l.Department).ToList();
			var row = list.GroupBy(l => l.StaffId);
			//List<List<string>> res = new List<List<string>>();
			//res.Add(new List<string>() { "ID", "Role", "Name" });

			//-------已修复bug, 讲整张表的所有出现的时间添加到Set中,这样便不会有遗漏的数据
			#region 构造表头
			SortedSet<DateTime> times = new SortedSet<DateTime>();
			foreach (var item in list)
			{
				times.Add(item.StartDate);
			}
			DateTime[] arrTimes = times.ToArray();
			string[][] table = new string[row.Count() + 1][];
			table[0] = new string[times.Count() + 3];
			table[0][0] = "ID";
			table[0][1] = "Role";
			table[0][2] = "Name";
			for (int i = 0; i < arrTimes.Length; i++)
			{
				table[0][i + 3] = arrTimes[i].ToShortDateString();
			}
			//foreach (var item in times)
			//{
			//    res.First().Add(item.ToShortDateString() + " - " + item.AddDays(5).ToShortDateString());
			//}
			#endregion

			int indexForTable = 1;
			foreach (var item in row)
			{
				table[indexForTable] = new string[times.Count() + 3];
				table[indexForTable][0] = item.FirstOrDefault().StaffId.ToString();
				table[indexForTable][1] = item.FirstOrDefault().Department ?? "Other";
				table[indexForTable][2] = item.FirstOrDefault().Name;
				//res.Add(new List<string>(){
				//item.FirstOrDefault().StaffId.ToString(),
				//item.FirstOrDefault().Department??"Other",
				//item.FirstOrDefault().Name});

				foreach (var cell in item.OrderBy(c => c.StartDate))
				{
					for (int indexForDate = 0; indexForDate < arrTimes.Length; indexForDate++)
					{
						if (arrTimes[indexForDate].Equals(cell.StartDate))
						{
							table[indexForTable][indexForDate + 3] = cell.WorkContent ?? "";
						}
					}
					// res.Last().Add(cell.WorkContent ?? "");
				}
				indexForTable++;
			}
			return table;
			//return res;
		}

		public JsonResult WorkHoursForDay(string day, string type)
		{
			DateTime dt = DateTime.Parse(day);

			switch (type)
			{
				case "last":
					var res = DomainAdapter.getWorkHoursForAll(dt.AddDays(-7));
					return Json(res, JsonRequestBehavior.AllowGet);
				case "next":
					res = DomainAdapter.getWorkHoursForAll(dt.AddDays(7));
					return Json(res, JsonRequestBehavior.AllowGet);
				default:
					return Json("");
			}

		}

		[HttpPost]
		public JsonResult UpdateWorkhours(string data)
		{
			List<UpdateWorksInfo> list = JsonConvert.DeserializeObject<List<UpdateWorksInfo>>(data);
			string info = DomainAdapter.UpdateWorkHoursInfo(list);
			return Json(info, JsonRequestBehavior.AllowGet);
		}
	}
}