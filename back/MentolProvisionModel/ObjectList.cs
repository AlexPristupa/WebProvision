using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentolProvisionModel
{
    /// <summary>
    /// Модель для работы с объектами
    /// </summary>
    [Table("ObjectList")]
    public class ObjectList
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
