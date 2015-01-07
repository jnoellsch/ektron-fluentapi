using System.Collections.Generic;
using System.Linq;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.ModelAttributes;
using NUnit.Framework;

namespace Ektron.SharedSource.FluentApi.Tests.Mappers
{
    public class SmartFormComplexMapperTests
    {
        [TestFixture]
        public class Map
        {
            [Test]
            public void ReadComplexFromSmartFormXml()
            {
                var sut = new ContentData
                {
                    Html = @"<Sample>
                                <Item>
                                    <Value>123</Value>
                                </Item>
                            </Sample>"
                };

                var result = sut.AsContentType<SmartFormComplexResult>();

                Assert.AreEqual(result.Item.Value, 123);
            }

            [Test]
            public void ReadComplexArrayFromSmartFormXml()
            {
                var sut = new ContentData
                {
                    Html = @"<Sample>
                                <Item>
                                    <Value>123</Value>
                                </Item>
                                <Item>
                                    <Value>234</Value>
                                </Item>
                                <Item>
                                    <Value>345</Value>
                                </Item>
                            </Sample>"
                };

                var result = sut.AsContentType<SmartFormComplexEnumerableResult>();

                Assert.AreEqual(result.Items.First().Value, 123);
                Assert.AreEqual(result.Items.Skip(1).First().Value, 234);
                Assert.AreEqual(result.Items.Skip(2).First().Value, 345);
            }

            public class SmartFormComplexResult
            {
                [SmartFormComplex("/Sample/Item")]
                public SmartFormComplexInnerResult Item { get; set; }
            }

            public class SmartFormComplexInnerResult
            {
                [SmartFormPrimitive("./Value")]
                public int Value { get; set; }
            }

            public class SmartFormComplexEnumerableResult
            {
                [SmartFormComplex("/Sample/Item")]
                public IEnumerable<SmartFormComplexInnerResult> Items { get; set; }
            }
        }
    }
}
