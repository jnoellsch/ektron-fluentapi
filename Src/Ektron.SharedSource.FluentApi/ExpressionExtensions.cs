namespace Ektron.SharedSource.FluentApi
{
    using Ektron.Cms.Search.Expressions;

    public static class ExpressionExtensions
    {
        public static Expression And(this Expression leftExpression, Expression[] rightExpressions)
        {
            foreach (var rightExpression in rightExpressions)
            {
                leftExpression = leftExpression == null ? rightExpression : leftExpression.And(rightExpression);
            }

            return leftExpression;
        }

        public static Expression Or(this Expression leftExpression, params Expression[] rightExpressions)
        {
            foreach (var rightExpression in rightExpressions)
            {
                leftExpression = leftExpression == null ? rightExpression : leftExpression.Or(rightExpression);
            }

            return leftExpression;
        }
    }
}