using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MentolProvision.Models.Request
{
    /// <summary>
    /// Модель для авторизации пользователя
    /// </summary>
    public class AuthRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
