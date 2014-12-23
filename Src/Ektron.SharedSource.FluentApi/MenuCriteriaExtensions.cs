namespace Ektron.SharedSource.FluentApi
{
    using Ektron.Cms.Framework.Organization;
    using Ektron.Cms.Organization;

    /// <summary>
    /// Represents a set of extensions for <see cref="MenuCriteria"/>.
    /// </summary>
    public static class MenuCriteriaExtensions
    {
        /// <summary>
        /// Adds an exact match filter for <see cref="MenuProperty.Id"/>.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="id">The menu id.</param>
        /// <returns>The updated criteria.</returns>
        public static MenuCriteria ByMenu(this MenuCriteria criteria, long id)
        {
            criteria.FilteredBy(MenuProperty.Id).EqualTo(id);
            return criteria;
        }

        /// <summary>
        /// Adds an exact match filter for <see cref="MenuProperty.Text"/>.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="name">The menu name.</param>
        /// <returns>The updated criteria.</returns>
        public static MenuCriteria ByMenu(this MenuCriteria criteria, string name)
        {
            criteria.FilteredBy(MenuProperty.Text).EqualTo(name);
            return criteria;
        }

        /// <summary>
        /// Adds a filter option.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="field">The field to filter on.</param>
        /// <returns>An instance of <see cref="FilterMenu"/>, which further refines the operator.</returns>
        public static FilterMenu FilteredBy(this MenuCriteria criteria, MenuProperty field)
        {
            return new FilterMenu(criteria, field);
        }
    }
}