using System;

namespace MentolProvision.Models.Response
{
	public class AcceptedTaskData
	{
		public int TaskTypeId { get; set; }

		public string TaskDescription { get; set; }

		public DateTime? TaskDateRun { get; set; }

		public int? DeviceId { get; set; }

		public int? ServerTestId { get; set; }

		public string TaskJsonNew { get; set; }

		public string TaskJsonOld { get; set; }
	}
}
