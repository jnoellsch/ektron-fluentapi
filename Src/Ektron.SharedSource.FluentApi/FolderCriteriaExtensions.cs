namespace Ektron.SharedSource.FluentApi
{
    using Ektron.Cms;
    using Ektron.Cms.Common;

    /// <summary>
    /// Represents a set of extensions for <see cref="FolderCriteria"/>.
    /// </summary>
    public static class FolderCriteriaExtensions
    {
        /// <summary>
        /// Adds an exact match filter for <see cref="FolderProperty.Id"/>.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="id">The folder id.</param>
        /// <returns>The updated criteria.</returns>
        public static FolderCriteria ByFolder(this FolderCriteria criteria, long id)
        {
            criteria.FilteredBy(FolderProperty.Id).EqualTo(id);
            return criteria;
        }

        /// <summary>
        /// Adds an exact match filter for <see cref="FolderProperty.FolderPath"/>.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="path">The folder path.</param>
        /// <returns>The updated criteria.</returns>
        public static FolderCriteria ByFolder(this FolderCriteria criteria, string path)
        {
            criteria.FilteredBy(FolderProperty.FolderPath).EqualTo(path);
            return criteria;
        }

        /// <summary>
        /// Sets the <see cref="FolderCriteria.ReturnChildProperties"/> property to true.
        /// Ths informs the query to return folder details.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <returns>The updated criteria.</returns>
        public static FolderCriteria WithChildProperties(this FolderCriteria criteria)
        {
            criteria.ReturnChildProperties = true;
            return criteria;
        }

        /// <summary>
        /// Adds a filter option.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="field">The field to filter on.</param>
        /// <returns>An instance of <see cref="FilterFolder"/>, which further refines the operator.</returns>
        public static FilterFolder FilteredBy(this FolderCriteria criteria, FolderProperty field)
        {
            return new FilterFolder(criteria, field);
        }
    }
}
