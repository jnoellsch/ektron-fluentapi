﻿using System;
using System.Collections.Generic;
using System.Linq;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.ModelAttributes;
using NUnit.Framework;

namespace Ektron.SharedSource.FluentApi.Tests.Mappers
{
    public class SmartFormPrimitiveMapperTests
    {
        [TestFixture]
        public class Map
        {
            [Test]
            public void ReadInteger()
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

            public void ReadString()
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
            public void DateTimeString()
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
            public void ReadPrimitiveArray()
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

            public class IntegerResult
            {
                [SmartFormPrimitive("/Sample/Value")]
                public int Value { get; set; }
            }

            public class StringResult
            {
                [SmartFormPrimitive("/Sample/Value")]
                public string Value { get; set; }
            }

            public class DateTimeResult
            {
                [SmartFormPrimitive("/Sample/Value")]
                public DateTime Value { get; set; }
            }

            public class EnumerableResult
            {
                [SmartFormPrimitive("/Sample/Value")]
                public IEnumerable<int> Value { get; set; }
            }
        }
    }
}
