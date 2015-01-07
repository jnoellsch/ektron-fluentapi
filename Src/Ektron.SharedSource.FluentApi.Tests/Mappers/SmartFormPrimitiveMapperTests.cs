using System.Collections.Generic;
using System.Linq;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.ModelAttributes;
using NUnit.Framework;

namespace Ektron.SharedSource.FluentApi.Tests.Mappers
{
    public class SmartFormPrimitiveMapperTests
    {
        [TestFixture]
        public class Map
        {
            [Test]
            public void ReadPrimitiveFromSmartFormXml()
            {
                var sut = new ContentData
                {
                    Html = @"<Sample>
                                <Value>123</Value>
                            </Sample>"
                };

                var result = sut.AsContentType<SmartFormResult>();

                Assert.AreEqual(result.Value, 123);
            }

            [Test]
            public void ReadPrimitiveArrayFromSmartFormXml()
            {
                var sut = new ContentData
                {
                    Html = @"<Sample>
                                <Value>123</Value>
                                <Value>234</Value>
                                <Value>345</Value>
                            </Sample>"
                };

                var result = sut.AsContentType<SmartFormEnumerableResult>();

                Assert.AreEqual(result.Value.First(), 123);
                Assert.AreEqual(result.Value.Skip(1).First(), 234);
                Assert.AreEqual(result.Value.Skip(2).First(), 345);
            }

            public class SmartFormResult
            {
                [SmartFormPrimitive("/Sample/Value")]
                public int Value { get; set; }
            }

            public class SmartFormEnumerableResult
            {
                [SmartFormPrimitive("/Sample/Value")]
                public IEnumerable<int> Value { get; set; }
            }
        }
    }
}
