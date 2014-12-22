namespace Ektron.SharedSource.FluentApi
{
    using Ektron.Cms.Common;
    using Ektron.Cms.Content;

    /// <summary>
    /// Represents a set of extensions for <see cref="ContentCollectionCriteria"/>.
    /// </summary>
    public static class ContentCollectionCriteriaExtensions
    {
        /// <summary>
        /// Adds an exact match filter for <see cref="ContentCollectionProperty.Id"/>.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="id">The collection id.</param>
        /// <returns>The updated criteria.</returns>
        public static ContentCollectionCriteria ByCollection(this ContentCollectionCriteria criteria, long id)
        {
            criteria.AddFilter(id);
            return criteria;
        }

        /// <summary>
        /// Adds an exact match filter for <see cref="ContentCollectionProperty.Title"/>.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="name">The collection title.</param>
        /// <returns>The updated criteria.</returns>
        public static ContentCollectionCriteria ByCollection(this ContentCollectionCriteria criteria, string name)
        {
            criteria.AddFilter(name);
            return criteria;
        }

        /// <summary>
        /// Sets the <see cref="ContentCollectionCriteria.OrderByCollectionOrder"/> property to true.
        /// Ths informs the query how to sort results by how the items are ordered within the collection.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <returns>The updated criteria.</returns>
        public static ContentCollectionCriteria Ordered(this ContentCollectionCriteria criteria)
        {
            criteria.OrderByCollectionOrder = true;
            return criteria;
        }

        /// <summary>
        /// Adds a filter option.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="field">The field to filter on.</param>
        /// <returns>An instance of <see cref="FilterContentCollection"/>, which further refines the operator.</returns>
        public static FilterContentCollection FilteredBy(this ContentCollectionCriteria criteria, ContentCollectionProperty field) 
        {
            return new FilterContentCollection(criteria, field);
        }
    }
}
