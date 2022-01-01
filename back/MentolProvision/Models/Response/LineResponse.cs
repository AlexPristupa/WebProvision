namespace MentolProvision.Models.Response
{
	/// <summary>
	/// Модель для работы с линиями связи
	/// </summary>
	public class LineResponse
	{ 
		/// <summary>
		/// ИД линии
		/// </summary>
		public int LineId { get; set; }

		/// <summary>
		/// Номер телефона линии связи
		/// </summary>
		public string LinePhoneNumber { get; set; }
		
		/// <summary>
		/// Описание линии
		/// </summary>
		public string LineDescription { get; set; }

		/// <summary>
		/// LineAlertingName
		/// </summary>
		public string LineAlertingName { get; set; }

		/// <summary>
		/// LineASCIIAlertingName
		/// </summary>
		public string LineASCIIAlertingName { get; set; }

		/// <summary>
		/// LineDisplayCallerId
		/// </summary>
		public string LineDisplayCallerId { get; set; }

		/// <summary>
		/// LineASCIIDisplayCallerId
		/// </summary>
		public string LineASCIIDisplayCallerId { get; set; }

		/// <summary>
		/// LineUserAssociatedLine
		/// </summary>
		public string LineUserAssociatedLine { get; set; }
	}
}
