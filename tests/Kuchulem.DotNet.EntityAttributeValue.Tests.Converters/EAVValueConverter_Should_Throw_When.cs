using Kuchulem.DotNet.EntityAttributeValue.Converters.EAVValueConverters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Tests.Converters
{
    public class EAVValueConverter_Should_Throw_When
    {
        [Test]
        public void InvalidInt()
        {
            var converter = new EAVValueToIntConverter();

            Assert.That(() => converter.Convert("lorem ipsum"), Throws.Exception.InstanceOf<InvalidCastException>());
        }

        [Test]
        public void InvalidBool()
        {
            var converter = new EAVValueToBoolConverter();

            Assert.That(() => converter.Convert("lorem ipsum"), Throws.Exception.InstanceOf<InvalidCastException>());
        }

        [Test]
        public void InvalidDateTime()
        {
            var converter = new EAVValueToDateTimeConverter();

            Assert.That(() => converter.Convert("lorem ipsum"), Throws.Exception.InstanceOf<InvalidCastException>());
        }

        [Test]
        public void InvalidDouble()
        {
            var converter = new EAVValueToDoubleConverter();

            Assert.That(() => converter.Convert("lorem ipsum"), Throws.Exception.InstanceOf<InvalidCastException>());
        }

        [Test]
        public void InvalidFloat()
        {
            var converter = new EAVValueToFloatConverter();

            Assert.That(() => converter.Convert("lorem ipsum"), Throws.Exception.InstanceOf<InvalidCastException>());
        }

        [Test]
        public void InvalidLong()
        {
            var converter = new EAVValueToLongConverter();

            Assert.That(() => converter.Convert("lorem ipsum"), Throws.Exception.InstanceOf<InvalidCastException>());
        }
    }
}
