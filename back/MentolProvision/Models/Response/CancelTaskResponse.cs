using MentolProvision.Models.Response.Metadatas;

namespace MentolProvision.Models.Response
{
	public class CancelTaskResponse<T> where T: class, new()
	{
		public CancelTaskMetadata Meta { get; set; } = new CancelTaskMetadata();

		public TaskTransactionStatus Status { get; set; } = new TaskTransactionStatus();

		public T Task { get; set; } = new T();
	}
}
