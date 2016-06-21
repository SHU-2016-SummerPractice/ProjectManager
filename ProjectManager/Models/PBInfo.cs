using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectManager.Models
{
	public class PBInfo
	{
		[Key]
		public int Id { get; set; }
		public int? ProjectID { get; set; }

		public string PBNo { get; set; }
		public string PBLink { get; set; }
		public string PBName { get; set; }
		public int? USPMId { get; set; }
		//工时相关信息
		public double? Maturity { get; set; }
		public double? EstimatedHours { get; set; }
		public double? MaturityEstimateHours { get; set; }
		public double? ActualHours { get; set; }
		//其他相关信息
		public string NeedInMobile { get; set; }
		public string WillFinish { get; set; }
		public string BizUnit  { get; set; }
		public string Domain { get; set; }
		//看起来不太重要的字段
		public DateTime? AppproveDate { get; set; }
		public DateTime? GQCDate { get; set; }
		public string CRLType { get; set; }
		public string ProjectOwner { get; set; }
		public string BSA { get; set; }
		public string USPT { get; set; }
		public string CDPT { get; set; }
		public double? SCRL { get; set; }
		public string Purpose { get; set; }
		public string Memo { get; set; }
	}
}