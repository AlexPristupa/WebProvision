using System;
using System.Collections.Generic;
using MentolProvision.Models.Response.Common;

namespace MentolProvision.Models.Response.Summaries
{
	public class UsersWithRolesSummaryResponse
	{
		public UsersWithRolesSummaryResponse(FiltrationMetadata meta, List<UserWithRolesResponse> users)
		{
			Meta = meta ?? throw new ArgumentNullException(nameof(meta));
			Users = users ?? new List<UserWithRolesResponse>();
		}

		public FiltrationMetadata Meta { get; }
		public List<UserWithRolesResponse> Users { get; }
	}
}
