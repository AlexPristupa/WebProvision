namespace MentolProvision.Models.Response
{
    /// <summary>
    /// Модель для работы со страницами
    /// </summary>
    public class PageResponse
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Idr { get; set; }

        /// <summary>
        /// Отображаемое наименование
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// ID родительской страницы
        /// </summary>
        public int? ParentId { get; set; }
    }
}
