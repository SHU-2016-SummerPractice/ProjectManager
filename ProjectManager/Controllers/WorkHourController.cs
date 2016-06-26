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
			var row = list.GroupBy(l => l.StaffId);
			List<List<string>> res = new List<List<string>>();
			res.Add(new List<string>() { "ID", "Role", "Name" });
			foreach (var item in row.FirstOrDefault().OrderBy(m => m.StartDate))
			{
				res.First().Add(item.StartDate.ToShortDateString() + " - " + item.StartDate.AddDays(5).ToShortDateString());
			}
			foreach (var item in row)
			{
				res.Add(new List<string>(){
					item.FirstOrDefault().StaffId.ToString(),
					item.FirstOrDefault().Department??"Other",
					item.FirstOrDefault().Name});
				foreach (var cell in item.OrderBy(c => c.StartDate))
				{
					res.Last().Add(cell.WorkContent ?? "");
				}
			}
			return Json(res, JsonRequestBehavior.AllowGet);
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