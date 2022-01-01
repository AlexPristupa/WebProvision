using System;

namespace MentolProvisionModel.CustomQueries
{
	public class TaskQueryRow
	{
		public int Idr { get; set; }

		public string Description { get; set; }

		public int? DevicesId { get; set; }

		public int? ServerId { get; set; }

		public int? TaskListId { get; set; }

		public int? TestServerId { get; set; }

		public int? UsersId { get; set; }

		public string TaskId { get; set; }

		public string PhoneNumber { get; set; }

		public string TaskListName { get; set; }

		public string DisplayName { get; set; }

		public DateTime? DateRun { get; set; }

		public string TestServerName { get; set; }

		public string ServerName { get; set; }

		public string UserLogin { get; set; }

		public string DeviceName { get; set; }
	}
}
