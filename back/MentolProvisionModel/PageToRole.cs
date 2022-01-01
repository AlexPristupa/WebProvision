using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentolProvisionModel
{
    /// <summary>
    /// Модель для работы со страницами в разрезе по ролям
    /// </summary>
    [Table("PageToRole")]
    public class PageToRole
    {
        /// <summary>
        /// ID роли
        /// </summary>
        [Key]
        public int RoleId { get; set; }

        /// <summary>
        /// ID страницы
        /// </summary>
        [Key]
        public int PageId { get; set; }
    }
}