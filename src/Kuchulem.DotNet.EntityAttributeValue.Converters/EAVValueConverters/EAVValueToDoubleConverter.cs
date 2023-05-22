using Kuchulem.DotNet.EntityAttributeValue.Abstractions;
using Kuchulem.DotNet.EntityAttributeValue.Converters.Exceptions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kuchulem.DotNet.EntityAttributeValue.Converters.EAVValueConverters
{
    public class EAVValueToDoubleConverter : IEAVValueConverter<double>
    {
        public double Convert(string value)
        {
            if (double.TryParse(value, CultureInfo.InvariantCulture, out double result)) { return result; }

            throw new InvalidConversionException(value, typeof(double));
        }

        public string ConvertBack(double value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
