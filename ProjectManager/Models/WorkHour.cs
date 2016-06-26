using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class WorkHour
    {
        [Key]
        public int Id { get; set; }

        public int StaffId { get; set; }
        public DateTime StartDate { get; set; }
        public int WorkType { get; set; }
        public string WorkContent { get; set; }
    }
}