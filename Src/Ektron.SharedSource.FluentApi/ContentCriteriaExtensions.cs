namespace Ektron.SharedSource.FluentApi
{
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
    }
}
