using System;
using System.Collections.Generic;
using System.Text;

namespace MentolProvisionModel.CustomQueries
{
	public class UserRow
	{
		public int UserId { get; set; }

		public string UserLogin { get; set; }

		public string UserDisplayName { get; set; }

		public string UserEmail { get; set; }

		public string UserSid { get; set; }

		public string UserProvider { get; set; }

		public string UserPosition { get; set; }

		public bool? UserIsDeleted { get; set; }

		public string UserPassword { get; set; }

		public string RoleId { get; set; }
	}
}
