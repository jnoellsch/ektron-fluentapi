using Ektron.Cms.Organization;

namespace Ektron.SharedSource.FluentApi
{
    /// <summary>
    /// Represents a set of extensions for <see cref="TaxonomyCriteria"/>.
    /// </summary>
    public static class TaxonomyCriteriaExtensions
    {
        /// <summary>
        /// Includes descendent taxonomy.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <returns>The updated criteria.</returns>
        public static TaxonomyCriteria Recursive(this TaxonomyCriteria criteria)
        {
            criteria.ReturnRecursiveChildren = true;
            return criteria;
        }

        /// <summary>
        /// Adds a filter option.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="field">The field to filter on.</param>
        /// <returns>An instance of <see cref="FilterTaxonomy"/>, which further refines the operator.</returns>
        public static FilterTaxonomy FilteredBy(this TaxonomyCriteria criteria, TaxonomyProperty field)
        {
            return new FilterTaxonomy(criteria, field);
        }
    }
}
