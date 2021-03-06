﻿using System.Collections.Generic;
using System.Linq;
using Ektron.Cms;
using Ektron.SharedSource.FluentApi.Mapping.Attributes;
using NUnit.Framework;

namespace Ektron.SharedSource.FluentApi.Tests.Mapping
{
    public class MetadataMapperTests
    {
        [TestFixture]
        public class MapMethod
        {
            [Test]
            public void MapsString()
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

                var result = sut.AsContentType<StringResult>();

                Assert.AreEqual(result.Value, "123");
            }

            [Test]
            public void MapsEnumerable()
            {
                var sut = new ContentData()
                {
                    MetaData = new ContentMetaData[]
                    {
                        new ContentMetaData()
                        {
                            Name = "Values",
                            Text = "123;234",
                            Separator = ";",
                            AllowMultiple = true
                        }
                    }
                };

                var result = sut.AsContentType<EnumerableResult>();

                Assert.AreEqual(result.Values.First(), "123");
                Assert.AreEqual(result.Values.Skip(1).First(), "234");
            }

            public class StringResult
            {
                [Metadata("Value")]
                public string Value { get; set; }
            }

            public class EnumerableResult
            {
                [Metadata("Values")]
                public IEnumerable<string> Values { get; set; }
            }
        }
    }
}
