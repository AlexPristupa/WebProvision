using System.Collections.Generic;
using System.Text.Json.Serialization;
using MentolProvisionModel;

namespace MentolProvision.Models.Response
{
    /// <summary>
    /// Модель для работы с серверами
    /// </summary>
    public class ServerResponse
    {
	    public ServerResponse(Server server)
	    {
		    ServerId = server.Idr;
		    ServerVendorModelId = server.ModelId;
		    ServerPassword = server.Password;
		    ServerLogin = server.Login;
		    ServerFQDN = server.FQDN;
		    ServerDescription = server.Description;
		    Enable = server.IsEnabled;
		    ServerIpAddress = server.IpAddress;
		    ServerPort = server.Port;
		    ServerIsTest = server.TestBench ?? false;
	    }

	    public ServerResponse()
	    {
		    
	    }

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

        public bool? ServerIsEnabled { get; set; }

        public bool? ServerIsTest { get; set; }

        public string NodeId { get; set; }

        public List<NodeResponse> Nodes { get; set; } = new List<NodeResponse>();
    }
}
