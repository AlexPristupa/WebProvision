namespace MentolProvision.Models.Response
{
    /// <summary>
    /// Модель для работы с пользователями
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Отображаемое имя
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Название роли (User.Id => UserToRoles.UserId, UserToRoles.RoleId => Role.Id)
        /// </summary>
        public string RoleName { get; set; }

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
