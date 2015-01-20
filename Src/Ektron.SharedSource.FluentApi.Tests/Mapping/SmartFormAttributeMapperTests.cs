using Ektron.Cms;
using Ektron.SharedSource.FluentApi.Mapping.Attributes;
using NUnit.Framework;

namespace Ektron.SharedSource.FluentApi.Tests.Mapping
{
    public class SmartFormAttributeMapperTests
    {
        [TestFixture]
        public class MapMethod
        {
            [Test]
            public void MapsInteger()
            {
                var sut = new ContentData
                {
                    Html = @"<Sample>
                                <Value id=""123"">text</Value>
                            </Sample>",
                    XmlConfiguration = new XmlConfigData()
                    {
                        Id = 1,
                    }
                };

                var result = sut.AsContentType<IntegerResult>();

                Assert.AreEqual(result.Value, 123);
            }

            public class IntegerResult
            {
                [SmartFormAttribute("./Value/@id")]
                public int Value { get; set; }
            }
        }
    }
}
