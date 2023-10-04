using Kuchulem.DotNet.EntityAttributeValue.Converters.EAVValueConverters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Converters.Tests
{
    public class EavRawValueConverter_Should_Throw_When
    {
        [Test]
        public void InvalidInt()
        {
            var converter = new EavRawValueToIntConverter();

            Assert.That(() => converter.Convert("lorem ipsum"), Throws.Exception.InstanceOf<InvalidCastException>());
        }

        [Test]
        public void InvalidBool()
        {
            var converter = new EavRawValueToBoolConverter();

            Assert.That(() => converter.Convert("lorem ipsum"), Throws.Exception.InstanceOf<InvalidCastException>());
        }

        [Test]
        public void InvalidDateTime()
        {
            var converter = new EavRawValueToDateTimeConverter();

            Assert.That(() => converter.Convert("lorem ipsum"), Throws.Exception.InstanceOf<InvalidCastException>());
        }

        [Test]
        public void InvalidDouble()
        {
            var converter = new EavRawValueToDoubleConverter();

            Assert.That(() => converter.Convert("lorem ipsum"), Throws.Exception.InstanceOf<InvalidCastException>());
        }

        [Test]
        public void InvalidFloat()
        {
            var converter = new EavRawValueToFloatConverter();

            Assert.That(() => converter.Convert("lorem ipsum"), Throws.Exception.InstanceOf<InvalidCastException>());
        }

        [Test]
        public void InvalidLong()
        {
            var converter = new EavRawValueToLongConverter();

            Assert.That(() => converter.Convert("lorem ipsum"), Throws.Exception.InstanceOf<InvalidCastException>());
        }
    }
}
