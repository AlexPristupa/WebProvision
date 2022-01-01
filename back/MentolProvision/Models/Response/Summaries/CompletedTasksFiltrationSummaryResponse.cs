using System.Collections.Generic;
using MentolProvision.Models.Response.Common;

namespace MentolProvision.Models.Response.Summaries
{
	public class CompletedTasksFiltrationSummaryResponse
	{
		public FiltrationMetadata Meta { get; set; } = new FiltrationMetadata();
		public List<CompletedTaskResponse> Tasks { get; set; } = new List<CompletedTaskResponse>();
	}
}
