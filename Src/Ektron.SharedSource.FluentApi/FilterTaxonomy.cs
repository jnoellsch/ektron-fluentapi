namespace Ektron.SharedSource.FluentApi
{
    using Ektron.Cms.Common;
    using Ektron.Cms.Organization;

    /// <summary>
    /// Represents filter operators for <see cref="MenuCriteria"/>.
    /// </summary>
    public class FilterTaxonomy : Filter<TaxonomyCriteria>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FilterMenu"/> class.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="field">The field to filter on.</param>
        public FilterTaxonomy(TaxonomyCriteria criteria, TaxonomyProperty field)
            : base(criteria)
        {
            this.Field = field;
        }

        /// <summary>
        /// Gets the field to filter on.
        /// </summary>
        public TaxonomyProperty Field { get; private set; }

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
