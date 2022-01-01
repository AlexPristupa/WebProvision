namespace MentolProvision.Models.Response.Common
{
	public class PaginationMetadata
	{
		public int Count { get; set; }

		public int Offset { get; set; }

		public int Limit { get; set; }

		public bool OrderDesc { get; set; }
	}
}
