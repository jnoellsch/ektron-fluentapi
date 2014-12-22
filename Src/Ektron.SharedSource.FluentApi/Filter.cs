namespace Ektron.SharedSource.FluentApi
{
    using System;

    using Ektron.Cms.Common;

    /// <summary>
    /// Represents filter operators.
    /// </summary>
    /// <typeparam name="T">The criteria class.</typeparam>
    public abstract class Filter<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Filter{T}" /> class. Used for unit testing only.
        /// </summary>
        internal Filter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Filter{T}"/> class.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        protected Filter(T criteria)
        {
            if (criteria == null) throw new ArgumentNullException("criteria");

            this.Criteria = criteria;
        }

        /// <summary>
        /// Gets the criteria object.
        /// </summary>
        public T Criteria { get; private set; }

        /// <summary>
        /// Applies the <see cref="CriteriaFilterOperator.Contains"/> operator to the filter.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <returns>The modified criteria.</returns>
        public T Contains(object value)
        {
            this.AddFilter(CriteriaFilterOperator.Contains, value);
            return this.Criteria;
        }

        /// <summary>
        /// Applies the <see cref="CriteriaFilterOperator.DoesNotContain"/> operator to the filter.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <returns>The modified criteria.</returns>
        public T DoesNotContain(object value)
        {
            this.AddFilter(CriteriaFilterOperator.DoesNotContain, value);
            return this.Criteria;
        }

        /// <summary>
        /// Applies the <see cref="CriteriaFilterOperator.EndsWith"/> operator to the filter.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <returns>The modified criteria.</returns>
        public T EndsWith(object value)
        {
            this.AddFilter(CriteriaFilterOperator.EndsWith, value);
            return this.Criteria;
        }

        /// <summary>
        /// Applies the <see cref="CriteriaFilterOperator.EqualTo"/> operator to the filter.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <returns>The modified criteria.</returns>
        public T EqualTo(object value)
        {
            this.AddFilter(CriteriaFilterOperator.EqualTo, value);
            return this.Criteria;
        }

        /// <summary>
        /// Applies the <see cref="CriteriaFilterOperator.GreaterThan"/> operator to the filter.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <returns>The modified criteria.</returns>
        public T GreaterThan(object value)
        {
            this.AddFilter(CriteriaFilterOperator.GreaterThan, value);
            return this.Criteria;
        }

        /// <summary>
        /// Applies the <see cref="CriteriaFilterOperator.GreaterThanOrEqualTo"/> operator to the filter.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <returns>The modified criteria.</returns>
        public T GreaterThanOrEqualTo(object value)
        {
            this.AddFilter(CriteriaFilterOperator.GreaterThanOrEqualTo, value);
            return this.Criteria;
        }

        /// <summary>
        /// Applies the <see cref="CriteriaFilterOperator.In"/> operator to the filter.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <returns>The modified criteria.</returns>
        public T In(object value)
        {
            this.AddFilter(CriteriaFilterOperator.In, value);
            return this.Criteria;
        }

        /// <summary>
        /// Applies the <see cref="CriteriaFilterOperator.InSubClause"/> operator to the filter.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <returns>The modified criteria.</returns>
        public T InSubClause(object value)
        {
            this.AddFilter(CriteriaFilterOperator.InSubClause, value);
            return this.Criteria;
        }

        /// <summary>
        /// Applies the <see cref="CriteriaFilterOperator.IsNull"/> operator to the filter.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <returns>The modified criteria.</returns>
        public T IsNull(object value)
        {
            this.AddFilter(CriteriaFilterOperator.IsNull, value);
            return this.Criteria;
        }

        /// <summary>
        /// Applies the <see cref="CriteriaFilterOperator.LessThan"/> operator to the filter.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <returns>The modified criteria.</returns>
        public T LessThan(object value)
        {
            this.AddFilter(CriteriaFilterOperator.LessThan, value);
            return this.Criteria;
        }

        /// <summary>
        /// Applies the <see cref="CriteriaFilterOperator.LessThanOrEqualTo"/> operator to the filter.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <returns>The modified criteria.</returns>
        public T LessThanOrEqualTo(object value)
        {
            this.AddFilter(CriteriaFilterOperator.LessThanOrEqualTo, value);
            return this.Criteria;
        }

        /// <summary>
        /// Applies the <see cref="CriteriaFilterOperator.NotEqualTo"/> operator to the filter.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <returns>The modified criteria.</returns>
        public T NotEqualTo(object value)
        {
            this.AddFilter(CriteriaFilterOperator.NotEqualTo, value);
            return this.Criteria;
        }

        /// <summary>
        /// Applies the <see cref="CriteriaFilterOperator.NotIn"/> operator to the filter.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <returns>The modified criteria.</returns>
        public T NotIn(object value)
        {
            this.AddFilter(CriteriaFilterOperator.NotIn, value);
            return this.Criteria;
        }

        /// <summary>
        /// Applies the <see cref="CriteriaFilterOperator.StartsWith"/> operator to the filter.
        /// </summary>
        /// <param name="value">The value to filter on.</param>
        /// <returns>The modified criteria.</returns>
        public T StartsWith(object value)
        {
            this.AddFilter(CriteriaFilterOperator.StartsWith, value);
            return this.Criteria;
        }

        /// <summary>
        /// Adds the filter definition to the criteria.
        /// </summary>
        /// <param name="operator">The comparison operator.</param>
        /// <param name="value">The value to filter on.</param>
        protected abstract void AddFilter(CriteriaFilterOperator @operator, object value);
    }
}
