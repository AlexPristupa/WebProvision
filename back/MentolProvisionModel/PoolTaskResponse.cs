using System;
using System.Text.Json.Serialization;

namespace MentolProvisionModel
{
	public class PoolTaskResponse
	{
		public string TaskId { get; set; }

		public string DevicePhoneNumber { get; set; }

		public string TaskType { get; set; }

		public string UserName { get; set; }

		public string TaskDescription { get; set; }

		[JsonIgnore]
		public string ServerId { get; set; }

		public string ServerTestId { get; set; }

		public string ServerFQDN { get; set; }

		public string UserLogin { get; set; }

		public string TaskDateRun { get; set; }

		[JsonIgnore]
		public DateTime? TaskDateRunRaw { get; set; } 
	}
}