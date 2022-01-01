namespace MentolProvisionRepository.Filter
{
    public class FilterRequest
    {
        /// <summary>
        /// asc or desc
        /// if desc = true
        /// </summary>
        public bool? OrderDesc { get; set; }
        /// <summary>
        /// sortable column name
        /// </summary>
        public string TableColumn { get; set; }

        /// <summary>
        /// number of rows to skip
        /// </summary>
        public int? Offset { get; set; }
        /// <summary>
        /// number of rows to get
        /// </summary>
        public int? Limit { get; set; }
        /// <summary>
        /// search pattern
        /// </summary>
        public string Search { get; set; }
    }
}
