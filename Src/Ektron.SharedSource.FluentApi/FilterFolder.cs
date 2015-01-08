namespace Ektron.SharedSource.FluentApi
{
    using Ektron.Cms;
    using Ektron.Cms.Common;

    /// <summary>
    /// Represents filter operators for <see cref="FolderCriteria"/>.
    /// </summary>
    public class FilterFolder : Filter<FolderCriteria>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterFolder"/> class.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="field">The field to filter on.</param>
        public FilterFolder(FolderCriteria criteria, FolderProperty field)
            : base(criteria)
        {
            this.Field = field;
        }

        /// <summary>
        /// Gets the field to filter on.
        /// </summary>
        public FolderProperty Field { get; private set; }

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
