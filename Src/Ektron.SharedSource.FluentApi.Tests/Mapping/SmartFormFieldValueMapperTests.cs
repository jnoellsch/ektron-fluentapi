using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.Mapping;
using Ektron.SharedSource.FluentApi.Mapping.Attributes;
using NUnit.Framework;

namespace Ektron.SharedSource.FluentApi.Tests.Mapping
{
    public class SmartFormFieldValueMapperTests
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
                                <Value>123</Value>
                            </Sample>",
                    XmlConfiguration = new XmlConfigData()
                    {
                        Id = 1,
                    }
                };

                var result = sut.AsContentType<IntegerResult>();

                Assert.AreEqual(result.Value, 123);
            }

            [Test]
            public void MapsBaseAndDerivedClasses()
            {
                var sut = new ContentData
                {
                    Html = @"<Sample>
                                <Value>123</Value>
                                <Value2>234</Value2>
                            </Sample>",
                    XmlConfiguration = new XmlConfigData()
                    {
                        Id = 1,
                    }
                };

                var result = sut.AsContentType<DerivedIntegerResult>();

                Assert.AreEqual(result.Value, 123);
                Assert.AreEqual(result.Value2, 234);
            }

            public void MapsString()
            {
                var sut = new ContentData
                {
                    Html = @"<Sample>
                                <Value>123</Value>
                            </Sample>",
                    XmlConfiguration = new XmlConfigData()
                    {
                        Id = 1,
                    }
                };

                var result = sut.AsContentType<StringResult>();

                Assert.AreEqual(result.Value, "123");
            }

            [Test]
            public void MapsDateTime()
            {
                var now = DateTime.Parse(DateTime.Now.ToString());
                var html = @"<Sample>
                                <Value>{0}</Value>
                            </Sample>";

                var sut = new ContentData
                {
                    Html = string.Format(html, now),
                    XmlConfiguration = new XmlConfigData()
                    {
                        Id = 1,
                    }
                };

                var result = sut.AsContentType<DateTimeResult>();

                Assert.AreEqual(result.Value, now);
            }

            [Test]
            public void MapsPrimitiveArray()
            {
                var sut = new ContentData
                {
                    Html = @"<Sample>
                                <Value>123</Value>
                                <Value>234</Value>
                                <Value>345</Value>
                            </Sample>",
                    XmlConfiguration = new XmlConfigData()
                    {
                        Id = 1,
                    }
                };

                var result = sut.AsContentType<EnumerableResult>();

                Assert.AreEqual(result.Value.First(), 123);
                Assert.AreEqual(result.Value.Skip(1).First(), 234);
                Assert.AreEqual(result.Value.Skip(2).First(), 345);
            }

            [Test]
            public void MapsEnum()
            {
                var sut = new ContentData
                {
                    Html = @"<Sample>
                                <Value>1</Value>
                            </Sample>",
                    XmlConfiguration = new XmlConfigData()
                    {
                        Id = 1,
                    }
                };

                var result = sut.AsContentType<EnumResult>();

                Assert.AreEqual(result.Value, EnumOptions.Second);
            }

            [Test]
            public void MapsEnumArray()
            {
                var sut = new ContentData
                {
                    Html = @"<Sample>
                                <Value>0</Value>
                                <Value>1</Value>
                            </Sample>",
                    XmlConfiguration = new XmlConfigData()
                    {
                        Id = 1,
                    }
                };

                var result = sut.AsContentType<EnumArrayResult>();

                Assert.AreEqual(result.Values.First(), EnumOptions.First);
                Assert.AreEqual(result.Values.Skip(1).First(), EnumOptions.Second);
            }

            [Test]
            public void LoadTest()
            {
                Mapper.RegisterType<EnumerableResult>();
                var sut = new ContentData
                {
                    Html = @"<Sample>
                                <Value>123</Value>
                                <Value>234</Value>
                                <Value>345</Value>
                            </Sample>",
                    XmlConfiguration = new XmlConfigData()
                    {
                        Id = 1,
                    }
                };

                var start = DateTime.Now;
                for (var i = 0; i < 30000; i++)
                {
                    var result = sut.AsContentType<EnumerableResult>();
                }
                var end = DateTime.Now;

                Console.WriteLine(end - start);
            }

            public class IntegerResult
            {
                [SmartFormFieldValue("./Value")]
                public int Value { get; set; }
            }

            public class DerivedIntegerResult : IntegerResult
            {
                [SmartFormFieldValue("./Value2")]
                public int Value2 { get; set; }
            }

            public class StringResult
            {
                [SmartFormFieldValue("./Value")]
                public string Value { get; set; }
            }

            public class DateTimeResult
            {
                [SmartFormFieldValue("./Value")]
                public DateTime Value { get; set; }
            }

            public class EnumerableResult
            {
                [SmartFormFieldValue("./Value")]
                public IEnumerable<int> Value { get; set; }
            }

            public class EnumResult
            {
                [SmartFormFieldValue("./Value")]
                public EnumOptions Value { get; set; }
            }

            public class EnumArrayResult
            {
                [SmartFormFieldValue("./Value")]
                public IEnumerable<EnumOptions> Values { get; set; }
            }

            public enum EnumOptions
            {
                First = 0,
                Second = 1
            }
        }
    }
}
