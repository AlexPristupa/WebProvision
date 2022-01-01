using System.Collections.Generic;
using MentolProvision.Models.Response.Common;

namespace MentolProvision.Models.Response.Summaries
{
	public class ServerListSummaryResponse
	{
		public PaginationMetadata Meta { get; set; } = new PaginationMetadata();
		public List<ServerResponse> Servers { get; set; } = new List<ServerResponse>();
	}
}
