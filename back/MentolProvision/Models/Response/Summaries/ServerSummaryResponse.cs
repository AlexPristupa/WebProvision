using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MentolProvision.Models.Response.Common;

namespace MentolProvision.Models.Response.Summaries
{
	public class ServerSummaryResponse
	{
		public OperationStatus Status { get; set; } = new OperationStatus();

		public ServerResponse Data { get; set; } = new ServerResponse();
	}
}
