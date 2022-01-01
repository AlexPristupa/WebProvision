using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentolProvisionModel
{
    /// <summary>
    /// Модель для работы с действиями
    /// </summary>
    [Table("ActionList")]
    public class ActionList
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Key]
        public int Idr { get; set; }

        /// <summary>
        /// Отображаемое наименование
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// Внутренние наименование
        /// </summary>
        public string PrivateName { get; set; }
    }
}
