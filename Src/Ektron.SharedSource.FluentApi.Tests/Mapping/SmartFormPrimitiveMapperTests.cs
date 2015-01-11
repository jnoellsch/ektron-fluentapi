using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.Mapping.Attributes;
using NUnit.Framework;

namespace Ektron.SharedSource.FluentApi.Tests.Mapping
{
    public class SmartFormPrimitiveMapperTests
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
                            </Sample>"
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
                            </Sample>"
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
                            </Sample>"
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
                    Html = string.Format(html, now)
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
                            </Sample>"
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
                            </Sample>"
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
                            </Sample>"
                };

                var result = sut.AsContentType<EnumArrayResult>();

                Assert.AreEqual(result.Values.First(), EnumOptions.First);
                Assert.AreEqual(result.Values.Skip(1).First(), EnumOptions.Second);
            }

            public class IntegerResult
            {
                [SmartFormPrimitive("./Value")]
                public int Value { get; set; }
            }

            public class DerivedIntegerResult : IntegerResult
            {
                [SmartFormPrimitive("./Value2")]
                public int Value2 { get; set; }
            }

            public class StringResult
            {
                [SmartFormPrimitive("./Value")]
                public string Value { get; set; }
            }

            public class DateTimeResult
            {
                [SmartFormPrimitive("./Value")]
                public DateTime Value { get; set; }
            }

            public class EnumerableResult
            {
                [SmartFormPrimitive("./Value")]
                public IEnumerable<int> Value { get; set; }
            }

            public class EnumResult
            {
                [SmartFormPrimitive("./Value")]
                public EnumOptions Value { get; set; }
            }

            public class EnumArrayResult
            {
                [SmartFormPrimitive("./Value")]
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
