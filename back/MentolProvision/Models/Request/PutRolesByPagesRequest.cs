using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolProvision.Models.Request
{
    /// <summary>
    /// Модель для работы с изменением доступа роли к страницам
    /// </summary>
    public class PutRolesByPagesRequest
    {
        /// <summary>
        /// Cписок id страниц
        /// </summary>
        public List<int> PageIdList { get; set; }
    }
}
