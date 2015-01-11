using System.Collections.Generic;
using System.Linq;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.Mapping.Attributes;
using NUnit.Framework;

namespace Ektron.SharedSource.FluentApi.Tests.Mapping
{
    public class SmartFormComplexMapperTests
    {
        [TestFixture]
        public class MapMethod
        {
            [Test]
            public void MapsComplexType()
            {
                var sut = new ContentData
                {
                    Html = @"<Sample>
                                <Item>
                                    <Value>123</Value>
                                </Item>
                            </Sample>"
                };

                var result = sut.AsContentType<ComplexParent>();

                Assert.AreEqual(result.Item.Value, 123);
            }

            [Test]
            public void MapsComplexTypeArray()
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

                var result = sut.AsContentType<ComplexEnumerableParent>();

                Assert.AreEqual(result.Items.First().Value, 123);
                Assert.AreEqual(result.Items.Skip(1).First().Value, 234);
                Assert.AreEqual(result.Items.Skip(2).First().Value, 345);
            }

            public class ComplexParent
            {
                [SmartFormComplex("./Item")]
                public ComplexChild Item { get; set; }
            }

            public class ComplexChild
            {
                [SmartFormPrimitive("./Value")]
                public int Value { get; set; }
            }

            public class ComplexEnumerableParent
            {
                [SmartFormComplex("./Item")]
                public IEnumerable<ComplexChild> Items { get; set; }
            }
        }
    }
}
