using System;
using System.Threading.Tasks;
using MentolProvision.Models.Request;

namespace MentolProvision.Models
{
	public class CacheEntry<T> where T: class
	{
		public CacheEntry(T data, string transactionId, CancelTaskData taskData, string userLogin)
		{
			if (taskData == null) throw new ArgumentNullException(nameof(taskData));
			Data = data ?? throw new ArgumentNullException(nameof(data));

			TransactionId = transactionId;
			TestServerId = taskData.ServerTestId;
			UserLogin = userLogin;
			TaskDescription = taskData.TaskDescription;
		}

		public T Data { get; }

		public string TransactionId { get; }

		public int? TestServerId { get; }

		public string UserLogin { get; }

		public string TaskDescription { get; set; }
	}
}
