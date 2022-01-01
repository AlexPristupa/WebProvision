using System;

namespace MentolProvisionModel.CustomQueries
{
	public class CompletedTaskRow
	{
		public int TaskCompletedId { get; set; }

		public int? TaskId { get; set; }

		public string DevicePhoneNumber { get; set; }

		public string TaskType { get; set; }

		public string UserName { get; set; }

		public string UserLogin { get; set; }

		public string TaskDescription { get; set; }

		public DateTime? TaskDateCreate { get; set; }

		public DateTime? TaskDateRun { get; set; }

		public DateTime? TaskDateEnd { get; set; }

		public string TaskStatus { get; set; }

		public string TaskStatusDesc { get; set; }

		public int? TaskCancelId { get; set; }

		public int? ServerTestId { get; set; }
		public string ServerFQDN { get; set; }
	}
}
