namespace Ektron.SharedSource.FluentApi
{
    using Ektron.Cms;
    using Ektron.Cms.Common;
    using Ektron.Cms.Content;

    /// <summary>
    /// Represents a set of extensions for <see cref="ContentCriteria"/>.
    /// </summary>
    public static class ContentCriteriaExtensions
    {
        /// <summary>
        /// Sets the <see cref="ContentCriteria.Condition"/> property to <see cref="LogicalOperation.Or"/>.
        /// This informs any filters how to be compared.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <typeparam name="T">The criteria type.</typeparam>
        /// <returns>The updated criteria.</returns>
        public static T OrSeparated<T>(this T criteria) where T : ContentCriteria
        {
            criteria.Condition = LogicalOperation.Or;
            return criteria;
        }

        /// <summary>
        /// Sets the <see cref="ContentCriteria.Condition"/> property to <see cref="LogicalOperation.And"/>. 
        /// This informs any filters how to be compared.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <typeparam name="T">The criteria type.</typeparam>
        /// <returns>The updated criteria.</returns>
        public static T AndSeparated<T>(this T criteria) where T : ContentCriteria
        {
            criteria.Condition = LogicalOperation.And;
            return criteria;
        }

        /// <summary>
        /// Sets the <see cref="ContentCriteria.FolderRecursive"/> property to true.
        /// This informs the query to ignore folders and traverse the tree.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <typeparam name="T">The criteria type.</typeparam>
        /// <returns>The updated criteria.</returns>
        public static T Recursive<T>(this T criteria) where T : ContentCriteria
        {
            criteria.FolderRecursive = true;
            return criteria;
        }

        /// <summary>
        /// Sets the <see cref="PagingInfo.RecordsPerPage"/> property to <see cref="int.MaxValue"/>.
        /// This informs the query to return all results, no paging.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <typeparam name="T">The criteria type.</typeparam>
        /// <returns>The updated criteria.</returns>
        public static T MaxItems<T>(this T criteria) where T : ContentCriteria
        {
            criteria.PagingInfo = new PagingInfo(int.MaxValue);
            return criteria;
        }

        /// <summary>
        /// Sets the <see cref="ContentCriteria.ReturnMetadata"/> property to true.
        /// This informs the query to include metadata properties, which are initially excluded for performance reasons.
        /// </summary>
        /// <param name="critera">The criteria to extend.</param>
        /// <typeparam name="T">The criteria type.</typeparam>
        /// <returns>The updated criteria.</returns>
        public static T WithMetadata<T>(this T critera) where T : ContentCriteria
        {
            critera.ReturnMetadata = true;
            return critera;
        }

        /// <summary>
        /// Sets the <see cref="ContentCriteria.OrderByDirection"/> property to <see cref="EkEnumeration.OrderByDirection.Ascending"/> and 
        /// also sets the <see cref="ContentCriteria.OrderByField"/> properties.
        /// Ths informs the query how to sort results.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="field">The field to sort on.</param>
        /// <typeparam name="T">The criteria type.</typeparam>
        /// <returns>The updated criteria.</returns>
        public static T OrderBy<T>(this T criteria, ContentProperty field) where T : ContentCriteria
        {
            criteria.OrderByDirection = EkEnumeration.OrderByDirection.Ascending;
            criteria.OrderByField = field;
            return criteria;
        }

        /// <summary>
        /// Sets the <see cref="ContentCriteria.OrderByDirection"/> property to <see cref="EkEnumeration.OrderByDirection.Descending"/> and 
        /// also sets the <see cref="ContentCriteria.OrderByField"/> properties.
        /// Ths informs the query how to sort results.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="field">The field to sort on.</param>
        /// <typeparam name="T">The criteria type.</typeparam>
        /// <returns>The updated criteria.</returns>
        public static T OrderByDescending<T>(this T criteria, ContentProperty field) where T : ContentCriteria
        {
            criteria.OrderByDirection = EkEnumeration.OrderByDirection.Descending;
            criteria.OrderByField = field;
            return criteria;
        }

        /// <summary>
        /// Adds an exact match filter for <see cref="ContentProperty.XmlConfigurationId"/>.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="xmlConfigId">The Smart Form configuration id.</param>
        /// <returns>The updated criteria.</returns>
        public static ContentCriteria BySmartForm(this ContentCriteria criteria, long xmlConfigId)
        {
            criteria.FilteredBy(ContentProperty.XmlConfigurationId).EqualTo(xmlConfigId);
            return criteria;
        }

        /// <summary>
        /// Adds a filter option.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="field">The field to filter on.</param>
        /// <returns>An instance of <see cref="FilterContent"/>, which further refines the operator.</returns>
        public static FilterContent FilteredBy(this ContentCriteria criteria, ContentProperty field)
        {
            return new FilterContent(criteria, field);
        }
    }
}
