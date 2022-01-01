namespace MentolProvision.Models.Response
{
	public class NodeResponse
	{
		public int? NodeId { get; set; }

		public string NodeFQDN { get; set; }

		public string NodeIpAddress { get; set; }

		public int? NodePriority { get; set; }

		public string NodeVersion { get; set; }
	}
}
