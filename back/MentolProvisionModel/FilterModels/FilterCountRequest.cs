namespace MentolProvisionModel.Infrastructure
{
	public class FilterCountRequest
	{
		/// <summary>
		/// search pattern
		/// </summary>
		public string Search { get; set; }

		/// <summary>
		/// Имя колонки, по которой производится поиск
		/// </summary>
		public string TableColumn { get; set; }
	}
}
