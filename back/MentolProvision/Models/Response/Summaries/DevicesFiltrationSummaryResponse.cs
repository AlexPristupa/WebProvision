using System.Collections.Generic;
using MentolProvision.Models.Response.Common;

namespace MentolProvision.Models.Response.Summaries
{
	public class DevicesFiltrationSummaryResponse
	{
		public FiltrationMetadata Meta { get; set; } = new FiltrationMetadata();

		public List<DeviceResponse> Devices { get; set; } = new List<DeviceResponse>();
	}
}
