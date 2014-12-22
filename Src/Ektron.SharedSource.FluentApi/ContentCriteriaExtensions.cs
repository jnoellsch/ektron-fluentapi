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
        /// <returns>The updated criteria.</returns>
        public static ContentCriteria OrSeparated(this ContentCriteria criteria)
        {
            criteria.Condition = LogicalOperation.Or;
            return criteria;
        }

        /// <summary>
        /// Sets the <see cref="ContentCriteria.Condition"/> property to <see cref="LogicalOperation.And"/>. 
        /// This informs any filters how to be compared.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <returns>The updated criteria.</returns>
        public static ContentCriteria AndSeparated(this ContentCriteria criteria)
        {
            criteria.Condition = LogicalOperation.And;
            return criteria;
        }

        /// <summary>
        /// Sets the <see cref="ContentCriteria.FolderRecursive"/> property to true.
        /// This informs the query to ignore folders and traverse the tree.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <returns>The updated criteria.</returns>
        public static ContentCriteria Recursive(this ContentCriteria criteria)
        {
            criteria.FolderRecursive = true;
            return criteria;
        }

        /// <summary>
        /// Sets the <see cref="PagingInfo.RecordsPerPage"/> property to <see cref="int.MaxValue"/>.
        /// This informs the query to return all results, no paging.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <returns>The updated criteria.</returns>
        public static ContentCriteria MaxItems(this ContentCriteria criteria)
        {
            criteria.PagingInfo = new PagingInfo(int.MaxValue);
            return criteria;
        }

        /// <summary>
        /// Sets the <see cref="ContentCriteria.ReturnMetadata"/> property to true.
        /// This informs the query to include metadata properties, which are initially excluded for performance reasons.
        /// </summary>
        /// <param name="critera">The criteria to extend.</param>
        /// <returns>The updated criteria.</returns>
        public static ContentCriteria WithMetadata(this ContentCriteria critera)
        {
            critera.ReturnMetadata = true;
            return critera;
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
        /// Sets the <see cref="ContentCriteria.OrderByDirection"/> property to <see cref="EkEnumeration.OrderByDirection.Ascending"/> and 
        /// also sets the <see cref="ContentCriteria.OrderByField"/> properties.
        /// Ths informs the query how to sort results.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="field">The field to sort on.</param>
        /// <returns>The updated criteria.</returns>
        public static ContentCriteria OrderBy(this ContentCriteria criteria, ContentProperty field)
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
        /// <returns>The updated criteria.</returns>
        public static ContentCriteria OrderByDescending(this ContentCriteria criteria, ContentProperty field)
        {
            criteria.OrderByDirection = EkEnumeration.OrderByDirection.Descending;
            criteria.OrderByField = field;
            return criteria;
        }

        /// <summary>
        /// Adds a filter option.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="field">The field to sort on.</param>
        /// <returns>An instance of <see cref="FilterContent"/>, which further refines the operator.</returns>
        public static FilterContent FilteredBy(this ContentCriteria criteria, ContentProperty field)
        {
            return new FilterContent(criteria, field);
        }
    }
}
