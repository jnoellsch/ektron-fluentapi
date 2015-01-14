namespace Ektron.SharedSource.FluentApi
{
    using Ektron.Cms;
    using Ektron.Cms.Common;
    using Ektron.Cms.Framework.Organization;
    using Ektron.Cms.Organization;
    using Ektron.Cms.Search;
    using Ektron.Cms.Search.Expressions;
    using Ektron.Cms.Search.Solr;

    public static class ExpressionFactory
    {
        public static Expression Create(FolderManager folderManager, params long[] folderIds)
        {
            Expression expression = null;

            // Get folders with a single call to Ektron:
            var folderCriteria = new FolderCriteria();
            foreach (long folderId in folderIds)
            {
                folderCriteria.AddFilter(FolderProperty.Id, CriteriaFilterOperator.EqualTo, folderId);
            }
            var folderDataList = folderManager.GetList(folderCriteria);

            // Build folder expression for search criteria:
            foreach (var folderData in folderDataList)
            {
                var rightExpression = SearchContentProperty.FolderIdPath.Contains(folderData.FolderIdWithPath);
                expression = expression == null ? rightExpression : expression.Or(rightExpression);
            }

            return expression;
        }

        public static Expression Create(TaxonomyManager taxonomyManager, params long[] taxonomyIds)
        {
            Expression expression = null;

            // Get taxonomy with a single call to Ektron:
            var taxonomyCriteria = new TaxonomyCriteria();
            foreach (long taxonomyId in taxonomyIds)
            {
                taxonomyCriteria.AddFilter(TaxonomyProperty.Id, CriteriaFilterOperator.EqualTo, taxonomyId);
            }
            var taxonomyDataList = taxonomyManager.GetList(taxonomyCriteria);

            // Build taxonomy expression for search criteria:
            foreach (var taxonomyData in taxonomyDataList)
            {
                var rightExpression = SearchSolrProperty
                    .CreateExactStringProperty(SearchContentProperty.TaxonomyCategory)
                    .Contains(taxonomyData.Path); // "Contains" really means "StartsWith"

                expression = expression == null ? rightExpression : expression.Or(rightExpression);
            }

            return expression;
        }

        public static Expression CreateSmartFormExpression(params long[] smartFormIds)
        {
            Expression expression = null;

            foreach (long smartFormId in smartFormIds)
            {
                var rightExpression = SearchContentProperty.XmlConfigId.EqualTo(smartFormId);

                expression = expression == null
                    ? rightExpression
                    : expression.Or(rightExpression);
            }

            return expression;
        }
    }
}