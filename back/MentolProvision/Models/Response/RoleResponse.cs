using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolProvision.Models.Response
{
    /// <summary>
    /// Модель для работы с ролью
    /// </summary>
    public class RoleResponse
    {
	    public int RoleId { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string RoleDescription { get; set; }
    }
}
