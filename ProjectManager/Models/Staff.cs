﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
	public class Staff
	{
		[Key]
        public int Id { get; set; }
		public string Name { get; set; }
		public string Department { get; set; }
		public string IsOnJob { get; set; }
	}
}