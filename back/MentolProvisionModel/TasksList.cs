using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentolProvisionModel
{
    /// <summary>
    /// Модель для работы со списком заданий
    /// </summary>
    [Table("Taskslist")]
    public class TasksList
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int Idr { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Частное название
        /// </summary>
        public string PrivateName { get; set; }
    }
}