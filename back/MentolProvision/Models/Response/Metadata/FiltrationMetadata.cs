namespace MentolProvision.Models.Response.Common
{
	public class FiltrationMetadata
	{
		public int? Offset { get; set; }

		public int? Limit { get; set; }

		public bool OrderDesc { get; set; }

		public string TableColumn { get; set; }

		public string Search { get; set; }
	}
}
