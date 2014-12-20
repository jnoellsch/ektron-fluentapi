namespace Ektron.SharedSource.FluentApi
{
    using Ektron.Cms.Content;

    public static class ContentCollectionCriteriaExtensions
    {
        public static ContentCollectionCriteria ByCollection(this ContentCollectionCriteria criteria, long? collectionId)
        {
            if (collectionId.HasValue)
            {
                criteria.AddFilter(collectionId.Value);
            }

            return criteria;
        }

        public static ContentCollectionCriteria Ordered(this ContentCollectionCriteria criteria)
        {
            criteria.OrderByCollectionOrder = true;
            return criteria;
        }
    }
}
