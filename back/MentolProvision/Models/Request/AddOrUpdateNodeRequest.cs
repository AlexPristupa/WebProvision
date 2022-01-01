namespace MentolProvision.Models.Request
{
	public class AddOrUpdateNodeRequest: NodeDataRequest
	{
		public int? NodeId { get; set; }

		public string NodeVersion { get; set; }
	}
}
