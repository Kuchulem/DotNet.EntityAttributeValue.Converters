using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using Kuchulem.DotNet.EntityAttributeValue.Converters.EAVValueConverters;
using Kuchulem.DotNet.EntityAttributeValue.Tests.Converters.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Converters.Tests.EavConverterTests
{
    public class TryConvertValue_Should
    {
        private IEavRawValueConverterProvider provider;

        [SetUp]
        public void SetUp() 
        {
            provider = new EavRawValueConverterProvider();
        }

        [Test]
        public void ConvertStringValue()
        {
            provider.Register(a => a.ValueKind == EavValueKind.String, new EavRawValueToStringConverter());

            var value = new MockValue
            {
                Attribute = new MockAttribute
                {
                    AttributeName = "value",
                    Source = null,
                    ValueKind = EavValueKind.String,
                    ValueListKind = EavValueListKind.None
                },
                Entity = null,
                RawValue = "lorem"
            };

            var converter = new EavConverter(provider);
            var result = converter.TryConvertValue(value, out var converted);

            Assert.That(result, Is.True);
            Assert.That(converted, Is.EqualTo("lorem"));
        }

        [Test]
        public void ConvertBoolValue()
        {
            provider.Register(a => a.ValueKind == EavValueKind.Boolean, new EavRawValueToBoolConverter());

            var expected = true;

            var value = new MockValue
            {
                Attribute = new MockAttribute
                {
                    AttributeName = "value",
                    Source = null,
                    ValueKind = EavValueKind.Boolean,
                    ValueListKind = EavValueListKind.None
                },
                Entity = null,
                RawValue = expected.ToString(CultureInfo.InvariantCulture)
            };

            var converter = new EavConverter(provider);
            var result = converter.TryConvertValue(value, out var converted);

            Assert.That(result, Is.True);
            Assert.That(converted, Is.EqualTo(expected));
        }

        [Test]
        public void ConvertDoubleValue()
        {
            provider.Register(a => a.ValueKind == EavValueKind.Number, new EavRawValueToDoubleConverter());

            var expected = 2.3;

            var value = new MockValue
            {
                Attribute = new MockAttribute
                {
                    AttributeName = "value",
                    Source = null,
                    ValueKind = EavValueKind.Number,
                    ValueListKind = EavValueListKind.None
                },
                Entity = null,
                RawValue = expected.ToString(CultureInfo.InvariantCulture)
            };

            var converter = new EavConverter(provider);
            var result = converter.TryConvertValue(value, out var converted);

            Assert.That(result, Is.True);
            Assert.That(converted, Is.EqualTo(expected));
        }

        [Test]
        public void ConvertFloatValue()
        {
            provider.Register(a => a.ValueKind == EavValueKind.Number, new EavRawValueToFloatConverter());

            var expected = 2.3f;

            var value = new MockValue
            {
                Attribute = new MockAttribute
                {
                    AttributeName = "value",
                    Source = null,
                    ValueKind = EavValueKind.Number,
                    ValueListKind = EavValueListKind.None
                },
                Entity = null,
                RawValue = expected.ToString(CultureInfo.InvariantCulture)
            };

            var converter = new EavConverter(provider);
            var result = converter.TryConvertValue(value, out var converted);

            Assert.That(result, Is.True);
            Assert.That(converted, Is.EqualTo(expected));
        }

        [Test]
        public void ConvertIntValue()
        {
            provider.Register(a => a.ValueKind == EavValueKind.Integer, new EavRawValueToIntConverter());

            int expected = 2;

            var value = new MockValue
            {
                Attribute = new MockAttribute
                {
                    AttributeName = "value",
                    Source = null,
                    ValueKind = EavValueKind.Integer,
                    ValueListKind = EavValueListKind.None
                },
                Entity = null,
                RawValue = expected.ToString(CultureInfo.InvariantCulture)
            };

            var converter = new EavConverter(provider);
            var result = converter.TryConvertValue(value, out var converted);

            Assert.That(result, Is.True);
            Assert.That(converted, Is.EqualTo(expected));
        }

        [Test]
        public void ConvertLongValue()
        {
            provider.Register(a => a.ValueKind == EavValueKind.Integer, new EavRawValueToLongConverter());

            long expected = 2;

            var value = new MockValue
            {
                Attribute = new MockAttribute
                {
                    AttributeName = "value",
                    Source = null,
                    ValueKind = EavValueKind.Integer,
                    ValueListKind = EavValueListKind.None
                },
                Entity = null,
                RawValue = expected.ToString(CultureInfo.InvariantCulture)
            };

            var converter = new EavConverter(provider);
            var result = converter.TryConvertValue(value, out var converted);

            Assert.That(result, Is.True);
            Assert.That(converted, Is.EqualTo(expected));
        }
    }
}
