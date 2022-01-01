using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MentolProvisionModel
{
	[Table("Lines")]
	public class Line
	{
		[Key]
		public int Idr { get; set; }

		public string PhoneNumber { get; set; }

		public string Description { get; set; }

		public string AlertingName { get; set; }

		[Column("ASCIIAlertingName")]
		public string AsciiAlertingName { get; set; }

		[Column("DisplayCallerID")]
		public string DisplayCallerId { get; set; }

		[Column("ASCIIDisplayCallerID")]
		public string AsciiDisplayCallerId { get; set; }

		public string UserAssociatedLine { get; set; }

		public int DeviceId { get; set; }

		[ForeignKey(nameof(DeviceId))]
		public virtual Device Device { get; set; }

		public virtual List<ServerTask> Tasks { get; set; } = new List<ServerTask>();
	}
}
