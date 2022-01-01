using System.Collections.Generic;
using MentolProvision.Models.Response.Common;

namespace MentolProvision.Models.Response.Summaries
{
	public class DevicesListSummaryResponse
	{
		public PaginationMetadata Meta { get; set; } = new PaginationMetadata();

		public List<DeviceResponse> Devices { get; set; } = new List<DeviceResponse>();
	}
}
