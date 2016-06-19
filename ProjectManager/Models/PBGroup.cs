using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
    public class PBGroup
    {
        [Key]
        public int Id { get; set; }
        public string GroupName { get; set; }
    }
}