using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentolProvisionModel
{
    /// <summary>
    /// Модель для работы с пользователями
    /// </summary>
    [Table("Users")]
    public class User
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int Idr { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Отображаемое имя
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Sid
        /// </summary>
        public string Sid { get; set; }

        /// <summary>
        /// Провайдер
        /// </summary>
        public string Provider { get; set; }

        public string Position { get; set; }

        public bool? IsDeleted { get; set; }

        /// <summary>
        /// Отношение Пользователя к Роли
        /// </summary>
        public List<UserToRole> UserToRoles { get; set; } = new List<UserToRole>();
    }
}
