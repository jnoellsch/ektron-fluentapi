namespace Ektron.SharedSource.FluentApi
{
    using System;

    using Ektron.Cms.Common;
    using Ektron.Cms.Content;

    public class FilterContentCollection : Filter<ContentCollectionCriteria>
    {
        private readonly ContentCollectionProperty _field;

        public FilterContentCollection(ContentCollectionCriteria criteria, ContentCollectionProperty field)
            : base(criteria)
        {
            this._field = field;
        }

        protected override void AddFilter(CriteriaFilterOperator @operator, object value)
        {
            this.Criteria.AddFilter(this._field, @operator, value);
        }
    }
}
