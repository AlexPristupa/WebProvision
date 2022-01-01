namespace MentolProvision.Models.Request
{
	public class NodeDataRequest
	{
		public string NodeFQDN { get; set; }

		public string NodeIpAddress { get; set; }

		public int? NodePriority { get; set; }
	}
}
