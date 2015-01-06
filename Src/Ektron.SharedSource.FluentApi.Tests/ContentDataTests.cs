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
            public void ReadValueFromSmartFormXml()
            {
                var sut = new ContentData
                {
                    Html = @"<Sample>
                                <Value>123</Value>
                            </Sample>"
                };

                var result = sut.AsContentType<SmartFormResultClass>();

                Assert.AreEqual(result.Value, "123");
            }

            [Test]
            public void ReadValueFromContentData()
            {
                var sut = new ContentData()
                {
                    DateCreated = DateTime.Now
                };

                var result = sut.AsContentType<ContentDataResultClass>();

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

                var result = sut.AsContentType<MetadataResultClass>();

                Assert.AreEqual(result.Value, "123");
            }

            public class SmartFormResultClass
            {
                [SmartForm("/Sample/Value")]
                public string Value { get; set; }
            }

            public class ContentDataResultClass
            {
                [ContentData("DateCreated")]
                public DateTime CreatedDate { get; set; }
            }

            public class MetadataResultClass
            {
                [Metadata("Value")]
                public string Value { get; set; }
            }
        }
    }
}
