using System.Collections.Generic;

namespace MentolProvision.Models.Request
{
	public class ServerPostRequest
	{
		/// <summary>
		/// Домен сервера
		/// </summary>
		public string ServerFQDN { get; set; }

		/// <summary>
		/// Удаленный IP адресс
		/// </summary>
		public string ServerIpAddress { get; set; }

		/// <summary>
		/// Порт
		/// </summary>
		public int? ServerPort { get; set; }

		/// <summary>
		/// Логин
		/// </summary>
		public string ServerLogin { get; set; }

		/// <summary>
		/// Пароль
		/// </summary>
		public string ServerPassword { get; set; }

		/// <summary>
		/// Описание
		/// </summary>
		public string ServerDescription { get; set; }

		public int? ServerVendorModelId { get; set; }

		/// <summary>
		/// Доступен
		/// </summary>
		public bool? ServerIsEnabled { get; set; }

		public bool? ServerIsTest { get; set; }

		public List<NodeDataRequest> Nodes { get; set; } = new List<NodeDataRequest>();
	}
}
