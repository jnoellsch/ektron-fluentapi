namespace Ektron.SharedSource.FluentApi
{
    using Ektron.Cms.Common;
    using Ektron.Cms.Content;

    /// <summary>
    /// Represents filter operators for <see cref="ContentCriteria"/>.
    /// </summary>
    public class FilterContent : Filter<ContentCriteria>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterContent"/> class.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="field">The field to filter on.</param>
        public FilterContent(ContentCriteria criteria, ContentProperty field) : base(criteria)
        {
            this.Field = field;
        }

        /// <summary>
        /// Gets the field to filter on.
        /// </summary>
        public ContentProperty Field { get; private set; }

        /// <summary>
        /// Adds the filter definition to the criteria.
        /// </summary>
        /// <param name="operator">The comparison operator.</param>
        /// <param name="value">The value to filter on.</param>
        protected override void AddFilter(CriteriaFilterOperator @operator, object value)
        {
            this.Criteria.AddFilter(this.Field, @operator, value);
        }
    }
}