using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MentolProvisionModel
{
    /// <summary>
    /// Модель для работы с моделями вендоров
    /// </summary>
    public class VendorModel
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int Idr { get; set; }

        /// <summary>
        /// ID вендора
        /// </summary>
        public int? VendorId { get; set; }

        /// <summary>
        /// Наименование
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        public Vendor Vendor { get; set; }
    }
}
