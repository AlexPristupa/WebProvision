using System;
using MentolProvision.Models.Response.Common;

namespace MentolProvision.Models.Response.Summaries
{
	public class UserWithRoleSummaryResponse
	{
		public UserWithRoleSummaryResponse(UserWithRolesResponse user)
		{
			User = user;
		}

		public UserWithRolesResponse User { get; }
	}
}
