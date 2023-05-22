using Kuchulem.DotNet.EntityAttributeValue.Converters.EAVValueConverters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Tests.Converters
{
    public class EAVValueConverters_Should
    {
        [Test]
        public void ConvertToString()
        {
            var converter = new EAVValueToStringConverter();

            var expected = "Lorem Ipsum";

            Assert.That(converter.Convert(expected), Is.EqualTo(expected));
        }

        [Test]
        public void ConvertToInt()
        {
            var converter = new EAVValueToIntConverter();

            var expected = 156;

            Assert.That(converter.Convert(converter.ConvertBack(expected)), Is.EqualTo(expected));
        }

        [Test]
        public void ConvertToBool_True()
        {
            var converter = new EAVValueToBoolConverter();

            var expected = true;

            Assert.That(converter.Convert(converter.ConvertBack(expected)), Is.True);
        }

        [Test]
        public void ConvertToBool_False()
        {
            var converter = new EAVValueToBoolConverter();

            var expected = false;

            Assert.That(converter.Convert(converter.ConvertBack(expected)), Is.False);
        }

        [Test]
        public void ConvertToDouble()
        {
            var converter = new EAVValueToDoubleConverter();

            var expected = 3.1415;

            Assert.That(converter.Convert(converter.ConvertBack(expected)), Is.EqualTo(expected));
        }

        [Test]
        public void ConvertToFloat()
        {
            var converter = new EAVValueToFloatConverter();

            var expected = 3.1415f;

            Assert.That(converter.Convert(converter.ConvertBack(expected)), Is.EqualTo(expected));
        }

        [Test]
        public void ConvertToLong()
        {
            var converter = new EAVValueToLongConverter();

            long expected = 3;

            Assert.That(converter.Convert(converter.ConvertBack(expected)), Is.EqualTo(expected));
        }

        [Test]
        public void ConvertToDateTime()
        {
            var converter = new EAVValueToDateTimeConverter();

            var expected = new DateTime(2023, 5, 18, 21, 17, 35);

            Assert.That(converter.Convert(converter.ConvertBack(expected)), Is.EqualTo(expected));
        }

        [Test]
        public void ConvertListToString()
        {
            var converter = new EAVValueListToRawValueConverter();

            var expected = new List<string>()
            {
                "lorem",
                "ipsum",
                "dolore",
                "sit",
            };

            var converted = converter.Convert(expected);

            Assert.That(converted, Is.InstanceOf<string>());
        }

        [Test]
        public void ConvertBackStringToList()
        {
            var converter = new EAVValueListToRawValueConverter();

            var expected = new List<string>()
            {
                "lorem",
                "ipsum",
                "dolore",
                "sit",
            } as IEnumerable<string>;

            var converted = converter.Convert(expected);
            var convertedBack = converter.ConvertBack(converted);

            Assert.That(convertedBack, Is.EqualTo(expected));
        }
    }
}
