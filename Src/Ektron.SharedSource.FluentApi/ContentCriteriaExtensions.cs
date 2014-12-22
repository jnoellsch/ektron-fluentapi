namespace Ektron.SharedSource.FluentApi
{
    using Ektron.Cms;
    using Ektron.Cms.Common;
    using Ektron.Cms.Content;

    public static class ContentCriteriaExtensions
    {
        public static ContentCriteria OrSeparated(this ContentCriteria criteria)
        {
            criteria.Condition = LogicalOperation.Or;
            return criteria;
        }

        public static ContentCriteria AndSeparated(this ContentCriteria criteria)
        {
            criteria.Condition = LogicalOperation.And;
            return criteria;
        }

        public static ContentCriteria Recursive(this ContentCriteria criteria)
        {
            criteria.FolderRecursive = true;
            return criteria;
        }

        public static ContentCriteria MaxItems(this ContentCriteria criteria)
        {
            criteria.PagingInfo = new PagingInfo(int.MaxValue);
            return criteria;
        }

        public static ContentCriteria WithMetadata(this ContentCriteria critera)
        {
            critera.ReturnMetadata = true;
            return critera;
        }

        public static FilterContent FilteredBy(this ContentCriteria criteria, ContentProperty field)
        {
            return new FilterContent(criteria, field);
        }
    }
}
