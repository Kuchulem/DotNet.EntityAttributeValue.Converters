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
    public class EAVValueToLongConverter : IEAVValueConverter<long>
    {
        public long Convert(string value)
        {
            if(long.TryParse(value, CultureInfo.InvariantCulture, out var valueLong)) 
                return valueLong;

            throw new InvalidConversionException(value, typeof(long));
        }

        public string ConvertBack(long value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
