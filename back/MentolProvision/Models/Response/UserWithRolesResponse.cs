using System.Collections.Generic;

namespace MentolProvision.Models.Response
{
	public class UserWithRolesResponse
	{
		public int UserId { get; set; }

		public string UserDisplayName { get; set; }

		public string UserEmail { get; set; }

		public string UserSid { get; set; }

		public string UserProvider { get; set; }

		public string UserPosition { get; set; }

		public bool UserIsDeleted { get; set; }

		public string UserPassword { get; set; }

		public List<RoleResponse> Roles { get; set; } = new List<RoleResponse>();
	}
}
