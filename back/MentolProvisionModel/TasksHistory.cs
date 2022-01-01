using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentolProvisionModel
{
    /// <summary>
    /// Модель для работы с историей задания
    /// </summary>
    [Table("TasksHistory")]
    public class TasksHistory
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int Idr { get; set; }

        /// <summary>
        /// Дата и время создания
        /// </summary>
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// Дата и время запуска
        /// </summary>
        public DateTime? DateRun { get; set; }

        /// <summary>
        /// Номер задания
        /// </summary>
        public int? TaskId { get; set; }
        /// <summary>
        /// Задача
        /// </summary>
        public ServerTask Task { get; set; }

        /// <summary>
        /// Статус 
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Описание статуса
        /// </summary>
        public string StatusDesc { get; set; }

        /// <summary>
        /// Дата и время окончания выполнения задания
        /// </summary>
        public DateTime? DateEnd { get; set; }
    }
}
