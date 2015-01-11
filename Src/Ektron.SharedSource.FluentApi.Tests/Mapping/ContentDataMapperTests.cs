using System;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.Mapping;
using Ektron.SharedSource.FluentApi.Mapping.Attributes;
using NUnit.Framework;

namespace Ektron.SharedSource.FluentApi.Tests.Mapping
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

            [Test]
            public void LoadTest()
            {
                MappingRegistry.RegisterType<DateResult>();

                var start = DateTime.Now;
                for (var i = 0; i < 30000; i++)
                {
                    var sut = new ContentData()
                    {
                        DateCreated = DateTime.Now
                    };

                    var result = sut.AsContentType<DateResult>();
                }
                var end = DateTime.Now;
                
                Console.WriteLine(end - start);
            }

            public class DateResult
            {
                [ContentData("DateCreated")]
                public DateTime CreatedDate { get; set; }
            }
        }
    }
}
