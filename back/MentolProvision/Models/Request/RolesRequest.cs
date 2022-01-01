using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolProvision.Models.Request
{
    /// <summary>
    /// Модель для работы с ролями
    /// </summary>
    public class RolesRequest
    {
        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public int? Idr { get; set; }

        /// <summary>
        /// Наименование роли
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание роли
        /// </summary>
        public string Description { get; set; }
    }
}
