﻿namespace Ektron.SharedSource.FluentApi
{
    using Ektron.Cms.Common;
    using Ektron.Cms.Content;

    public static class ContentCollectionCriteriaExtensions
    {
        public static ContentCollectionCriteria ByCollection(this ContentCollectionCriteria criteria, long id)
        {
            criteria.AddFilter(id);
            return criteria;
        }

        public static ContentCollectionCriteria ByCollection(this ContentCollectionCriteria criteria, string name)
        {
            criteria.AddFilter(name);
            return criteria;
        }

        public static ContentCollectionCriteria Ordered(this ContentCollectionCriteria criteria)
        {
            criteria.OrderByCollectionOrder = true;
            return criteria;
        }

        public static FilterContentCollection FilteredBy(this ContentCollectionCriteria criteria, ContentCollectionProperty field) 
        {
            return new FilterContentCollection(criteria, field);
        }
    }
}
