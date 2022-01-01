using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolProvision.Models.Request
{
    /// <summary>
    /// Модель для изменения доступа роли к объектам
    /// </summary>
    public class PutRolesObjectsRequest
    {
        public List<ObjectIdItem> ObjectsIdList { get; set; }
    }

    public class ObjectIdItem
    {
        /// <summary>
        /// Идентификатор обьекта
        /// </summary>
        public int ObjectId { get; set; }

        /// <summary>
        /// Идентификатор действия
        /// </summary>
        public int ActionId { get; set; }
    }
}
