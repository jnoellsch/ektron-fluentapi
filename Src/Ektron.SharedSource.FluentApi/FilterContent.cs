namespace Ektron.SharedSource.FluentApi
{
    using Ektron.Cms.Common;
    using Ektron.Cms.Content;

    public class FilterContent : Filter<ContentCriteria>
    {
        public FilterContent(ContentCriteria criteria, ContentProperty field) : base(criteria)
        {
            this.Field = field;
        }

        public ContentProperty Field { get; private set; }

        protected override void AddFilter(CriteriaFilterOperator @operator, object value)
        {
            this.Criteria.AddFilter(this.Field, @operator, value);
        }
    }
}