using System;
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
