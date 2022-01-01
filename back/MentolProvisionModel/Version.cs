using System;
using System.ComponentModel.DataAnnotations;

namespace MentolProvisionModel
{
	public class Version
	{
		[Key]
		public int Idr { get; set; }

		public string VersionValue { get; set; }

		public string Type { get; set; }

		public string Description { get; set; }

		public DateTime? DateRecord { get; set; }

		public bool? IsLastRecord { get; set; }

		public int? ServerId { get; set; }

		public Server Server { get; set; }

		public int? NodeId { get; set; }

		public Node Node { get; set; }
	}
}
