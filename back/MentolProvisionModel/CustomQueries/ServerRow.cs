using System.Collections.Generic;

namespace MentolProvisionModel.CustomQueries
{
	public class ServerRow
	{
		/// <summary>
		/// PK
		/// </summary>
		public int ServerId { get; set; }

		/// <summary>
		/// Наименование
		/// </summary>
		public string ServerFQDN { get; set; }

		/// <summary>
		/// Описание
		/// </summary>
		public string ServerDescription { get; set; }

		/// <summary>
		/// Удаленный IP адресс
		/// </summary>
		public string ServerIpAddress { get; set; }

		/// <summary>
		/// Логин
		/// </summary>
		public string ServerLogin { get; set; }

		/// <summary>
		/// Пароль
		/// </summary>
		public string ServerPassword { get; set; }

		/// <summary>
		/// Порт
		/// </summary>
		public int? ServerPort { get; set; }

		/// <summary>
		/// Доступен
		/// </summary>
		public bool? Enable { get; set; }

		/// <summary>
		/// ServerVendorModelId
		/// </summary>
		public int? ServerVendorModelId { get; set; }

		public string ServerVendorName { get; set; }

		public string ServerVersion { get; set; }

		public bool ServerIsEnabled { get; set; }

		public bool ServerIsTest { get; set; }

		public List<NodeRow> Nodes { get; set; } = new List<NodeRow>();
    }
}
