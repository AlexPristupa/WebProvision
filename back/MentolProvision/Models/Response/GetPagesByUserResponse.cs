using MentolProvisionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolProvision.Models.Response
{
    /// <summary>
    /// Модель ответа со страницами для пользователя
    /// </summary>
    public class GetPagesByUserResponse
    {
        public List<PageByUser> menuList { get; set; }
    }

    /// <summary>
    /// Модель страницы для пользователя
    /// </summary>
    public class PageByUser
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Отображаемое наименование
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// Идентификатор родительской страницы
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// URL страницы
        /// </summary>
        public string Action { get; set; }
    }
}
