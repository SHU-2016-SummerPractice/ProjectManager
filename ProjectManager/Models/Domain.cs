using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class Domain
    {
        [Key]
        public int ID { get; set; }
        public int StaffId { get; set; }
        public int? Percentage { get; set; }
        public int? WorkLoadPercentage { get; set; }
        public string? Project { get; set; }
        public string? Memo { get; set; }

    }
}