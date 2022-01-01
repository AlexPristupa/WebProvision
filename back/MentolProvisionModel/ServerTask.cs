using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentolProvisionModel
{
    /// <summary>
    /// Модель для работы с задачами
    /// </summary>
    [Table("Tasks")]
    public class ServerTask
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int Idr { get; set; }

        /// <summary>
        /// ID списка задач
        /// </summary>
        public int? TaskListId { get; set; }
        /// <summary>
        /// Список задач
        /// </summary>
        public TasksList TaskList { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }


        /// <summary>
        /// Настройки задачи в старом формате json
        /// </summary>
        public string JsonOld { get; set; }

        /// <summary>
        /// Настройки задачи в новом формате json
        /// </summary>
        public string JsonNew { get; set; }

        /// <summary>
        /// ID тестового сервера
        /// </summary>
        public int? TestServerId { get; set; }

        [ForeignKey(nameof(TestServerId))]
        public virtual Server TestServer { get; set; }

        /// <summary>
        /// ID рабочего сервера
        /// </summary>
        public int? ServerId { get; set; }

        [ForeignKey(nameof(ServerId))]
        public virtual Server ProductionServer { get; set; }

        /// <summary>
        /// ID устройства
        /// </summary>
        [Column("DevicesId")]
        [ForeignKey("Device")]
        public int? DeviceId { get; set; }
        /// <summary>
        /// Устройство
        /// </summary>
        public Device Device { get; set; }

        /// <summary>
        /// ID пользователя
        /// </summary>
        [ForeignKey("User")]
        public int? UsersId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Список запусков задачи
        /// </summary>
        public List<TasksPool> TasksPool { get; set; } = new List<TasksPool>();

        [Column("LinesId")]
        public int? IdLine { get; set; }

        [ForeignKey(nameof(IdLine))]
        public virtual Line Line { get; set; }

        public int? CancelTaskId { get; set; }
    }
}