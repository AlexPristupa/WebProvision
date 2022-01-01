namespace MentolProvision.Models.Response
{
    /// <summary>
    /// Модель для работы с пользователями
    /// </summary>
    public class UserRequest
    {
        /// <summary>
        /// ID роли
        /// </summary>
        public int? Id { get; set; }

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
        /// ID роли
        /// </summary>
        public int? RoleId { get; set; }

        /// <summary>
        /// Sid
        /// </summary>
        public string Sid { get; set; }

        /// <summary>
        /// Провайдер
        /// </summary>
        public string Provider { get; set; }
    }
}
