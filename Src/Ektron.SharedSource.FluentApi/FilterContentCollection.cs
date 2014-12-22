namespace Ektron.SharedSource.FluentApi
{
    using System;

    using Ektron.Cms.Common;
    using Ektron.Cms.Content;

    public class FilterContentCollection : Filter<ContentCollectionCriteria>
    {
        public FilterContentCollection(ContentCollectionCriteria criteria, ContentCollectionProperty field)
            : base(criteria)
        {
            this.Field = field;
        }

        public ContentCollectionProperty Field { get; private set; }

        protected override void AddFilter(CriteriaFilterOperator @operator, object value)
        {
            this.Criteria.AddFilter(this.Field, @operator, value);
        }
    }
}
