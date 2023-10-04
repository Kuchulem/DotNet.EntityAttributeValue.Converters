using Kuchulem.DotNet.EntityAttributeValue.Converters.EAVValueConverters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Converters.Tests
{
    public class EavRawValueConverters_Should
    {
        [Test]
        public void ConvertToString()
        {
            var converter = new EavRawValueToStringConverter();

            var expected = "Lorem Ipsum";

            Assert.That(converter.Convert(expected), Is.EqualTo(expected));
        }

        [Test]
        public void ConvertToInt()
        {
            var converter = new EavRawValueToIntConverter();

            var expected = 156;

            Assert.That(converter.Convert(converter.ConvertBack(expected)), Is.EqualTo(expected));
        }

        [Test]
        public void ConvertToBool_True()
        {
            var converter = new EavRawValueToBoolConverter();

            var expected = true;

            Assert.That(converter.Convert(converter.ConvertBack(expected)), Is.True);
        }

        [Test]
        public void ConvertToBool_False()
        {
            var converter = new EavRawValueToBoolConverter();

            var expected = false;

            Assert.That(converter.Convert(converter.ConvertBack(expected)), Is.False);
        }

        [Test]
        public void ConvertToDouble()
        {
            var converter = new EavRawValueToDoubleConverter();

            var expected = 3.1415;

            Assert.That(converter.Convert(converter.ConvertBack(expected)), Is.EqualTo(expected));
        }

        [Test]
        public void ConvertToFloat()
        {
            var converter = new EavRawValueToFloatConverter();

            var expected = 3.1415f;

            Assert.That(converter.Convert(converter.ConvertBack(expected)), Is.EqualTo(expected));
        }

        [Test]
        public void ConvertToLong()
        {
            var converter = new EavRawValueToLongConverter();

            long expected = 3;

            Assert.That(converter.Convert(converter.ConvertBack(expected)), Is.EqualTo(expected));
        }

        [Test]
        public void ConvertToDateTime()
        {
            var converter = new EavRawValueToDateTimeConverter();

            var expected = new DateTime(2023, 5, 18, 21, 17, 35);

            Assert.That(converter.Convert(converter.ConvertBack(expected)), Is.EqualTo(expected));
        }
    }
}
