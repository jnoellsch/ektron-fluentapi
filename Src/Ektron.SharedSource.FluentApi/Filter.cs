namespace Ektron.SharedSource.FluentApi
{
    using System;

    using Ektron.Cms.Common;

    public abstract class Filter<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Filter{T}" /> class. Used for unit testing only.
        /// </summary>
        internal Filter()
        {
        }

        protected Filter(T criteria)
        {
            if (criteria == null) throw new ArgumentNullException("criteria");

            this.Criteria = criteria;
        }

        protected T Criteria { get; private set; }

        public T Contains(object value)
        {
            this.AddFilter(CriteriaFilterOperator.Contains, value);
            return this.Criteria;
        }

        public T DoesNotContain(object value)
        {
            this.AddFilter(CriteriaFilterOperator.DoesNotContain, value);
            return this.Criteria;
        }

        public T EndsWith(object value)
        {
            this.AddFilter(CriteriaFilterOperator.EndsWith, value);
            return this.Criteria;
        }

        public T EqualTo(object value)
        {
            this.AddFilter(CriteriaFilterOperator.EqualTo, value);
            return this.Criteria;
        }

        public T GreaterThan(object value)
        {
            this.AddFilter(CriteriaFilterOperator.GreaterThan, value);
            return this.Criteria;
        }

        public T GreaterThanOrEqualTo(object value)
        {
            this.AddFilter(CriteriaFilterOperator.GreaterThanOrEqualTo, value);
            return this.Criteria;
        }

        public T In(object value)
        {
            this.AddFilter(CriteriaFilterOperator.In, value);
            return this.Criteria;
        }

        public T InSubClause(object value)
        {
            this.AddFilter(CriteriaFilterOperator.InSubClause, value);
            return this.Criteria;
        }

        public T IsNull(object value)
        {
            this.AddFilter(CriteriaFilterOperator.IsNull, value);
            return this.Criteria;
        }

        public T LessThan(object value)
        {
            this.AddFilter(CriteriaFilterOperator.LessThan, value);
            return this.Criteria;
        }

        public T LessThanOrEqualTo(object value)
        {
            this.AddFilter(CriteriaFilterOperator.LessThanOrEqualTo, value);
            return this.Criteria;
        }

        public T NotEqualTo(object value)
        {
            this.AddFilter(CriteriaFilterOperator.NotEqualTo, value);
            return this.Criteria;
        }

        public T NotIn(object value)
        {
            this.AddFilter(CriteriaFilterOperator.NotIn, value);
            return this.Criteria;
        }

        public T StartsWith(object value)
        {
            this.AddFilter(CriteriaFilterOperator.StartsWith, value);
            return this.Criteria;
        }

        protected abstract void AddFilter(CriteriaFilterOperator @operator, object value);
    }
}
