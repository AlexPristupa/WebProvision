using System.Collections.Generic;
using MentolProvision.Models.Response.Common;

namespace MentolProvision.Models.Response.Summaries
{
	public class ServersFiltrationSummaryResponse
	{
		public FiltrationMetadata Meta { get; set; } = new FiltrationMetadata();

		public List<ServerResponse> Servers { get; set; } = new List<ServerResponse>();
	}
}
