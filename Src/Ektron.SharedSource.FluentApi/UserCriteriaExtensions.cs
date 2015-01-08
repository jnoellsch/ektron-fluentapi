namespace Ektron.SharedSource.FluentApi
{
    using Ektron.Cms.User;

    /// <summary>
    /// Represents a set of extensions for <see cref="UserCriteria"/>.
    /// </summary>
    public static class UserCriteriaExtensions
    {
        /// <summary>
        /// Sets the <see cref="UserCriteria.ReturnCustomProperties"/> property to true.
        /// Ths informs the query to return user properties.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <returns>The updated criteria.</returns>
        public static UserCriteria WithCustomProperties(this UserCriteria criteria)
        {
            criteria.ReturnCustomProperties = true;
            return criteria;
        }

        //TODO: Add FilteredBy on top of UserCriteriaExtensions
        /////// <summary>
        /////// Adds a filter option.
        /////// </summary>
        /////// <param name="criteria">The criteria to extend.</param>
        /////// <param name="field">The field to filter on.</param>
        /////// <returns>An instance of <see cref="FilterFolder"/>, which further refines the operator.</returns>
        ////public static FilterUser FilteredBy(this UserCriteria criteria, UserProperty field)
        ////{
        ////    return new FilterUser(criteria, field);
        ////}
    }
}
