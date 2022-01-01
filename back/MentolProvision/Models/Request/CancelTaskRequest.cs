using MentolProvision.Models.Response.Common;

namespace MentolProvision.Models.Request
{
	public class CancelTaskRequest
	{
		public CancelTaskRequestMetadata Meta { get; set; } = new CancelTaskRequestMetadata();

		public CancelTaskData Task { get; set; } = new CancelTaskData();
	}
}
