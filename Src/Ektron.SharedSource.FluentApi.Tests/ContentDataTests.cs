using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.ModelAttributes;
using NUnit.Framework;

namespace Ektron.SharedSource.FluentApi.Tests
{
    public class ContentDataTests
    {
        [TestFixture]
        public class AsContentType
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

            [Test]
            public void ReadValueFromMetadata()
            {
                var sut = new ContentData()
                {
                    MetaData = new ContentMetaData[]
                    {
                        new ContentMetaData()
                        {
                            Name = "Value",
                            Text = "123"
                        }
                    }
                };

                var result = sut.AsContentType<MetadataResult>();

                Assert.AreEqual(result.Value, "123");
            }

            public class SmartFormResult
            {
                [SmartFormPrimitive("/Sample/Value")]
                public int Value { get; set; }
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

            public class SmartFormEnumerableResult
            {
                [SmartFormPrimitive("/Sample/Value")]
                public IEnumerable<int> Value { get; set; }
            }

            public class SmartFormComplexEnumerableResult
            {
                [SmartFormComplex("/Sample/Item")]
                public IEnumerable<SmartFormComplexInnerResult> Items { get; set; }
            }

            public class ContentDataResult
            {
                [ContentData("DateCreated")]
                public DateTime CreatedDate { get; set; }
            }

            public class MetadataResult
            {
                [Metadata("Value")]
                public string Value { get; set; }
            }
        }
    }
}
