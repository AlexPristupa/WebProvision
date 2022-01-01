using System.Collections.Generic;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace MentolProvision.Models.Response
{
    /// <summary>
    /// Модель для работы с устройствами
    /// </summary>
    public class DeviceResponse
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

        public string LinePhoneNumber { get; set; }

        public List<LineResponse> Lines { get; set; } = new List<LineResponse>();
	}
}