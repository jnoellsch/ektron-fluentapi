using System;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.ModelAttributes;
using NUnit.Framework;

namespace Ektron.SharedSource.FluentApi.Tests.Mappers
{
    public class ContentDataMapperTests
    {
        [TestFixture]
        public class Map
        {
            [Test]
            public void ReadValueFromContentData()
            {
                var sut = new ContentData()
                {
                    DateCreated = DateTime.Now
                };

                var result = sut.AsContentType<ContentDataResult>();

                Assert.AreEqual(result.CreatedDate, sut.DateCreated);
            }

            public class ContentDataResult
            {
                [ContentData("DateCreated")]
                public DateTime CreatedDate { get; set; }
            }
        }
    }
}
