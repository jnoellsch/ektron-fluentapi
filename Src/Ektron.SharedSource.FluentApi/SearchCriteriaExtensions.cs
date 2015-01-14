namespace Ektron.SharedSource.FluentApi
{
    using Ektron.Cms.Framework.Organization;
    using Ektron.Cms.Search;
    using Ektron.Cms.Search.Expressions;
    using Ektron.Cms.Search.Solr;

    public static class SearchCriteriaExtensions
    {
        public static SearchCriteria And(this SearchCriteria criteria, params Expression[] expressions)
        {
            criteria.ExpressionTree = ExpressionExtensions.And(criteria.ExpressionTree, expressions);
            return criteria;
        }

        public static SearchCriteria AndFolders(
            this SearchCriteria criteria,
            FolderManager folderManager,
            params long[] folderIds)
        {
            return criteria.And(ExpressionFactory.Create(folderManager, folderIds));
        }

        public static SearchCriteria AndSmartForms(this SearchCriteria criteria, params long[] smartFormIds)
        {
            return criteria.And(ExpressionFactory.CreateSmartFormExpression(smartFormIds));
        }

        public static SearchCriteria AndTaxonomy(
            this SearchCriteria criteria,
            TaxonomyManager taxonomyManager,
            params long[] taxonomyIds)
        {
            return criteria.And(ExpressionFactory.Create(taxonomyManager, taxonomyIds));
        }

        /// <summary>
        /// Sets the <see cref="SearchCriteria"/>'s <see cref="RefinementCriteria.IsEnabled"/> property to true.
        /// Enables refinement functionality.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <returns>The updated criteria.</returns>
        public static SearchCriteria EnableRefinement(this SearchCriteria criteria)
        {
            criteria.Refinement.IsEnabled = true;
            return criteria;
        }

        public static SearchCriteria Or(this SearchCriteria criteria, params Expression[] expressions)
        {
            criteria.ExpressionTree = ExpressionExtensions.Or(criteria.ExpressionTree, expressions);
            return criteria;
        }

        public static SearchCriteria RefineBy(this SearchCriteria criteria, params PropertyExpression[] propertyExpressions)
        {
            criteria.EnableRefinement();

            foreach (var propertyExpression in propertyExpressions)
            {
                if (propertyExpression is DatePropertyExpression)
                {
                    criteria.Refinement.Add(
                        new DateRefinementSpecification((DatePropertyExpression)propertyExpression));
                }
                else if (propertyExpression is DecimalPropertyExpression)
                {
                    criteria.Refinement.Add(
                        new DecimalRefinementSpecification((DecimalPropertyExpression)propertyExpression));
                }
                else if (propertyExpression is IntegerPropertyExpression)
                {
                    criteria.Refinement.Add(
                        new IntegerRefinementSpecification((IntegerPropertyExpression)propertyExpression));
                }
                else if (propertyExpression is StringPropertyExpression)
                {
                    criteria.Refinement.Add(
                        new StringRefinementSpecification((StringPropertyExpression)propertyExpression));
                }
            }

            return criteria;
        }

        public static SearchCriteria RefineMultiValueBy(
            this SearchCriteria criteria,
            params StringPropertyExpression[] propertyExpressions)
        {
            foreach (var propertyExpression in propertyExpressions)
            {
                var multiValuePropertyExpression = SearchSolrProperty.CreateExactStringProperty(propertyExpression);
                criteria.EnableRefinement()
                    .Refinement.Add(new StringRefinementSpecification(multiValuePropertyExpression));
            }

            return criteria;
        }

        public static SearchCriteria ReturnsWith(
            this SearchCriteria criteria,
            params PropertyExpression[] propertyExpressions)
        {
            foreach (var propertyExpression in propertyExpressions)
            {
                criteria.ReturnProperties.Add(propertyExpression);
            }

            return criteria;
        }
    }
}