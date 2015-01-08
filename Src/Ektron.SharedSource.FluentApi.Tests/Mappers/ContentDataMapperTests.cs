using System;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.ModelAttributes;
using NUnit.Framework;

namespace Ektron.SharedSource.FluentApi.Tests.Mappers
{
    public class ContentDataMapperTests
    {
        [TestFixture]
        public class MapMethod
        {
            [Test]
            public void MapsDate()
            {
                var sut = new ContentData()
                {
                    DateCreated = DateTime.Now
                };

                var result = sut.AsContentType<DateResult>();

                Assert.AreEqual(result.CreatedDate, sut.DateCreated);
            }

            public class DateResult
            {
                [ContentData("DateCreated")]
                public DateTime CreatedDate { get; set; }
            }
        }
    }
}
