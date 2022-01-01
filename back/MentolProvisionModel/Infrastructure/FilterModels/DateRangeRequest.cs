using System;

namespace MentolProvision.Models.Request
{
	public class DateRangeRequest
	{
		public DateTime? DateFrom { get; set; }

		public DateTime? DateTo { get; set; }

		public string TableColumn { get; set; }

		public int? Offset { get; set; }

		public int? Limit { get; set; }

		public bool? OrderDesc { get; set; }
	}
}
