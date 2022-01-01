using System.Collections.Generic;

namespace MentolProvisionModel.CustomQueries
{
	public class DeviceRow
	{
		/// <summary>
		/// Идентификатор
		/// </summary>
		public int PhoneId { get; set; }

		/// <summary>
		/// Наименование
		/// </summary>
		public string PhoneName { get; set; }

		/// <summary>
		/// Описание
		/// </summary>
		public string PhoneDescription { get; set; }

		/// <summary>
		/// IP адресс
		/// </summary>
		public string PhoneIpAddress { get; set; }
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
