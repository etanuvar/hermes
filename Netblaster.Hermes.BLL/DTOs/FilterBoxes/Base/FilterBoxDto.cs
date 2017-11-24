namespace Netblaster.Hermes.BLL.DTOs.FilterBoxes.Base
{
    public class FilterBoxDTO
    {
        // SORTING
        /// <summary>
        /// Gets or sets the sort parameter.
        /// </summary>
        /// <value>The sort parameter.</value>
        public virtual string SortParam { get; set; }

        // PAGINATION

        /// <summary>
        /// Gets or sets the page.
        /// </summary>
        /// <value>The page.</value>
        public virtual int Page { get; set; }
        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        /// <value>The size of the page.</value>
        public virtual int PageSize { get; set; }
        /// <summary>
        /// Gets or sets the items total count.
        /// </summary>
        /// <value>The items total count.</value>
        public virtual int ItemsTotalCount { get; set; }
    }
}
