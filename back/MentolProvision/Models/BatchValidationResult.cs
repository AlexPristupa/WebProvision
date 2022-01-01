namespace MentolProvision.Models
{
	public class BatchValidationResult
	{
		public BatchValidationResult(string error = null)
		{
			Error = error;
		}

		public bool IsSucceeded => string.IsNullOrEmpty(Error);

		public string Error { get; }

		public static BatchValidationResult Succeeded => new BatchValidationResult();
	}
}
