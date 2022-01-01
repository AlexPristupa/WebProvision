using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentolProvisionModel
{
    /// <summary>
    /// Модель для работы с пользователями
    /// </summary>
    public class UserToRole
    {
        /// <summary>
        /// ID роли
        /// </summary>
        [Key]
        public int RoleId { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// ID пользователя
        /// </summary>
        [Key]
        public int UserId { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }
    }
}
