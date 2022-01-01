using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MentolProvisionModel
{
	[Table("Nodes")]
	public class Node
	{
		[Key]
		public int Idr { get; set; }

		public int? Priority { get; set; }

		public string IpAddress { get; set; }

		public string FQDN { get; set; }

		[Column("ServersId")]
		public int? IdServer { get; set; }

		[ForeignKey(nameof(IdServer))]
		public Server Server { get; set; }

		public List<Version> Versions { get; set; } = new List<Version>();
	}
}
