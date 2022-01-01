using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MentolProvisionModel
{
    /// <summary>
    /// Модель для работы с вендорами
    /// </summary>
    public class Vendor
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

        public List<VendorModel> VendorModels { get; set; } = new List<VendorModel>();
    }
}
