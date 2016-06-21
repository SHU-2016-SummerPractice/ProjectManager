using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class ProjectInfo
    {
        [Key]
        public int ID { get; set; }
        public int ProjectID { get; set; }
        public int? LO { get; set; }
        public string MISStatus { get; set; }
        public string IsLanuched { get; set; }
        public int? CNPMId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public DateTime? DelayReleaseDate { get; set; }
        public DateTime? LanuchDate { get; set; }
        public DateTime? DelayLanuchDate { get; set; }
    }
}