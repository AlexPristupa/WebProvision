using System;
using System.Text.Json.Serialization;

namespace MentolProvisionModel.CustomQueries
{
	public class TaskDetailsRow
	{
		public int? TaskId { get; set; }

		public string DevicePhoneNumber { get; set; }

		public string TaskType { get; set; }

		public string UserLogin { get; set; }

		public string UserName { get; set; }

		public string TaskDescription { get; set; }

		public DateTime? TaskDateRun { get; set; }

		[JsonIgnore]
		public int? ServerId { get; set; }

		[JsonIgnore]
		public int? ServerTestBench { get; set; }

		[JsonIgnore]
		public string ServerName { get; set; }

		public string ServerTestName { get; set; }

		[JsonIgnore]
		public string ProductionServerName { get; set; }

		public int? ServerTestId { get; set; }

		public string DeviceJson { get; set; }
	}
}
