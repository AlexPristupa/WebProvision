using System;
using System.Collections.Generic;
using MentolProvision.Models.Response.Common;
using MentolProvisionModel;

namespace MentolProvision.Models.Response
{
    /// <summary>
    /// Модель для работы со списком заданий
    /// </summary>
    public class TasksSummaryResponse
    {
	    public TasksSummaryResponse(List<PoolTaskResponse> tasks)
	    {
		    Tasks = tasks ?? throw new ArgumentNullException(nameof(tasks));
	    }

        /// <summary>
        /// Список заданий
        /// </summary>
        public List<PoolTaskResponse> Tasks { get; set; }

        public OperationStatus Status { get; set; } = new OperationStatus();

		public FiltrationMetadata Meta { get; set; } = new FiltrationMetadata();
    }
}
