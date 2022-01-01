using System;

namespace MentolProvision.Models.Request
{
	public class CancelTaskData
	{
		public int? TaskId { get; set; }

		public string TaskDescription { get; set; }

		public DateTime? TaskDateRun { get; set; }

		public int? ServerTestId { get; set; }
	}
}
