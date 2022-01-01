using System.Collections.Generic;
using MentolProvision.Models.Response.Common;

namespace MentolProvision.Models.Response.Summaries
{
	public class CompletedTasksDateRangeFiltrationSummaryResponse
	{
		public PaginationMetadata Meta { get; set; } = new PaginationMetadata();

		public List<CompletedTaskResponse> Tasks { get; set; } = new List<CompletedTaskResponse>();
	}
}
