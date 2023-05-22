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
    public class EAVValueToFloatConverter : IEAVValueConverter<float>
    {
        public float Convert(string value)
        {
            if(float.TryParse(value, CultureInfo.InvariantCulture, out var floatValue)) return floatValue;

            throw new InvalidConversionException(value, typeof(float));
        }

        public string ConvertBack(float value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
