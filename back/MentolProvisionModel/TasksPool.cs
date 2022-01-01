using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentolProvisionModel
{
    /// <summary>
    /// Модель для работы с пулом заданий
    /// </summary>
    [Table("TasksPool")]
    public class TasksPool
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

        [ForeignKey(nameof(TaskId))]
        public virtual ServerTask Task { get; set; }
    }
}