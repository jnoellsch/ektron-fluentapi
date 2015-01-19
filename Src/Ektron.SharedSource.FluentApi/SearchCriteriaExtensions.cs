namespace Ektron.SharedSource.FluentApi
{
    using Ektron.Cms.Framework.Organization;
    using Ektron.Cms.Search;
    using Ektron.Cms.Search.Expressions;
    using Ektron.Cms.Search.Solr;

    public static class SearchCriteriaExtensions
    {
        /// <summary>
        /// ANDs a set of expressions to a <see cref="SearchCriteria.ExpressionTree" />.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="expressions">The set of expressions to be individually ANDed to the criteria.</param>
        /// <returns>The updated criteria.</returns>
        public static T And<T>(this T criteria, params Expression[] expressions)
            where T : SearchCriteria
        {
            criteria.ExpressionTree = ExpressionExtensions.And(criteria.ExpressionTree, expressions);
            return criteria;
        }

        /// <summary>
        /// Restricts search results to a set of folders (recursive). 
        /// ORs the set of folders together, then ANDs the result expression to the <see cref="SearchCriteria">search criteria</see> <see cref="SearchCriteria.ExpressionTree">expression tree</see>.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="folderManager">A <see cref="FolderManager"/> instance from which to query the folders.</param>
        /// <param name="folderIds">The set of folder IDs to restrict results to (recursive).</param>
        /// <returns>The updated criteria.</returns>
        public static T AndFolders<T>(this T criteria, FolderManager folderManager, params long[] folderIds)
            where T : SearchCriteria
        {
            return criteria.And(ExpressionFactory.Create(folderManager, folderIds));
        }

        /// <summary>
        /// Restricts search results to a set of SmartForms.
        /// ORs a set of SmartForms together, then ANDs the result expression to a <see cref="SearchCriteria.ExpressionTree" />.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="smartFormIds">The set of SmartForm IDs to restrict results to.</param>
        /// <returns>The updated criteria.</returns>
        public static T AndSmartForms<T>(this T criteria, params long[] smartFormIds)
            where T : SearchCriteria
        {
            return criteria.And(ExpressionFactory.CreateSmartFormExpression(smartFormIds));
        }

        /// <summary>
        /// Restricts search results to a set of taxonomies.
        /// ORs a set of taxonomies together, then ANDs the result expression to the <see cref="SearchCriteria">search criteria</see> <see cref="SearchCriteria.ExpressionTree">expression tree</see>.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="taxonomyManager">A <see cref="TaxonomyManager"/> instance from qhich to query the taxonomies.</param>
        /// <param name="taxonomyIds">The set of taxonomy IDs to restrict results to.</param>
        /// <returns>The updated criteria.</returns>
        public static T AndTaxonomy<T>(this T criteria, TaxonomyManager taxonomyManager, params long[] taxonomyIds)
            where T : SearchCriteria
        {
            return criteria.And(ExpressionFactory.Create(taxonomyManager, taxonomyIds));
        }

        /// <summary>
        /// Sets the <see cref="SearchCriteria"/>'s <see cref="RefinementCriteria.IsEnabled"> property to true.
        /// Enables refinement functionality.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <returns>The updated criteria.</returns>
        public static T EnableRefinement<T>(this T criteria)
            where T : SearchCriteria
        {
            criteria.Refinement.IsEnabled = true;
            return criteria;
        }

        /// <summary>
        /// ORs a set of expressions to a <see cref="SearchCriteria">search criteria</see> <see cref="SearchCriteria.ExpressionTree">expression tree</see>.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="expressions">The set of expressions to be individually ORed to the criteria.</param>
        /// <returns>The updated criteria.</returns>
        public static T Or<T>(this T criteria, params Expression[] expressions)
            where T : SearchCriteria
        {
            criteria.ExpressionTree = ExpressionExtensions.Or(criteria.ExpressionTree, expressions);
            return criteria;
        }

        /// <summary>
        /// Adds a set of <see cref="Ektron.Cms.Search.RefinementSpecification">refinements</see> to the search criteria.
        /// For multivalue properties, use <see cref="RefineMultiValueBy"/> instead.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="propertyExpressions">The set of property expressions to be added to as refinements.</param>
        /// <returns>The updated criteria.</returns>
        public static T RefineBy<T>(this T criteria, params PropertyExpression[] propertyExpressions)
            where T : SearchCriteria
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

        /// <summary>
        /// Adds a set of <see cref="Ektron.Cms.Search.RefinementSpecification">refinements</see> to the search criteria.
        /// Used for building facets from multivalue fields (i.e. taxonomy).
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="propertyExpressions"></param>
        /// <returns>The updated criteria.</returns>
        public static T RefineMultiValueBy<T>(this T criteria, params StringPropertyExpression[] propertyExpressions)
            where T : SearchCriteria
        {
            foreach (var propertyExpression in propertyExpressions)
            {
                var multiValuePropertyExpression = SearchSolrProperty.CreateExactStringProperty(propertyExpression);
                criteria.EnableRefinement()
                    .Refinement.Add(new StringRefinementSpecification(multiValuePropertyExpression));
            }

            return criteria;
        }

        /// <summary>
        /// Sets the return properties of a search criteria.
        /// </summary>
        /// <param name="criteria">The criteria to extend.</param>
        /// <param name="propertyExpressions">The search properties to return in each search result.</param>
        /// <returns>The updated criteria.</returns>
        public static T ReturnsWith<T>(this T criteria, params PropertyExpression[] propertyExpressions)
            where T : SearchCriteria
        {
            foreach (var propertyExpression in propertyExpressions)
            {
                criteria.ReturnProperties.Add(propertyExpression);
            }

            return criteria;
        }
    }
}