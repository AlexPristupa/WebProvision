using System.Collections.Generic;

namespace MentolProvision.Models.Response
{
    /// <summary>
    /// Модель для работы со списком пользоателей
    /// </summary>
    public class UserSummaryResponse
    {
        /// <summary>
        /// Список пользователей
        /// </summary>
        public List<UserResponse> Users { get; set; }

        /// <summary>
        /// Количество элементов в списке пользоателей
        /// </summary>
        public int TotalCount { get; set; }
    }
}
