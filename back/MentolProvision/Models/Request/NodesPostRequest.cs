using System.Collections.Generic;

namespace MentolProvision.Models.Request
{
	public class NodesPostRequest
	{
		public int? ServerId { get; set; }
		public List<NodeDataRequest> Nodes { get; set; } = new List<NodeDataRequest>();
	}
}
