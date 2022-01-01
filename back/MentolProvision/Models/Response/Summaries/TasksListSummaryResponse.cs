using System.Collections.Generic;
using MentolProvision.Models.Response.Common;

namespace MentolProvision.Models.Response
{
	public class TasksListSummaryResponse: IHasData<TasksListResponse>
	{
		public CollectionMetadata Meta { get; set; } = new CollectionMetadata();
		public List<TasksListResponse> Data { get; set; } = new List<TasksListResponse>();
		public OperationStatus Status { get; set; } = new OperationStatus();
	}
}
