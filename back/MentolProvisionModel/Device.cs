using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentolProvisionModel
{
    /// <summary>
    /// Модель для работы с устройствами
    /// </summary>
    [Table("Devices")]
    public class Device
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int Idr { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// IP адрес
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Маркер удаленного объекта
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Линии звязи
        /// </summary>
        [InverseProperty(nameof(Line.Device))]
        public List<Line> Lines { get; set; } = new List<Line>();

        public override string ToString()
        {
	        return $"[Idr: {Idr};Name: {Name}; IpAddress: {IpAddress}]";
        }
    }
}
