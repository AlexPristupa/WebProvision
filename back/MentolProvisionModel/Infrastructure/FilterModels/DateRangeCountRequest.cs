using System;

namespace MentolProvisionModel.Infrastructure.FilterModels
{
	public class DateRangeCountRequest
	{
		/// <summary>
		/// Нижняя граница фильтра
		/// </summary>
		public DateTime? DateFrom { get; set; }

		/// <summary>
		/// Верхняя граница фильтра
		/// </summary>
		public DateTime? DateTo { get; set; }

		/// <summary>
		/// Имя колонки с датой, по которой производится поиск
		/// </summary>
		public string TableColumn { get; set; }
	}
}
