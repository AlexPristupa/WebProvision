using MentolProvision.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MentolProvision.Models.Response
{
    /// <summary>
    /// Модель для изменения доступа роли к объектам
    /// </summary>
    public class PutRolesObjectsResponse
    {
        public List<ObjectIdItem> ObjectsIdList { get; set; }
    }
}