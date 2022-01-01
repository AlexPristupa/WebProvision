using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentolProvisionModel
{
    /// <summary>
    /// Модель для работы с серверами
    /// </summary>
    [Table("Servers")]
    public class Server
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int Idr { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string FQDN { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Удаленный IP адресс
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Режим
        /// </summary>
        public string Mode { get; set; }

        /// <summary>
        /// Порт
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// Доступен
        /// </summary>
        [Column("Enable")]
        public bool? IsEnabled { get; set; }

        /// <summary>
        /// Пройден тестовый бенчмарк
        /// </summary>
        public bool? TestBench { get; set; }

        /// <summary>
        /// Id модели
        /// </summary>
        public int? ModelId { get; set; }

        public int? Timezone { get; set; }

        public DateTime? DefStartTime { get; set; }

        public bool? IsDeleted { get; set; }

        public List<Node> Nodes { get; set; } = new List<Node>();

        public List<Version> Versions { get; set; } = new List<Version>();

        [ForeignKey(nameof(ModelId))]
        public VendorModel VendorModel { get; set; }
    }
}
