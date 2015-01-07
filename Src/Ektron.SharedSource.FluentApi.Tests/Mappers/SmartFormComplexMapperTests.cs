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
            public void ReadComplexType()
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
            public void ReadComplexTypeArray()
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
                [SmartFormComplex("/Sample/Item")]
                public ComplexChild Item { get; set; }
            }

            public class ComplexChild
            {
                [SmartFormPrimitive("./Value")]
                public int Value { get; set; }
            }

            public class ComplexEnumerableParent
            {
                [SmartFormComplex("/Sample/Item")]
                public IEnumerable<ComplexChild> Items { get; set; }
            }
        }
    }
}
