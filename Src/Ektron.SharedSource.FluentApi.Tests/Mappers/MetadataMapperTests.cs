using Ektron.Cms;
using Ektron.SharedSource.FluentApi.ModelAttributes;
using NUnit.Framework;

namespace Ektron.SharedSource.FluentApi.Tests.Mappers
{
    public class MetadataMapperTests
    {
        [TestFixture]
        public class Map
        {
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

            public class MetadataResult
            {
                [Metadata("Value")]
                public string Value { get; set; }
            }
        }
    }
}
