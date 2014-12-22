namespace Ektron.SharedSource.FluentApi
{
    using Ektron.Cms.Common;
    using Ektron.Cms.Content;

    /// <summary>
    /// Represents filter operators for <see cref="ContentCollectionCriteria"/>.
    /// </summary>
    public class FilterContentCollection : Filter<ContentCollectionCriteria>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterContentCollection"/> class.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="field">The field to filter on.</param>
        public FilterContentCollection(ContentCollectionCriteria criteria, ContentCollectionProperty field)
            : base(criteria)
        {
            this.Field = field;
        }

        /// <summary>
        /// Gets the field to filter on.
        /// </summary>
        public ContentCollectionProperty Field { get; private set; }

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
