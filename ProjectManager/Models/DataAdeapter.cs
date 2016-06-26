using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace ProjectManager.Models
{
    public class DomainAdapter
    {
        static ModelDbContext mdc = new ModelDbContext();
        
        public static List<StaffInfo> getStaffInfoForWorkPercent()
        {   
            List<StaffInfo> res = new List<StaffInfo>();
            var workPercent = from percentageSet in mdc.Domains select percentageSet;
            foreach (var wp in workPercent)
            {
                var info = from staff in mdc.Staffs
                           where wp.StaffId == staff.Id
                           select new { staff.Name, staff.Department, staff.IsOnJob };
                res.Add(new StaffInfo{
                    Name = info.FirstOrDefault().Name,
                    Department = info.FirstOrDefault().Department,
                    ID = wp.ID,
                    WorkPercentage = wp.Percentage,
                    WorkLoadPercentage = wp.WorkLoadPercentage,
                    InvolvedProject = wp.Project,
                    Memo = wp.Memo,
                    IsOnJob = info.FirstOrDefault().IsOnJob
                });
            }
            return res;
        }

        public static List<StaffWorkHoursInfo> getWorkHoursForAll(DateTime startday)
        {
            DateTime endday = startday.AddDays(7);
            var res = from w in mdc.WorkHours
                      join s in mdc.Staffs
                      on w.StaffId equals s.Id
                      where w.StartDate >= startday && w.StartDate < endday
                      select new StaffWorkHoursInfo()
                      {
                          StaffId = w.StaffId,
                          Name = s.Name,
                          StartDate = w.StartDate,
                          Department = s.Department,
                          WorkType = w.WorkType,
                          WorkContent = w.WorkContent,
                      };
            var list = res.ToList();
            RefreshWorkContent(ref list);
            return list;
        }

        public static void RefreshWorkContent(ref List<StaffWorkHoursInfo> res)
        {
            foreach (var item in res)
            {
                switch (item.WorkType)
                {
                    case 1:
                        item.WorkContent = "Support";
                        break;
                    case 2:
                        item.WorkContent = "请假";
                        break;
                    case 3:
                        item.WorkContent = "Arch";
                        break;
                }
            }
        }

        public static string UpdateWorkHoursInfo(List<UpdateWorksInfo> list)
        {
            StringBuilder restr = new StringBuilder();
            int errIndex=0;
            foreach (var item in list)
            {
                DateTime startdate;
                string dateString = item.date.Split('-')[0].Trim();
                if (!DateTime.TryParse(dateString, out startdate))
                {
                    restr.AppendLine(errIndex++ + ": "+ dateString + " is illegal DateTime");
                    continue;
                }
                int staffid = Convert.ToInt32(item.staffid);
                var oldInfo = mdc.WorkHours.FirstOrDefault(m =>
                    m.StartDate.Equals(startdate) &&
                    m.StaffId == staffid);
                if( oldInfo != null)
                {
                    oldInfo.WorkContent = item.work;
                    oldInfo.WorkType = getWorkType(item.work);
                    mdc.Entry(oldInfo).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    mdc.WorkHours.Add(new WorkHour
                    {
                        StaffId = staffid,
                        StartDate = startdate,
                        WorkContent = item.work,
                        WorkType = getWorkType(item.work)
                    });
                }
            }

            if (errIndex == 0) 
            {
                try
                {
                    mdc.SaveChanges();
                }
                catch (InvalidOperationException ex)
                {
                    restr.AppendLine(errIndex++ + ": " + ex.Message);
                }
                catch (Exception ex)
                {
                    restr.AppendLine(errIndex++ + ": " + ex.Message);
                }   
            }

            if (errIndex != 0)
                return restr.ToString();
            else
                return "true";
        }

        public static int getWorkType(string workContent)
        {
            switch (workContent)
            {
                case "Support":
                    return 1;
                case "请假":
                    return 2;
                case "Arch":
                    return 3;
                default:
                    return 0;
            }
        }
    }

	public class StaffWorkHoursInfo
	{
		public int StaffId { get; set; }
		public string Name { get; set; }
		public string Department { get; set; }
		public DateTime StartDate { get; set; }
		public string StartDateString { get; set; }
		/// <summary>
		/// 0 --- Project
		/// 1 -- Surpport
		/// 2 --- 请假
		/// 3 --- Arch
		/// </summary>
		public int WorkType { get; set; }
		/// <summary>
		/// if WorkType isn't 0, It means the Work Content, else set null
		/// </summary>
		public string WorkContent { get; set; }
	}

	public class StaffInfo
	{
		/// <summary>
		/// staff id
		/// </summary>
		public int ID { get; set; }
		public string Name { get; set; }
		public string Department { get; set; }
		public string IsOnJob { get; set; }
		public int? WorkPercentage { get; set; }
		public int? WorkLoadPercentage { get; set; }
		public string InvolvedProject { get; set; }
		public string Memo { get; set; }
	}

	public class UpdateWorksInfo
	{
		//TODO:猜测UpdateWorksInfo的定义
		public string date { get; set; }
		public string staffid { get; set; }
		public string work { get; set; }
	}
}