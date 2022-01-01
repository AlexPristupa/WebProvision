using System.Collections.Generic;
using MentolProvision.Models.Response.Common;

namespace MentolProvision.Models.Response
{
    /// <summary>
    /// Модель для работы со списком историй заданий
    /// </summary>
    public class CompletedTasksSummaryResponse
    {
        /// <summary>
        /// Mетаданные коллекции
        /// </summary>
	    public PaginationMetadata Meta { get; set; } = new PaginationMetadata();

	    /// <summary>
        /// Список историй заданий
        /// </summary>
        public List<CompletedTaskResponse> Tasks { get; set; }
    }
}
