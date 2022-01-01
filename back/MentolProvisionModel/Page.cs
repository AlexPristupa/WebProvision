using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentolProvisionModel
{
    /// <summary>
    /// Модель для работы со страницами
    /// </summary>
    [Table("Pages")]
    public class Page
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int Idr { get; set; }

        /// <summary>
        /// Отображаемое наименование
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// ID родительской страницы
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Закрытое наименование
        /// </summary>
        public string PrivateName { get; set; }

        /// <summary>
        /// URL страницы
        /// </summary>
        public string Action { get; set; }
    }
}